using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BAL
{
   public class BAL_C_Installment
    {
       public int Flag { get; set; }
    public int Customer_ID { get; set; }
public string  Bill_No { get; set; }
public double  Total_Price { get; set; }
public double Paid_Amount { get; set; }
public double Balance_Amount { get; set; }
public double Monthly_Amount { get; set; }
public string Total_Installment_Month { get; set; }
public string Current_Installment_No { get; set; }
public string Remaining_Installments { get; set; }
public double Current_Installment_Amount { get; set; }
public string CInstallment_Date { get; set; }
public string Paid_Unpaid { get; set; }
public string S_Status { get; set; }
public string C_Date { get; set; }
public string c_Ins { get; set; }
    }
}
