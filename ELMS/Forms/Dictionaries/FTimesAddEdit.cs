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
    public partial class FTimesAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FTimesAddEdit()
        {
            InitializeComponent();
        }
        
        public TransactionTypeEnum TransactionType;
        public int? TimesID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void FTimesAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.TIMES", -1, "WHERE ID = " + TimesID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        private void FTimesAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Müddətin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.TIMES", GlobalVariables.V_UserID, "WHERE ID = " + TimesID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş Müddət hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
                this.Text = "Müddətlərin əlavə edilməsi";            
        }

        private void ComponentEnabled(bool status)
        {
            PeriodValue.Enabled =
                PercentValue.Enabled =
                NoteText.Enabled =
                BOK.Visible = !status;
        }

        private void LoadDetails()
        {       
            List<Times> lstTimes = TimesDAL.SelectTimesByID(TimesID).ToList<Times>();
            if (lstTimes.Count > 0)
            {
                var times = lstTimes.LastOrDefault();
                PeriodValue.EditValue = times.PERIOD;
                PercentValue.EditValue = times.PERCENT;
                NoteText.EditValue = times.NOTE;
                UsedUserID = times.USED_USER_ID;
                orderID = times.ORDER_ID;
            }
        }

        private bool ControlDetail()
        {
            bool b = false;

            if (PeriodValue.Value == 0)
            {
                PeriodValue.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Müddət daxil edilməyib.");
                PeriodValue.Focus();
                PeriodValue.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (PercentValue.Value == 0)
            {
                PercentValue.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Faiz daxil edilməyib.");
                PercentValue.Focus();
                PercentValue.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;            
        }

        private void InsertDetail()
        {
            Times times = new Times
            {
                PERIOD = (int)PeriodValue.Value,
                PERCENT = PercentValue.Value,
                NOTE = NoteText.Text.Trim()
            };
            TimesDAL.InsertTimes(times);            
        }
        
        private void UpdateDetail()
        {
            isClickBOK = true;

            Times times = new Times
            {
                PERIOD = (int)PeriodValue.Value,
                PERCENT = PercentValue.Value,
                NOTE = NoteText.Text.Trim(),
                ID = TimesID.Value,
                ORDER_ID = orderID,
                USED_USER_ID = -1
            };

            TimesDAL.UpdateTimes(times);
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