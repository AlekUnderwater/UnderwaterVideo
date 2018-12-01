namespace UnderwaterVideo
{
    partial class SettingsEditor
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
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sampleRateCbx = new System.Windows.Forms.ComboBox();
            this.FFTSizeCbx = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(164, 186);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(292, 186);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sample rate, Hz";
            // 
            // sampleRateCbx
            // 
            this.sampleRateCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sampleRateCbx.FormattingEnabled = true;
            this.sampleRateCbx.Items.AddRange(new object[] {
            "44100",
            "48000",
            "96000"});
            this.sampleRateCbx.Location = new System.Drawing.Point(140, 42);
            this.sampleRateCbx.Name = "sampleRateCbx";
            this.sampleRateCbx.Size = new System.Drawing.Size(121, 24);
            this.sampleRateCbx.TabIndex = 3;
            // 
            // FFTSizeCbx
            // 
            this.FFTSizeCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FFTSizeCbx.FormattingEnabled = true;
            this.FFTSizeCbx.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048"});
            this.FFTSizeCbx.Location = new System.Drawing.Point(140, 72);
            this.FFTSizeCbx.Name = "FFTSizeCbx";
            this.FFTSizeCbx.Size = new System.Drawing.Size(121, 24);
            this.FFTSizeCbx.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "FFT size";
            // 
            // SettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 221);
            this.Controls.Add(this.FFTSizeCbx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sampleRateCbx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsEditor";
            this.Text = "SettingsEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox sampleRateCbx;
        private System.Windows.Forms.ComboBox FFTSizeCbx;
        private System.Windows.Forms.Label label2;
    }
}