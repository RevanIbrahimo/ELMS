using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    class CustomerCard
    {
        public int ID { get; set; }
        public int CUSTOMER_ID { get; set; }
        public string DOCUMENT_GROUP_NAME { get; set; }
        public string DOCUMENT_TYPE_NAME { get; set; }
        public string CARD { get; set; }
        public string CARD_ISSUE_NAME { get; set; }
        public DateTime RELIABLE_DATE { get; set; }
        public DateTime ISSUE_DATE { get; set; }
        public int USED_USER_ID { get; set; }
        public int DOCUMENT_GROUP_ID { get; set; }
        public int DOCUMENT_TYPE_ID { get; set; }
        public int CARD_ISSUING_ID { get; set; }

    }
}
