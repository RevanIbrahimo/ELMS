using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Collections;
using DevExpress.Utils;
using Oracle.ManagedDataAccess.Client;
using ELMS.Class;
using ELMS.Class.Tables;
using ELMS.Class.DataAccess;
using System.Text.RegularExpressions;
using System.Threading;
using static ELMS.Class.Enum;
using ELMS.Forms.Dictionaries;

namespace ELMS.Forms
{
    public partial class FUserAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FUserAddEdit()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public int? UserID,GroupID;

        string PhoneNumber,
            UserImage,
            UserImagePath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Images",
            group_name;

        int PhoneID,
            MailID,
            status_id = 1,
            sex_id,
            UsedUserID = -1,
            crop_image_count = 0,
            mail_selected_count = 0,
            branch_id = 0;

        bool CurrentStatus = false,
            UserClosed = false,
            UserConnected = false,
            UserUsed = false,
            maildetails = false,
            permissiondetails = false,
            existsImage = false;

        public delegate void DoEvent();
        public event DoEvent RefreshUserDataGridView;

        private void FUserAddEdit_Load(object sender, EventArgs e)
        {
            //permission
            if (GlobalVariables.V_UserID > 0)
            {
                GroupNameLookUp.Properties.Buttons[1].Visible = GlobalVariables.UsersGroup;
            }

            GlobalProcedures.FillLookUpEdit(SexLookUp, "SEX", "ID", "NAME", "1 = 1 ORDER BY ID");
            GlobalProcedures.FillLookUpEdit(BranchLookUp, "BRANCH", "ID", "NAME", "1 = 1 ORDER BY NAME");
            GlobalProcedures.FillLookUpEdit(GroupNameLookUp, "USER_GROUP", "ID", "GROUP_NAME", null);
            if (TransactionType == TransactionTypeEnum.Update)
            {
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.SYSTEM_USER", GlobalVariables.V_UserID, "WHERE ID = " + UserID + " AND USED_USER_ID = -1");

                List<Users> lstComsUser = UserDAL.SelectUserByID(UserID).ToList<Users>();
                var user = lstComsUser.First();

                if (user.IS_ACTIVE == 2)
                    UserClosed = true;
                else
                {
                    UserClosed = false;
                    if (user.SESSION_ID == 0)
                        UserConnected = false;
                    else
                        UserConnected = true;
                }

                UsedUserID = user.USED_USER_ID;
                UserUsed = (user.USED_USER_ID > 0);

                if ((UserClosed && UserUsed) || (UserClosed && !UserUsed))
                {
                    GlobalProcedures.ShowWarningMessage("İstifadəçi sistemdə bağlanılıb. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
                    CurrentStatus = true;
                }
                else if ((!UserClosed) && (UserUsed))
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("İstifadəçinin məlumatları hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
                        CurrentStatus = true;
                    }
                    else if (UserConnected)
                    {
                        GlobalProcedures.ShowWarningMessage("İstifadəçi sistemə daxil olub. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
                        CurrentStatus = true;
                    }
                    else
                        CurrentStatus = false;
                }
                else
                    CurrentStatus = false;

                ComponentEnable(CurrentStatus);
                InsertTemps();
                LoadUserDetails();
            }
            else
            {
                maildetails = permissiondetails = true;
                UserID = GlobalFunctions.GetOracleSequenceValue("SYSTEM_USER_SEQUENCE");
                RegistrationIDText.EditValue = UserID;
            }
        }

        private void ComponentEnable(bool status)
        {
            BLoadPicture.Enabled =
                PhoneStandaloneBarDockControl.Enabled =
                MailStandaloneBarDockControl.Enabled =
                GroupNameLookUp.Enabled =
                BranchLookUp.Enabled =
                UserNameText.Enabled =
                PasswordValue.Enabled =
                FullNameText.Enabled =
                BirthdayDate.Enabled =
                AddressText.Enabled =
                SexLookUp.Enabled =
                NoteText.Enabled =
                BOK.Visible = !status;
        }

        private void LoadUserDetails()
        {
            string s = $@"SELECT U.ID,
                               U.FULL_NAME,                               
                               B.NAME BRANCH_NAME,
                               U.LOGIN_NAME,
                               U.PASSWORD,                            
                               U.BIRTHDAY,
                               SE.NAME SEX_NAME,                               
                               U.ADDRESS,
                               G.GROUP_NAME,                               
                               U.CLOSED_DATE,
                               U.NOTE,
                               U.INSERT_DATE,
                               UI.IMAGE,
                               U.GROUP_ID
                          FROM ELMS_USER.SYSTEM_USER U,
                               ELMS_USER.SEX SE,
                               ELMS_USER.USER_GROUP G,
                               ELMS_USER.USER_IMAGE UI,
                               ELMS_USER.BRANCH B
                         WHERE     U.GROUP_ID = G.ID
                               AND U.SEX_ID = SE.ID
                               AND U.ID = UI.USER_ID
                               AND U.BRANCH_ID = B.ID
                               AND U.ID = {UserID}";
            try
            {
                DataTable dt = GlobalFunctions.GenerateDataTable(s);

                foreach (DataRow dr in dt.Rows)
                {
                    RegistrationIDText.Text = dr["ID"].ToString();
                    FullNameText.Text = dr["FULL_NAME"].ToString();
                    UserNameText.Text = GlobalFunctions.Decrypt(dr["LOGIN_NAME"].ToString());
                    PasswordValue.Text = GlobalFunctions.Decrypt(dr["PASSWORD"].ToString());
                    BOK.Enabled = !(status_id == 2);
                    NoteText.Enabled = !(status_id == 2);
                    BLoadPicture.Enabled = !(status_id == 2);
                    BDeletePicture.Enabled = !(status_id == 2);
                    GlobalProcedures.LookUpEditValue(BranchLookUp, dr["BRANCH_NAME"].ToString());
                    BirthdayDate.EditValue = DateTime.Parse(dr["BIRTHDAY"].ToString());
                    SexLookUp.EditValue = SexLookUp.Properties.GetKeyValueByDisplayText(dr["SEX_NAME"].ToString());
                    AddressText.Text = dr["ADDRESS"].ToString();
                    group_name = dr["GROUP_NAME"].ToString();
                    GroupID = Convert.ToInt32(dr["GROUP_ID"].ToString());
                    NoteText.Text = dr["NOTE"].ToString();
                    if (!DBNull.Value.Equals(dr["IMAGE"]))
                    {
                        existsImage = true;
                        Byte[] BLOBData = (byte[])dr["IMAGE"];
                        MemoryStream stmBLOBData = new MemoryStream(BLOBData);
                        UserPictureBox.Image = Image.FromStream(stmBLOBData);

                        UserImagePath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Images";

                        FileStream fs = new FileStream(UserImagePath + "\\U_" + RegistrationIDText.Text + ".jpeg", FileMode.Create, FileAccess.Write);
                        stmBLOBData.WriteTo(fs);
                        fs.Close();
                        stmBLOBData.Close();
                        UserImage = UserImagePath + "\\U_" + RegistrationIDText.Text + ".jpeg";
                        BLoadPicture.Text = "Dəyiş";
                        if (status_id == 1)
                        {
                            BDeletePicture.Enabled = !CurrentStatus;
                            BLoadPicture.Enabled = !CurrentStatus;
                        }
                    }
                    else
                    {
                        BLoadPicture.Text = "Yüklə";
                        if (status_id == 1)
                            BLoadPicture.Enabled = !CurrentStatus;
                        BDeletePicture.Enabled = false;
                        UserImage = null;
                    }
                }
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("İstifadəçinin parametrləri açılmadı.", s, GlobalVariables.V_UserName, this.Name, this.GetType().FullName + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        private void BLoadPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "İstifadəçinin şəklini seçin";
                dlg.Filter = "All files (*.*)|*.*|Image files (*.jpeg)|*.jpeg|Bmp files (*.bmp)|*.bmp|Png files (*.png)|*.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    UserPictureBox.Image = new Bitmap(dlg.FileName);
                    UserImage = dlg.FileName;
                    BDeletePicture.Enabled = !CurrentStatus;
                }
                dlg.Dispose();
            }
        }

        private void BDeletePicture_Click(object sender, EventArgs e)
        {
            UserPictureBox.Image = null;
            UserImage = null;
            BLoadPicture.Text = "Yüklə";
            BDeletePicture.Enabled = false;
        }

        private bool ControlUserDetails()
        {
            bool b = false;

            if (FullNameText.Text.Length == 0)
            {
                FullNameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("İstifadəçinin tam adı daxil edilməyib.");
                FullNameText.Focus();
                FullNameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (SexLookUp.EditValue == null)
            {
                SexLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("İstifadəçinin cinsi daxil edilməyib.");
                SexLookUp.Focus();
                SexLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (String.IsNullOrEmpty(BirthdayDate.Text))
            {
                BirthdayDate.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("İstifadəçinin doğum günü daxil edilməyib.");
                BirthdayDate.Focus();
                BirthdayDate.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (BirthdayDate.DateTime == DateTime.Today)
            {
                BirthdayDate.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("İstifadəçinin doğum günü cari tarix ola bilməz.");
                BirthdayDate.Focus();
                BirthdayDate.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (branch_id == 0)
            {
                UserTabControl.SelectedTabPageIndex = 0;
                BranchLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Filial seçilməyib.");
                BranchLookUp.Focus();
                BranchLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }

            if (String.IsNullOrEmpty(UserNameText.Text))
            {
                UserTabControl.SelectedTabPageIndex = 0;
                UserNameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sistemə qoşulmaq üçün istifadəçi adı daxil edilməyib.");
                UserNameText.Focus();
                UserNameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(UserNameText.Text, "^[a-zA-Z0-9]+$"))
            {
                UserTabControl.SelectedTabPageIndex = 0;
                UserNameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("İstifadəçi adı yalnız ingilis hərflərindən və ya rəqəmdən ibarət olmalıdır");
                UserNameText.Focus();
                UserNameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else if (TransactionType == TransactionTypeEnum.Insert && GlobalFunctions.GetCount($@"SELECT COUNT(*) FROM ELMS_USER.SYSTEM_USER WHERE LOGIN_NAME = '{GlobalFunctions.Encrypt(UserNameText.Text)}'") > 0)
            {
                UserTabControl.SelectedTabPageIndex = 0;
                UserNameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage(UserNameText.Text + " istifadəçi adı artıq bazaya daxil edilib. İstifadəçi adını təkrar olaraq daxil edilə bilməz.");
                UserNameText.Focus();
                UserNameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;


            if (String.IsNullOrEmpty(PasswordValue.Text.Trim()))
            {
                UserTabControl.SelectedTabPageIndex = 0;
                PasswordValue.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sistemə qoşulmaq üçün şifrə daxil edilməyib. Zəhmət olmasa şifrəni aşağıdakı qaydalara əsasən yaradın.\r\n\r\n<b>● Şifrənin uzunluğu ən azı 8 simvol və ən çoxu 15 simvol olmalıdır</b>\r\n<b>● Ən azı 1 dənə böyük hərf (A-Z)</b>\r\n<b>● Ən azı 1 xüsusi simvol (!@#$%^&*_-)</b>\r\n<b>● Ən azı 1 rəqəm (0-9)</b>\r\n<b>● Ən azı 1 balaca hərf (a-z)</b>");
                PasswordValue.Focus();
                PasswordValue.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
            {
                UserTabControl.SelectedTabPageIndex = 0;
                string input = PasswordValue.Text.Trim();
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMiniMaxChars = new Regex(@".{8,15}");
                var hasLowerChar = new Regex(@"[a-z]+");
                var hasSymbols = new Regex(@"[!@#$%^&*_-]");

                if (!hasLowerChar.IsMatch(input))
                {
                    PasswordValue.BackColor = Color.Red;
                    GlobalProcedures.ShowErrorMessage("Şifrədə ən azı 1 dənə kiçik ingilis hərfi olmalıdır.");
                    PasswordValue.Focus();
                    PasswordValue.BackColor = GlobalFunctions.ElementColor();
                    return false;
                }
                else if (!hasUpperChar.IsMatch(input))
                {
                    PasswordValue.BackColor = Color.Red;
                    GlobalProcedures.ShowErrorMessage("Şifrədə ən azı 1 dənə böyük ingilis hərfi olmalıdır.");
                    PasswordValue.Focus();
                    PasswordValue.BackColor = GlobalFunctions.ElementColor();
                    return false;
                }
                else if (!hasMiniMaxChars.IsMatch(input))
                {
                    PasswordValue.BackColor = Color.Red;
                    GlobalProcedures.ShowErrorMessage("Şifrənin uzunluğu ən azı 8 simvol, ən çoxu isə 15 simvol olmalıdır.");
                    PasswordValue.Focus();
                    PasswordValue.BackColor = GlobalFunctions.ElementColor();
                    return false;
                }
                else if (!hasNumber.IsMatch(input))
                {
                    PasswordValue.BackColor = Color.Red;
                    GlobalProcedures.ShowErrorMessage("Şifrədə ən azı 1 dənə rəqəm olmalıdır.");
                    PasswordValue.Focus();
                    PasswordValue.BackColor = GlobalFunctions.ElementColor();
                    return false;
                }
                else if (!hasSymbols.IsMatch(input))
                {
                    PasswordValue.BackColor = Color.Red;
                    GlobalProcedures.ShowErrorMessage("Şifrədə ən azı 1 dənə xüsusi simvol (!@#$%^&*_-) olmalıdır.");
                    PasswordValue.Focus();
                    PasswordValue.BackColor = GlobalFunctions.ElementColor();
                    return false;
                }
                else
                    b = true;
            }

            if (permissiondetails && PermissionGridView.RowCount == 0)
            {
                UserTabControl.SelectedTabPageIndex = 1;
                GroupNameLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("İstifadəçiyə heç bir hüquq verilməyib.");
                GroupNameLookUp.Focus();
                GroupNameLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (!maildetails)
                LoadMailDataGridView();

            if (MailGridView.RowCount == 0)
            {
                UserTabControl.SelectedTabPageIndex = 2;
                GlobalProcedures.ShowErrorMessage("İstifadəçi üçün ən azı bir elektron ünvan daxil edilməli və mail göndərmək üçün ən azı bir elektron ünvan seçilməlidir.");
                return false;
            }
            else
                b = true;

            if (mail_selected_count == 0)
            {
                UserTabControl.SelectedTabPageIndex = 2;
                GlobalProcedures.ShowErrorMessage("Mail göndərmək üçün ən azı bir elektron ünvan seçilməlidir.");
                return false;
            }
            else
                b = true;

            return b;
        }

        private void InsertUser(OracleTransaction tran)
        {
            if(GroupID == 0)
            {
                GroupID = 2;
            }
            status_id = 1;

            string sqlUser = $@"INSERT INTO ELMS_USER.SYSTEM_USER(ID,
                                                                FULL_NAME,
                                                                BRANCH_ID,
                                                                LOGIN_NAME,
                                                                PASSWORD,
                                                                IS_ACTIVE,
                                                                BIRTHDAY,
                                                                SEX_ID,
                                                                ADDRESS,
                                                                GROUP_ID,
                                                                NOTE,
                                                                INSERT_USER) 
                            VALUES({UserID},                                    
                                    '{FullNameText.Text.Trim()}',
                                    {branch_id},
                                    '{GlobalFunctions.Encrypt(UserNameText.Text.Trim())}',
                                    '{GlobalFunctions.Encrypt(PasswordValue.Text.Trim())}',
                                    {status_id},
                                    TO_DATE('{BirthdayDate.Text}','DD/MM/YYYY'),
                                    {sex_id},
                                    '{AddressText.Text.Trim()}',
                                    {GroupID},
                                    '{NoteText.Text.Trim()}',
                                    {GlobalVariables.V_UserID})",
                       sqlImage = null;

            if (UserImage != null)
                sqlImage = $@"INSERT INTO ELMS_USER.USER_IMAGE(USER_ID,IMAGE)VALUES({UserID},:BlobFile)";

            if (UserImage == null)
                GlobalProcedures.ExecuteQuery(tran, sqlUser);
            else
                GlobalProcedures.ExecuteTwoQueryWithBlob(tran, sqlUser, sqlImage, UserImage);
        }

        private void UppdateUser(OracleTransaction tran)
        {
            string sqlUser = null;

            if (status_id == 1)
            {
                sqlUser = $@"UPDATE ELMS_USER.SYSTEM_USER SET                                                
                                                FULL_NAME = '{FullNameText.Text.Trim()}',
                                                BRANCH_ID = {branch_id},
                                                LOGIN_NAME = '{GlobalFunctions.Encrypt(UserNameText.Text.Trim())}',
                                                PASSWORD = '{GlobalFunctions.Encrypt(PasswordValue.Text.Trim())}',
                                                BIRTHDAY = TO_DATE('{BirthdayDate.Text.Trim()}','DD/MM/YYYY'),
                                                SEX_ID = {sex_id},
                                                NOTE = '{NoteText.Text}',
                                                ADDRESS = '{AddressText.Text}',
                                                GROUP_ID = {GroupID},
                                                UPDATE_USER = {GlobalVariables.V_UserID},
                                                UPDATE_DATE = SYSDATE
                                        WHERE ID = {UserID}";
            }
            else
            {
                sqlUser = $@"UPDATE ELMS_USER.SYSTEM_USER SET 
                                                IS_ACTIVE = {status_id},
                                                NOTE = '{NoteText.Text}' 
                            WHERE ID = {UserID}";
            }

            if (existsImage)
            {
                if (String.IsNullOrWhiteSpace(UserImage))
                    GlobalProcedures.ExecuteTwoQuery(tran, sqlUser, $@"DELETE ELMS_USER.USER_IMAGE WHERE USER_ID = {UserID}");
                else
                    GlobalProcedures.ExecuteTwoQueryWithBlob(tran, sqlUser, $@"UPDATE ELMS_USER.USER_IMAGE SET IMAGE = :BlobFile WHERE USER_ID = {UserID}", UserImage);
            }
            else
            {
                if (UserImage != null)
                    GlobalProcedures.ExecuteTwoQueryWithBlob(tran, sqlUser, $@"INSERT INTO ELMS_USER.USER_IMAGE(USER_ID,IMAGE)VALUES({UserID},:BlobFile)", UserImage);
                else
                    GlobalProcedures.ExecuteQuery(tran, sqlUser);
            }
        }

        private void LoadUserGroupPermissionDataGridView()
        {
            PermissionGridControl.DataSource = PermissionDAL.SelectPermissionByID(GroupID).ToList<Permission>();

            //string s = $@"SELECT R.DESCRIPTION ROLES_DESCRIPTION, RD.DETAIL_NAME ROLE_DETAIL_NAME, RD.ID
            //                      FROM ELMS_USER.ALL_USER_GROUP_ROLE_DETAILS RDT,
            //                           ELMS_USER.ROLES R,
            //                           ELMS_USER.ALL_ROLE_DETAILS RD
            //                     WHERE     RD.ID = RDT.ROLE_DETAIL_ID
            //                           AND R.ID = RD.ROLE_ID
            //                           AND RDT.GROUP_ID = {GroupID}";
            //PermissionGridControl.DataSource = GlobalFunctions.GenerateDataTable(s);
        }

        private void DeleteAllTemp()
        {
            GlobalProcedures.ExecuteProcedureWithTwoParametr("ELMS_USER_TEMP.PROC_USER_DELETE_ALL_TEMP", "P_USED_USER_ID", GlobalVariables.V_UserID, "P_OWNER_TYPE", MailOwnerEnum.User, "İstifadəçinin məlumatları temp cədvəldən silinmədi.");
        }

        private void FUserAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.SYSTEM_USER", -1, "WHERE ID = " + UserID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            GlobalProcedures.DeleteAllFilesInDirectory(UserImagePath);
            DeleteAllTemp();
            this.RefreshUserDataGridView();
        }

        private void LoadPhoneDataGridView()
        {
            if (!UserID.HasValue)
                UserID = 0;

            PhoneGridControl.DataSource = PhoneDAL.SelectPhoneByOwnerID(UserID.Value, PhoneOwnerEnum.User);

            EditPhoneBarButton.Enabled = DeletePhoneBarButton.Enabled = PhoneGridView.RowCount > 0;
            //string s = $@"SELECT P.ID,
            //                     PD.NAME DESCRIPTION_NAME,
            //                     P.PHONE_NUMBER PHONE_NUMBER,
            //                     P.IS_SEND_SMS
            //                FROM ELMS_USER_TEMP.PHONE_TEMP P,
            //                     ELMS_USER.PHONE_DESCRIPTIONS PD
            //               WHERE     P.IS_CHANGE IN (0, 1)
            //                     AND P.PHONE_DESCRIPTION_ID = PD.ID
            //                     AND P.OWNER_TYPE = {PhoneOwnerEnum.User}
            //                     AND P.OWNER_ID = {UserID}
            //            ORDER BY P.ORDER_ID";
            //try
            //{
            //    PhoneGridControl.DataSource = GlobalFunctions.GenerateDataTable(s);
            //    if (PhoneGridView.RowCount > 0)
            //        EditPhoneBarButton.Enabled =
            //            DeletePhoneBarButton.Enabled = true;
            //    else
            //        EditPhoneBarButton.Enabled =
            //            DeletePhoneBarButton.Enabled =
            //            UpPhoneBarButton.Enabled =
            //            DownPhoneBarButton.Enabled = false;
            //    try
            //    {
            //        PhoneGridView.BeginUpdate();
            //        for (int i = 0; i < PhoneGridView.RowCount; i++)
            //        {
            //            DataRow row = PhoneGridView.GetDataRow(PhoneGridView.GetVisibleRowHandle(i));
            //            if (Convert.ToInt32(row["IS_SEND_SMS"].ToString()) == 1)
            //                PhoneGridView.SelectRow(i);
            //        }
            //    }
            //    finally
            //    {
            //        PhoneGridView.EndUpdate();
            //    }
            //}
            //catch (Exception exx)
            //{
            //    GlobalProcedures.LogWrite("Telefon nömrələri cədvələ yüklənmədi.", s, GlobalVariables.V_UserName, this.Name, this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, exx);
            //}


        }

        private void LoadMailDataGridView()
        {
            if (!UserID.HasValue)
                UserID = 0;

            MailGridControl.DataSource = MailDAL.SelectMailByOwnerID(UserID, MailOwnerEnum.User).ToList<Mail>();

            EditMailBarButton.Enabled = DeleteMailBarButton.Enabled = MailGridView.RowCount > 0;

            //string s = $@"SELECT ID,
            //                       MAIL,
            //                       NOTE,
            //                       IS_SEND
            //                  FROM ELMS_USER_TEMP.MAILS_TEMP
            //                 WHERE IS_CHANGE IN (0, 1) AND OWNER_TYPE = {PhoneOwnerEnum.User} AND OWNER_ID = {UserID}";
            //try
            //{
            //    MailGridControl.DataSource = GlobalFunctions.GenerateDataTable(s);
            //    EditMailBarButton.Enabled = DeleteMailBarButton.Enabled = (MailGridView.RowCount > 0);
            //    try
            //    {
            //        MailGridView.BeginUpdate();
            //        for (int i = 0; i < MailGridView.RowCount; i++)
            //        {
            //            DataRow row = MailGridView.GetDataRow(MailGridView.GetVisibleRowHandle(i));
            //            if (Convert.ToInt32(row["IS_SEND"].ToString()) == 1)
            //                MailGridView.SelectRow(i);
            //        }
            //    }
            //    finally
            //    {
            //        MailGridView.EndUpdate();
            //    }
            //}
            //catch (Exception exx)
            //{
            //    GlobalProcedures.LogWrite("Maillər cədvələ yüklənmədi.", s, GlobalVariables.V_UserName, this.Name, this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, exx);
            //}
        }

        private void UpdatePhoneSendSms(OracleTransaction tran)
        {
            ArrayList rows = new ArrayList();
            rows.Clear();
            for (int i = 0; i < PhoneGridView.SelectedRowsCount; i++)
            {
                rows.Add(PhoneGridView.GetDataRow(PhoneGridView.GetSelectedRows()[i]));
            }

            string listID = null;
            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i] as DataRow;
                listID += row["ID"] + ",";
            }

            if (listID != null)
            {
                listID = listID.TrimEnd(',');

                GlobalProcedures.ExecuteTwoQuery(tran, $@"UPDATE ELMS_USER_TEMP.PHONE_TEMP SET IS_SEND_SMS = 0, IS_CHANGE = 1 WHERE IS_CHANGE <> 2 AND IS_SEND_SMS = 1 AND OWNER_TYPE = {PhoneOwnerEnum.User} AND OWNER_ID = {UserID} AND USED_USER_ID = {GlobalVariables.V_UserID}",
                                                       $@"UPDATE ELMS_USER_TEMP.PHONE_TEMP SET IS_SEND_SMS = 1, IS_CHANGE = 1 WHERE IS_CHANGE <> 2 AND OWNER_TYPE = {PhoneOwnerEnum.User} AND ID IN ({listID}) AND USED_USER_ID = {GlobalVariables.V_UserID}");
            }
        }

        private void UpdateMailSend(OracleTransaction tran)
        {
            ArrayList rows = new ArrayList();
            rows.Clear();
            for (int i = 0; i < MailGridView.SelectedRowsCount; i++)
            {
                rows.Add(MailGridView.GetDataRow(MailGridView.GetSelectedRows()[i]));
            }

            string listID = null;
            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i] as DataRow;
                listID += row["ID"] + ",";
            }

            if (listID != null)
            {
                listID = listID.TrimEnd(',');
                GlobalProcedures.ExecuteTwoQuery(tran, $@"UPDATE ELMS_USER_TEMP.MAILS_TEMP SET IS_SEND = 0, IS_CHANGE = 1 WHERE IS_SEND = 1 AND OWNER_ID = {UserID} AND OWNER_TYPE = {MailOwnerEnum.User} AND USED_USER_ID = {GlobalVariables.V_UserID}",
                                                       $@"UPDATE ELMS_USER_TEMP.MAILS_TEMP SET IS_SEND = 1, IS_CHANGE = 1 WHERE ID IN ({listID}) AND OWNER_TYPE = {MailOwnerEnum.User} AND USED_USER_ID = {GlobalVariables.V_UserID}");
            }
        }

        private void InsertTemps()
        {
            if (TransactionType == TransactionTypeEnum.Insert)
                return;
            //GlobalProcedures.ExecuteProcedureWithTwoParametrAndUser("ELMS_USER_TEMP.PROC_INSERT_USER_TEMP", "P_USER_ID", UserID, "P_OWNER_TYPE", (int)PhoneOwnerEnum.User, "İstifadəçinin məlumatları temp cədvələ daxil edilmədi.");
            GlobalFunctions.RunInOneTransaction<int>(tran =>
            {
                GlobalProcedures.ExecuteProcedureWithTwoParametrAndUser(tran, "ELMS_USER_TEMP.PROC_INSERT_PHONE_TEMP", "P_OWNER_ID", UserID.Value, "P_OWNER_TYPE", (int)PhoneOwnerEnum.Customer);
                GlobalProcedures.ExecuteProcedureWithTwoParametrAndUser(tran, "ELMS_USER_TEMP.PROC_INSERT_USER_TEMP", "P_OWNER_ID", UserID.Value, "P_OWNER_TYPE", (int)MailOwnerEnum.Customer);
                return 1;
            }, "İstifadəçinin məlumatları temp cədvələ daxil edilmədi.");

        }

        private void BirthdayDate_EditValueChanged(object sender, EventArgs e)
        {
            AgeLabel.Text = GlobalFunctions.CalculationAgeWithYear(BirthdayDate.DateTime, DateTime.Today);
        }

        private void InsertUserGroupPermission(OracleTransaction tran)
        {
            if (permissiondetails)
                GlobalProcedures.ExecuteTwoQuery(tran, "DELETE FROM ELMS_USER.USER_GROUP_PERMISSION WHERE USER_ID = " + UserID,
                                                       $@"INSERT INTO ELMS_USER.USER_GROUP_PERMISSION(ID,USER_ID,GROUP_ID) VALUES(USER_GROUP_PERMISSION_SEQUENCE.NEXTVAL,{UserID},{GroupID})");
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlUserDetails())
            {
                GlobalProcedures.SplashScreenShow(this, typeof(WaitForms.FWait));
                GlobalFunctions.RunInOneTransaction<int>(tran =>
                {
                    if (TransactionType == TransactionTypeEnum.Insert)
                        InsertUser(tran);
                    else
                        UppdateUser(tran);

                    //InsertUserGroupPermission(tran);
                    //UpdatePhoneSendSms(tran);
                    //UpdateMailSend(tran);
                    InsertUserDetails(tran);
                    return 1;
                }, TransactionType == TransactionTypeEnum.Insert ? "İstifadəçinin məlumatları bazaya daxil edilmədi." : "İstifadəçinin məlumatları bazada dəyişdirilmədi.");

                GlobalProcedures.SplashScreenClose();
                this.Close();
            }
        }

        private void InsertUserDetails(OracleTransaction tran)
        {
            GlobalProcedures.ExecuteProcedureWithTwoParametrAndUser(tran, "ELMS_USER.PROC_INSERT_USER_DETAILS", "P_USER_ID", UserID,"P_OWNER_TYPE",1);
        }

        void RefreshPhone()
        {
            LoadPhoneDataGridView();
        }

        public void LoadFPhoneAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            Phone.FPhoneAddEdit fp = new Phone.FPhoneAddEdit();
            fp.TransactionType = transactionType;
            fp.OwnerID = UserID;
            fp.PhoneOwner = PhoneOwnerEnum.User;
            fp.PhoneID = id;
            fp.RefreshDataGridView += new Phone.FPhoneAddEdit.DoEvent(RefreshPhone);
            fp.ShowDialog();
        }

        private void NewPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFPhoneAddEdit(TransactionTypeEnum.Insert, null);
        }

        private void EditPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFPhoneAddEdit(TransactionTypeEnum.Update, PhoneID);
        }

