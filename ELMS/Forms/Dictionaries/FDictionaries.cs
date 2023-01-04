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
using ELMS.Class.DataAccess;
using DevExpress.XtraGrid.Views.Grid;
using ELMS.Class.Tables;
using ELMS.Class;
using static ELMS.Class.Enum;
using ELMS.Forms.Dictionaries;

namespace ELMS.Forms.Dictionaries
{
    public partial class FDictionaries : DevExpress.XtraEditors.XtraForm
    {
        public FDictionaries()
        {
            InitializeComponent();
        }
        public TransactionTypeEnum TransactionType;
        public int ViewSelectedTabIndex = 0;

        public delegate void DoEvent(int index);
        public event DoEvent RefreshList;

        int topindex,
            old_row_id,
            documentTypeID,
            countryID,
            phoneDescriptionID,
            cardIssuingID,
            productID,
            branchID,
            timesID,
            professionID,
            sourceID,
            orderid,
            kindShipID;
        bool FormStatus = false;

        private void FDictionaries_Load(object sender, EventArgs e)
        {
            BackstageViewControl.SelectedTabIndex = ViewSelectedTabIndex;
            LoadDocumentType();
            FormStatus = true;
            if (TransactionType == TransactionTypeEnum.Update)
                Edit();
        }

        void Edit()
        {
            switch (BackstageViewControl.SelectedTabIndex)
            {
                case 0:
                    LoadDocumentType();
                    break;
                case 1:
                    LoadCountry();
                    break;
                case 2:
                    LoadCardIssuing();
                    break;
                case 3:
                    LoadKindShip();
                    break;
                case 4:
                    LoadPhoneDescription();
                    break;
                case 5:
                    LoadProduct();
                    break;
                case 6:
                    LoadBranch();
                    break;
                case 7:
                    LoadProfession();
                    break;
                case 8:
                    LoadTimes();
                    break;
                case 9:
                    LoadSource();
                    break;
            }
        }

        private void LoadDocumentType()
        {
            DocumentTypeGridControl.DataSource = DocumentTypeDAL.SelectDocumentTypeByID(null).ToList<DocumentType>();            
        }

        private void RefreshSenedBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadDocumentType();
        }

