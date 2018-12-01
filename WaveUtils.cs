using System;

namespace UnderwaterVideo
{
    [Flags]
    public enum FadeType
    {
        fadeIn,
        fadeOut,
    }

    public static class WaveUtils
    {      

        /// <summary>
        /// Calculates duration for signal with specified sampleRate and number of samples
        /// </summary>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="sampleCount">number of samples</param>
        /// <returns>Duration in ms</returns>
        public static double GetDurationMs(double sampleRate, int sampleCount)
        {
            return sampleCount * 1000.0 / sampleRate;
        }

        /// <summary>
        /// Calculates number of samples for specified sample rate and duration in ms
        /// </summary>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="durationMs">Duration in ms</param>
        /// <returns>number of samples</returns>
        public static int GetSampleCount(double sampleRate, double durationMs)
        {
            return Convert.ToInt32(durationMs * sampleRate / 1000.0);
        }

        /// <summary>
        /// Calculates maximal absolute value in double array
        /// </summary>
        /// <param name="source">source array</param>
        /// <returns>maximal absolute value</returns>
        public static double GetMaxAmplitude(double[] source)
        {
            double max = double.MinValue;
            double temp;
            for (int i = 0; i < source.Length; i++)
            {
                temp = Math.Abs(source[i]);
                if (temp > max)
                    max = temp;
            }

            return max;
        }

        /// <summary>
        /// Multiplies samples by specified value
        /// </summary>
        /// <param name="source">Source signal</param>
        /// <param name="multiplier">Multiplier</param>
        public static void Multiply(ref double[] source, double multiplier)
        {
            for (int i = 0; i < source.Length; i++)
            {
                source[i] *= multiplier;
            }
        }

        /// <summary>
        /// Shifts signal DC
        /// </summary>
        /// <param name="source">Source signal</param>
        /// <param name="shift">Shift value</param>
        public static void DCShift(ref double[] source, double shift)
        {
            for (int i = 0; i < source.Length; i++)
            {
                source[i] += shift;
            }
        }

