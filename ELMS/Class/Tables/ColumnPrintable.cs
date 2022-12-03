using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    public class ColumnPrintable
    {
        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }

        public ColumnPrintable(int index, string name)
        {
            this.ColumnIndex = index;
            this.ColumnName = name;
        }
    }
}
