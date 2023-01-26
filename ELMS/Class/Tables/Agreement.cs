using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    class Agreement
    {

        public int ID { get; set; }
        public string NOTE { get; set; }
        public string AGREEMENT_NUMBER { get; set; }
        public DateTime AGREEMENT_DATE { get; set; }
        public int BRANCH_ID { get; set; }
        public string BRANCH_NAME { get; set; }
        public decimal AGREEMENT_AMOUNT { get; set; }
        public int IS_ACTIVE { get; set; }
        public int USED_USER_ID { get; set; }
    }
}
