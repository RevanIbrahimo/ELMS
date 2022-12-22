using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ELMS.Class;
using static ELMS.Class.Enum;

namespace ELMS.Forms
{
    public partial class FMailAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FMailAddEdit()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public MailOwnerEnum OwnerType;
        public int? MailID;
        public int? OwnerID;

        public delegate void DoEvent();
        public event DoEvent RefreshEmailDataGridView;

        private void FMailAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)  
                LoadMailDetails();                
        }

        private void ComponentEnabled(bool status)
        {
            MailText.Enabled =
            NoteText.Enabled =
            BOK.Visible = !status;
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FMailAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {            
            this.RefreshEmailDataGridView();
        }

        private bool ControlEmailDetails()
        {
            bool b = false;

            if (MailText.Text.Length == 0)
            {
                MailText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Elektron ünvan daxil edilməyib.");                
                MailText.Focus();
                MailText.BackColor = Color.White;
                return false;
            }
            else
                b = true;

            if (!RegexUtilities.IsValidEmail(MailText.Text))            
            {
                MailText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Elektron ünvan düz deyil.");                
                MailText.Focus();
                MailText.BackColor = Color.White;
                return false;
            }
            else
                b = true;

            if (OwnerType == MailOwnerEnum.User && (GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE M = '" + MailText.Text.Trim().ToUpper() + "'") > 0 || GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE M = '" + MailText.Text.Trim().ToLower() + "'") > 0))
            {
                MailText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage(MailText.Text + " elektron ünvanı sistem istifadəçiləri üçün artıq bazaya daxil edilib. Elektron ünvan təkrarlana bilməz.");                
                MailText.Focus();
                MailText.BackColor = Color.White;
                return false;
            }
            else
                b = true;

            if (OwnerType == MailOwnerEnum.Customer && (GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'C' AND M = '" + MailText.Text.Trim().ToUpper() + "'") > 0 || GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'C' AND M = '" + MailText.Text.Trim().ToLower() + "'") > 0))
            {
                MailText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage(MailText.Text + " elektron ünvanı müştərilər üçün artıq bazaya daxil edilib. Elektron ünvan təkrarlana bilməz.");                
                MailText.Focus();
                MailText.BackColor = Color.White;
                return false;
            }
            else
                b = true;

            if (OwnerType == MailOwnerEnum.CC && (GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'CC' AND M = '" + MailText.Text.Trim().ToUpper() + "'") > 0 || GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'CC' AND M = '" + MailText.Text.Trim().ToLower() + "'") > 0))
            {
                MailText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage(MailText.Text + " elektron ünvanı öhdəlik götürənlər üçün artıq bazaya daxil edilib. Elektron ünvan təkrarlana bilməz.");               
                MailText.Focus();
                MailText.BackColor = Color.White;
                return false;
            }
            else
                b = true;

            if (OwnerType == MailOwnerEnum.F && (GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'F' AND M = '" + MailText.Text.Trim().ToUpper() + "'") > 0 || GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'F' AND M = '" + MailText.Text.Trim().ToLower() + "'") > 0))
            {
                MailText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage(MailText.Text + " elektron ünvanı təsisçilər üçün artıq bazaya daxil edilib. Elektron ünvan təkrarlana bilməz.");               
                MailText.Focus();
                MailText.BackColor = Color.White;
                return false;
            }
            else
                b = true;

            return b;
        }

        private void InsertMail()
        {
            GlobalProcedures.ExecuteQuery("INSERT INTO ELMS_USER_TEMP.MAILS_TEMP(ID,OWNER_ID,OWNER_TYPE,MAIL,NOTE,IS_CHANGE,USED_USER_ID)VALUES(MAIL_SEQUENCE.NEXTVAL," + OwnerID + ",'" + OwnerType + "','" + MailText.Text.Trim() + "','" + NoteText.Text.Trim() + "',1," + GlobalVariables.V_UserID + ")",
                                                "Mail temp cədvələ daxil edilmədi.");
        }

        private void UpdateMail()
        {
            GlobalProcedures.ExecuteQuery($@"UPDATE ELMS_USER_TEMP.MAILS_TEMP SET MAIL = '{MailText.Text.Trim()}',NOTE = '{NoteText.Text.Trim()}',IS_CHANGE = 1 WHERE USED_USER_ID = {GlobalVariables.V_UserID} AND OWNER_TYPE = '{OwnerType}' AND ID = {MailID}",
                                                "Mail temp cədvəldə dəyişdirilmədi.");
        }

        private void LoadMailDetails()
        {
            string s = $@"SELECT MAIL,NOTE FROM ELMS_USER_TEMP.MAILS_TEMP WHERE OWNER_TYPE = '{OwnerType}' AND USED_USER_ID = {GlobalVariables.V_UserID} AND ID = {MailID}" ;
            DataTable dt = GlobalFunctions.GenerateDataTable(s, this.Name + "/LoadMailDetails", "Elektron ünvan açılmadı.");
            if(dt.Rows.Count > 0)
            {
                MailText.Text = dt.Rows[0]["MAIL"].ToString();
                NoteText.Text = dt.Rows[0]["NOTE"].ToString();
            }            
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlEmailDetails())
            {
                if (TransactionType == TransactionTypeEnum.Insert)
                    InsertMail();
                else
                    UpdateMail();
                this.Close();
            }
        }
    }
}