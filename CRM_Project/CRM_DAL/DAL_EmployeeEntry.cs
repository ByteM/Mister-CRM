using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data ;
using System.Data .SqlClient ;
using System.Configuration ;
using CRM_BAL;

namespace CRM_DAL
{
    public class DAL_EmployeeEntry
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        BAL_EmployeeEntry baproduct = new BAL_EmployeeEntry();


        public int EmployeeEntry_Insert_Update_Delete(BAL_EmployeeEntry baemployee)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@EmployeeID", baemployee.EmployeeID);
                cmd.Parameters.AddWithValue("@EmployeeName", baemployee.EmployeeName);
                cmd.Parameters.AddWithValue("@DateOfBirth", baemployee.DateOfBirth);
                cmd.Parameters.AddWithValue("@EmpAddress", baemployee.EmpAddress);
                cmd.Parameters.AddWithValue("@MobileNo", baemployee.MobileNo);
                cmd.Parameters.AddWithValue("@PhoneNo", baemployee.PhoneNo);
                cmd.Parameters.AddWithValue("@Designation", baemployee.Designation);
                cmd.Parameters.AddWithValue("@DateOfJoining", baemployee.DateOfJoining);
                cmd.Parameters.AddWithValue("@NoOfYears", baemployee.NoOfYears);
                cmd.Parameters.AddWithValue("@Years", baemployee.Years);
                cmd.Parameters.AddWithValue("@NoOfMonths", baemployee.NoOfMonths);
                cmd.Parameters.AddWithValue("@Months", baemployee.Months);
                cmd.Parameters.AddWithValue("@Salary", baemployee.Salary);
                cmd.Parameters.AddWithValue("@S_Status", baemployee.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", baemployee.C_Date);
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
