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
    public class DAL_FinalDealer
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        
        public int FinalDealer_Insert_Update_Delete(BAL_FinalDealer bfinaldealer1)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_FinalDealer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Dealer_ID", bfinaldealer1.FDealerID);
                cmd.Parameters.AddWithValue("@SalesID", bfinaldealer1.SalesID);
                cmd.Parameters.AddWithValue("@Domain_ID", bfinaldealer1.Domain_ID);
                cmd.Parameters.AddWithValue("@Product_ID", bfinaldealer1.Product_ID);
                cmd.Parameters.AddWithValue("@Brand_ID", bfinaldealer1.Brand_ID);
                cmd.Parameters.AddWithValue("@P_Category", bfinaldealer1.P_Category);
                cmd.Parameters.AddWithValue("@Model_No_ID", bfinaldealer1.Model_No_ID);
                cmd.Parameters.AddWithValue("@Color_ID", bfinaldealer1.Color_ID);
                cmd.Parameters.AddWithValue("@ProcNetAmt", bfinaldealer1.ProcNetAmt);
                cmd.Parameters.AddWithValue("@ProcPrice", bfinaldealer1.ProcPrice);
                cmd.Parameters.AddWithValue("@ProcQty", bfinaldealer1.ProcQty);
                cmd.Parameters.AddWithValue("@FinalPrice", bfinaldealer1.FinalPrice);
                cmd.Parameters.AddWithValue("@FinalQty", bfinaldealer1.FinalQty);
                cmd.Parameters.AddWithValue("@SubTotal", bfinaldealer1.SubTotal);
                cmd.Parameters.AddWithValue("@RoundUp", bfinaldealer1.RoundUp);
                cmd.Parameters.AddWithValue("@NetAmt", bfinaldealer1.NetAmt);
                //cmd.Parameters.AddWithValue("@FinalDate", bfinaldealer.FinalDate);
                cmd.Parameters.AddWithValue("@SDefault", bfinaldealer1.SDefault);
                cmd.Parameters.AddWithValue("@ServiceIntervalMonth", bfinaldealer1.ServiceIntervalMonth);
                //cmd.Parameters.AddWithValue("@FMonths,", bfinaldealer1.FMonths);
                cmd.Parameters.AddWithValue("@S_Status", bfinaldealer1.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bfinaldealer1.C_Date);
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
