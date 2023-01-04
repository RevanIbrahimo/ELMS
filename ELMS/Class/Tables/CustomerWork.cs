using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    class CustomerWork
    {
        public int ID { get; set; }
        public int CUSTOMER_ID { get; set; }
        public int IS_ACTIVE { get; set; }
        public string PLACE_NAME { get; set; }
        public string NOTE { get; set; }
        public string POSITION { get; set; }
        public decimal SALARY { get; set; }
        public int PROFESSION_ID { get; set; }
        public int NOAVAILABLE { get; set; }
        public int USED_USER_ID { get; set; }
        public int IS_CHANGE { get; set; }

    }
}
