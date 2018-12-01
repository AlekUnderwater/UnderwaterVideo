using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using NAudio.Wave;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace UnderwaterVideo
{
    public partial class MainForm : Form
    {
        #region Invokers

        private void InvokeSetImage(PictureBox pBox, Bitmap image)
        {
            if (pBox.InvokeRequired)
                pBox.Invoke((MethodInvoker)delegate { pBox.Image = image; });
            else
                pBox.Image = image;
        }

        private void InvokeSetChecked(ToolStrip strip, ToolStripButton btn, bool isChecked)
        {
            if (strip.InvokeRequired)
                strip.Invoke((MethodInvoker)delegate { btn.Checked = isChecked; });
            else
                btn.Checked = isChecked;
                    
        }

        private void InvokeSetEnable(ToolStrip strip, ToolStripItem btn, bool isEnabled)
        {
            if (strip.InvokeRequired)
                strip.Invoke((MethodInvoker)delegate { btn.Enabled = isEnabled; });
            else
                btn.Enabled = isEnabled;

        }


        #endregion

        #region Properties

        bool isRestart = false;
        Size outFrameSize = new Size(125, 120);

        volatile VideoCaptureDevice vDevice;
        FilterInfoCollection videoDevices;
        FiltersSequence framePostFilter;
        GammaCorrection gammaCorrector;
        ResizeBicubic resizer;

        Transmitter transmitter;
        Receiver receiver;
        Encoder encoder;


        bool receiverExists = false;
        int receiverBufferMultiplier = 5;


        Size webCamDesiredFrameSize = new Size(160, 120);
        Size frameSize = new Size(125, 120);
        
        int StartLine
        {
            get
            {
                return startFrequencyTrk.Value;
            }
            set
            {
                startFrequencyTrk.Value = value;
            }
        }

        double subcarrierSpacing
        {
            get
            {
                return settings.Data.SampleRateHz / settings.Data.FFTSize;
            }
        }

        double startFrequency
        {
            get
            {
                return startFrequencyTrk.Value * subcarrierSpacing;
            }
        }

        double endFrequency
        {
            get
            {
                return startFrequency + frameSize.Height * subcarrierSpacing;
            }
        }

        int[] rMask = { 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1,
                        0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 0, 0, 1,
                        1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 1,
                        1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0,
                        0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1,
                        1, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1,
                        1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0,
                        0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 };

        SettingsProviderXML<SettingsContainer> settings;


        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            #region settings

            settings = new SettingsProviderXML<SettingsContainer>();
            settings.Load(Path.ChangeExtension(Application.ExecutablePath, "settings"));

            #endregion

            #region encoder

            encoder = new Encoder(settings.Data.SampleRateHz, settings.Data.FFTSize, frameSize, StartLine, rMask);

            #endregion

            #region 

            startFrequencyTrk.Minimum = 0;
            startFrequencyTrk.Maximum = settings.Data.FFTSize / 2 - 128;
            StartLine = 60;
            startFrequencyTrk_ValueChanged(startFrequencyTrk, new EventArgs());

            #endregion

            #region framePostFilter

            gammaCorrector = new GammaCorrection(1.5);
            resizer = new ResizeBicubic(outFrameSize.Width, outFrameSize.Height);

            framePostFilter = new FiltersSequence(new IFilter[]
            {
                Grayscale.CommonAlgorithms.RMY,
                gammaCorrector,
                resizer
            });

            gammaFactorTrk.Value = 150;
            gammaFactorTrk_ValueChanged(gammaFactorTrk, new EventArgs());

            #endregion        
    
            #region receiver
            
            for (int n = 0; n < WaveIn.DeviceCount; n++)
            {
                audioInputDeviceCbx.Items.Add(WaveIn.GetCapabilities(n).ProductName);
            }

            if (audioInputDeviceCbx.Items.Count > 0)
            {
                receiverExists = true;
                audioInputDeviceCbx.SelectedIndex = 0;
                audioInputDeviceCbx_SelectedIndexChanged(audioInputDeviceCbx, new EventArgs());

                receiverBtn.Enabled = true;
            }

            #endregion

            #region transmitter

            transmitter = new Transmitter(encoder, 2, 4, 5);
            transmitter.FrameTransmitted += new EventHandler<NextFrameEventArgs>(transmitter_FrameTransmitted);
            transmitter.TransmissionStarted += new EventHandler(transmitter_TransmissionStarted);
            transmitter.TransmissionStopped += new EventHandler(transmitter_TransmissionStopped);

            #endregion

            #region vDevice

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            for (int i = 0; i < videoDevices.Count; i++)
                videoCaptureDeviceCbx.Items.Add(videoDevices[i].Name);

            if (videoCaptureDeviceCbx.Items.Count > 0)
            {
                videoCaptureDeviceCbx.SelectedIndex = 0;
                videoCaptureDeviceCbx_SelectedIndexChanged(new object(), new EventArgs());
                videoCaptureBtn.Enabled = true;
            }

            #endregion

        }

        #endregion

        #region Hadnlers

        #region vDevice

        private void vDevice_NewFrame(object sender, NewFrameEventArgs e)
        {
            Bitmap newBitmap = new Bitmap(e.Frame);
            transmitter.ProcessNextFrame(newBitmap);
            InvokeSetImage(srcPicture, newBitmap);
            e.Frame.Dispose();
        }

        #endregion

        #region transmitter

        private void transmitter_FrameTransmitted(object sender, NextFrameEventArgs e)
        {
            InvokeSetImage(txPicture, encoder.Decode(e.FrameSamples, 5));
        }

        private void transmitter_TransmissionStarted(object sender, EventArgs e)
        {
            InvokeSetChecked(mainToolStrip, transmitterBtn, true);
            InvokeSetEnable(mainToolStrip, settingsBtn, false);
        }

        private void transmitter_TransmissionStopped(object sender, EventArgs e)
        {
            InvokeSetChecked(mainToolStrip, transmitterBtn, false);

            bool settingsEnabled = !vDevice.IsRunning && ((receiver == null) || (!receiver.IsRunning));
            InvokeSetEnable(mainToolStrip, settingsBtn, settingsEnabled);
        }
        
        #endregion

        #region receiver

        private void receiver_FrameReceived(object sender, FrameReceivedEventArgs e)
        {
            InvokeSetImage(rxPicture, framePostFilter.Apply(e.receivedFrame));          
        }

        private void receiver_ReceivingStarted(object sender, EventArgs e)
        {
            InvokeSetChecked(mainToolStrip, receiverBtn, true);
            InvokeSetEnable(mainToolStrip, settingsBtn, false);
        }

        private void receiver_ReceivingStopped(object sender, EventArgs e)
        {
            InvokeSetChecked(mainToolStrip, receiverBtn, false);
            bool settingsEnabled = !vDevice.IsRunning && !transmitter.IsRunning;
            InvokeSetEnable(mainToolStrip, settingsBtn, settingsEnabled);
        }
        
        #endregion

        #region Controls

        private void audioInputDeviceCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (receiver != null)
            {
                receiver.FrameReceived = null;
                receiver.ReceivingStarted = null;
                receiver.ReceivingStopped = null;    
                receiver = null;
            }
             
            receiver = new Receiver(encoder, audioInputDeviceCbx.SelectedIndex, receiverBufferMultiplier);
            receiver.FrameReceived += new EventHandler<FrameReceivedEventArgs>(receiver_FrameReceived);
            receiver.ReceivingStarted += new EventHandler(receiver_ReceivingStarted);
            receiver.ReceivingStopped += new EventHandler(receiver_ReceivingStopped);
        }

        private void videoCaptureDeviceCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool found = false;
            string vDeviceMoniker = "";

            for (int i = 0; (i < videoDevices.Count) && (!found); i++)
            {
                if (string.Compare(videoCaptureDeviceCbx.SelectedItem.ToString(), videoDevices[i].Name) == 0)
                {
                    found = true;
                    vDeviceMoniker = videoDevices[i].MonikerString;
                }
            }

            if (found)
            {
                vDevice = new VideoCaptureDevice(vDeviceMoniker);                
                vDevice.DesiredFrameRate = 15;
                vDevice.DesiredFrameSize = new System.Drawing.Size(160, 120);
                vDevice.NewFrame += new NewFrameEventHandler(vDevice_NewFrame);
            }
        }

        private void videoCaptureBtn_Click(object sender, EventArgs e)
        {            
            if (videoCaptureBtn.Checked)
            {
                if ((!transmitter.IsRunning) && (!receiver.IsRunning))
                {
                    vDevice.SignalToStop();
                    videoCaptureBtn.Checked = false;
                    transmitterBtn.Enabled = false;
                    receiverBtn.Enabled = receiverExists;
                    settingsBtn.Enabled = false;
                    settingsBtn.Enabled = !transmitter.IsRunning && ((receiver == null) || (!receiver.IsRunning));                    
                }
                else
                {
                    MessageBox.Show("Stop transmitter or/and receiver first", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                vDevice.Start();
                videoCaptureBtn.Checked = true;
                transmitterBtn.Enabled = true;
                receiverBtn.Enabled = receiverExists;
                settingsBtn.Enabled = false;
            }
        }

        private void receiverBtn_Click(object sender, EventArgs e)
        {
            if (!receiver.IsRunning)
                receiver.Start();
            else
                receiver.Stop();
        }

        private void transmitterBtn_Click(object sender, EventArgs e)
        {
            if (!transmitter.IsRunning)
                transmitter.Start();
            else
                transmitter.Stop();
        }

        private void AboutBtn_Click(object sender, EventArgs e)
        {
            using (AboutBox aBox = new AboutBox())
            {
                aBox.ApplyAssembly(Assembly.GetExecutingAssembly());
                aBox.Weblink = "www.unavlab.com";
                aBox.ShowDialog();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (!isRestart) && (MessageBox.Show("Close application?", "Question", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ((vDevice != null) && (vDevice.IsRunning)) vDevice.SignalToStop();
        }

        private void startFrequencyTrk_ValueChanged(object sender, EventArgs e)
        {
            bandwidthGroup.Text = string.Format("Bandwidth: {0:F01} .. {1:F01} Hz", startFrequency, endFrequency);
            encoder.StartLine = startFrequencyTrk.Value;
        }

        private void gammaFactorTrk_ValueChanged(object sender, EventArgs e)
        {
            gammaGroup.Text = string.Format(CultureInfo.InvariantCulture, "Gamma factor: {0:F01}", gammaFactorTrk.Value / 100.0);
            gammaCorrector.Gamma = gammaFactorTrk.Value / 100.0;

        }

        private void miscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsEditor sEditor = new SettingsEditor())
            {
                sEditor.Text = "Settings";
                sEditor.Value = settings.Data;

                if (sEditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bool isSaved = false;
                    settings.Data = sEditor.Value;

                    try
                    {
                        settings.Save(Path.ChangeExtension(Application.ExecutablePath, "settings"));
                        isSaved = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if ((isSaved) && (MessageBox.Show("Settings saved. Do you wish to restart application to apply?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        isRestart = true;
                        Application.Restart();
                    }

                }
            }
        }
     
        #endregion                        

        
        
        #endregion                
    }
}
