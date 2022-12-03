using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    class DocumentType
    {
        public int ID { get; set; }
        public int NORESIDENT { get; set; }
        public string GROUP_NAME { get; set; }
        public string NAME { get; set; }
        public string PTTRN { get; set; }
        public int ISPINCODE { get; set; }
        public string PERSON_TYPE_NAME { get; set; }
        public string NOTE { get; set; }
        public int ORDER_ID { get; set; }
        public int USED_USER_ID { get; set; }
        public int PERSONTYPEID { get; set; }
        public int DOCUMENTGROUPID { get; set; }
    }
}
