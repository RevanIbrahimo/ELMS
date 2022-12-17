using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ELMS.Class;
using ELMS.Class.Tables;
using ELMS.Class.DataAccess;
using static ELMS.Class.Enum;

namespace ELMS.Forms.Option
{
    public partial class FUserAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FUserAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public int? UserID;

        bool Used = false, isClickBOK = false;

        int UsedUserID = -1, mailID;

        public delegate void DoEvent();
        public event DoEvent RefreshUserDataGridView;

        bool CurrentStatus = false,
            UserClosed = false,
            UserConnected = false,
            UserUsed = false;
        

        private void FUserAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.SYSTEM_USER", -1, "WHERE ID = " + UserID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshUserDataGridView();
        }

       

        private void ShowPasswordCheck_CheckedChanged(object sender, EventArgs e)
        {
            PasswordText.Properties.UseSystemPasswordChar = !ShowPasswordCheck.Checked;
        }

        private void FUserAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {

                this.Text = "Istifadəçilərin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.SYSTEM_USER", GlobalVariables.V_UserID, "WHERE ID = " + UserID + " AND USED_USER_ID = -1");
                LoadUserDetails();
        Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
        GlobalProcedures.ShowWarningMessage("Seçilmiş istifadəçi hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
                        CurrentStatus = true;
                    }
                    else
                        CurrentStatus = false;
                }
                else
                    CurrentStatus = false;
                ComponentEnable(CurrentStatus);
            }
            else
                this.Text = "İstifadəçilərin əlavə edilməsi";
        }

        

        private void ComponentEnable(bool status)
        {
            PasswordText.Enabled =
            NiknameText.Enabled =
            FullNameText.Enabled =
            NoteText.Enabled =
            ShowPasswordCheck.Enabled =
            PermissionBarButton.Enabled =
            MailText.Enabled =
            ShowPasswordCheck.Visible =
            ActiveCheck.Enabled =
            BOK.Visible = !status;
        }

        

        private void LoadUserDetails()
        {
            List<Users> lstUsers = UserDAL.SelectUserByID(UserID).ToList<Users>();
            if (lstUsers.Count > 0)
            {
                var users = lstUsers.LastOrDefault();
                FullNameText.Text = users.FULL_NAME;
                NiknameText.Text = users.LOGIN_NAME;
                PasswordText.EditValue = users.PASSWORD;
                int isActive = Convert.ToInt32(users.IS_ACTIVE);
                ActiveCheck.Checked = (isActive == 1);
                BOK.Enabled = !(isActive == 0);
                NoteText.Text = users.NOTE;
                MailText.Text =users.EMAIL;
                NoteText.EditValue = users.NOTE;
                UsedUserID = users.USED_USER_ID;
            }

        }
        

        private bool ControlUserDetails()
        {
            bool b = false;

            if (FullNameText.Text.Length == 0)
            {
                FullNameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("İstifadəçinin soyadı daxil edilməyib.");
                FullNameText.Focus();
                FullNameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

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

            if (String.IsNullOrEmpty(NiknameText.Text))
            {

                NiknameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sistemə qoşulmaq üçün istifadəçi adı daxil edilməyib.");
                NiknameText.Focus();
                NiknameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(NiknameText.Text, "^[a-zA-Z0-9!]+$"))
            {
                NiknameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("İstifadəçi adı yalnız ingilis hərflərindən və ya rəqəmdən ibarət olmalıdır");
                NiknameText.Focus();
                NiknameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else if (TransactionType == TransactionTypeEnum.Insert && GlobalFunctions.GetCount($@"SELECT ID FROM MCMS.MCMS_USERS WHERE NIKNAME = '{NiknameText.Text.Trim()}'") > 0)
            {
                NiknameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage(NiknameText.Text + " istifadəçi adı artıq bazaya daxil edilib. İstifadəçi adını təkrar olaraq daxil etmək olmaz.");
                NiknameText.Focus();
                NiknameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (String.IsNullOrEmpty(PasswordText.Text))
            {
                PasswordText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sistemə qoşulmaq üçün şifrə daxil edilməyib.");
                PasswordText.Focus();
                PasswordText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else if (PasswordText.Text.Length < 6)
            {
                PasswordText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Şifrənin uzunluğu 6 simvoldan az ola bilməz.");
                PasswordText.Focus();
                PasswordText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (!System.Text.RegularExpressions.Regex.IsMatch(PasswordText.Text, "^[a-zA-Z0-9]+$"))
            {
                PasswordText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Şifrə yalnız ingilis hərflərindən və ya rəqəmdən ibarət olmalıdır");
                PasswordText.Focus();
                PasswordText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;
        }

        private void PermissionGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(PermissionGridView, PopupMenu, e);
        }

        private void InsertUser()
        {
           }

        private void PhoneStandaloneBarDockControl_Click(object sender, EventArgs e)
        {

        }

        private void UpdateUser()
        {
        }

        private void InsertDetail()
        {
            Users lstUsers = new Users
            {
                FULL_NAME = GlobalFunctions.FirstCharToUpper(FullNameText.Text.Trim()),
                LOGIN_NAME = NiknameText.Text.Trim(),
                PASSWORD = PasswordText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                EMAIL = MailText.Text.Trim()
            };
            UserDAL.InsertUsers(lstUsers);

        }

        private void UpdateDetail()
        {
            int isActive = (ActiveCheck.Checked) ? 1 : 0;
            isClickBOK = true;

            Users lstUsers = new Users
            {
               FULL_NAME= FullNameText.Text.Trim(), 
               LOGIN_NAME = NiknameText.Text.Trim(),
               PASSWORD = PasswordText.Text.Trim(),
               IS_ACTIVE = isActive,
               NOTE = NoteText.Text.Trim(),
               EMAIL = MailText.Text.Trim(),
               ID = UserID.Value,
               USED_USER_ID = -1
            };

            UserDAL.UpdateUsers(lstUsers);
        }


        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlUserDetails())
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