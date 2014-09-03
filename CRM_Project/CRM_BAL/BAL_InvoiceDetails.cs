using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BAL
{
  public   class BAL_InvoiceDetails
    {
      public int Flag { get; set; }

      public int Customer_ID { get; set; }
      public int Employee_ID { get; set; }
      public string Bill_No { get; set; }

      public int Domain_ID { get; set; }
      public int Product_ID { get; set; }
      public int Brand_ID { get; set; }
      public int P_Category { get; set; }
      public int Model_No_ID { get; set; }
      public int Color_ID { get; set; }
      public string Products123 { get; set; }
      public int ID { get ;set;}
      public double Per_Product_Price { get; set; }
      public double Qty { get; set; }
      public double AvilableQty { get; set; }
      public double SaleQty { get; set; }
      public double  C_Price{ get; set; }
      public string Tax_Name { get; set; }
      public double Tax { get; set; }
      public double Total_Price { get; set; }
      public string Payment_Mode { get; set; }
    
      public double Paid_Amount { get; set; }
      public double Balance_Amount { get; set; }
      public string Warranty { get; set; }
      public string S_Status { get; set; }
      public string C_Date { get; set; }

      
    }
}
