﻿using System;
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
using System.Text.RegularExpressions;
using ELMS.Forms.Order;

namespace ELMS.Forms.Agreement
{
    public partial class FAgreementAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FAgreementAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public int? AgreementID, CustomerID, EnumID;

        bool CurrentStatus = false, Used = false, isClickBOK = false,
            contract_click = false;

        int UsedUserID = -1, orderOperationID,
            productID, topindex,
            old_row_id,
            branchID = 0,
            sourceID = 0,
            timeID = 0,
            credit_currency_id = 0,
            customer_type_id = 1,
            operationID;
        decimal calcTotalPrice = 0;
        string pinCode,agreement_number,
            credit_currency_name = "null";

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;


        List<CustomerImage> lstImage = new List<CustomerImage>();

        private void RefreshContractsBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadContracts();
        }

        private void LoadContracts()
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                if (!AgreementID.HasValue)
                    AgreementID = 0;

                DataTable dt = AgreementDAL.SelectDataByAgreementID(AgreementID);
                ContractsGridControl.DataSource = dt;

                if (dt.Rows.Count > 0)
                    calcTotalPrice = Convert.ToDecimal(dt.Compute("SUM(CREDIT_AMOUNT)", string.Empty));
                else
                    calcTotalPrice = 0;
            }
            else
            {
                if (!AgreementID.HasValue)
                    AgreementID = 0;

                DataTable dt = AgreementDAL.SelectDataByOperationTypeID(5);
                ContractsGridControl.DataSource = dt;

                if (dt.Rows.Count > 0)
                    calcTotalPrice = Convert.ToDecimal(dt.Compute("SUM(CREDIT_AMOUNT)", string.Empty));
                else
                    calcTotalPrice = 0;

            }
               
        }

        private void ContractsGridControl_Click(object sender, EventArgs e)
        {

        }

        private void LoadAgreementDetails()
        {
            DataTable dt = AgreementDAL.SelectAgreements(AgreementID);

            if (dt.Rows.Count > 0)
            {
                RegisterCodeText.EditValue = dt.Rows[0]["AGREEMENT_NUMBER"];
                AgreementDate.EditValue = dt.Rows[0]["AGREEMENT_DATE"];
                if (AgreementDate.DateTime == DateTime.MinValue)
                    AgreementDate.EditValue = null;
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
            }
        }

        private void ComponentEnabled(bool status)
        {
                BOK.Visible = !status;
        }

        private void ContractsGridView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            GlobalProcedures.GridCustomDrawFooterCell(Contracts_PaymentAmount, "Far", e);
        }

        private void ContractsGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            productID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));

            //DataRow row = ContractsGridView.GetFocusedDataRow();
            //if (row != null)
            //   productID = Convert.ToInt32(row["ID"].ToString());

        }
        
        private void InsertAgreement(OracleTransaction tran)
        {
            Class.Tables.Agreement order = new Class.Tables.Agreement
            {
                BRANCH_ID = 1,
                AGREEMENT_NUMBER = agreement_number,
                AGREEMENT_DATE = AgreementDate.DateTime,
                AGREEMENT_AMOUNT = calcTotalPrice
            };

            AgreementID = AgreementDAL.InsertAgreement(tran, order);
        }

        private void UpdateAgreement(OracleTransaction tran)
        {
            isClickBOK = true;
            Class.Tables.Agreement order = new Class.Tables.Agreement
            {
                BRANCH_ID = 1,
                AGREEMENT_NUMBER = agreement_number,
                AGREEMENT_DATE = AgreementDate.DateTime,
                AGREEMENT_AMOUNT = calcTotalPrice,
                USED_USER_ID = -1,
                ID = AgreementID.Value
            };

            AgreementDAL.UpdateAgreement(tran, order);
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlCardDetails())
            {
                GlobalFunctions.RunInOneTransaction<int>(tran =>
                {

                    if (TransactionType == TransactionTypeEnum.Insert)
                    {
                        InsertAgreement(tran);
                    }
                    else
                    {
                        UpdateAgreement(tran);
                    }
                    return 1;
                }, TransactionType == TransactionTypeEnum.Insert ? "Müraciət məlumatları bazaya daxil edilmədi." : "Müraciət məlumatları bazada dəyişdirilmədi.");

                UpdateContracts(OperationTypeEnum.Saziş_yaradıldı);
                this.Close();
            }
        }
        

        private bool ControlCardDetails()
        {

            bool b = false;

            var rows = GlobalFunctions.GridviewSelectedRow(ContractsGridView);

            if (rows.Count == 0)
            {
                GlobalProcedures.ShowWarningMessage("Müqaviləni seçin.");
                return false;
            }
            else
                b = true;


            if (String.IsNullOrEmpty(AgreementDate.Text))
            {
                AgreementDate.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Tarix daxil edilməyib.");
                AgreementDate.Focus();
                AgreementDate.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (ContractsGridView.RowCount == 0)
            {
                ContractsGridControl.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Sifarişlər daxil edilməyib.");
                ContractsGridControl.BackColor = GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;
            

            return b;
        }
        
        void UpdateContracts(OperationTypeEnum operationTypeEnum)
        {
            var rows = GlobalFunctions.GridviewSelectedRow(ContractsGridView);

            if (rows.Count == 0)
            {
                GlobalProcedures.ShowWarningMessage("Müqaviləni seçin.");
                return;
            }

            if (GlobalFunctions.CallDialogResult("Seçilmiş müqavilələrlə sazişi yaratmaq istəyirsiniz?", "Sazişin yaradılması") == DialogResult.Yes)
                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i] as DataRow;
                    AgreementDAL.UpdateContracts(Convert.ToInt32(row["ID"]), AgreementID.Value);
                    AgreementDAL.UpdateOrderOperation(Convert.ToInt32(row["OPERATION_ID"]), operationTypeEnum);
                }
        }


        


        private void ContractsGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Contracts_SS, e);
        }

        private void ContractsGridView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

        }

        private void BranchLookUp_EditValueChanged(object sender, EventArgs e)
        {
            branchID = GlobalFunctions.GetLookUpID(sender);
        }
        
        private void BCancel_Click(object sender, EventArgs e)
        {
            AgreementID = 0;
        }

        
        

        private void ContractsGridView_MouseUp(object sender, MouseEventArgs e)
        {

        }


        private void FAgreementAddEdit_Load(object sender, EventArgs e)
        {

            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Sazişin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.AGREEMENT", GlobalVariables.V_UserID, "WHERE ID = " + AgreementID + " AND USED_USER_ID = -1");
                LoadAgreementDetails();
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
                this.Text = "Sazişin əlavə edilməsi";
                AgreementDate.DateTime = DateTime.Today;

                AgreementID = GlobalFunctions.GetOracleSequenceValue("AGREEMENT_SEQUENCE");
                agreement_number = "000" + AgreementID.ToString();
                RegisterCodeText.EditValue = agreement_number;
            }
           // InsertTemps();
            LoadContracts();
        }
        

        private void FAgreementAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.AGREEMENT", -1, "WHERE ID = " + AgreementID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            DeleteAllTemp();

            if (TransactionType == TransactionTypeEnum.Insert)
                AgreementID = 0;
            AgreementDAL.DeleteAgreement(AgreementID.Value);

            this.RefreshDataGridView();
        }

        private void DeleteAllTemp()
        {
            GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER_TEMP.PROC_DELETE_ORDER_TEMP", "P_USED_USER_ID", GlobalVariables.V_UserID, "İstifadəçinin məlumatları temp cədvəldən silinmədi.");
        }



        //private void LoadFContractsAddEdit(TransactionTypeEnum transaction, UserControlTypeEnum userControl, int? id)
        //{
        //    topindex = ContractsGridView.TopRowIndex;
        //    old_row_id = ContractsGridView.FocusedRowHandle;
        //    FConfirmAddEdit fd = new FConfirmAddEdit()
        //    {
        //        TransactionType = transaction,
        //        UserControlType = userControl,
        //        OrderID = id,
        //        OperationID = operationID
        //    };
        //    fd.RefreshDataGridView += new FConfirmAddEdit.DoEvent(LoadContracts);
        //    fd.ShowDialog();
        //    ContractsGridView.TopRowIndex = topindex;
        //    ContractsGridView.FocusedRowHandle = old_row_id;
        //}



        //private void NewContractsBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    LoadFContractsAddEdit(TransactionTypeEnum.Insert, UserControlTypeEnum.Contract, null);
        //}



    }
}