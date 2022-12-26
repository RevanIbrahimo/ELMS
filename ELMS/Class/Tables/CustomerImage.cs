using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.Tables
{
    public class CustomerImage
    {
        public int ID { get; set; }
        public int CUSTOMER_ID { get; set; }
        public byte[] IMAGE { get; set; }
    }
}
