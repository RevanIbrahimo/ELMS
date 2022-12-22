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

namespace ELMS.Forms.Phone
{
    public partial class FPhoneAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FPhoneAddEdit()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public PhoneOwnerEnum PhoneOwner;
        public int? OwnerID;
        public int? PhoneID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        int descriptionID = 0;

        private void FPhoneAddEdit_Load(object sender, EventArgs e)
        {
            this.ActiveControl = PhoneText;
            LoadPhoneDescription();
            if (TransactionType == TransactionTypeEnum.Update)
                LoadDetail();
        }

        private void LoadDetail()
        {
            DataTable dt = PhoneDAL.SelectPhoneByOwnerID((int)OwnerID, PhoneOwner, PhoneID);

            if (dt.Rows.Count > 0)
            {
                GlobalProcedures.LookUpEditValue(PhoneDescriptionLookUp, dt.Rows[0]["DESCRIPTION_NAME"].ToString());
                PhoneText.EditValue = GlobalFunctions.TrimStart(dt.Rows[0]["PHONE_NUMBER"].ToString(), "+9940");
                NoteText.EditValue = dt.Rows[0]["NOTE"];
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
        {
            if (ControlDetails())
            {
                if (TransactionType == TransactionTypeEnum.Insert)
                    InsertDetail();
                else
                    UpdateDetail();
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

        private void InsertDetail()
        {
            Class.Tables.Phone phone = new Class.Tables.Phone
            {
                OWNER_TYPE = (int)PhoneOwner,
                OWNER_ID = (int)OwnerID,
                PHONE_DESCRIPTION_ID = descriptionID,
                PHONE_NUMBER = "+9940" + PhoneText.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", ""),
                NOTE = NoteText.Text.Trim(),
                IS_SEND_SMS = 0,
                IS_CHANGE = (int)ChangeTypeEnum.Change
            };

            PhoneDAL.InsertPhone(phone);
        }

        private void UpdateDetail()
        {
            Class.Tables.Phone phone = new Class.Tables.Phone
            {
                OWNER_TYPE = (int)PhoneOwner,
                OWNER_ID = (int)OwnerID,
                PHONE_DESCRIPTION_ID = descriptionID,
                PHONE_NUMBER = "+9940" + PhoneText.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", ""),
                NOTE = NoteText.Text.Trim(),
                IS_SEND_SMS = 0,
                IS_CHANGE = (int)ChangeTypeEnum.Change,
                ID = PhoneID.Value
            };

            PhoneDAL.UpdatePhone(phone);
        }
    }
}