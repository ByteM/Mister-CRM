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
    public class DAL_FinalDealerUpdate
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;

        public int FinalUpdateD_Insert_Update_Delete(BAL_FinalDealer bfinaldealerup)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_FinalDealerUpdateStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@DealerID", bfinaldealerup.FDealerID);
                cmd.Parameters.AddWithValue("@S_Status", bfinaldealerup.S_Status);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
