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
   public  class DAL_Alerts
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        BAL_Alerts balr = new BAL_Alerts();
        public int Save_Alert_SMSTransaction(BAL_Alerts balr)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_SMSTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@C_F_Id", balr.C_F_Id);
                cmd.Parameters.AddWithValue("@Name", balr.Name);
                cmd.Parameters.AddWithValue("@To_Mobile_No", balr.To_Mobile_No);
                cmd.Parameters.AddWithValue("@Message_Type", balr.Message_Type);
                cmd.Parameters.AddWithValue("@SMS", balr.SMS);
                cmd.Parameters.AddWithValue("@OnDate", balr.OnDate);
                cmd.Parameters.AddWithValue("@OnTime", balr.OnTime);
                cmd.Parameters.AddWithValue("@Alert_Type", balr.Alert_Type);
                cmd.Parameters.AddWithValue("@From_Type", balr.From_Type);
                cmd.Parameters.AddWithValue("@S_Status", balr.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", balr.C_Date);
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
