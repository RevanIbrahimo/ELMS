using DevExpress.XtraEditors;
using ELMS.Class;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace ELMS.Forms.General
{
    public partial class FLoginSystem : Form
    {
        public FLoginSystem()
        {
            InitializeComponent();            
        }

        bool FormStatus = false;

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlLoginParametr())
            {
                if (SaveCheck.Checked)
                {
                    GlobalProcedures.SetSetting("SavedLoginName", UserNameText.Text.Trim());
                    GlobalProcedures.SetSetting("SavedLoginPassword", GlobalFunctions.Encrypt(PasswordText.Text.Trim()));
                    GlobalProcedures.SetSetting("SaveLogin", "1");
                }
                else
                {
                    GlobalProcedures.SetSetting("SavedLoginName", null);
                    GlobalProcedures.SetSetting("SavedLoginPassword", null);
                    GlobalProcedures.SetSetting("SaveLogin", "0");
                }
                //GenerateUserPermisions();
                //UpdateUserConnected();
                this.Hide();
                MainForm fm = new MainForm();
                fm.ShowDialog();
            }
        }

        private void GenerateUserPermisions() // istifadecinin huquqlarinin teyin edilmesi
        {
            //string s = $@"SELECT RD.ROLE_ID, RD.NAME
            //                  FROM MCMS.ALL_USER_ROLE_DETAILS URD, MCMS.ALL_ROLE_DETAILS RD
            //                 WHERE URD.ROLE_DETAIL_ID = RD.ID AND URD.USER_ID = {GlobalVariables.V_UserID}";


        }

        private bool ControlLoginParametr()
        {
            bool b = false;

            if (UserNameText.Text.Length == 0)
            {
                UserNameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("İstifadəçi adı daxil edilməyib.");
                UserNameText.Focus();
                UserNameText.BackColor = Color.White;
                return false;
            }
            else
                b = true;

            var user = GlobalVariables.lstUsers.Find(u => u.LOGIN_NAME == UserNameText.Text.Trim() && u.IS_ACTIVE == 1);
            if (user == null)
            {
                GlobalProcedures.ShowErrorMessage("İstifadəçi adı ya düz deyil ya da bu istifadəçinin sistemə girişi bağlanılıb.");
                return false;
            }

            if (user.SESSION_ID != 0)
            {
                UserNameText.BackColor = Color.Red;
                XtraMessageBox.Show(UserNameText.Text + " adlı istifadəçi artıq sistemə daxil olub. Zəhmət olmasa başqa istifadəçi adı ilə sistemə daxil olun", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UserNameText.Focus();
                UserNameText.BackColor = Color.White;
                return false;
            }

            if (PasswordText.Text.Length == 0)
            {
                PasswordText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Şifrə daxil edilməyib.");
                PasswordText.Focus();
                PasswordText.BackColor = Color.White;
                return false;
            }
            else if (user.PASSWORD == PasswordText.Text.Trim())
                b = true;
            else
            {
                PasswordText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Şifrə düz deyil.");
                PasswordText.Focus();
                PasswordText.BackColor = Color.White;
                return false;
            }

            GlobalVariables.V_UserName = UserNameText.Text.Trim();
            GlobalVariables.V_UserID = user.ID;
            GlobalVariables.V_UserGroupID = user.GROUP_ID;
            GlobalVariables.V_BranchID = user.BRANCH_ID;
            GlobalVariables.V_DoctorID = user.DOCTOR_ID;

            var branch = GlobalVariables.lstBranch.Find(item => item.ID == GlobalVariables.V_BranchID);
            if (branch != null)
                GlobalVariables.V_BranchName = branch.NAME;

            return b;
        }

        private void FLoginSystem_Load(object sender, EventArgs e)
        {
            CopyrightLabel.Text = "Copyright © " + DateTime.Now.Year;
            VersionLabel.Text = GlobalVariables.V_Version;
            int saved = Convert.ToInt32(GlobalFunctions.ReadSetting("SaveLogin"));
            GlobalVariables.V_DefaultMenu = int.Parse(GlobalFunctions.ReadSetting("DefaultMenu"));
            SaveCheck.Visible = !(saved == 1);

            if (saved == 1)
            {
                GlobalVariables.V_Connect_User = GlobalFunctions.ReadSetting("SavedLoginName");
                UserNameText.Text = GlobalVariables.V_Connect_User;
                PasswordText.Text = GlobalFunctions.Decrypt(GlobalFunctions.ReadSetting("SavedLoginPassword"));
                SaveCheck.Checked = true;
            }
            FormStatus = true;

            GlobalVariables.V_BlockColor1 = -1048576;
            GlobalVariables.V_BlockColor2 = 0;
            GlobalVariables.V_ConnectColor1 = -16711936;
            GlobalVariables.V_ConnectColor2 = -3883625;
            GlobalVariables.V_CloseColor1 = -5658199;
            GlobalVariables.V_CloseColor2 = -5658199;
        }

        private void UpdateUserConnected()
        {
            //GlobalProcedures.ExecuteQuery($@"UPDATE MCMS.MCMS_USERS SET SESSION_ID = 1 WHERE ID = {GlobalVariables.V_UserID}",
            //                 "İstifadəçinin sistemə qoşulması istifadəçilər cədvəlində qeyd olunmadı.",
            //                    this.Name + "/UpdateUserConnected");
        }

        private void UserNameText_TextChanged(object sender, EventArgs e)
        {
            SaveCheck.Visible = FormStatus;
            SaveCheck.Checked = false;
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {
            //FForgetPassword ff = new FForgetPassword();
            //ff.ShowDialog();
        }
    }
}
