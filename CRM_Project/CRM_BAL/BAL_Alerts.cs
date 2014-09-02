using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BAL
{
   public  class BAL_Alerts
    {
       public int Flag { get; set; }
       public string C_F_Id { get; set; }
       public string Name { get; set; }
       public string To_Mobile_No { get; set; }
       public string Message_Type { get; set; }
       public string SMS { get; set; }
       public string OnDate { get; set; }
       public string OnTime { get; set; }
       public string Alert_Type { get; set; }
       public string From_Type { get; set; }
       public string S_Status { get; set; }
       public string C_Date { get; set; }
    }
}
