namespace ELMS
{
    partial class FLoginSystem
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            ManiXButton.Office2010Red office2010Red1 = new ManiXButton.Office2010Red();
            ManiXButton.Office2010Blue office2010Blue1 = new ManiXButton.Office2010Blue();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLoginSystem));
            this.panel1 = new System.Windows.Forms.Panel();
            this.VersionLabel = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.SaveCheck = new DevExpress.XtraEditors.CheckEdit();
            this.PasswordLabel = new DevExpress.XtraEditors.LabelControl();
            this.UserNameLabel = new DevExpress.XtraEditors.LabelControl();
            this.PasswordText = new DevExpress.XtraEditors.ButtonEdit();
            this.UserNameText = new DevExpress.XtraEditors.ButtonEdit();
            this.CopyrightLabel = new DevExpress.XtraEditors.LabelControl();
            this.BCancel = new ManiXButton.XButton();
            this.BOK = new ManiXButton.XButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveCheck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserNameText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel1.Controls.Add(this.VersionLabel);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(375, 72);
            this.panel1.TabIndex = 0;
            // 
            // VersionLabel
            // 
            this.VersionLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.VersionLabel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.VersionLabel.Appearance.Options.UseFont = true;
            this.VersionLabel.Appearance.Options.UseForeColor = true;
            this.VersionLabel.Location = new System.Drawing.Point(290, 54);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(44, 13);
            this.VersionLabel.TabIndex = 21;
            this.VersionLabel.Text = "v1.0.2.0";
            // 
            // labelControl1
            // 
            this.labelControl1.AllowHtmlString = true;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(14, 25);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(292, 24);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "<color=104,20,150>ELLONI</color><color=255,255,255> Management System</color>";
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(23, 406);
            this.separatorControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.separatorControl1.Size = new System.Drawing.Size(331, 19);
            this.separatorControl1.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelControl2.Location = new System.Drawing.Point(265, 311);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(92, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Şifrəni unutmusuz?";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // SaveCheck
            // 
            this.SaveCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveCheck.Location = new System.Drawing.Point(23, 310);
            this.SaveCheck.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SaveCheck.Name = "SaveCheck";
            this.SaveCheck.Properties.Appearance.Options.UseFont = true;
            this.SaveCheck.Properties.Caption = "Yadda saxla";
            this.SaveCheck.Size = new System.Drawing.Size(90, 19);
            this.SaveCheck.TabIndex = 18;
            this.SaveCheck.TabStop = false;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.PasswordLabel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PasswordLabel.Appearance.Options.UseFont = true;
            this.PasswordLabel.Appearance.Options.UseForeColor = true;
            this.PasswordLabel.Location = new System.Drawing.Point(24, 250);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(26, 13);
            this.PasswordLabel.TabIndex = 17;
            this.PasswordLabel.Text = "Şifrə";
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.UserNameLabel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.UserNameLabel.Appearance.Options.UseFont = true;
            this.UserNameLabel.Appearance.Options.UseForeColor = true;
            this.UserNameLabel.Location = new System.Drawing.Point(23, 193);
            this.UserNameLabel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(73, 13);
            this.UserNameLabel.TabIndex = 16;
            this.UserNameLabel.Text = "İstifadəçi adı";
            // 
            // PasswordText
            // 
            this.PasswordText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PasswordText.EditValue = "";
            this.PasswordText.Location = new System.Drawing.Point(24, 270);
            this.PasswordText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.PasswordText.Properties.Appearance.Options.UseFont = true;
            this.PasswordText.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            editorButtonImageOptions1.Image = global::ELMS.Properties.Resources.password_icon;
            this.PasswordText.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, false, true, true, editorButtonImageOptions1)});
            this.PasswordText.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.PasswordText.Properties.NullValuePrompt = "Şifrəni daxil edin";
            this.PasswordText.Properties.NullValuePromptShowForEmptyValue = true;
            this.PasswordText.Properties.PasswordChar = '*';
            this.PasswordText.Size = new System.Drawing.Size(330, 22);
            this.PasswordText.TabIndex = 1;
            this.PasswordText.TextChanged += new System.EventHandler(this.UserNameText_TextChanged);
            // 
            // UserNameText
            // 
            this.UserNameText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserNameText.EditValue = "";
            this.UserNameText.Location = new System.Drawing.Point(24, 211);
            this.UserNameText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UserNameText.Name = "UserNameText";
            this.UserNameText.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.UserNameText.Properties.Appearance.Options.UseFont = true;
            this.UserNameText.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            editorButtonImageOptions2.Image = global::ELMS.Properties.Resources.User_icon;
            this.UserNameText.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, false, true, true, editorButtonImageOptions2)});
            this.UserNameText.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.UserNameText.Properties.NullText = "df";
            this.UserNameText.Properties.NullValuePrompt = "İstifadəçi adını daxil edin";
            this.UserNameText.Properties.NullValuePromptShowForEmptyValue = true;
            this.UserNameText.Size = new System.Drawing.Size(330, 22);
            this.UserNameText.TabIndex = 0;
            this.UserNameText.TextChanged += new System.EventHandler(this.UserNameText_TextChanged);
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.CopyrightLabel.Location = new System.Drawing.Point(150, 448);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(87, 13);
            this.CopyrightLabel.TabIndex = 20;
            this.CopyrightLabel.Text = "Copyright © 2019";
            // 
            // BCancel
            // 
            office2010Red1.BorderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(161)))));
            office2010Red1.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(135)))), ((int)(((byte)(228)))));
            office2010Red1.ButtonMouseOverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Red1.ButtonMouseOverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Red1.ButtonMouseOverColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(225)))), ((int)(((byte)(137)))));
            office2010Red1.ButtonMouseOverColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(224)))));
            office2010Red1.ButtonNormalColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(77)))), ((int)(((byte)(45)))));
            office2010Red1.ButtonNormalColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(148)))), ((int)(((byte)(64)))));
            office2010Red1.ButtonNormalColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(77)))), ((int)(((byte)(45)))));
            office2010Red1.ButtonNormalColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(148)))), ((int)(((byte)(64)))));
            office2010Red1.ButtonSelectedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Red1.ButtonSelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Red1.ButtonSelectedColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(117)))));
            office2010Red1.ButtonSelectedColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(107)))));
            office2010Red1.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Red1.SelectedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Red1.TextColor = System.Drawing.Color.White;
            this.BCancel.ColorTable = office2010Red1;
            this.BCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BCancel.Location = new System.Drawing.Point(195, 362);
            this.BCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BCancel.Name = "BCancel";
            this.BCancel.Size = new System.Drawing.Size(159, 38);
            this.BCancel.TabIndex = 21;
            this.BCancel.Text = "İmtina et";
            this.BCancel.Theme = ManiXButton.Theme.MSOffice2010_RED;
            this.BCancel.UseVisualStyleBackColor = true;
            this.BCancel.Click += new System.EventHandler(this.BCancel_Click);
            // 
            // BOK
            // 
            office2010Blue1.BorderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(161)))));
            office2010Blue1.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(135)))), ((int)(((byte)(228)))));
            office2010Blue1.ButtonMouseOverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Blue1.ButtonMouseOverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Blue1.ButtonMouseOverColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(225)))), ((int)(((byte)(137)))));
            office2010Blue1.ButtonMouseOverColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(224)))));
            office2010Blue1.ButtonNormalColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(161)))));
            office2010Blue1.ButtonNormalColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(135)))), ((int)(((byte)(228)))));
            office2010Blue1.ButtonNormalColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(97)))), ((int)(((byte)(181)))));
            office2010Blue1.ButtonNormalColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(125)))), ((int)(((byte)(219)))));
            office2010Blue1.ButtonSelectedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Blue1.ButtonSelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Blue1.ButtonSelectedColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(117)))));
            office2010Blue1.ButtonSelectedColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(107)))));
            office2010Blue1.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Blue1.SelectedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Blue1.TextColor = System.Drawing.Color.White;
            this.BOK.ColorTable = office2010Blue1;
            this.BOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BOK.Location = new System.Drawing.Point(24, 362);
            this.BOK.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BOK.Name = "BOK";
            this.BOK.Size = new System.Drawing.Size(159, 38);
            this.BOK.TabIndex = 19;
            this.BOK.Text = "Daxil ol";
            this.BOK.Theme = ManiXButton.Theme.MSOffice2010_BLUE;
            this.BOK.UseVisualStyleBackColor = true;
            this.BOK.Click += new System.EventHandler(this.BOK_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.EditValue = global::ELMS.Properties.Resources.login;
            this.pictureEdit1.Location = new System.Drawing.Point(150, 90);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit1.Size = new System.Drawing.Size(75, 78);
            this.pictureEdit1.TabIndex = 4;
            // 
            // FLoginSystem
            // 
            this.AcceptButton = this.BOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.BCancel;
            this.ClientSize = new System.Drawing.Size(375, 471);
            this.Controls.Add(this.BCancel);
            this.Controls.Add(this.CopyrightLabel);
            this.Controls.Add(this.BOK);
            this.Controls.Add(this.SaveCheck);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.UserNameText);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FLoginSystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FLoginSystem";
            this.Load += new System.EventHandler(this.FLoginSystem_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveCheck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserNameText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.CheckEdit SaveCheck;
        private DevExpress.XtraEditors.LabelControl PasswordLabel;
        private DevExpress.XtraEditors.LabelControl UserNameLabel;
        private DevExpress.XtraEditors.ButtonEdit PasswordText;
        private DevExpress.XtraEditors.ButtonEdit UserNameText;
        private ManiXButton.XButton BOK;
        private DevExpress.XtraEditors.LabelControl CopyrightLabel;
        private DevExpress.XtraEditors.LabelControl VersionLabel;
        private ManiXButton.XButton BCancel;
    }
}