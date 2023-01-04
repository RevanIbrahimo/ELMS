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
using ELMS.Class.DataAccess;
using ELMS.Class.Tables;
using ELMS.Forms.Dictionaries;
using Oracle.ManagedDataAccess.Client;

namespace ELMS.Forms.Customer
{
    public partial class FRelativeAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FRelativeAddEdit()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public PhoneOwnerEnum PhoneOwner;
        public int? Customer_ID;
        public int? RelativeID;
        public int? PhoneID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        bool isClickBOK = false;
        int UsedUserID = -1, descriptionID = 0, professionID ,kindID;

        private void FPhoneAddEdit_Load(object sender, EventArgs e)
        {

            GlobalProcedures.FillLookUpEdit(PositionLookUp, ProfessionDAL.SelectProfession(null).Tables[0]);
            GlobalProcedures.FillLookUpEdit(KindNameLookUp, KindShipDAL.SelectKindShip(null).Tables[0]);
            this.ActiveControl = PhoneText;
            LoadPhoneDescription();
            if (TransactionType == TransactionTypeEnum.Update)
                LoadDetail();
        }

        private void LoadDetail()
        {
            DataTable dt = RelativeCardDAL.SelectRelativeByOwnerID((int)Customer_ID, PhoneOwner, RelativeID);

            if (dt.Rows.Count > 0)
            {
                GlobalProcedures.LookUpEditValue(PhoneDescriptionLookUp, dt.Rows[0]["DESCRIPTION_NAME"].ToString());
                PhoneText.EditValue = GlobalFunctions.TrimStart(dt.Rows[0]["PHONE_NUMBER"].ToString(), "+9940");
                NoteText.EditValue = dt.Rows[0]["NOTE"];
                NameText.EditValue = dt.Rows[0]["NAME"]; 
                GlobalProcedures.LookUpEditValue(PositionLookUp, dt.Rows[0]["PROFESSION_NAME"].ToString());
                GlobalProcedures.LookUpEditValue(KindNameLookUp, dt.Rows[0]["KIND_NAME"].ToString());
                SalaryValue.EditValue = Convert.ToDecimal(dt.Rows[0]["SALARY"].ToString());
                PhoneID = Convert.ToInt32(dt.Rows[0]["PHONE_ID"].ToString());
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
            }
        }

