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
    public class DAL_DefaultSMS
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;

        public int DefaultSMS_Insert_Update_Delete(BALDefaultSMS bdefaultSMS)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_DefaultSMS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@SelectCategory", bdefaultSMS.SelectCategory);
                cmd.Parameters.AddWithValue("@DefaultDate", bdefaultSMS.DefaultSMSDate);
                cmd.Parameters.AddWithValue("@DefaultMessage", bdefaultSMS.DefaultMessage);
                cmd.Parameters.AddWithValue("@S_Status", bdefaultSMS.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bdefaultSMS.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }

        }

        public int BalanceDefaultSMS_Insert_Update_Delete(BALDefaultSMS bdefaultSMS)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_BalanceDefaultSMS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@SelectCategory", bdefaultSMS.SelectCategory);
                cmd.Parameters.AddWithValue("@DefaultDate", bdefaultSMS.DefaultSMSDate);
                cmd.Parameters.AddWithValue("@DefaultMessage", bdefaultSMS.DefaultMessage);
                cmd.Parameters.AddWithValue("@S_Status", bdefaultSMS.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bdefaultSMS.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }

        }

        public int WarantyDefaultSMS_Insert_Update_Delete(BALDefaultSMS bdefaultSMS)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_WarantyDefaultSMS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@SelectCategory", bdefaultSMS.SelectCategory);
                cmd.Parameters.AddWithValue("@DefaultDate", bdefaultSMS.DefaultSMSDate);
                cmd.Parameters.AddWithValue("@DefaultMessage", bdefaultSMS.DefaultMessage);
                cmd.Parameters.AddWithValue("@S_Status", bdefaultSMS.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bdefaultSMS.C_Date);
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
