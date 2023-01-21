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
using System.IO;
using System.Diagnostics;

namespace ELMS.Forms.Order
{
    public partial class FOrderAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FOrderAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public NewOrderEnum NewOrder;
        public int? OrderID, CustomerID, EnumID;

        bool CurrentStatus = false, Used = false, isClickBOK = false,
            contract_click = false;

        int UsedUserID = -1, orderOperationID,
            productID, topindex,
            old_row_id,
            branchID = 0,
            sourceID = 0,
            timeID = 0,
            ContractID,
            credit_currency_id = 0,
            customer_type_id = 1;
        decimal calcTotalPrice = 0;
        string pinCode,
            credit_currency_name = "null";
        string UserImagePath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Images";

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;


        List<CustomerImage> lstImage = new List<CustomerImage>();

        private void RefreshProductBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadProduct();
        }

        private void NewProductBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFProductAddEdit(TransactionTypeEnum.Insert, null);
        }

        private void EditProductBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateProduct();
        }

        private void LoadProduct()
        {
            if (!OrderID.HasValue)
                OrderID = 0;

            DataTable dt = ProductCardDAL.SelectViewData(OrderID.Value);
            ProductGridControl.DataSource = dt;

            EditProductBarButton.Enabled = DeleteProductBarButton.Enabled = ProductGridView.RowCount > 0;
            if (dt.Rows.Count > 0)
                calcTotalPrice = Convert.ToDecimal(dt.Compute("SUM(TOTAL)", string.Empty));
            else
                calcTotalPrice = 0;

            OrderAmountValue.EditValue = calcTotalPrice;
        }

        private void LoadFProductAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = ProductGridView.TopRowIndex;
            old_row_id = ProductGridView.FocusedRowHandle;
            FProductCardAddEdit fd = new FProductCardAddEdit()
            {
                TransactionType = transactionType,
                OrderID = OrderID.Value,
                CardID = id
            };
            fd.RefreshDataGridView += new FProductCardAddEdit.DoEvent(LoadProduct);
            fd.ShowDialog();
            ProductGridView.TopRowIndex = topindex;
            ProductGridView.FocusedRowHandle = old_row_id;
        }

        private void ProductGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditProductBarButton.Enabled)
                UpdateProduct();
        }

        void UpdateProduct()
        {
            LoadFProductAddEdit(TransactionTypeEnum.Update, productID);
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
                ContractID = Convert.ToInt16(dt.Rows[0]["ID"]);
                NoteText.EditValue = dt.Rows[0]["NOTE"];
                OrderDate.EditValue = dt.Rows[0]["ORDER_DATE"];
                if (OrderDate.DateTime == DateTime.MinValue)
                    OrderDate.EditValue = null;
                GlobalProcedures.LookUpEditValue(BranchLookUp, dt.Rows[0]["BRANCH_NAME"].ToString());
                GlobalProcedures.LookUpEditValue(SourceLookUp, dt.Rows[0]["ORDER_SOURCE"].ToString());
                FirstPaymentValue.EditValue = Convert.ToDecimal(dt.Rows[0]["FIRST_PAYMENT"].ToString());
                OrderAmountValue.EditValue = Convert.ToDecimal(dt.Rows[0]["ORDER_AMOUNT"].ToString());
                TotalOrderAmountValue.EditValue = Convert.ToDecimal(dt.Rows[0]["CREDIT_AMOUNT"].ToString());
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
            productID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));

            //DataRow row = ProductGridView.GetFocusedDataRow();
            //if (row != null)
            //   productID = Convert.ToInt32(row["ID"].ToString());

        }
        private void InsertOrderOperation(OracleTransaction tran)
        {
            OrderOperation order = new OrderOperation
            {

                OPERATION_ID = (int)OperationTypeEnum.Yeni_muraciet,
                ORDER_ID = OrderID.Value
            };

            orderOperationID = OperationDAL.InsertOrderOperation(tran, order);
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
                CREDIT_AMOUNT = TotalOrderAmountValue.Value,
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
                CREDIT_AMOUNT = TotalOrderAmountValue.Value,
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
                        InsertOrderOperation(tran);
                    }
                    else
                    {
                        UpdateOrder(tran);
                    }
                    GlobalProcedures.ExecuteProcedureWithParametr(tran, "ELMS_USER.PROC_INSERT_PRODUCT_CARDS", "P_ORDER_TAB_ID", OrderID.Value);


                    return 1;
                }, TransactionType == TransactionTypeEnum.Insert ? "Müraciət məlumatları bazaya daxil edilmədi." : "Müraciət məlumatları bazada dəyişdirilmədi.");

                InsertContractDocument();
                this.Close();
            }
        }
        private void InsertContractDocument()
        {
            string file_name = null, code = "01", sql = null, filePath = null;
            if (GlobalVariables.WordDocumentUsed)
            {
                GlobalProcedures.SplashScreenClose();
                XtraMessageBox.Show("Açıq olan bütün word fayllar avtomatik olaraq bağlanılacaq.", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GlobalProcedures.KillWord();
                GlobalVariables.WordDocumentUsed = false;
            }

            //ProgressPanel.Description = "Hazırlanmış fayllar bazaya yüklənir...";

            //#region muqavile
            filePath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents\\" + RegisterCodeText.Text.Replace("/", "") + "_Müqavilə.docx";
            //if (contract_click && File.Exists(filePath))
            //{

            file_name = Path.GetFileName(filePath);
            code = DocumentCode(1);
            sql = $@"INSERT INTO ELMS_USER.CONTRACT_DOCUMENTS(CONTRACT_ID,
                                                                 CONTRACT_DOCUMENT_TYPE_ID,
                                                                 DOCUMENT_FILE,
                                                                 CODE,
                                                                 FILE_NAME) 
                                        VALUES({ContractID},
                                                1,
                                                :BlobFile,
                                                '{code}',
                                                '{file_name}')";

            GlobalFunctions.ExecuteQueryWithBlob(sql, filePath,
                                                    "Müqavilən hazır çap faylı bazaya daxil edilmədi.");

            //}
            //#endregion
        }

        private string DocumentCode(int type_id)
        {
            string s = "01";
            int max_code = GlobalFunctions.GetMax($@"SELECT NVL(MAX(LTRIM(CODE,'0')),0) + 1 MAXCODE FROM ELMS_USER.CONTRACT_DOCUMENTS WHERE CONTRACT_DOCUMENT_TYPE_ID = {type_id} AND CONTRACT_ID = {ContractID}");
            s = max_code.ToString().PadLeft(2, '0');
            return s;
        }

        private bool ControlCardDetails()
        {

            bool b = false;

            if (String.IsNullOrEmpty(OrderDate.Text))
            {
                OrderDate.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Tarix daxil edilməyib.");
                OrderDate.Focus();
                OrderDate.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (branchID == 0)
            {
                BranchLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Filial seçilməyib.");
                BranchLookUp.Focus();
                BranchLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (FinCodeSearch.Text.Length == 0)
            {
                FinCodeSearch.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Müştərinin fin kodu daxil edilməyib.");
                FinCodeSearch.Focus();
                FinCodeSearch.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (ProductGridView.RowCount == 0)
            {
                ProductGridControl.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sifarişlər daxil edilməyib.");
                ProductGridControl.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (sourceID == 0)
            {
                SourceLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sifarişin mənbəyi seçilməyib.");
                SourceLookUp.Focus();
                SourceLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (timeID == 0)
            {
                TimeLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Müddət seçilməyib.");
                TimeLookUp.Focus();
                TimeLookUp.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (FirstPaymentValue.Value <= 0)
            {
                FirstPaymentValue.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("İlkin ödəniş sıfırdan böyük olmalıdır.");
                FirstPaymentValue.Focus();
                FirstPaymentValue.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;


            return b;
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

        void DeleteProduct()
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
            DeleteProduct();
            LoadProduct();
        }


        private void ProductGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Product_SS, e);
        }

        private void ProductGridView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

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

        private void ProductGridView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            GlobalProcedures.GridCustomDrawFooterCell(Product_TotalPrice, "Far", e);
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            OrderID = 0;
        }

        private void LoadCustomerDetails()
        {
            if (pinCode.Length < 1)
            {
                NameText.Text =
                    RegisterAddressText.Text =
                        ActualAddressText.Text =
                        PhoneAllText.Text = null;
                CustomerID = 0;
                PictureEdit.Image = null;
            }
            else
            {
                DataTable dt = CustomerDAL.SelectCustomerData(pinCode);
                if (FinCodeSearch.Text.Length != 7)
                {
                    NameText.Text =
                      RegisterAddressText.Text =
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
                    UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
                    CustomerID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    string str = dt.Rows[0]["PINCODE"].ToString();
                    //char[] result;
                    //string fin = "";
                    //// copies str to result
                    //result = str.ToCharArray();

                    //// prints result
                    //for (int i = 1; i < result.Length; i++)
                    //{
                    //    fin = fin + (result[i] + "").ToString();
                    //}
                    FinCodeSearch.EditValue = str;

                    LoadImage();
                }

            }
        }

        private void ProductGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void BContract_Click(object sender, EventArgs e)
        {
            GlobalProcedures.SplashScreenShow(this, typeof(WaitForms.FPrintDocumentWait));
            object fileName = Path.Combine(GlobalVariables.V_ExecutingFolder + "\\Documents\\Müqavilə.docx");
            if (!File.Exists(fileName.ToString()))
            {
                contract_click = false;
                GlobalVariables.WordDocumentUsed = false;
                XtraMessageBox.Show("Müqavilənin faylı tapılmadı.");
                GlobalProcedures.SplashScreenClose();
                return;
            }

            string qep = null,
                amount_with_word,
                com_with_word,
                fifd_with_word,
                phone = null,
                period = null,
                date = GlobalFunctions.DateWithDayMonthYear(ContractStartDate.DateTime),
                filePath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents\\" + ContractCodeText.Text.Replace("/", "") + "_Müqavilə.docx";

            object missing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document aDoc = null;
            object saveAs = Path.Combine(filePath);
            object readOnly = false;
            object isVisible = false;
            wordApp.Visible = false;

            aDoc = wordApp.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);

            aDoc.Activate();
            double d = 0, div = 0;
            int mod = 0;
            d = (double)CreditAmountValue.Value * 100;

            div = (int)(d / 100);
            mod = (int)(d % 100);
            if (mod > 0)
            {
                if (credit_currency_id == 1)
                    qep = " " + mod.ToString() + " qəpik";
                else
                    qep = " " + mod.ToString();
            }

            amount_with_word = "(" + GlobalFunctions.IntegerToWritten(div) + ") " + credit_currency_name + qep;

            //Komissiya
            d = (double)CommissionValue.Value * 100;

            div = (int)(d / 100);
            mod = (int)(d % 100);
            if (mod > 0)
            {
                if (credit_currency_id == 1)
                    qep = " " + mod.ToString() + " qəpik";
                else
                    qep = " " + mod.ToString();
            }

            com_with_word = "(" + GlobalFunctions.IntegerToWritten(div) + ") " + credit_currency_name + qep;

            //FIFD
            decimal fifd = Math.Round(FifdValue.Value, 2);
            d = (double)fifd * 100;

            div = (int)(d / 100);
            mod = (int)(d % 100);
            if (mod > 0)
                qep = " tam yüzdə " + GlobalFunctions.IntegerToWritten(mod);

            fifd_with_word = "(" + GlobalFunctions.IntegerToWritten(div) + qep + ")";

            if (PeriodCheckEdit.Checked)
                period = ContractEndDate.Text + " tarixinə qədər";
            else
                period = PeriodValue.Value + " (" + GlobalFunctions.IntegerToWritten((int)PeriodValue.Value) + ") ay";

            phone = GlobalFunctions.GetName($@"SELECT PHONE FROM CRS_USER.V_PHONE WHERE OWNER_TYPE = '{person_description}' AND OWNER_ID = {CustomerID}");

            try
            {
                GlobalProcedures.FindAndReplace(wordApp, "[$contractcode]", ContractCodeText.Text);
                GlobalProcedures.FindAndReplace(wordApp, "[$contractdate]", date);
                if (customer_type_id == 1)
                {
                    GlobalProcedures.FindAndReplace(wordApp, "[$customer]", CustomerFullNameText.Text + " (" + CardDescriptionText.Text + ", " + IssuingDateText.Text + " tarixində " + IssuingText.Text + " tərəfindən verilib)");
                    GlobalProcedures.FindAndReplace(wordApp, "[$carddate]", IssuingDateText.Text + " tarixində " + IssuingText.Text + " tərəfindən verilib");
                }
                else
                {
                    GlobalProcedures.FindAndReplace(wordApp, "[$customer]", CustomerFullNameText.Text + " (" + CardDescriptionText.Text + ")");
                    GlobalProcedures.FindAndReplace(wordApp, "[$carddate]", null);
                }
                GlobalProcedures.FindAndReplace(wordApp, "[$companyname]", GlobalVariables.V_CompanyName);
                GlobalProcedures.FindAndReplace(wordApp, "[$companyvoen]", GlobalVariables.V_CompanyVoen);
                GlobalProcedures.FindAndReplace(wordApp, "[$companyphone]", GlobalVariables.V_CompanyPhone);
                GlobalProcedures.FindAndReplace(wordApp, "[$companyaddress]", GlobalVariables.V_CompanyAddress);
                GlobalProcedures.FindAndReplace(wordApp, "[$companydirector]", GlobalVariables.V_CompanyDirector);
                GlobalProcedures.FindAndReplace(wordApp, "[$card]", CardDescriptionText.Text);
                GlobalProcedures.FindAndReplace(wordApp, "[$cur]", credit_currency_name);
                GlobalProcedures.FindAndReplace(wordApp, "[$creditpurpose]", "biznes tələblərinin ödənilməsi");
                GlobalProcedures.FindAndReplace(wordApp, "[$percent]", InterestValue.Value);
                GlobalProcedures.FindAndReplace(wordApp, "[$percentwrite]", "(" + GlobalFunctions.IntegerToWritten((double)InterestValue.Value) + ")");
                GlobalProcedures.FindAndReplace(wordApp, "[$amount]", CreditAmountValue.Value.ToString("N2"));
                GlobalProcedures.FindAndReplace(wordApp, "[$amountwrite]", amount_with_word);
                GlobalProcedures.FindAndReplace(wordApp, "[$grace]", GracePeriodValue.Text);
                GlobalProcedures.FindAndReplace(wordApp, "[$gracewrite]", "(" + GlobalFunctions.IntegerToWritten((int)GracePeriodValue.Value) + ")");
                GlobalProcedures.FindAndReplace(wordApp, "[$period]", PeriodValue.Value);
                GlobalProcedures.FindAndReplace(wordApp, "[$periodwrite]", "(" + GlobalFunctions.IntegerToWritten((double)PeriodValue.Value) + ")");
                GlobalProcedures.FindAndReplace(wordApp, "[$com]", CommissionValue.Value);
                GlobalProcedures.FindAndReplace(wordApp, "[$comwrite]", com_with_word);
                GlobalProcedures.FindAndReplace(wordApp, "[$fifd]", fifd);
                GlobalProcedures.FindAndReplace(wordApp, "[$fifdwrite]", fifd_with_word);
                GlobalProcedures.FindAndReplace(wordApp, "[$contractenddate]", GlobalFunctions.DateWithDayMonthYear(ContractEndDate.DateTime));
                GlobalProcedures.FindAndReplace(wordApp, "[$customername]", CustomerFullNameText.Text);
                GlobalProcedures.FindAndReplace(wordApp, "[$address]", RegistrationAddressText.Text);
                GlobalProcedures.FindAndReplace(wordApp, "[$phones]", phone);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                aDoc.SaveAs2(ref saveAs, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                aDoc.Close(ref missing, ref missing, ref missing);

                GlobalVariables.WordDocumentUsed = true;
                contract_click = true;

                Process.Start(filePath);
            }
            catch
            {
                GlobalProcedures.SplashScreenClose();
                GlobalProcedures.ShowErrorMessage(ContractCodeText.Text + "_Müqavilə.docx faylı açıq olduğu üçün bu faylı yenidən yaratmaq olmaz.");
            }
            finally
            {
                GlobalProcedures.SplashScreenClose();
            }
        }

        private void ProductGridView_MouseUp(object sender, MouseEventArgs e)
        {

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

                case 8:
                    GlobalProcedures.FillLookUpEdit(TimeLookUp, TimesDAL.SelectTimesByID(null).Tables[0]);
                    break;
                case 9:
                    GlobalProcedures.FillLookUpEdit(SourceLookUp, SourceDAL.SelectSourceByID(null).Tables[0]);
                    break;
            }
        }





        // LOOKUP -a aid olan hisse
        /// /////////
        /// /////////
        /// /////////

        private void SourceLookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                LoadDictionaries(TransactionTypeEnum.Update, 9);
        }

        private void TimeLookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                LoadDictionaries(TransactionTypeEnum.Update, 8);
        }

        private void TimeLookUp_EditValueChanged(object sender, EventArgs e)
        {
            timeID = GlobalFunctions.GetLookUpID(sender);
        }

        private void SourceLookUp_EditValueChanged(object sender, EventArgs e)
        {
            sourceID = GlobalFunctions.GetLookUpID(sender);
        }

        private void FinCodeSearch_EditValueChanged(object sender, EventArgs e)
        {
            pinCode = FinCodeSearch.Text.Trim();
            LoadCustomerDetails();
        }



        private void FOrderAddEdit_Load(object sender, EventArgs e)
        {
            GlobalProcedures.FillLookUpEdit(BranchLookUp, BranchDAL.SelectBranchByID(null).Tables[0]);
            GlobalProcedures.FillLookUpEdit(SourceLookUp, SourceDAL.SelectSourceByID(null).Tables[0]);
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

                OrderID = GlobalFunctions.GetOracleSequenceValue("ORDER_TAB_SEQUENCE");
                RegisterCodeText.EditValue = OrderID;
                ContractID = Convert.ToInt32(RegisterCodeText.Text.Trim());
                if(NewOrder == NewOrderEnum.FromCustomerUserControl)
                {
                    FinCodeSearch.EditValue = "1234567";
                    LoadCustomerData();
                }
            }
            InsertTemps();
            LoadProduct();
        }


        private void LoadCustomerData()
        {
                DataTable dt = CustomerDAL.SelectCustomerDataView(CustomerID.Value);
                if (FinCodeSearch.Text.Length != 7)
                {
                    NameText.Text =
                      RegisterAddressText.Text =
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
                    UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
                    CustomerID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    string str = dt.Rows[0]["PINCODE"].ToString();
                    FinCodeSearch.EditValue = str;

                    LoadImage();
                }

            
        }

        private void FOrderAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.ORDER_TAB", -1, "WHERE ID = " + OrderID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            DeleteAllTemp();

            if (TransactionType == TransactionTypeEnum.Insert)
                OrderID = 0;
            OrderDAL.DeleteOrder(OrderID.Value);

            this.RefreshDataGridView();
        }

        private void DeleteAllTemp()
        {
            GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER_TEMP.PROC_DELETE_ORDER_TEMP", "P_USED_USER_ID", GlobalVariables.V_UserID, "İstifadəçinin məlumatları temp cədvəldən silinmədi.");
        }
    }
}