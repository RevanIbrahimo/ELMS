namespace ELMS.Forms
{
    partial class FPermissionAddEdit
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPermissionAddEdit));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            ManiXButton.Office2010Green office2010Green2 = new ManiXButton.Office2010Green();
            ManiXButton.Office2010Blue office2010Blue2 = new ManiXButton.Office2010Blue();
            ManiXButton.Office2010Red office2010Red2 = new ManiXButton.Office2010Red();
            this.BDeletePermission = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.BAddPermission = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.UsedPermissionGridControl = new DevExpress.XtraGrid.GridControl();
            this.UsedPermissionGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.UsedPermissionGroupControl = new DevExpress.XtraEditors.GroupControl();
            this.PanelOption = new DevExpress.XtraEditors.PanelControl();
            this.BRefresh = new ManiXButton.XButton();
            this.BOK = new ManiXButton.XButton();
            this.BCancel = new ManiXButton.XButton();
            this.AllPermissionGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.AllPermissionGridControl = new DevExpress.XtraGrid.GridControl();
            this.AllPermissionGroupControl = new DevExpress.XtraEditors.GroupControl();
            this.All_RoleDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.All_DetailName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.All_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.UsedPermissionGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsedPermissionGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsedPermissionGroupControl)).BeginInit();
            this.UsedPermissionGroupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelOption)).BeginInit();
            this.PanelOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllPermissionGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllPermissionGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllPermissionGroupControl)).BeginInit();
            this.AllPermissionGroupControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // BDeletePermission
            // 
            windowsUIButtonImageOptions3.Image = ((System.Drawing.Image)(resources.GetObject("windowsUIButtonImageOptions3.Image")));
            this.BDeletePermission.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Hüquq al", true, windowsUIButtonImageOptions3),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton()});
            this.BDeletePermission.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BDeletePermission.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BDeletePermission.Location = new System.Drawing.Point(534, 239);
            this.BDeletePermission.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BDeletePermission.Name = "BDeletePermission";
            this.BDeletePermission.Size = new System.Drawing.Size(90, 84);
            this.BDeletePermission.TabIndex = 17;
            this.BDeletePermission.Text = "windowsUIButtonPanel2";
            this.BDeletePermission.Click += new System.EventHandler(this.BDeletePermission_Click);
            // 
            // BAddPermission
            // 
            windowsUIButtonImageOptions4.Image = ((System.Drawing.Image)(resources.GetObject("windowsUIButtonImageOptions4.Image")));
            this.BAddPermission.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Hüquq ver", true, windowsUIButtonImageOptions4),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton()});
            this.BAddPermission.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BAddPermission.ForeColor = System.Drawing.Color.Navy;
            this.BAddPermission.Location = new System.Drawing.Point(534, 148);
            this.BAddPermission.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BAddPermission.Name = "BAddPermission";
            this.BAddPermission.Size = new System.Drawing.Size(90, 84);
            this.BAddPermission.TabIndex = 16;
            this.BAddPermission.Text = "windowsUIButtonPanel1";
            this.BAddPermission.Click += new System.EventHandler(this.BAddPermission_Click);
            // 
            // UsedPermissionGridControl
            // 
            this.UsedPermissionGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsedPermissionGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UsedPermissionGridControl.Location = new System.Drawing.Point(2, 25);
            this.UsedPermissionGridControl.MainView = this.UsedPermissionGridView;
            this.UsedPermissionGridControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UsedPermissionGridControl.Name = "UsedPermissionGridControl";
            this.UsedPermissionGridControl.Size = new System.Drawing.Size(509, 570);
            this.UsedPermissionGridControl.TabIndex = 55;
            this.UsedPermissionGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UsedPermissionGridView});
            // 
            // UsedPermissionGridView
            // 
            this.UsedPermissionGridView.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.UsedPermissionGridView.Appearance.FooterPanel.Options.UseFont = true;
            this.UsedPermissionGridView.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.UsedPermissionGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.UsedPermissionGridView.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.UsedPermissionGridView.Appearance.ViewCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.UsedPermissionGridView.Appearance.ViewCaption.Options.UseBackColor = true;
            this.UsedPermissionGridView.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.UsedPermissionGridView.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.UsedPermissionGridView.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.UsedPermissionGridView.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.UsedPermissionGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.UsedPermissionGridView.GridControl = this.UsedPermissionGridControl;
            this.UsedPermissionGridView.Name = "UsedPermissionGridView";
            this.UsedPermissionGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.UsedPermissionGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.UsedPermissionGridView.OptionsBehavior.Editable = false;
            this.UsedPermissionGridView.OptionsFilter.UseNewCustomFilterDialog = true;
            this.UsedPermissionGridView.OptionsFind.AlwaysVisible = true;
            this.UsedPermissionGridView.OptionsFind.FindDelay = 100;
            this.UsedPermissionGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.UsedPermissionGridView.OptionsSelection.MultiSelect = true;
            this.UsedPermissionGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.UsedPermissionGridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.UsedPermissionGridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.UsedPermissionGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.UsedPermissionGridView.OptionsView.ShowGroupPanel = false;
            this.UsedPermissionGridView.OptionsView.ShowIndicator = false;
            this.UsedPermissionGridView.PaintStyleName = "Skin";
            this.UsedPermissionGridView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            // 
            // UsedPermissionGroupControl
            // 
            this.UsedPermissionGroupControl.AppearanceCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.UsedPermissionGroupControl.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.UsedPermissionGroupControl.AppearanceCaption.Options.UseFont = true;
            this.UsedPermissionGroupControl.AppearanceCaption.Options.UseForeColor = true;
            this.UsedPermissionGroupControl.Controls.Add(this.UsedPermissionGridControl);
            this.UsedPermissionGroupControl.Location = new System.Drawing.Point(631, 15);
            this.UsedPermissionGroupControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UsedPermissionGroupControl.Name = "UsedPermissionGroupControl";
            this.UsedPermissionGroupControl.Size = new System.Drawing.Size(513, 597);
            this.UsedPermissionGroupControl.TabIndex = 15;
            this.UsedPermissionGroupControl.Text = "Verilmiş hüquqlar";
            // 
            // PanelOption
            // 
            this.PanelOption.Controls.Add(this.BRefresh);
            this.PanelOption.Controls.Add(this.BOK);
            this.PanelOption.Controls.Add(this.BCancel);
            this.PanelOption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelOption.Location = new System.Drawing.Point(0, 624);
            this.PanelOption.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PanelOption.Name = "PanelOption";
            this.PanelOption.Size = new System.Drawing.Size(1161, 62);
            this.PanelOption.TabIndex = 13;
            // 
            // BRefresh
            // 
            office2010Green2.BorderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(161)))));
            office2010Green2.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(135)))), ((int)(((byte)(228)))));
            office2010Green2.ButtonMouseOverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Green2.ButtonMouseOverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Green2.ButtonMouseOverColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(225)))), ((int)(((byte)(137)))));
            office2010Green2.ButtonMouseOverColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(224)))));
            office2010Green2.ButtonNormalColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(126)))), ((int)(((byte)(43)))));
            office2010Green2.ButtonNormalColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(184)))), ((int)(((byte)(67)))));
            office2010Green2.ButtonNormalColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(126)))), ((int)(((byte)(43)))));
            office2010Green2.ButtonNormalColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(184)))), ((int)(((byte)(67)))));
            office2010Green2.ButtonSelectedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Green2.ButtonSelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Green2.ButtonSelectedColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(117)))));
            office2010Green2.ButtonSelectedColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(107)))));
            office2010Green2.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Green2.SelectedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Green2.TextColor = System.Drawing.Color.White;
            this.BRefresh.ColorTable = office2010Green2;
            this.BRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BRefresh.Location = new System.Drawing.Point(16, 17);
            this.BRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BRefresh.Name = "BRefresh";
            this.BRefresh.Size = new System.Drawing.Size(87, 28);
            this.BRefresh.TabIndex = 6;
            this.BRefresh.Text = "Təzələ";
            this.BRefresh.Theme = ManiXButton.Theme.MSOffice2010_Green;
            this.BRefresh.UseVisualStyleBackColor = true;
            this.BRefresh.Click += new System.EventHandler(this.BRefresh_Click);
            // 
            // BOK
            // 
            this.BOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            office2010Blue2.BorderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(161)))));
            office2010Blue2.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(135)))), ((int)(((byte)(228)))));
            office2010Blue2.ButtonMouseOverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Blue2.ButtonMouseOverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Blue2.ButtonMouseOverColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(225)))), ((int)(((byte)(137)))));
            office2010Blue2.ButtonMouseOverColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(224)))));
            office2010Blue2.ButtonNormalColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(161)))));
            office2010Blue2.ButtonNormalColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(135)))), ((int)(((byte)(228)))));
            office2010Blue2.ButtonNormalColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(97)))), ((int)(((byte)(181)))));
            office2010Blue2.ButtonNormalColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(125)))), ((int)(((byte)(219)))));
            office2010Blue2.ButtonSelectedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Blue2.ButtonSelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Blue2.ButtonSelectedColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(117)))));
            office2010Blue2.ButtonSelectedColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(107)))));
            office2010Blue2.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Blue2.SelectedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Blue2.TextColor = System.Drawing.Color.White;
            this.BOK.ColorTable = office2010Blue2;
            this.BOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BOK.Location = new System.Drawing.Point(962, 16);
            this.BOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BOK.Name = "BOK";
            this.BOK.Size = new System.Drawing.Size(87, 31);
            this.BOK.TabIndex = 4;
            this.BOK.Text = "Yadda saxla";
            this.BOK.Theme = ManiXButton.Theme.MSOffice2010_BLUE;
            this.BOK.UseVisualStyleBackColor = true;
            this.BOK.Click += new System.EventHandler(this.BOK_Click);
            // 
            // BCancel
            // 
            this.BCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            office2010Red2.BorderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(161)))));
            office2010Red2.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(135)))), ((int)(((byte)(228)))));
            office2010Red2.ButtonMouseOverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Red2.ButtonMouseOverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Red2.ButtonMouseOverColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(225)))), ((int)(((byte)(137)))));
            office2010Red2.ButtonMouseOverColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(224)))));
            office2010Red2.ButtonNormalColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(77)))), ((int)(((byte)(45)))));
            office2010Red2.ButtonNormalColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(148)))), ((int)(((byte)(64)))));
            office2010Red2.ButtonNormalColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(77)))), ((int)(((byte)(45)))));
            office2010Red2.ButtonNormalColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(148)))), ((int)(((byte)(64)))));
            office2010Red2.ButtonSelectedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Red2.ButtonSelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Red2.ButtonSelectedColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(117)))));
            office2010Red2.ButtonSelectedColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(107)))));
            office2010Red2.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Red2.SelectedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Red2.TextColor = System.Drawing.Color.White;
            this.BCancel.ColorTable = office2010Red2;
            this.BCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BCancel.Location = new System.Drawing.Point(1057, 16);
            this.BCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BCancel.Name = "BCancel";
            this.BCancel.Size = new System.Drawing.Size(87, 31);
            this.BCancel.TabIndex = 5;
            this.BCancel.Text = "İmtina et";
            this.BCancel.Theme = ManiXButton.Theme.MSOffice2010_RED;
            this.BCancel.UseVisualStyleBackColor = true;
            this.BCancel.Click += new System.EventHandler(this.BCancel_Click);
            // 
            // AllPermissionGridView
            // 
            this.AllPermissionGridView.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.AllPermissionGridView.Appearance.FooterPanel.Options.UseFont = true;
            this.AllPermissionGridView.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.AllPermissionGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.AllPermissionGridView.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AllPermissionGridView.Appearance.ViewCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AllPermissionGridView.Appearance.ViewCaption.Options.UseBackColor = true;
            this.AllPermissionGridView.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.AllPermissionGridView.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AllPermissionGridView.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AllPermissionGridView.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.AllPermissionGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.All_RoleDescription,
            this.All_DetailName,
            this.All_ID});
            this.AllPermissionGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.AllPermissionGridView.GridControl = this.AllPermissionGridControl;
            this.AllPermissionGridView.GroupCount = 1;
            this.AllPermissionGridView.Name = "AllPermissionGridView";
            this.AllPermissionGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.AllPermissionGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.AllPermissionGridView.OptionsBehavior.Editable = false;
            this.AllPermissionGridView.OptionsFilter.UseNewCustomFilterDialog = true;
            this.AllPermissionGridView.OptionsFind.AlwaysVisible = true;
            this.AllPermissionGridView.OptionsFind.FindDelay = 100;
            this.AllPermissionGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.AllPermissionGridView.OptionsSelection.MultiSelect = true;
            this.AllPermissionGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.AllPermissionGridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.AllPermissionGridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.AllPermissionGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.AllPermissionGridView.OptionsView.ShowGroupPanel = false;
            this.AllPermissionGridView.OptionsView.ShowIndicator = false;
            this.AllPermissionGridView.PaintStyleName = "Skin";
            this.AllPermissionGridView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            this.AllPermissionGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.All_RoleDescription, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // AllPermissionGridControl
            // 
            this.AllPermissionGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllPermissionGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AllPermissionGridControl.Location = new System.Drawing.Point(2, 25);
            this.AllPermissionGridControl.MainView = this.AllPermissionGridView;
            this.AllPermissionGridControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AllPermissionGridControl.Name = "AllPermissionGridControl";
            this.AllPermissionGridControl.Size = new System.Drawing.Size(509, 570);
            this.AllPermissionGridControl.TabIndex = 55;
            this.AllPermissionGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.AllPermissionGridView});
            // 
            // AllPermissionGroupControl
            // 
            this.AllPermissionGroupControl.AppearanceCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.AllPermissionGroupControl.AppearanceCaption.ForeColor = System.Drawing.Color.Navy;
            this.AllPermissionGroupControl.AppearanceCaption.Options.UseFont = true;
            this.AllPermissionGroupControl.AppearanceCaption.Options.UseForeColor = true;
            this.AllPermissionGroupControl.Controls.Add(this.AllPermissionGridControl);
            this.AllPermissionGroupControl.Location = new System.Drawing.Point(14, 15);
            this.AllPermissionGroupControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AllPermissionGroupControl.Name = "AllPermissionGroupControl";
            this.AllPermissionGroupControl.Size = new System.Drawing.Size(513, 597);
            this.AllPermissionGroupControl.TabIndex = 14;
            this.AllPermissionGroupControl.Text = "Verilməmiş hüquqlar";
            // 
            // All_RoleDescription
            // 
            this.All_RoleDescription.Caption = "Modulun adı";
            this.All_RoleDescription.FieldName = "ROLE_DESCRIPTION";
            this.All_RoleDescription.Name = "All_RoleDescription";
            this.All_RoleDescription.Visible = true;
            this.All_RoleDescription.VisibleIndex = 1;
            // 
            // All_DetailName
            // 
            this.All_DetailName.AppearanceHeader.Options.UseTextOptions = true;
            this.All_DetailName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.All_DetailName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.All_DetailName.Caption = "Hüquqların adı";
            this.All_DetailName.FieldName = "DETAIL_NAME_AZ";
            this.All_DetailName.Name = "All_DetailName";
            this.All_DetailName.Visible = true;
            this.All_DetailName.VisibleIndex = 1;
            // 
            // All_ID
            // 
            this.All_ID.Caption = "ID";
            this.All_ID.FieldName = "ID";
            this.All_ID.Name = "All_ID";
            // 
            // FPermissionAddEdit
            // 
            this.AcceptButton = this.BOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BCancel;
            this.ClientSize = new System.Drawing.Size(1161, 686);
            this.Controls.Add(this.BDeletePermission);
            this.Controls.Add(this.BAddPermission);
            this.Controls.Add(this.UsedPermissionGroupControl);
            this.Controls.Add(this.PanelOption);
            this.Controls.Add(this.AllPermissionGroupControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FPermissionAddEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hüquqlar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FPermissionAddEdit_FormClosing);
            this.Load += new System.EventHandler(this.FPermissionAddEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UsedPermissionGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsedPermissionGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsedPermissionGroupControl)).EndInit();
            this.UsedPermissionGroupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelOption)).EndInit();
            this.PanelOption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AllPermissionGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllPermissionGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllPermissionGroupControl)).EndInit();
            this.AllPermissionGroupControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel BDeletePermission;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel BAddPermission;
        private DevExpress.XtraGrid.GridControl UsedPermissionGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView UsedPermissionGridView;
        private DevExpress.XtraEditors.GroupControl UsedPermissionGroupControl;
        private DevExpress.XtraEditors.PanelControl PanelOption;
        private ManiXButton.XButton BRefresh;
        private ManiXButton.XButton BOK;
        private ManiXButton.XButton BCancel;
        private DevExpress.XtraGrid.Views.Grid.GridView AllPermissionGridView;
        private DevExpress.XtraGrid.GridControl AllPermissionGridControl;
        private DevExpress.XtraEditors.GroupControl AllPermissionGroupControl;
        private DevExpress.XtraGrid.Columns.GridColumn All_RoleDescription;
        private DevExpress.XtraGrid.Columns.GridColumn All_DetailName;
        private DevExpress.XtraGrid.Columns.GridColumn All_ID;
    }
}