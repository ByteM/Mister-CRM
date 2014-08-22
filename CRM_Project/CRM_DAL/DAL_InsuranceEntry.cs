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
    public class DAL_InsuranceEntry
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;

        public int InsuranceEntry_Insert_Update_Delete(BAL_InsuranceEntry binsuranceentry)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_InsuranceEntry", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@Customer_ID", binsuranceentry.CustomerID);

                cmd.Parameters.AddWithValue("@InsuranceNo", binsuranceentry.InsuranceNo);
                cmd.Parameters.AddWithValue("@ProductName", binsuranceentry.ProductName);
                cmd.Parameters.AddWithValue("@InsuranceAmt", binsuranceentry.InsuranceAmt);
                cmd.Parameters.AddWithValue("@BankName", binsuranceentry.BankName);
                cmd.Parameters.AddWithValue("@InsuranceDate", binsuranceentry.InsuranceDate);
                cmd.Parameters.AddWithValue("@NoOfYearMonths", binsuranceentry.NoOfYearsMonths);
                cmd.Parameters.AddWithValue("@NoOfMonth", binsuranceentry.NoOfMonth);
                cmd.Parameters.AddWithValue("@YearsMonths", binsuranceentry.YearsMonth);
                cmd.Parameters.AddWithValue("@IntervalMonths", binsuranceentry.IntervalMonth);
                cmd.Parameters.AddWithValue("@IntervalMonthY", binsuranceentry.IntervalMonthY);
                cmd.Parameters.AddWithValue("@IntervalAmt", binsuranceentry.IntervalAmount);
                cmd.Parameters.AddWithValue("@NewInsuranceDate", binsuranceentry.NewInsuranceDate);
                cmd.Parameters.AddWithValue("@FirstPartyInsurance", binsuranceentry.FirstPartyInsurance);
                cmd.Parameters.AddWithValue("@IsClear", binsuranceentry.IsClear);
                cmd.Parameters.AddWithValue("@S_Status", binsuranceentry.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", binsuranceentry.C_Date);
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


        public int InsuranceEntry_Update_Delete(BAL_InsuranceEntry binsuranceentry)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_InsuranceEntry", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 2);
                cmd.Parameters.AddWithValue("@InsuranceID", binsuranceentry.InsuranceID);
                cmd.Parameters.AddWithValue("@Customer_ID", binsuranceentry.CustomerID);
                cmd.Parameters.AddWithValue("@InsuranceNo", binsuranceentry.InsuranceNo);
                cmd.Parameters.AddWithValue("@ProductName", binsuranceentry.ProductName);
                cmd.Parameters.AddWithValue("@InsuranceAmt", binsuranceentry.InsuranceAmt);
                cmd.Parameters.AddWithValue("@BankName", binsuranceentry.BankName);
                cmd.Parameters.AddWithValue("@InsuranceDate", binsuranceentry.InsuranceDate);
                cmd.Parameters.AddWithValue("@NoOfYearMonths", binsuranceentry.NoOfYearsMonths);
                cmd.Parameters.AddWithValue("@NoOfMonth", binsuranceentry.NoOfMonth);
                cmd.Parameters.AddWithValue("@YearsMonths", binsuranceentry.YearsMonth);
                cmd.Parameters.AddWithValue("@IntervalMonths", binsuranceentry.IntervalMonth);
                cmd.Parameters.AddWithValue("@IntervalMonthY", binsuranceentry.IntervalMonthY);
                cmd.Parameters.AddWithValue("@IntervalAmt", binsuranceentry.IntervalAmount);
                cmd.Parameters.AddWithValue("@NewInsuranceDate", binsuranceentry.NewInsuranceDate);
                cmd.Parameters.AddWithValue("@FirstPartyInsurance", binsuranceentry.FirstPartyInsurance);
                cmd.Parameters.AddWithValue("@IsClear", binsuranceentry.IsClear);
                cmd.Parameters.AddWithValue("@S_Status", binsuranceentry.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", binsuranceentry.C_Date);
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
