using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    class Order
    {
        public int ID { get; set; }
        public int CUSTOMER_ID { get; set; }
        public string NOTE { get; set; }
        public DateTime ORDER_DATE { get; set; }
        public int BRANCH_ID { get; set; }
        public string BRANCH_NAME { get; set; }
        public int TIME_ID { get; set; }
        public string TIME { get; set; }
        public int SOURCE_ID { get; set; }
        public string ORDER_SOURCE { get; set; }
        public decimal FIRST_PAYMENT { get; set; }
        public decimal ORDER_AMOUNT { get; set; }
        public decimal CREDIT_AMOUNT { get; set; }
        public int IS_ACTIVE { get; set; }
        public DateTime CLOSED_DATE { get; set; }
        public int USED_USER_ID { get; set; }
    }
}
