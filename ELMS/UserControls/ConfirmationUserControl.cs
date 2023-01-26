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
        int topindex, old_row_id, orderID, operationID, typeID;

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
            OrderGridControl.DataSource = OrderDAL.SelectConfirmData(null);

            //EnabledButton();
        }

        void EnabledButton()
        {
            EditBarButton.Enabled = OrderGridView.RowCount > 0;
        }

        //void RefreshCustomer(string customerName)
        //{
        //    LoadCustomerData();
        //}

        void RefreshCustomer()
        {
            LoadCustomerData();
        }

        private void LoadFConfirmAddEdit(TransactionTypeEnum transaction, UserControlTypeEnum userControl, int? id)
        {
            topindex = OrderGridView.TopRowIndex;
            old_row_id = OrderGridView.FocusedRowHandle;
            FConfirmAddEdit fc = new FConfirmAddEdit()
            {
                TransactionType = transaction,
                UserControlType = userControl,
                OrderID = id,
                OperationID = operationID
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

        void UpdateConfirm()
        {
            LoadFConfirmAddEdit(TransactionTypeEnum.Update, UserControlTypeEnum.Confirm, orderID);
        }

        private void EditBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (EditBarButton.Enabled)
                UpdateConfirm();
        }

        private void OrderGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(OrderGridView, OrderPopupMenu, e);
        }

        private void OrderGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Order_SS, e);
        }

        private void OrderGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditBarButton.Enabled)
                UpdateConfirm();
        }

        private void PrintBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ShowGridPreview(OrderGridControl);
        }

        private void OrderGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if ((e.RowHandle >= 0) && (int.Parse(OrderGridView.GetRowCellDisplayText(e.RowHandle, OrderGridView.Columns["TYPE_ID"])) == 1))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
            else if ((e.RowHandle >= 0) && (int.Parse(OrderGridView.GetRowCellDisplayText(e.RowHandle, OrderGridView.Columns["TYPE_ID"])) == 3))
            {
                e.Appearance.BackColor = Color.Gray;
            }

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
            operationID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "OPERATION_ID"));
            typeID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "TYPE_ID"));
        }


        private void ExcelBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.GridExportToFile(OrderGridControl, "xls");
        }

        private void PdfBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.GridExportToFile(OrderGridControl, "pdf");
        }

        private void RtfBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.GridExportToFile(OrderGridControl, "rtf");
        }

        private void OrderGridView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            GlobalProcedures.GridCustomDrawFooterCell(Order_Amount, HorzalignmentEnum.Right, e);
            GlobalProcedures.GridCustomDrawFooterCell(Order_FirstPayment, HorzalignmentEnum.Right, e);
            GlobalProcedures.GridCustomDrawFooterCell(Order_CreditAmount, HorzalignmentEnum.Right, e);
        }

        private void HtmlBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.GridExportToFile(OrderGridControl, "html");
        }

        private void TxtBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.GridExportToFile(OrderGridControl, "txt");
        }

        private void CsvBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.GridExportToFile(OrderGridControl, "csv");
        }

        private void MhtBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.GridExportToFile(OrderGridControl, "mht");
        }
    }
}
