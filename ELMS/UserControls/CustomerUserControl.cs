using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ELMS.Class.DataAccess;
using ELMS.Class;
using DevExpress.XtraGrid.Views.Grid;
using static ELMS.Class.Enum;
using ELMS.Class.Tables;

namespace ELMS.UserControls
{
    public partial class CustomerUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public CustomerUserControl()
        {
            InitializeComponent();
        }
        int topindex, old_row_id, customerID;
        string customerName;

        private void CustomerUserControl_Load(object sender, EventArgs e)
        {
            
        }

        private void CustomerUserControl_Enter(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            //CustomerGridControl.DataSource = CustomerDAL.SelectViewData(null);

            EnabledButton();
        }

        void EnabledButton()
        {
            if (CustomerGridView.RowCount > 0)
            {
                EditBarButton.Enabled = true;
                DeleteBarButton.Enabled = true;
                HistroryBarButton.Enabled = true;
            }
            else
                EditBarButton.Enabled = DeleteBarButton.Enabled = HistroryBarButton.Enabled = false;
        }

        void RefreshCustomer(string customerName)
        {
            LoadCustomerData();
        }

        private void LoadFCustomerAddEdit(TransactionTypeEnum transaction, int? id)
        {
            topindex = CustomerGridView.TopRowIndex;
            old_row_id = CustomerGridView.FocusedRowHandle;
            //Forms.Cards.FCardAddEdit fc = new Forms.Cards.FCardAddEdit()
            //{
            //    TransactionType = transaction,
            //    CustomerID = id
            //};
            //fc.RefreshDataGridView += new Forms.Cards.FCardAddEdit.DoEvent(RefreshCustomer);
            //fc.ShowDialog();
            CustomerGridView.TopRowIndex = topindex;
            CustomerGridView.FocusedRowHandle = old_row_id;
        }

        private void RefreshBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadCustomerData();
        }

        private void CustomerGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            EnabledButton();
        }

        void UpdateCustomer()
        {
            LoadFCustomerAddEdit(TransactionTypeEnum.Update, customerID);
        }

        private void EditBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (EditBarButton.Enabled)
                UpdateCustomer();
        }

        private void CustomerGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(CustomerGridView, CustomerPopupMenu, e);
        }

        private void CustomerGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Customer_SS, e);
        }

        private void CustomerGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditBarButton.Enabled)
                UpdateCustomer();
        }

        private void PrintBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ShowGridPreview(CustomerGridControl);
        }

        void DeleteCustomer()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(CustomerGridView, "USED_USER_ID"));
            if (UsedUserID < 0)
            {
                
            }
            else
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                GlobalProcedures.ShowWarningMessage($@"Seçilmiş məlumat hal-hazırda {used_user_name} tərəfindən istifadə ediliyi üçün silinə bilməz.");
            }
        }

        private void DeleteBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteCustomer();
            LoadCustomerData();
        }

        private void CustomerGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);
        }

        private void LoadFScheduleAddEdit()
        {
            topindex = CustomerGridView.TopRowIndex;
            old_row_id = CustomerGridView.FocusedRowHandle;
            //Forms.Schedules.FAppointmentAddEdit fAppointmentAddEdit = new Forms.Schedules.FAppointmentAddEdit
            //{
            //    TransactionType = TransactionTypeEnum.Insert,
            //    AppointmentID = null,
            //    CustomerName = customerName,
            //};
            //fAppointmentAddEdit.RefreshDataGridView += new Forms.Schedules.FAppointmentAddEdit.DoEvent(LoadCustomerData);
            //fAppointmentAddEdit.ShowDialog();
            CustomerGridView.TopRowIndex = topindex;
            CustomerGridView.FocusedRowHandle = old_row_id;
        }

        private void ScheduleBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFScheduleAddEdit();
        }

        private void HistroryBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Forms.Customers.FCustomerHistory fCustomerHistory = new Forms.Customers.FCustomerHistory
            //{
            //    CustomerID = customerID
            //};
            //fCustomerHistory.ShowDialog();
        }

        private void CustomerGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            customerID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            customerName = GlobalFunctions.GetGridRowCellValue((sender as GridView), "NAME").ToString();
        }

        private void NewBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFCustomerAddEdit(TransactionTypeEnum.Insert, null);
        }
    }
}