        private void BackstageViewControl_SelectedTabChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            if (FormStatus)
                Edit();
        }

        private void LoadCardIssuing()
        {
            CardIssuingGridControl.DataSource = CardIssuingDAL.SelectCardIssuingByID(null).ToList<CardIssuing>();
        }
        private void LoadCountry()
        {
            CountriesGridControl.DataSource = CountriesDAL.SelectCountriesByID(null).ToList<Countries>();
        }

        private void DocumentTypeGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, DocumentType_SS, e);
        }

        private void DocumentTypeGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(DocumentTypeGridView, DocumentTypePopupMenu, e);
        }

        private void NewDocumentTypeBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFDocumentTypeAddEdit(TransactionTypeEnum.Insert, null);
        }

        void UpdateDocumentType()
        {
            LoadFDocumentTypeAddEdit(TransactionTypeEnum.Update, documentTypeID);
        }        

        private void DocumentTypeGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            documentTypeID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            //UpProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == 0);
            //DawnProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == (sender as GridView).RowCount - 1);

        }

        void DeleteDocumentType()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(DocumentTypeGridView, "USED_USER_ID"));
            if (UsedUserID < 0)
            {
                
                    if (GlobalFunctions.CallDialogResult("Seçilmiş Sənədi silmək istəyirsiniz?", "Sənədin silinməsi") == DialogResult.Yes)
                    DocumentTypeDAL.DeleteDocumentType(documentTypeID);
              }
            else
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                GlobalProcedures.ShowWarningMessage($@"Seçilmiş məlumat hal-hazırda {used_user_name} tərəfindən istifadə ediliyi üçün silinə bilməz.");
            }
        }

       

        private void DeleteDocumentTypeBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteDocumentType();
            LoadDocumentType();
        }


        private void DocumentTypeGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);
        }

        


        private void UpDocumentTypeBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("DOCUMENT_TYPE", documentTypeID, "up", out orderid);
            LoadDocumentType();
            DocumentTypeGridView.FocusedRowHandle = orderid - 1;
        }

        


        private void DownDocumentTypeBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("DOCUMENT_TYPE", documentTypeID, "down", out orderid);
            LoadDocumentType();
            DocumentTypeGridView.FocusedRowHandle = orderid - 1;
        }

        private void EditDocumentTypeBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateDocumentType();
        }

        private void LoadFDocumentTypeAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = DocumentTypeGridView.TopRowIndex;
            old_row_id = DocumentTypeGridView.FocusedRowHandle;
            FDocumentTypeAddEdit fd = new FDocumentTypeAddEdit()
            {
                TransactionType = transactionType,
                DocumentTypeID = id
            };
            fd.RefreshDataGridView += new FDocumentTypeAddEdit.DoEvent(LoadDocumentType);
            fd.ShowDialog();
            DocumentTypeGridView.TopRowIndex = topindex;
            DocumentTypeGridView.FocusedRowHandle = old_row_id;
        }
        //"DocumentType" -a aid olan kod hissəsi burda bitir
        ////////////////////////////////////////////
        ///////////////////////////////////////////
        //////////////////////////////////////////
        /////////////////////////////////////////
        ////////////////////////////////////////
        ///////////////////////////////////////
        //////////////////////////////////////
        /////////////////////////////////////
        ////////////////////////////////////
        ///////////////////////////////////
        //////////////////////////////////
        /////////////////////////////////
        ////////////////////////////////
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //"Country" -a aid olan kod hissəsi burdan başlayır


        private void NewCountriesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFCountryAddEdit(TransactionTypeEnum.Insert, null);
        }

  private void LoadFCountryAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = CountriesGridView.TopRowIndex;
            old_row_id = CountriesGridView.FocusedRowHandle;
            FCountryAddEdit fd = new FCountryAddEdit()
            {
                TransactionType = transactionType,
                CountryID = id
            };
            fd.RefreshDataGridView += new FCountryAddEdit.DoEvent(LoadCountry);
            fd.ShowDialog();
            CountriesGridView.TopRowIndex = topindex;
            CountriesGridView.FocusedRowHandle = old_row_id;
        }

        private void EditCountriesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateCountry();
        }

        void UpdateCountry()
        {
            LoadFCountryAddEdit(TransactionTypeEnum.Update, countryID);
        }
        
        private void DeleteCountriesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteCountry();
            LoadCountry();
        }

        void DeleteCountry()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(CountriesGridView, "USED_USER_ID"));
            if (UsedUserID < 0)
            {

                if (GlobalFunctions.CallDialogResult("Seçilmiş Ölkəni silmək istəyirsiniz?", "Ölkənin silinməsi") == DialogResult.Yes)
                    CountriesDAL.DeleteCountries(countryID);
            }
            else
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                GlobalProcedures.ShowWarningMessage($@"Seçilmiş məlumat hal-hazırda {used_user_name} tərəfindən istifadə ediliyi üçün silinə bilməz.");
            }
        }

        
        private void UpCountriesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("COUNTRY", countryID, "up", out orderid);
            LoadCountry();
            CountriesGridView.FocusedRowHandle = orderid - 1;
        }

        private void DownCountriesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("COUNTRY", countryID, "down", out orderid);
            LoadCountry();
            CountriesGridView.FocusedRowHandle = orderid - 1;
        }


        private void CountriesGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Country_SS, e);
        }

        private void CountriesGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(CountriesGridView, CountriesPopupMenu, e);
        }

        private void CountriesGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            countryID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            //UpProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == 0);
            //DawnProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == (sender as GridView).RowCount - 1);
        }

        private void CountriesGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);
        }

        //"Country" -a aid olan kod hissəsi burda bitir
        //.////////////////////////////////////////////
        //.///////////////////////////////////////////
        //.//////////////////////////////////////////
        //./////////////////////////////////////////
        //.////////////////////////////////////////
        //.///////////////////////////////////////
        //.//////////////////////////////////////
        //./////////////////////////////////////
        //.////////////////////////////////////
        //.///////////////////////////////////
        //.//////////////////////////////////
        //./////////////////////////////////
        //.////////////////////////////////

        private void NewCardIssuingBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFCardIssuingAddEdit(TransactionTypeEnum.Insert, null);

        }

        private void LoadFCardIssuingAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = CardIssuingGridView.TopRowIndex;
            old_row_id = CardIssuingGridView.FocusedRowHandle;
            FCardIssuingAddEdit fd = new FCardIssuingAddEdit()
            {
                TransactionType = transactionType,
                CardIssuingID = id
            };
            fd.RefreshDataGridView += new FCardIssuingAddEdit.DoEvent(LoadCardIssuing);
            fd.ShowDialog();
            CardIssuingGridView.TopRowIndex = topindex;
            CardIssuingGridView.FocusedRowHandle = old_row_id;
        }

        private void EditCardIssuingBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateCardIssuing();
        }

        void UpdateCardIssuing()
        {
            LoadFCardIssuingAddEdit(TransactionTypeEnum.Update, cardIssuingID);

        }

        private void DeleteCardIssuingBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteCardIssuing();
            LoadCardIssuing();
        }

        void DeleteCardIssuing()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(CardIssuingGridView, "USED_USER_ID"));
            if (UsedUserID < 0)
            {

                if (GlobalFunctions.CallDialogResult("Seçilmiş Orqanı silmək istəyirsiniz?", "Orqanın silinməsi") == DialogResult.Yes)
                    CardIssuingDAL.DeleteCardIssuing(cardIssuingID);
            }
            else
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                GlobalProcedures.ShowWarningMessage($@"Seçilmiş məlumat hal-hazırda {used_user_name} tərəfindən istifadə ediliyi üçün silinə bilməz.");
            }
        }

        private void UpCardIssuingBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("CARD_ISSUING", cardIssuingID, "up", out orderid);
            LoadCardIssuing();
            CardIssuingGridView.FocusedRowHandle = orderid - 1;
        }

        private void CardIssuingGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditCardIssuingBarButton.Enabled)
                UpdateCardIssuing();
        }

        private void DownCardIssuingBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("CARD_ISSUING", cardIssuingID, "down", out orderid);
            LoadCardIssuing();
            CardIssuingGridView.FocusedRowHandle = orderid - 1;
        }

        private void CardIssuingGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, CardIssuing_SS, e);

        }

        private void CardIssuingGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            cardIssuingID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            //UpProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == 0);
            //DawnProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == (sender as GridView).RowCount - 1);

        }

        private void CardIssuingGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(CardIssuingGridView, CardIssuingPopupMenu, e);

        }


        private void CardIssuingGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);

        }


        //"CardIssuing" -a aid olan kod hissəsi burda bitir
        //.////////////////////////////////////////////
        //.///////////////////////////////////////////
        //.//////////////////////////////////////////
        //./////////////////////////////////////////
        //.////////////////////////////////////////
        //.///////////////////////////////////////
        //.//////////////////////////////////////
        //./////////////////////////////////////
        //.////////////////////////////////////
        //.///////////////////////////////////
        //.//////////////////////////////////
        //./////////////////////////////////
        //.////////////////////////////////

        private void LoadKindShip()
        {
            KindShipGridControl.DataSource = KindShipDAL.SelectKindShipByID(null).ToList<KindShip>();
        }

        private void NewKindShipBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFKindShipAddEdit(TransactionTypeEnum.Insert, null);

        }

        private void LoadFKindShipAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = KindShipGridView.TopRowIndex;
            old_row_id = KindShipGridView.FocusedRowHandle;
            FKindShipAddEdit fd = new FKindShipAddEdit()
            {
                TransactionType = transactionType,
                KindShipID = id
            };
            fd.RefreshDataGridView += new FKindShipAddEdit.DoEvent(LoadKindShip);
            fd.ShowDialog();
            KindShipGridView.TopRowIndex = topindex;
            KindShipGridView.FocusedRowHandle = old_row_id;
        }

        private void EditKindShipBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateKindShip();
        }

        private void KindShipGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditKindShipBarButton.Enabled)
                UpdateKindShip();
        }

        void UpdateKindShip()
        {
            LoadFKindShipAddEdit(TransactionTypeEnum.Update, kindShipID);

        }

        private void DeleteKindShipBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteKindShip();
            LoadKindShip();
        }

        void DeleteKindShip()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(KindShipGridView, "USED_USER_ID"));
            if (UsedUserID < 0)
            {

                if (GlobalFunctions.CallDialogResult("Seçilmiş Qohumluq dərəcəsini silmək istəyirsiniz?", "Qohumluq dərəcəsinin silinməsi") == DialogResult.Yes)
                    KindShipDAL.DeleteKindShip(kindShipID);
            }
            else
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                GlobalProcedures.ShowWarningMessage($@"Seçilmiş məlumat hal-hazırda {used_user_name} tərəfindən istifadə ediliyi üçün silinə bilməz.");
            }
        }

       

        private void UpKindShipBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("KINDSHIP_RATE", kindShipID, "up", out orderid);
            LoadKindShip();
            KindShipGridView.FocusedRowHandle = orderid - 1;
        }

        private void DownKindShipBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("KINDSHIP_RATE", kindShipID, "down", out orderid);
            LoadKindShip();
            KindShipGridView.FocusedRowHandle = orderid - 1;
        }

        private void KindShipGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, KindShip_SS, e);

        }

        private void KindShipGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            kindShipID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            //UpProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == 0);
            //DawnProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == (sender as GridView).RowCount - 1);

        }

        private void KindShipGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(KindShipGridView, KindShipPopupMenu, e);

        }


        private void KindShipGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);

        }

        //"KindShip" -a aid olan kod hissəsi burda bitir
        //.////////////////////////////////////////////
        //.///////////////////////////////////////////
        //.//////////////////////////////////////////
        //./////////////////////////////////////////
        //.////////////////////////////////////////
        //.///////////////////////////////////////
        //.//////////////////////////////////////
        //./////////////////////////////////////
        //.////////////////////////////////////
        //.///////////////////////////////////
        //.//////////////////////////////////
        //./////////////////////////////////
        //.////////////////////////////////


        private void LoadPhoneDescription()
        {
            PhoneDescriptionGridControl.DataSource = PhoneDescriptionDAL.SelectPhoneDescriptionByID(null).ToList<PhoneDescription>();

            if (PhoneDescriptionGridView.RowCount > 0)
            {
                EditPhoneDescriptionBarButton.Enabled = DeletePhoneDescriptionBarButton.Enabled = true;
                UpPhoneDescriptionBarButton.Enabled = !(PhoneDescriptionGridView.FocusedRowHandle == 0);
                DownPhoneDescriptionBarButton.Enabled = (PhoneDescriptionGridView.RowCount > 1);
            }
            else
                EditPhoneDescriptionBarButton.Enabled =
                    DeletePhoneDescriptionBarButton.Enabled =
                    UpPhoneDescriptionBarButton.Enabled =
                    DownPhoneDescriptionBarButton.Enabled = false;
        }

        private void PhoneDescriptionGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, PhoneDescription_SS, e);
        }

        private void PhoneDescriptionGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            phoneDescriptionID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            UpPhoneDescriptionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == 0);
            DownPhoneDescriptionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == (sender as GridView).RowCount - 1);
        }

        private void LoadFPhoneDescriptionAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = PhoneDescriptionGridView.TopRowIndex;
            old_row_id = PhoneDescriptionGridView.FocusedRowHandle;
            FPhoneDescriptionsAddEdit ft = new FPhoneDescriptionsAddEdit()
            {
                TransactionType = transactionType,
                DescriptionID = id
            };
            ft.RefreshDataGridView += new FPhoneDescriptionsAddEdit.DoEvent(LoadPhoneDescription);
            ft.ShowDialog();
            PhoneDescriptionGridView.TopRowIndex = topindex;
            PhoneDescriptionGridView.FocusedRowHandle = old_row_id;
        }

        private void NewPhoneDescriptionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFPhoneDescriptionAddEdit(TransactionTypeEnum.Insert, null);
        }

        void UpdatePhoneDescription()
        {
            LoadFPhoneDescriptionAddEdit(TransactionTypeEnum.Update, phoneDescriptionID);
        }

        private void DocumentTypeGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditDocumentTypeBarButton.Enabled)
                UpdateDocumentType();
        }

        private void CountriesGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditCountriesBarButton.Enabled)
                UpdateCountry();
        }

        private void PhoneDescriptionGridView_DoubleClick_1(object sender, EventArgs e)
        {
            if (EditPhoneDescriptionBarButton.Enabled)
                UpdatePhoneDescription();
        }

        

        

        

        private void EditPhoneDescriptionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdatePhoneDescription();
        }

        private void PhoneDescriptionGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditPhoneDescriptionBarButton.Enabled)
                UpdatePhoneDescription();
        }

        private void RefreshPhoneDescriptionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPhoneDescription();
        }

        private void UpPhoneDescriptionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("PHONE_DESCRIPTIONS", phoneDescriptionID, "up", out orderid);
            LoadPhoneDescription();
            PhoneDescriptionGridView.FocusedRowHandle = orderid - 1;
        }

        private void DownPhoneDescriptionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("PHONE_DESCRIPTIONS", phoneDescriptionID, "down", out orderid);
            LoadPhoneDescription();
            PhoneDescriptionGridView.FocusedRowHandle = orderid - 1;
        }
        private void DeletePhoneDescriptionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        private void PhoneDescriptionGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(PhoneDescriptionGridView, PhoneDescriptionPopupMenu, e);
        }

        //"PhoneDescriptions" -a aid olan kod hissəsi burda bitir
        //.////////////////////////////////////////////
        //.///////////////////////////////////////////
        //.//////////////////////////////////////////
        //./////////////////////////////////////////
        //.////////////////////////////////////////
        //.///////////////////////////////////////
        //.//////////////////////////////////////
        //./////////////////////////////////////
        //.////////////////////////////////////
        //.///////////////////////////////////
        //.//////////////////////////////////
        //./////////////////////////////////
        //.////////////////////////////////
        //"Product" burdan Baslayir


        private void LoadProduct()
        {
             ProductGridControl.DataSource = ProductDAL.SelectProductByID(null).ToList<Product>();
        }


        private void NewProductBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFProductAddEdit(TransactionTypeEnum.Insert, null);

        }

        private void LoadFProductAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = ProductGridView.TopRowIndex;
            old_row_id = ProductGridView.FocusedRowHandle;
            FProductAddEdit fd = new FProductAddEdit()
            {
                TransactionType = transactionType,
                ProductID = id
            };
            fd.RefreshDataGridView += new FProductAddEdit.DoEvent(LoadProduct);
            fd.ShowDialog();
            ProductGridView.TopRowIndex = topindex;
            ProductGridView.FocusedRowHandle = old_row_id;
        }

        private void EditProductBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateProduct();
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

        private void DeleteProductBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteProduct();
            LoadProduct();
        }

        void DeleteProduct()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(ProductGridView, "USED_USER_ID"));
            if (UsedUserID < 0)
            {

                if (GlobalFunctions.CallDialogResult("Seçilmiş Məhsulu silmək istəyirsiniz?", "Məhsulun silinməsi") == DialogResult.Yes)
                    ProductDAL.DeleteProduct(productID);
            }
            else
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                GlobalProcedures.ShowWarningMessage($@"Seçilmiş məlumat hal-hazırda {used_user_name} tərəfindən istifadə ediliyi üçün silinə bilməz.");
            }
        }

        private void RefreshProductBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadProduct();
        }

        private void UpProductBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("PRODUCT", productID, "up", out orderid);
            LoadProduct();
            ProductGridView.FocusedRowHandle = orderid - 1;
        }

        private void DownProductBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("PRODUCT", productID, "down", out orderid);
            LoadProduct();
            ProductGridView.FocusedRowHandle = orderid - 1;
        }

        private void ProductGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Product_SS, e);

        }

        private void ProductGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            productID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            //UpProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == 0);
            //DawnProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == (sender as GridView).RowCount - 1);

        }

        private void ProductGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(ProductGridView, ProductPopupMenu, e);

        }


        private void ProductGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);

        }

        //"Product" -a aid olan kod hissəsi burda bitir
        //.////////////////////////////////////////////
        //.///////////////////////////////////////////
        //.//////////////////////////////////////////
        //./////////////////////////////////////////
        //.////////////////////////////////////////
        //.///////////////////////////////////////
        //.//////////////////////////////////////
        //./////////////////////////////////////
        //.////////////////////////////////////
        //.///////////////////////////////////
        //.//////////////////////////////////
        //./////////////////////////////////
        //.////////////////////////////////
        //"Branch" burdan baslayir


        private void LoadBranch()
        {
            BranchGridControl.DataSource = BranchDAL.SelectBranchByID(null).ToList<Branch>();
        }


        private void NewBranchBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFBranchAddEdit(TransactionTypeEnum.Insert, null);

        }

        private void LoadFBranchAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = BranchGridView.TopRowIndex;
            old_row_id = BranchGridView.FocusedRowHandle;
            FBranchAddEdit fd = new FBranchAddEdit()
            {
                TransactionType = transactionType,
                BranchID = id
            };
            fd.RefreshDataGridView += new FBranchAddEdit.DoEvent(LoadBranch);
            fd.ShowDialog();
            BranchGridView.TopRowIndex = topindex;
            BranchGridView.FocusedRowHandle = old_row_id;
        }

        private void EditBranchBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateBranch();
        }

        private void BranchGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditBranchBarButton.Enabled)
                UpdateBranch();
        }

        void UpdateBranch()
        {
            LoadFBranchAddEdit(TransactionTypeEnum.Update, branchID);

        }

        private void DeleteBranchBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteBranch();
            LoadBranch();
        }

        void DeleteBranch()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(BranchGridView, "USED_USER_ID"));
            if (UsedUserID < 0)
            {

                if (GlobalFunctions.CallDialogResult("Seçilmiş Filialı silmək istəyirsiniz?", "Filialın silinməsi") == DialogResult.Yes)
                    BranchDAL.DeleteBranch(branchID);
            }
            else
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                GlobalProcedures.ShowWarningMessage($@"Seçilmiş məlumat hal-hazırda {used_user_name} tərəfindən istifadə ediliyi üçün silinə bilməz.");
            }
        }

        private void RefreshBranchBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadBranch();
        }

        private void UpBranchBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("BRANCH", branchID, "up", out orderid);
            LoadBranch();
            BranchGridView.FocusedRowHandle = orderid - 1;
        }

        private void DownBranchBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("BRANCH", branchID, "down", out orderid);
            LoadBranch();
            BranchGridView.FocusedRowHandle = orderid - 1;
        }

        private void BranchGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Branch_SS, e);

        }

        private void BranchGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            branchID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            //UpProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == 0);
            //DawnProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == (sender as GridView).RowCount - 1);

        }

        private void BranchGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(BranchGridView, BranchPopupMenu, e);

        }


        private void BranchGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);

        }

        //"Branch" -a aid olan kod hissəsi burda bitir
        //.////////////////////////////////////////////
        //.///////////////////////////////////////////
        //.//////////////////////////////////////////
        //./////////////////////////////////////////
        //.////////////////////////////////////////
        //.///////////////////////////////////////
        //.//////////////////////////////////////
        //./////////////////////////////////////
        //.////////////////////////////////////
        //.///////////////////////////////////
        //.//////////////////////////////////
        //./////////////////////////////////
        //.////////////////////////////////
        //"Profession" burdan baslayir

        private void LoadProfession()
        {
            ProfessionGridControl.DataSource = ProfessionDAL.SelectProfessionByID(null).ToList<Profession>();
        }


        private void NewProfessionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFProfessionAddEdit(TransactionTypeEnum.Insert, null);

        }

        private void LoadFProfessionAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = ProfessionGridView.TopRowIndex;
            old_row_id = ProfessionGridView.FocusedRowHandle;
            FProfessionAddEdit fd = new FProfessionAddEdit()
            {
                TransactionType = transactionType,
                ProfessionID = id
            };
            fd.RefreshDataGridView += new FProfessionAddEdit.DoEvent(LoadProfession);
            fd.ShowDialog();
            ProfessionGridView.TopRowIndex = topindex;
            ProfessionGridView.FocusedRowHandle = old_row_id;
        }

        private void EditProfessionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateProfession();
        }

        private void ProfessionGridView_DoubleClick(object sender, EventArgs e)
        {
                if (EditProfessionBarButton.Enabled)
                    UpdateProfession();            
        }

        void UpdateProfession()
        {
            LoadFProfessionAddEdit(TransactionTypeEnum.Update, professionID);

        }

        private void DeleteProfessionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteProfession();
            LoadProfession();
        }

        void DeleteProfession()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(ProfessionGridView, "USED_USER_ID"));
            if (UsedUserID < 0)
            {

                if (GlobalFunctions.CallDialogResult("Seçilmiş Peşəni silmək istəyirsiniz?", "Peşənin silinməsi") == DialogResult.Yes)
                    ProfessionDAL.DeleteProfession(professionID);
            }
            else
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                GlobalProcedures.ShowWarningMessage($@"Seçilmiş məlumat hal-hazırda {used_user_name} tərəfindən istifadə ediliyi üçün silinə bilməz.");
            }
        }

        private void RefreshProfessionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadProfession();
        }

        private void UpProfessionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("PROFESSION", professionID, "up", out orderid);
            LoadProfession();
            ProfessionGridView.FocusedRowHandle = orderid - 1;
        }

        private void DownProfessionBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("PROFESSION", professionID, "down", out orderid);
            LoadProfession();
            ProfessionGridView.FocusedRowHandle = orderid - 1;
        }

        private void ProfessionGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Profession_SS, e);

        }

        private void ProfessionGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            professionID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            //UpProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == 0);
            //DawnProfessionBarButton.Enabled = !((sender as GridView).FocusedRowHandle == (sender as GridView).RowCount - 1);

        }

        private void ProfessionGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(ProfessionGridView, ProfessionPopupMenu, e);

        }


        private void ProfessionGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);

        }
        //"Profession" -a aid olan kod hissəsi burda bitir
        //.////////////////////////////////////////////
        //.///////////////////////////////////////////
        //.//////////////////////////////////////////
        //./////////////////////////////////////////
        //.////////////////////////////////////////
        //.///////////////////////////////////////
        //.//////////////////////////////////////
        //./////////////////////////////////////
        //.////////////////////////////////////
        //.///////////////////////////////////
        //.//////////////////////////////////
        //./////////////////////////////////
        //.////////////////////////////////
        //"Times" burdan baslayir



        private void LoadTimes()
        {
            TimesGridControl.DataSource = TimesDAL.SelectTimesByID(null).ToList<Times>();
        }


        private void NewTimesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFTimesAddEdit(TransactionTypeEnum.Insert, null);

        }

        private void LoadFTimesAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = TimesGridView.TopRowIndex;
            old_row_id = TimesGridView.FocusedRowHandle;
            FTimesAddEdit fd = new FTimesAddEdit()
            {
                TransactionType = transactionType,
                TimesID = id
            };
            fd.RefreshDataGridView += new FTimesAddEdit.DoEvent(LoadTimes);
            fd.ShowDialog();
            TimesGridView.TopRowIndex = topindex;
            TimesGridView.FocusedRowHandle = old_row_id;
        }

        private void EditTimesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateTimes();
        }

        private void TimesGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditTimesBarButton.Enabled)
                UpdateTimes();
        }

        void UpdateTimes()
        {
            LoadFTimesAddEdit(TransactionTypeEnum.Update, timesID);

        }

        private void DeleteTimesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTimes();
            LoadTimes();
        }

        void DeleteTimes()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(TimesGridView, "USED_USER_ID"));
            if (UsedUserID < 0)
            {

                if (GlobalFunctions.CallDialogResult("Seçilmiş Müddəti silmək istəyirsiniz?", "Müddətin silinməsi") == DialogResult.Yes)
                    TimesDAL.DeleteTimes(timesID);
            }
            else
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                GlobalProcedures.ShowWarningMessage($@"Seçilmiş məlumat hal-hazırda {used_user_name} tərəfindən istifadə ediliyi üçün silinə bilməz.");
            }
        }

        private void RefreshTimesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadTimes();
        }

        private void UpTimesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("TIMES", timesID, "up", out orderid);
            LoadTimes();
            TimesGridView.FocusedRowHandle = orderid - 1;
        }

        private void DownTimesBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("TIMES", timesID, "down", out orderid);
            LoadTimes();
            TimesGridView.FocusedRowHandle = orderid - 1;
        }

        private void TimesGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Times_SS, e);

        }

        private void TimesGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            timesID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            //UpTimesBarButton.Enabled = !((sender as GridView).FocusedRowHandle == 0);
            //DawnTimesBarButton.Enabled = !((sender as GridView).FocusedRowHandle == (sender as GridView).RowCount - 1);

        }

        private void TimesGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(TimesGridView, TimesPopupMenu, e);

        }


        private void TimesGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);

        }
        

        //"Times" -a aid olan kod hissəsi burda bitir
        //.////////////////////////////////////////////
        //.///////////////////////////////////////////
        //.//////////////////////////////////////////
        //./////////////////////////////////////////
        //.////////////////////////////////////////
        //.///////////////////////////////////////
        //.//////////////////////////////////////
        //./////////////////////////////////////
        //.////////////////////////////////////
        //.///////////////////////////////////
        //.//////////////////////////////////
        //./////////////////////////////////
        //.////////////////////////////////
        //"Source" burdan baslayir



        private void LoadSource()
        {
            SourceGridControl.DataSource = SourceDAL.SelectSourceByID(null).ToList<Source>();
        }


        private void NewSourceBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadFSourceAddEdit(TransactionTypeEnum.Insert, null);

        }

        private void LoadFSourceAddEdit(TransactionTypeEnum transactionType, int? id)
        {
            topindex = SourceGridView.TopRowIndex;
            old_row_id = SourceGridView.FocusedRowHandle;
            FSourceAddEdit fd = new FSourceAddEdit()
            {
                TransactionType = transactionType,
                SourceID = id
            };
            fd.RefreshDataGridView += new FSourceAddEdit.DoEvent(LoadSource);
            fd.ShowDialog();
            SourceGridView.TopRowIndex = topindex;
            SourceGridView.FocusedRowHandle = old_row_id;
        }

        private void EditSourceBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateSource();
        }

        private void SourceGridView_DoubleClick(object sender, EventArgs e)
        {
            if (EditSourceBarButton.Enabled)
                UpdateSource();
        }

        void UpdateSource()
        {
            LoadFSourceAddEdit(TransactionTypeEnum.Update, sourceID);

        }

        private void DeleteSourceBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteSource();
            LoadSource();
        }

        void DeleteSource()
        {
            int UsedUserID = Convert.ToInt16(GlobalFunctions.GetGridRowCellValue(SourceGridView, "USED_USER_ID"));
            if (UsedUserID < 0)
            {

                if (GlobalFunctions.CallDialogResult("Seçilmiş Mənbəni silmək istəyirsiniz?", "Mənbənin silinməsi") == DialogResult.Yes)
                    SourceDAL.DeleteSource(sourceID);
            }
            else
            {
                string used_user_name = GlobalVariables.lstUsers.Find(u => u.ID == UsedUserID).FULL_NAME;
                GlobalProcedures.ShowWarningMessage($@"Seçilmiş məlumat hal-hazırda {used_user_name} tərəfindən istifadə ediliyi üçün silinə bilməz.");
            }
        }

        private void RefreshSourceBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadSource();
        }

        private void UpSourceBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("SOURCE", sourceID, "up", out orderid);
            LoadSource();
            SourceGridView.FocusedRowHandle = orderid - 1;
        }

        private void DownSourceBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GlobalProcedures.ChangeOrderID("SOURCE", sourceID, "down", out orderid);
            LoadSource();
            SourceGridView.FocusedRowHandle = orderid - 1;
        }

        private void SourceGridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GlobalProcedures.GenerateAutoRowNumber(sender, Source_SS, e);
        }

        private void SourceGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            sourceID = Convert.ToInt32(GlobalFunctions.GetGridRowCellValue((sender as GridView), "ID"));
            //UpSourceBarButton.Enabled = !((sender as GridView).FocusedRowHandle == 0);
            //DawnSourceBarButton.Enabled = !((sender as GridView).FocusedRowHandle == (sender as GridView).RowCount - 1);

        }

        private void SourceGridView_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalProcedures.GridMouseUpForPopupMenu(SourceGridView, SourcePopupMenu, e);

        }
        
        private void SourceGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GlobalProcedures.GridRowCellStyleForBlock((sender as GridView), e);

        }
        

    }
}