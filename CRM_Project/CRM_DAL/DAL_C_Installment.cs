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
  public  class DAL_C_Installment
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        public int Save_C_Installment(BAL_C_Installment  bCins)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_C_Installment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Customer_ID", bCins.Customer_ID);
                cmd.Parameters.AddWithValue("@Bill_No", bCins.Bill_No);
                cmd.Parameters.AddWithValue("@Total_Price", bCins.Total_Price);
                cmd.Parameters.AddWithValue("@Paid_Amount", bCins.Paid_Amount);
                cmd.Parameters.AddWithValue("@Balance_Amount", bCins.Balance_Amount);
                cmd.Parameters.AddWithValue("@Monthly_Amount", bCins.Monthly_Amount);
                cmd.Parameters.AddWithValue("@Total_Installment_Month", bCins.Total_Installment_Month);
                cmd.Parameters.AddWithValue("@Current_Installment_No", bCins.Current_Installment_No);
                cmd.Parameters.AddWithValue("@Remaining_Installments", bCins.Remaining_Installments);
                cmd.Parameters.AddWithValue("@Current_Installment_Amount", bCins.Current_Installment_Amount);
                cmd.Parameters.AddWithValue("@CInstallment_Date", bCins.CInstallment_Date);
                cmd.Parameters.AddWithValue("@Paid_Unpaid", bCins.Paid_Unpaid);
                cmd.Parameters.AddWithValue("@S_Status", bCins.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bCins.C_Date);
                cmd.Parameters.AddWithValue("@c_Ins", bCins.c_Ins);
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
