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
    public class DAL_StaockDetailsUpdate
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        //BAL_StockDetails bstockdetup = new BAL_StockDetails();

        public int AddStockDetailsUp_Insert_Update_Delete(BAL_StockDetails bstockdetup)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_StockDetailsUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@id", bstockdetup.SID);
                //cmd.Parameters.AddWithValue("@Products123", bstockdetup.Products123);
                //cmd.Parameters.AddWithValue("@AvilableQty", bstockdetup.AvilableQty);
                cmd.Parameters.AddWithValue("@NewQty", bstockdetup.NewQty);
                cmd.Parameters.AddWithValue("@FinalPrice", bstockdetup.FinalPrice);
                cmd.Parameters.AddWithValue("@S_Status", bstockdetup.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bstockdetup.C_Date);
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
