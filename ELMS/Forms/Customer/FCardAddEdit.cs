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
        public int? CustomerID;
        public int? CardID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        int documentGroupID = 0,
            documentTypeID = 0,
            cardIssuingID = 0;
        bool CurrentStatus = false, Used = false,isClickBOK = false;
        int UsedUserID = -1;


        List<DocumentType> lstDocumentType = null;

        private void FCardAddEdit_Load(object sender, EventArgs e)
        {
            GlobalProcedures.FillLookUpEdit(IssuingLookUp, CardIssuingDAL.SelectCardIssuingByID(null).Tables[0]);
            GlobalProcedures.FillLookUpEdit(DocumentTypeLookUp, DocumentTypeDAL.SelectDocumentTypeByID(null).Tables[0]);
            GlobalProcedures.FillLookUpEdit(DocumentGroupLookUp, DocumentGroupDAL.SelectDocumentGroupByID(null).Tables[0]);
            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Sənədlərin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CUSTOMER_CARDS", GlobalVariables.V_UserID, "WHERE ID = " + CardID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş sənədlərə hal-hazırda " + used_user_name + " tərəfindən düzəliş edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
                this.Text = "Müştərinin əlavə edilməsi";
            //InsertTemps();
            //LoadDocument();
            //LoadPhone();
            //RefreshDictionaries(2);
            //if(TransactionType == TransactionTypeEnum.Insert)
            //    DocumentGroupLookUp.EditValue = DocumentGroupLookUp.Properties.GetKeyValueByDisplayText("Şəxsiyyət vəsiqəsi");
        }

        private void LoadDetails()
        {

            DataTable dt = CustomerCardDAL.SelectViewData(CustomerID);

            if (dt.Rows.Count > 0)
            {
                PinCodeText.EditValue = dt.Rows[0]["PINCODE"];
                NumberText.EditValue = dt.Rows[0]["CARD_NUMBER"];
                DateOfIssueDate.EditValue = dt.Rows[0]["ISSUE_DATE"];
                if (DateOfIssueDate.DateTime == DateTime.MinValue)
                    DateOfIssueDate.EditValue = null;
                ReliableDate.EditValue = dt.Rows[0]["RELIABLE_DATE"];
                if (ReliableDate.DateTime == DateTime.MinValue)
                    ReliableDate.EditValue = null;
                GlobalProcedures.LookUpEditValue(DocumentTypeLookUp, dt.Rows[0]["DOCUMENT_TYPE"].ToString());
                GlobalProcedures.LookUpEditValue(IssuingLookUp, dt.Rows[0]["ISSUE_NAME"].ToString());
                GlobalProcedures.LookUpEditValue(DocumentGroupLookUp, dt.Rows[0]["DOCUMENT_GROUP"].ToString());
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
            }
        }

        private void ComponentEnabled(bool status)
        {
            NumberText.Enabled =
                PinCodeText.Enabled =
                BOK.Visible = !status;
        }

        private void InsertDetail()
        {
            CustomerCard customerCard = new CustomerCard
            {
                DOCUMENT_GROUP_ID = documentTypeID,
                DOCUMENT_TYPE_ID = documentGroupID,
                CARD_ISSUING_ID = cardIssuingID,
                PINCODE = PinCodeText.Text.Trim(),
                CARD_NUMBER = NumberText.Text.Trim(),

            };
            CustomerCardDAL.InsertCustomerCard(customerCard);
        }

        private void UpdateDetail()
        {
            isClickBOK = true;

            CustomerCard customerCard = new CustomerCard
            {
                DOCUMENT_GROUP_ID = documentTypeID,
                DOCUMENT_TYPE_ID = documentGroupID,
                CARD_ISSUING_ID = cardIssuingID,
                PINCODE = PinCodeText.Text.Trim(),
                CARD_NUMBER = NumberText.Text.Trim(),
                ISSUE_DATE = DateOfIssueDate.DateTime,
                RELIABLE_DATE = ReliableDate.DateTime,
                ID = CardID.Value,
                CUSTOMER_ID = CustomerID.Value,
                USED_USER_ID = -1
            };

            CustomerCardDAL.UpdateCustomerCard(customerCard);
        }


        private void DocumentGroupLookUp_EditValueChanged(object sender, EventArgs e)
        {
            documentGroupID = GlobalFunctions.GetLookUpID(sender);
            RefreshDictionaries(0);
        }

        private void FCardAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CUSTOMER_CARDS", -1, "WHERE ID = " +CardID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
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
            else if (!(GlobalFunctions.ChangeStringToDate(DateOfIssueDate.Text, "ddmmyyyy") == GlobalFunctions.ChangeStringToDate(ReliableDate.Text, "ddmmyyyy")))
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
                //GlobalFunctions.RunInOneTransaction<int>(tran =>
                //{
                //    if (TransactionType == TransactionTypeEnum.Insert)
                //        InsertDetail();
                //    else
                //        UpdateDetail();
                //    GlobalProcedures.ExecuteProcedureWithParametr(tran, "ELMS_USER.PROC_INSERT_CUSTOMER_CARD", "P_CUSTOMER_ID", CustomerID);

                //    return 1;
                //}, TransactionType == TransactionTypeEnum.Insert ? "Sənəd bazaya daxil edilmədi." : "Sənəd bazada dəyişdirilmədi.");

                if (TransactionType == TransactionTypeEnum.Insert)
                    InsertDetail();
                else
                    UpdateDetail();
                GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER.PROC_INSERT_CUSTOMER_CARD", "P_CUSTOMER_ID", CustomerID, "Sənəd bazada dəyişdirilmədi.");

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