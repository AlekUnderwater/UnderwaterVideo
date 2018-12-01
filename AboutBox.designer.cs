namespace UnderwaterVideo
{
    partial class AboutBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.okBtn = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.descriptionTxb = new System.Windows.Forms.TextBox();
            this.titleLbl = new System.Windows.Forms.Label();
            this.versionLbl = new System.Windows.Forms.Label();
            this.copyrightLbl = new System.Windows.Forms.Label();
            this.weblinkLbl = new System.Windows.Forms.LinkLabel();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.mainPanel.SuspendLayout();
            this.tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okBtn.Location = new System.Drawing.Point(647, 411);
            this.okBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(100, 28);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Controls.Add(this.tableLayout);
            this.mainPanel.Controls.Add(this.logoBox);
            this.mainPanel.Location = new System.Drawing.Point(16, 15);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(739, 389);
            this.mainPanel.TabIndex = 1;
            // 
            // tableLayout
            // 
            this.tableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.descriptionTxb, 0, 4);
            this.tableLayout.Controls.Add(this.titleLbl, 0, 0);
            this.tableLayout.Controls.Add(this.versionLbl, 0, 1);
            this.tableLayout.Controls.Add(this.copyrightLbl, 0, 2);
            this.tableLayout.Controls.Add(this.weblinkLbl, 0, 3);
            this.tableLayout.Location = new System.Drawing.Point(296, 4);
            this.tableLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 5;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tableLayout.Size = new System.Drawing.Size(439, 382);
            this.tableLayout.TabIndex = 1;
            // 
            // descriptionTxb
            // 
            this.descriptionTxb.BackColor = System.Drawing.Color.White;
            this.descriptionTxb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionTxb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionTxb.Location = new System.Drawing.Point(4, 120);
            this.descriptionTxb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.descriptionTxb.Multiline = true;
            this.descriptionTxb.Name = "descriptionTxb";
            this.descriptionTxb.ReadOnly = true;
            this.descriptionTxb.Size = new System.Drawing.Size(431, 258);
            this.descriptionTxb.TabIndex = 0;
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLbl.Location = new System.Drawing.Point(4, 6);
            this.titleLbl.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(431, 17);
            this.titleLbl.TabIndex = 1;
            this.titleLbl.Text = "Title";
            this.titleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.titleLbl.UseMnemonic = false;
            // 
            // versionLbl
            // 
            this.versionLbl.AutoSize = true;
            this.versionLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.versionLbl.Location = new System.Drawing.Point(4, 35);
            this.versionLbl.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(431, 17);
            this.versionLbl.TabIndex = 2;
            this.versionLbl.Text = "version";
            this.versionLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.versionLbl.UseMnemonic = false;
            // 
            // copyrightLbl
            // 
            this.copyrightLbl.AutoSize = true;
            this.copyrightLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.copyrightLbl.Location = new System.Drawing.Point(4, 64);
            this.copyrightLbl.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.copyrightLbl.Name = "copyrightLbl";
            this.copyrightLbl.Size = new System.Drawing.Size(431, 17);
            this.copyrightLbl.TabIndex = 3;
            this.copyrightLbl.Text = "Copyrights";
            this.copyrightLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.copyrightLbl.UseMnemonic = false;
            // 
            // weblinkLbl
            // 
            this.weblinkLbl.AutoSize = true;
            this.weblinkLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weblinkLbl.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.weblinkLbl.Location = new System.Drawing.Point(4, 93);
            this.weblinkLbl.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.weblinkLbl.Name = "weblinkLbl";
            this.weblinkLbl.Size = new System.Drawing.Size(431, 17);
            this.weblinkLbl.TabIndex = 4;
            this.weblinkLbl.TabStop = true;
            this.weblinkLbl.Text = "Weblink";
            this.weblinkLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.weblinkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.weblinkLbl_LinkClicked);
            // 
            // logoBox
            // 
            this.logoBox.Image = ((System.Drawing.Image)(resources.GetObject("logoBox.Image")));
            this.logoBox.Location = new System.Drawing.Point(4, 4);
            this.logoBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(284, 382);
            this.logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoBox.TabIndex = 0;
            this.logoBox.TabStop = false;
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(771, 454);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.okBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.mainPanel.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.TextBox descriptionTxb;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Label versionLbl;
        private System.Windows.Forms.Label copyrightLbl;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.LinkLabel weblinkLbl;

    }
}