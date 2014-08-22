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
    public class BAL_CheckClearUpdate
    {
        public int Flag { get; set; }

        public int CheckID { get; set; }

        public string IsClear { get; set; }
    }
}
