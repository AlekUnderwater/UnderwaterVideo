using AForge.Imaging.Filters;
using System;
using System.Drawing;

namespace UnderwaterVideo
{
    public class Encoder
    {
        #region Properties

        int sampleRate = 96000;
        public int SampleRate
        {
            get { return sampleRate; }
            set 
            {
                if (value > 0)
                {
                    sampleRate = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("SampleRate", "Value must be greater than zero");
                }
            }
        }

        Size frameSize = new Size(120, 120);
        public Size FrameSize
        {
            get { return frameSize; }
            set 
            {
                if (value.Height < fftSize)
                {
                    frameSize = value;
                    resizer.NewWidth = frameSize.Width;
                    resizer.NewHeight = frameSize.Height;
                    randomizerMask = BuildSequence(randomizerSequence);
                    frequencyMask = Abs(randomizerMask);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("FrameSize.Height", "Value must be lower than FFT size");
                }
            }
        }

        int fftSize = 512;
        public int FFTSize
        {
            get { return fftSize; }
            set 
            {
                if ((value & (value - 1)) == 0)
                {
                    fftSize = value;
                    randomizerMask = BuildSequence(randomizerSequence);
                    frequencyMask = Abs(randomizerMask);
                }
                else
                {
                    throw new ArgumentException("parameter must be a power of 2", "FFTSize");
                }
            }
        }

        int startLine = 100;
        public int StartLine
        {
            get { return startLine; }
            set 
            {
                if (value + frameSize.Height <= fftSize)
                {
                    startLine = value;
                    randomizerMask = BuildSequence(randomizerSequence);
                    frequencyMask = Abs(randomizerMask);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("StartLine", 
                        string.Format("Parameter must be in range from {0} to {1} and even for specified FFT size and frame height", 0, fftSize - frameSize.Height));
                }
            }
        }

        ResizeBilinear resizer;

        int[] randomizerSequence;

        double[] randomizerMask;
        public double[] Mask
        {
            get
            {
                return randomizerMask;
            }
        }

        double[] frequencyMask;
        public double[] FrequencyMask
        {
            get { return frequencyMask; }
        }                    

        #endregion

        #region Constructor

        public Encoder(int sampleRate, int FFTSize, Size frameSize, int startLine, int[] randomizer)
        {
            randomizerSequence = randomizer;
            randomizerMask = BuildSequence(randomizerSequence);
            frequencyMask = Abs(randomizerMask);

            this.SampleRate = sampleRate;
            this.FFTSize = FFTSize;
            resizer = new ResizeBilinear(frameSize.Width, frameSize.Height);
            this.FrameSize = frameSize;
            this.StartLine = startLine;
        }

        #endregion

        #region Methods

        private double[] Abs(double[] source)
        {
            double[] result = new double[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = Math.Abs(source[i]);
            }

            return result;
        }

        private double[] BuildSequence(int[] source)
        {
            double[] result = new double[fftSize];

            for (int i = 0; i < source.Length; i++)
            {
                result[i + startLine] = source[i] * 2 - 1;
            }

            return result;
        }

