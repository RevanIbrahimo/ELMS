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
using ELMS.Class;
using ELMS.Class.DataAccess;
using ELMS.Class.Tables;

namespace ELMS.Forms.Order
{
    public partial class FProductCardAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FProductCardAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public int? OrderID = 1;
        public int? CardID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        decimal countValue = 2;
        int productID = 0;
        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1;

        private void FCardAddEdit_Load(object sender, EventArgs e)
        {
            GlobalProcedures.FillLookUpEdit(ProductLookUp, ProductDAL.SelectProductByID(null).Tables[0]);
            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Sifarişlərin düzəliş edilməsi";                
                LoadDetails();                
            }
            else
                this.Text = "Sifarişin əlavə edilməsi";
        }

        private void LoadDetails()
        {
            DataTable dt = ProductCardDAL.SelectViewData(OrderID);

            if (dt.Rows.Count > 0)
            {
                NoteText.EditValue = dt.Rows[0]["NOTE"];
                PriceValue.EditValue = Convert.ToDecimal(dt.Rows[0]["PRICE"].ToString());
                CountValue.EditValue = Convert.ToDecimal(dt.Rows[0]["PRODUCT_COUNT"].ToString());
                GlobalProcedures.LookUpEditValue(ProductLookUp, dt.Rows[0]["PRODUCT_NAME"].ToString());
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
            }
        }

        private void ComponentEnabled(bool status)
        {
            NoteText.Enabled =
            BOK.Visible = !status;
        }

        private void InsertDetail()
        {
            ProductCard productCard = new ProductCard
            {
                PRICE = PriceValue.Value,
                PRODUCT_COUNT = CountValue.Value,
                TOTAL = TotalPriceValue.Value,
                PRODUCT_ID = productID,
                ORDER_TAB_ID = 0,
                NOTE = NoteText.Text.Trim()
            };
            ProductCardDAL.InsertProductCard(productCard);
        }

        private void UpdateDetail()
        {
            isClickBOK = true;

            ProductCard productCard = new ProductCard
            {
                PRICE = PriceValue.Value,
                PRODUCT_COUNT = CountValue.Value,
                TOTAL = TotalPriceValue.Value,
                NOTE = NoteText.Text.Trim(),
                ORDER_TAB_ID = OrderID.Value,
                PRODUCT_ID = productID,
                ID = CardID.Value,
                USED_USER_ID = -1,
                IS_CHANGE = (int)ChangeTypeEnum.Change
            };

            ProductCardDAL.UpdateProductCard(productCard);
        }

        private void FCardAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.PRODUCT_CARDS", -1, "WHERE ID = " + CardID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        void RefreshDictionaries(int index)
        {
            switch (index)
            {
                case 0:
                    GlobalProcedures.FillLookUpEdit(ProductLookUp, DocumentTypeDAL.SelectDocumentTypeByID(null).Tables[0]);
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

        private void ProductLookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                LoadDictionaries(TransactionTypeEnum.Update, 0);

        }

        void CalcTotalPrice()
        {
            
            TotalPriceValue.EditValue = CountValue.Value * PriceValue.Value;
        }

        private void CountValue_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalPrice();
        }

        private void ProductLookUp_EditValueChanged(object sender, EventArgs e)
        {
            productID = GlobalFunctions.GetLookUpID(sender);
        }

        private bool ControlCardDetails()
        {
            bool b = false;

            if (productID == 0)
            {
                ProductLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sifariş edilən məhsul seçilməyib.");
                ProductLookUp.Focus();
                ProductLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (CountValue.Value <= 0)
            {
                CountValue.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Say daxil edilməyib.");
                CountValue.Focus();
                CountValue.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (PriceValue.Value <= 0)
            {
                PriceValue.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Qiymət daxil edilməyib.");
                PriceValue.Focus();
                PriceValue.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlCardDetails())
            {
                if (TransactionType == TransactionTypeEnum.Insert)
                    InsertDetail();
                else
                    UpdateDetail();
                this.Close();
            }
        }
    }
}