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
using ELMS.Class.DataAccess;
using ELMS.Class.Tables;

namespace ELMS.Forms.Dictionaries
{
    public partial class FBranchAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FBranchAddEdit()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public int? BranchID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void FBranchAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Filialların düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.BRANCH", GlobalVariables.V_UserID, "WHERE ID = " + BranchID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş filial hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
                        CurrentStatus = true;
                    }
                    else
                        CurrentStatus = false;
                }
                else
                    CurrentStatus = false;
                ComponentEnabled(CurrentStatus);
            }
            else
                this.Text = "Filialın əlavə edilməsi";
        }

        private void FBranchAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.BRANCH", -1, "WHERE ID = " + BranchID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        private void LoadDetails()
        {
            List<Branch> lstBranch = BranchDAL.SelectBranchByID(BranchID).ToList<Branch>();
            if (lstBranch.Count > 0)
            {
                var branch = lstBranch.LastOrDefault();
                NameText.EditValue = branch.NAME;
                LeadingNameText.EditValue = branch.LEADING_NAME;
                AddressText.EditValue = branch.ADDRESS;
                PhoneText.EditValue = branch.PHONE;
                NoteText.EditValue = branch.NOTE;
                UsedUserID = branch.USED_USER_ID;
                orderID = branch.ORDER_ID;
            }
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlDetail())
            {
                if (TransactionType == TransactionTypeEnum.Insert)
                    InsertDetail();
                else
                    UpdateDetail();
                this.Close();
            }
        }

        private void InsertDetail()
        {
            Branch branch = new Branch
            {
                NAME = NameText.Text.Trim(),
                LEADING_NAME = LeadingNameText.Text.Trim(),
                ADDRESS = AddressText.Text.Trim(),
                PHONE = PhoneText.Text.Trim(),
                NOTE = NoteText.Text.Trim()                
            };

            BranchDAL.InsertBranch(branch);
        }
        

        private void UpdateDetail()
        {
            isClickBOK = true;

            Branch branch = new Branch
            {
                NAME = NameText.Text.Trim(),
                LEADING_NAME = LeadingNameText.Text.Trim(),
                PHONE = PhoneText.Text.Trim(),
                ADDRESS = AddressText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                ID = BranchID.Value,
                ORDER_ID = orderID,
                USED_USER_ID = -1
            };

            BranchDAL.UpdateBranch(branch);
        }

        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
            LeadingNameText.Enabled =
            AddressText.Enabled =
            PhoneText.Enabled =
            NoteText.Enabled =
            BOK.Visible = !status;
        }

        private bool ControlDetail()
        {
            bool b = false;

            if (NameText.Text.Length == 0)
            {
                NameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Filialın adı daxil edilməyib.");
                NameText.Focus();
                NameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;
        }

    }
}