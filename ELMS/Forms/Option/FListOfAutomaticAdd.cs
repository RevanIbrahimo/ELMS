using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using ELMS.Class;
using static ELMS.Class.Enum;

namespace ELMS.Forms
{
    public partial class FListOfAutomaticAdd : DevExpress.XtraEditors.XtraForm
    {
        public FListOfAutomaticAdd()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public string GroupName;
        public int? GroupID;
        public delegate void DoEvent();
        public event DoEvent RefreshListDataGridView;

        private void FListOfAutomaticAdd_Load(object sender, EventArgs e)
        {
            if (TransactionType == TransactionTypeEnum.Update)
                NoteLabelControl.Text = "Bu siyahıda yalnız aktiv olan,sistemə daxil olmamış və istifadə edilməyən istifadəçilər göstərilib";
            else
                NoteLabelControl.Text = "Bu siyahıda yalnız aktiv olan və istifadə edilməyən istifadəçilər göstərilib";
            GroupNameTextEdit.Text = GroupName;
            LoadListDataGridView();
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadListDataGridView()
        {
            string s = null;
            try
            {
                if (TransactionType == TransactionTypeEnum.Update)
                    s = "SELECT 1 SS,ID,SURNAME||' '||NAME||' '||PATRONYMIC||' '||DECODE(SEX_ID,1,'oğlu','qızı') FULL_NAME FROM ELMS_USER.SYSTEM_USER WHERE STATUS_ID = 1 AND SESSION_ID = 0 AND USED_USER_ID = -1 AND GROUP_ID <> " + GroupID;
                else
                    s = "";

                ListGridControl.DataSource = GlobalFunctions.GenerateDataTable(s, this.Name + "/LoadListDataGridView");
                ListGridView.PopulateColumns();
                ListGridView.Columns[0].Caption = "S/s";
                ListGridView.Columns[1].Visible = false;
                ListGridView.Columns[2].Caption = "Soyadı, adı, atasının adı";


                ListGridView.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                ListGridView.Columns[0].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                ListGridView.BestFitColumns();
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("Verilmiş hüquqlar cədvələ yüklənmədi.", s, GlobalVariables.V_UserName, this.Name, this.GetType().FullName + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        private void FListOfAutomaticAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.RefreshListDataGridView();
        }

        private void ListGridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            GlobalProcedures.GridCustomColumnDisplayText(e);
        }

        private void UpdateUser()
        {
            ArrayList rows = new ArrayList();
            rows.Clear();
            try
            {
                for (int i = 0; i < ListGridView.SelectedRowsCount; i++)
                {
                    rows.Add(ListGridView.GetDataRow(ListGridView.GetSelectedRows()[i]));
                }

                ListGridView.BeginUpdate();

                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i] as DataRow;
                    GlobalProcedures.ExecuteTwoQuery($@"UPDATE ELMS_USER.SYSTEM_USER SET GROUP_ID = {GroupID} WHERE ID = {row["ID"]}",
                                                     $@"UPDATE ELMS_USER.USER_GROUP_PERMISSION SET GROUP_ID = {GroupID} WHERE USER_ID = {row["ID"]}",
                                                        "İstifadəçinin qrupu dəyişdirilmədi.",
                                                        this.Name + "/UpdateUser");
                }
            }
            finally
            {
                ListGridView.EndUpdate();
            }
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            UpdateUser();
            this.Close();
        }
    }
}