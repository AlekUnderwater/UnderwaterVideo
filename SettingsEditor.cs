using System;
using System.Windows.Forms;

namespace UnderwaterVideo
{
    public partial class SettingsEditor : Form
    {
        #region Properties

        public SettingsContainer Value
        {
            get
            {
                SettingsContainer result = new SettingsContainer();
                result.SampleRateHz = Convert.ToInt32(sampleRateCbx.SelectedItem.ToString());
                result.FFTSize = Convert.ToInt32(FFTSizeCbx.SelectedItem.ToString());

                return result;
            }
            set
            {
                int idx = -1;
                idx = sampleRateCbx.Items.IndexOf(value.SampleRateHz.ToString());
                if (idx >= 0) sampleRateCbx.SelectedIndex = idx;

                idx = FFTSizeCbx.Items.IndexOf(value.FFTSize.ToString());
                if (idx >= 0) FFTSizeCbx.SelectedIndex = idx;

            }
        }

        #endregion

        #region Constructor

        public SettingsEditor()
        {
            InitializeComponent();

            sampleRateCbx.SelectedIndex = 0;
            FFTSizeCbx.SelectedIndex = 0;
        }

        #endregion
    }
}
