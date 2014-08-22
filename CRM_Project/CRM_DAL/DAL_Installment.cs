using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CRM_BAL;

namespace CRM_DAL
{
   public  class DAL_Installment
   {
       public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
       SqlCommand cmd;
       public int Save_Installment(BAL_Installment  bins)
       {
           try
           {

               con.Open();
               cmd = new SqlCommand("SP_MainInstallment", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Flag", 1);
               cmd.Parameters.AddWithValue("@Customer_ID", bins.Customer_ID);
               cmd.Parameters.AddWithValue("@Bill_No", bins.Bill_No);
               cmd.Parameters.AddWithValue("@Total_Price", bins.Total_Price);
               cmd.Parameters.AddWithValue("@Paid_Amount", bins.Paid_Amount);
               cmd.Parameters.AddWithValue("@Balance_Amount", bins.Balance_Amount);
               cmd.Parameters.AddWithValue("@Monthly_Amount", bins.Monthly_Amount);
               cmd.Parameters.AddWithValue("@Installment_Year", bins.Installment_Year);
               cmd.Parameters.AddWithValue("@Installment_Month", bins.Installment_Month);
               cmd.Parameters.AddWithValue("@Installment_Date", bins.Installment_Date);
               cmd.Parameters.AddWithValue("@S_Status", bins.S_Status);
               cmd.Parameters.AddWithValue("@C_Date", bins.C_Date);
               cmd.Parameters.AddWithValue("@Ins",bins.Ins);
               int i = cmd.ExecuteNonQuery();
               return i;


           }
           catch (Exception)
           {

               throw;
           }
           finally { con.Close(); }
       }
    }
}
