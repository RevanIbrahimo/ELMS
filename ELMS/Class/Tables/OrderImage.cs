using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    class OrderImage
    {
        public int ID { get; set; }
        public int ORDER_TAB_ID { get; set; }
        public byte[] IMAGE { get; set; }
    }
}
