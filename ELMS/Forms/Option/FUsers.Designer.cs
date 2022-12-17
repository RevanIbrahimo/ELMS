namespace ELMS.Forms.Option
{
    partial class FUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FUsers));
            this.UsersRibbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.NewBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.EditBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.DeleteBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.RefreshBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.UnLockBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.LockBarButton = new DevExpress.XtraBars.BarButtonItem();
            this.UsersRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.InfoRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.UsersGridControl = new DevExpress.XtraGrid.GridControl();
            this.UsersGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.User_SS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_CustomerFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_Note = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_IsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_UsedUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_SessionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.PopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.UsersRibbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepositoryItemPictureEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // UsersRibbon
            // 
            this.UsersRibbon.ExpandCollapseItem.Id = 0;
            this.UsersRibbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.UsersRibbon.ExpandCollapseItem,
            this.NewBarButton,
            this.EditBarButton,
            this.DeleteBarButton,
            this.RefreshBarButton,
            this.UnLockBarButton,
            this.LockBarButton});
            this.UsersRibbon.Location = new System.Drawing.Point(0, 0);
            this.UsersRibbon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UsersRibbon.MaxItemId = 7;
            this.UsersRibbon.Name = "UsersRibbon";
            this.UsersRibbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.UsersRibbonPage});
            this.UsersRibbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.UsersRibbon.ShowToolbarCustomizeItem = false;
            this.UsersRibbon.Size = new System.Drawing.Size(877, 143);
            this.UsersRibbon.StatusBar = this.ribbonStatusBar;
            this.UsersRibbon.Toolbar.ShowCustomizeItem = false;
            // 
            // NewBarButton
            // 
            this.NewBarButton.Caption = "Yeni";
            this.NewBarButton.Id = 1;
            this.NewBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.user_add_32;
            this.NewBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.NewBarButton.Name = "NewBarButton";
            this.NewBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.NewBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.NewBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.NewBarButton_ItemClick);
            // 
            // EditBarButton
            // 
            this.EditBarButton.Caption = "Dəyiş";
            this.EditBarButton.Id = 2;
            this.EditBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.user_edit_32;
            this.EditBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this.EditBarButton.Name = "EditBarButton";
            this.EditBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.EditBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.EditBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.EditBarButton_ItemClick);
            // 
            // DeleteBarButton
            // 
            this.DeleteBarButton.Caption = "Sil";
            this.DeleteBarButton.Id = 3;
            this.DeleteBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.user_delete_32;
            this.DeleteBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete));
            this.DeleteBarButton.Name = "DeleteBarButton";
            this.DeleteBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.DeleteBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.DeleteBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DeleteBarButton_ItemClick);
            // 
            // RefreshBarButton
            // 
            this.RefreshBarButton.Caption = "Təzələ";
            this.RefreshBarButton.Id = 4;
            this.RefreshBarButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RefreshBarButton.ImageOptions.Image")));
            this.RefreshBarButton.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.RefreshBarButton.Name = "RefreshBarButton";
            this.RefreshBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.RefreshBarButton.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            this.RefreshBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RefreshBarButton_ItemClick);
            // 
            // UnLockBarButton
            // 
            this.UnLockBarButton.Caption = "Blokdan çıxar";
            this.UnLockBarButton.Id = 5;
            this.UnLockBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.lock_off_32;
            this.UnLockBarButton.Name = "UnLockBarButton";
            this.UnLockBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.UnLockBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.UnLockBarButton_ItemClick);
            // 
            // LockBarButton
            // 
            this.LockBarButton.Caption = "Blokla";
            this.LockBarButton.Id = 6;
            this.LockBarButton.ImageOptions.Image = global::ELMS.Properties.Resources.lock_32;
            this.LockBarButton.Name = "LockBarButton";
            this.LockBarButton.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.LockBarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.LockBarButton_ItemClick);
            // 
            // UsersRibbonPage
            // 
            this.UsersRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.InfoRibbonPageGroup});
            this.UsersRibbonPage.Name = "UsersRibbonPage";
            this.UsersRibbonPage.Text = "İstifadəçilər";
            // 
            // InfoRibbonPageGroup
            // 
            this.InfoRibbonPageGroup.ItemLinks.Add(this.NewBarButton);
            this.InfoRibbonPageGroup.ItemLinks.Add(this.EditBarButton);
            this.InfoRibbonPageGroup.ItemLinks.Add(this.DeleteBarButton);
            this.InfoRibbonPageGroup.ItemLinks.Add(this.RefreshBarButton);
            this.InfoRibbonPageGroup.ItemLinks.Add(this.UnLockBarButton, true);
            this.InfoRibbonPageGroup.ItemLinks.Add(this.LockBarButton);
            this.InfoRibbonPageGroup.Name = "InfoRibbonPageGroup";
            this.InfoRibbonPageGroup.Text = "Məlumat";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 593);
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.UsersRibbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(877, 31);
            // 
            // UsersGridControl
            // 
            this.UsersGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersGridControl.Location = new System.Drawing.Point(0, 143);
            this.UsersGridControl.MainView = this.UsersGridView;
            this.UsersGridControl.Name = "UsersGridControl";
            this.UsersGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepositoryItemPictureEdit});
            this.UsersGridControl.Size = new System.Drawing.Size(877, 450);
            this.UsersGridControl.TabIndex = 59;
            this.UsersGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UsersGridView});
            // 
            // UsersGridView
            // 
            this.UsersGridView.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.UsersGridView.Appearance.FooterPanel.Options.UseFont = true;
            this.UsersGridView.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.UsersGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.UsersGridView.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.UsersGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.UsersGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.UsersGridView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.UsersGridView.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.UsersGridView.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.UsersGridView.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.UsersGridView.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.UsersGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.User_SS,
            this.User_CustomerFullName,
            this.User_Note,
            this.User_IsActive,
            this.User_UsedUserID,
            this.User_SessionID});
            this.UsersGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.UsersGridView.GridControl = this.UsersGridControl;
            this.UsersGridView.Name = "UsersGridView";
            this.UsersGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.UsersGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.UsersGridView.OptionsBehavior.Editable = false;
            this.UsersGridView.OptionsFilter.UseNewCustomFilterDialog = true;
            this.UsersGridView.OptionsFind.FindDelay = 100;
            this.UsersGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.UsersGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.UsersGridView.OptionsView.ShowFooter = true;
            this.UsersGridView.OptionsView.ShowGroupPanel = false;
            this.UsersGridView.OptionsView.ShowIndicator = false;
            this.UsersGridView.PaintStyleName = "Skin";
            this.UsersGridView.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;
            this.UsersGridView.ViewCaption = "Müqavilələrin siyahısı";
            this.UsersGridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.UsersGridView_RowCellStyle);
            this.UsersGridView.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.UsersGridView_FocusedRowObjectChanged);
            this.UsersGridView.ColumnFilterChanged += new System.EventHandler(this.UsersGridView_ColumnFilterChanged);
            this.UsersGridView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.UsersGridView_CustomColumnDisplayText);
            this.UsersGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UsersGridView_MouseUp);
            this.UsersGridView.DoubleClick += new System.EventHandler(this.UsersGridView_DoubleClick);
            // 
            // User_SS
            // 
            this.User_SS.AppearanceCell.Options.UseTextOptions = true;
            this.User_SS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.User_SS.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.User_SS.Caption = "S/s";
            this.User_SS.FieldName = "SS";
            this.User_SS.Name = "User_SS";
            this.User_SS.OptionsColumn.FixedWidth = true;
            this.User_SS.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "SS", "{0}")});
            this.User_SS.Visible = true;
            this.User_SS.VisibleIndex = 0;
            this.User_SS.Width = 45;
            // 
            // User_CustomerFullName
            // 
            this.User_CustomerFullName.Caption = "İstifadəçinin tam adı";
            this.User_CustomerFullName.FieldName = "CUSTOMERFULLNAME";
            this.User_CustomerFullName.Name = "User_CustomerFullName";
            this.User_CustomerFullName.Visible = true;
            this.User_CustomerFullName.VisibleIndex = 1;
            this.User_CustomerFullName.Width = 250;
            // 
            // User_Note
            // 
            this.User_Note.Caption = "Qeyd";
            this.User_Note.FieldName = "NOTE";
            this.User_Note.Name = "User_Note";
            this.User_Note.Visible = true;
            this.User_Note.VisibleIndex = 2;
            this.User_Note.Width = 250;
            // 
            // User_IsActive
            // 
            this.User_IsActive.Caption = "IsActive";
            this.User_IsActive.FieldName = "IS_ACTIVE";
            this.User_IsActive.Name = "User_IsActive";
            // 
            // User_UsedUserID
            // 
            this.User_UsedUserID.Caption = "UsedUserID";
            this.User_UsedUserID.FieldName = "USED_USER_ID";
            this.User_UsedUserID.Name = "User_UsedUserID";
            // 
            // User_SessionID
            // 
            this.User_SessionID.Caption = "SessionID";
            this.User_SessionID.FieldName = "SESSION_ID";
            this.User_SessionID.Name = "User_SessionID";
            // 
            // RepositoryItemPictureEdit
            // 
            this.RepositoryItemPictureEdit.Name = "RepositoryItemPictureEdit";
            this.RepositoryItemPictureEdit.NullText = " ";
            this.RepositoryItemPictureEdit.ZoomAccelerationFactor = 1D;
            // 
            // PopupMenu
            // 
            this.PopupMenu.ItemLinks.Add(this.NewBarButton);
            this.PopupMenu.ItemLinks.Add(this.EditBarButton);
            this.PopupMenu.ItemLinks.Add(this.DeleteBarButton);
            this.PopupMenu.ItemLinks.Add(this.RefreshBarButton);
            this.PopupMenu.ItemLinks.Add(this.UnLockBarButton);
            this.PopupMenu.ItemLinks.Add(this.LockBarButton);
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Ribbon = this.UsersRibbon;
            // 
            // FUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 624);
            this.Controls.Add(this.UsersGridControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.UsersRibbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FUsers";
            this.Ribbon = this.UsersRibbon;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "İstifadəçilər";
            this.Load += new System.EventHandler(this.FUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UsersRibbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepositoryItemPictureEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl UsersRibbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage UsersRibbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup InfoRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem NewBarButton;
        private DevExpress.XtraBars.BarButtonItem EditBarButton;
        private DevExpress.XtraBars.BarButtonItem DeleteBarButton;
        private DevExpress.XtraBars.BarButtonItem RefreshBarButton;
        private DevExpress.XtraGrid.GridControl UsersGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView UsersGridView;
        private DevExpress.XtraGrid.Columns.GridColumn User_SS;
        private DevExpress.XtraGrid.Columns.GridColumn User_CustomerFullName;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit RepositoryItemPictureEdit;
        private DevExpress.XtraBars.PopupMenu PopupMenu;
        private DevExpress.XtraGrid.Columns.GridColumn User_Note;
        private DevExpress.XtraGrid.Columns.GridColumn User_IsActive;
        private DevExpress.XtraGrid.Columns.GridColumn User_UsedUserID;
        private DevExpress.XtraGrid.Columns.GridColumn User_SessionID;
        private DevExpress.XtraBars.BarButtonItem UnLockBarButton;
        private DevExpress.XtraBars.BarButtonItem LockBarButton;
    }
}