        private void PhoneGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditPhoneBarButton.Enabled && PhoneStandaloneBarDockControl.Enabled)
                LoadFPhoneAddEdit(TransactionTypeEnum.Update, PhoneID);
        }

        private void PhoneGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow row = PhoneGridView.GetFocusedDataRow();
            if (row != null)
            {
                PhoneID = Convert.ToInt32(row["ID"].ToString());
                PhoneNumber = row["PHONE_NUMBER"].ToString();
                UpPhoneBarButton.Enabled = !(PhoneGridView.FocusedRowHandle == 0);
                DownPhoneBarButton.Enabled = !(PhoneGridView.FocusedRowHandle == PhoneGridView.RowCount - 1);
            }
        }

        private void DeletePhone()
        {
            DialogResult dialogResult = XtraMessageBox.Show(PhoneNumber + " nömrəsini silmək istəyirsiniz?", "Telefon nömrəsinin silinməsi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                GlobalProcedures.ExecuteQuery("UPDATE ELMS_USER_TEMP.PHONE_TEMP SET IS_CHANGE = 2 WHERE OWNER_TYPE = " + PhoneOwnerEnum.User + " AND OWNER_ID = " + UserID + " AND ID = " + PhoneID, "Telefon nömrəsi temp cədvəldən silinmədi.");
            }
        }

        private void DeletePhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeletePhone();
            LoadPhoneDataGridView();
        }

        private void RefreshPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPhoneDataGridView();
        }

        private void PhoneGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(PhoneGridView, PhonePopupMenu, e);
        }

        void RefreshMail()
        {
            LoadMailDataGridView();
        }

        public void LoadFMailAddEdit(TransactionTypeEnum transaction, int? id)
        {
            FMailAddEdit fp = new FMailAddEdit();
            fp.TransactionType = transaction;
            fp.OwnerID = UserID;
            fp.MailOwner = MailOwnerEnum.User;
            fp.MailID = id;
            fp.RefreshEmailDataGridView += new FMailAddEdit.DoEvent(RefreshMail);
            fp.ShowDialog();
        }

        private void UpPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int orderid;
            GlobalProcedures.ChangeOrderIDforTEMP("PHONE_TEMP", PhoneID, "up", out orderid);
            LoadPhoneDataGridView();
            PhoneGridView.FocusedRowHandle = orderid - 1;
        }

        private void DownPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int orderid;
            GlobalProcedures.ChangeOrderIDforTEMP("PHONE_TEMP", PhoneID, "down", out orderid);
            LoadPhoneDataGridView();
            PhoneGridView.FocusedRowHandle = orderid - 1;
        }



        private void BranchLookUp_EditValueChanged(object sender, EventArgs e)
        {
            branch_id = GlobalFunctions.GetLookUpID(sender);
        }

        private void PasswordValue_EditValueChanged(object sender, EventArgs e)
        {
            if (PasswordValue.Text.Trim().Length == 0)
                PasswordLengthLabel.Visible = false;
            else
            {
                PasswordLengthLabel.Visible = true;
                PasswordLengthLabel.Text = PasswordValue.Text.Trim().Length.ToString();
            }
        }

        private void PhoneGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Phone_SS, e);
        }

        private void MailGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Mail_SS, e);
        }

        private void PasswordValue_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }


        private void FullNameText_EditValueChanged(object sender, EventArgs e)
        {
            if (FullNameText.EditorContainsFocus)
                if (FullNameText.Text.IndexOf("oğlu") > -1)
                    GlobalProcedures.LookUpEditValue(SexLookUp, "Kişi");
                else if (FullNameText.Text.IndexOf("qızı") > -1)
                    GlobalProcedures.LookUpEditValue(SexLookUp, "Qadın");
                else
                    GlobalProcedures.LookUpEditValue(SexLookUp, null);
        }

        private void PasswordValue_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            PasswordValue.Properties.UseSystemPasswordChar = false;
        }

        void RefreshUserGroup()
        {
            GlobalProcedures.FillLookUpEdit(GroupNameLookUp, "USER_GROUP", "ID", "GROUP_NAME", null);
        }

        private void GroupNameLookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FUsersGroups fug = new FUsersGroups();
                fug.RefreshUserGroup += new FUsersGroups.DoEvent(RefreshUserGroup);
                fug.ShowDialog();
            }
        }

        private void GroupNameLookUp_EditValueChanged(object sender, EventArgs e)
        {
            if (GroupNameLookUp.EditValue == null)
                return;

            GroupID = Convert.ToInt32(GroupNameLookUp.EditValue);
            LoadUserGroupPermissionDataGridView();
        }

        private void SexLookUp_EditValueChanged(object sender, EventArgs e)
        {
            if (SexLookUp.EditValue == null)
                return;

            sex_id = Convert.ToInt32(SexLookUp.EditValue);
        }

        private void NewMailBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFMailAddEdit(TransactionTypeEnum.Insert, null);
        }

        private void EditMailBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFMailAddEdit(TransactionTypeEnum.Update, MailID);
        }

        private void MailGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditMailBarButton.Enabled && MailStandaloneBarDockControl.Enabled)
                LoadFMailAddEdit(TransactionTypeEnum.Update, MailID);
        }

        private void DeleteMail()
        {
            DialogResult dialogResult = XtraMessageBox.Show("Seçilmiş elektron ünvanları silmək istəyirsiniz?", "Elektron ünvanların silinməsi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                GlobalProcedures.ExecuteQuery("UPDATE ELMS_USER_TEMP.MAILS_TEMP SET IS_CHANGE = 2 WHERE OWNER_TYPE = " + PhoneOwnerEnum.User + " AND OWNER_ID = " + UserID + " AND ID = " + MailID, "Elektron ünvanlar temp cədvəldən silinmədi.");
            }
        }

        private void DeleteMailBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteMail();
            LoadMailDataGridView();
        }

        private void RefreshMailBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadMailDataGridView();
        }

        private void MailGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(MailGridView, MailPopupMenu, e);
        }

        private void MailGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow row = MailGridView.GetFocusedDataRow();
            if (row != null)
                MailID = Convert.ToInt32(row["ID"].ToString());

        }





        void SelectionImage(string a, int count)
        {
            if (!String.IsNullOrEmpty(a) && File.Exists(a))
            {
                UserPictureBox.Image = Image.FromFile(a);
                UserImage = a;
                BDeletePicture.Enabled = true;
            }
            crop_image_count = count;
        }

        private void UserPictureBox_DoubleClick(object sender, EventArgs e)
        {
            UserPictureBox.Image = null;
            FImageCrop crop = new FImageCrop();
            crop.PictureOwner = "C" + UserID.ToString();
            crop.count = crop_image_count;
            crop.SelectionImage += new FImageCrop.DoEvent(SelectionImage);
            crop.ShowDialog();
        }

        private void MailGridView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            ArrayList rows = new ArrayList();
            rows.Clear();
            for (int i = 0; i < MailGridView.SelectedRowsCount; i++)
            {
                rows.Add(MailGridView.GetDataRow(MailGridView.GetSelectedRows()[i]));
            }
            mail_selected_count = rows.Count;
        }

        private void UserTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (UserTabControl.SelectedTabPageIndex)
            {
                case 1:
                    {
                        permissiondetails = true;
                        GroupNameLookUp.EditValue = GroupNameLookUp.Properties.GetKeyValueByDisplayText(group_name);
                    }
                    break;
                case 2:
                    {
                        LoadPhoneDataGridView();
                        LoadMailDataGridView();
                    }
                    break;
            }
        }

        private void MailGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            EditMailBarButton.Enabled = DeleteMailBarButton.Enabled = (MailGridView.RowCount > 0);
        }

        private void PhoneGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            EditPhoneBarButton.Enabled = DeletePhoneBarButton.Enabled = (PhoneGridView.RowCount > 0);
        }
    }
}