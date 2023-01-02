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
using ELMS.Forms.Customer;
using ELMS.Forms.Order;

namespace ELMS.UserControls
{
    public partial class ConfirmationUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public ConfirmationUserControl()
        {
            InitializeComponent();
        }
        int topindex, old_row_id, orderID;
        string orderName;

        private void OrderUserControl_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        private void OrderUserControl_Enter(object sender, EventArgs e)
        {
            //LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            OrderGridControl.DataSource = OrderDAL.SelectViewData(null);

            //EnabledButton();
        }

        void EnabledButton()
        {
            if (OrderGridView.RowCount > 0)
            {
                EditBarButton.Enabled = true;
                DeleteBarButton.Enabled = true;
                HistroryBarButton.Enabled = true;
            }
            else
                EditBarButton.Enabled = DeleteBarButton.Enabled = HistroryBarButton.Enabled = false;
        }

        //void RefreshCustomer(string customerName)
        //{
        //    LoadCustomerData();
        //}

         void RefreshCustomer()
        {
            LoadCustomerData();
        }

        private void LoadFConfirmAddEdit(TransactionTypeEnum transaction, int? id)
        {
            topindex = OrderGridView.TopRowIndex;
            old_row_id = OrderGridView.FocusedRowHandle;
            FConfirmAddEdit fc = new FConfirmAddEdit()
            {
                TransactionType = transaction,
                OrderID = id,
            };
            fc.RefreshDataGridView += new FConfirmAddEdit.DoEvent(RefreshCustomer);
            fc.ShowDialog();
            OrderGridView.TopRowIndex = topindex;
            OrderGridView.FocusedRowHandle = old_row_id;
        }

        private void RefreshBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadCustomerData();
        }

        private void OrderGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            EnabledButton();
        }

        void UpdateCustomer()
        {
            LoadFConfirmAddEdit(TransactionTypeEnum.Update, orderID);
        }

        private void EditBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (EditBarButton.Enabled)
                UpdateCustomer();
        }

        private void OrderGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(OrderGridView, OrderPopupMenu, e);
        }

        private void OrderGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Customer_SS, e);
        }

        private void OrderGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditBarButton.Enabled)
                UpdateCustomer();
        }

        private void PrintBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ShowGridPreview(OrderGridControl);
        }

        void DeleteCustomer()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(OrderGridView, "USED_USER_ID"));
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

        private void OrderGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);
        }

        private void LoadFScheduleAddEdit()
        {
            topindex = OrderGridView.TopRowIndex;
            old_row_id = OrderGridView.FocusedRowHandle;
            
            OrderGridView.TopRowIndex = topindex;
            OrderGridView.FocusedRowHandle = old_row_id;
        }

        private void ScheduleBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFScheduleAddEdit();
        }

        private void OrderGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            orderID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
        }

        private void HistroryBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        

        private void NewBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFConfirmAddEdit(TransactionTypeEnum.Insert, null);
        }
    }
}
