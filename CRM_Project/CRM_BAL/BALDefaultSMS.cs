using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CRM_BAL
{
    public class BALDefaultSMS
    {
        public int Flag { get; set; }

        public string SelectCategory { get; set; }

        public string DefaultSMSDate { get; set; }

        public string DefaultMessage { get; set; }

        public string S_Status { get; set; }

        public string C_Date { get; set; }
    }
}
