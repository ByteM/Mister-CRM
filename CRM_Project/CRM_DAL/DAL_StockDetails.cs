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
    public class DAL_StockDetails
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        BAL_StockDetails bstockdet = new BAL_StockDetails();

        public int AddStockDetails_Insert_Update_Delete(BAL_StockDetails bstockdet)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_StockDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@DomainID", bstockdet.DomainID);
                cmd.Parameters.AddWithValue("@ProductID", bstockdet.ProductID);
                cmd.Parameters.AddWithValue("@BrandID", bstockdet.BrandID);
                cmd.Parameters.AddWithValue("@ProductCatID", bstockdet.ProductCatID);
                cmd.Parameters.AddWithValue("@ModelID", bstockdet.ModelID);
                cmd.Parameters.AddWithValue("@ColorId", bstockdet.ColorId);
                cmd.Parameters.AddWithValue("@Products123", bstockdet.Products1234);
                cmd.Parameters.AddWithValue("@AvilableQty", bstockdet.AvilableQty);
                cmd.Parameters.AddWithValue("@SaleQty", bstockdet.SaleQty);
                cmd.Parameters.AddWithValue("@NewQty", bstockdet.NewQty);
                cmd.Parameters.AddWithValue("@FinalPrice", bstockdet.FinalPrice);
                cmd.Parameters.AddWithValue("@HaveInsurance", bstockdet.Insurance);
                cmd.Parameters.AddWithValue("@S_Status", bstockdet.S_Status  );
                cmd.Parameters.AddWithValue("@C_Date", bstockdet.C_Date);
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
