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
using ELMS.Forms.Customer;

namespace ELMS.Forms.Order
{
    public partial class FConfirmAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FConfirmAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public int? OrderID, CustomerID = 1, OperationID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;

        int UsedUserID = -1, orderID,
            documentID, topindex,
            old_row_id, customerID, relativeID,
            branchID = 0,
            sourceID = 0,
            timeID = 0;
        decimal calcTotalPrice = 0;
        string OrderImage, pinCode;
        string UserImagePath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Images";

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;


        List<CustomerImage> lstImage = new List<CustomerImage>();
        
        private void LoadProduct()
        {
            ProductGridControl.DataSource = ProductCardDAL.SelectViewData(null);
            DataTable dt = ProductCardDAL.SelectTotal(null);
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
                OrderDateText.EditValue = dt.Rows[0]["ORDER_DATE"];
                SourceText.EditValue = dt.Rows[0]["ORDER_SOURCE"];
                BranchText.EditValue = dt.Rows[0]["BRANCH_NAME"];
                TimeText.EditValue = dt.Rows[0]["TIME"];
                FirstPaymentValue.EditValue = Convert.ToDecimal(dt.Rows[0]["FIRST_PAYMENT"].ToString());
                OrderAmountValue.EditValue = Convert.ToDecimal(dt.Rows[0]["ORDER_AMOUNT"].ToString());
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
            }
        }
        private void InsertTemps()
        {
            if (TransactionType == TransactionTypeEnum.Insert)
                return;
            GlobalProcedures.ExecuteProcedureWithUser("ELMS_USER_TEMP.PROC_INSERT_PRODUCT_CARDS_TEMP", "P_ORDER_TAB_ID", OrderID, "Müştərinin məlumatları temp cədvələ daxil edilmədi.");
            GlobalProcedures.ExecuteProcedureWithUser("ELMS_USER_TEMP.PROC_INSERT_CUSTOM_TEMP_DATA", "P_CUSTOMER_ID", CustomerID, "Müştərinin məlumatları temp cədvələ daxil edilmədi.");

        }
        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                PhoneAllText.Enabled =
                BClose.Visible = !status;
        }
        
        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlCardDetails())
            {
                    if (TransactionType == TransactionTypeEnum.Update)
                    {
                        OrderOperation order = new OrderOperation
                        {
                            ORDER_ID = OrderID.Value,
                            ID = OperationID.Value,
                            OPERATION_ID = (int)OperationTypeEnum.Tesdiq_edildi
                        };
                        
                        OperationDAL.UpdateOrderOperation(order);
                    }
                    GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER.PROC_INSERT_PRODUCT_CARDS", "P_CUSTOMER_ID", OrderID.Value, "Müraciət məlumatları təsdiq edilmədi.");
                    
                this.Close();
            }
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            if (ControlCardDetails())
            {
                if (TransactionType == TransactionTypeEnum.Update)
                {
                    OrderOperation order = new OrderOperation
                    {
                        ORDER_ID = OrderID.Value,
                        ID = OperationID.Value,
                        OPERATION_ID = (int)OperationTypeEnum.Tesdiq_edilmedi
                    };

                    OperationDAL.UpdateOrderOperation(order);
                }
                GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER.PROC_INSERT_PRODUCT_CARDS", "P_CUSTOMER_ID", OrderID.Value, "Müraciət məlumatları təsdiq edilmədi.");

                this.Close();
            }
        }

        private void BClose_Click(object sender, EventArgs e)
        {

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

        void CalcTotalAmount()
        {
            TotalOrderAmountValue.EditValue = OrderAmountValue.Value - FirstPaymentValue.Value;
        }

        private void FirstPaymentValue_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalAmount();
        }
        
        private void ProductGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, CustomerProduct_SS, e);
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
                RegisterAddressText.EditValue = dt.Rows[0]["REGISTERED_ADDRESS"];
                PhoneAllText.EditValue = dt.Rows[0]["PHONE"];
                BranchCustomerText.EditValue = dt.Rows[0]["BRANCH_NAME"];
                CountryText.EditValue = dt.Rows[0]["COUNTRY_NAME"];
                SexText.EditValue = dt.Rows[0]["SEX_NAME"];
                BirthdayText.EditValue = dt.Rows[0]["BIRTHDAY"];
                BirthPlaceText.EditValue = dt.Rows[0]["BIRTH_PLACE"];
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
        
        private void FOrderAddEdit_Load(object sender, EventArgs e)
        {

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
                //OrderDate.DateTime = DateTime.Today;
            }
            InsertTemps();
            LoadProduct();
            LoadDocument();
            LoadPhone();
            LoadWork();
            LoadRelative();
        }

        private void FOrderAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.ORDER_TAB", -1, "WHERE ID = " + OrderID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            OrderDAL.DeleteOrder(OrderID.Value);

            this.RefreshDataGridView();
        }

        private void OtherInfoTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (OtherInfoTabControl.SelectedTabPageIndex)
            {
                case 0:
                    {
                        LoadProduct();
                    }
                    break;
                case 1:
                    {
                        LoadDocument();
                        LoadPhone();
                        LoadWork();
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
            DocumentGridControl.DataSource = CustomerCardDAL.SelectViewDataAll(CustomerID);
        }
        
        private void DocumentGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, CustomerDocument_SS, e);
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
            
        }

        private void PhoneGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Phone_SS, e);
        }





        // WORK -e aid olan hisse
        /// /////////
        /// /////////
        /// /////////

        private void LoadWork()
        {
            WorkGridControl.DataSource = CustomerWorkDAL.SelectViewDataAll(CustomerID);
        }

        private void WorkGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Work_SS, e);
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

            EditRelativeBarButton.Enabled = RelativeGridView.RowCount > 0;
        }

        private void RefreshRelativeBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadRelative();
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

        private void RelativeGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {

            DataRow row = RelativeGridView.GetFocusedDataRow();
            if (row != null)
                relativeID = Convert.ToInt32(row["ID"].ToString());
        }

        private void RelativeGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Relative_SS, e);
        }



    }
}