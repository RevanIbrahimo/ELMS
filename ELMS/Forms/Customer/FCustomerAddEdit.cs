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
            old_row_id
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
    }
}