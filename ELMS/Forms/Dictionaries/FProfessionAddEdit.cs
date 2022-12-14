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
    public partial class FProfessionAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FProfessionAddEdit()
        {
            InitializeComponent();
        }
        
        public TransactionTypeEnum TransactionType;
        public int? ProfessionID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void FProfessionAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.PROFESSION", -1, "WHERE ID = " + ProfessionID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        private void FProfessionAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Peşənin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.PROFESSION", GlobalVariables.V_UserID, "WHERE ID = " + ProfessionID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş Peşə hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
                this.Text = "Peşələrin əlavə edilməsi";            
        }

        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                NoteText.Enabled =
                BOK.Visible = !status;
        }

        private void LoadDetails()
        {       
            List<Profession> lstProfession = ProfessionDAL.SelectProfessionByID(ProfessionID).ToList<Profession>();
            if (lstProfession.Count > 0)
            {
                var profession = lstProfession.LastOrDefault();
                NameText.EditValue = profession.NAME;
                NoteText.EditValue = profession.NOTE;
                UsedUserID = profession.USED_USER_ID;
                orderID = profession.ORDER_ID;
            }
        }

        private bool ControlDetail()
        {
            bool b = false;

            if (NameText.Text.Length == 0)
            {
                NameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Peşənin adı daxil edilməyib.");
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
            Profession profession = new Profession
            {
                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim()
            };
            ProfessionDAL.InsertProfession(profession);            
        }

        private void UpdateDetail()
        {
            isClickBOK = true;

            Profession profession = new Profession
            {
                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                ID = ProfessionID.Value,
                ORDER_ID = orderID,
                USED_USER_ID = -1
            };

            ProfessionDAL.UpdateProfession(profession);
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