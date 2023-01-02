using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    class ProductCard
    {
        public int ID { get; set; }
        public int ORDER_TAB_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string IMEI { get; set; }
        public decimal PRICE { get; set; }
        public decimal PRODUCT_COUNT { get; set; }
        public decimal TOTAL { get; set; }
        public int USED_USER_ID { get; set; }
        public int PRODUCT_ID { get; set; }
        public int IS_CHANGE { get; set; }
    }
}
