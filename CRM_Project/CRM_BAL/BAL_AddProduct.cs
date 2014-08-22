using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CRM_BAL
{//FF171515,Margin="540,0,-620,630"
  public   class BAL_AddProduct
    {
      public int Flag { get; set; }
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
      public string Narration { get; set; }
      public double Price { get; set; }
      public string S_Status { get; set; }
      
      public  string PAN_Card { get; set; }
      public string Adhar_Card { get; set; }
      public string Passport{ get; set; }
      public string Address_Proof  { get; set; }
      public string Seven_Twevel { get; set; }
      public string Form_16 { get; set; }
      public string Dealer_Lisence { get; set; }
      public string Other_ID_Proof { get; set; }
      public string No_Documents { get; set; }
      public string Cmp_ID_Proof { get; set; }
      public string  C_Date { get; set; }

    }
}
