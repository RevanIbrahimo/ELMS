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
        int UsedUserID = -1, positionID;

        private void FWorkAddEdit_Load(object sender, EventArgs e)
        {
            GlobalProcedures.FillLookUpEdit(PositionLookUp, ProfessionDAL.SelectProfession(null).Tables[0]);

            if (TransactionType == TransactionTypeEnum.Update)
            {
                this.Text = "Sənədlərin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER_TEMP.CUSTOMER_WORKPLACE_TEMP", GlobalVariables.V_UserID, "WHERE ID = " + WorkID + " AND USED_USER_ID = -1");
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
            DataTable dt = CustomerWorkDAL.SelectViewDataByID(WorkID);

            if (dt.Rows.Count > 0)
            {
                OfficeText.EditValue = dt.Rows[0]["PLACE_NAME"];
                NoteText.EditValue = dt.Rows[0]["NOTE"];
                NoAvailableRadioGroup.SelectedIndex = Convert.ToInt16(dt.Rows[0]["NOAVAILABLE"]);
                GlobalProcedures.LookUpEditValue(PositionLookUp, dt.Rows[0]["POSITION"].ToString());
                SalaryValue.EditValue = Convert.ToDecimal(dt.Rows[0]["SALARY"].ToString());
                UsedUserID = Convert.ToInt16(dt.Rows[0]["USED_USER_ID"]);
            }
        }
       
        private void ComponentEnabled(bool status)
        {
            OfficeText.Enabled =
                BOK.Visible = !status;
        }


        

        private void InsertDetail()
        {
           CustomerWork customerWork = new CustomerWork
           {
                PLACE_NAME = OfficeText.Text.Trim(),
                PROFESSION_ID = positionID,
                SALARY = SalaryValue.Value,
                NOAVAILABLE = NoAvailableRadioGroup.SelectedIndex,
                NOTE = NoteText.Text.Trim(),
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
                PROFESSION_ID = positionID,
                SALARY = SalaryValue.Value,
                NOAVAILABLE = NoAvailableRadioGroup.SelectedIndex,
                NOTE = NoteText.Text.Trim(),
                CUSTOMER_ID = CustomerID.Value,
                ID = WorkID.Value,
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
                GlobalProcedures.ShowErrorMessage("İş yerinin adı daxil edilməyib.");               
                OfficeText.Focus();
                OfficeText.BackColor = Class.GlobalFunctions.ElementColor();
                return false;
            }
            else
                b = true;

            if (positionID == 0)
            {
                PositionLookUp.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Peşə seçilməyib.");
                PositionLookUp.Focus();
                PositionLookUp.BackColor = GlobalFunctions.ElementColor();
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

        private void PositionLookUp_EditValueChanged(object sender, EventArgs e)
        {
            positionID = GlobalFunctions.GetLookUpID(sender);
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