        /// <summary>
        /// Signal normalization to [0..1] range
        /// </summary>
        /// <param name="source">Source signal</param>
        public static void Normalize(ref double[] source)
        {
            double maxValue = GetMaxAmplitude(source);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] /= maxValue;
            }
        }

        /// <summary>
        /// Signal normalization to signed 16-bit
        /// </summary>
        /// <param name="source">Source signal</param>
        /// <returns>Normalized to full signed 16-bit range</returns>
        public static short[] NormalizeToInt16(double[] source)
        {
            short[] result = new short[source.Length];
            double maxAmplitude = GetMaxAmplitude(source);

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = Convert.ToInt16(short.MaxValue * source[i] / maxAmplitude);
            }

            return result;
        }

        /// <summary>
        /// Signal normalization to signed 16-bit
        /// </summary>
        /// <param name="source">Source signal</param>
        /// <param name="scale">Scale 0..1</param>
        /// <returns>Normalized to full signed 16-bit range with scaling</returns>
        public static short[] NormalizeToInt16(double[] source, double scale)
        {
            if ((scale >= 0) && (scale <= 1.0))
            {

                short[] result = new short[source.Length];
                double maxAmplitude = GetMaxAmplitude(source);
                double scaleFactor = short.MaxValue * scale;

                for (int i = 0; i < source.Length; i++)
                    result[i] = Convert.ToInt16(scaleFactor * source[i] / maxAmplitude);

                return result;
            }
            else
            {
                throw new ArgumentOutOfRangeException("scale", "Value from 0.0 to 1.0 expected");
            }
        }

        /// <summary>
        /// Signal normalization to signed 32-bit
        /// </summary>
        /// <param name="source">Source signal</param>
        /// <returns>Normalized to full signed 32-bit range</returns>
        public static int[] NormalizeToInt32(double[] source)
        {
            int[] result = new int[source.Length];
            double maxAmplitude = GetMaxAmplitude(source);

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = Convert.ToInt32(int.MaxValue * source[i] / maxAmplitude);
            }

            return result;
        }

        /// <summary>
        /// Converts signal from double precision floating point to single
        /// </summary>
        /// <param name="source">Source signal</param>
        /// <returns>Signal, as sigle-precision floating point values array</returns>
        public static float[] ToSingle(double[] source)
        {
            float[] result = new float[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                result[i] = Convert.ToSingle(source[i]);
            }

            return result;
        }

        /// <summary>
        /// Calculates weighted total of two signals
        /// </summary>
        /// <param name="stSignal">1st signal</param>
        /// <param name="stWeight">1st signal weight</param>
        /// <param name="ndSignal">2nd signal</param>
        /// <param name="ndWeight">2nd signal weight</param>
        /// <returns>Weighted total of specified signals</returns>
        public static double[] MixSignals(double[] stSignal, double stWeight, double[] ndSignal, double ndWeight)
        {
            if (stSignal.Length != ndSignal.Length)
            {
                throw new ArgumentException("Signals must have the same sizes");
            }

            double[] result = new double[stSignal.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = stSignal[i] * stWeight + ndSignal[i] * ndWeight;
            }

            return result;
        }

        /// <summary>
        /// Mix signals with weights and shift
        /// </summary>
        /// <param name="stSignal">1st signal</param>
        /// <param name="stWeight">1st signal weight</param>
        /// <param name="ndSignal">2ng signal</param>
        /// <param name="ndWeight">2nd signal weight</param>
        /// <param name="ndShift">2nd signal to 1st signal relative shift in samples</param>
        /// <returns></returns>
        public static double[] MixSignals(double[] stSignal, double stWeight, double[] ndSignal, double ndWeight, int ndShift)
        {
            if (ndShift < 0)
            {
                throw new ArgumentOutOfRangeException("ndShift", "Parameter must be non-negative");
            }
            else
            {
                int resultLength = Math.Max(stSignal.Length, ndSignal.Length + ndShift);
                double[] result = new double[resultLength];

                double stSample;
                double ndSample;
                for (int i = 0; i < result.Length; i++)
                {
                    if (i < stSignal.Length)
                    {
                        stSample = stSignal[i] * stWeight;
                    }
                    else
                    {
                        stSample = 0.0;
                    }

                    if ((i >= ndShift) && (i < ndShift + ndSignal.Length))
                    {
                        ndSample = ndSignal[i - ndShift] * ndWeight;
                    }
                    else
                    {
                        ndSample = 0.0;
                    }

                    result[i] = stSample + ndSample;
                }

                return result;
            }
        }

        /// <summary>
        /// Calculates signal to noise ration for specified signal
        /// </summary>
        /// <param name="position">Position of maximum</param>
        /// <param name="source">Source signal</param>
        /// <returns>Signal to noise ration in dB</returns>
        public static double CalculateSNR(out int position, double[] source)
        {
            double s;
            double vMax = 0.0;
            double vMin = 0.0;
            double Sum = 0.0;
            int iMax = 0;
            int iMin = 0;

            double SNR;
            int length = source.Length;

            for (int i = 0; i < length; i++)
            {
                s = source[i];
                Sum = Sum + Math.Pow(s, 2);
                if (s > vMax)
                {
                    vMax = s;
                    iMax = i;
                }

                if (s < vMin)
                {
                    vMin = s;
                    iMin = i;
                }
            }

            s = Math.Abs(vMin);
            if (s > vMax)
            {
                vMax = s;
                iMax = iMin;
            }

            vMax = vMax * vMax;
            SNR = 10.0 * Math.Log10((vMax * length) / (Sum - vMax));
            position = iMax;

            return SNR;
        }

        /// <summary>
        /// Generates linear fade in and/or fade out effect for specified signal
        /// </summary>
        /// <param name="signal">Source signal</param>
        /// <param name="fadePart">a part of signal to fade in [0..1], where 1 - full signal length</param>
        /// <param name="fadeType"></param>
        public static void LinearFade(ref double[] signal, double fadePart, FadeType fadeType)
        {
            int fadeLength = (int)(signal.Length * fadePart);
            double delta = 1.0 / fadeLength;

            bool isFadeIn = ((fadeType & FadeType.fadeIn) == FadeType.fadeIn);
            bool isFadeOut = ((fadeType & FadeType.fadeOut) == FadeType.fadeOut);

            for (int i = 0; i < fadeLength; i++)
            {
                if (isFadeIn)
                {
                    signal[i] = (short)(signal[i] * delta * i);
                }

                if (isFadeOut)
                {
                    signal[signal.Length - i - 1] = (short)(signal[signal.Length - i - 1] * delta * i);
                }
            }
        }        

        public static int MSecToSamples(double mSec, double sampleRate)
        {
            return Convert.ToInt32(sampleRate * (mSec / 1000));
        }

        public static double[] MakeDelay(double[] source, int samples)
        {
            int resultLength = source.Length + samples;
            double[] result = new double[resultLength];

            Array.Copy(source, 0, result, samples, source.Length);

            return result;
        }

        public static double[] MixSignals(double[][] signals, double[] amplitudes)
        {
            int maxLength = 0;
            int signalsCount = signals.Length;

            if (signalsCount != amplitudes.Length)
                throw new ArgumentOutOfRangeException("Number of signals differs from number of amplitudes");

            for (int i = 0; i < signalsCount; i++)
                if (signals[i].Length > maxLength)
                    maxLength = signals[i].Length;

            double[] result = new double[maxLength];

            double temp;
            for (int i = 0; i < maxLength; i++)
            {
                temp = 0.0;
                for (int j = 0; j < signalsCount; j++)
                {
                    if (i < signals[j].Length)
                        temp += signals[j][i] * amplitudes[j];
                }

                result[i] = temp;
            }

            return result;
        }
    }
}
