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
using static ELMS.Class.Enum;
using ELMS.Class.Tables;
using ELMS.Class.DataAccess;

namespace ELMS.Forms.Dictionaries
{
    public partial class FKindShipAddEdit : DevExpress.XtraEditors.XtraForm
    {
        public FKindShipAddEdit()
        {
            InitializeComponent();
        }

        public TransactionTypeEnum TransactionType;
        public int? KindShipID;

        bool CurrentStatus = false, Used = false, isClickBOK = false;
        int UsedUserID = -1, orderID;

        public delegate void DoEvent();
        public event DoEvent RefreshDataGridView;

        private void FKindShipAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClickBOK && TransactionType == TransactionTypeEnum.Update)
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.KINDSHIP_RATE", -1, "WHERE ID = " + KindShipID + " AND USED_USER_ID = " + GlobalVariables.V_UserID);
            this.RefreshDataGridView();
        }

        private void FKindShipAddEdit_Load(object sender, EventArgs e)
        {

            if (TransactionType == TransactionTypeEnum.Update)
            {

                this.Text = "Qohumluq dərəcəsinin düzəliş edilməsi";
                GlobalProcedures.Lock_or_UnLock_UserID("ELMS_USER.KINDSHIP_RATE", GlobalVariables.V_UserID, "WHERE ID = " + KindShipID + " AND USED_USER_ID = -1");
                LoadDetails();
                Used = (UsedUserID > 0);

                if (Used)
                {
                    if (GlobalVariables.V_UserID != UsedUserID)
                    {
                        string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                        GlobalProcedures.ShowWarningMessage("Seçilmiş Qohumluq dərəcəsi hal-hazırda " + used_user_name + " tərəfindən istifadə edilir. Onun məlumatları dəyişdirilə bilməz. Siz yalnız məlumatlara baxa bilərsiniz.");
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
                this.Text = "Qohumluq dərəcələrinin əlavə edilməsi";
                  }

        private void ComponentEnabled(bool status)
        {
            NameText.Enabled =
                NoteText.Enabled =
                BOK.Visible = !status;

        }

        private void LoadDetails()
        {


            List<KindShip> lstKindShip = KindShipDAL.SelectKindShipByID(KindShipID).ToList<KindShip>();
            if (lstKindShip.Count > 0)
            {
                var kindShip = lstKindShip.LastOrDefault();
                NameText.EditValue = kindShip.NAME;
                NoteText.EditValue = kindShip.NOTE;
                UsedUserID = kindShip.USED_USER_ID;
                orderID = kindShip.ORDER_ID;
            }
        }

        private bool ControlDetail()
        {

            bool b = false;

            if (NameText.Text.Length == 0)
            {
                NameText.BackColor = Color.Red;
                GlobalProcedures.ShowErrorMessage("Qohumluq dərəcəsinin adı daxil edilməyib.");
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
            KindShip kindShip = new KindShip
            {

                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim()
            };
            KindShipDAL.InsertKindShip(kindShip);

        }

        private void UpdateDetail()
        {
            isClickBOK = true;

            KindShip kindShip = new KindShip
            {
                NAME = NameText.Text.Trim(),
                NOTE = NoteText.Text.Trim(),
                ID = KindShipID.Value,
                ORDER_ID = orderID,
                USED_USER_ID = -1
            };

            KindShipDAL.UpdateKindShip(kindShip);
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