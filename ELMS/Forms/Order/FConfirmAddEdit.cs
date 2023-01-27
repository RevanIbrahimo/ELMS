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
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using WordToPDF;

namespace ELMS.Forms.Order
{
    public partial class FConfirmAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FConfirmAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public UserControlTypeEnum UserControlType;
        public int? OrderID, CustomerID = 1, OperationID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;

        int UsedUserID = -1, orderID,
            documentID, topindex,
            old_row_id, customerID, relativeID,
            branchID = 0,
            sourceID = 0,
            timeID = 0,
            code_number,
            ContractID;
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
            DataTable dt = OrderDAL.SelectConfirmData(OrderID);

            if (dt.Rows.Count > 0)
            {
                OperationNoteText.EditValue = dt.Rows[0]["OPERATION_NOTE"].ToString();
                pinCode = dt.Rows[0]["PINCODE"].ToString();
                RegisterCodeText.EditValue = dt.Rows[0]["ID"];
                ContractID = Convert.ToInt16(dt.Rows[0]["ID"]);
                NoteText.EditValue = dt.Rows[0]["NOTE"];
                OrderDateText.EditValue = dt.Rows[0]["ORDER_DATE"];
                SourceText.EditValue = dt.Rows[0]["ORDER_SOURCE"];
                BranchText.EditValue = dt.Rows[0]["BRANCH_NAME"];
                TimeText.EditValue = dt.Rows[0]["TIME"];
                FirstPaymentValue.EditValue = dt.Rows[0]["FIRST_PAYMENT"].ToString();
                OrderAmountValue.EditValue = Convert.ToDecimal(dt.Rows[0]["ORDER_AMOUNT"].ToString());
                TotalOrderAmountValue.EditValue = Convert.ToDecimal(dt.Rows[0]["CREDIT_AMOUNT"].ToString());
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
        //Təsdiqin kliki........................................
        private void BOK_Click(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                OrderOperation order = new OrderOperation
                {
                    ORDER_ID = OrderID.Value,
                    ID = OperationID.Value,
                    NOTE = OperationNoteText.Text.Trim(),
                     INSERT_USER = GlobalVariables.V_UserID
                };
                if (UserControlType == UserControlTypeEnum.Confirm)
                {
                    order.OPERATION_ID = (int)OperationTypeEnum.Tesdiq_edildi;
                    GenerateContract();
                    InsertContractDocument();
                }
                else
                {
                    order.OPERATION_ID = (int)OperationTypeEnum.Muqavile_tesdiq_edildi;
                }
                OperationDAL.UpdateOrderOperation(order);
            }
            this.Close();

        }
        private void InsertContractDocument()
        {
            string file_name = null, sql = null, filePath = null;
            if (GlobalVariables.WordDocumentUsed)
            {
                GlobalProcedures.SplashScreenClose();
                XtraMessageBox.Show("Açıq olan bütün word fayllar avtomatik olaraq bağlanılacaq.", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GlobalProcedures.KillWord();
                GlobalVariables.WordDocumentUsed = false;
            }
            filePath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents\\" + RegisterCodeText.Text.Replace("/", "") + "_Müqavilə.pdf";
            if (File.Exists(filePath))
            {
                file_name = Path.GetFileName(filePath);
                sql = $@"INSERT INTO ELMS_USER.ORDER_DOCUMENTS(ORDER_ID,
                                                                 ORDER_DOCUMENT_TYPE_ID,
                                                                 DOCUMENT_FILE)
                                        VALUES({ContractID},
                                                1,
                                                :BlobFile)";

                GlobalFunctions.ExecuteQueryWithBlob(sql, filePath,
                                                        "Müqavilən hazır çap faylı bazaya daxil edilmədi.");
            }
        }
        

