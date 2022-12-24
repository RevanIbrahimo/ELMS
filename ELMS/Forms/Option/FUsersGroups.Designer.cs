namespace ELMS.Forms
{
    partial class FUsersGroups
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FUsersGroups));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem1 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            ManiXButton.Office2010Red office2010Red1 = new ManiXButton.Office2010Red();
            this.Ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.NewBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.EditBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.DeleteBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.RefreshBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.PrintBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.ExportBarSubItem = new DevExpress.XtraBars.BarSubItem();
            this.ExcelBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.PdfBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.RtfBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.HtmlBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.TxtBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.CsvBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.MhtBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.ShowOrHideUserBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.CopyBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.UsersGroupRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.InfoRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.OutRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ViewRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.UserGridControl = new DevExpress.XtraGrid.GridControl();
            this.UserGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.User_SS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_FullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_UsedID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_SexID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_SessionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserToolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.RefreshGroupBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.GroupGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Group_SS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Group_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Group_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Group_Note = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Group_UserUsedID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GroupGridControl = new DevExpress.XtraGrid.GridControl();
            this.BCancel = new ManiXButton.XButton();
            this.PanelOption = new DevExpress.XtraEditors.PanelControl();
            this.PopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelOption)).BeginInit();
            this.PanelOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PopupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // Ribbon
            // 
            this.Ribbon.ExpandCollapseItem.Id = 0;
            this.Ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.Ribbon.ExpandCollapseItem,
            this.NewBarButton,
            this.EditBarButton,
            this.DeleteBarButton,
            this.RefreshBarButton,
            this.PrintBarButton,
            this.ExportBarSubItem,
            this.ShowOrHideUserBarButton,
            this.ExcelBarButton,
            this.PdfBarButton,
            this.RtfBarButton,
            this.HtmlBarButton,
            this.TxtBarButton,
            this.CsvBarButton,
            this.MhtBarButton,
            this.CopyBarButton});
            this.Ribbon.Location = new System.Drawing.Point(0, 0);
            this.Ribbon.MaxItemId = 1;
            this.Ribbon.Name = "Ribbon";
            this.Ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.UsersGroupRibbonPage});
            this.Ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.Ribbon.ShowQatLocationSelector = false;
            this.Ribbon.ShowToolbarCustomizeItem = false;
            this.Ribbon.Size = new System.Drawing.Size(732, 143);
            this.Ribbon.Toolbar.ShowCustomizeItem = false;
            this.Ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // NewBarButton
            // 
            this.NewBarButton.Caption = "Yeni";
            this.NewBarButton.CloseRadialMenuOnItemClick = true;
            this.NewBarButton.Id = 1;
            this.NewBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.user_group_add_32;
            this.NewBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.NewBarButton.Name = "NewBarButton";
            this.NewBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.NewBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.NewBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.NewBarButton_ItemClick);
            // 
            // EditBarButton
            // 
            this.EditBarButton.Caption = "Dəyiş";
            this.EditBarButton.CloseRadialMenuOnItemClick = true;
            this.EditBarButton.Id = 2;
            this.EditBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.user_group_edit_32;
            this.EditBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this.EditBarButton.Name = "EditBarButton";
            this.EditBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.EditBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.EditBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.EditBarButton_ItemClick);
            // 
            // DeleteBarButton
            // 
            this.DeleteBarButton.Caption = "Sil";
            this.DeleteBarButton.CloseRadialMenuOnItemClick = true;
            this.DeleteBarButton.Id = 3;
            this.DeleteBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.user_group_delete_32;
            this.DeleteBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Delete);
            this.DeleteBarButton.Name = "DeleteBarButton";
            this.DeleteBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.DeleteBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.DeleteBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DeleteBarButton_ItemClick);
            // 
            // RefreshBarButton
            // 
            this.RefreshBarButton.Caption = "Təzələ";
            this.RefreshBarButton.CloseRadialMenuOnItemClick = true;
            this.RefreshBarButton.Id = 4;
            this.RefreshBarButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RefreshBarButton.ImageOptions.Image")));
            this.RefreshBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.RefreshBarButton.Name = "RefreshBarButton";
            this.RefreshBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.RefreshBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.RefreshBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RefreshBarButton_ItemClick);
            // 
            // PrintBarButton
            // 
            this.PrintBarButton.Caption = "Çap";
            this.PrintBarButton.CloseRadialMenuOnItemClick = true;
            this.PrintBarButton.Id = 5;
            this.PrintBarButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("PrintBarButton.ImageOptions.Image")));
            this.PrintBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this.PrintBarButton.Name = "PrintBarButton";
            this.PrintBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.PrintBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.PrintBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PrintBarButton_ItemClick);
            // 
            // ExportBarSubItem
            // 
            this.ExportBarSubItem.Caption = "İxrac";
            this.ExportBarSubItem.Id = 7;
            this.ExportBarSubItem.ImageOptions.Image = global::ELMS.Properties.Resources.table_export_32;
            this.ExportBarSubItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ExcelBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.PdfBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.RtfBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.HtmlBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.TxtBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.CsvBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.MhtBarButton)});
            this.ExportBarSubItem.MultiColumn = DevExpress.Utils.DefaultBoolean.True;
            this.ExportBarSubItem.Name = "ExportBarSubItem";
            this.ExportBarSubItem.OptionsMultiColumn.ImageHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Right;
            this.ExportBarSubItem.OptionsMultiColumn.ShowItemText = DevExpress.Utils.DefaultBoolean.True;
            this.ExportBarSubItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ExcelBarButton
            // 
            this.ExcelBarButton.Caption = "Excel";
            this.ExcelBarButton.CloseRadialMenuOnItemClick = true;
            this.ExcelBarButton.Id = 9;
            this.ExcelBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.excel_32;
            this.ExcelBarButton.Name = "ExcelBarButton";
            this.ExcelBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ExcelBarButton_ItemClick);
            // 
            // PdfBarButton
            // 
            this.PdfBarButton.Caption = "Pdf";
            this.PdfBarButton.CloseRadialMenuOnItemClick = true;
            this.PdfBarButton.Id = 10;
            this.PdfBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.pdf_32;
            this.PdfBarButton.Name = "PdfBarButton";
            this.PdfBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PdfBarButton_ItemClick);
            // 
            // RtfBarButton
            // 
            this.RtfBarButton.Caption = "Rtf";
            this.RtfBarButton.CloseRadialMenuOnItemClick = true;
            this.RtfBarButton.Id = 11;
            this.RtfBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.rtf_32;
            this.RtfBarButton.Name = "RtfBarButton";
            this.RtfBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RtfBarButton_ItemClick);
            // 
            // HtmlBarButton
            // 
            this.HtmlBarButton.Caption = "Html";
            this.HtmlBarButton.CloseRadialMenuOnItemClick = true;
            this.HtmlBarButton.Id = 12;
            this.HtmlBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.html_32;
            this.HtmlBarButton.Name = "HtmlBarButton";
            this.HtmlBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.HtmlBarButton_ItemClick);
            // 
            // TxtBarButton
            // 
            this.TxtBarButton.Caption = "Txt";
            this.TxtBarButton.CloseRadialMenuOnItemClick = true;
            this.TxtBarButton.Id = 13;
            this.TxtBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.txt_32;
            this.TxtBarButton.Name = "TxtBarButton";
            this.TxtBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.TxtBarButton_ItemClick);
            // 
            // CsvBarButton
            // 
            this.CsvBarButton.Caption = "Csv";
            this.CsvBarButton.CloseRadialMenuOnItemClick = true;
            this.CsvBarButton.Id = 14;
            this.CsvBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.csv_32;
            this.CsvBarButton.Name = "CsvBarButton";
            this.CsvBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.CsvBarButton_ItemClick);
            // 
            // MhtBarButton
            // 
            this.MhtBarButton.Caption = "Mht";
            this.MhtBarButton.CloseRadialMenuOnItemClick = true;
            this.MhtBarButton.Id = 15;
            this.MhtBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.explorer_32;
            this.MhtBarButton.Name = "MhtBarButton";
            this.MhtBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MhtBarButton_ItemClick);
            // 
            // ShowOrHideUserBarButton
            // 
            this.ShowOrHideUserBarButton.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.ShowOrHideUserBarButton.Caption = "İstifadəçiləri gizlət";
            this.ShowOrHideUserBarButton.Id = 8;
            this.ShowOrHideUserBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.split_vertical;
            this.ShowOrHideUserBarButton.Name = "ShowOrHideUserBarButton";
            this.ShowOrHideUserBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.ShowOrHideUserBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ShowOrHideUserBarButton_ItemClick);
            // 
            // CopyBarButton
            // 
            this.CopyBarButton.Caption = "Qrupun surəti";
            this.CopyBarButton.Id = 16;
            this.CopyBarButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("CopyBarButton.ImageOptions.Image")));
            this.CopyBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
            this.CopyBarButton.Name = "CopyBarButton";
            this.CopyBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.CopyBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            superToolTip1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem1.Text = "<color=255,0,0>Seçilmiş qrupun surətini çıxart</color>";
            toolTipItem1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipItem1.Appearance.Options.UseImage = true;
            toolTipItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolTipItem1.Image")));
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Seçilmiş qrupun surətini çıxarmaq üçün nəzərdə tutulub.";
            toolTipTitleItem2.LeftIndent = 6;
            toolTipTitleItem2.Text = "Qeyd: <i>Qrupun surəti çıxarılan zaman yalnız əvvəlki qrupa aid olan hüquqlar avt" +
    "omatik olaraq yeni qrupa verilir. İstifadəçilər isə avtomatik olaraq yeni qrupa " +
    "daxil edilmir.</i>";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            superToolTip1.Items.Add(toolTipSeparatorItem1);
            superToolTip1.Items.Add(toolTipTitleItem2);
            this.CopyBarButton.SuperTip = superToolTip1;
            this.CopyBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.CopyBarButton_ItemClick);
            // 
            // UsersGroupRibbonPage
            // 
            this.UsersGroupRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.InfoRibbonPageGroup,
            this.OutRibbonPageGroup,
            this.ViewRibbonPageGroup});
            this.UsersGroupRibbonPage.Name = "UsersGroupRibbonPage";
            this.UsersGroupRibbonPage.Text = "İstifadəçi qrupları";
            // 
            // InfoRibbonPageGroup
            // 
            this.InfoRibbonPageGroup.ItemLinks.Add(this.NewBarButton);
            this.InfoRibbonPageGroup.ItemLinks.Add(this.EditBarButton);
            this.InfoRibbonPageGroup.ItemLinks.Add(this.DeleteBarButton);
            this.InfoRibbonPageGroup.ItemLinks.Add(this.RefreshBarButton);
            this.InfoRibbonPageGroup.ItemLinks.Add(this.CopyBarButton, true);
            this.InfoRibbonPageGroup.Name = "InfoRibbonPageGroup";
            this.InfoRibbonPageGroup.Text = "Məlumat";
            // 
            // OutRibbonPageGroup
            // 
            this.OutRibbonPageGroup.ItemLinks.Add(this.PrintBarButton);
            this.OutRibbonPageGroup.ItemLinks.Add(this.ExportBarSubItem);
            this.OutRibbonPageGroup.Name = "OutRibbonPageGroup";
            this.OutRibbonPageGroup.Text = "Çıxış";
            // 
            // ViewRibbonPageGroup
            // 
            this.ViewRibbonPageGroup.ItemLinks.Add(this.ShowOrHideUserBarButton);
            this.ViewRibbonPageGroup.Name = "ViewRibbonPageGroup";
            this.ViewRibbonPageGroup.Text = "Görünüş";
            // 
            // UserGridControl
            // 
            this.UserGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserGridControl.Location = new System.Drawing.Point(0, 362);
            this.UserGridControl.MainView = this.UserGridView;
            this.UserGridControl.Name = "UserGridControl";
            this.UserGridControl.Size = new System.Drawing.Size(732, 205);
            this.UserGridControl.TabIndex = 62;
            this.UserGridControl.ToolTipController = this.UserToolTipController;
            this.UserGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UserGridView});
            // 
            // UserGridView
            // 
            this.UserGridView.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.UserGridView.Appearance.FooterPanel.Options.UseFont = true;
            this.UserGridView.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.UserGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.UserGridView.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.UserGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.UserGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.UserGridView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.UserGridView.Appearance.ViewCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.UserGridView.Appearance.ViewCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.UserGridView.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Navy;
            this.UserGridView.Appearance.ViewCaption.Options.UseBackColor = true;
            this.UserGridView.Appearance.ViewCaption.Options.UseFont = true;
            this.UserGridView.Appearance.ViewCaption.Options.UseForeColor = true;
            this.UserGridView.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.UserGridView.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.UserGridView.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.UserGridView.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.UserGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.User_SS,
            this.User_ID,
            this.User_FullName,
            this.User_UsedID,
            this.User_SexID,
            this.User_SessionID});
            this.UserGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.UserGridView.GridControl = this.UserGridControl;
            this.UserGridView.Name = "UserGridView";
            this.UserGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.UserGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.UserGridView.OptionsBehavior.Editable = false;
            this.UserGridView.OptionsFilter.UseNewCustomFilterDialog = true;
            this.UserGridView.OptionsFind.FindDelay = 100;
            this.UserGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.UserGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.UserGridView.OptionsView.ShowFooter = true;
            this.UserGridView.OptionsView.ShowGroupPanel = false;
            this.UserGridView.OptionsView.ShowIndicator = false;
            this.UserGridView.OptionsView.ShowViewCaption = true;
            this.UserGridView.PaintStyleName = "Skin";
            this.UserGridView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            this.UserGridView.ViewCaption = "Qrupa daxil olan istifadəçilərin siyahısı";
            this.UserGridView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.UserGridView_CustomUnboundColumnData);
            this.UserGridView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GroupGridView_CustomColumnDisplayText);
            // 
            // User_SS
            // 
            this.User_SS.AppearanceCell.Options.UseTextOptions = true;
            this.User_SS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.User_SS.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.User_SS.Caption = "S/s";
            this.User_SS.FieldName = "User_SS";
            this.User_SS.Name = "User_SS";
            this.User_SS.OptionsColumn.FixedWidth = true;
            this.User_SS.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "User_SS", "{0}")});
            this.User_SS.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.User_SS.Visible = true;
            this.User_SS.VisibleIndex = 0;
            this.User_SS.Width = 45;
            // 
            // User_ID
            // 
            this.User_ID.Caption = "ID";
            this.User_ID.FieldName = "ID";
            this.User_ID.Name = "User_ID";
            // 
            // User_FullName
            // 
            this.User_FullName.Caption = "İstifadəçilərin adı";
            this.User_FullName.FieldName = "FULL_NAME";
            this.User_FullName.Name = "User_FullName";
            this.User_FullName.Visible = true;
            this.User_FullName.VisibleIndex = 1;
            // 
            // User_UsedID
            // 
            this.User_UsedID.Caption = "UsedUserID";
            this.User_UsedID.FieldName = "USER_USED_ID";
            this.User_UsedID.Name = "User_UsedID";
            // 
            // User_SexID
            // 
            this.User_SexID.Caption = "SexID";
            this.User_SexID.FieldName = "SEX_ID";
            this.User_SexID.Name = "User_SexID";
            // 
            // User_SessionID
            // 
            this.User_SessionID.Caption = "SessionID";
            this.User_SessionID.FieldName = "SESSION_ID";
            this.User_SessionID.Name = "User_SessionID";
            // 
            // RefreshGroupBarButton
            // 
            this.RefreshGroupBarButton.Caption = "Təzələ";
            this.RefreshGroupBarButton.CloseRadialMenuOnItemClick = true;
            this.RefreshGroupBarButton.Id = 4;
            this.RefreshGroupBarButton.ItemInMenuAppearance.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.RefreshGroupBarButton.ItemInMenuAppearance.Normal.Options.UseBackColor = true;
            this.RefreshGroupBarButton.Name = "RefreshGroupBarButton";
            this.RefreshGroupBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 357);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(732, 5);
            this.splitterControl1.TabIndex = 61;
            this.splitterControl1.TabStop = false;
            // 
            // GroupGridView
            // 
            this.GroupGridView.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.GroupGridView.Appearance.FooterPanel.Options.UseFont = true;
            this.GroupGridView.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.GroupGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GroupGridView.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GroupGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GroupGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GroupGridView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GroupGridView.Appearance.ViewCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.GroupGridView.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Red;
            this.GroupGridView.Appearance.ViewCaption.Options.UseFont = true;
            this.GroupGridView.Appearance.ViewCaption.Options.UseForeColor = true;
            this.GroupGridView.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.GroupGridView.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.GroupGridView.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GroupGridView.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GroupGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Group_SS,
            this.Group_ID,
            this.Group_Name,
            this.Group_Note,
            this.Group_UserUsedID});
            this.GroupGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.GroupGridView.GridControl = this.GroupGridControl;
            this.GroupGridView.Name = "GroupGridView";
            this.GroupGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.GroupGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.GroupGridView.OptionsBehavior.Editable = false;
            this.GroupGridView.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GroupGridView.OptionsFind.FindDelay = 100;
            this.GroupGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GroupGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.GroupGridView.OptionsView.ShowFooter = true;
            this.GroupGridView.OptionsView.ShowGroupPanel = false;
            this.GroupGridView.OptionsView.ShowIndicator = false;
            this.GroupGridView.OptionsView.ShowViewCaption = true;
            this.GroupGridView.PaintStyleName = "Skin";
            this.GroupGridView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            this.GroupGridView.ViewCaption = "Qrupların siyahısı";
            this.GroupGridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GroupGridView_RowCellStyle);
            this.GroupGridView.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.GroupGridView_FocusedRowObjectChanged);
            this.GroupGridView.ColumnFilterChanged += new System.EventHandler(this.GroupGridView_ColumnFilterChanged);
            this.GroupGridView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GroupGridView_CustomColumnDisplayText);
            this.GroupGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GroupGridView_MouseUp);
            this.GroupGridView.DoubleClick += new System.EventHandler(this.GroupGridView_DoubleClick);
            // 
            // Group_SS
            // 
            this.Group_SS.AppearanceCell.Options.UseTextOptions = true;
            this.Group_SS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Group_SS.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Group_SS.Caption = "S/s";
            this.Group_SS.FieldName = "Group_SS";
            this.Group_SS.Name = "Group_SS";
            this.Group_SS.OptionsColumn.FixedWidth = true;
            this.Group_SS.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "SS", "{0}")});
            this.Group_SS.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.Group_SS.Visible = true;
            this.Group_SS.VisibleIndex = 0;
            this.Group_SS.Width = 45;
            // 
            // Group_ID
            // 
            this.Group_ID.Caption = "ID";
            this.Group_ID.FieldName = "ID";
            this.Group_ID.Name = "Group_ID";
            // 
            // Group_Name
            // 
            this.Group_Name.Caption = "Qrupun adı";
            this.Group_Name.FieldName = "GROUP_NAME";
            this.Group_Name.Name = "Group_Name";
            this.Group_Name.Visible = true;
            this.Group_Name.VisibleIndex = 1;
            // 
            // Group_Note
            // 
            this.Group_Note.Caption = "Qeyd";
            this.Group_Note.FieldName = "NOTE";
            this.Group_Note.Name = "Group_Note";
            this.Group_Note.Visible = true;
            this.Group_Note.VisibleIndex = 2;
            // 
            // Group_UserUsedID
            // 
            this.Group_UserUsedID.Caption = "UsedUserID";
            this.Group_UserUsedID.FieldName = "USED_USER_ID";
            this.Group_UserUsedID.Name = "Group_UserUsedID";
            // 
            // GroupGridControl
            // 
            this.GroupGridControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupGridControl.Location = new System.Drawing.Point(0, 143);
            this.GroupGridControl.MainView = this.GroupGridView;
            this.GroupGridControl.Name = "GroupGridControl";
            this.GroupGridControl.Size = new System.Drawing.Size(732, 214);
            this.GroupGridControl.TabIndex = 60;
            this.GroupGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GroupGridView});
            // 
            // BCancel
            // 
            this.BCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.BCancel.Location = new System.Drawing.Point(645, 13);
            this.BCancel.Name = "BCancel";
            this.BCancel.Size = new System.Drawing.Size(75, 25);
            this.BCancel.TabIndex = 6;
            this.BCancel.Text = "Bağla";
            this.BCancel.Theme = ManiXButton.Theme.MSOffice2010_RED;
            this.BCancel.UseVisualStyleBackColor = true;
            this.BCancel.Click += new System.EventHandler(this.BCancel_Click);
            // 
            // PanelOption
            // 
            this.PanelOption.Controls.Add(this.BCancel);
            this.PanelOption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelOption.Location = new System.Drawing.Point(0, 567);
            this.PanelOption.Name = "PanelOption";
            this.PanelOption.Size = new System.Drawing.Size(732, 50);
            this.PanelOption.TabIndex = 59;
            // 
            // PopupMenu
            // 
            this.PopupMenu.ItemLinks.Add(this.NewBarButton);
            this.PopupMenu.ItemLinks.Add(this.EditBarButton);
            this.PopupMenu.ItemLinks.Add(this.DeleteBarButton);
            this.PopupMenu.ItemLinks.Add(this.RefreshBarButton);
            this.PopupMenu.ItemLinks.Add(this.CopyBarButton, true);
            this.PopupMenu.ItemLinks.Add(this.PrintBarButton, true);
            this.PopupMenu.ItemLinks.Add(this.ExportBarSubItem);
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Ribbon = this.Ribbon;
            // 
            // FUsersGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BCancel;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(732, 617);
            this.Controls.Add(this.UserGridControl);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.GroupGridControl);
            this.Controls.Add(this.PanelOption);
            this.Controls.Add(this.Ribbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FUsersGroups";
            this.Ribbon = this.Ribbon;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İstifadəçi qruplarının siyahısı";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FUsersGroups_FormClosing);
            this.Load += new System.EventHandler(this.FUsersGroups_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelOption)).EndInit();
            this.PanelOption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PopupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl Ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage UsersGroupRibbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup InfoRibbonPageGroup;
        private DevExpress.XtraBars.BarButtonItem NewBarButton;
        private DevExpress.XtraBars.BarButtonItem EditBarButton;
        private DevExpress.XtraBars.BarButtonItem DeleteBarButton;
        private DevExpress.XtraBars.BarButtonItem RefreshBarButton;
        private DevExpress.XtraBars.BarButtonItem PrintBarButton;
        private DevExpress.XtraBars.BarSubItem ExportBarSubItem;
        private DevExpress.XtraBars.BarButtonItem ShowOrHideUserBarButton;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup OutRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ViewRibbonPageGroup;
        private DevExpress.XtraGrid.GridControl UserGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView UserGridView;
        private DevExpress.Utils.ToolTipController UserToolTipController;
        private DevExpress.XtraBars.BarButtonItem RefreshGroupBarButton;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView GroupGridView;
        private DevExpress.XtraGrid.GridControl GroupGridControl;
        private ManiXButton.XButton BCancel;
        private DevExpress.XtraEditors.PanelControl PanelOption;
        private DevExpress.XtraBars.BarButtonItem ExcelBarButton;
        private DevExpress.XtraBars.BarButtonItem PdfBarButton;
        private DevExpress.XtraBars.BarButtonItem RtfBarButton;
        private DevExpress.XtraBars.BarButtonItem HtmlBarButton;
        private DevExpress.XtraBars.BarButtonItem TxtBarButton;
        private DevExpress.XtraBars.BarButtonItem CsvBarButton;
        private DevExpress.XtraBars.BarButtonItem MhtBarButton;
        private DevExpress.XtraBars.PopupMenu PopupMenu;
        private DevExpress.XtraBars.BarButtonItem CopyBarButton;
        private DevExpress.XtraGrid.Columns.GridColumn Group_SS;
        private DevExpress.XtraGrid.Columns.GridColumn Group_ID;
        private DevExpress.XtraGrid.Columns.GridColumn Group_Name;
        private DevExpress.XtraGrid.Columns.GridColumn Group_Note;
        private DevExpress.XtraGrid.Columns.GridColumn Group_UserUsedID;
        private DevExpress.XtraGrid.Columns.GridColumn User_SS;
        private DevExpress.XtraGrid.Columns.GridColumn User_ID;
        private DevExpress.XtraGrid.Columns.GridColumn User_FullName;
        private DevExpress.XtraGrid.Columns.GridColumn User_UsedID;
        private DevExpress.XtraGrid.Columns.GridColumn User_SexID;
        private DevExpress.XtraGrid.Columns.GridColumn User_SessionID;
    }
}