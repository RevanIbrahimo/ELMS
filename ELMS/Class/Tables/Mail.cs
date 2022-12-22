using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    class Mail
    {
        public int ID { get; set; }
        public int OWNER_TYPE { get; set; }
        public int OWNER_ID { get; set; }
        public string MAIL { get; set; }
        public string NOTE { get; set; }
        public int IS_SEND { get; set; }
        public int ORDER_ID { get; set; }
        public int IS_CHANGE { get; set; }
        public int USED_USER_ID { get; set; }
    }
}
