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
    public class DAL_EmployeeUpdate
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        BAL_EmployeeEntry badealer = new BAL_EmployeeEntry();

        public int EmployeeEntry_Insert_Update_Delete(BAL_EmployeeEntry baemployeeentry)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 2);
                cmd.Parameters.AddWithValue("@ID", baemployeeentry.EID );
                cmd.Parameters.AddWithValue("@EmployeeID", baemployeeentry.EmployeeID);
                cmd.Parameters.AddWithValue("@EmployeeName", baemployeeentry.EmployeeName);
                cmd.Parameters.AddWithValue("@DateOfBirth", baemployeeentry.DateOfBirth);
                cmd.Parameters.AddWithValue("@EmpAddress", baemployeeentry.EmpAddress);
                cmd.Parameters.AddWithValue("@MobileNo", baemployeeentry.MobileNo);
                cmd.Parameters.AddWithValue("@PhoneNo", baemployeeentry.PhoneNo);
                cmd.Parameters.AddWithValue("@Designation", baemployeeentry.Designation);
                cmd.Parameters.AddWithValue("@DateOfJoining", baemployeeentry.DateOfJoining);
                cmd.Parameters.AddWithValue("@NoOfYears", baemployeeentry.NoOfYears);
                cmd.Parameters.AddWithValue("@Years", baemployeeentry.Years);
                cmd.Parameters.AddWithValue("@NoOfMonths", baemployeeentry.NoOfMonths);
                cmd.Parameters.AddWithValue("@Months", baemployeeentry.Months);
                cmd.Parameters.AddWithValue("@Salary", baemployeeentry.Salary);
                cmd.Parameters.AddWithValue("@S_Status", baemployeeentry.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", baemployeeentry.C_Date);
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
