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
    public class DAL_AddProduct
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        BAL_AddProduct baproduct = new BAL_AddProduct();
        public int AddDomain_Insert_Update_Delete(BAL_AddProduct baproduct)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_Add_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Domain_Name", baproduct.Domain_Name);
                cmd.Parameters.AddWithValue("@PAN_Card", baproduct.PAN_Card);
                cmd.Parameters.AddWithValue("@Adhar_Card", baproduct.Adhar_Card);
                cmd.Parameters.AddWithValue("@Passport", baproduct.Passport);
                cmd.Parameters.AddWithValue("@Address_Proof", baproduct.Address_Proof);
                cmd.Parameters.AddWithValue("@Seven_Twevel", baproduct.Seven_Twevel);
                cmd.Parameters.AddWithValue("@Form_16", baproduct.Form_16);
                cmd.Parameters.AddWithValue("@Dealer_Lisence", baproduct.Dealer_Lisence);
                cmd.Parameters.AddWithValue("@Other_ID_Proof", baproduct.Other_ID_Proof);
                cmd.Parameters.AddWithValue("@No_Documents", baproduct.No_Documents);
                cmd.Parameters.AddWithValue("@Cmp_ID_Proof", baproduct.Cmp_ID_Proof);
                cmd.Parameters.AddWithValue("@S_Status", baproduct.S_Status  );
                cmd.Parameters.AddWithValue("@C_Date", baproduct.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }


        }
        public int AddProducts_Insert_Update_Delete(BAL_AddProduct baproduct)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_ADDProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Domain_ID", baproduct.Domain_ID);
                cmd.Parameters.AddWithValue("@Product_Name", baproduct.Product_Name);
                cmd.Parameters.AddWithValue("@S_Status", baproduct.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", baproduct.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
        public int AddBrand_Insert_Update_Delete(BAL_AddProduct baproduct)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_ADDBrand", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Product_ID", baproduct.Product_ID);
                cmd.Parameters.AddWithValue("@Brand_Name", baproduct.Brand_Name);
                cmd.Parameters.AddWithValue("@S_Status", baproduct.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", baproduct.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
        public int AddP_Category_Insert_Update_Delete(BAL_AddProduct baproduct)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_ADDP_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Brand_ID", baproduct.Brand_ID);
                cmd.Parameters.AddWithValue("@Product_Category", baproduct.Product_Category);
                cmd.Parameters.AddWithValue("@S_Status", baproduct.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", baproduct.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
        public int AddModel_Insert_Update_Delete(BAL_AddProduct baproduct)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_ADDModel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@P_Category", baproduct.P_Category);
                cmd.Parameters.AddWithValue("@Model_No", baproduct.Model_No);
                cmd.Parameters.AddWithValue("@S_Status", baproduct.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", baproduct.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
        public int AddColor_Insert_Update_Delete(BAL_AddProduct baproduct)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_ADDColor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Model_No_ID", baproduct.Model_No_ID);
                cmd.Parameters.AddWithValue("@Color", baproduct.Color);
                cmd.Parameters.AddWithValue("@S_Status", baproduct.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", baproduct.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
        public int Save_Insert_Update_Delete(BAL_AddProduct baproduct)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_Save", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Domain_ID", baproduct.Domain_ID);
                cmd.Parameters.AddWithValue("@Product_ID", baproduct.Product_ID);
                cmd.Parameters.AddWithValue("@Brand_ID", baproduct.Brand_ID);
                cmd.Parameters.AddWithValue("@P_Category", baproduct.P_Category);
                cmd.Parameters.AddWithValue("@Model_No_ID", baproduct.Model_No_ID);
                cmd.Parameters.AddWithValue("@Color_ID", baproduct.Color_ID);
                cmd.Parameters.AddWithValue("@Narration", baproduct.Narration);
                cmd.Parameters .AddWithValue ("@Price",baproduct .Price );
                cmd.Parameters.AddWithValue("@S_Status", baproduct.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", baproduct.C_Date);
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