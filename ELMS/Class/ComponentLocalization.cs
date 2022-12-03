using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars.Localization;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraTreeList.Localization;

namespace ELMS.Class
{
    class ComponentLocalization
    {
        public class CustomGridLocalizer : GridLocalizer
        {
            public override string GetLocalizedString(GridStringId id)
            {
                if (id == GridStringId.FilterBuilderApplyButton)
                    return "Tətbiq et";
                if (id == GridStringId.FilterBuilderCancelButton)
                    return "Bağla";
                if (id == GridStringId.FilterBuilderCaption)
                    return "Filtrlərin yaradılması";
                if (id == GridStringId.FilterPanelCustomizeButton)
                    return "Filtri dəyiş";
                if (id == GridStringId.MenuColumnBestFit)
                    return "Seçilmiş sütunun enini cədvələ görə uyğunlaşdır";
                if (id == GridStringId.MenuColumnBestFitAllColumns)
                    return "Bütün sütunların enini cədvələ görə uyğunlaşdır";
                if (id == GridStringId.MenuColumnClearAllSorting)
                    return "Bütün sıralamaları sil";
                if (id == GridStringId.MenuColumnClearFilter)
                    return "Filtri sil";
                if (id == GridStringId.MenuColumnClearSorting)
                    return "Sıralamanı sil";
                if (id == GridStringId.MenuColumnFilterEditor)
                    return "Filtrlər";
                if (id == GridStringId.MenuColumnSortAscending)
                    return "Artan sırada düz";
                if (id == GridStringId.MenuColumnSortDescending)
                    return "Azalan sırada düz";
                if (id == GridStringId.MenuColumnFindFilterShow)
                    return "Axtarış panelini göstər";
                if (id == GridStringId.MenuColumnFindFilterHide)
                    return "Axtarış panelini gizlət";
                if (id == GridStringId.MenuColumnAutoFilterRowShow)
                    return "Avtomatik axtarış panelini göstər";
                if (id == GridStringId.MenuColumnAutoFilterRowHide)
                    return "Avtomatik axtarış panelini gizlət";
                if (id == GridStringId.MenuColumnRemoveColumn)
                    return "Sütunu gizlət";
                if (id == GridStringId.MenuColumnShowColumn)
                    return "Sütunu göstər";
                if (id == GridStringId.MenuColumnGroup)
                    return "Sütunu qruplaşdır";
                if (id == GridStringId.MenuColumnUnGroup)
                    return "Sütunu qrupdan çıxart";
                if (id == GridStringId.MenuGroupPanelShow)
                    return "Qrup panelini göstər";
                if (id == GridStringId.MenuGroupPanelHide)
                    return "Qrup panelini gizlət";
                if (id == GridStringId.MenuGroupPanelClearGrouping)
                    return "Bütün sütunları qruplaşmadan sil";
                if (id == GridStringId.MenuGroupPanelFullCollapse)
                    return "Qruplaşmış sütunu bağla";
                if (id == GridStringId.MenuGroupPanelFullExpand)
                    return "Qruplaşmış sütunu aç";
                if (id == GridStringId.MenuColumnColumnCustomization)
                    return "Gizlədilmiş sütunlar";
                if (id == GridStringId.CheckboxSelectorColumnCaption)
                    return "Bütün sətirləri seç";
                if (id == GridStringId.FindNullPrompt)
                    return "Açar sözü daxil edin";
                if (id == GridStringId.FindControlClearButton)
                    return "Təmizlə";
                if (id == GridStringId.FindControlFindButton)
                    return "Axtar";
                if (id == GridStringId.GridGroupPanelText)
                    return "Qruplaşdırmaq istədiyiniz sütunu buraya sürükləyib buraxın";
                if (id == GridStringId.CustomizationCaption)
                    return "Gizlədilmiş sütunlar";
                if (id == GridStringId.MenuFooterSum)
                    return "Cəm";
                if (id == GridStringId.MenuFooterSumFormat)
                    return "Cəm = {0}";
                if (id == GridStringId.MenuFooterMax)
                    return "Ən böyük";
                if (id == GridStringId.MenuFooterMaxFormat)
                    return "Ən böyük = {0}";
                if (id == GridStringId.MenuFooterMin)
                    return "Ən kiçik";
                if (id == GridStringId.MenuFooterMinFormat)
                    return "Ən kiçik = {0}";
                if (id == GridStringId.MenuFooterAverage)
                    return "Ədədi orta";
                if (id == GridStringId.MenuFooterAverageFormat)
                    return "Ədədi orta = {0}";
                if (id == GridStringId.MenuFooterNone)
                    return "Hesablama yoxdur";
                if (id == GridStringId.MenuFooterCount)
                    return "Say";
                if (id == GridStringId.MenuFooterCountFormat)
                    return "Say = {0}";
                if (id == GridStringId.MenuFooterAddSummaryItem)
                    return "Digər hesablama əlavə et";
                if (id == GridStringId.MenuFooterClearSummaryItems)
                    return "Hesablamaları sil";
                if (id == GridStringId.PopupFilterCustom)
                    return "(Xüsusi filtr)";
                if (id == GridStringId.PopupFilterBlanks)
                    return "(Boş olanlar)";
                if (id == GridStringId.PopupFilterNonBlanks)
                    return "(Boş olmayanlar)";
                if (id == GridStringId.PopupFilterAll)
                    return "Hamısı";
                if (id == GridStringId.CustomFilterDialogFormCaption)
                    return "Xüsusi filtrlər";
                if (id == GridStringId.CustomFilterDialogEmptyValue)
                    return "dəyəri daxil et";
                if (id == GridStringId.CustomFilterDialogRadioAnd)
                    return "Və";
                if (id == GridStringId.CustomFilterDialogRadioOr)
                    return "Və ya";
                if (id == GridStringId.CustomFilterDialogCancelButton)
                    return "İmtina et";
                if (id == GridStringId.CustomFilterDialogOkButton)
                    return "Təsdiq et";
                if (id == GridStringId.CustomFilterDialogCaption)
                    return "Şərt sətirində göstər";
                if (id == GridStringId.EditFormUpdateButton)
                    return "Dəyiş";
                if (id == GridStringId.EditFormCancelButton)
                    return "Bağla";
                return base.GetLocalizedString(id);
            }
        }

