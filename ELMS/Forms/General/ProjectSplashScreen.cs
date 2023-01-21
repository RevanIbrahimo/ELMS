using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using ELMS.Class;

namespace ELMS.Forms.General
{
    public partial class ProjectSplashScreen : SplashScreen
    {
        public ProjectSplashScreen()
        {
            InitializeComponent();
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

        private void ProjectSplashScreen_Load(object sender, EventArgs e)
        {
            GlobalVariables.V_ExecutingFolder = new FileInfo((Assembly.GetExecutingAssembly().Location)).Directory.FullName;
            GlobalVariables.V_WindowsUserName = Environment.UserName;

            if (BackgroundWorker.IsBusy != true)
                BackgroundWorker.RunWorkerAsync();
        }

        private void ControlDirectories()
        {
            if (!Directory.Exists(GlobalVariables.V_ExecutingFolder + "\\Login"))
                Directory.CreateDirectory(GlobalVariables.V_ExecutingFolder + "\\Login");
            if (!Directory.Exists(GlobalVariables.V_ExecutingFolder + "\\Documents"))
                Directory.CreateDirectory(GlobalVariables.V_ExecutingFolder + "\\Documents");
            if (!Directory.Exists(GlobalVariables.V_ExecutingFolder + "\\Documents\\" + GlobalVariables.V_WindowsUserName))
                Directory.CreateDirectory(GlobalVariables.V_ExecutingFolder + "\\Documents\\" + GlobalVariables.V_WindowsUserName);            
            if (!Directory.Exists(GlobalVariables.V_ExecutingFolder + "\\TEMP"))
                Directory.CreateDirectory(GlobalVariables.V_ExecutingFolder + "\\TEMP");            
            if (!Directory.Exists(GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents"))
                Directory.CreateDirectory(GlobalVariables.V_ExecutingFolder + "\\TEMP\\Documents");            
            if (!Directory.Exists(GlobalVariables.V_ExecutingFolder + "\\TEMP\\Images"))
                Directory.CreateDirectory(GlobalVariables.V_ExecutingFolder + "\\TEMP\\Images");
            if (!Directory.Exists(GlobalVariables.V_ExecutingFolder + "\\TEMP\\XmlFile"))
                Directory.CreateDirectory(GlobalVariables.V_ExecutingFolder + "\\TEMP\\XmlFile");
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

            DescriptionLabel.Invoke((MethodInvoker)delegate
            {
                DescriptionLabel.Text = "Baza ilə əlaqə var";
            });

            ControlDirectories();

            DescriptionLabel.Invoke((MethodInvoker)delegate
            {
                DescriptionLabel.Text = "Qovluqlar mövcuddur";
            });

            is_connected = true;

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

        private void ProjectSplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!is_connected)
            //    return;

            //FConnect fc = new FConnect();
            //fc.ShowDialog();
            //if (!is_connected)
            //    return;

            //Forms.General.FLoginSystem fc = new Forms.General.FLoginSystem();
            //fc.ShowDialog();
            MainForm fm = new MainForm();
            fm.ShowDialog();
        }
    }
}