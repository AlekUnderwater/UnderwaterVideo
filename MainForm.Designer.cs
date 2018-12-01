namespace UnderwaterVideo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.videoCaptureBtn = new System.Windows.Forms.ToolStripButton();
            this.transmitterBtn = new System.Windows.Forms.ToolStripButton();
            this.receiverBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.videoCaptureDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoCaptureDeviceCbx = new System.Windows.Forms.ToolStripComboBox();
            this.audioInputDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioInputDeviceCbx = new System.Windows.Forms.ToolStripComboBox();
            this.miscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutBtn = new System.Windows.Forms.ToolStripButton();
            this.sourceFrameGroup = new System.Windows.Forms.GroupBox();
            this.srcPicture = new System.Windows.Forms.PictureBox();
            this.transmitterGroup = new System.Windows.Forms.GroupBox();
            this.txPicture = new System.Windows.Forms.PictureBox();
            this.receiverGroup = new System.Windows.Forms.GroupBox();
            this.rxPicture = new System.Windows.Forms.PictureBox();
            this.consoleGroup = new System.Windows.Forms.GroupBox();
            this.gammaGroup = new System.Windows.Forms.GroupBox();
            this.gammaFactorTrk = new System.Windows.Forms.TrackBar();
            this.bandwidthGroup = new System.Windows.Forms.GroupBox();
            this.startFrequencyTrk = new System.Windows.Forms.TrackBar();
            this.mainToolStrip.SuspendLayout();
            this.sourceFrameGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.srcPicture)).BeginInit();
            this.transmitterGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txPicture)).BeginInit();
            this.receiverGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rxPicture)).BeginInit();
            this.consoleGroup.SuspendLayout();
            this.gammaGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gammaFactorTrk)).BeginInit();
            this.bandwidthGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startFrequencyTrk)).BeginInit();
            this.SuspendLayout();
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 505);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(951, 22);
            this.mainStatusStrip.TabIndex = 0;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoCaptureBtn,
            this.transmitterBtn,
            this.receiverBtn,
            this.toolStripSeparator1,
            this.settingsBtn,
            this.AboutBtn});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(951, 27);
            this.mainToolStrip.TabIndex = 1;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // videoCaptureBtn
            // 
            this.videoCaptureBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.videoCaptureBtn.Enabled = false;
            this.videoCaptureBtn.Image = ((System.Drawing.Image)(resources.GetObject("videoCaptureBtn.Image")));
            this.videoCaptureBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.videoCaptureBtn.Name = "videoCaptureBtn";
            this.videoCaptureBtn.Size = new System.Drawing.Size(106, 24);
            this.videoCaptureBtn.Text = "Video capture";
            this.videoCaptureBtn.Click += new System.EventHandler(this.videoCaptureBtn_Click);
            // 
            // transmitterBtn
            // 
            this.transmitterBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.transmitterBtn.Enabled = false;
            this.transmitterBtn.Image = ((System.Drawing.Image)(resources.GetObject("transmitterBtn.Image")));
            this.transmitterBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.transmitterBtn.Name = "transmitterBtn";
            this.transmitterBtn.Size = new System.Drawing.Size(87, 24);
            this.transmitterBtn.Text = "Transmitter";
            this.transmitterBtn.Click += new System.EventHandler(this.transmitterBtn_Click);
            // 
            // receiverBtn
            // 
            this.receiverBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.receiverBtn.Enabled = false;
            this.receiverBtn.Image = ((System.Drawing.Image)(resources.GetObject("receiverBtn.Image")));
            this.receiverBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.receiverBtn.Name = "receiverBtn";
            this.receiverBtn.Size = new System.Drawing.Size(69, 24);
            this.receiverBtn.Text = "Receiver";
            this.receiverBtn.Click += new System.EventHandler(this.receiverBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // settingsBtn
            // 
            this.settingsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoCaptureDeviceToolStripMenuItem,
            this.audioInputDeviceToolStripMenuItem,
            this.miscToolStripMenuItem});
            this.settingsBtn.Image = ((System.Drawing.Image)(resources.GetObject("settingsBtn.Image")));
            this.settingsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(75, 24);
            this.settingsBtn.Text = "Settings";
            // 
            // videoCaptureDeviceToolStripMenuItem
            // 
            this.videoCaptureDeviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoCaptureDeviceCbx});
            this.videoCaptureDeviceToolStripMenuItem.Name = "videoCaptureDeviceToolStripMenuItem";
            this.videoCaptureDeviceToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.videoCaptureDeviceToolStripMenuItem.Text = "Video capture device";
            // 
            // videoCaptureDeviceCbx
            // 
            this.videoCaptureDeviceCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.videoCaptureDeviceCbx.DropDownWidth = 200;
            this.videoCaptureDeviceCbx.Name = "videoCaptureDeviceCbx";
            this.videoCaptureDeviceCbx.Size = new System.Drawing.Size(200, 28);
            this.videoCaptureDeviceCbx.SelectedIndexChanged += new System.EventHandler(this.videoCaptureDeviceCbx_SelectedIndexChanged);
            // 
            // audioInputDeviceToolStripMenuItem
            // 
            this.audioInputDeviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.audioInputDeviceCbx});
            this.audioInputDeviceToolStripMenuItem.Name = "audioInputDeviceToolStripMenuItem";
            this.audioInputDeviceToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.audioInputDeviceToolStripMenuItem.Text = "Audio input device";
            // 
            // audioInputDeviceCbx
            // 
            this.audioInputDeviceCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.audioInputDeviceCbx.DropDownWidth = 200;
            this.audioInputDeviceCbx.Name = "audioInputDeviceCbx";
            this.audioInputDeviceCbx.Size = new System.Drawing.Size(121, 28);
            this.audioInputDeviceCbx.SelectedIndexChanged += new System.EventHandler(this.audioInputDeviceCbx_SelectedIndexChanged);
            // 
            // miscToolStripMenuItem
            // 
            this.miscToolStripMenuItem.Name = "miscToolStripMenuItem";
            this.miscToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.miscToolStripMenuItem.Text = "Misc";
            this.miscToolStripMenuItem.Click += new System.EventHandler(this.miscToolStripMenuItem_Click);
            // 
            // AboutBtn
            // 
            this.AboutBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.AboutBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AboutBtn.Image = ((System.Drawing.Image)(resources.GetObject("AboutBtn.Image")));
            this.AboutBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AboutBtn.Name = "AboutBtn";
            this.AboutBtn.Size = new System.Drawing.Size(54, 24);
            this.AboutBtn.Text = "About";
            this.AboutBtn.Click += new System.EventHandler(this.AboutBtn_Click);
            // 
            // sourceFrameGroup
            // 
            this.sourceFrameGroup.Controls.Add(this.srcPicture);
            this.sourceFrameGroup.Location = new System.Drawing.Point(12, 28);
            this.sourceFrameGroup.Name = "sourceFrameGroup";
            this.sourceFrameGroup.Size = new System.Drawing.Size(304, 294);
            this.sourceFrameGroup.TabIndex = 2;
            this.sourceFrameGroup.TabStop = false;
            this.sourceFrameGroup.Text = "Source";
            // 
            // srcPicture
            // 
            this.srcPicture.Location = new System.Drawing.Point(6, 21);
            this.srcPicture.Name = "srcPicture";
            this.srcPicture.Size = new System.Drawing.Size(292, 267);
            this.srcPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.srcPicture.TabIndex = 0;
            this.srcPicture.TabStop = false;
            // 
            // transmitterGroup
            // 
            this.transmitterGroup.Controls.Add(this.txPicture);
            this.transmitterGroup.Location = new System.Drawing.Point(322, 28);
            this.transmitterGroup.Name = "transmitterGroup";
            this.transmitterGroup.Size = new System.Drawing.Size(302, 294);
            this.transmitterGroup.TabIndex = 3;
            this.transmitterGroup.TabStop = false;
            this.transmitterGroup.Text = "Transmitter/Loop";
            // 
            // txPicture
            // 
            this.txPicture.Location = new System.Drawing.Point(6, 21);
            this.txPicture.Name = "txPicture";
            this.txPicture.Size = new System.Drawing.Size(290, 267);
            this.txPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.txPicture.TabIndex = 1;
            this.txPicture.TabStop = false;
            // 
            // receiverGroup
            // 
            this.receiverGroup.Controls.Add(this.rxPicture);
            this.receiverGroup.Location = new System.Drawing.Point(630, 28);
            this.receiverGroup.Name = "receiverGroup";
            this.receiverGroup.Size = new System.Drawing.Size(308, 294);
            this.receiverGroup.TabIndex = 4;
            this.receiverGroup.TabStop = false;
            this.receiverGroup.Text = "Receiver";
            // 
            // rxPicture
            // 
            this.rxPicture.Location = new System.Drawing.Point(6, 21);
            this.rxPicture.Name = "rxPicture";
            this.rxPicture.Size = new System.Drawing.Size(296, 267);
            this.rxPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rxPicture.TabIndex = 1;
            this.rxPicture.TabStop = false;
            // 
            // consoleGroup
            // 
            this.consoleGroup.Controls.Add(this.gammaGroup);
            this.consoleGroup.Controls.Add(this.bandwidthGroup);
            this.consoleGroup.Location = new System.Drawing.Point(12, 328);
            this.consoleGroup.Name = "consoleGroup";
            this.consoleGroup.Size = new System.Drawing.Size(926, 168);
            this.consoleGroup.TabIndex = 5;
            this.consoleGroup.TabStop = false;
            this.consoleGroup.Text = "Console";
            // 
            // gammaGroup
            // 
            this.gammaGroup.Controls.Add(this.gammaFactorTrk);
            this.gammaGroup.Location = new System.Drawing.Point(6, 93);
            this.gammaGroup.Name = "gammaGroup";
            this.gammaGroup.Size = new System.Drawing.Size(606, 66);
            this.gammaGroup.TabIndex = 4;
            this.gammaGroup.TabStop = false;
            this.gammaGroup.Text = "Gamma factor:";
            // 
            // gammaFactorTrk
            // 
            this.gammaFactorTrk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gammaFactorTrk.AutoSize = false;
            this.gammaFactorTrk.Location = new System.Drawing.Point(6, 19);
            this.gammaFactorTrk.Maximum = 500;
            this.gammaFactorTrk.Minimum = 10;
            this.gammaFactorTrk.Name = "gammaFactorTrk";
            this.gammaFactorTrk.Size = new System.Drawing.Size(589, 41);
            this.gammaFactorTrk.TabIndex = 0;
            this.gammaFactorTrk.TickFrequency = 10;
            this.gammaFactorTrk.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.gammaFactorTrk.Value = 10;
            this.gammaFactorTrk.ValueChanged += new System.EventHandler(this.gammaFactorTrk_ValueChanged);
            // 
            // bandwidthGroup
            // 
            this.bandwidthGroup.Controls.Add(this.startFrequencyTrk);
            this.bandwidthGroup.Location = new System.Drawing.Point(6, 21);
            this.bandwidthGroup.Name = "bandwidthGroup";
            this.bandwidthGroup.Size = new System.Drawing.Size(606, 66);
            this.bandwidthGroup.TabIndex = 3;
            this.bandwidthGroup.TabStop = false;
            this.bandwidthGroup.Text = "Bandwidth";
            // 
            // startFrequencyTrk
            // 
            this.startFrequencyTrk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startFrequencyTrk.AutoSize = false;
            this.startFrequencyTrk.Location = new System.Drawing.Point(6, 19);
            this.startFrequencyTrk.Name = "startFrequencyTrk";
            this.startFrequencyTrk.Size = new System.Drawing.Size(589, 41);
            this.startFrequencyTrk.TabIndex = 0;
            this.startFrequencyTrk.TickFrequency = 10;
            this.startFrequencyTrk.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.startFrequencyTrk.ValueChanged += new System.EventHandler(this.startFrequencyTrk_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 527);
            this.Controls.Add(this.consoleGroup);
            this.Controls.Add(this.receiverGroup);
            this.Controls.Add(this.transmitterGroup);
            this.Controls.Add(this.sourceFrameGroup);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Underwater video transmission demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.sourceFrameGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.srcPicture)).EndInit();
            this.transmitterGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txPicture)).EndInit();
            this.receiverGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rxPicture)).EndInit();
            this.consoleGroup.ResumeLayout(false);
            this.gammaGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gammaFactorTrk)).EndInit();
            this.bandwidthGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.startFrequencyTrk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.GroupBox sourceFrameGroup;
        private System.Windows.Forms.GroupBox transmitterGroup;
        private System.Windows.Forms.GroupBox receiverGroup;
        private System.Windows.Forms.ToolStripButton videoCaptureBtn;
        private System.Windows.Forms.ToolStripButton transmitterBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton AboutBtn;
        private System.Windows.Forms.PictureBox srcPicture;
        private System.Windows.Forms.PictureBox txPicture;
        private System.Windows.Forms.PictureBox rxPicture;
        private System.Windows.Forms.ToolStripDropDownButton settingsBtn;
        private System.Windows.Forms.ToolStripMenuItem videoCaptureDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox videoCaptureDeviceCbx;
        private System.Windows.Forms.GroupBox consoleGroup;
        private System.Windows.Forms.TrackBar startFrequencyTrk;
        private System.Windows.Forms.ToolStripButton receiverBtn;
        private System.Windows.Forms.GroupBox bandwidthGroup;
        private System.Windows.Forms.GroupBox gammaGroup;
        private System.Windows.Forms.TrackBar gammaFactorTrk;
        private System.Windows.Forms.ToolStripMenuItem audioInputDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox audioInputDeviceCbx;
        private System.Windows.Forms.ToolStripMenuItem miscToolStripMenuItem;
    }
}

