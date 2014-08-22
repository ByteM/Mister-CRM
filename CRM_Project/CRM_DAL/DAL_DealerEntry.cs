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
    public class DAL_DealerEntry
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        BAL_DealerEntry badealer = new BAL_DealerEntry();


        public int EmployeeEntry_Insert_Update_Delete(BAL_DealerEntry badealerentry)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_DealerEntry", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@DealerEntryID", badealerentry.DealerEntryID);
                cmd.Parameters.AddWithValue("@CompanyName", badealerentry.CompanyName);
                cmd.Parameters.AddWithValue("@DealerFirstName", badealerentry.DealerFirstName);
                cmd.Parameters.AddWithValue("@DealerLastName", badealerentry.DealerLastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", badealerentry.DateOfBirth);
                cmd.Parameters.AddWithValue("@MobileNo", badealerentry.MobileNo);
                cmd.Parameters.AddWithValue("@PhoneNo", badealerentry.PhoneNo);
                cmd.Parameters.AddWithValue("@DealerAddress", badealerentry.DealerAddress);
                cmd.Parameters.AddWithValue("@City", badealerentry.City);
                cmd.Parameters.AddWithValue("@Zip", badealerentry.Zip);
                cmd.Parameters.AddWithValue("@DState", badealerentry.DState);
                cmd.Parameters.AddWithValue("@Country", badealerentry.Country);
                cmd.Parameters.AddWithValue("@S_Status", badealerentry.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", badealerentry.C_Date);
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
