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
    public class DAL_CheckClearUpdate
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;

        public int CheckUpdate_Insert_Update_Delete(BAL_CheckClearUpdate bcheckUpdate)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_CheckUpdateStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@CheckID", bcheckUpdate.CheckID);
                cmd.Parameters.AddWithValue("@IsClear", bcheckUpdate.IsClear);
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
