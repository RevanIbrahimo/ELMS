using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ELMS.Class;
using DevExpress.XtraEditors;
using static ELMS.Class.Enum;

namespace ELMS.Forms.Option
{
    public partial class FUsers : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FUsers()
        {
            InitializeComponent();
        }
        int userID, topindex, old_row_id, session_id = 0, used_user_id = -1;

        private void NewBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadFUserAddEdit(TransactionTypeEnum.Insert, null);
        }

        private void LoadFUserAddEdit(TransactionTypeEnum transactionType, int? userID)
        {
            topindex = UsersGridView.TopRowIndex;
            old_row_id = UsersGridView.FocusedRowHandle;
            FUserAddEdit fu = new FUserAddEdit();
            fu.TransactionType = transactionType;
            fu.UserID = userID;
            fu.RefreshUserDataGridView += new FUserAddEdit.DoEvent(LoadUserDetails);
            fu.ShowDialog();
            UsersGridView.TopRowIndex = topindex;
            UsersGridView.FocusedRowHandle = old_row_id;
        }

        private void UsersGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(UsersGridView, PopupMenu, e);
        }

        private void RefreshBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadUserDetails();
        }

        private void LoadUserDetails()
        {
            string s = $@"SELECT 1 SS,
                                   ID,
                                   FULL_NAME CUSTOMERFULLNAME,
                                   NOTE,
                                   IS_ACTIVE,
                                   USED_USER_ID,
                                   SESSION_ID
                              FROM ELMS_USER.SYSTEM_USER";

            UsersGridControl.DataSource = GlobalFunctions.GenerateDataTable(s, this.Name + "/LoadUserDetails", "İstifadəçilərin siyahısı yüklənmədi");

            if (UsersGridView.RowCount > 0)
            {
                if (GlobalVariables.V_UserID > 1)
                {
                    EditBarButton.Enabled = GlobalVariables.NewUser;
                    DeleteBarButton.Enabled = GlobalVariables.DeleteUser;
                }
                else
                    EditBarButton.Enabled = DeleteBarButton.Enabled = true;
                UnLockBarButton.Enabled = LockBarButton.Enabled = true;
            }
            else
                DeleteBarButton.Enabled = EditBarButton.Enabled = UnLockBarButton.Enabled = LockBarButton.Enabled = false;
        }

        private void FUsers_Load(object sender, EventArgs e)
        {
            LoadUserDetails();
        }

        private void UsersGridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            GlobalProcedures.GridCustomColumnDisplayText(e);
        }

        private void UsersGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow row = UsersGridView.GetFocusedDataRow();
            if (row != null)
            {
                userID = Convert.ToInt32(row["ID"]);
                session_id = Convert.ToInt32(row["SESSION_ID"].ToString());
                used_user_id = Convert.ToInt32(row["USED_USER_ID"].ToString());
            }
        }

        private void EditBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadFUserAddEdit(TransactionTypeEnum.Update, userID);
        }

        private void UsersGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditBarButton.Enabled)
                LoadFUserAddEdit(TransactionTypeEnum.Update, userID);
        }

        private void UsersGridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock(UsersGridView, e);
            GlobalProcedures.GridRowCellStyleForConnect(UsersGridView, e);
        }

        private void UsersGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (UsersGridView.RowCount > 0)
            {
                EditBarButton.Enabled = GlobalVariables.NewUser;
                DeleteBarButton.Enabled = GlobalVariables.DeleteUser;
                UnLockBarButton.Enabled = LockBarButton.Enabled = true;
            }
            else
                DeleteBarButton.Enabled = EditBarButton.Enabled = UnLockBarButton.Enabled = LockBarButton.Enabled = false;
        }
        

        private void UnLockBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                GlobalProcedures.ExecuteQuery($@"UPDATE MCMS.MCMS_USERS SET IS_ACTIVE = 1, SESSION_ID = 0 WHERE ID = {userID}",
                                                    "İstifadəçi blokdan çıxmadı.");
                XtraMessageBox.Show("Seçilmiş istifadəçi blokdan çıxdı.");
            }
            catch { }
        }

        private void LockBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                GlobalProcedures.ExecuteQuery($@"UPDATE MCMS.MCMS_USERS SET IS_ACTIVE = 0, SESSION_ID = 0 WHERE ID = {userID}",
                                                    "İstifadəçi bloklanmadı.");
                XtraMessageBox.Show("Seçilmiş istifadəçi bloklandı.");
            }
            catch { }
        }

        private void DeleteUser()
        {
            DialogResult dialogResult = XtraMessageBox.Show("Seçilmiş istifadəçini silmək istəyirsiniz?", "İstifadəçinin silinməsi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                GlobalProcedures.ExecuteProcedureWithParametr("MCMS.PROC_USER_DELETE", "P_USER_ID", userID, "Seçilmiş istifadəçi bazadan silinmədi.");
            }
        }

        private void DeleteBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (userID == 1)
            {
                XtraMessageBox.Show("İstifadəçi admin olduğu üçün onu bazadan silmək olmaz.", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (session_id != 0)
                XtraMessageBox.Show("İstifadəçi hal-hazırda sistemə qoşulduğu üçün onu bazadan silmək olmaz.", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (used_user_id >= 0)
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == used_user_id).FULL_NAME;
                XtraMessageBox.Show("İstifadəçinin məlumatları hal-hazırda " + used_user_name + " tərəfindən istifadə edildiyi üçün onu bazadan silmək olmaz.", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DeleteUser();
                LoadUserDetails();
            }
        }
    }
}