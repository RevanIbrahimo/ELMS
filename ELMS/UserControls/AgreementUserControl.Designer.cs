namespace ELMS.UserControls
{
    partial class AgreementUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgreementUserControl));
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
            this.CustomerGridControl = new DevExpress.XtraGrid.GridControl();
            this.CustomerGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Agreement_SS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Agreement_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Agreement_Number = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Agreement_Branch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Agreement_Amount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Agreement_Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Agreement_Note = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Agreement_UsedUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.AgreementPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepositoryItemPictureEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgreementPopupMenu)).BeginInit();
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
            this.BarManager.MaxItemId = 16;
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
            // CustomerGridControl
            // 
            this.CustomerGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomerGridControl.Location = new System.Drawing.Point(0, 47);
            this.CustomerGridControl.MainView = this.CustomerGridView;
            this.CustomerGridControl.Name = "CustomerGridControl";
            this.CustomerGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepositoryItemPictureEdit});
            this.CustomerGridControl.Size = new System.Drawing.Size(1430, 566);
            this.CustomerGridControl.TabIndex = 56;
            this.CustomerGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.CustomerGridView});
            // 
            // CustomerGridView
            // 
            this.CustomerGridView.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.CustomerGridView.Appearance.FooterPanel.Options.UseFont = true;
            this.CustomerGridView.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.CustomerGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.CustomerGridView.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CustomerGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.CustomerGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CustomerGridView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CustomerGridView.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.CustomerGridView.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.CustomerGridView.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CustomerGridView.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.CustomerGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Agreement_SS,
            this.Agreement_ID,
            this.Agreement_Number,
            this.Agreement_Branch,
            this.Agreement_Amount,
            this.Agreement_Date,
            this.Agreement_Note,
            this.Agreement_UsedUserID});
            this.CustomerGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.CustomerGridView.GridControl = this.CustomerGridControl;
            this.CustomerGridView.Name = "CustomerGridView";
            this.CustomerGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.CustomerGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.CustomerGridView.OptionsBehavior.Editable = false;
            this.CustomerGridView.OptionsFilter.UseNewCustomFilterDialog = true;
            this.CustomerGridView.OptionsFind.FindDelay = 100;
            this.CustomerGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.CustomerGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.CustomerGridView.OptionsView.ShowFooter = true;
            this.CustomerGridView.OptionsView.ShowGroupPanel = false;
            this.CustomerGridView.OptionsView.ShowIndicator = false;
            this.CustomerGridView.PaintStyleName = "Skin";
            this.CustomerGridView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            this.CustomerGridView.ViewCaption = "Müştərilərin siyahısı";
            this.CustomerGridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.CustomerGridView_RowCellStyle);
            this.CustomerGridView.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.CustomerGridView_FocusedRowObjectChanged);
            this.CustomerGridView.ColumnFilterChanged += new System.EventHandler(this.CustomerGridView_ColumnFilterChanged);
            this.CustomerGridView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.CustomerGridView_CustomUnboundColumnData);
            this.CustomerGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CustomerGridView_MouseUp);
            this.CustomerGridView.DoubleClick += new System.EventHandler(this.CustomerGridView_DoubleClick);
            // 
            // Agreement_SS
            // 
            this.Agreement_SS.AppearanceCell.Options.UseTextOptions = true;
            this.Agreement_SS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Agreement_SS.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Agreement_SS.Caption = "S/s";
            this.Agreement_SS.FieldName = "Customer_SS";
            this.Agreement_SS.Name = "Agreement_SS";
            this.Agreement_SS.OptionsColumn.FixedWidth = true;
            this.Agreement_SS.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Customer_SS", "{0}")});
            this.Agreement_SS.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.Agreement_SS.Visible = true;
            this.Agreement_SS.VisibleIndex = 0;
            this.Agreement_SS.Width = 45;
            // 
            // Agreement_ID
            // 
            this.Agreement_ID.Caption = "ID";
            this.Agreement_ID.FieldName = "ID";
            this.Agreement_ID.Name = "Agreement_ID";
            this.Agreement_ID.OptionsColumn.AllowShowHide = false;
            // 
            // Agreement_Number
            // 
            this.Agreement_Number.Caption = "Sazişin nömrəsi";
            this.Agreement_Number.FieldName = "AGREEMENT_NUMBER";
            this.Agreement_Number.Name = "Agreement_Number";
            this.Agreement_Number.OptionsColumn.FixedWidth = true;
            this.Agreement_Number.Visible = true;
            this.Agreement_Number.VisibleIndex = 1;
            this.Agreement_Number.Width = 100;
            // 
            // Agreement_Branch
            // 
            this.Agreement_Branch.Caption = "Filial";
            this.Agreement_Branch.FieldName = "BRANCH_NAME";
            this.Agreement_Branch.Name = "Agreement_Branch";
            this.Agreement_Branch.OptionsColumn.FixedWidth = true;
            this.Agreement_Branch.Visible = true;
            this.Agreement_Branch.VisibleIndex = 2;
            this.Agreement_Branch.Width = 150;
            // 
            // Agreement_Amount
            // 
            this.Agreement_Amount.AppearanceCell.Options.UseTextOptions = true;
            this.Agreement_Amount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Agreement_Amount.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Agreement_Amount.Caption = "Sazişin məbləği";
            this.Agreement_Amount.DisplayFormat.FormatString = "n2";
            this.Agreement_Amount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Agreement_Amount.FieldName = "AGREEMENT_AMOUNT";
            this.Agreement_Amount.Name = "Agreement_Amount";
            this.Agreement_Amount.OptionsColumn.FixedWidth = true;
            this.Agreement_Amount.Visible = true;
            this.Agreement_Amount.VisibleIndex = 3;
            this.Agreement_Amount.Width = 120;
            // 
            // Agreement_Date
            // 
            this.Agreement_Date.AppearanceCell.Options.UseTextOptions = true;
            this.Agreement_Date.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Agreement_Date.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Agreement_Date.Caption = "Tarix";
            this.Agreement_Date.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.Agreement_Date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Agreement_Date.FieldName = "AGREEMENT_DATE";
            this.Agreement_Date.Name = "Agreement_Date";
            this.Agreement_Date.OptionsColumn.FixedWidth = true;
            this.Agreement_Date.Visible = true;
            this.Agreement_Date.VisibleIndex = 4;
            this.Agreement_Date.Width = 120;
            // 
            // Agreement_Note
            // 
            this.Agreement_Note.Caption = "Qeyd";
            this.Agreement_Note.FieldName = "NOTE";
            this.Agreement_Note.Name = "Agreement_Note";
            this.Agreement_Note.OptionsColumn.FixedWidth = true;
            this.Agreement_Note.Visible = true;
            this.Agreement_Note.VisibleIndex = 5;
            this.Agreement_Note.Width = 250;
            // 
            // Agreement_UsedUserID
            // 
            this.Agreement_UsedUserID.Caption = "UsedUserID";
            this.Agreement_UsedUserID.FieldName = "USED_USER_ID";
            this.Agreement_UsedUserID.Name = "Agreement_UsedUserID";
            this.Agreement_UsedUserID.OptionsColumn.AllowShowHide = false;
            // 
            // RepositoryItemPictureEdit
            // 
            this.RepositoryItemPictureEdit.Name = "RepositoryItemPictureEdit";
            this.RepositoryItemPictureEdit.NullText = " ";
            this.RepositoryItemPictureEdit.ZoomAccelerationFactor = 1D;
            // 
            // AgreementPopupMenu
            // 
            this.AgreementPopupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.NewBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.EditBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.DeleteBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.RefreshBarButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.ScheduleBarButton, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.HistroryBarButton, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.PrintBarButton, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.ExportBarButton)});
            this.AgreementPopupMenu.Manager = this.BarManager;
            this.AgreementPopupMenu.Name = "AgreementPopupMenu";
            // 
            // AgreementUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CustomerGridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AgreementUserControl";
            this.Size = new System.Drawing.Size(1430, 636);
            this.Load += new System.EventHandler(this.CustomerUserControl_Load);
            this.Enter += new System.EventHandler(this.CustomerUserControl_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.BarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepositoryItemPictureEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgreementPopupMenu)).EndInit();
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
        private DevExpress.XtraGrid.GridControl CustomerGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView CustomerGridView;
        private DevExpress.XtraGrid.Columns.GridColumn Agreement_SS;
        private DevExpress.XtraGrid.Columns.GridColumn Agreement_Note;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit RepositoryItemPictureEdit;
        private DevExpress.XtraGrid.Columns.GridColumn Agreement_UsedUserID;
        private DevExpress.XtraBars.BarButtonItem RtfBarButton;
        private DevExpress.XtraBars.PopupMenu AgreementPopupMenu;
        private DevExpress.XtraGrid.Columns.GridColumn Agreement_ID;
        private DevExpress.XtraGrid.Columns.GridColumn Agreement_Number;
        private DevExpress.XtraGrid.Columns.GridColumn Agreement_Date;
        private DevExpress.XtraGrid.Columns.GridColumn Agreement_Branch;
        private DevExpress.XtraGrid.Columns.GridColumn Agreement_Amount;
    }
}