        public class StringLocalizer : Localizer
        {
            public override string GetLocalizedString(StringId id)
            {
                if (id == StringId.XtraMessageBoxYesButtonText)
                    return "Bəli";
                if (id == StringId.XtraMessageBoxNoButtonText)
                    return "Xeyr";
                if (id == StringId.XtraMessageBoxCancelButtonText)
                    return "Bağla";
                if (id == StringId.FilterAggregateAvg)
                    return "Ədədi orta";
                if (id == StringId.FilterShowAll)
                    return "Hamısını seç";
                if (id == StringId.Cancel)
                    return "Bağla";
                if (id == StringId.OK)
                    return "Tətbiq et";
                if (id == StringId.DateEditClear)
                    return "Təmizlə";
                if (id == StringId.DateEditToday)
                    return "Bu gün";
                if (id == StringId.TextEditMenuUndo)
                    return "Geri qaytar";
                if (id == StringId.TextEditMenuSelectAll)
                    return "Hamısını seç";
                if (id == StringId.TextEditMenuPaste)
                    return "Yapışdır";
                if (id == StringId.TextEditMenuDelete)
                    return "Sil";
                if (id == StringId.TextEditMenuCut)
                    return "Kəs";
                if (id == StringId.TextEditMenuCopy)
                    return "Surətini çıxar";
                if(id == StringId.PictureEditMenuCopy)
                    return "Surətini çıxar";
                if (id == StringId.PictureEditMenuDelete)
                    return "Sil";
                if (id == StringId.PictureEditMenuCut)
                    return "Kəs";
                if (id == StringId.PictureEditMenuPaste)
                    return "Yapışdır";
                if (id == StringId.PictureEditMenuSave)
                    return "Yadda saxla";
                if (id == StringId.PictureEditMenuLoad)
                    return "Yüklə";
                if (id == StringId.TakePictureDialogCapture)
                    return "Çək";
                if (id == StringId.TakePictureDialogSave)
                    return "Təsdiqlə";
                if (id == StringId.TakePictureDialogTitle)
                    return "Kamera";
                if (id == StringId.TakePictureMenuItem)
                    return "Kamera ilə çək";
                if (id == StringId.InvalidValueText)
                    return "Format düz deyil";
                if (id == StringId.CameraSettingsCaption)
                    return "Kamera sazlamaları";
                if (id == StringId.CameraSettingsDefaults)
                    return "İlkin sazlamalara qaytar";
                return base.GetLocalizedString(id);
            }
        }

