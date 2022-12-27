using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using static ELMS.Class.Enum;
using ELMS.Class;
using ELMS.Class.DataAccess;
using ELMS.Class.Tables;

namespace ELMS.Forms.Customer
{
    public partial class FWorkAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FWorkAddEdit()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public int? CustomerID;
        public int? WorkID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1;

        private void FWorkAddEdit_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Sənədlərin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CUSTOMER_WORKPLACE", GlobalVariables.V_UserID, "WHERE ID = " + WorkID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş sənədlərə hal-hazırda " + used_user_name + " tərəfindən düzəliş edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
                this.Text = "İş yerinin əlavə edilməsi";
        }

        private void LoadDetails()
        {

            DataTable dt = CustomerWorkDAL.SelectViewData(CustomerID);

            if (dt.Rows.Count > 0)
            {
                OfficeText.EditValue = dt.Rows[0]["PLACE_NAME"];
                PositionText.EditValue = dt.Rows[0]["POSITION"];
                StartDate.EditValue = dt.Rows[0]["START_DATE"];
                if (StartDate.DateTime == DateTime.MinValue)
                    StartDate.EditValue = null;
                EndDate.EditValue = dt.Rows[0]["END_DATE"];
                if (EndDate.DateTime == DateTime.MinValue)
                    EndDate.EditValue = null;
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
            }
        }

        private void ComponentEnabled(bool status)
        {
            OfficeText.Enabled =
                PositionText.Enabled =
                BOK.Visible = !status;
        }


        private void LoadWorkDetails()
        {
            string s = "SELECT PLACE_NAME,POSITION,START_DATE,END_DATE,NOTE FROM COMS_USER_TEMP.CUSTOMER_WORKPLACE_TEMP WHERE ID = " + WorkID;
            try
            {
                DataTable dt = Class.GlobalFunctions.GenerateDataTable(s);

                foreach (DataRow dr in dt.Rows)
                {
                    OfficeText.Text = dr[0].ToString();
                    PositionText.Text = dr[1].ToString();
                    StartDate.EditValue = DateTime.Parse(dr[2].ToString());
                    EndDate.EditValue = DateTime.Parse(dr[3].ToString());
                    NoteText.Text = dr[4].ToString();
                }  
            }
            catch (Exception exx)
            {
                Class.GlobalProcedures.LogWrite("İş yerinin detalları tapılmadı.", s, Class.GlobalVariables.V_UserName, this.Name, this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, exx);                
            }
        }

        private void InsertDetail()
        {
           CustomerWork customerWork = new CustomerWork
           {
                PLACE_NAME = OfficeText.Text.Trim(),
                POSITION = PositionText.Text.Trim(),
                START_DATE = StartDate.DateTime,
                END_DATE = EndDate.DateTime,
                CUSTOMER_ID = CustomerID.Value
            };
            CustomerWorkDAL.InsertCustomerWork(customerWork);
        }

        private void UpdateDetail()
        {
            isClickBOK = true;

            CustomerWork customerWork = new CustomerWork
            {
                PLACE_NAME = OfficeText.Text.Trim(),
                POSITION = PositionText.Text.Trim(),
                START_DATE = StartDate.DateTime,
                END_DATE = EndDate.DateTime,
                ID = WorkID.Value,
                CUSTOMER_ID = CustomerID.Value,
                USED_USER_ID = -1,
                IS_CHANGE = (int)ChangeTypeEnum.Change
            };

            CustomerWorkDAL.UpdateCustomerWork(customerWork);
        }

        private bool ControlWorkDetails()
        {
            bool b = false;

            if (OfficeText.Text.Length == 0)
            {
                OfficeText.BackColor = Color.Red;
                Class.GlobalProcedures.ShowErrorMessage("İş yerinin adı daxil edilməyib.");               
                OfficeText.Focus();
                OfficeText.BackColor = Class.GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (PositionText.Text.Length == 0)
            {
                PositionText.BackColor = Color.Red;
                Class.GlobalProcedures.ShowErrorMessage("Vəzifə daxil edilməyib.");                
                PositionText.Focus();
                PositionText.BackColor = Class.GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (StartDate.Text.Length == 0)
            {
                StartDate.BackColor = Color.Red;
                Class.GlobalProcedures.ShowErrorMessage("Tarix daxil edilməyib.");                
                StartDate.Focus();
                StartDate.BackColor = Class.GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (EndDate.Text.Length == 0)
            {
                EndDate.BackColor = Color.Red;
                Class.GlobalProcedures.ShowErrorMessage("Tarix daxil edilməyib.");                
                EndDate.Focus();
                EndDate.BackColor = Class.GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (Class.GlobalFunctions.ChangeStringToDate(StartDate.Text, "ddmmyyyy") == Class.GlobalFunctions.ChangeStringToDate(EndDate.Text, "ddmmyyyy"))
            {
                StartDate.BackColor = Color.Red;
                EndDate.BackColor = Color.Red;
                Class.GlobalProcedures.ShowErrorMessage("Başlanğıc tarixi ilə son tarix eyni ola bilməz.");                
                StartDate.Focus();
                StartDate.BackColor = Class.GlobalFunctions.ElementColor();
                EndDate.BackColor = Class.GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            return b;
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FWorkAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.CUSTOMER_WORKPLACE", -1, "WHERE ID = " + WorkID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ControlWorkDetails())
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