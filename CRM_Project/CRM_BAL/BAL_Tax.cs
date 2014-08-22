using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BAL
{
   public  class BAL_Tax
    {
       public int Flag { get; set; }
       public string Tax_Type { get; set; }
       public double Tax_Percentage { get; set; }
       public string S_Status { get; set; }
       public string C_Date { get; set; }
    }
}
