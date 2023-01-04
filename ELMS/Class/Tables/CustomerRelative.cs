using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    class CustomerRelative
    {
        public int ID { get; set; }
        public int PHONE_ID { get; set; }
        public int KIND_ID { get; set; }
        public int CUSTOMER_ID { get; set; }
        public int IS_ACTIVE { get; set; }
        public string NAME { get; set; }
        public string KIND_NAME { get; set; }
        public string NOTE { get; set; }
        public string PROFESSION_NAME { get; set; }
        public decimal SALARY { get; set; }
        public int PROFESSION_ID { get; set; }
        public int USED_USER_ID { get; set; }
        public int IS_CHANGE { get; set; }
    }
}
