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
using ELMS.Class.Tables;
using ELMS.Class.DataAccess;

namespace ELMS.Forms.Dictionaries
{
    public partial class FCountryAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FCountryAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public int? CountryID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void FCountryAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.COUNTRY", -1, "WHERE ID = " + CountryID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        private void FCountryAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Ölkələrin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.COUNTRY", GlobalVariables.V_UserID, "WHERE ID = " + CountryID + " AND USED_USER_ID = -1");
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
                this.Text = "Ölkələrin əlavə edilməsi";
        }

        private void LoadDetails()
        {
            List<Countries> lstCountries = CountriesDAL.SelectCountriesByID(CountryID).ToList<Countries>();
            if (lstCountries.Count > 0)
            {
                var countries = lstCountries.LastOrDefault();
                NameText.EditValue = countries.NAME;
                Alpha3CodeText.EditValue = countries.ALPHA3CODE;
                NoteText.EditValue = countries.NOTE;
                UsedUserID = countries.USED_USER_ID;
                orderID = countries.ORDER_ID;
            }
        }

        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                Alpha3CodeText.Enabled =
                NoteText.Enabled =
                BOK.Visible = !status;
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

        private bool ControlDetail()
        {
            bool b = false;

            if (NameText.Text.Length == 0)
            {
                NameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Ölkə adı daxil edilməyib.");
                NameText.Focus();
                NameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (Alpha3CodeText.Text.Length == 0)
            {
                Alpha3CodeText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Alpha3 kodu daxil edilməyib.");
                Alpha3CodeText.Focus();
                Alpha3CodeText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;
        }

        private void InsertDetail()
        {
            Countries countries = new Countries
            {               
                NAME = NameText.Text.Trim(),
                ALPHA3CODE = Alpha3CodeText.Text.Trim(),
                NOTE = NoteText.Text.Trim()
            };
            CountriesDAL.InsertCountries(countries);
        }

        private void UpdateDetail()
        {
            isClickBOK = true;

            Countries countries = new Countries
            {
                NAME = NameText.Text.Trim(),
                ALPHA3CODE = Alpha3CodeText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                ID = CountryID.Value,
                ORDER_ID = orderID,
                USED_USER_ID = -1
            };

            CountriesDAL.UpdateCountries(countries);
        }
    }
}