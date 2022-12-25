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
using ELMS.Class.DataAccess;
using ELMS.Class;
using ELMS.Class.Tables;
using DevExpress.XtraGrid.Views.Grid;

namespace ELMS.Forms.Customer
{
    public partial class FCustomerAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FCustomerAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public int? CustomerID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID,
            documentID=1, topindex,
            old_row_id, phoneID
            ;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void RefreshDocumentBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadDocument();
        }

        private void NewDocumentBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFDocumentAddEdit(TransactionTypeEnum.Insert, null);
        }

        

        private void EditDocumentBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateDocument();
        }

        private void LoadDocument()
        {
            DocumentGridControl.DataSource = DocumentTypeDAL.SelectDocumentTypeByID(null).ToList<DocumentType>();
        }

        private void LoadFDocumentAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = DocumentGridView.TopRowIndex;
            old_row_id = DocumentGridView.FocusedRowHandle;
            FCardAddEdit fd = new FCardAddEdit()
            {
                TransactionType = transactionType,
                CardID = id
            };
            fd.RefreshDataGridView += new FCardAddEdit.DoEvent(LoadDocument);
            fd.ShowDialog();
            DocumentGridView.TopRowIndex = topindex;
            DocumentGridView.FocusedRowHandle = old_row_id;
        }
        

        private void DocumentGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditDocumentBarButton.Enabled)
                UpdateDocument();
        }

        void UpdateDocument()
        {
            LoadFDocumentAddEdit(TransactionTypeEnum.Update, documentID);

        }



        ////Phone

        private void LoadPhone()
        {
            if (!CustomerID.HasValue)
                CustomerID = 0;

            PhoneGridControl.DataSource = PhoneDAL.SelectPhoneByOwnerID(CustomerID.Value, PhoneOwnerEnum.Customer);

            EditPhoneBarButton.Enabled = DeletePhoneBarButton.Enabled = PhoneGridView.RowCount > 0;
        }

        
        private void NewPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFPhoneAddEdit(TransactionTypeEnum.Insert, null);
        }

        private void LoadFPhoneAddEdit(TransactionTypeEnum transaction, int? id)
        {
            topindex = PhoneGridView.TopRowIndex;
            old_row_id = PhoneGridView.FocusedRowHandle;
            Phone.FPhoneAddEdit fp = new Phone.FPhoneAddEdit()
            {
                TransactionType = transaction,
                PhoneID = id,
                PhoneOwner = PhoneOwnerEnum.Customer,
                OwnerID = CustomerID.Value
            };
            fp.RefreshDataGridView += new Phone.FPhoneAddEdit.DoEvent(LoadPhone);
            fp.ShowDialog();
            PhoneGridView.TopRowIndex = topindex;
            PhoneGridView.FocusedRowHandle = old_row_id;
        }


        void UpdatePhone()
        {
            LoadFPhoneAddEdit(TransactionTypeEnum.Update, phoneID);
        }

        private void EditPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdatePhone();
        }

        private void FCustomerAddEdit_Load(object sender, EventArgs e)
        {
            GlobalProcedures.FillLookUpEdit(SexLookUp, SexDAL.SelectSexByID(null).Tables[0]);

            GlobalProcedures.FillLookUpEdit(CountryLookUp, CountryDAL.SelectCountryByID(null).Tables[0]);

            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Müştərinin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CUSTOMER", GlobalVariables.V_UserID, "WHERE ID = " + CustomerID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş müştəriyə hal-hazırda " + used_user_name + " tərəfindən düzəliş edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
            InsertTemps();
            //LoadDocument();

        }

        private void LoadDetails()
        {
            DataTable dt =CustomerDAL.SelectViewData(CustomerID);

            if (dt.Rows.Count > 0)
            {

                NameText.EditValue = dt.Rows[0]["FULL_NAME"];
                BirthPlaceText.EditValue = dt.Rows[0]["BIRTH_PLACE"];
                RegisteredAddressText.EditValue = dt.Rows[0]["REGISTERED_ADDRESS"];
                ActualAddressText.EditValue = dt.Rows[0]["ADDRESS"];
                NoteText.EditValue = dt.Rows[0]["NOTE"];
                BirthdayDate.EditValue = dt.Rows[0]["BIRTHDAY"];
                if (BirthdayDate.DateTime == DateTime.MinValue)
                    BirthdayDate.EditValue = null;
                GlobalProcedures.LookUpEditValue(SexLookUp, dt.Rows[0]["SEX_NAME"].ToString());
                GlobalProcedures.LookUpEditValue(CountryLookUp, dt.Rows[0]["COUNTRY_NAME"].ToString());
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
            }
        }
        private void InsertTemps()
        {
            if (TransactionType == TransactionTypeEnum.Insert)
                return;
            GlobalProcedures.ExecuteProcedureWithUser("ELMS_USER_TEMP.PROC_INSERT_CUSTOMER_TEMP", "P_CUSTOMER_ID", CustomerID, "Müştərinin məlumatları temp cədvələ daxil edilmədi.");
        }
        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                BirthPlaceText.Enabled =
                BOK.Visible = !status;
        }

        private void PhoneGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditPhoneBarButton.Enabled)
                UpdatePhone();
        }

        void DeletePhone()
        {
            var rows = GlobalFunctions.GridviewSelectedRow(PhoneGridView);

            if (rows.Count == 0)
            {
                GlobalProcedures.ShowWarningMessage("Silmək istədiyiniz telefonu seçin.");
                return;
            }

            if (GlobalFunctions.CallDialogResult("Seçilmiş telefonları silmək istəyirsiniz?", "Telefonların silinməsi") == DialogResult.Yes)
                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i] as DataRow;
                    PhoneDAL.DeletePhone(Convert.ToInt32(row["ID"]), CustomerID.Value, PhoneOwnerEnum.Customer);
                }
        }

        private void DeletePhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeletePhone();
            LoadPhone();
        }

        private void RefreshPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPhone();
        }

        private void PhoneGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            phoneID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
        }

        private void PhoneGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(PhoneGridView, PhonePopupMenu, e);
        }

        private void PhoneGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, CustomerPhone_SS, e);
        }
    }
}