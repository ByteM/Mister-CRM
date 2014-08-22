using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BAL
{
   public  class BAL_Customer
    {
       public int Flag { get; set; }
       public int F_ID { get; set; }

      // public int Employee_ID { get; set; }

       public string Cust_ID { get; set; }
      
       public string Name { get; set; }
       public string Mobile_No { get; set; }
       public string  Date_Of_Birth { get; set; }
       public string Email_ID { get; set; }
       public string Address { get; set; }
       public string Occupation { get; set; }
       public string Payment_Mode { get; set; }

       public string Source_OF_Enquiry { get; set; }
       public int SourceEnqID { get; set; }

       //public string Bill_No { get; set; }
       public string S_Status { get; set; }
       public string C_Date { get; set; }


    }
}
