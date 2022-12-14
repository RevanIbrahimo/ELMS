namespace ELMS
{
    partial class DentalSplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DentalSplashScreen));
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.CopyrightLabel = new DevExpress.XtraEditors.LabelControl();
            this.DescriptionLabel = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.VersionLabel = new DevExpress.XtraEditors.LabelControl();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.EditValue = 0;
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(31, 284);
            this.marqueeProgressBarControl1.Margin = new System.Windows.Forms.Padding(4);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(539, 15);
            this.marqueeProgressBarControl1.TabIndex = 5;
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.CopyrightLabel.Location = new System.Drawing.Point(31, 352);
            this.CopyrightLabel.Margin = new System.Windows.Forms.Padding(4);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(135, 16);
            this.CopyrightLabel.TabIndex = 6;
            this.CopyrightLabel.Text = "Copyright © 1998-2013";
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.Location = new System.Drawing.Point(31, 254);
            this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(4);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(57, 16);
            this.DescriptionLabel.TabIndex = 7;
            this.DescriptionLabel.Text = "Yüklənir...";
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit2.EditValue = global::ELMS.Properties.Resources.dental_splash;
            this.pictureEdit2.Location = new System.Drawing.Point(16, 15);
            this.pictureEdit2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.AllowFocused = false;
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ShowMenu = false;
            this.pictureEdit2.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit2.Size = new System.Drawing.Size(568, 222);
            this.pictureEdit2.TabIndex = 9;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(371, 327);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit1.Size = new System.Drawing.Size(213, 59);
            this.pictureEdit1.TabIndex = 8;
            // 
            // VersionLabel
            // 
            this.VersionLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.VersionLabel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.VersionLabel.Appearance.Options.UseFont = true;
            this.VersionLabel.Appearance.Options.UseForeColor = true;
            this.VersionLabel.Location = new System.Drawing.Point(514, 253);
            this.VersionLabel.Margin = new System.Windows.Forms.Padding(4);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(56, 17);
            this.VersionLabel.TabIndex = 13;
            this.VersionLabel.Text = "v1.0.2.0";
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // DentalSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 394);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.pictureEdit2);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.CopyrightLabel);
            this.Controls.Add(this.marqueeProgressBarControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DentalSplashScreen";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DentalSplashScreen_FormClosed);
            this.Load += new System.EventHandler(this.DentalSplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraEditors.LabelControl CopyrightLabel;
        private DevExpress.XtraEditors.LabelControl DescriptionLabel;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private DevExpress.XtraEditors.LabelControl VersionLabel;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
    }
}
