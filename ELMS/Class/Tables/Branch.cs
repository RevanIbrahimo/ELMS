using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    public class Branch
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public string NOTE { get; set; }
        public int USED_USER_ID { get; set; }
        public int ORDER_ID { get; set; }
    }
}
