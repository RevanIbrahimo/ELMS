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
using ELMS.Class.Tables;
using ELMS.Class;

namespace ELMS
{
    public partial class FPrintableCheck : DevExpress.XtraEditors.XtraForm
    {
        public FPrintableCheck()
        {
            InitializeComponent();
        }
        public string FormText;
        public List<ColumnPrintable> ListColumnName;

        List<int> lstIndex = new List<int>();

        public delegate void DoEvent(List<int> lstColumnIndex);
        public event DoEvent RefreshDataGridView;

        private void FPrintableCheck_Load(object sender, EventArgs e)
        {
            this.Text = FormText;
            ColumnsCheckedListBox.DataSource = ListColumnName;
            ColumnsCheckedListBox.DisplayMember = "ColumnName";
            ColumnsCheckedListBox.ValueMember = "ColumnIndex";
            ColumnsCheckedListBox.CheckAll();
            AllCheck.Checked = true;
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (ColumnsCheckedListBox.CheckedItems.Count != 0)
            {
                for (int x = 0; x < ColumnsCheckedListBox.CheckedItems.Count; x++)
                {
                    lstIndex.Add(int.Parse(ColumnsCheckedListBox.CheckedItems[x].ToString()));
                }

                this.Close();
            }
            else
                GlobalProcedures.ShowWarningMessage("Çap və ya ixrac etmək üçün ən azı 1 sətr seçilməlidir.");
        }

        private void FPrintableCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.RefreshDataGridView(lstIndex);
        }

        private void AllCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (AllCheck.Checked)
                ColumnsCheckedListBox.CheckAll();
            else
                ColumnsCheckedListBox.UnCheckAll();
            AllCheck.Text = AllCheck.Checked ? "Bütün seçimləri sil" : "Bütün sətirləri seç";
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            lstIndex.Clear();
        }
    }
}