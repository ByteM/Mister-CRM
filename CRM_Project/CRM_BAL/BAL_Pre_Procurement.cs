using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BAL
{
 public  class BAL_Pre_Procurement
 {
     public int Flag { get; set; }
     public int DealerID { get; set; }
    // public int Domain_ID { get; set; }
     public string Phone { get; set; }
     public String  Phone_Id { get; set; }
     public string Domain_Name { get; set; }
     //public string Product_Name { get; set; }
     public int Domain_ID { get; set; }
     public string Product_Name { get; set; }
     public int Product_ID { get; set; }
     public string Brand_Name { get; set; }
     public int Brand_ID { get; set; }
     public string Product_Category { get; set; }
     public int P_Category { get; set; }
     public string Model_No { get; set; }
     public int Model_No_ID { get; set; }
     public string Color { get; set; }
     public int Color_ID { get; set; }
     public double Procurment_Price { get; set; }
     public double Quantity { get; set; }
     public double Total_Amount { get; set; }
     public double Net_Amount { get; set; }
     public double Round_Off { get; set; }
     public string Reg_Document{ get; set; }
     
     public string Have_Insurance { get; set; }
     public string Warranty { get; set; }
     public double re_ferb_cost { get; set; }
     public string Follow_up { get; set; }
     public string Narration { get; set; }
        public string S_Status { get; set; }
        public string  C_Date { get; set; }
    }
}
