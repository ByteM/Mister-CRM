using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BAL
{
  public   class BAL_Warranty
    {
        public int Flag { get; set; }
        public int ID { get; set; }
        public int Customer_ID { get; set; }
        public string Bill_No { get; set; }
        public string Products123 { get; set; }
        public string Warranty { get; set; }
        public string Warr_Months { get; set; }
        public string Warr_StartDate { get; set; }
        public string Warr_EndDate { get; set; }
        public string Warr_RemainingDate { get; set; }
        public string Warr_RemainingMonths { get; set; }
        public string Warr_RemainingDays { get; set; }
        public string Extend_Y_M { get; set; }
        public string C_ExtendDate { get; set; }
        public string Paid_Amount { get; set; }
        public string Warr_Status { get; set; }
        public string S_Status { get; set; }
        public string C_Date { get; set; }
    }
}
