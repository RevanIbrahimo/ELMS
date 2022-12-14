using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using static ELMS.Class.Enum;
using ELMS.Class;
using ELMS.Class.Tables;
using ELMS.Class.DataAccess;
using DevExpress.XtraGrid.Views.Grid;
using Oracle.ManagedDataAccess.Client;

namespace ELMS.Forms.Dictionaries
{
    public partial class FPhoneDescriptionsAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FPhoneDescriptionsAddEdit()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public int? DescriptionID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID, prefixID, topindex, old_row_id;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void PrefixGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Prefix_SS, e);
        }        

        private void FPhoneDescriptions_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Təsvirin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.PHONE_DESCRIPTIONS", GlobalVariables.V_UserID, "WHERE ID = " + DescriptionID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş təsvir hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
                        CurrentStatus = true;
                    }
                    else
                        CurrentStatus = false;
                }
                else
                    CurrentStatus = false;
                ComponentEnabled(CurrentStatus);
                InsertPrefixTemp();
            }
            else
            {
                this.Text = "Təsvirin əlavə edilməsi";
                DescriptionID = GlobalFunctions.GetOracleSequenceValue("PHONE_DESCRIPTIONS_SEQUENCE");
            }
            LoadPrefix();
        }

        private void LoadDetails()
        {
            List<PhoneDescription> lstDescription = PhoneDescriptionDAL.SelectPhoneDescriptionByID(DescriptionID).ToList<PhoneDescription>();
            if (lstDescription.Count > 0)
            {
                var description = lstDescription.LastOrDefault();
                NameText.EditValue = description.NAME;
                NoteText.EditValue = description.NOTE;
                UsedUserID = description.USED_USER_ID;
                orderID = description.ORDER_ID;
            }
        }

        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                NoteText.Enabled =
                BOK.Visible = !status;
        }

        private void FPhoneDescriptionsAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalFunctions.RunInOneTransaction<int>(tran =>
            {
                if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                    GlobalProcedures.Lock_or_UnLock_UserID(tran, "ELMS_USER.PHONE_DESCRIPTIONS", -1, "WHERE ID = " + DescriptionID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
                PhonePrefixDAL.DeleteTempPhonePrefix(tran);
                return 1;
            }, "Prefikslər temp cədvəldən silinmədi.");
            
            this.RefreshDataGridView();
        }

        private void LoadPrefix()
        {
            PrefixGridControl.DataSource = PhonePrefixDAL.SelectTempPhoneProfixByDescriptionID(DescriptionID.Value).ToList<PhonePrefix>();
            EditBarButton.Enabled = DeleteBarButton.Enabled = PrefixGridView.RowCount > 0;
        }

        private void InsertPrefixTemp()
        {
            GlobalProcedures.ExecuteProcedureWithUser("ELMS_USER_TEMP.PROC_INSERT_PHONE_PREFIX_TEMP", "P_DESCRIPTION_ID", DescriptionID, "Prefikslər temp cədvələ daxil olmadı.");
        }

        private void RefreshBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPrefix();
        }

        private void PrefixGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            prefixID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
        }

        void UpdatePrefix()
        {
            LoadFPhonePrefixAddEdit(TransactionTypeEnum.Update, prefixID);
        }

        private void EditBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdatePrefix();
        }

        private void PrefixGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditBarButton.Enabled)
                UpdatePrefix();
        }

        private void PrefixGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(PrefixGridView, PopupMenu, e);
        }

        private void DeleteBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (GlobalFunctions.CallDialogResult("Seçilmiş prefiksi silmək istəyirsiniz?", "Prefiksin silinməsi") == DialogResult.Yes)
                PhonePrefixDAL.DeletePhonePrefix(prefixID);
            LoadPrefix();
        }

        private void LoadFPhonePrefixAddEdit(TransactionTypeEnum transaction, int? id)
        {
            topindex = PrefixGridView.TopRowIndex;
            old_row_id = PrefixGridView.FocusedRowHandle;
            FPhonePrefixAddEdit fp = new FPhonePrefixAddEdit()
            {
                TransactionType = transaction,
                DescriptionID = DescriptionID.Value,
                PrefixID = id
            };
            fp.RefreshDataGridView += new FPhonePrefixAddEdit.DoEvent(LoadPrefix);
            fp.ShowDialog();
            PrefixGridView.TopRowIndex = topindex;
            PrefixGridView.FocusedRowHandle = old_row_id;
        }

        private void NewBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFPhonePrefixAddEdit(TransactionTypeEnum.Insert, null);
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlDetail())
            {
                GlobalFunctions.RunInOneTransaction<int>(tran =>
                {
                    if (TransactionType == TransactionTypeEnum.Insert)
                        InsertDetail(tran);
                    else
                        UpdateDetail(tran);
                    GlobalProcedures.ExecuteProcedureWithParametr(tran, "ELMS_USER.PROC_INSERT_PHONE_PREFIX", "P_DESCRIPTION_ID", DescriptionID.Value);
                    
                    return 1;
                }, TransactionType == TransactionTypeEnum.Insert? "Təsvir bazaya daxil edilmədi." : "Təsvir bazada dəyişdirilmədi.");
                
                this.Close();
            }
        }

        private bool ControlDetail()
        {
            bool b = false;

            if (NameText.Text.Length == 0)
            {
                NameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Təsvirin adı daxil edilməyib.");
                NameText.Focus();
                NameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;
        }

        private void InsertDetail(OracleTransaction tran)
        {
            PhoneDescription description = new PhoneDescription
            {
                ID = DescriptionID.Value,
                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim()
            };

            PhoneDescriptionDAL.InsertPhoneDescription(tran, description);
        }

        private void UpdateDetail(OracleTransaction tran)
        {
            isClickBOK = true;

            PhoneDescription description = new PhoneDescription
            {
                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                ID = DescriptionID.Value,
                ORDER_ID = orderID,
                USED_USER_ID = -1
            };

            PhoneDescriptionDAL.UpdatePhoneDescription(tran, description);
        }
    }
}