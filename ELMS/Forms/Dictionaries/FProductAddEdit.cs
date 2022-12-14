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
using ELMS.Class;
using ELMS.Class.Tables;
using ELMS.Class.DataAccess;
using static ELMS.Class.Enum;


namespace ELMS.Forms.Dictionaries
{
    public partial class FProductAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FProductAddEdit()
        {
            InitializeComponent();
        }
        
        public TransactionTypeEnum TransactionType;
        public int? ProductID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void FProductAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CARD_ISSUING", -1, "WHERE ID = " + ProductID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        private void FProductAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Məhsulun düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CARD_ISSUING", GlobalVariables.V_UserID, "WHERE ID = " + ProductID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş Məhsul hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
                this.Text = "Məhsulun əlavə edilməsi";            
        }

        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                NoteText.Enabled =
                BOK.Visible = !status;
        }

        private void LoadDetails()
        {       
            List<Product> lstProduct = ProductDAL.SelectProductByID(ProductID).ToList<Product>();
            if (lstProduct.Count > 0)
            {
                var product = lstProduct.LastOrDefault();
                NameText.EditValue = product.NAME;
                NoteText.EditValue = product.NOTE;
                UsedUserID = product.USED_USER_ID;
                orderID = product.ORDER_ID;
            }
        }

        private bool ControlDetail()
        {
            bool b = false;

            if (NameText.Text.Length == 0)
            {
                NameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Məhsulun adı daxil edilməyib.");
                NameText.Focus();
                NameText.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;
           

            return b;            
        }

        private void InsertDetail()
        {
            Product product = new Product
            {
                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim()
            };
            ProductDAL.InsertProduct(product);            
        }

        private void UpdateDetail()
        {
            isClickBOK = true;

            Product product = new Product
            {
                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                ID = ProductID.Value,
                ORDER_ID = orderID,
                USED_USER_ID = -1
            };

            ProductDAL.UpdateProduct(product);
        }        

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlDetail())
            {
                if (TransactionType == TransactionTypeEnum.Insert)
                    InsertDetail();
                else
                    UpdateDetail();
                this.Close();
            }            
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}