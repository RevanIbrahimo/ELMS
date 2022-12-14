using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ELMS.Class;
using ELMS.Class.DataAccess;
using ELMS.Class.Tables;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList.Localization;

namespace ELMS
{
    public partial class DentalSplashScreen : SplashScreen
    {
        public DentalSplashScreen()
        {
            Localizer.Active = new ComponentLocalization.StringLocalizer();
            GridLocalizer.Active = new ComponentLocalization.CustomGridLocalizer();
            BarLocalizer.Active = new ComponentLocalization.CustomBarLocalizer();
            DockManagerResXLocalizer.Active = new ComponentLocalization.CustomDockManagerLocalizer();
            TreeListLocalizer.Active = new ComponentLocalization.CustomTreeListLocalizer();            
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("az-Latn-AZ");
            InitializeComponent();
            XtraMessageBox.AllowHtmlText = true;
        }
        bool is_connected = false;
        #region Overrides

        public override void ProcessCommand(System.Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void DentalSplashScreen_Load(object sender, EventArgs e)
        {
            CopyrightLabel.Text = "Copyright © " + DateTime.Now.Year;
            VersionLabel.Text = "v" + GlobalVariables.V_Version;
            GlobalVariables.V_ExecutingFolder = new FileInfo((Assembly.GetExecutingAssembly().Location)).Directory.FullName;

            if (BackgroundWorker.IsBusy != true)
                BackgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DescriptionLabel.Invoke((MethodInvoker)delegate
            {
                DescriptionLabel.Text = "Versiya yoxlanılır";
            });

            DescriptionLabel.Invoke((MethodInvoker)delegate
            {
                DescriptionLabel.Text = "Baza ilə əlaqə yoxlanılır";
            });

            if (!GlobalFunctions.ConnectDataBase())
            {
                XtraMessageBox.Show("Baza ilə əlaqə yoxdur...", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            DescriptionLabel.Invoke((MethodInvoker)delegate
            {
                DescriptionLabel.Text = "Baza ilə əlaqə var";
            });

            GlobalVariables.V_StyleName = GlobalFunctions.ReadSetting("StyleName");

            GlobalVariables.lstUsers = UserDAL.SelectUserByID(null).ToList<Users>();
            GlobalVariables.lstBranch = BranchDAL.SelectBranchByID(null).ToList<Branch>();

            //if (ControlFiles())
            is_connected = true;
            //else
            //    Application.Exit();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                XtraMessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                this.Close();
            }
        }

        private void DentalSplashScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!is_connected)
                return;

            FLoginSystem fc = new FLoginSystem();
            fc.ShowDialog();
        }
    }
}