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
   public  class DAL_Warranty
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        public int Warranty_Save(BAL_Warranty  bw)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_Warranty", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Customer_ID", bw.Customer_ID);
                cmd.Parameters.AddWithValue("@Bill_No", bw.Bill_No);
                cmd.Parameters.AddWithValue("@Products123", bw.Products123);
                cmd.Parameters.AddWithValue("@Warranty", bw.Warranty);
                cmd.Parameters.AddWithValue("@Warr_Months", bw.Warr_Months);
                cmd.Parameters.AddWithValue("@Warr_StartDate", bw.Warr_StartDate);
                cmd.Parameters.AddWithValue("@Warr_EndDate", bw.Warr_EndDate);
                cmd.Parameters.AddWithValue("@Warr_RemainingDate", bw.Warr_RemainingDate);
                cmd.Parameters.AddWithValue("@Warr_RemainingMonths", bw.Warr_RemainingMonths);
                cmd.Parameters.AddWithValue("@Warr_RemainingDays", bw.Warr_RemainingDays);
                cmd.Parameters.AddWithValue("@Extend_Y_M", bw.Extend_Y_M);
                cmd.Parameters.AddWithValue("@C_ExtendDate", bw.C_ExtendDate);
                cmd.Parameters.AddWithValue("@Paid_Amount", bw.Paid_Amount);
                cmd.Parameters.AddWithValue("@Warr_Status", bw.Warr_Status);
                cmd.Parameters.AddWithValue("@S_Status", bw.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bw.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;


            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }

       public int Warranty_Column_Update(BAL_Warranty bwu)
        {
            try
            { con.Open();
            cmd = new SqlCommand("SP_WarrantyUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 2);
                cmd.Parameters.AddWithValue("@ID", bwu.ID);
                cmd.Parameters.AddWithValue("@Warr_Status", bwu.Warr_Status);
                cmd.Parameters.AddWithValue("@S_Status", bwu.S_Status);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {
                
                throw;
            }

        }
    }
}
