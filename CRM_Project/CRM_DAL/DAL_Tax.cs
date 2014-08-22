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
  public   class DAL_Tax
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        public int Add_TAX_Save(BAL_Tax  btax)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_Tax_Save", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Tax_Type", btax.Tax_Type);
                cmd.Parameters.AddWithValue("@Tax_Percentage", btax.Tax_Percentage);
                cmd.Parameters.AddWithValue("@S_Status", btax.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", btax.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;


               
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
    public int Add_TAX_Delete_Update(BAL_Tax bt)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_Tax_Save", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 2);
                cmd.Parameters.AddWithValue("@Tax_Type", bt.Tax_Type);
                cmd.Parameters.AddWithValue("@Tax_Percentage", bt.Tax_Percentage);
                cmd.Parameters.AddWithValue("@S_Status", bt.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bt.C_Date);
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
