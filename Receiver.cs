using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace UnderwaterVideo
{
    public class FrameReceivedEventArgs : EventArgs
    {
        public Bitmap receivedFrame { get; private set; }

        public FrameReceivedEventArgs(Bitmap frame)
        {
            receivedFrame = frame;
        }
    }

    public class Receiver
    {
        #region Properties

        Encoder encoder;
        WaveIn waveIn;

        bool isRunning = false;
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
        }

        #region Buffers

        short[] buffer;
        int count = 0;
        int writePosition = 0;
        int readPosition = 0;
        int isSearching = 0;

        List<double[]> slices;

        #endregion

        double risePeekRatio = 2;
        int frameWidthTolerance = 5;
        double previousWeight = 0.0;
        double prevRisePosition = 0;

        double[] response;

        #endregion

        #region Constructor

        public Receiver(Encoder encoder, int waveInDeviceNumber, int bufferMultiplier)
        {
            if (encoder == null)
            {
                throw new ArgumentNullException("encoder");
            }
            else
            {
                this.encoder = encoder;
                waveIn = new WaveIn();
                waveIn.WaveFormat = new WaveFormat(encoder.SampleRate, 1);
                waveIn.DeviceNumber = waveInDeviceNumber;
                waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
                waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStopped);

                buffer = new short[encoder.FFTSize * encoder.FrameSize.Width * bufferMultiplier];
                slices = new List<double[]>();

                response = new double[encoder.FFTSize];
                for (int i = 0; i < response.Length; i++)
                    response[i] = 1.0;
            }
        }                        

        #endregion

        #region Methods

        public static double[] Correlation(double[] x, double[] y)
        {
            int dataLength = x.Length;
            int totalLength = x.Length + y.Length;
            double newCorrelation;
            int yShift = dataLength - 1;
            int count = 0;

            int lb, rb;
            double[] result = new double[totalLength + 1];

            for (int xShift = 0; xShift < totalLength - 1; xShift++)
            {
                lb = Math.Max(xShift, yShift);
                rb = Math.Min(yShift + dataLength, xShift + dataLength);

                newCorrelation = 0.0;
                for (int i = lb; i < rb; i++)
                {
                    newCorrelation += x[i - xShift] * y[i - yShift];
                }

                result[count] = newCorrelation / (rb - lb);
                count++;
            }
            return result;
        }

        public void Start()
        {
            if (!isRunning)
            {
                waveIn.StartRecording();
                isRunning = true;

                if (ReceivingStarted != null)
                    ReceivingStarted(this, new EventArgs());
            }
            else
            {
                throw new ApplicationException("Transmission in process");
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                waveIn.StopRecording();
            }
            else
            {
                throw new ApplicationException("Transmission not in running mode");
            }
        }

        private void Search()
        {
            int sliceIndex = 0;
            int frameWidth = encoder.FrameSize.Width;
            int minSlicesToSearch = Convert.ToInt32((frameWidth + 5) * 2);
            int sliceSize = encoder.FFTSize;
            double weight;
            int lastRisePosition = 0;
            int prevRisePosition = 0;
            
            while ((slices.Count > minSlicesToSearch) && (sliceIndex < slices.Count))
            {
                weight = 0.0;
                for (int i = 0; i < sliceSize; i++)
                    weight += Math.Abs(slices[sliceIndex][i]);

                double ratio = weight / previousWeight;

                if ((ratio >= risePeekRatio) && (sliceIndex - prevRisePosition > frameWidth))
                {
                    prevRisePosition = lastRisePosition;
                    lastRisePosition = sliceIndex;

                    if (lastRisePosition + (frameWidth + 5) < slices.Count)
                    {
                        double[][] samples = new double[frameWidth + 5][];
                        for (int i = 0; i < frameWidth + 5; i++)
                        {
                            samples[i] = new double[sliceSize];
                            Array.Copy(slices[lastRisePosition + i], samples[i], sliceSize);
                        }

                        slices.RemoveRange(0, sliceIndex);
                        lastRisePosition = 0;

                        if (FrameReceived != null)
                            FrameReceived(this, new FrameReceivedEventArgs(encoder.DecodeEx(samples, 5)));                            

                        lastRisePosition = sliceIndex;
                    }
                    
                }

                sliceIndex++;
                previousWeight = weight;
            }

            Interlocked.Decrement(ref isSearching);
        }
        
        #endregion

        #region Handlers

        #region WaveIn

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            int samplesLength = e.BytesRecorded / 2;

            for (int i = 0; i < samplesLength; i++)
            {
                buffer[writePosition] = BitConverter.ToInt16(e.Buffer, i * 2);
                count++;

                if (++writePosition >= buffer.Length)
                    writePosition = 0;                
            }

            int fftSize = encoder.FFTSize;
            double[] slice = new double[fftSize];
            double[] mask = encoder.FrequencyMask;
            alglib.complex[] sliceC;

            double[] sliceA;
            int slicesAdded = 0;
            while (count > fftSize)
            {
                for (int i = 0; i < fftSize; i++)
                {
                    slice[i] = buffer[readPosition];
                    count--;

                    if (++readPosition >= buffer.Length)
                        readPosition = 0;
                }

                alglib.fftr1d(slice, out sliceC);

                sliceA = new double[fftSize];
                for (int i = 0; i < sliceC.Length; i++)
                {
                    sliceA[i] = alglib.math.abscomplex(sliceC[i]) * mask[i];
                }
                
                slices.Add(sliceA);
                slicesAdded++;
            }

            if (Interlocked.CompareExchange(ref isSearching, 1, 0) == 0)
            {
                if (slices.Count > encoder.FrameSize.Width * 5)
                    slices.RemoveRange(0, slicesAdded);
                
                Search();
            }            
        }

        private void waveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            isRunning = false;

            if (ReceivingStopped != null)
                ReceivingStopped(this, new EventArgs());
        }        

        #endregion

        #endregion

        #region Events

        public EventHandler ReceivingStarted;
        public EventHandler ReceivingStopped;
        public EventHandler<FrameReceivedEventArgs> FrameReceived;

        #endregion
    }
}