        public class CustomBarLocalizer : BarLocalizer
        {
            public override string GetLocalizedString(BarString id)
            {
                if (id == BarString.SkinsBonus)
                    return "Bonus görünüşlər";
                if (id == BarString.SkinsOffice)
                    return "Office görünüşlər";
                if (id == BarString.SkinsTheme)
                    return "Mövzu gürünüşlər";
                if (id == BarString.SkinsMain)
                    return "Standart gürünüşlər";
                if (id == BarString.SkinCaptions)
                    return "Üzlüklər";
                if (id == BarString.RibbonToolbarMinimizeRibbon)
                    return "Paneli gizlət (Ctrl + F1)";
                return base.GetLocalizedString(id);
            }
        }

        public class CustomDockManagerLocalizer : DockManagerResXLocalizer
        {
            public override string GetLocalizedString(DockManagerStringId id)
            {
                if (id == DockManagerStringId.CommandClose)
                    return "Bağla";
                if (id == DockManagerStringId.CommandAutoHide)
                    return "Gizlət";
                if (id == DockManagerStringId.CommandDock)
                    return "İlkin vəziyyəti";
                if (id == DockManagerStringId.CommandFloat)
                    return "Yer dəyişdir";
                if (id == DockManagerStringId.CommandMaximize)
                    return "Tam ölçü";
                if (id == DockManagerStringId.CommandRestore)
                    return "Geri qaytar";
                return base.GetLocalizedString(id);
            }
        }

        public class CustomTreeListLocalizer : TreeListLocalizer
        {
            public override string GetLocalizedString(TreeListStringId id)
            {
                if (id == TreeListStringId.MenuColumnBestFit)
                    return "Seçilmiş sütunun enini cədvələ görə uyğunlaşdır";
                if (id == TreeListStringId.MenuColumnBestFitAllColumns)
                    return "Bütün sütunların enini cədvələ görə uyğunlaşdır";
                if (id == TreeListStringId.MenuColumnSortAscending)
                    return "Artan sırada düz";
                if (id == TreeListStringId.MenuColumnSortDescending)
                    return "Azalan sırada düz";
                if (id == TreeListStringId.MenuColumnClearSorting)
                    return "Sıralamanı sil";
                if (id == TreeListStringId.MenuColumnColumnCustomization)
                    return "Gizlədilmiş sütunlar";
                if (id == TreeListStringId.ColumnCustomizationText)
                    return "Gizlədilmiş sütunlar";
                if (id == TreeListStringId.CustomizationFormBandHint)
                    return "Gizlədilmiş";
                if (id == TreeListStringId.MenuColumnFindFilterShow)
                    return "Axtarış panelini göstər";
                if (id == TreeListStringId.MenuColumnAutoFilterRowShow)
                    return "Avtomatik axtarış panelini göstər";
                if (id == TreeListStringId.MenuColumnFindFilterHide)
                    return "Axtarış panelini gizlət";
                if (id == TreeListStringId.MenuColumnAutoFilterRowHide)
                    return "Avtomatik axtarış panelini gizlət";
                return base.GetLocalizedString(id);
            }                
        }
    }
}
