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
   public  class DAL_PaymentMode
   {
       public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
       SqlCommand cmd;

       public int Save_Cash(BAL_PaymentModes   bpm)
       {
           try
           {

               con.Open();
               cmd = new SqlCommand("SP_PaymentMode", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Flag", 1);
               cmd.Parameters.AddWithValue("@Customer_ID", bpm.Customer_ID);
               cmd.Parameters.AddWithValue("@Bill_No", bpm.Bill_No);
               cmd.Parameters.AddWithValue("@Total_Price", bpm.Total_Price);
               cmd.Parameters.AddWithValue("@Paid_Amount", bpm.Paid_Amount);
               cmd.Parameters.AddWithValue("@Balance_Amount", bpm.Balance_Amount);
               cmd.Parameters.AddWithValue("@S_Status", bpm.S_Status);
               cmd.Parameters.AddWithValue("@C_Date", bpm.C_Date);
               int i = cmd.ExecuteNonQuery();
               return i;


           }
           catch (Exception)
           {

               throw;
           }
           finally { con.Close(); }
       }
       public int Save_Cheque(BAL_PaymentModes bpm)
       {
           try
           {

               con.Open();
               cmd = new SqlCommand("SP_PaymentMode_Cheque", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Flag", 1);
               cmd.Parameters.AddWithValue("@Customer_ID", bpm.Customer_ID);
               cmd.Parameters.AddWithValue("@Bill_No", bpm.Bill_No);
               cmd.Parameters.AddWithValue("@Total_Price", bpm.Total_Price);
               cmd.Parameters.AddWithValue("@Cheque_Amount", bpm.Cheque_Amount);
               cmd.Parameters.AddWithValue("@Cheque_No", bpm.Cheque_No);
               cmd.Parameters.AddWithValue("@Cheque_Date", bpm.Cheque_Date);
               cmd.Parameters.AddWithValue("@Cheque_Bank_Name", bpm.Cheque_Bank_Name);
               cmd.Parameters.AddWithValue("@IsClear", bpm.IsClear);
               cmd.Parameters.AddWithValue("@S_Status", bpm.S_Status);
               cmd.Parameters.AddWithValue("@C_Date", bpm.C_Date);
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
