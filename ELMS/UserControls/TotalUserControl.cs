using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ELMS.Class.DataAccess;
using ELMS.Class;
using DevExpress.XtraGrid.Views.Grid;
using static ELMS.Class.Enum;
using ELMS.Class.Tables;
using ELMS.Forms.Customer;
using ELMS.Forms.Order;

namespace ELMS.UserControls
{
    public partial class TotalUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public TotalUserControl()
        {
            InitializeComponent();
        }
        
    }
}
