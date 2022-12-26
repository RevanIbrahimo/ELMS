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
using Oracle.ManagedDataAccess.Client;

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
            documentID, topindex,
            old_row_id, 
            phoneID,
            countryID = 0,
            sexID = 0;

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
            DocumentGridControl.DataSource = CustomerCardDAL.SelectViewData(null).ToList<CustomerCard>();
        }

        private void LoadFDocumentAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = DocumentGridView.TopRowIndex;
            old_row_id = DocumentGridView.FocusedRowHandle;
            FCardAddEdit fd = new FCardAddEdit()
            {
                TransactionType = transactionType,
                CustomerID = CustomerID, 
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
            RefreshDictionaries(1);

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
            LoadDocument();
            LoadPhone();
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

        private void DocumentGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow row = DocumentGridView.GetFocusedDataRow();
            if (row != null)
                documentID = Convert.ToInt32(row["ID"].ToString());
        }

        private void InsertCustomer(OracleTransaction tran)
        {
            Class.Tables.Customer customer = new Class.Tables.Customer
            {
                FULL_NAME = NameText.Text.Trim(),
                BRANCH_ID = GlobalVariables.V_BranchID,
                ADDRESS = ActualAddressText.Text.Trim(),
                BIRTH_PLACE = BirthPlaceText.Text.Trim(),
                BIRTHDAY = BirthdayDate.DateTime,
                COUNTRY_ID = countryID,
                SEX_ID = sexID,
                NOTE = NoteText.Text.Trim(),
                REGISTERED_ADDRESS = RegisteredAddressText.Text.Trim()
            };

            CustomerDAL.InsertCustomer(tran, customer);
        }

        private void UpdateCustomer(OracleTransaction tran)
        {
            Class.Tables.Customer customer = new Class.Tables.Customer
            {
                FULL_NAME = NameText.Text.Trim(),
                BRANCH_ID = GlobalVariables.V_BranchID,
                ADDRESS = ActualAddressText.Text.Trim(),
                BIRTH_PLACE = BirthPlaceText.Text.Trim(),
                BIRTHDAY = BirthdayDate.DateTime,
                COUNTRY_ID = countryID,
                SEX_ID = sexID,
                NOTE = NoteText.Text.Trim(),
                REGISTERED_ADDRESS = RegisteredAddressText.Text.Trim()
            };

            CustomerDAL.UpdateCustomer(tran, customer);
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            GlobalFunctions.RunInOneTransaction<int>(tran =>
            {
                if (TransactionType == TransactionTypeEnum.Insert)
                {
                    InsertCustomer(tran);
                    InsertImageDetail(tran);
                }
                else
                {
                    UpdateCustomer(tran);
                    UpdateImageDetail(tran);
                }
                

                return 1;
            }, TransactionType == TransactionTypeEnum.Insert ? "Xəstənin məlumatları bazaya daxil edilmədi." : "Xəstənin məlumatları bazada dəyişdirilmədi.");
        }

        private void InsertImageDetail(OracleTransaction tran)
        {
            if (PictureEdit.Image != null)
            {
                CustomerImage image = new CustomerImage
                {
                    CUSTOMER_ID = CustomerID.Value,
                    IMAGE = GlobalFunctions.ImageToByteArray(PictureEdit.Image)
                };

                CustomerImageDAL.InsertCustomerImage(tran, image);
            }
            else
                CustomerImageDAL.DeleteCustomerImage(tran, CustomerID.Value);
        }

        private void UpdateImageDetail(OracleTransaction tran)
        {
            if (PictureEdit.Image != null)
            {
                CustomerImage image = new CustomerImage
                {
                    CUSTOMER_ID = CustomerID.Value,
                    IMAGE = GlobalFunctions.ImageToByteArray(PictureEdit.Image)
                };

                if (lstImage.Count > 0)
                    CustomerImageDAL.UpdateCustomerImage(tran, image);
                else
                    CustomerImageDAL.InsertCustomerImage(tran, image);
            }
            else
                CustomerImageDAL.DeleteCustomerImage(tran, CustomerID.Value);
        }

        void RefreshDictionaries(int index)
        {
            switch (index)
            {
                case 1:
                    GlobalProcedures.FillLookUpEdit(CountryLookUp, CountryDAL.SelectCountryByID(null).Tables[0]);
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

        private void CountryLookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                LoadDictionaries(TransactionTypeEnum.Update, 1);
        }

        private void SexLookUp_EditValueChanged(object sender, EventArgs e)
        {
            sexID = GlobalFunctions.GetLookUpID(sender);
        }

        private void CountryLookUp_EditValueChanged(object sender, EventArgs e)
        {
            countryID = GlobalFunctions.GetLookUpID(sender);
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