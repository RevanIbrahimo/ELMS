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
using DevExpress.Data;
using DevExpress.XtraGrid;

namespace ELMS.Forms.Order
{
    public partial class FConfirmAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FConfirmAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public int? OrderID, CustomerID = 1;

        bool CurrentStatus = false, Used = false, isClickBOK = false;

        int UsedUserID = -1, orderID,
            documentID, topindex,
            old_row_id, customerID,            
            branchID = 0,
            sourceID = 0,
            timeID = 0;
        decimal calcTotalPrice = 0;
        string OrderImage, pinCode;
        string UserImagePath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Images";

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;


        List<CustomerImage> lstImage = new List<CustomerImage>();

        private void RefreshDocumentBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadProduct();
        }

        private void NewDocumentBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFDocumentAddEdit(TransactionTypeEnum.Insert, null);
        }

        private void EditDocumentBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateDocument();
        }

        private void LoadProduct()
        {
            ProductGridControl.DataSource = ProductCardDAL.SelectViewData(null);
            DataTable dt = ProductCardDAL.SelectTotal(null);

            OrderAmountValue.EditValue = calcTotalPrice;
            //if (dt.Rows.Count > 0)
            //{
            //    OrderAmountValue.EditValue = Convert.ToDecimal(dt.Rows[0]["ORDER_AMOUNT"].ToString());
            //}
        }

        private void LoadFDocumentAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = ProductGridView.TopRowIndex;
            old_row_id = ProductGridView.FocusedRowHandle;
            FProductCardAddEdit fd = new FProductCardAddEdit()
            {
                TransactionType = transactionType,
                OrderID = OrderID,
                CardID = id
            };
            fd.RefreshDataGridView += new FProductCardAddEdit.DoEvent(LoadProduct);
            fd.ShowDialog();
            ProductGridView.TopRowIndex = topindex;
            ProductGridView.FocusedRowHandle = old_row_id;
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

        private void LoadOrderDetails()
        {
            DataTable dt = OrderDAL.SelectViewData(OrderID);

            if (dt.Rows.Count > 0)
            {
                pinCode = dt.Rows[0]["PINCODE"].ToString();
                RegisterCodeText.EditValue = dt.Rows[0]["ID"];
                NoteText.EditValue = dt.Rows[0]["NOTE"];
                OrderDate.EditValue = dt.Rows[0]["ORDER_DATE"];
                if (OrderDate.DateTime == DateTime.MinValue)
                    OrderDate.EditValue = null;
                GlobalProcedures.LookUpEditValue(BranchLookUp, dt.Rows[0]["BRANCH_NAME"].ToString());
                GlobalProcedures.LookUpEditValue(SourceLookUp, dt.Rows[0]["ORDER_SOURCE"].ToString());
                FirstPaymentValue.EditValue = Convert.ToDecimal(dt.Rows[0]["FIRST_PAYMENT"].ToString());
                OrderAmountValue.EditValue = Convert.ToDecimal(dt.Rows[0]["ORDER_AMOUNT"].ToString());
                GlobalProcedures.LookUpEditValue(TimeLookUp, dt.Rows[0]["TIME"].ToString());
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
            }
        }
        private void InsertTemps()
        {
            if (TransactionType == TransactionTypeEnum.Insert)
                return;
            GlobalProcedures.ExecuteProcedureWithUser("ELMS_USER_TEMP.PROC_INSERT_PRODUCT_CARDS_TEMP", "P_ORDER_TAB_ID", OrderID, "Müştərinin məlumatları temp cədvələ daxil edilmədi.");

        }
        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                PhoneAllText.Enabled =
                BOK.Visible = !status;
        }

        private void ProductGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow row = ProductGridView.GetFocusedDataRow();
            if (row != null)
                documentID = Convert.ToInt32(row["ID"].ToString());
        }

        private void InsertOrder(OracleTransaction tran)
        {
            Class.Tables.Order order = new Class.Tables.Order
            {
                CUSTOMER_ID = CustomerID.Value,
                BRANCH_ID = branchID,
                ORDER_DATE = OrderDate.DateTime,
                SOURCE_ID = sourceID,
                TIME_ID = timeID,
                FIRST_PAYMENT = FirstPaymentValue.Value,
                ORDER_AMOUNT = OrderAmountValue.Value,
                NOTE = NoteText.Text.Trim()
            };

            OrderID = OrderDAL.InsertOrder(tran, order);
        }

        private void UpdateOrder(OracleTransaction tran)
        {
            isClickBOK = true;
            Class.Tables.Order order = new Class.Tables.Order
            {
                CUSTOMER_ID = CustomerID.Value,
                BRANCH_ID = branchID,
                ORDER_DATE = OrderDate.DateTime,
                SOURCE_ID = sourceID,
                TIME_ID = timeID,
                FIRST_PAYMENT = FirstPaymentValue.Value,
                ORDER_AMOUNT = OrderAmountValue.Value,
                NOTE = NoteText.Text.Trim(),
                USED_USER_ID = -1,
                ID = OrderID.Value
            };

            OrderDAL.UpdateOrder(tran, order);
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlCardDetails())
            {
                GlobalFunctions.RunInOneTransaction<int>(tran =>
                {

                    if (TransactionType == TransactionTypeEnum.Insert)
                    {
                        InsertOrder(tran);
                    }
                    else
                    {
                        UpdateOrder(tran);
                    }
                    GlobalProcedures.ExecuteProcedureWithParametr(tran, "ELMS_USER.PROC_INSERT_PRODUCT_CARDS", "P_CUSTOMER_ID", OrderID.Value);


                    return 1;
                }, TransactionType == TransactionTypeEnum.Insert ? "Müştərinin məlumatları bazaya daxil edilmədi." : "Müştərinin məlumatları bazada dəyişdirilmədi.");
                this.Close();
            }
        }

        private bool ControlCardDetails()
        {

            bool b = false;

            if (CustomerID.Value == 0)
            {
                FinCodeSearch.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Müştəri seçilməyib.");
                FinCodeSearch.Focus();
                FinCodeSearch.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;
        }

        void RefreshDictionaries(int index)
        {
            switch (index)
            {
                case 1:
                    GlobalProcedures.FillLookUpEdit(SourceLookUp, FundsSourcesDAL.SelectFundsSourcesByID(null).Tables[0]);
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

        private void FinCodeSearch_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                LoadFCustomerAddEdit(TransactionTypeEnum.Insert, null);
            else if (e.Button.Index == 2)
                LoadFCustomerAddEdit(TransactionTypeEnum.Update, CustomerID);
        }

        void CalcTotalAmount()
        {
            TotalOrderAmountValue.EditValue = OrderAmountValue.Value - FirstPaymentValue.Value;
        }

        private void FirstPaymentValue_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalAmount();
        }

        void DeleteDocument()
        {
            var rows = GlobalFunctions.GridviewSelectedRow(ProductGridView);

            if (rows.Count == 0)
            {
                GlobalProcedures.ShowWarningMessage("Silmək istədiyiniz məhsulu seçin.");
                return;
            }

            if (GlobalFunctions.CallDialogResult("Seçilmiş məhsulları silmək istəyirsiniz?", "Məhsulların silinməsi") == DialogResult.Yes)
                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i] as DataRow;
                    ProductCardDAL.DeleteProductCard(Convert.ToInt32(row["ID"]), OrderID.Value);
                }
        }

        private void DeleteProductBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteDocument();
            LoadProduct();
        }


        private void ProductGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Product_SS, e);
        }

        private void ProductGridView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridView currentView = sender as GridView;
            if (currentView.RowCount == 0)
                return;

            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {                
                if (((GridSummaryItem)e.Item).FieldName.CompareTo("Product_TotalPrice") == 0) //qaliq
                    calcTotalPrice = 0;
            }

            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                if (((GridSummaryItem)e.Item).FieldName.CompareTo("Product_TotalPrice") == 0)
                    calcTotalPrice += Convert.ToDecimal(e.FieldValue);
            }

            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                if (((GridSummaryItem)e.Item).FieldName.CompareTo("Product_TotalPrice") == 0) //verilen              
                    e.TotalValue = calcTotalPrice;
            }
        }

        private void BranchLookUp_EditValueChanged(object sender, EventArgs e)
        {
            branchID = GlobalFunctions.GetLookUpID(sender);
        }

        private void NewCustomerButton_Click(object sender, EventArgs e)
        {

        }

        private void LoadFCustomerAddEdit(TransactionTypeEnum transaction, int? id)
        {

            Customer.FCustomerAddEdit fc = new Customer.FCustomerAddEdit()
            {
                TransactionType = transaction,
                CustomerID = id
            };
            fc.RefreshDataGridView += new Customer.FCustomerAddEdit.DoEvent(LoadCustomerDetails);
            fc.ShowDialog();
        }

        private void EditCustomerLabel_Click(object sender, EventArgs e)
        {

        }

        private void FinCodeSearch_Click(object sender, EventArgs e)
        {

        }

        private void SourceLookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                LoadDictionaries(TransactionTypeEnum.Update, 1);
        }

        private void FinCodeSearch_EditValueChanged(object sender, EventArgs e)
        {
            pinCode = ("0" + FinCodeSearch.Text.Trim());
            LoadCustomerDetails();
        }

        private void LoadCustomerDetails()
        {
            DataTable dt = CustomerDAL.SelectCustomerData(pinCode);
            if (FinCodeSearch.Text.Length != 7)
            {
                NameText.Text =
                    ActualAddressText.Text =
                    PhoneAllText.Text = null;
                CustomerID = 0;
                PictureEdit.Image = null;
            }

            if (dt.Rows.Count > 0)
            {
                NameText.EditValue = dt.Rows[0]["FULL_NAME"];
                ActualAddressText.EditValue = dt.Rows[0]["ADDRESS"];
                PhoneAllText.EditValue = dt.Rows[0]["PHONE"];
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
                CustomerID = Convert.ToInt32(dt.Rows[0]["ID"]);
                string str = dt.Rows[0]["PINCODE"].ToString();
                char[] result;
                string fin = "";
                // copies str to result
                result = str.ToCharArray();

                // prints result
                for (int i = 1; i < result.Length; i++)
                {
                    fin = fin + (result[i] + "").ToString();
                }
                FinCodeSearch.EditValue = fin;

                LoadImage();
            }
        }

        private void TimeLookUp_EditValueChanged(object sender, EventArgs e)
        {
            timeID = GlobalFunctions.GetLookUpID(sender);
        }

        private void SourceLookUp_EditValueChanged(object sender, EventArgs e)
        {
            sourceID = GlobalFunctions.GetLookUpID(sender);
        }

        private void FOrderAddEdit_Load(object sender, EventArgs e)
        {
            GlobalProcedures.FillLookUpEdit(BranchLookUp, BranchDAL.SelectBranchByID(null).Tables[0]);
            GlobalProcedures.FillLookUpEdit(SourceLookUp, FundsSourcesDAL.SelectFundsSourcesByID(null).Tables[0]);
            GlobalProcedures.FillLookUpEdit(TimeLookUp, TimesDAL.SelectTimesByID(null).Tables[0]);
            RefreshDictionaries(1);

            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Müraciətin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.ORDER_TAB", GlobalVariables.V_UserID, "WHERE ID = " + OrderID + " AND USED_USER_ID = -1");
                LoadOrderDetails();
                LoadCustomerDetails();
                //LoadImage();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş müraciətə hal-hazırda " + used_user_name + " tərəfindən düzəliş edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
            {
                this.Text = "Müraciətin əlavə edilməsi";
                OrderDate.DateTime = DateTime.Today;
            }
            InsertTemps();
            LoadProduct();
            //LoadPhone();
        }

        private void FOrderAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.ORDER_TAB", -1, "WHERE ID = " + OrderID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            OrderDAL.DeleteOrder(OrderID.Value);

            this.RefreshDataGridView();
        }
    }
}