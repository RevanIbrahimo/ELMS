namespace ELMS.UserControls
{
    partial class OrderUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderUserControl));
            this.BarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.ToolBar = new DevExpress.XtraBars.Bar();
            this.NewBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.EditBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.DeleteBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.RefreshBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.PrintBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.ExportBarButton = new DevExpress.XtraBars.BarSubItem();
            this.ExcelBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.PdfBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.TxtBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.HtmlBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.CsvBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.MhtBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.RtfBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ScheduleBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.HistroryBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.OrderGridControl = new DevExpress.XtraGrid.GridControl();
            this.OrderGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Customer_SS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Customer_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Customer_RegisteredAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Customer_FullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Customer_SexName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Order_Percent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Customer_BirthPlace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Customer_Address = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Customer_Note = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Customer_UsedUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Order_CreditAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Order_TypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OrderPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderPopupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // BarManager
            // 
            this.BarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.ToolBar,
            this.bar3});
            this.BarManager.DockControls.Add(this.barDockControlTop);
            this.BarManager.DockControls.Add(this.barDockControlBottom);
            this.BarManager.DockControls.Add(this.barDockControlLeft);
            this.BarManager.DockControls.Add(this.barDockControlRight);
            this.BarManager.Form = this;
            this.BarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.NewBarButton,
            this.EditBarButton,
            this.DeleteBarButton,
            this.RefreshBarButton,
            this.PrintBarButton,
            this.ExportBarButton,
            this.ExcelBarButton,
            this.PdfBarButton,
            this.TxtBarButton,
            this.HtmlBarButton,
            this.CsvBarButton,
            this.MhtBarButton,
            this.ScheduleBarButton,
            this.HistroryBarButton,
            this.RtfBarButton});
            this.BarManager.MaxItemId = 15;
            this.BarManager.StatusBar = this.bar3;
            // 
            // ToolBar
            // 
            this.ToolBar.BarName = "Tools";
            this.ToolBar.DockCol = 0;
            this.ToolBar.DockRow = 0;
            this.ToolBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.ToolBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.NewBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.EditBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.DeleteBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.RefreshBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.PrintBarButton, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.ExportBarButton)});
            this.ToolBar.OptionsBar.DrawBorder = false;
            this.ToolBar.OptionsBar.DrawDragBorder = false;
            this.ToolBar.OptionsBar.UseWholeRow = true;
            this.ToolBar.Text = "Tools";
            // 
            // NewBarButton
            // 
            this.NewBarButton.Caption = "Yeni";
            this.NewBarButton.Id = 0;
            this.NewBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.plus_32;
            this.NewBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.NewBarButton.Name = "NewBarButton";
            this.NewBarButton.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.NewBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.NewBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.NewBarButton_ItemClick);
            // 
            // EditBarButton
            // 
            this.EditBarButton.Caption = "Dəyiş";
            this.EditBarButton.Id = 1;
            this.EditBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.pencil_32;
            this.EditBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this.EditBarButton.Name = "EditBarButton";
            this.EditBarButton.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.EditBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.EditBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.EditBarButton_ItemClick);
            // 
            // DeleteBarButton
            // 
            this.DeleteBarButton.Caption = "Sil";
            this.DeleteBarButton.Id = 2;
            this.DeleteBarButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("DeleteBarButton.ImageOptions.Image")));
            this.DeleteBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete));
            this.DeleteBarButton.Name = "DeleteBarButton";
            this.DeleteBarButton.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.DeleteBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.DeleteBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DeleteBarButton_ItemClick);
            // 
            // RefreshBarButton
            // 
            this.RefreshBarButton.Caption = "Yenilə";
            this.RefreshBarButton.Id = 3;
            this.RefreshBarButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RefreshBarButton.ImageOptions.Image")));
            this.RefreshBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.RefreshBarButton.Name = "RefreshBarButton";
            this.RefreshBarButton.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.RefreshBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.RefreshBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RefreshBarButton_ItemClick);
            // 
            // PrintBarButton
            // 
            this.PrintBarButton.Caption = "Çap et";
            this.PrintBarButton.Id = 4;
            this.PrintBarButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("PrintBarButton.ImageOptions.Image")));
            this.PrintBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this.PrintBarButton.Name = "PrintBarButton";
            this.PrintBarButton.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.PrintBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.PrintBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PrintBarButton_ItemClick);
            // 
            // ExportBarButton
            // 
            this.ExportBarButton.Caption = "İxrac et";
            this.ExportBarButton.Id = 5;
            this.ExportBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.table_export_32;
            this.ExportBarButton.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ExcelBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.PdfBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.TxtBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.HtmlBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.CsvBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.MhtBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.RtfBarButton)});
            this.ExportBarButton.Name = "ExportBarButton";
            this.ExportBarButton.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // ExcelBarButton
            // 
            this.ExcelBarButton.Caption = "Excel";
            this.ExcelBarButton.Id = 6;
            this.ExcelBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.excel_32;
            this.ExcelBarButton.Name = "ExcelBarButton";
            this.ExcelBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ExcelBarButton_ItemClick);
            // 
            // PdfBarButton
            // 
            this.PdfBarButton.Caption = "Pdf";
            this.PdfBarButton.Id = 7;
            this.PdfBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.pdf_32;
            this.PdfBarButton.Name = "PdfBarButton";
            this.PdfBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PdfBarButton_ItemClick);
            // 
            // TxtBarButton
            // 
            this.TxtBarButton.Caption = "Txt";
            this.TxtBarButton.Id = 8;
            this.TxtBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.txt_32;
            this.TxtBarButton.Name = "TxtBarButton";
            this.TxtBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.TxtBarButton_ItemClick);
            // 
            // HtmlBarButton
            // 
            this.HtmlBarButton.Caption = "Html";
            this.HtmlBarButton.Id = 9;
            this.HtmlBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.html_32;
            this.HtmlBarButton.Name = "HtmlBarButton";
            this.HtmlBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.HtmlBarButton_ItemClick);
            // 
            // CsvBarButton
            // 
            this.CsvBarButton.Caption = "Csv";
            this.CsvBarButton.Id = 10;
            this.CsvBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.csv_32;
            this.CsvBarButton.Name = "CsvBarButton";
            this.CsvBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.CsvBarButton_ItemClick);
            // 
            // MhtBarButton
            // 
            this.MhtBarButton.Caption = "Mht";
            this.MhtBarButton.Id = 11;
            this.MhtBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.explorer_32;
            this.MhtBarButton.Name = "MhtBarButton";
            this.MhtBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MhtBarButton_ItemClick);
            // 
            // RtfBarButton
            // 
            this.RtfBarButton.Caption = "Rtf";
            this.RtfBarButton.Id = 14;
            this.RtfBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.rtf_32;
            this.RtfBarButton.Name = "RtfBarButton";
            this.RtfBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RtfBarButton_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.BarManager;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1430, 47);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 613);
            this.barDockControlBottom.Manager = this.BarManager;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1430, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 47);
            this.barDockControlLeft.Manager = this.BarManager;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 566);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1430, 47);
            this.barDockControlRight.Manager = this.BarManager;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 566);
            // 
            // ScheduleBarButton
            // 
            this.ScheduleBarButton.Caption = "Qəbula yaz";
            this.ScheduleBarButton.Id = 12;
            this.ScheduleBarButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ScheduleBarButton.ImageOptions.Image")));
            this.ScheduleBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q));
            this.ScheduleBarButton.Name = "ScheduleBarButton";
            this.ScheduleBarButton.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.ScheduleBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.ScheduleBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ScheduleBarButton_ItemClick);
            // 
            // HistroryBarButton
            // 
            this.HistroryBarButton.Caption = "Müalicə tarixçəsi";
            this.HistroryBarButton.Id = 13;
            this.HistroryBarButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("HistroryBarButton.ImageOptions.Image")));
            this.HistroryBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H));
            this.HistroryBarButton.Name = "HistroryBarButton";
            this.HistroryBarButton.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.HistroryBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.HistroryBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.HistroryBarButton_ItemClick);
            // 
            // OrderGridControl
            // 
            this.OrderGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrderGridControl.Location = new System.Drawing.Point(0, 47);
            this.OrderGridControl.MainView = this.OrderGridView;
            this.OrderGridControl.Name = "OrderGridControl";
            this.OrderGridControl.Size = new System.Drawing.Size(1430, 566);
            this.OrderGridControl.TabIndex = 56;
            this.OrderGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.OrderGridView});
            // 
            // OrderGridView
            // 
            this.OrderGridView.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.OrderGridView.Appearance.FooterPanel.Options.UseFont = true;
            this.OrderGridView.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.OrderGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.OrderGridView.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OrderGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.OrderGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OrderGridView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OrderGridView.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.OrderGridView.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.OrderGridView.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OrderGridView.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.OrderGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Customer_SS,
            this.Customer_ID,
            this.Customer_RegisteredAddress,
            this.Customer_FullName,
            this.Customer_SexName,
            this.Order_Percent,
            this.Customer_BirthPlace,
            this.Customer_Address,
            this.Customer_Note,
            this.Customer_UsedUserID,
            this.Order_CreditAmount,
            this.Order_TypeID});
            this.OrderGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.OrderGridView.GridControl = this.OrderGridControl;
            this.OrderGridView.Name = "OrderGridView";
            this.OrderGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.OrderGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.OrderGridView.OptionsBehavior.Editable = false;
            this.OrderGridView.OptionsFilter.UseNewCustomFilterDialog = true;
            this.OrderGridView.OptionsFind.FindDelay = 100;
            this.OrderGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.OrderGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.OrderGridView.OptionsView.ShowFooter = true;
            this.OrderGridView.OptionsView.ShowGroupPanel = false;
            this.OrderGridView.OptionsView.ShowIndicator = false;
            this.OrderGridView.PaintStyleName = "Skin";
            this.OrderGridView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            this.OrderGridView.ViewCaption = "Müraciətlərin siyahısı";
            this.OrderGridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.OrderGridView_RowCellStyle);
            this.OrderGridView.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.OrderGridView_FocusedRowObjectChanged);
            this.OrderGridView.ColumnFilterChanged += new System.EventHandler(this.OrderGridView_ColumnFilterChanged);
            this.OrderGridView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.OrderGridView_CustomUnboundColumnData);
            this.OrderGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OrderGridView_MouseUp);
            this.OrderGridView.DoubleClick += new System.EventHandler(this.OrderGridView_DoubleClick);
            // 
            // Customer_SS
            // 
            this.Customer_SS.AppearanceCell.Options.UseTextOptions = true;
            this.Customer_SS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Customer_SS.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Customer_SS.Caption = "S/s";
            this.Customer_SS.FieldName = "Customer_SS";
            this.Customer_SS.Name = "Customer_SS";
            this.Customer_SS.OptionsColumn.FixedWidth = true;
            this.Customer_SS.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Customer_SS", "{0}")});
            this.Customer_SS.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.Customer_SS.Visible = true;
            this.Customer_SS.VisibleIndex = 0;
            this.Customer_SS.Width = 100;
            // 
            // Customer_ID
            // 
            this.Customer_ID.Caption = "ID";
            this.Customer_ID.FieldName = "ID";
            this.Customer_ID.Name = "Customer_ID";
            this.Customer_ID.OptionsColumn.AllowShowHide = false;
            // 
            // Customer_RegisteredAddress
            // 
            this.Customer_RegisteredAddress.Caption = "Qeydiyyat nömrəsi";
            this.Customer_RegisteredAddress.FieldName = "ID";
            this.Customer_RegisteredAddress.Name = "Customer_RegisteredAddress";
            this.Customer_RegisteredAddress.OptionsColumn.FixedWidth = true;
            this.Customer_RegisteredAddress.Visible = true;
            this.Customer_RegisteredAddress.VisibleIndex = 1;
            this.Customer_RegisteredAddress.Width = 250;
            // 
            // Customer_FullName
            // 
            this.Customer_FullName.Caption = "Filial";
            this.Customer_FullName.FieldName = "BRANCH_NAME";
            this.Customer_FullName.Name = "Customer_FullName";
            this.Customer_FullName.OptionsColumn.FixedWidth = true;
            this.Customer_FullName.Visible = true;
            this.Customer_FullName.VisibleIndex = 2;
            this.Customer_FullName.Width = 200;
            // 
            // Customer_SexName
            // 
            this.Customer_SexName.AppearanceCell.Options.UseTextOptions = true;
            this.Customer_SexName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Customer_SexName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Customer_SexName.Caption = "Müddət";
            this.Customer_SexName.FieldName = "TIME";
            this.Customer_SexName.Name = "Customer_SexName";
            this.Customer_SexName.OptionsColumn.FixedWidth = true;
            this.Customer_SexName.Visible = true;
            this.Customer_SexName.VisibleIndex = 3;
            this.Customer_SexName.Width = 130;
            // 
            // Order_Percent
            // 
            this.Order_Percent.Caption = "Faiz";
            this.Order_Percent.FieldName = "PERCENT";
            this.Order_Percent.Name = "Order_Percent";
            this.Order_Percent.OptionsColumn.FixedWidth = true;
            this.Order_Percent.Visible = true;
            this.Order_Percent.VisibleIndex = 4;
            this.Order_Percent.Width = 130;
            // 
            // Customer_BirthPlace
            // 
            this.Customer_BirthPlace.AppearanceCell.Options.UseTextOptions = true;
            this.Customer_BirthPlace.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Customer_BirthPlace.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Customer_BirthPlace.Caption = "Sifarişin mənbəyi";
            this.Customer_BirthPlace.FieldName = "ORDER_SOURCE";
            this.Customer_BirthPlace.Name = "Customer_BirthPlace";
            this.Customer_BirthPlace.OptionsColumn.FixedWidth = true;
            this.Customer_BirthPlace.Visible = true;
            this.Customer_BirthPlace.VisibleIndex = 5;
            this.Customer_BirthPlace.Width = 250;
            // 
            // Customer_Address
            // 
            this.Customer_Address.Caption = "İlkin ödəniş";
            this.Customer_Address.FieldName = "FIRST_PAYMENT";
            this.Customer_Address.Name = "Customer_Address";
            this.Customer_Address.OptionsColumn.FixedWidth = true;
            this.Customer_Address.Visible = true;
            this.Customer_Address.VisibleIndex = 7;
            this.Customer_Address.Width = 200;
            // 
            // Customer_Note
            // 
            this.Customer_Note.Caption = "Məbləğ";
            this.Customer_Note.FieldName = "ORDER_AMOUNT";
            this.Customer_Note.Name = "Customer_Note";
            this.Customer_Note.OptionsColumn.FixedWidth = true;
            this.Customer_Note.Visible = true;
            this.Customer_Note.VisibleIndex = 6;
            this.Customer_Note.Width = 200;
            // 
            // Customer_UsedUserID
            // 
            this.Customer_UsedUserID.Caption = "UsedUserID";
            this.Customer_UsedUserID.FieldName = "USED_USER_ID";
            this.Customer_UsedUserID.Name = "Customer_UsedUserID";
            this.Customer_UsedUserID.OptionsColumn.AllowShowHide = false;
            // 
            // Order_CreditAmount
            // 
            this.Order_CreditAmount.Caption = "Nisyə məbləği";
            this.Order_CreditAmount.FieldName = "CREDIT_AMOUNT";
            this.Order_CreditAmount.Name = "Order_CreditAmount";
            this.Order_CreditAmount.OptionsColumn.FixedWidth = true;
            this.Order_CreditAmount.Visible = true;
            this.Order_CreditAmount.VisibleIndex = 8;
            this.Order_CreditAmount.Width = 200;
            // 
            // Order_TypeID
            // 
            this.Order_TypeID.Caption = "Order_TypeID";
            this.Order_TypeID.FieldName = "TYPE_ID";
            this.Order_TypeID.Name = "Order_TypeID";
            // 
            // OrderPopupMenu
            // 
            this.OrderPopupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.NewBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.EditBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.DeleteBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.RefreshBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.ScheduleBarButton, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.HistroryBarButton, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.PrintBarButton, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.ExportBarButton)});
            this.OrderPopupMenu.Manager = this.BarManager;
            this.OrderPopupMenu.Name = "OrderPopupMenu";
            // 
            // OrderUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OrderGridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "OrderUserControl";
            this.Size = new System.Drawing.Size(1430, 636);
            this.Load += new System.EventHandler(this.OrderUserControl_Load);
            this.Enter += new System.EventHandler(this.OrderUserControl_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.BarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderPopupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager BarManager;
        private DevExpress.XtraBars.Bar ToolBar;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem NewBarButton;
        private DevExpress.XtraBars.BarButtonItem EditBarButton;
        private DevExpress.XtraBars.BarButtonItem DeleteBarButton;
        private DevExpress.XtraBars.BarButtonItem RefreshBarButton;
        private DevExpress.XtraBars.BarButtonItem PrintBarButton;
        private DevExpress.XtraBars.BarSubItem ExportBarButton;
        private DevExpress.XtraBars.BarButtonItem ExcelBarButton;
        private DevExpress.XtraBars.BarButtonItem PdfBarButton;
        private DevExpress.XtraBars.BarButtonItem TxtBarButton;
        private DevExpress.XtraBars.BarButtonItem HtmlBarButton;
        private DevExpress.XtraBars.BarButtonItem CsvBarButton;
        private DevExpress.XtraBars.BarButtonItem MhtBarButton;
        private DevExpress.XtraBars.BarButtonItem ScheduleBarButton;
        private DevExpress.XtraBars.BarButtonItem HistroryBarButton;
        private DevExpress.XtraGrid.GridControl OrderGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView OrderGridView;
        private DevExpress.XtraGrid.Columns.GridColumn Customer_SS;
        private DevExpress.XtraGrid.Columns.GridColumn Customer_FullName;
        private DevExpress.XtraGrid.Columns.GridColumn Customer_BirthPlace;
        private DevExpress.XtraGrid.Columns.GridColumn Customer_Address;
        private DevExpress.XtraGrid.Columns.GridColumn Customer_Note;
        private DevExpress.XtraGrid.Columns.GridColumn Customer_SexName;
        private DevExpress.XtraGrid.Columns.GridColumn Customer_UsedUserID;
        private DevExpress.XtraBars.BarButtonItem RtfBarButton;
        private DevExpress.XtraBars.PopupMenu OrderPopupMenu;
        private DevExpress.XtraGrid.Columns.GridColumn Customer_ID;
        private DevExpress.XtraGrid.Columns.GridColumn Customer_RegisteredAddress;
        private DevExpress.XtraGrid.Columns.GridColumn Order_CreditAmount;
        private DevExpress.XtraGrid.Columns.GridColumn Order_TypeID;
        private DevExpress.XtraGrid.Columns.GridColumn Order_Percent;
    }
}
