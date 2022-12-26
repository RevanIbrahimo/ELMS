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
using ELMS.Class.DataAccess;

namespace ELMS.Forms.Mail
{
    public partial class FMailAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FMailAddEdit()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public MailOwnerEnum MailOwner;
        public int? MailID;
        public int? OwnerID;

        public delegate void DoEvent();
        public event DoEvent RefreshEmailDataGridView;

        private void FMailAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)  
                LoadMailDetails();                
        }

        private void LoadMailDetails()
        {

            DataTable dt = MailDAL.SelectMailByOwnerID((int)OwnerID, MailOwner, MailID);

            if (dt.Rows.Count > 0)
            {
                MailText.EditValue = dt.Rows[0]["MAIL"];
                NoteText.EditValue = dt.Rows[0]["NOTE"];
            }
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

            if (MailOwner == MailOwnerEnum.User && (GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE M = '" + MailText.Text.Trim().ToUpper() + "'") > 0 || GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE M = '" + MailText.Text.Trim().ToLower() + "'") > 0))
            {
                MailText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage(MailText.Text + " elektron ünvanı sistem istifadəçiləri üçün artıq bazaya daxil edilib. Elektron ünvan təkrarlana bilməz.");                
                MailText.Focus();
                MailText.BackColor = Color.White;
                return false;
            }
            else
                b = true;

            if (MailOwner == MailOwnerEnum.Customer && (GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'C' AND M = '" + MailText.Text.Trim().ToUpper() + "'") > 0 || GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'C' AND M = '" + MailText.Text.Trim().ToLower() + "'") > 0))
            {
                MailText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage(MailText.Text + " elektron ünvanı müştərilər üçün artıq bazaya daxil edilib. Elektron ünvan təkrarlana bilməz.");                
                MailText.Focus();
                MailText.BackColor = Color.White;
                return false;
            }
            else
                b = true;

            if (MailOwner == MailOwnerEnum.CC && (GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'CC' AND M = '" + MailText.Text.Trim().ToUpper() + "'") > 0 || GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'CC' AND M = '" + MailText.Text.Trim().ToLower() + "'") > 0))
            {
                MailText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage(MailText.Text + " elektron ünvanı öhdəlik götürənlər üçün artıq bazaya daxil edilib. Elektron ünvan təkrarlana bilməz.");               
                MailText.Focus();
                MailText.BackColor = Color.White;
                return false;
            }
            else
                b = true;

            if (MailOwner == MailOwnerEnum.F && (GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT UPPER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'F' AND M = '" + MailText.Text.Trim().ToUpper() + "'") > 0 || GlobalFunctions.GetCount("SELECT COUNT(*) FROM (SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER.MAILS UNION SELECT LOWER(MAIL) M,OWNER_TYPE FROM ELMS_USER_TEMP.MAILS_TEMP) WHERE OWNER_TYPE = 'F' AND M = '" + MailText.Text.Trim().ToLower() + "'") > 0))
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
            Class.Tables.Mail mail = new Class.Tables.Mail
            {
                OWNER_TYPE = (int)MailOwner,
                OWNER_ID = (int)OwnerID,
                MAIL = MailText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                IS_SEND = 0,
                IS_CHANGE = (int)ChangeTypeEnum.Change
            };

            MailDAL.InsertMail(mail);
        }

        private void UpdateMail()
        {
            Class.Tables.Mail mail = new Class.Tables.Mail
            {
                OWNER_TYPE = (int)MailOwner,
                OWNER_ID = (int)OwnerID,
                MAIL = MailText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                IS_SEND = 0,
                IS_CHANGE = (int)ChangeTypeEnum.Change,
                ID = MailID.Value
            };

            MailDAL.UpdateMail(mail);
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