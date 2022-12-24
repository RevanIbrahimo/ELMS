using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    class UserList
    {
        public int ID { get; set; }
        public int BRANCH_ID { get; set; }
        public string FULL_NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public int IS_ACTIVE { get; set; }
        public DateTime BIRTHDAY { get; set; }
        public int SEX_ID { get; set; }
        public string ADDRESS { get; set; }
        public string EMAIL { get; set; }
        public string NOTE { get; set; }
        public int GROUP_ID { get; set; }
        public DateTime CLOSED_DATE { get; set; }
        public int SESSION_ID { get; set; }
        public int DOCTOR_ID { get; set; }
        public int USED_USER_ID { get; set; }
    }
}
