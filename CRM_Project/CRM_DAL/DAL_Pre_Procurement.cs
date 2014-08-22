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
    public class DAL_Pre_Procurement
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        public int Pre_Procurement_Save_Insert_Update_Delete(BAL_Pre_Procurement bapreoduct)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_Pre_Procurement_Save", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@DealerID", bapreoduct.DealerID);
               // cmd.Parameters.AddWithValue("@Phone", bapreoduct.Phone);
                cmd.Parameters.AddWithValue("@Domain_ID", bapreoduct.Domain_ID);
                cmd.Parameters.AddWithValue("@Product_ID", bapreoduct.Product_ID);
                cmd.Parameters.AddWithValue("@Brand_ID", bapreoduct.Brand_ID);
                cmd.Parameters.AddWithValue("@P_Category", bapreoduct.P_Category);
                cmd.Parameters.AddWithValue("@Model_No_ID", bapreoduct.Model_No_ID);
                cmd.Parameters.AddWithValue("@Color_ID", bapreoduct.Color_ID);

                cmd.Parameters.AddWithValue("@Procurment_Price", bapreoduct.Procurment_Price);
                cmd.Parameters.AddWithValue("@Quantity", bapreoduct.Quantity);
                cmd.Parameters.AddWithValue("@Total_Amount", bapreoduct.Total_Amount);
                cmd.Parameters.AddWithValue("@Net_Amount", bapreoduct.Net_Amount);
                cmd.Parameters.AddWithValue("@Round_Off", bapreoduct.Round_Off);

                cmd.Parameters.AddWithValue("@Reg_Document", bapreoduct.Reg_Document);
                cmd.Parameters.AddWithValue("@Have_Insurance", bapreoduct.Have_Insurance);
                cmd.Parameters.AddWithValue("@Warranty", bapreoduct.Warranty);
                cmd.Parameters.AddWithValue ("@re_ferb_cost", bapreoduct.re_ferb_cost);
                cmd.Parameters .AddWithValue ("@Follow_up",bapreoduct.Follow_up);
                cmd.Parameters.AddWithValue("@Narration", bapreoduct.Narration);
                cmd.Parameters.AddWithValue("@S_Status", bapreoduct.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bapreoduct.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;


                //con.Open();
                //cmd = new SqlCommand("SP_Pre_Procurement_Save", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Flag", 1);
                //cmd.Parameters.AddWithValue("@Saler_Name",bapreoduct.Saler_Name );
                //    cmd.Parameters.AddWithValue("@Phone",bapreoduct.Phone );
                //cmd.Parameters.AddWithValue("@Domain_Name", bapreoduct.Domain_Name );
                //cmd.Parameters.AddWithValue("@Product_Name", bapreoduct.Product_Name );
                //cmd.Parameters.AddWithValue("@Brand_Name", bapreoduct.Brand_Name);
                //cmd.Parameters.AddWithValue("@Product_Category", bapreoduct.Product_Category);
                //cmd.Parameters.AddWithValue("@Model_No", bapreoduct.Model_No);
                //cmd.Parameters.AddWithValue("@Color", bapreoduct.Color);

                //cmd.Parameters .AddWithValue ("@Price",bapreoduct .Price );

                // cmd.Parameters.AddWithValue("@Reg_Document", bapreoduct.Reg_Document );
                //cmd.Parameters .AddWithValue ("@Have_Insurance",bapreoduct .Have_Insurance);

                //cmd.Parameters.AddWithValue("@S_Status", bapreoduct.S_Status);
                //cmd.Parameters.AddWithValue("@C_Date", bapreoduct.C_Date);
                //int i = cmd.ExecuteNonQuery();
                //return i;
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
    }
}
