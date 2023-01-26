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
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using ELMS.Class;

namespace ELMS
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        XtraUserControl orderUserControl;
        XtraUserControl agreementUserControl;
        XtraUserControl customerUserControl;
        XtraUserControl contractUserControl;
        XtraUserControl confirmationUserControl;
        XtraUserControl bookkeepingUserControl;
        XtraUserControl totalUserControl;
        XtraUserControl akbUserControl;
        XtraUserControl userControl = null;

        int a = 0;

        private static class NavigationNameClass
        {
            public static readonly string Customer = "Müştərilər";
            public static readonly string Order = "Müraciətlər";
            public static readonly string Agreement = "Sazişlər";
            public static readonly string Confirm = "Təsdiq gözləyənlər";
            public static readonly string Contract = "Müqavilələr";
            public static readonly string Bookkeeping = "Mühasibatlıq";
            public static readonly string Total = "Kredit portfeli";
            public static readonly string AKB = "AKB";
        }

        public MainForm()
        {
            InitializeComponent();
            orderUserControl = CreateUserControl(NavigationNameClass.Order);
            agreementUserControl = CreateUserControl(NavigationNameClass.Agreement);
            confirmationUserControl = CreateUserControl(NavigationNameClass.Confirm);
            customerUserControl = CreateUserControl(NavigationNameClass.Customer);
            contractUserControl = CreateUserControl(NavigationNameClass.Contract);
            bookkeepingUserControl = CreateUserControl(NavigationNameClass.Bookkeeping);
            totalUserControl = CreateUserControl(NavigationNameClass.Total);
            akbUserControl = CreateUserControl(NavigationNameClass.AKB);
            accordionControl.SelectedElement = customerAccordionControlElement;
        }
        XtraUserControl CreateUserControl(string navigationText)
        {
            XtraUserControl result = new XtraUserControl();

            if (navigationText == NavigationNameClass.Customer)
            {
                result = new UserControls.CustomerUserControl();
                result.Text = NavigationNameClass.Customer;
            }
            else if (navigationText == NavigationNameClass.Order)
            {
                result = new UserControls.OrderUserControl();
                result.Text = NavigationNameClass.Order;
            } else if (navigationText == NavigationNameClass.Agreement)
            {
                result = new UserControls.AgreementUserControl();
                result.Text = NavigationNameClass.Agreement;
            } 
            else if (navigationText == NavigationNameClass.Confirm)
            {
                result = new UserControls.ConfirmationUserControl();
                result.Text = NavigationNameClass.Confirm;
            }
            else if (navigationText == NavigationNameClass.Contract)
            {
                result = new UserControls.ContractUserControl();
                result.Text = NavigationNameClass.Contract;
            }
            else if (navigationText == NavigationNameClass.Bookkeeping)
            {
                result = new UserControls.BookkeepingUserControl();
                result.Text = NavigationNameClass.Bookkeeping;
            }
            else if (navigationText == NavigationNameClass.Total)
            {
                result = new UserControls.TotalUserControl();
                result.Text = NavigationNameClass.Total;
            }
            else if (navigationText == NavigationNameClass.AKB)
            {
                result = new UserControls.AKBUserControl();
                result.Text = NavigationNameClass.AKB;
            }
            return result;
        }

        void LoadUserControl(string elementName)
        {
            if (elementName == NavigationNameClass.Order)
                userControl = orderUserControl;
            else if (elementName == NavigationNameClass.Agreement)
                userControl = agreementUserControl;
            else if (elementName == NavigationNameClass.Customer)
                userControl = customerUserControl;
            else if (elementName == NavigationNameClass.Confirm)
                userControl = confirmationUserControl;
            else if (elementName == NavigationNameClass.Contract)
                userControl = contractUserControl;
            else if (elementName == NavigationNameClass.Bookkeeping)
                userControl = bookkeepingUserControl;
            else if (elementName == NavigationNameClass.Total)
                userControl = totalUserControl;
            else if (elementName == NavigationNameClass.AKB)
                userControl = akbUserControl;
            tabbedView.AddDocument(userControl);
            tabbedView.ActivateDocument(userControl);
        }

        private bool ContainsControl(Control control)
        {
            foreach (BaseDocument document in documentManager.View.Documents)
            {
                if (document.Control == control)
                {
                    return true;
                }
            }
            return false;
        }

        void SetAccordionSelectedElement(DocumentEventArgs e)
        {
            if (tabbedView.Documents.Count != 0)
            {
                foreach (BaseDocument document in tabbedView.Documents)
                {
                    if (document.IsActive)
                    {
                        if (document.Caption == NavigationNameClass.Order)
                            accordionControl.SelectedElement = ordersAccordionControlElement;
                       if (document.Caption == NavigationNameClass.Customer)
                            accordionControl.SelectedElement = customerAccordionControlElement;
                        if (document.Caption == NavigationNameClass.Agreement)
                            accordionControl.SelectedElement = agreementAccordionControlElement;
                        if (document.Caption == NavigationNameClass.Confirm)
                            accordionControl.SelectedElement = confirmationAccordionControlElement;
                        if (document.Caption == NavigationNameClass.Contract)
                            accordionControl.SelectedElement = contractAccordionControlElement;
                        if (document.Caption == NavigationNameClass.Bookkeeping)
                            accordionControl.SelectedElement = bookkeepingAccordionControlElement;
                        if (document.Caption == NavigationNameClass.Total)
                            accordionControl.SelectedElement = totalAccordionControlElement;
                        if (document.Caption == NavigationNameClass.AKB)
                            accordionControl.SelectedElement = akbAccordionControlElement;
                    }
                }
            }
            else
            {
                accordionControl.SelectedElement = null;
            }
        }

        void accordionControl_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {
            if (e.Element == null) return;

            LoadUserControl(e.Element.Text);
        }
        void barButtonNavigation_ItemClick(object sender, ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            accordionControl.SelectedElement = mainAccordionGroup.Elements[barItemIndex];
        }
        void tabbedView_DocumentClosed(object sender, DocumentEventArgs e)
        {
            RecreateUserControls(e);
            SetAccordionSelectedElement(e);
        }
        
        void RecreateUserControls(DocumentEventArgs e)
        {
            if (e.Document.Caption == NavigationNameClass.Customer)
                customerUserControl = CreateUserControl(NavigationNameClass.Customer);
            else if (e.Document.Caption == NavigationNameClass.Order)
                orderUserControl = CreateUserControl(NavigationNameClass.Order);
            else if (e.Document.Caption == NavigationNameClass.Confirm)
                confirmationUserControl = CreateUserControl(NavigationNameClass.Confirm);
            else if (e.Document.Caption == NavigationNameClass.Agreement)
                agreementUserControl = CreateUserControl(NavigationNameClass.Agreement);
            else if (e.Document.Caption == NavigationNameClass.Contract)
                contractUserControl = CreateUserControl(NavigationNameClass.Contract);
            else if (e.Document.Caption == NavigationNameClass.Bookkeeping)
                bookkeepingUserControl = CreateUserControl(NavigationNameClass.Bookkeeping);
            else if (e.Document.Caption == NavigationNameClass.Total)
                totalUserControl = CreateUserControl(NavigationNameClass.Total);
            else if (e.Document.Caption == NavigationNameClass.AKB)
                akbUserControl = CreateUserControl(NavigationNameClass.AKB);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UserNameBarStaticItem.Caption = GlobalVariables.V_UserName;
            CurrentDayBarStatic.Caption = "Bu gün : " + DateTime.Now.ToString("dddd,d MMMM yyyy");
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(GlobalVariables.V_StyleName);
            Bitmap bm = new Bitmap(Properties.Resources.User_icon);
            notifyIcon.Icon = Icon.FromHandle(bm.GetHicon());
            notifyIcon.Text = "ELLONI Management System (Versiya " + GlobalVariables.V_Version + ")";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.BalloonTipTitle = "ELLONI Management System";
            notifyIcon.BalloonTipText = "Versiya " + GlobalVariables.V_Version;
            notifyIcon.ShowBalloonTip(1000);

            PageControl();
        }

        private void PageControl()
        {
            //List<UserGroupRole> lstUserGroupRole = UserGroupRoleDAL.SelectUserGroupRoleByGroupID(GlobalVariables.V_UserGroupID).ToList<UserGroupRole>();

            //for (int i = 0; i < lstUserGroupRole.Count; i++)
            //{
            //    switch (lstUserGroupRole[i].ROLE_ID)
            //    {
            //        case (int)DentalRoleEnum.Home:
            //            {
            //                dashboardAccordionControlElement.Visible = true;
            //                dashboardBarButtonItem.Visibility = BarItemVisibility.Always;
            //                accordionControl.SelectedElement = dashboardAccordionControlElement;
            //            }
            //            break;
            //        case (int)DentalRoleEnum.Customer:
            //            {
            //                customerAccordionControlElement.Visible = true;
            //                customersBarButtonItem.Visibility = BarItemVisibility.Always;
            //                cutsomerAccordionControlSeparator.Visible = dashboardAccordionControlElement.Visible;
            //                accordionControl.SelectedElement = accordionControl.SelectedElement == null ? customerAccordionControlElement : accordionControl.SelectedElement;
            //            }
            //            break;
            //        case (int)DentalRoleEnum.Doctor:
            //            {
            //                doctorAccordionControlElement.Visible = true;
            //                doctorBarButtonItem.Visibility = BarItemVisibility.Always;
            //                accordionControlSeparator5.Visible = customerAccordionControlElement.Visible || dashboardAccordionControlElement.Visible;
            //                accordionControl.SelectedElement = accordionControl.SelectedElement == null ? doctorAccordionControlElement : accordionControl.SelectedElement;
            //            }
            //            break;
            //        case (int)DentalRoleEnum.Schedule:
            //            {
            //                scheduleAccordionControlElement.Visible = true;
            //                scheduleBarButtonItem.Visibility = BarItemVisibility.Always;
            //                accordionControlSeparator6.Visible = customerAccordionControlElement.Visible ||
            //                                                        dashboardAccordionControlElement.Visible ||
            //                                                        doctorAccordionControlElement.Visible;
            //                accordionControl.SelectedElement = accordionControl.SelectedElement == null ? scheduleAccordionControlElement : accordionControl.SelectedElement;
            //            }
            //            break;
            //        //case (int)DentalRoleEnum.Treatment:
            //        //    {
            //        //        treatmentsAccordionControlElement.Visible = true;
            //        //        treatmentBarButtonItem.Visibility = BarItemVisibility.Always;
            //        //        accordionControlSeparator7.Visible = customerAccordionControlElement.Visible || 
            //        //                                                dashboardAccordionControlElement.Visible || 
            //        //                                                doctorAccordionControlElement.Visible ||
            //        //                                                scheduleAccordionControlElement.Visible;
            //        //        accordionControl.SelectedElement = accordionControl.SelectedElement == null ? treatmentsAccordionControlElement : accordionControl.SelectedElement;
            //        //    }
            //        //    break;
            //        case (int)DentalRoleEnum.Report:
            //            {
            //                reportAccordionControlElement.Visible = true;
            //                reportBarButtonItem.Visibility = BarItemVisibility.Always;
            //                accordionControlSeparator2.Visible = customerAccordionControlElement.Visible ||
            //                                                        dashboardAccordionControlElement.Visible ||
            //                                                        doctorAccordionControlElement.Visible ||
            //                                                        scheduleAccordionControlElement.Visible;
            //                accordionControl.SelectedElement = accordionControl.SelectedElement == null ? reportAccordionControlElement : accordionControl.SelectedElement;
            //            }
            //            break;
            //        case (int)DentalRoleEnum.Cabinet:
            //            {
            //                doctorCabinetAccordionControlElement.Visible = true;
            //                doctorCabinetBarButtonItem.Visibility = BarItemVisibility.Always;
            //                accordionControlSeparator8.Visible = customerAccordionControlElement.Visible ||
            //                                                        dashboardAccordionControlElement.Visible ||
            //                                                        doctorAccordionControlElement.Visible ||
            //                                                        scheduleAccordionControlElement.Visible ||
            //                                                        reportAccordionControlElement.Visible;
            //                accordionControl.SelectedElement = accordionControl.SelectedElement == null ? doctorCabinetAccordionControlElement : accordionControl.SelectedElement;
            //            }
            //            break;
            //        case (int)DentalRoleEnum.CallCenter:
            //            {
            //                callcenterAccordionControlElement.Visible = true;
            //                callCenterBarButtonItem.Visibility = BarItemVisibility.Always;
            //                accordionControlSeparator1.Visible = customerAccordionControlElement.Visible ||
            //                                                        dashboardAccordionControlElement.Visible ||
            //                                                        doctorAccordionControlElement.Visible ||
            //                                                        scheduleAccordionControlElement.Visible ||
            //                                                        reportAccordionControlElement.Visible ||
            //                                                        doctorCabinetAccordionControlElement.Visible;
            //                accordionControl.SelectedElement = accordionControl.SelectedElement == null ? callcenterAccordionControlElement : accordionControl.SelectedElement;
            //            }
            //            break;
            //    }
            //}

        }

        private void tabbedView_DocumentActivated(object sender, DocumentEventArgs e)
        {
            if (e.Document.Caption == NavigationNameClass.Customer)
                accordionControl.SelectedElement = customerAccordionControlElement;
            else if (e.Document.Caption == NavigationNameClass.Order)
                accordionControl.SelectedElement = ordersAccordionControlElement;
            else if (e.Document.Caption == NavigationNameClass.Agreement)
                accordionControl.SelectedElement = agreementAccordionControlElement;
            else if (e.Document.Caption == NavigationNameClass.Confirm)
                accordionControl.SelectedElement = confirmationAccordionControlElement;
            else if (e.Document.Caption == NavigationNameClass.Contract)
                accordionControl.SelectedElement = contractAccordionControlElement;
            else if (e.Document.Caption == NavigationNameClass.Bookkeeping)
                accordionControl.SelectedElement = bookkeepingAccordionControlElement;
            else if (e.Document.Caption == NavigationNameClass.Total)
                accordionControl.SelectedElement = totalAccordionControlElement;
            else if (e.Document.Caption == NavigationNameClass.AKB)
                accordionControl.SelectedElement = akbAccordionControlElement;
        }

        private void DictionaryBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.Dictionaries.FDictionaries fd = new Forms.Dictionaries.FDictionaries();
            fd.ShowDialog();
        }

        private void UserBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.Option.FUsers fu = new Forms.Option.FUsers();
            fu.ShowDialog();
        }
    }
}