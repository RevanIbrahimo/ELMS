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


namespace ELMS.Forms.General
{
    public partial class FCardIssuingAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FCardIssuingAddEdit()
        {
            InitializeComponent();
        }
        
        public TransactionTypeEnum TransactionType;
        public int? CardIssuingID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void FCardIssuingAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CARD_ISSUING", -1, "WHERE ID = " + CardIssuingID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        private void FCardIssuingAddEdit_Load(object sender, EventArgs e)
        {

            if (TransactionType == TransactionTypeEnum.Update)
            {

                this.Text = "Orqanların düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CARD_ISSUING", GlobalVariables.V_UserID, "WHERE ID = " + CardIssuingID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş ölkə hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
                this.Text = "Orqanların əlavə edilməsi";
            //if (TransactionName == "EDIT")
            //{
            //    GlobalProcedures.Lock_or_UnLock_UserID("COMS_USER.CARD_ISSUING", GlobalVariables.V_UserID, "WHERE ID = " + IssuingID + " AND USED_USER_ID = -1");
            //    lstIssuing = CardIssuingDAL.SelectCardIssuingByID(int.Parse(IssuingID)).ToList<CardIssuing>();
            //    IssuingUsedUserID = lstIssuing.First().USED_USER_ID;
            //    IssuingUsed = (IssuingUsedUserID > 0);

            //    if (IssuingUsed)
            //    {
            //        if (GlobalVariables.V_UserID != IssuingUsedUserID)
            //        {
            //            string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == IssuingUsedUserID).FULLNAME;
            //            XtraMessageBox.Show("Seçilmiş orqanın adı hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.", "Seçilmiş orqanın hal-hazırkı statusu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            CurrentStatus = true;
            //        }
            //        else
            //            CurrentStatus = false;
            //    }
            //    else
            //        CurrentStatus = false;
            //    ComponentEnabled(CurrentStatus);
            //    LoadSeriesDetails();
            //}
        }

        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                NoteText.Enabled =
                BOK.Visible = !status;

        }

        private void LoadDetails()
        {
           

            List<CardIssuing> lstCardIssuing = CardIssuingDAL.SelectCardIssuingByID(CardIssuingID).ToList<CardIssuing>();
            if (lstCardIssuing.Count > 0)
            {
                var cardIssuing = lstCardIssuing.LastOrDefault();
                NameText.EditValue = cardIssuing.NAME;
                NoteText.EditValue = cardIssuing.NOTE;
                UsedUserID = cardIssuing.USED_USER_ID;
                orderID = cardIssuing.ORDER_ID;
            }
        }

        private bool ControlDetail()
        {

            bool b = false;

            if (NameText.Text.Length == 0)
            {
                NameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Orqanın adı daxil edilməyib.");
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
            CardIssuing cardIssuing = new CardIssuing
            {

                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim()
            };
            CardIssuingDAL.InsertCardIssuing(cardIssuing);
            
        }

        private void UpdateDetail()
        {
            isClickBOK = true;

            CardIssuing cardIssuing = new CardIssuing
            {
                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                ID = CardIssuingID.Value,
                ORDER_ID = orderID,
                USED_USER_ID = -1
            };

            CardIssuingDAL.UpdateCardIssuing(cardIssuing);
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