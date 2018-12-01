using System;
using System.Text;

namespace UnderwaterVideo
{
    [Serializable]
    public class SettingsContainer
    {
        #region Properties

        public int SampleRateHz;
        public int FFTSize;
              
        #endregion

        #region Constructor

        public SettingsContainer()
        {
            SetDefaults();
        }

        #endregion

        #region Methods

        public void SetDefaults()
        {
            SampleRateHz = 96000;
            FFTSize = 512;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Settings:\r\nSampleRateHz: {0}\r\nFFTSize: {1}\r\n", SampleRateHz, FFTSize);
           
            return sb.ToString();
        }

        #endregion
    }
}
