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
using ELMS.Class;
using ELMS.Class.Tables;
using ELMS.Class.DataAccess;
using static ELMS.Class.Enum;


namespace ELMS.Forms.Dictionaries
{
    public partial class FSourceAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FSourceAddEdit()
        {
            InitializeComponent();
        }
        
        public TransactionTypeEnum TransactionType;
        public int? SourceID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void FSourceAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.SOURCE", -1, "WHERE ID = " + SourceID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        private void FSourceAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Mənbənin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.SOURCE", GlobalVariables.V_UserID, "WHERE ID = " + SourceID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş Mənbə hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
                this.Text = "Mənbələrin əlavə edilməsi";            
        }

        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                NoteText.Enabled =
                BOK.Visible = !status;
        }

        private void LoadDetails()
        {       
            List<Source> lstSource = SourceDAL.SelectSourceByID(SourceID).ToList<Source>();
            if (lstSource.Count > 0)
            {
                var source = lstSource.LastOrDefault();
                NameText.EditValue = source.NAME;
                NoteText.EditValue = source.NOTE;
                UsedUserID = source.USED_USER_ID;
                orderID = source.ORDER_ID;
            }
        }

        private bool ControlDetail()
        {
            bool b = false;

            if (NameText.Text.Length == 0)
            {
                NameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Mənbənin adı daxil edilməyib.");
                NameText.Focus();
                NameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;
           

            return b;            
        }

        private void InsertDetail()
        {
            Source source = new Source
            {
                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim()
            };
            SourceDAL.InsertSource(source);            
        }

        private void UpdateDetail()
        {
            isClickBOK = true;

            Source source = new Source
            {
                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                ID = SourceID.Value,
                ORDER_ID = orderID,
                USED_USER_ID = -1
            };

            SourceDAL.UpdateSource(source);
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

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}