        private void FPhoneAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.RefreshDataGridView();
        }

        private void PhoneDescriptionLookUp_EditValueChanged(object sender, EventArgs e)
        {
            descriptionID = GlobalFunctions.GetLookUpID(sender);
            PhoneDescriptionLookUp.Properties.Buttons[2].Visible = descriptionID > 0;
            PhoneText.Focus();
        }

        private bool ControlDetails()
        {
            bool b = false;

            if (descriptionID == 0)
            {
                PhoneDescriptionLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Təsvir seçilməyib.");
                PhoneDescriptionLookUp.Focus();
                PhoneDescriptionLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (((TransactionType == TransactionTypeEnum.Insert && PhoneText.Text.Length < 13) || (TransactionType == TransactionTypeEnum.Update && PhoneText.Text.Length < 9) || PhoneText.Text == "" || PhoneText.Text == "(__)___-__-__" || PhoneText.Text[0] == '0'))
            {
                PhoneText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Nömrə düz deyil.");
                PhoneText.Focus();
                PhoneText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            string phone_prefix = null, phone = PhoneText.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "");
            phone_prefix = "0" + phone.Substring(0, 2);
            List<PhonePrefix> lstPrefix = PhonePrefixDAL.SelectPhoneProfixByDescriptionID(descriptionID).ToList<PhonePrefix>();
            var prefix = lstPrefix.Find(p => p.PREFIX.Trim() == phone_prefix);
            if (prefix == null)
            {
                PhoneText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Nömrənin prefix-i təsvir ilə uyğun deyil və ya bazada yoxdur. Zəhmət olmasa nömrənin mobil və ya şəhər nömrəsi olduğunu düzgün seçin.");
                PhoneText.Focus();
                PhoneText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;
        }

        private void BOK_Click(object sender, EventArgs e)
        {  if (ControlDetails())
            {
                GlobalFunctions.RunInOneTransaction<int>(tran =>
                {
                    if (TransactionType == TransactionTypeEnum.Insert)
                    {
                        InsertDetail(tran);
                        InsertRelative(tran);
                    }
                    else
                    {
                        UpdateDetail(tran);
                        UpdateRelative(tran);
                    }
                    return 1;
                }, TransactionType == TransactionTypeEnum.Insert ? "Müştərinin məlumatları bazaya daxil edilmədi." : "Müştərinin məlumatları bazada dəyişdirilmədi.");
                this.Close();
            }
            }

        void LoadPhoneDescription()
        {
            GlobalProcedures.FillLookUpEdit(PhoneDescriptionLookUp, PhoneDescriptionDAL.SelectPhoneDescriptionByID(null).Tables[0], 0);
        }

        private void LoadFPhoneDescriptionAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            FPhoneDescriptionsAddEdit ft = new FPhoneDescriptionsAddEdit()
            {
                TransactionType = transactionType,
                DescriptionID = id
            };
            ft.RefreshDataGridView += new FPhoneDescriptionsAddEdit.DoEvent(LoadPhoneDescription);
            ft.ShowDialog();
        }

        private void PhoneDescriptionLookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                LoadFPhoneDescriptionAddEdit(TransactionTypeEnum.Insert, null);
            else if (e.Button.Index == 2)
                LoadFPhoneDescriptionAddEdit(TransactionTypeEnum.Update, descriptionID);
        }

        private void InsertDetail(OracleTransaction tran)
        {
            Class.Tables.Phone phone = new Class.Tables.Phone
            {
                OWNER_TYPE = (int)PhoneOwner,
                OWNER_ID = (int)Customer_ID,
                PHONE_DESCRIPTION_ID = descriptionID,
                PHONE_NUMBER = "+9940" + PhoneText.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", ""),
                IS_SEND_SMS = 0,
                IS_CHANGE = (int)ChangeTypeEnum.Change
            };

           PhoneID = PhoneDAL.InsertRelativePhone(tran,phone);
        }

        private void UpdateDetail(OracleTransaction tran)
        {
            Class.Tables.Phone phone = new Class.Tables.Phone
            {
                OWNER_TYPE = (int)PhoneOwner,
                OWNER_ID = (int)Customer_ID,
                PHONE_DESCRIPTION_ID = descriptionID,
                PHONE_NUMBER = "+9940" + PhoneText.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", ""),
                IS_SEND_SMS = 0,
                IS_CHANGE = (int)ChangeTypeEnum.Change,
                ID = PhoneID.Value
            };

            PhoneDAL.UpdateRelativePhone(tran,phone);
        }
        
        private void KindNameLookUp_EditValueChanged(object sender, EventArgs e)
        {
            kindID = GlobalFunctions.GetLookUpID(sender);
        }

        private void PositionLookUp_EditValueChanged(object sender, EventArgs e)
        {
            professionID = GlobalFunctions.GetLookUpID(sender);

        }

        private void InsertRelative(OracleTransaction tran)
            {
                CustomerRelative customerRelative = new CustomerRelative
                {
                    NAME = NameText.Text.Trim(),
                    PROFESSION_ID = professionID,
                    KIND_ID = kindID,
                    SALARY = SalaryValue.Value,
                    NOTE = NoteText.Text.Trim(),
                    PHONE_ID = PhoneID.Value,
                    CUSTOMER_ID = Customer_ID.Value
                };
           RelativeID = RelativeCardDAL.InsertCustomerRelative(tran,customerRelative);
            }

        private void UpdateRelative(OracleTransaction tran)
        {
            isClickBOK = true;

            CustomerRelative customerRelative = new CustomerRelative
            {
                NAME = NameText.Text.Trim(),
                PROFESSION_ID = professionID,
                KIND_ID = kindID,
                SALARY = SalaryValue.Value,
                NOTE = NoteText.Text.Trim(),
                PHONE_ID = PhoneID.Value,
                CUSTOMER_ID = Customer_ID.Value,
                ID = RelativeID.Value,
                USED_USER_ID = -1,
                IS_CHANGE = (int)ChangeTypeEnum.Change
            };
            RelativeCardDAL.UpdateCustomerRelative(tran,customerRelative);
        }
    }
}