namespace ELMS.UserControls
{
    partial class BookkeepingUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookkeepingUserControl));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule5 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue5 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule6 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue6 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.Bookkeeping_T1D = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Bookkeeping_T1C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Bookkeeping_T2D = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Bookkeeping_T2C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Bookkeeping_DDEBT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Bookkeeping_CDEBT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.ToolBar = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem12 = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.BookeepingPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.OperationsGridControl = new DevExpress.XtraGrid.GridControl();
            this.OperationsGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Bookkeeping_SS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Bookkeeping_AccountNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Bookkeeping_AccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Bookkeeping_FullAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookeepingPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperationsGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperationsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Bookkeeping_T1D
            // 
            this.Bookkeeping_T1D.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.Bookkeeping_T1D.AppearanceHeader.ForeColor = System.Drawing.Color.Red;
            this.Bookkeeping_T1D.AppearanceHeader.Options.UseFont = true;
            this.Bookkeeping_T1D.AppearanceHeader.Options.UseForeColor = true;
            this.Bookkeeping_T1D.Caption = "Dövrün əvvəlinə debet";
            this.Bookkeeping_T1D.DisplayFormat.FormatString = "### ### ### ### ### ### ##0.00";
            this.Bookkeeping_T1D.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.Bookkeeping_T1D.FieldName = "T1D";
            this.Bookkeeping_T1D.Name = "Bookkeeping_T1D";
            this.Bookkeeping_T1D.OptionsColumn.FixedWidth = true;
            this.Bookkeeping_T1D.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "T1D", "{0:### ### ### ### ### ### ##0.00}")});
            this.Bookkeeping_T1D.Visible = true;
            this.Bookkeeping_T1D.VisibleIndex = 3;
            this.Bookkeeping_T1D.Width = 140;
            // 
            // Bookkeeping_T1C
            // 
            this.Bookkeeping_T1C.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.Bookkeeping_T1C.AppearanceHeader.ForeColor = System.Drawing.Color.Red;
            this.Bookkeeping_T1C.AppearanceHeader.Options.UseFont = true;
            this.Bookkeeping_T1C.AppearanceHeader.Options.UseForeColor = true;
            this.Bookkeeping_T1C.Caption = "Dövrün əvvəlinə kredit";
            this.Bookkeeping_T1C.DisplayFormat.FormatString = "### ### ### ### ### ### ##0.00";
            this.Bookkeeping_T1C.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.Bookkeeping_T1C.FieldName = "T1C";
            this.Bookkeeping_T1C.Name = "Bookkeeping_T1C";
            this.Bookkeeping_T1C.OptionsColumn.FixedWidth = true;
            this.Bookkeeping_T1C.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "T1C", "{0:### ### ### ### ### ### ##0.00}")});
            this.Bookkeeping_T1C.Visible = true;
            this.Bookkeeping_T1C.VisibleIndex = 4;
            this.Bookkeeping_T1C.Width = 140;
            // 
            // Bookkeeping_T2D
            // 
            this.Bookkeeping_T2D.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.Bookkeeping_T2D.AppearanceHeader.ForeColor = System.Drawing.Color.Blue;
            this.Bookkeeping_T2D.AppearanceHeader.Options.UseFont = true;
            this.Bookkeeping_T2D.AppearanceHeader.Options.UseForeColor = true;
            this.Bookkeeping_T2D.Caption = "Dövür ərzində debet";
            this.Bookkeeping_T2D.DisplayFormat.FormatString = "### ### ### ### ### ### ##0.00";
            this.Bookkeeping_T2D.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.Bookkeeping_T2D.FieldName = "T2D";
            this.Bookkeeping_T2D.Name = "Bookkeeping_T2D";
            this.Bookkeeping_T2D.OptionsColumn.FixedWidth = true;
            this.Bookkeeping_T2D.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "T2D", "{0:### ### ### ### ### ### ##0.00}")});
            this.Bookkeeping_T2D.Visible = true;
            this.Bookkeeping_T2D.VisibleIndex = 5;
            this.Bookkeeping_T2D.Width = 140;
            // 
            // Bookkeeping_T2C
            // 
            this.Bookkeeping_T2C.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.Bookkeeping_T2C.AppearanceHeader.ForeColor = System.Drawing.Color.Blue;
            this.Bookkeeping_T2C.AppearanceHeader.Options.UseFont = true;
            this.Bookkeeping_T2C.AppearanceHeader.Options.UseForeColor = true;
            this.Bookkeeping_T2C.Caption = "Dövür ərzində kredit";
            this.Bookkeeping_T2C.DisplayFormat.FormatString = "### ### ### ### ### ### ##0.00";
            this.Bookkeeping_T2C.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.Bookkeeping_T2C.FieldName = "T2C";
            this.Bookkeeping_T2C.Name = "Bookkeeping_T2C";
            this.Bookkeeping_T2C.OptionsColumn.FixedWidth = true;
            this.Bookkeeping_T2C.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "T2C", "{0:### ### ### ### ### ### ##0.00}")});
            this.Bookkeeping_T2C.Visible = true;
            this.Bookkeeping_T2C.VisibleIndex = 6;
            this.Bookkeeping_T2C.Width = 140;
            // 
            // Bookkeeping_DDEBT
            // 
            this.Bookkeeping_DDEBT.AppearanceCell.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.Bookkeeping_DDEBT.AppearanceCell.Options.UseFont = true;
            this.Bookkeeping_DDEBT.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.Bookkeeping_DDEBT.AppearanceHeader.Options.UseFont = true;
            this.Bookkeeping_DDEBT.Caption = "Dövrün sonuna debet";
            this.Bookkeeping_DDEBT.DisplayFormat.FormatString = "### ### ### ### ### ### ##0.00";
            this.Bookkeeping_DDEBT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.Bookkeeping_DDEBT.FieldName = "DDEBT";
            this.Bookkeeping_DDEBT.Name = "Bookkeeping_DDEBT";
            this.Bookkeeping_DDEBT.OptionsColumn.FixedWidth = true;
            this.Bookkeeping_DDEBT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DDEBT", "{0:### ### ### ### ### ### ##0.00}")});
            this.Bookkeeping_DDEBT.Visible = true;
            this.Bookkeeping_DDEBT.VisibleIndex = 7;
            this.Bookkeeping_DDEBT.Width = 140;
            // 
            // Bookkeeping_CDEBT
            // 
            this.Bookkeeping_CDEBT.AppearanceCell.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.Bookkeeping_CDEBT.AppearanceCell.Options.UseFont = true;
            this.Bookkeeping_CDEBT.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.Bookkeeping_CDEBT.AppearanceHeader.Options.UseFont = true;
            this.Bookkeeping_CDEBT.Caption = "Dövrun sonuna kredit";
            this.Bookkeeping_CDEBT.DisplayFormat.FormatString = "### ### ### ### ### ### ##0.00";
            this.Bookkeeping_CDEBT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.Bookkeeping_CDEBT.FieldName = "CDEBT";
            this.Bookkeeping_CDEBT.Name = "Bookkeeping_CDEBT";
            this.Bookkeeping_CDEBT.OptionsColumn.FixedWidth = true;
            this.Bookkeeping_CDEBT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CDEBT", "{0:### ### ### ### ### ### ##0.00}")});
            this.Bookkeeping_CDEBT.Visible = true;
            this.Bookkeeping_CDEBT.VisibleIndex = 8;
            this.Bookkeeping_CDEBT.Width = 140;
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
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonItem8,
            this.barButtonItem9,
            this.barButtonItem11,
            this.barButtonItem12,
            this.barSubItem1});
            this.BarManager.MaxItemId = 30;
            this.BarManager.StatusBar = this.bar3;
            // 
            // ToolBar
            // 
            this.ToolBar.BarName = "Tools";
            this.ToolBar.DockCol = 0;
            this.ToolBar.DockRow = 0;
            this.ToolBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.ToolBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem8),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem9, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem11, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem12)});
            this.ToolBar.OptionsBar.DrawBorder = false;
            this.ToolBar.OptionsBar.DrawDragBorder = false;
            this.ToolBar.OptionsBar.UseWholeRow = true;
            this.ToolBar.Text = "Tools";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Jurnal";
            this.barButtonItem1.Id = 16;
            this.barButtonItem1.ImageOptions.Image = global::ELMS.Properties.Resources.journal_32;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Detallar";
            this.barButtonItem2.Id = 17;
            this.barButtonItem2.ImageOptions.Image = global::ELMS.Properties.Resources.view_detail_32;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Yenilə";
            this.barButtonItem3.Id = 18;
            this.barButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Hesablar planı";
            this.barButtonItem4.Id = 19;
            this.barButtonItem4.ImageOptions.Image = global::ELMS.Properties.Resources.notebook;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Hesabların ilkin qalıqları";
            this.barButtonItem5.Id = 20;
            this.barButtonItem5.ImageOptions.Image = global::ELMS.Properties.Resources.safe_32;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "Mənfəət və zərər";
            this.barButtonItem6.Id = 21;
            this.barButtonItem6.ImageOptions.Image = global::ELMS.Properties.Resources.profits_32;
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "Məzuniyyətlər tarixləri";
            this.barButtonItem7.Id = 22;
            this.barButtonItem7.ImageOptions.Image = global::ELMS.Properties.Resources.calendar_month_32;
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "Kassa";
            this.barButtonItem8.Id = 23;
            this.barButtonItem8.ImageOptions.Image = global::ELMS.Properties.Resources.cash_32png;
            this.barButtonItem8.Name = "barButtonItem8";
            this.barButtonItem8.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem9
            // 
            this.barButtonItem9.Caption = "Çap";
            this.barButtonItem9.Id = 24;
            this.barButtonItem9.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem9.ImageOptions.Image")));
            this.barButtonItem9.Name = "barButtonItem9";
            this.barButtonItem9.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "İxrac";
            this.barSubItem1.Id = 29;
            this.barSubItem1.ImageOptions.Image = global::ELMS.Properties.Resources.table_export_32;
            this.barSubItem1.Name = "barSubItem1";
            this.barSubItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem11
            // 
            this.barButtonItem11.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItem11.Caption = "Ətraflı axtar";
            this.barButtonItem11.Down = true;
            this.barButtonItem11.Id = 26;
            this.barButtonItem11.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem11.ImageOptions.Image")));
            this.barButtonItem11.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.F));
            this.barButtonItem11.Name = "barButtonItem11";
            this.barButtonItem11.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem11.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            // 
            // barButtonItem12
            // 
            this.barButtonItem12.Caption = "Filtrləri sil";
            this.barButtonItem12.Id = 27;
            this.barButtonItem12.ImageOptions.Image = global::ELMS.Properties.Resources.filter_delete_32;
            this.barButtonItem12.Name = "barButtonItem12";
            this.barButtonItem12.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
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
            // BookeepingPopupMenu
            // 
            this.BookeepingPopupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem8),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem9),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem11),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem12)});
            this.BookeepingPopupMenu.Manager = this.BarManager;
            this.BookeepingPopupMenu.Name = "BookeepingPopupMenu";
            // 
            // OperationsGridControl
            // 
            this.OperationsGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OperationsGridControl.Location = new System.Drawing.Point(0, 47);
            this.OperationsGridControl.MainView = this.OperationsGridView;
            this.OperationsGridControl.Name = "OperationsGridControl";
            this.OperationsGridControl.Size = new System.Drawing.Size(1430, 566);
            this.OperationsGridControl.TabIndex = 79;
            this.OperationsGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.OperationsGridView});
            // 
            // OperationsGridView
            // 
            this.OperationsGridView.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.OperationsGridView.Appearance.FooterPanel.Options.UseFont = true;
            this.OperationsGridView.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.OperationsGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.OperationsGridView.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OperationsGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.OperationsGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OperationsGridView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OperationsGridView.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.OperationsGridView.Appearance.ViewCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.OperationsGridView.Appearance.ViewCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.OperationsGridView.Appearance.ViewCaption.Options.UseFont = true;
            this.OperationsGridView.Appearance.ViewCaption.Options.UseForeColor = true;
            this.OperationsGridView.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.OperationsGridView.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.OperationsGridView.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OperationsGridView.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.OperationsGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Bookkeeping_SS,
            this.Bookkeeping_AccountNumber,
            this.Bookkeeping_AccountName,
            this.Bookkeeping_T1D,
            this.Bookkeeping_T1C,
            this.Bookkeeping_T2D,
            this.Bookkeeping_T2C,
            this.Bookkeeping_DDEBT,
            this.Bookkeeping_CDEBT,
            this.Bookkeeping_FullAccountName});
            this.OperationsGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.Bookkeeping_T1D;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue1.PredefinedName = "Red Text";
            formatConditionRuleValue1.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.Bookkeeping_T1C;
            gridFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue2.PredefinedName = "Red Text";
            formatConditionRuleValue2.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule2.Rule = formatConditionRuleValue2;
            gridFormatRule3.Column = this.Bookkeeping_T2D;
            gridFormatRule3.Name = "Format2";
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue3.PredefinedName = "Red Text";
            formatConditionRuleValue3.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.Column = this.Bookkeeping_T2C;
            gridFormatRule4.Name = "Format3";
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue4.PredefinedName = "Red Text";
            formatConditionRuleValue4.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule4.Rule = formatConditionRuleValue4;
            gridFormatRule5.Column = this.Bookkeeping_DDEBT;
            gridFormatRule5.Name = "Format4";
            formatConditionRuleValue5.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue5.PredefinedName = "Red Text";
            formatConditionRuleValue5.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule5.Rule = formatConditionRuleValue5;
            gridFormatRule6.Column = this.Bookkeeping_CDEBT;
            gridFormatRule6.Name = "Format5";
            formatConditionRuleValue6.Condition = DevExpress.XtraEditors.FormatCondition.Less;
            formatConditionRuleValue6.PredefinedName = "Red Text";
            formatConditionRuleValue6.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridFormatRule6.Rule = formatConditionRuleValue6;
            this.OperationsGridView.FormatRules.Add(gridFormatRule1);
            this.OperationsGridView.FormatRules.Add(gridFormatRule2);
            this.OperationsGridView.FormatRules.Add(gridFormatRule3);
            this.OperationsGridView.FormatRules.Add(gridFormatRule4);
            this.OperationsGridView.FormatRules.Add(gridFormatRule5);
            this.OperationsGridView.FormatRules.Add(gridFormatRule6);
            this.OperationsGridView.GridControl = this.OperationsGridControl;
            this.OperationsGridView.Name = "OperationsGridView";
            this.OperationsGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.OperationsGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.OperationsGridView.OptionsBehavior.Editable = false;
            this.OperationsGridView.OptionsFilter.UseNewCustomFilterDialog = true;
            this.OperationsGridView.OptionsFind.FindDelay = 100;
            this.OperationsGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.OperationsGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.OperationsGridView.OptionsView.ShowFooter = true;
            this.OperationsGridView.OptionsView.ShowGroupPanel = false;
            this.OperationsGridView.OptionsView.ShowIndicator = false;
            this.OperationsGridView.OptionsView.ShowViewCaption = true;
            this.OperationsGridView.PaintStyleName = "Skin";
            this.OperationsGridView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            this.OperationsGridView.ViewCaption = "Dövriyyə";
            // 
            // Bookkeeping_SS
            // 
            this.Bookkeeping_SS.AppearanceCell.Options.UseTextOptions = true;
            this.Bookkeeping_SS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Bookkeeping_SS.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Bookkeeping_SS.Caption = "S/s";
            this.Bookkeeping_SS.FieldName = "SS";
            this.Bookkeeping_SS.Name = "Bookkeeping_SS";
            this.Bookkeeping_SS.OptionsColumn.ReadOnly = true;
            this.Bookkeeping_SS.Visible = true;
            this.Bookkeeping_SS.VisibleIndex = 0;
            this.Bookkeeping_SS.Width = 34;
            // 
            // Bookkeeping_AccountNumber
            // 
            this.Bookkeeping_AccountNumber.AppearanceCell.Options.UseTextOptions = true;
            this.Bookkeeping_AccountNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Bookkeeping_AccountNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Bookkeeping_AccountNumber.Caption = "Hesab nömrəsi";
            this.Bookkeeping_AccountNumber.FieldName = "ACCOUNT_NUMBER";
            this.Bookkeeping_AccountNumber.Name = "Bookkeeping_AccountNumber";
            this.Bookkeeping_AccountNumber.OptionsColumn.FixedWidth = true;
            this.Bookkeeping_AccountNumber.Visible = true;
            this.Bookkeeping_AccountNumber.VisibleIndex = 1;
            this.Bookkeeping_AccountNumber.Width = 100;
            // 
            // Bookkeeping_AccountName
            // 
            this.Bookkeeping_AccountName.Caption = "Hesabın adı";
            this.Bookkeeping_AccountName.FieldName = "ACCOUNT_NAME";
            this.Bookkeeping_AccountName.Name = "Bookkeeping_AccountName";
            this.Bookkeeping_AccountName.Visible = true;
            this.Bookkeeping_AccountName.VisibleIndex = 2;
            this.Bookkeeping_AccountName.Width = 311;
            // 
            // Bookkeeping_FullAccountName
            // 
            this.Bookkeeping_FullAccountName.Caption = "Hesab";
            this.Bookkeeping_FullAccountName.FieldName = "ACCOUNTNAME";
            this.Bookkeeping_FullAccountName.Name = "Bookkeeping_FullAccountName";
            // 
            // BookkeepingUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OperationsGridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "BookkeepingUserControl";
            this.Size = new System.Drawing.Size(1430, 636);
            ((System.ComponentModel.ISupportInitialize)(this.BarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookeepingPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperationsGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperationsGridView)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem EditBarButton;
        private DevExpress.XtraBars.BarButtonItem RefreshBarButton;
        private DevExpress.XtraBars.BarButtonItem PrintBarButton;
        private DevExpress.XtraBars.BarSubItem ExportBarButton;
        private DevExpress.XtraBars.BarButtonItem ExcelBarButton;
        private DevExpress.XtraBars.BarButtonItem PdfBarButton;
        private DevExpress.XtraBars.BarButtonItem TxtBarButton;
        private DevExpress.XtraBars.BarButtonItem HtmlBarButton;
        private DevExpress.XtraBars.BarButtonItem CsvBarButton;
        private DevExpress.XtraBars.BarButtonItem MhtBarButton;
        private DevExpress.XtraBars.BarButtonItem RtfBarButton;
        private DevExpress.XtraBars.PopupMenu BookeepingPopupMenu;
        private DevExpress.XtraGrid.GridControl OperationsGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView OperationsGridView;
        private DevExpress.XtraGrid.Columns.GridColumn Bookkeeping_SS;
        private DevExpress.XtraGrid.Columns.GridColumn Bookkeeping_AccountNumber;
        private DevExpress.XtraGrid.Columns.GridColumn Bookkeeping_AccountName;
        private DevExpress.XtraGrid.Columns.GridColumn Bookkeeping_T1D;
        private DevExpress.XtraGrid.Columns.GridColumn Bookkeeping_T1C;
        private DevExpress.XtraGrid.Columns.GridColumn Bookkeeping_T2D;
        private DevExpress.XtraGrid.Columns.GridColumn Bookkeeping_T2C;
        private DevExpress.XtraGrid.Columns.GridColumn Bookkeeping_DDEBT;
        private DevExpress.XtraGrid.Columns.GridColumn Bookkeeping_CDEBT;
        private DevExpress.XtraGrid.Columns.GridColumn Bookkeeping_FullAccountName;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barButtonItem9;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem11;
        private DevExpress.XtraBars.BarButtonItem barButtonItem12;
    }
}
