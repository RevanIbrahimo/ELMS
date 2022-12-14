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
using ELMS.Class.Tables;
using ELMS.Class.DataAccess;

namespace ELMS.Forms.Dictionaries
{
    public partial class FPhonePrefixAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FPhonePrefixAddEdit()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public int DescriptionID;
        public int? PrefixID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void FPhonePrefixAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
                LoadDetails();
        }

        private void FPhonePrefixAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.RefreshDataGridView();
        }

        public void InsertDetail()
        {
            PhonePrefix prefix = new PhonePrefix
            {
                PHONE_DESCRIPTION_ID = DescriptionID,
                PREFIX = PrefixText.Text.Trim(),
                NOTE = NoteText.Text.Trim()
            };

            PhonePrefixDAL.InsertPhonePrefix(prefix);
        }

        private void UpdateDetail()
        {
            PhonePrefix prefix = new PhonePrefix
            {
                PHONE_DESCRIPTION_ID = DescriptionID,
                PREFIX = PrefixText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                ID = PrefixID.Value,
                USED_USER_ID = GlobalVariables.V_UserID
            };

            PhonePrefixDAL.UpdatePhonePrefix(prefix);            
        }

        private void LoadDetails()
        {
            List<PhonePrefix> lstPrefix = PhonePrefixDAL.SelectPhoneProfixByID(PrefixID).ToList<PhonePrefix>();
            if (lstPrefix.Count > 0)
            {
                var prefix = lstPrefix.LastOrDefault();
                PrefixText.EditValue = prefix.PREFIX;
                NoteText.EditValue = prefix.NOTE;
            }
        }

        private void PrefixHyperlinkLabel_HyperlinkClick(object sender, DevExpress.Utils.HyperlinkClickEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link);
        }

        private bool ControlDetails()
        {
            List<PhonePrefix> lstPrefix = PhonePrefixDAL.SelectTempPhoneProfixByDescriptionID(DescriptionID).ToList<PhonePrefix>();
            bool b = false;

            if (PrefixText.Text.Length < 3)
            {
                PrefixText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Prefiks 3 simvol olmalıdır");
                PrefixText.Focus();
                PrefixText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (TransactionType == TransactionTypeEnum.Insert && lstPrefix.Count > 0 && lstPrefix.Count(item => item.PREFIX == PrefixText.Text) > 0)
            {
                PrefixText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Daxil etdiyiniz " + PrefixText.Text + " prefiksi artıq bazada mövcuddur.");
                PrefixText.Focus();
                PrefixText.BackColor = GlobalFunctions.ElementColor();
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
    }
}