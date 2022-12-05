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

namespace ELMS.Forms.General
{
    public partial class FDictionaries : DevExpress.XtraEditors.XtraForm
    {
        public FDictionaries()
        {
            InitializeComponent();
        }

        public int ViewSelectedTabIndex = 0;

        int topindex,
            old_row_id,
            documentTypeID,
            countryID,
            cardIssuingID,
            orderid;
        bool FormStatus = false;

        private void FDictionaries_Load(object sender, EventArgs e)
        {
            BackstageViewControl.SelectedTabIndex = ViewSelectedTabIndex;
            LoadDocumentType();
            FormStatus = true;
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
                }
            }
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


        //"Country" -a aid olan kod hissəsi burdan başlayır
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




    }
}