        public double[] Encode(Bitmap source)
        {
            Bitmap frame;

            if (source.PixelFormat != System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                frame = Grayscale.CommonAlgorithms.RMY.Apply(source);
            else
                frame = source;

            if (!frame.Size.Equals(frameSize))
                frame = resizer.Apply(frame);       

            double[] samples = new double[fftSize * frameSize.Width];
            alglib.complex[] slice = new alglib.complex[fftSize];
            double maxSlice;
            int sampleIndex = 0;

            int colsCount = frameSize.Width;
            int startRow = startLine;
            int endRow = startRow + frameSize.Height;

            for (int x = 0; x < colsCount; x++)
            {
                for (int y = startRow; y < endRow; y++)
                    slice[y].x = (frame.GetPixel(x, frameSize.Height - (y - startRow) - 1).R / 255.0) * short.MaxValue;

                for (int y = 0; y < fftSize; y++)
                    slice[y].x *= randomizerMask[y];

                alglib.fftc1dinv(ref slice);

                maxSlice = double.MinValue;
                for (int y = 0; y < slice.Length; y++)
                    if (Math.Abs(slice[y].x) > maxSlice)
                        maxSlice = Math.Abs(slice[y].x);
                
                for (int i = 0; i < slice.Length; i++)
                {
                    samples[sampleIndex] = (short)Math.Round(slice[i].x * short.MaxValue / maxSlice);
                    sampleIndex++;
                }    
            }

            return samples;
        }

        public double[] Encode(Bitmap source, int pilotPrefixWidth)
        {
            Bitmap frame;

            if (source.PixelFormat != System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                frame = Grayscale.CommonAlgorithms.RMY.Apply(source);
            else
                frame = source;

            if (!frame.Size.Equals(frameSize))
                frame = resizer.Apply(frame);           

            double[] samples = new double[fftSize * (frameSize.Width + pilotPrefixWidth)];
            alglib.complex[] slice = new alglib.complex[fftSize];
            double maxSlice;
            int sampleIndex = 0;

            int colsCount = frameSize.Width;
            int startRow = startLine;
            int endRow = startRow + frameSize.Height;


            for (int x = 0; x < pilotPrefixWidth; x++)
            {
                for (int y = startRow; y < endRow; y++)
                    slice[y].x = short.MaxValue;

                for (int y = 0; y < fftSize; y++)
                {
                    slice[y].x *= randomizerMask[y];
                    slice[y].y = slice[y].x;
                }

                alglib.fftc1dinv(ref slice);

                maxSlice = double.MinValue;
                for (int y = 0; y < slice.Length; y++)
                    if (Math.Abs(slice[y].x) > maxSlice)
                        maxSlice = Math.Abs(slice[y].x);

                for (int i = 0; i < slice.Length; i++)
                {
                    samples[sampleIndex] = (short)Math.Round(slice[i].x * short.MaxValue / maxSlice);
                    sampleIndex++;
                }
            }

            for (int x = 0; x < colsCount; x++)
            {
                for (int y = startRow; y < endRow; y++)
                    slice[y].x = (frame.GetPixel(x, frameSize.Height - (y - startRow) - 1).R / 255.0) * short.MaxValue;                    

                for (int y = 0; y < fftSize; y++)
                {
                    slice[y].x *= randomizerMask[y];
                    slice[y].y = slice[y].x;
                }

                alglib.fftc1dinv(ref slice);

                maxSlice = double.MinValue;
                for (int y = 0; y < slice.Length; y++)
                    if (Math.Abs(slice[y].x) > maxSlice)
                        maxSlice = Math.Abs(slice[y].x);

                for (int i = 0; i < slice.Length; i++)
                {
                    samples[sampleIndex] = (short)Math.Round(slice[i].x * short.MaxValue / maxSlice);
                    sampleIndex++;
                }
            }

            return samples;
        }        

        public Bitmap Decode(double[] samples)
        {
            int colCount = samples.Length / fftSize;
            if (colCount == frameSize.Width)
            {
                int rowCount = frameSize.Height;
                Bitmap temp = new Bitmap(colCount, rowCount);

                double[] slice = new double[fftSize];
                alglib.complex[] sliceC = new alglib.complex[fftSize];
                int samplesCount = 0;
                byte component;

                int decodeStart = startLine;
                int decodeEnd = startLine + rowCount;

                double maxSlice;

                for (int x = 0; x < colCount; x++)
                {
                    for (int y = 0; y < fftSize; y++)
                    {
                        slice[y] = samples[samplesCount];
                        samplesCount++;
                    }

                    alglib.fftr1d(slice, out sliceC);

                    maxSlice = double.MinValue;
                    for (int y = decodeStart; y < decodeEnd; y++)
                        if (alglib.math.abscomplex(sliceC[y].x) > maxSlice)
                            maxSlice = alglib.math.abscomplex(sliceC[y].x);

                    int offset = temp.Height + decodeStart - 1;

                    for (int y = decodeStart; y < decodeEnd; y++)
                    {
                        component = (byte)(255.0 * alglib.math.abscomplex(sliceC[y].x) / maxSlice);
                        temp.SetPixel(x, offset - y, Color.FromArgb(component, component, component));
                    }
                }              

                return temp;

            }
            else
            {
                throw new ApplicationException("Specified array length error");
            }
        }

        public Bitmap Decode(double[] samples, int measureCols)
        {
            int colCount = samples.Length / fftSize;
            if (colCount == frameSize.Width + measureCols)
            {
                int rowCount = frameSize.Height;
                Bitmap temp = new Bitmap(colCount, rowCount);

                double[] slice = new double[fftSize];
                alglib.complex[] sliceC = new alglib.complex[fftSize];
                int samplesCount = 0;
                byte component;

                int decodeStart = startLine;
                int decodeEnd = startLine + rowCount;

                double maxSlice;

                for (int x = 0; x < colCount; x++)
                {
                    for (int y = 0; y < fftSize; y++)
                    {
                        slice[y] = samples[samplesCount];
                        samplesCount++;
                    }

                    alglib.fftr1d(slice, out sliceC);

                    maxSlice = double.MinValue;
                    for (int y = decodeStart; y < decodeEnd; y++)
                        if (alglib.math.abscomplex(sliceC[y].x) > maxSlice)
                            maxSlice = alglib.math.abscomplex(sliceC[y].x);

                    int offset = temp.Height + decodeStart - 1;

                    for (int y = decodeStart; y < decodeEnd; y++)
                    {
                        component = (byte)(255.0 * alglib.math.abscomplex(sliceC[y].x) / maxSlice);
                        temp.SetPixel(x, offset - y, Color.FromArgb(component, component, component));
                    }
                }
                return temp;

            }
            else
            {
                throw new ApplicationException("Specified array length error");
            }
        }
        
        public Bitmap DecodeEx(double[] samples)
        {
            int colCount = samples.Length / fftSize;
            if (colCount == frameSize.Width)
            {
                int rowCount = frameSize.Height;
                Bitmap temp = new Bitmap(colCount, rowCount);

                double[] slice = new double[fftSize];
                int samplesCount = 0;
                byte component;

                int decodeStart = startLine;
                int decodeEnd = startLine + rowCount;

                double maxSlice;

                for (int x = 0; x < colCount; x++)
                {
                    for (int y = 0; y < fftSize; y++)
                    {
                        slice[y] = samples[samplesCount];
                        samplesCount++;
                    }

                    maxSlice = double.MinValue;
                    for (int y = decodeStart; y < decodeEnd; y++)
                        if (Math.Abs(slice[y]) > maxSlice)
                            maxSlice = Math.Abs(slice[y]);

                    int offset = temp.Height + decodeStart - 1;

                    for (int y = decodeStart; y < decodeEnd; y++)
                    {
                        component = (byte)(255.0 * Math.Abs(slice[y]) / maxSlice);
                        temp.SetPixel(x, offset - y, Color.FromArgb(component, component, component));
                    }
                }             

                return temp;

            }
            else
            {
                throw new ApplicationException("Specified array length error");
            }
        }

        public Bitmap DecodeEx(double[][] samples)
        {
            double max = double.MinValue;
            int cols = frameSize.Width;
            int rows = frameSize.Height;
            byte component;

            int decodeStart = startLine;
            int decodeEnd = startLine + rows;            

            Bitmap result = new Bitmap(cols, rows);
            int offset = result.Height + decodeStart - 1;

            for (int x = 0; x < cols; x++)
            {
                max = double.MinValue;
                for (int y = decodeStart; y < decodeEnd; y++)
                    if (samples[x][y] > max)
                        max = samples[x][y];

                for (int y = decodeStart; y < decodeEnd; y++)
                {
                    component = (byte)(255.0 * Math.Abs(samples[x][y]) / max);
                    result.SetPixel(x, offset - y, Color.FromArgb(component, component, component));
                }
            }

            return result;
        }

        public Bitmap DecodeEx(double[][] samples, int measureCols)
        {
            double max = double.MinValue;
            int cols = frameSize.Width;
            int rows = frameSize.Height;
            byte component;

            int decodeStart = startLine;
            int decodeEnd = startLine + rows;

            #region Build align curve

            double[] response = new double[fftSize];
            
            // approximate measurement signal
            for (int x = 0; x < measureCols; x++)
            {
                for (int y = 0; y < fftSize; y++)
                    response[y] = (response[y] + samples[x][y]) / 2.0;
            }

            for (int y = 0; y < fftSize; y++)
            {
                response[y] /= 255;
            }


            // smooth response
            int winSize = 3;
            double winMean = 0;
            for (int i = winSize; i < fftSize - winSize; i++)
            {
                winMean = 0;
                for (int j = 0; j < winSize; j++)                
                    winMean += response[i + j];

                winMean /= winSize;
                response[i] = winMean;
            }

            #endregion

            Bitmap result = new Bitmap(cols, rows);
            int offset = result.Height + decodeStart - 1;

            for (int x = 0; x < cols; x++)
            {
                max = double.MinValue;
                for (int y = decodeStart; y < decodeEnd; y++)
                {
                    samples[x][y] /= response[y];

                    if (samples[x][y] > max)
                        max = samples[x][y];
                }

                for (int y = decodeStart; y < decodeEnd; y++)
                {
                    component = (byte)(255.0 * Math.Abs(samples[x][y]) / max);
                    result.SetPixel(x, offset - y, Color.FromArgb(component, component, component));
                }
            }
           
            return result;

        }
        
        #endregion

    }
}
