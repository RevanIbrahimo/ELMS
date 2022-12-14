using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    public class PhonePrefix
    {
        public int ID { get; set; }
        public int PHONE_DESCRIPTION_ID { get; set; }
        public string PREFIX { get; set; }
        public string NOTE { get; set; }
        public int IS_CHANGE { get; set; }
        public int USED_USER_ID { get; set; }
    }
}