        private void GenerateContract()
        {
            Word2Pdf objWorPdf = new Word2Pdf();
            GlobalProcedures.SplashScreenShow(this, typeof(WaitForms.FPrintDocumentWait));
            object fileName = Path.Combine(GlobalVariables.V_ExecutingFolder + "\\Documents\\" + GlobalVariables.V_WindowsUserName + "\\Müqavilə.docx");
            if (!File.Exists(fileName.ToString()))
            {
                GlobalProcedures.ShowWarningMessage("Müqavilənin faylı tapılmadı.");
                GlobalProcedures.SplashScreenClose();
                return;
            }
            code_number = int.Parse(Regex.Replace(RegisterCodeText.Text, "[^0-9]", ""));
            string filePath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents\\" + code_number + "_Müqavilə.docx";
            

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
            //d = (double)CreditAmountValue.Value * 100;

            //div = (int)(d / 100);
            //mod = (int)(d % 100);
            //if (mod > 0)
            //{
            //    if (credit_currency_id == 1)
            //        qep = " " + mod.ToString() + " qəpik";
            //    else
            //        qep = " " + mod.ToString();
            //}

            //amount_with_word = "(" + GlobalFunctions.IntegerToWritten(div) + ") " + credit_currency_name + qep;

            ////Komissiya
            //d = (double)CommissionValue.Value * 100;

            //div = (int)(d / 100);
            //mod = (int)(d % 100);
            //if (mod > 0)
            //{
            //    if (credit_currency_id == 1)
            //        qep = " " + mod.ToString() + " qəpik";
            //    else
            //        qep = " " + mod.ToString();
            //}

            //com_with_word = "(" + GlobalFunctions.IntegerToWritten(div) + ") " + credit_currency_name + qep;

            ////FIFD
            //decimal fifd = Math.Round(FifdValue.Value, 2);
            //d = (double)fifd * 100;

            //div = (int)(d / 100);
            //mod = (int)(d % 100);
            //if (mod > 0)
            //    qep = " tam yüzdə " + GlobalFunctions.IntegerToWritten(mod);

            //fifd_with_word = "(" + GlobalFunctions.IntegerToWritten(div) + qep + ")";

            //if (PeriodCheckEdit.Checked)
            //    period = ContractEndDate.Text + " tarixinə qədər";
            //else
            //    period = PeriodValue.Value + " (" + GlobalFunctions.IntegerToWritten((int)PeriodValue.Value) + ") ay";

            //phone = GlobalFunctions.GetName($@"SELECT PHONE FROM CRS_USER.V_PHONE WHERE OWNER_TYPE = '{person_description}' AND OWNER_ID = {CustomerID}");

            try
            {
                GlobalProcedures.FindAndReplace(wordApp, "[$contractcode]", RegisterCodeText.Text);
                GlobalProcedures.FindAndReplace(wordApp, "[$contractdate]", OrderDateText.Text);
                GlobalProcedures.FindAndReplace(wordApp, "[$customername]", NameText.Text);
                GlobalProcedures.FindAndReplace(wordApp, "[$customerpincode]", FinCodeSearch.Text);
                GlobalProcedures.FindAndReplace(wordApp, "[$amount]", OrderAmountValue.Value.ToString());
                GlobalProcedures.FindAndReplace(wordApp, "[$firstpayment]", FirstPaymentValue.Text);
                //if (customer_type_id == 1)
                //{
                //    GlobalProcedures.FindAndReplace(wordApp, "[$customer]", CustomerFullNameText.Text + " (" + CardDescriptionText.Text + ", " + IssuingDateText.Text + " tarixində " + IssuingText.Text + " tərəfindən verilib)");
                //    GlobalProcedures.FindAndReplace(wordApp, "[$carddate]", IssuingDateText.Text + " tarixində " + IssuingText.Text + " tərəfindən verilib");
                //}
                //else
                //{
                //    GlobalProcedures.FindAndReplace(wordApp, "[$customer]", CustomerFullNameText.Text + " (" + CardDescriptionText.Text + ")");
                //    GlobalProcedures.FindAndReplace(wordApp, "[$carddate]", null);
                //}
                //GlobalProcedures.FindAndReplace(wordApp, "[$companyname]", GlobalVariables.V_CompanyName);
                //GlobalProcedures.FindAndReplace(wordApp, "[$companyvoen]", GlobalVariables.V_CompanyVoen);
                //GlobalProcedures.FindAndReplace(wordApp, "[$companyphone]", GlobalVariables.V_CompanyPhone);
                //GlobalProcedures.FindAndReplace(wordApp, "[$companyaddress]", GlobalVariables.V_CompanyAddress);
                //GlobalProcedures.FindAndReplace(wordApp, "[$companydirector]", GlobalVariables.V_CompanyDirector);
                

                if (File.Exists(filePath))
                    File.Delete(filePath);

                aDoc.SaveAs2(ref saveAs, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                aDoc.Close(ref missing, ref missing, ref missing);

                string strFileName = "Müqavilə.docx";
                object FromLocation = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents\\" + code_number + "_Müqavilə.docx";
                string FileExtension = Path.GetExtension(strFileName);
                string ChangeExtension = strFileName.Replace(FileExtension, ".pdf");
                if (FileExtension == ".doc" || FileExtension == ".docx")
                {
                    object ToLocation = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents\\" + code_number + "_Müqavilə.pdf";
                    objWorPdf.InputLocation = FromLocation;
                    objWorPdf.OutputLocation = ToLocation;
                    objWorPdf.Word2PdfCOnversion();
                }
                Process.Start(GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents\\" + code_number + "_Müqavilə.pdf");
            }
            catch
            {
                GlobalProcedures.SplashScreenClose();
                GlobalProcedures.ShowErrorMessage(code_number + "_Müqavilə.docx faylı yaradılmadı.");
            }
            finally
            {
                GlobalProcedures.SplashScreenClose();
            }
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                OrderOperation order = new OrderOperation
                {
                    ORDER_ID = OrderID.Value,
                    ID = OperationID.Value,
                    NOTE = OperationNoteText.Text.Trim()
                };
                if (UserControlType == UserControlTypeEnum.Confirm)
                {
                    order.OPERATION_ID = (int)OperationTypeEnum.Tesdiq_edilmedi;
                }
                else
                {
                    order.OPERATION_ID = (int)OperationTypeEnum.Muqavile_tesdiq_edilmedi;
                }
                OperationDAL.UpdateOrderOperation(order);
            }

            this.Close();
        }

        private void BClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //void CalcTotalAmount()
        //{
        //    TotalOrderAmountValue.EditValue = OrderAmountValue.Value - FirstPaymentValue.Value;
        //}

        //private void FirstPaymentValue_EditValueChanged(object sender, EventArgs e)
        //{
        //    CalcTotalAmount();
        //}

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
            if(UserControlType == UserControlTypeEnum.Contract)
            {
                RelativeBarManager.DockingEnabled = false;
                RelativeBar.Visible = false;
                RelativeStandaloneBarDockControl.Visible = false;
            }
        }

        private void OperationsGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Operation_SS, e);
        }

        private void FOrderAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.ORDER_TAB", -1, "WHERE ID = " + OrderID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            OrderDAL.DeleteOrder(OrderID.Value);
            CustomerDAL.DeleteCustomerData(CustomerID.Value);
            this.RefreshDataGridView();
        }

        private void ProductGridView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            GlobalProcedures.GridCustomDrawFooterCell(Product_TotalPrice, "Far", e);
        }

        private void OtherInfoTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (OtherInfoTabControl.SelectedTabPageIndex)
            {
                case 0:
                    LoadProduct();
                    break;
                case 1:
                    {
                        LoadDocument();
                        LoadPhone();
                        LoadWork();
                        LoadRelative();
                    }
                    break;
                case 2:
                    //LoadNoteMemo();
                    break;
                case 3:
                    LoadOperation();
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

        // NOTEMEMO -e aid olan hisse
        /// /////////
        /// /////////
        /// /////////

        private void LoadOperation()
        {
            if (!OrderID.HasValue)
                OrderID = 0;

            OperationsGridControl.DataSource = OperationDAL.SelectOperationData(null);
        }

    }
}