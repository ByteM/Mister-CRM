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
    public class BAL_FinalDealer
    {

        public int Flag { get; set; }

        public int FDealerID { get; set; }

        public string SalesID { get; set; }

        public int Domain_ID { get; set; }

        public int Product_ID { get; set; }

        public int Brand_ID { get; set; }

        public int P_Category { get; set; }

        public int Model_No_ID { get; set; }

        public int Color_ID { get; set; }

        public double ProcNetAmt { get; set; }

        public double ProcPrice { get; set; }

        public string ProcQty { get; set; }

        public double FinalPrice { get; set; }

        public string FinalQty { get; set; }

        public double SubTotal { get; set; }

        public double RoundUp { get; set; }

        //public string FinalDate { get; set; }

        public double NetAmt { get; set; }

        public string SDefault { get; set; }

        public string ServiceIntervalMonth { get; set; }

        //public string FMonths { get; set; }

        public string S_Status { get; set; }

        public string C_Date { get; set; }
    }
}
