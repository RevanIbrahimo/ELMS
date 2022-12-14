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

namespace ELMS.Forms.Customer
{
    public partial class FCardAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FCardAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public int? CardID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        int documentGroupID = 0,
            documentTypeID = 0,
            cardIssuingID = 0;
        List<DocumentType> lstDocumentType = null;

        private void FCardAddEdit_Load(object sender, EventArgs e)
        {
            GlobalProcedures.FillLookUpEdit(DocumentGroupLookUp, DocumentGroupDAL.SelectDocumentGroupByID(null).Tables[0]);
            RefreshDictionaries(2);
            if(TransactionType == TransactionTypeEnum.Insert)
                DocumentGroupLookUp.EditValue = DocumentGroupLookUp.Properties.GetKeyValueByDisplayText("Şəxsiyyət vəsiqəsi");
        }

        private void DocumentGroupLookUp_EditValueChanged(object sender, EventArgs e)
        {
            documentGroupID = GlobalFunctions.GetLookUpID(sender);
            RefreshDictionaries(0);
        }

        private void FCardAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.RefreshDataGridView();
        }

        void RefreshDictionaries(int index)
        {
            switch (index)
            {
                case 0:
                    GlobalProcedures.FillLookUpEdit(DocumentTypeLookUp, DocumentTypeDAL.SelectDocumentTypeByID(null).Tables[0]);
                    break;
                case 2:
                    GlobalProcedures.FillLookUpEdit(IssuingLookUp, CardIssuingDAL.SelectCardIssuingByID(null).Tables[0]);
                    break;
            }
        }

        private void LoadDictionaries(TransactionTypeEnum transaction, int index)
        {
            Dictionaries.FDictionaries fc = new Dictionaries.FDictionaries();
            fc.TransactionType = transaction;
            fc.ViewSelectedTabIndex = index;
            fc.RefreshList += new Dictionaries.FDictionaries.DoEvent(RefreshDictionaries);
            fc.ShowDialog();
        }

        private void DocumentTypeLookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                LoadDictionaries(TransactionTypeEnum.Update, 0);
        }

        private void IssuingLookUp_EditValueChanged(object sender, EventArgs e)
        {
            cardIssuingID = GlobalFunctions.GetLookUpID(sender);
        }

        private bool ControlCardDetails()
        {
            bool b = false;

            if (documentGroupID == 0)
            {
                DocumentGroupLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin qrupu seçilməyib.");
                DocumentGroupLookUp.Focus();
                DocumentGroupLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (documentTypeID == 0)
            {
                DocumentTypeLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin növü seçilməyib.");
                DocumentTypeLookUp.Focus();
                DocumentTypeLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (NumberText.Text.Length == 0)
            {
                NumberText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin nömrəsi daxil edilməyib.");
                NumberText.Focus();
                NumberText.BackColor = GlobalFunctions.ElementColor(); ;
                return false;
            }
            else
                b = true;

            var documentType = lstDocumentType.First();

            if (!GlobalFunctions.Regexp(documentType.PTTRN, NumberText.Text.Trim()))
            {
                NumberText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin nömrəsi düz deyil.");
                NumberText.Focus();
                NumberText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (documentType.ID == (int)DocumentTypeEnum.KohneSexsiyyetVesiqesi && NumberText.Text.Length != 11)
            {
                NumberText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin nömrəsinin uzunluğu 11 simvol olmalıdır.");
                NumberText.Focus();
                NumberText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if ((documentTypeID == (int)DocumentTypeEnum.KohneSexsiyyetVesiqesi || documentTypeID == (int)DocumentTypeEnum.YeniSexsiyyetVesiqesi) && PinCodeText.Text.Length == 0)
            {
                PinCodeText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin fin kodu daxil edilməyib. Kod yalnız 7 simvol olmalıdır.");
                PinCodeText.Focus();
                PinCodeText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if ((documentTypeID == (int)DocumentTypeEnum.KohneSexsiyyetVesiqesi || documentTypeID == (int)DocumentTypeEnum.YeniSexsiyyetVesiqesi) && (!GlobalFunctions.Regexp("[0-9A-Za-z]{1,7}", PinCodeText.Text.Trim()) || PinCodeText.Text.Length != 7))
            {
                PinCodeText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin fin kodu düz deyil. Kod yalnız 7 simvol olmalıdır.");
                PinCodeText.Focus();
                PinCodeText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (String.IsNullOrEmpty(DateOfIssueDate.Text))
            {
                DateOfIssueDate.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin verilmə tarixi daxil edilməyib.");
                DateOfIssueDate.Focus();
                DateOfIssueDate.BackColor = GlobalFunctions.ElementColor(); ;
                return false;
            }
            else if (String.IsNullOrEmpty(ReliableDate.Text))
            {
                ReliableDate.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin etibarlı olma tarixi daxil edilməyib.");
                ReliableDate.Focus();
                ReliableDate.BackColor = GlobalFunctions.ElementColor(); ;
                return false;
            }
            else if (GlobalFunctions.ChangeStringToDate(DateOfIssueDate.Text, "ddmmyyyy") == GlobalFunctions.ChangeStringToDate(ReliableDate.Text, "ddmmyyyy"))
            {
                DateOfIssueDate.BackColor = Color.Red;
                ReliableDate.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin verilmə tarixi ilə etibarlı olma tarixi eyni ola bilməz.");
                DateOfIssueDate.Focus();
                DateOfIssueDate.BackColor = GlobalFunctions.ElementColor(); ;
                ReliableDate.BackColor = GlobalFunctions.ElementColor(); ;
                return false;
            }
            else
                b = true;

            if (cardIssuingID == 0)
            {
                IssuingLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədi verən orqanın adı seçilməyib.");
                IssuingLookUp.Focus();
                IssuingLookUp.BackColor = GlobalFunctions.ElementColor(); ;
                return false;
            }
            else
                b = true;            

            int card_count = GlobalFunctions.GetCount($@"SELECT COUNT(*) FROM (SELECT CARD_NUMBER,DOCUMENT_GROUP_ID,DOCUMENT_TYPE_ID FROM COMS_USER_TEMP.CUSTOMER_CARDS_TEMP UNION ALL SELECT CARD_NUMBER,DOCUMENT_GROUP_ID,DOCUMENT_TYPE_ID FROM COMS_USER.CUSTOMER_CARDS) WHERE CARD_NUMBER = '{NumberText.Text.Trim()}' AND DOCUMENT_GROUP_ID = {documentGroupID} AND DOCUMENT_TYPE_ID = {documentTypeID}"); ;
            
            if (card_count > 0 && TransactionType == TransactionTypeEnum.Insert)
            {
                NumberText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Daxil etdiyiniz nömrə artıq bazaya daxil edilib.");
                NumberText.Focus();
                NumberText.BackColor = GlobalFunctions.ElementColor(); ;
                return false;
            }
            else
                b = true;

            int card_code = GlobalFunctions.GetCount($@"SELECT COUNT(*) FROM (SELECT PINCODE,CARD_NUMBER,DOCUMENT_GROUP_ID,DOCUMENT_TYPE_ID FROM COMS_USER_TEMP.CUSTOMER_CARDS_TEMP UNION ALL SELECT PINCODE,CARD_NUMBER,DOCUMENT_GROUP_ID,DOCUMENT_TYPE_ID FROM COMS_USER.CUSTOMER_CARDS) WHERE PINCODE = '{PinCodeText.Text.Trim()}' AND CARD_NUMBER = '{NumberText.Text.Trim()}' AND DOCUMENT_GROUP_ID = {documentGroupID} AND DOCUMENT_TYPE_ID = {documentTypeID}");
            
            if (card_code > 0 && TransactionType == TransactionTypeEnum.Insert)
            {
                PinCodeText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Daxil etdiyiniz fin kod artıq bazaya daxil edilib.");
                PinCodeText.Focus();
                PinCodeText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if(ControlCardDetails())
            {
                this.Close();
            }
        }

        private void IssuingLookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                LoadDictionaries(TransactionTypeEnum.Update,2);
        }

        private void DocumentTypeLookUp_EditValueChanged(object sender, EventArgs e)
        {
            documentTypeID = GlobalFunctions.GetLookUpID(sender);

            lstDocumentType = DocumentTypeDAL.SelectDocumentTypeByID(documentTypeID).ToList<DocumentType>();
            var documentType = lstDocumentType.First();
            PinCodeStarLabel.Visible = (documentTypeID == (int)DocumentTypeEnum.KohneSexsiyyetVesiqesi || documentTypeID == (int)DocumentTypeEnum.YeniSexsiyyetVesiqesi);
            NumberText.Text = (documentType.ID == (int)DocumentTypeEnum.KohneSexsiyyetVesiqesi) ? NumberText.Text + "AZE" : null;
            OldCardExampleLabel.Visible = documentType.ID == (int)DocumentTypeEnum.KohneSexsiyyetVesiqesi;
            NumberText.Focus();
        }
    }
}