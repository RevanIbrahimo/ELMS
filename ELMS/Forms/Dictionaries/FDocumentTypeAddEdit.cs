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
    public partial class FDocumentTypeAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FDocumentTypeAddEdit()
        {
            InitializeComponent();
        }
               
        public TransactionTypeEnum TransactionType;
        public int? DocumentTypeID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID,
            personTypeID = 0,
            documentGroupID = 0;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void FDocumentTypeAddEdit_Load(object sender, EventArgs e)
        {
            GlobalProcedures.FillLookUpEdit(GroupNameLookUp, DocumentGroupDAL.SelectDocumentGroupByID(null).Tables[0]);

            GlobalProcedures.FillLookUpEdit(PersonTypeLookUp, PersonTypeDAL.SelectPersonTypeByID(null).Tables[0]);  

            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Sənədin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.DOCUMENT_TYPE", GlobalVariables.V_UserID, "WHERE ID = " + DocumentTypeID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş sənəd hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
                this.Text = "Sənədin əlavə edilməsi";
        }

        private void FDocumentTypeAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.DOCUMENT_TYPE", -1, "WHERE ID = " + DocumentTypeID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        private void LoadDetails1()
        {
            List<DocumentType> lstDocumentType = DocumentTypeDAL.SelectDocumentTypeByID(DocumentTypeID).ToList<DocumentType>();
            if (lstDocumentType.Count > 0)
            {
                var documentType = lstDocumentType.LastOrDefault();
                NameText.EditValue = documentType.NAME;
                CodeText.EditValue = documentType.PTTRN;
                GlobalProcedures.LookUpEditValue(GroupNameLookUp, documentType.GROUP_NAME);
                GlobalProcedures.LookUpEditValue(PersonTypeLookUp, documentType.PERSON_TYPE_NAME);
                UsedUserID = documentType.USED_USER_ID;
                orderID = documentType.ORDER_ID;
            }
        }

        private void LoadDetails()
        {
            DataTable dt = DocumentTypeDAL.SelectViewData(DocumentTypeID);

            if (dt.Rows.Count > 0)
            {

                NameText.EditValue = dt.Rows[0]["NAME"];
                CodeText.EditValue = dt.Rows[0]["PTTRN"];
                GlobalProcedures.LookUpEditValue(GroupNameLookUp, dt.Rows[0]["GROUP_NAME"].ToString());
                GlobalProcedures.LookUpEditValue(PersonTypeLookUp, dt.Rows[0]["PERSON_TYPE_NAME"].ToString());
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
                orderID = Convert.ToInt32(dt.Rows[0]["ORDER_ID"]);
                NoResidentRadioGroup.SelectedIndex = Convert.ToInt16(dt.Rows[0]["NORESIDENT"]);
            }
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

        private void GroupNameLookUp_EditValueChanged(object sender, EventArgs e)
        {
            documentGroupID = GlobalFunctions.GetLookUpID(sender);
        }

        private void PersonTypeLookUp_EditValueChanged(object sender, EventArgs e)
        {
            personTypeID = GlobalFunctions.GetLookUpID(sender);
        }

        private void InsertDetail()
        {
            DocumentType documentType = new DocumentType
            {
                NORESIDENT = NoResidentRadioGroup.SelectedIndex,
                PERSONTYPEID = personTypeID,
                DOCUMENTGROUPID = documentGroupID,
                NAME = NameText.Text.Trim(),
                PTTRN = CodeText.Text.Trim()
            };
            DocumentTypeDAL.InsertDocumentType(documentType);
        }

        private void UpdateDetail()
        {
            isClickBOK = true;

            DocumentType documentType = new DocumentType
            {
                NORESIDENT = NoResidentRadioGroup.SelectedIndex,
                PERSONTYPEID = personTypeID,
                DOCUMENTGROUPID = documentGroupID,
                NAME = NameText.Text.Trim(),
                PTTRN = CodeText.Text.Trim(),
                ID = DocumentTypeID.Value,
                ORDER_ID = orderID,
                USED_USER_ID = -1
            };

            DocumentTypeDAL.UpdateDocumentType(documentType);
        }

        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                CodeText.Enabled =
                BOK.Visible = !status;
        }

        private bool ControlDetail()
        {
            bool b = false;

            if (NameText.Text.Length == 0)
            {
                NameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sənədin adı daxil edilməyib.");
                NameText.Focus();
                NameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (CodeText.Text.Length == 0)
            {
                CodeText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Yoxlama kodu daxil edilməyib.");
                CodeText.Focus();
                CodeText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (documentGroupID == 0)
            {
                GroupNameLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Qrup seçilməyib.");
                GroupNameLookUp.Focus();
                GroupNameLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (personTypeID == 0)
            {
                PersonTypeLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Şəxsin növü seçilməyib.");
                PersonTypeLookUp.Focus();
                PersonTypeLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;
        }
    }
}