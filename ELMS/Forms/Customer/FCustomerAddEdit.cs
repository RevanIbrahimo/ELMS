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
        int UsedUserID = -1,
            documentID, relativeID, topindex,
            old_row_id, 
            phoneID, 
            workID,
            countryID = 0,
            sexID = 0;
        string CustomerImage;
        string UserImagePath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Images";

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;


        List<CustomerImage> lstImage = new List<CustomerImage>();

        private void FCustomerAddEdit_Load(object sender, EventArgs e)
        {
            GlobalProcedures.FillLookUpEdit(SexLookUp, SexDAL.SelectSexByID(null).Tables[0]);
            RefreshDictionaries(1);

            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Müştərinin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CUSTOMER", GlobalVariables.V_UserID, "WHERE ID = " + CustomerID + " AND USED_USER_ID = -1");
                LoadDetails();
                LoadImage();
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
            //LoadPhone();
        }

        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                BirthPlaceText.Enabled =
                BOK.Visible = !status;
        }

        private void LoadDetails()
        {
            DataTable dt = CustomerDAL.SelectViewData(CustomerID);

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

        private void LoadImage()
        {
            if (CustomerID.HasValue)
            {
                lstImage = CustomerImageDAL.SelectCustomerImage(CustomerID.Value).ToList<CustomerImage>();
                if (lstImage.Count > 0)
                {
                    var image = lstImage.LastOrDefault();
                    PictureEdit.EditValue = image.IMAGE;
                }
            }
        }

        private void InsertTemps()
        {
            if (TransactionType == TransactionTypeEnum.Insert)
                return;
            GlobalProcedures.ExecuteProcedureWithUser("ELMS_USER_TEMP.PROC_INSERT_CUSTOM_TEMP_DATA", "P_CUSTOMER_ID", CustomerID, "Müştərinin məlumatları temp cədvələ daxil edilmədi.");
        }

        private void OtherInfoTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (OtherInfoTabControl.SelectedTabPageIndex)
            {
                case 0:
                    {
                        LoadDocument();
                    }
                    break;
                case 1:
                    {
                        LoadPhone();
                    }
                    break;
                case 2:
                    {
                        LoadWork();
                    }
                    break;
                case 3:
                    {
                        LoadRelative();
                    }
                    break;
            }
        }





        // DOCUMENT -e aid olan hisse
        /// /////////
        /// /////////
        /// /////////

        private void LoadDocument()
        {
           // DocumentGridControl.DataSource = CustomerCardDAL.SelectViewDataAll(CustomerID);
            if (!CustomerID.HasValue)
                CustomerID = 0;

            DocumentGridControl.DataSource = CustomerCardDAL.SelectViewDataAll(CustomerID.Value);

            EditDocumentBarButton.Enabled = DeleteDocumentBarButton.Enabled = DocumentGridView.RowCount > 0;
        }
        
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

        void UpdateDocument()
        {
            LoadFDocumentAddEdit(TransactionTypeEnum.Update, documentID);
        }

        private void DeleteDocumentBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteDocument();
            LoadDocument();
        }

        void DeleteDocument()
        {
            var rows = GlobalFunctions.GridviewSelectedRow(DocumentGridView);

            if (rows.Count == 0)
            {
                GlobalProcedures.ShowWarningMessage("Silmək istədiyiniz sənədi seçin.");
                return;
            }

            if (GlobalFunctions.CallDialogResult("Seçilmiş sənədləri silmək istəyirsiniz?", "Sənədlərin silinməsi") == DialogResult.Yes)
                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i] as DataRow;
                    CustomerCardDAL.DeleteCustomerCard(Convert.ToInt32(row["ID"]), CustomerID.Value);
                }
        }

        private void LoadFDocumentAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = DocumentGridView.TopRowIndex;
            old_row_id = DocumentGridView.FocusedRowHandle;
            FCardAddEdit fd = new FCardAddEdit()
            {
                TransactionType = transactionType,
                CustomerID = CustomerID.Value,
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

        private void DocumentGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow row = DocumentGridView.GetFocusedDataRow();
            if (row != null)
                documentID = Convert.ToInt32(row["ID"].ToString());
        }

        private void DocumentGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Document_SS, e);
        }

        private void DocumentGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(DocumentGridView, DocumentPopupMenu, e);

        }




        // PHONE -e aid olan hisse
        /// /////////
        /// /////////
        /// /////////

        private void LoadPhone()
        {
            if (!CustomerID.HasValue)
                CustomerID = 0;

            PhoneGridControl.DataSource = PhoneDAL.SelectPhoneByOwnerID(CustomerID.Value, PhoneOwnerEnum.Customer);

            EditPhoneBarButton.Enabled = DeletePhoneBarButton.Enabled = PhoneGridView.RowCount > 0;
        }

        private void RefreshPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPhone();
        }

        private void NewPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFPhoneAddEdit(TransactionTypeEnum.Insert, null);
        }
        
        private void EditPhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdatePhone();
        }

        void UpdatePhone()
        {
            LoadFPhoneAddEdit(TransactionTypeEnum.Update, phoneID);
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

        private void DeletePhoneBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeletePhone();
            LoadPhone();
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

        private void PhoneGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditPhoneBarButton.Enabled)
                UpdatePhone();
        }

        private void PhoneGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(PhoneGridView, PhonePopupMenu, e);
        }

        private void PhoneGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            phoneID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
        }

        private void PhoneGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, CustomerPhone_SS, e);
        }





        // WORK -e aid olan hisse
        /// /////////
        /// /////////
        /// /////////

        private void LoadWork()
        {
            WorkGridControl.DataSource = CustomerWorkDAL.SelectViewDataAll(CustomerID);
        }

        private void RefreshWorkBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadWork();
        }

        private void NewWorkBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFWorkAddEdit(TransactionTypeEnum.Insert, null);
        }

        private void EditWorkBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateWork();

        }

        void UpdateWork()
        {
            LoadFWorkAddEdit(TransactionTypeEnum.Update, workID);
        }

        private void LoadFWorkAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = WorkGridView.TopRowIndex;
            old_row_id = WorkGridView.FocusedRowHandle;
            FWorkAddEdit fd = new FWorkAddEdit()
            {
                TransactionType = transactionType,
                CustomerID = CustomerID.Value,
                WorkID = id
            };
            fd.RefreshDataGridView += new FWorkAddEdit.DoEvent(LoadWork);
            fd.ShowDialog();
            WorkGridView.TopRowIndex = topindex;
            WorkGridView.FocusedRowHandle = old_row_id;
        }

        private void DeleteWorkBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteWork();
            LoadWork();
        }

        void DeleteWork()
        {
            var rows = GlobalFunctions.GridviewSelectedRow(WorkGridView);

            if (rows.Count == 0)
            {
                GlobalProcedures.ShowWarningMessage("Silmək istədiyiniz iş yerini seçin.");
                return;
            }

            if (GlobalFunctions.CallDialogResult("Seçilmiş iş yerlərini silmək istəyirsiniz?", "İş yerlərinin silinməsi") == DialogResult.Yes)
                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i] as DataRow;
                    CustomerWorkDAL.DeleteCustomerWork(Convert.ToInt32(row["ID"]), CustomerID.Value);
                }
        }

        private void WorkGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow row = WorkGridView.GetFocusedDataRow();
            if (row != null)
                workID = Convert.ToInt32(row["ID"].ToString());
        }

        private void WorkGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, CustomerWork_SS, e);
        }

        private void WorkGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(WorkGridView, WorkPopupMenu, e);

        }





        // RELATIVE -e aid olan hisse
        /// /////////
        /// /////////
        /// /////////

        private void LoadRelative()
        {
            if (!CustomerID.HasValue)
                CustomerID = 0;

            RelativeGridControl.DataSource = RelativeCardDAL.SelectRelativeByOwnerID(CustomerID.Value, PhoneOwnerEnum.Relative);

            EditPhoneBarButton.Enabled = DeletePhoneBarButton.Enabled = RelativeGridView.RowCount > 0;
        }

        private void RefreshRelativeBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadRelative();
        }

        private void NewRelativeBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFRelativeAddEdit(TransactionTypeEnum.Insert, null);
        }

        private void EditRelativeBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFRelativeAddEdit(TransactionTypeEnum.Update, relativeID);
        }

        private void LoadFRelativeAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = RelativeGridView.TopRowIndex;
            old_row_id = RelativeGridView.FocusedRowHandle;
            FRelativeAddEdit fd = new FRelativeAddEdit()
            {
                TransactionType = transactionType,
                Customer_ID = CustomerID.Value,
                RelativeID = id,
                PhoneOwner = PhoneOwnerEnum.Relative,
            };
            fd.RefreshDataGridView += new FRelativeAddEdit.DoEvent(LoadRelative);
            fd.ShowDialog();
            RelativeGridView.TopRowIndex = topindex;
            RelativeGridView.FocusedRowHandle = old_row_id;
        }

        private void DeleteRelativeBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteRelative();
            LoadRelative();
        }

        void DeleteRelative()
        {
            var rows = GlobalFunctions.GridviewSelectedRow(RelativeGridView);

            if (rows.Count == 0)
            {
                GlobalProcedures.ShowWarningMessage("Silmək istədiyiniz qohumu seçin.");
                return;
            }

            if (GlobalFunctions.CallDialogResult("Seçilmiş qohumları silmək istəyirsiniz?", "Qohumların silinməsi") == DialogResult.Yes)
                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i] as DataRow;
                    RelativeCardDAL.DeleteCustomerRelative(Convert.ToInt32(row["ID"]), CustomerID.Value);
                    RelativeCardDAL.DeleteRelativePhone(Convert.ToInt32(row["PHONE_ID"]), CustomerID.Value, PhoneOwnerEnum.Relative);
                }
        }
        private void RelativeGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, RelativeCard_SS, e);
        }

        private void RelativeGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {

            DataRow row = RelativeGridView.GetFocusedDataRow();
            if (row != null)
                relativeID = Convert.ToInt32(row["ID"].ToString());
        }

        private void RelativeGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(RelativeGridView, RelativePopupMenu, e);
        }







        // DICTIONARIES -e aid olan hisse
        /// /////////
        /// /////////
        /// /////////

        private void LoadDictionaries(TransactionTypeEnum transaction, int index)
        {
            Dictionaries.FDictionaries fc = new Dictionaries.FDictionaries();
            fc.TransactionType = transaction;
            fc.ViewSelectedTabIndex = index;
            fc.RefreshList += new Dictionaries.FDictionaries.DoEvent(RefreshDictionaries);
            fc.ShowDialog();
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




        // LOOKUP -a aid olan hisse
        /// /////////
        /// /////////
        /// /////////

        private void CountryLookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                LoadDictionaries(TransactionTypeEnum.Update, 1);
        }

        private void SexLookUp_EditValueChanged(object sender, EventArgs e)
        {
            sexID = GlobalFunctions.GetLookUpID(sender);
        }

        private void BirthdayDate_EditValueChanged(object sender, EventArgs e)
        {

            AgeLabel.Text = GlobalFunctions.CalculationAgeWithYear(BirthdayDate.DateTime, DateTime.Today);

        }

        private void CountryLookUp_EditValueChanged(object sender, EventArgs e)
        {
            countryID = GlobalFunctions.GetLookUpID(sender);
        }
        




        // IMAGE -e aid olan hisse
        /// /////////
        /// /////////
        /// /////////
        
        private void UploadCustomerImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Müştərinin şəkilini seçin";
                dlg.Filter = "All files (*.jpeg;*.jpg;*.bmp;*.png)|*.jpeg;*.jpg;*.bmp;*.png|Image files (*.jpeg;*.jpg)|*.jpeg;*.jpg|Bmp files (*.bmp)|*.bmp|Png files (*.png)|*.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    PictureEdit.Image = new Bitmap(dlg.FileName);
                    CustomerImage = dlg.FileName;
                    DeleteCustomerImageButton.Enabled = true;
                }
                dlg.Dispose();
            }
        }

        private void DeleteCustomerImageButton_Click(object sender, EventArgs e)
        {
            PictureEdit.Image = null;
            CustomerImage = null;
            DeleteCustomerImageButton.Enabled = false;
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

            CustomerID = CustomerDAL.InsertCustomer(tran, customer);
        }

        private void UpdateCustomer(OracleTransaction tran)
        {
            isClickBOK = true;
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
                REGISTERED_ADDRESS = RegisteredAddressText.Text.Trim(),
                USED_USER_ID = -1,
                ID = CustomerID.Value
            };

            CustomerDAL.UpdateCustomer(tran, customer);
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlCardDetails())
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
                GlobalProcedures.ExecuteProcedureWithUser(tran, "ELMS_USER.PROC_INSERT_CUSTOMER_DATA", "P_CUSTOMER_ID", CustomerID.Value);
                //GlobalProcedures.ExecuteProcedureWithTwoParametrAndUser(tran,"ELMS_USER.PROC_INSERT_PHONE","P_OWNER_ID", CustomerID,"P_OWNER_TYPE",PhoneOwnerEnum.Customer);
                return 1;
            }, TransactionType == TransactionTypeEnum.Insert ? "Müştərinin məlumatları bazaya daxil edilmədi." : "Müştərinin məlumatları bazada dəyişdirilmədi.");
                this.Close();
            }
        }

        private bool ControlCardDetails()
        {

            bool b = false;

            if (NameText.Text.Length == 0)
            {
                NameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Müştərinin adı daxil edilməyib.");
                NameText.Focus();
                NameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (sexID == 0)
            {
                SexLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Cins seçilməyib.");
                SexLookUp.Focus();
                SexLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;
            
            if (String.IsNullOrEmpty(BirthdayDate.Text))
            {
                BirthdayDate.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Doğum tarixi daxil edilməyib.");
                BirthdayDate.Focus();
                BirthdayDate.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (BirthPlaceText.Text.Length == 0)
            {
                BirthPlaceText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Doğulduğu yer daxil edilməyib.");
                BirthPlaceText.Focus();
                BirthPlaceText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (countryID == 0)
            {
                CountryLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Ölkə seçilməyib.");
                CountryLookUp.Focus();
                CountryLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (RegisteredAddressText.Text.Length == 0)
            {
                RegisteredAddressText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Qeydiyyatda olduğu ünvanı daxil edilməyib.");
                RegisteredAddressText.Focus();
                RegisteredAddressText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;
            
            if (ActualAddressText.Text.Length == 0)
            {
                ActualAddressText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Faktiki yaşadığı ünvanı daxil edilməyib.");
                ActualAddressText.Focus();
                ActualAddressText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (PictureEdit == null)
            {
                PictureEdit.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Şəkil yeri boşdu.");
                PictureEdit.Focus();
                PictureEdit.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (PhoneGridView.RowCount == 0)
            {
                GlobalProcedures.ShowErrorMessage("Ən azı bir telefon nömrəsi daxil edilməlidir.");
                return false;
            }
            else
                b = true;

            if (DocumentGridView.RowCount == 0)
            {
                GlobalProcedures.ShowErrorMessage("Ən azı bir sənəd daxil edilməlidir.");
                return false;
            }
            else
                b = true;

            return b;
        }

        private void FCustomerAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CUSTOMER", -1, "WHERE ID = " + CustomerID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            CustomerDAL.DeleteCustomerData(CustomerID.Value);

            if (TransactionType == TransactionTypeEnum.Insert)
                CustomerID = 0;
            CustomerDAL.DeleteCustomerData(CustomerID.Value);

            this.RefreshDataGridView();
        }

    }
}