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
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ELMS.UserControls
{
    public partial class OrderUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public OrderUserControl()
        {
            InitializeComponent();
        }
        int topindex, old_row_id, orderID, typeID,code_number;
        string orderName;

        private void OrderUserControl_Load(object sender, EventArgs e)
        {
            LoadOrderData();
        }

        private void OrderUserControl_Enter(object sender, EventArgs e)
        {
            //LoadCustomerData();
        }

        private void LoadOrderData()
        {
            OrderGridControl.DataSource = OrderDAL.SelectConfirmData(null);

            //EnabledButton();
        }

        void EnabledButton()
        {
            if (OrderGridView.RowCount > 0)
            {
                EditBarButton.Enabled = true;
                DeleteBarButton.Enabled = true;
            }
            else
                EditBarButton.Enabled = DeleteBarButton.Enabled = false;
        }

        //void RefreshCustomer(string customerName)
        //{
        //    LoadCustomerData();
        //}

         void RefreshCustomer()
        {
            LoadOrderData();
        }

        private void LoadFCustomerAddEdit(TransactionTypeEnum transaction, int? id)
        {
            topindex = OrderGridView.TopRowIndex;
            old_row_id = OrderGridView.FocusedRowHandle;
            FOrderAddEdit fc = new FOrderAddEdit()
            {
                TransactionType = transaction,
                OrderID = id,
            };
            fc.RefreshDataGridView += new FOrderAddEdit.DoEvent(RefreshCustomer);
            fc.ShowDialog();
            OrderGridView.TopRowIndex = topindex;
            OrderGridView.FocusedRowHandle = old_row_id;
        }

        private void RefreshBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadOrderData();
        }
        
        void UpdateCustomer()
        {
            LoadFCustomerAddEdit(TransactionTypeEnum.Update, orderID);
        }

        private void EditBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (EditBarButton.Enabled)
                UpdateCustomer();
        }

        private void OrderGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            EnabledButton();
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
                UpdateCustomer();
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

        private void OrderGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            orderID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
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
            LoadOrderData();
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

        private void ViewFileBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFile();
        }

        private void LoadFile()
        {
            DataTable dt = GlobalFunctions.GenerateDataTable($@"SELECT T.DOCUMENT_FILE FROM ELMS_USER.ORDER_DOCUMENTS T WHERE T.ORDER_ID = {orderID}", this.Name + "/LoadFile");

            if (dt == null)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                if (!DBNull.Value.Equals(dr["DOCUMENT_FILE"]))
                {
                    Byte[] BLOBData = (byte[])dr["DOCUMENT_FILE"];
                    MemoryStream stmBLOBData = new MemoryStream(BLOBData);
                    // code_number = int.Parse(Regex.Replace(RegisterCodeText.Text, "[^0-9]", ""));
                    GlobalProcedures.DeleteFile(GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents\\" + orderID + "_Müqavilə.pdf");
                    FileStream fs = new FileStream(GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents\\" + orderID + "_Müqavilə.pdf", FileMode.Create, FileAccess.Write);
                    stmBLOBData.WriteTo(fs);
                    fs.Close();
                    stmBLOBData.Close();
                    Process.Start(GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents\\" + orderID + "_Müqavilə.pdf");
                }
            }
        }

        private void MhtBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.GridExportToFile(OrderGridControl, "mht");
        }

        private void HistroryBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void NewBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFCustomerAddEdit(TransactionTypeEnum.Insert, null);
        }
    }
}
