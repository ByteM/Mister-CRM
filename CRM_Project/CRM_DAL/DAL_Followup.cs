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
   public  class DAL_Followup
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
       //===========fire query when select folloup============
       // public int Followup_Save_Insert_Update_Delete(BAL_Followup  balfp)
       // {
       //     try
       //     {

       //         con.Open();
       //         cmd = new SqlCommand("SP_Followup", con);
       //         cmd.CommandType = CommandType.StoredProcedure;
       //         cmd.Parameters.AddWithValue("@Flag", 1);
       //         cmd.Parameters.AddWithValue("@Cust_ID", balfp.Cust_ID);
       //         cmd.Parameters.AddWithValue("@Followup_Type", balfp.Followup_Type);
       //         cmd.Parameters.AddWithValue("@F_Date", balfp.F_Date);
       //         cmd.Parameters.AddWithValue("@F_Message", balfp.F_Message);
       //       //  cmd.Parameters.AddWithValue("@Customer_Update", balfp.Customer_Update);
       //         //cmd.Parameters.AddWithValue("@Walkins", balfp.Walkins);
       //         cmd.Parameters.AddWithValue("@S_Status", balfp.S_Status);
       //         cmd.Parameters.AddWithValue("@C_Date", balfp.C_Date);
       //         int i = cmd.ExecuteNonQuery();
       //         return i;


       //     }
       //     catch (Exception)
       //     {

       //         throw;
       //     }
       //     finally { con.Close(); }
       // }
       ////==============end followup query=================================
       ////====================fire query when walkins option is selected========
       // public int walkin_Save_Insert_Update_Delete(BAL_Followup balfp)
       // {
       //     try
       //     {

       //         con.Open();
       //         cmd = new SqlCommand("SP_", con);
       //         cmd.CommandType = CommandType.StoredProcedure;
       //         cmd.Parameters.AddWithValue("@Flag", 2);
       //         cmd.Parameters.AddWithValue("@Cust_ID", balfp.Cust_ID);
       //        // cmd.Parameters.AddWithValue("@F_Date", balfp.F_Date);
       //        // cmd.Parameters.AddWithValue("@F_Message", balfp.F_Message);
       //      cmd.Parameters.AddWithValue("@Walkins", balfp.Walkins);
       //         cmd.Parameters.AddWithValue("@S_Status", balfp.S_Status);
       //         cmd.Parameters.AddWithValue("@C_Date", balfp.C_Date);
       //         int i = cmd.ExecuteNonQuery();
       //         return i;


       //     }
       //     catch (Exception)
       //     {

       //         throw;
       //     }
       //     finally { con.Close(); }
       // }
       //======================end wlakins ===========================
       //====================add customer===============================
        public int Follwup1_Save_Insert_Update_Delete(BAL_Followup balfp)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_Followup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Followup_ID", balfp.Followup_ID);
                cmd.Parameters.AddWithValue("@Name", balfp.Name);
                cmd.Parameters.AddWithValue("@Mobile_No", balfp.Mobile_No);
                cmd.Parameters.AddWithValue("@Date_Of_Birth", balfp.Date_Of_Birth);
                cmd.Parameters.AddWithValue("@Email_ID", balfp.Email_ID);
                cmd.Parameters.AddWithValue("@Address", balfp.Address);
                cmd.Parameters.AddWithValue("@Occupation", balfp.Occupation);
                cmd.Parameters.AddWithValue("@Product_Details", balfp.Product_Details);
              //  cmd.Parameters.AddWithValue("@Followup_Walkin_Option", balfp.Followup_Walkin_Option);

                cmd.Parameters.AddWithValue("@Followup_Type", balfp.Followup_Type);
                 cmd.Parameters.AddWithValue("@F_Date", balfp.F_Date);
                 cmd.Parameters.AddWithValue("@F_Message", balfp.F_Message);
               // cmd.Parameters.AddWithValue("@Walkins", balfp.Walkins);
                 cmd.Parameters.AddWithValue("@Folloup_Update", balfp.Folloup_Update);
                cmd.Parameters.AddWithValue("@S_Status", balfp.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", balfp.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;


            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
        public int Follwup2_Save_Insert_Update_Delete(BAL_Followup balfp)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_Followup2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Followup_ID", balfp.Followup_ID);
                cmd.Parameters.AddWithValue("@Name", balfp.Name);
                cmd.Parameters.AddWithValue("@Mobile_No", balfp.Mobile_No);
                cmd.Parameters.AddWithValue("@Date_Of_Birth", balfp.Date_Of_Birth);
                cmd.Parameters.AddWithValue("@Email_ID", balfp.Email_ID);
                cmd.Parameters.AddWithValue("@Address", balfp.Address);
                cmd.Parameters.AddWithValue("@Occupation", balfp.Occupation);
                cmd.Parameters.AddWithValue("@Product_Details", balfp.Product_Details);
                //  cmd.Parameters.AddWithValue("@Followup_Walkin_Option", balfp.Followup_Walkin_Option);

                cmd.Parameters.AddWithValue("@Followup_Type", balfp.Followup_Type);
               // cmd.Parameters.AddWithValue("@F_Date", balfp.F_Date);
               // cmd.Parameters.AddWithValue("@F_Message", balfp.F_Message);
                // cmd.Parameters.AddWithValue("@Walkins", balfp.Walkins);
                cmd.Parameters.AddWithValue("@Folloup_Update", balfp.Folloup_Update);
                cmd.Parameters.AddWithValue("@S_Status", balfp.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", balfp.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;


            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
       //=============================end cstomer=========================
        //public int Customer_Walking_Save_Insert_Update_Delete(BAL_Followup balfp)
        //{
        //    try
        //    {

        //        con.Open();
        //        cmd = new SqlCommand("SP_Customer", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Flag", 3);
               
        //        cmd.Parameters.AddWithValue("@C_Date", balfp.C_Date);
        //        int i = cmd.ExecuteNonQuery();
        //        return i;


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally { con.Close(); }
        //}
    }
}
