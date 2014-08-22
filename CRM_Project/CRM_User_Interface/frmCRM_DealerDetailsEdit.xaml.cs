using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CRM_BAL;
using CRM_DAL;

namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for frmCRM_DealerDetailsEdit.xaml
    /// </summary>
    public partial class frmCRM_DealerDetailsEdit : Window
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        SqlDataReader dr;
        string caption = "Green Future Glob";
        static int PK_ID;

        public frmCRM_DealerDetailsEdit()
        {
            InitializeComponent();
            //FillData();
        }

        BAL_DealerEntry bdealerupd = new BAL_DealerEntry();
        DAL_DealerUpdate ddealerupd = new DAL_DealerUpdate();

        public void DealerID(string id)
        {
            txtDealerID.Text = id;
        }

        public void FillData()
        {
            try
            {
                con.Open();
                string sqlquery = "SELECT * FROM tbl_DealerEntry where ID='" + txtDealerID.Text + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lbl_Edit_DealerID.Content = dt.Rows[0]["DealerEntryID"].ToString();
                    txtAdmEdit_CompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                    txtAdmEdit_DealerFirstName.Text = dt.Rows[0]["DealerFirstName"].ToString();
                    txtAdmEdit_DealerLastName.Text = dt.Rows[0]["DealerLastName"].ToString();
                    dtpAdmEdit_Dealer_DOB.SelectedDate = Convert.ToDateTime(dt.Rows[0]["DateOfBirth"].ToString());
                    txtAdmEdit_Dealer_MobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
                    txtAdmEdit_Dealer_PhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
                    txtAdmEdit_Dealer_Address.Text = dt.Rows[0]["DealerAddress"].ToString();
                    txtAdmEdit_Dealer_City.Text = dt.Rows[0]["City"].ToString();
                    txtAdmEdit_Dealer_Zip.Text = dt.Rows[0]["Zip"].ToString();
                    txtAdmEdit_Dealer_State.Text = dt.Rows[0]["DState"].ToString();
                    txtAdmEdit_Dealer_Country.Text = dt.Rows[0]["Country"].ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            btnAdmEdit_Dealer_Save.Content = "Update";
        }

        #region Button Event
        private void btnAdmEdit_Dealer_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bdealerupd.Flag = 2;
                bdealerupd.id = Convert.ToInt32(txtDealerID.Text);
                bdealerupd.DealerEntryID = lbl_Edit_DealerID.Content.ToString();
                bdealerupd.CompanyName = txtAdmEdit_CompanyName.Text;
                bdealerupd.DealerFirstName = txtAdmEdit_DealerFirstName.Text;
                bdealerupd.DealerLastName = txtAdmEdit_DealerLastName.Text;
                bdealerupd.DateOfBirth = Convert.ToDateTime(dtpAdmEdit_Dealer_DOB.SelectedDate);
                bdealerupd.MobileNo = txtAdmEdit_Dealer_MobileNo.Text;
                bdealerupd.PhoneNo = txtAdmEdit_Dealer_PhoneNo.Text;
                bdealerupd.DealerAddress = txtAdmEdit_Dealer_Address.Text;
                bdealerupd.City = txtAdmEdit_Dealer_City.Text;
                bdealerupd.Zip = txtAdmEdit_Dealer_Zip.Text;
                bdealerupd.DState = txtAdmEdit_Dealer_State.Text;
                bdealerupd.Country = txtAdmEdit_Dealer_Country.Text;
                bdealerupd.S_Status = "Active";

                //string STRTODAYDATE = System.DateTime.Now.ToShortDateString();
                //string time = System.DateTime.Now.ToShortTimeString();
                //string[] STRVAL = STRTODAYDATE.Split('-');
                //string STR_DATE1 = STRVAL[0];
                //string STR_MONTH = STRVAL[1];
                //string STR_YEAR = STRVAL[2];
                //string DATE = STR_DATE1 + "-" + STR_MONTH + "-" + STR_YEAR;
                ////txtdate.Text = DATE;
                ////txttime.Text = time;

                //baddprd.C_Date =Convert .ToDateTime( DATE);
                bdealerupd.C_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                ddealerupd.EmployeeEntry_Insert_Update_Delete(bdealerupd);
                MessageBox.Show("Updated Data Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        private void btnAdmEdit_Dealer_Clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdmEdit_Dealer_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion Button Event
    }
}
