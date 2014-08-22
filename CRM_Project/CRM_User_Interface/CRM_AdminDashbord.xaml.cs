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
using System.Globalization;
using Microsoft.Win32;
using System.Windows.Controls.DataVisualization.Charting;
using CRM_BAL;
using CRM_DAL;


namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for CRM_AdminDashbord.xaml
    /// </summary>
    public partial class CRM_AdminDashbord : Window
    {
        #region Global Veriable
        NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        SqlDataReader dr;
        string caption = "Green Future Glob";

        static int PK_ID;
        #endregion Global Veriable

        #region Load Event
        public CRM_AdminDashbord()
        {
            InitializeComponent();
            checkedStuff = new List<string>();

            Chart_Followup();
            Chart_Seals();
            Chart_Procurment();
            Chart_CustomerBase();
            Chart_HighestSingleProduct();
           Chart_HighestProduct();
            Chart_BestEnquerySource();

            LoadColumnChart_FollowUp();
        }
        #endregion Load Event
        
        /// <summary>
        /// Add Products
        /// </summary>
        BAL_AddProduct baddprd = new BAL_AddProduct();
        DAL_AddProduct dalprd = new DAL_AddProduct();

        /// <summary>
        /// Pre Procurment
        /// </summary>
        BAL_Pre_Procurement bpreproc = new BAL_Pre_Procurement();
        DAL_Pre_Procurement dpreproc = new DAL_Pre_Procurement();

        /// <summary>
        /// Check Update
        /// </summary>
        BAL_CheckClearUpdate bcheckUp = new BAL_CheckClearUpdate();
        DAL_CheckClearUpdate dcheckUp = new DAL_CheckClearUpdate();

        BAL_EmployeeEntry bempetr = new BAL_EmployeeEntry();
        DAL_EmployeeEntry dempetr = new DAL_EmployeeEntry();
        BAL_DealerEntry bdealeretr = new BAL_DealerEntry();
        DAL_DealerEntry ddealeretr = new DAL_DealerEntry();
        BAL_StockDetails bstockDet = new BAL_StockDetails();
        DAL_StockDetails dstockDet = new DAL_StockDetails();
        DAL_StaockDetailsUpdate dstUpdate = new DAL_StaockDetailsUpdate();
        BAL_FinalDealer bfinaldealer1 = new BAL_FinalDealer();
        DAL_FinalDealer dfinaldealer = new DAL_FinalDealer();
        DAL_StockAddQty daddqty = new DAL_StockAddQty();
        DAL_FinalDealerUpdate dFup = new DAL_FinalDealerUpdate();

        //string maincked, CName;
        //string bpg, cid1;
        //int fetcdoc, Cust_id;
        //int exist;
        List<string> checkedStuff;
        static DataTable dtstat = new DataTable();
        //double MA;

        private void btnadminexit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void smaddproducts_Click(object sender, RoutedEventArgs e)
        {

            grd_U_AddProducts.Visibility = System.Windows.Visibility.Visible;
        }

        #region Employee Function
        #region EmployeeEntry Button Event
        private void btnAdm_Emp_Save_Click(object sender, RoutedEventArgs e)
        {
            if (Employee_Validation() == true)
                return;

            try
            {
                bempetr.Flag = 1;
                bempetr.EmployeeID = lblEmpID.Content.ToString();
                bempetr.EmployeeName = txtAdm_EmpName.Text;
                bempetr.DateOfBirth = Convert.ToDateTime(dtpAdm_Emp_DOB.SelectedDate);
                bempetr.EmpAddress = txtAdm_Emp_Address.Text;
                bempetr.MobileNo = txtAdm_Emp_MobileNo.Text;
                bempetr.PhoneNo = txtAdm_Emp_PhoneNo.Text;
                bempetr.Designation = txtAdm_Emp_Designation.Text;
                bempetr.DateOfJoining = Convert.ToDateTime(dtpAdm_Emp_DOJ.SelectedDate);
                bempetr.NoOfYears = cmbAdm_Emp_YearExp.SelectedItem.ToString();
                bempetr.Years = lblYears.Content.ToString();
                bempetr.NoOfMonths = cmbAdm_Emp_Months.SelectedItem.ToString();
                bempetr.Months = lblMonths.Content.ToString();
                bempetr.Salary = Convert.ToDouble(txtAdm_Emp_Salary.Text);
                bempetr.S_Status = "Active";

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
                bempetr.C_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                dempetr.EmployeeEntry_Insert_Update_Delete(bempetr);
                MessageBox.Show("Data Save Successfully");
                ResetText();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            EEMPLOYEEid();
        }

        private void btnAdm_Emp_Clear_Click(object sender, RoutedEventArgs e)
        {
            ResetText();
        }

        private void smemployee_Click(object sender, RoutedEventArgs e)
        {
            grd_EmployeeDetails.Visibility = System.Windows.Visibility.Visible;
            EEMPLOYEEid();
            LoadNoOfYears();
            LoadNoOfMonths();
        }

        private void btnAdm_Emp_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_EmployeeDetails.Visibility = System.Windows.Visibility.Hidden;

        }
        #endregion EmployeeEntry Button Event

        #region EmployeeEntry Function
        public bool Employee_Validation()
        {
            bool result = false;
            if (txtAdm_EmpName.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Employee Name", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Emp_Address.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Employee Address", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Emp_MobileNo.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Employee Mobile No", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Emp_Designation.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Employee Designation", caption, MessageBoxButton.OK);
            }
            else if (dtpAdm_Emp_DOJ.Text == "")
            {
                result = true;
                MessageBox.Show("Please Select Employee Joining Date", caption, MessageBoxButton.OK);
            }
            else if (cmbAdm_Emp_YearExp.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Employee Experience Year", caption, MessageBoxButton.OK);
            }
            else if (cmbAdm_Emp_Months.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Employee Experience Month", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Emp_Salary.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Employee Salary", caption, MessageBoxButton.OK);
            }
            return result;
        }

        public void ResetText()
        {
            //txtAdm_EmpID.Text = "";
            txtAdm_EmpName.Text = "";
            txtAdm_Emp_MobileNo.Text = "";
            txtAdm_Emp_PhoneNo.Text = "";
            txtAdm_Emp_Designation.Text = "";
            //txtAdm_Emp_Experience.Text = "";
            txtAdm_Emp_Salary.Text = "";
            dtpAdm_Emp_DOB.SelectedDate = null;
            dtpAdm_Emp_DOJ.SelectedDate = null;
            cmbAdm_Emp_YearExp.Text = "Select Year";
            cmbAdm_Emp_Months.Text = "Select Months";
            txtAdm_Emp_Address.Text = "";
            cmbAdm_Emp_Months.Visibility = System.Windows.Visibility.Hidden;
            lblMonths.Visibility = System.Windows.Visibility.Hidden;
        }

        public void LoadNoOfYears()
        {
            cmbAdm_Emp_YearExp.Text = "Select Year";
            cmbAdm_Emp_YearExp.Items.Add("0");
            cmbAdm_Emp_YearExp.Items.Add("1");
            cmbAdm_Emp_YearExp.Items.Add("2");
            cmbAdm_Emp_YearExp.Items.Add("3");
            cmbAdm_Emp_YearExp.Items.Add("4");
            cmbAdm_Emp_YearExp.Items.Add("5");
            cmbAdm_Emp_YearExp.Items.Add("6");
            cmbAdm_Emp_YearExp.Items.Add("7");
            cmbAdm_Emp_YearExp.Items.Add("8");
            cmbAdm_Emp_YearExp.Items.Add("9");
            cmbAdm_Emp_YearExp.Items.Add("10");
            cmbAdm_Emp_YearExp.Items.Add("11");
            cmbAdm_Emp_YearExp.Items.Add("12");
            cmbAdm_Emp_YearExp.Items.Add("13");
            cmbAdm_Emp_YearExp.Items.Add("14");
            cmbAdm_Emp_YearExp.Items.Add("15");
        }

        public void LoadNoOfMonths()
        {
            cmbAdm_Emp_Months.Text = "Select Months";
            cmbAdm_Emp_Months.Items.Add("0");
            cmbAdm_Emp_Months.Items.Add("1");
            cmbAdm_Emp_Months.Items.Add("2");
            cmbAdm_Emp_Months.Items.Add("3");
            cmbAdm_Emp_Months.Items.Add("4");
            cmbAdm_Emp_Months.Items.Add("5");
            cmbAdm_Emp_Months.Items.Add("6");
            cmbAdm_Emp_Months.Items.Add("7");
            cmbAdm_Emp_Months.Items.Add("8");
            cmbAdm_Emp_Months.Items.Add("9");
            cmbAdm_Emp_Months.Items.Add("10");
            cmbAdm_Emp_Months.Items.Add("11");
        }

        public void EEMPLOYEEid()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from tbl_Employee", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            lblEmpID.Content = "# Emp /" + id1.ToString();
            con.Close();


        }

        private void cmbAdm_Emp_YearExp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbAdm_Emp_Months.Visibility = System.Windows.Visibility.Visible;
            lblMonths.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion EmployeeEntry Function

        #region EmployeeEdit Fun
        public void GetData_EmployeeDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT [ID],[EmployeeID],[EmployeeName],[DateOfBirth],[EmpAddress],[MobileNo],[Designation],[DateOfJoining],[NoOfYears] + ' ' + [Years] + ' , ' + [NoOfMonths] + ' ' + [Months] AS [Experience],[Salary] " +
                      "FROM [tbl_Employee] " +
                      "WHERE ";
                if (txtAdm_EmployeeName_Search.Text.Trim() != "")
                {
                    str = str + "[EmployeeName] LIKE ISNULL('" + txtAdm_EmployeeName_Search.Text.Trim() + "',[EmployeeName]) + '%' AND ";
                }
                if (txtAdm_EmployeeMN_Search.Text.Trim() != "")
                {
                    str = str + "[MobileNo] LIKE ISNULL('" + txtAdm_EmployeeMN_Search.Text.Trim() + "',[MobileNo]) + '%' AND ";
                }
                str = str + " [S_Status] = 'Active' ORDER BY [EmployeeName] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_EmployeeDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        private void smemployeedetails_Click(object sender, RoutedEventArgs e)
        {
            grd_EmployeeDet.Visibility = System.Windows.Visibility.Visible;
            GetData_EmployeeDetails();
        }
        #endregion EmployeeEdit Fun

        #region EmployeeEdit Button Event
        private void btnAdm_EmployeeExit_Click(object sender, RoutedEventArgs e)
        {
            grd_EmployeeDet.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btndgv_EmployeeDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id1 = (DataRowView)dgvAdm_EmployeeDetails.SelectedItem;  //Get specific ID From DataGrid after click on Delete Button.

                PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());
                //SqlConnection con = new SqlConnection(sqlstring);
                con.Open();
                string sqlquery = "UPDATE tbl_Employee SET S_Status='DeActive' where ID='" + PK_ID + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Deleted Successfully...", caption, MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            GetData_EmployeeDetails();
        }

        private void btndgv_EmployeeEditUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id1 = (DataRowView)dgvAdm_EmployeeDetails.SelectedItem; //get specific ID from          DataGrid after click on Edit button in DataGrid   
                PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());
                con.Open();
                string sqlquery = "SELECT * FROM tbl_Employee where Id='" + PK_ID + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtAdm_EmployeeID.Text = dt.Rows[0]["ID"].ToString();
                }

                frmCRM_EmpDetailsEdit obj = new frmCRM_EmpDetailsEdit();
                obj.EmployeeID(txtAdm_EmployeeID.Text.Trim());
                obj.FillData();
                obj.LoadNoOfYears1();
                obj.LoadNoOfMonths1();
                obj.ShowDialog();

                // con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            GetData_EmployeeDetails();
        }
        #endregion EmployeeEdit Button Event

        #region EmployeeEdit Event
        private void txtAdm_EmployeeName_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetData_EmployeeDetails();
        }

        private void txtAdm_EmployeeMN_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetData_EmployeeDetails();
        }
        #endregion EmployeeEdit Event
        #endregion Employee Function

        #region Dealer Function

        #region Dealer Button Event
        private void btnAdm_Dealer_Save_Click(object sender, RoutedEventArgs e)
        {
            if (Dealer_Validation() == true)
                return;

            try
            {
                bdealeretr.Flag = 1;
                bdealeretr.DealerEntryID = lblDealerID.Content.ToString();
                bdealeretr.CompanyName = txtAdm_CompanyName.Text;
                bdealeretr.DealerFirstName = txtAdm_DealerFirstName.Text;
                bdealeretr.DealerLastName = txtAdm_DealerLastName.Text;
                bdealeretr.DateOfBirth = Convert.ToDateTime(dtpAdm_Dealer_DOB.SelectedDate);
                bdealeretr.MobileNo = txtAdm_Dealer_MobileNo.Text;
                bdealeretr.PhoneNo = txtAdm_Dealer_PhoneNo.Text;
                bdealeretr.DealerAddress = txtAdm_Dealer_Address.Text;
                bdealeretr.City = txtAdm_Dealer_City.Text;
                bdealeretr.Zip = txtAdm_Dealer_Zip.Text;
                bdealeretr.DState = txtAdm_Dealer_State.Text;
                bdealeretr.Country = txtAdm_Dealer_Country.Text;
                bdealeretr.S_Status = "Active";

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
                bdealeretr.C_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                ddealeretr.EmployeeEntry_Insert_Update_Delete(bdealeretr);
                MessageBox.Show("Data Save Successfully");
                Dealer_ResetText();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            Dealerid();
        }
        
        private void btnAdm_Dealer_Clear_Click(object sender, RoutedEventArgs e)
        {
            Dealer_ResetText();
        }

        private void smdealerentry_Click(object sender, RoutedEventArgs e)
        {
            grd_DealerEntry.Visibility = System.Windows.Visibility.Visible;
            Dealerid();
        }

        private void btnAdm_Dealer_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_DealerEntry.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_DealerRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtAdm_CompName_Search.Text = "";
            txtAdm_DealerMN_Search.Text = "";
            txtAdm_DealerName_Search.Text = "";
            DealerDetails_LoadData();
        }
        
        private void btndgv_DealerEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id1 = (DataRowView)dgvAdm_Dealerdetails.SelectedItem; //get specific ID from          DataGrid after click on Edit button in DataGrid   
                PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());
                con.Open();
                string sqlquery = "SELECT * FROM tbl_DealerEntry where Id='" + PK_ID + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtAdm_DealerID1.Text = dt.Rows[0]["ID"].ToString();
                }

                frmCRM_DealerDetailsEdit obj = new frmCRM_DealerDetailsEdit();
                obj.DealerID(txtAdm_DealerID1.Text.Trim());
                obj.FillData();
                obj.ShowDialog();
                
               // con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            DealerDetails_LoadData();
        }

        private void btndgv_DealerDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id1 = (DataRowView)dgvAdm_Dealerdetails.SelectedItem;  //Get specific ID From DataGrid after click on Delete Button.

                PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());
                //SqlConnection con = new SqlConnection(sqlstring);
                con.Open();
                string sqlquery = "UPDATE tbl_DealerEntry SET S_Status='DeActive' where ID='" + PK_ID + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Deleted Successfully...", caption, MessageBoxButton.OK);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            DealerDetails_LoadData();
        }

        private void btnAdm_DealerExit_Click(object sender, RoutedEventArgs e)
        {
            grd_DealerDetails.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_FinalProcurment_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalProcurment.Visibility = System.Windows.Visibility.Hidden;
        }

        private void smviewprocurement_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalProcurment.Visibility = System.Windows.Visibility.Visible;
            LoadFinal();
            Final_PreProcurement();
        }

        private void txtAdm_Dealer_Filter_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Final_PreProcurement();
        }

        private void dtpAdmTo_Dealer_Search_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Final_PreProcurement();
        }

        private void dtpAdmBetween_Dealer_Search_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Final_PreProcurement();
        }

        private void btnAdm_FinalRefresh_Click(object sender, RoutedEventArgs e)
        {
            dtpAdmTo_Dealer_Search.SelectedDate = null;
            dtpAdmBetween_Dealer_Search.SelectedDate = null;
            cmbAdm_DealerFilter_Search.Text = "Select";
            txtAdm_Dealer_Filter_Search.Text = "";
            grd_FinalizeProducts.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion Dealer Button Event

        #region Dealer Fun
        public bool Dealer_Validation()
        {
            bool result = false;
            if(txtAdm_CompanyName.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Company Name", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_DealerFirstName.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Dealer First Name", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_DealerLastName.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Dealer Last Name", caption, MessageBoxButton.OK);
            }
            else if (dtpAdm_Dealer_DOB.Text == "")
            {
                result = true;
                MessageBox.Show("Please Select Dealer Date Of Birth", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Dealer_MobileNo.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Dealer Mobile No", caption, MessageBoxButton.OK);
            }
            else if(txtAdm_Dealer_PhoneNo.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Dealer Phone No", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Dealer_Address.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Dealer Address", caption, MessageBoxButton.OK);
            }
            return result;
        }

        public void Dealer_ResetText()
        {
            txtAdm_CompanyName.Text = "";
            txtAdm_DealerFirstName.Text = "";
            txtAdm_DealerLastName.Text = "";
            dtpAdm_Dealer_DOB.SelectedDate = null;
            txtAdm_Dealer_MobileNo.Text = "";
            txtAdm_Dealer_PhoneNo.Text = "";
            txtAdm_Dealer_Address.Text = "";
            txtAdm_Dealer_City.Text = "";
            txtAdm_Dealer_Zip.Text = "";
        }

        public void Dealerid()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from tbl_DealerEntry", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            lblDealerID.Content = "# Dealer /" + id1.ToString();
            con.Close();


        }
       
        public void DealerDetails_LoadData()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT [ID],[DealerEntryID],[CompanyName],[DealerFirstName] + ' ' + [DealerLastName] AS [DealerName],[DateOfBirth],[MobileNo],[PhoneNo],[DealerAddress] " +
                             "FROM [tbl_DealerEntry] " +
                             "WHERE ";
                if (txtAdm_CompName_Search.Text.Trim() != string.Empty)
                {
                    str = str + "[CompanyName] LIKE ISNULL('" + txtAdm_CompName_Search.Text.Trim() + "',CompanyName) + '%' AND ";
                }
                if (txtAdm_DealerName_Search.Text.Trim() != string.Empty)
                {
                    str = str + "[DealerFirstName] LIKE ISNULL('" + txtAdm_DealerName_Search.Text.Trim() + "',DealerFirstName) + '%' AND ";
                }
                if (txtAdm_DealerMN_Search.Text.Trim() != string.Empty)
                {
                    str = str + "[MobileNo] LIKE ISNULL('" + txtAdm_DealerMN_Search.Text.Trim() + "',MobileNo) + '%' AND ";
                }
                str = str + " S_Status = 'Active' ORDER BY DealerName ASC ";
                SqlCommand cmd = new SqlCommand(str,con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                    dgvAdm_Dealerdetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
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
        #endregion Fun

        #region Dealer Event
        private void smdealerDetails_Click(object sender, RoutedEventArgs e)
        {
            grd_DealerDetails.Visibility = System.Windows.Visibility.Visible;
            DealerDetails_LoadData();
        }

        private void txtAdm_DealerName_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            DealerDetails_LoadData();
        }

        private void txtAdm_DealerMN_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            DealerDetails_LoadData();
        }

        private void txtAdm_CompName_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            DealerDetails_LoadData();
        }

        private void dgvAdm_Dealerdetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion Dealer Event

        #endregion Function

        #region Final Pro
        private bool FinalPro_Validation()
        {
            bool result = false;
            if (txtPrice.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Price", caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txtQuantityF.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Quantity", caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (dtpFinalDate.Text == "")
            {
                result = true;
                MessageBox.Show("Please Select Date", caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return result;
        }
        
        public void Final_PreProcurement()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[DealerID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Warranty],P.[Quantity],P.[C_Date],P.[Have_Insurance] " +
                      ",D.[DealerFirstName] + '' + D.[DealerLastName] AS [DealerName],D.[MobileNo],D.[PhoneNo] " +
                      ",DM.[Domain_Name] + ' , ' +  PM.[Product_Name] + ' , ' + B.[Brand_Name] + ' , ' + PC.[Product_Category] + ' , ' + MN.[Model_No] + ' , ' + C.[Color] AS [Products]" +
                      ",PP.[Price] " +
                      "FROM [Pre_Procurement] P " +
                      "INNER JOIN [tbl_DealerEntry] D ON D.[ID] = P.[DealerID] " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "INNER JOIN [Pre_Products] PP ON PP.[Model_No_ID]=P.[Model_No_ID] " +
                      "WHERE ";
                if ((dtpAdmTo_Dealer_Search.SelectedDate != null) && (dtpAdmBetween_Dealer_Search.SelectedDate != null))
                {
                    DateTime StartDate = Convert.ToDateTime(dtpAdmTo_Dealer_Search.Text.Trim() + " 00:00:00.000");
                    DateTime EndDate = Convert.ToDateTime(dtpAdmBetween_Dealer_Search.Text.Trim() + " 23:59:59.999");
                    str = str + "P.[C_Date] Between '" + StartDate + "' AND '" + EndDate + "'  AND ";
                }

                //if (cmbAdm_DealerFilter_Search.Text.Equals("Domain"))
                //{
                //    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                //    {
                //        str = str + "DM.[Domain_Name] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',DM.[Domain_Name]) + '%' AND ";
                //    }
                //}
                if (cmbAdm_DealerFilter_Search.Text.Equals("Product Type"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "PM.[Product_Name] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',PM.[Product_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Brand"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "B.[Brand_Name] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',B.[Brand_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Product Category"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "PC.[Product_Category] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',PC.[Product_Category]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Model"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "MN.[Model_No] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',MN.[Model_No]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Color"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "C.[Color] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',C.[Color]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Products / Services"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "[Products] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',[Products]) + '%' AND ";
                    }
                }
                str = str + " P.[S_Status] = 'Active' ORDER BY P.[C_Date] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_FinalProcurement.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void LoadFinal()
        {
            cmbAdm_DealerFilter_Search.Text = "Select";
            //cmbAdm_DealerFilter_Search.Items.Add("Domain");
            cmbAdm_DealerFilter_Search.Items.Add("Product Type");
            cmbAdm_DealerFilter_Search.Items.Add("Brand");
            cmbAdm_DealerFilter_Search.Items.Add("Product Category");
            cmbAdm_DealerFilter_Search.Items.Add("Model");
            cmbAdm_DealerFilter_Search.Items.Add("Color");
            cmbAdm_DealerFilter_Search.Items.Add("Products / Services");
        }

        private bool CheckProduct()
        {
            try
            {
                bool result = false;
                string str = "SELECT * FROM [StockDetails] WHERE [S_Status] = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (txtAdm_DomainID.Text.Trim() == dt.Rows[i]["Domain_ID"].ToString())
                        {
                            if (txtAdm_ProductID.Text.Trim() == dt.Rows[i]["Product_ID"].ToString())
                            {
                                if (txtAdm_BrandID.Text.Trim() == dt.Rows[i]["Brand_ID"].ToString())
                                {
                                    if (txtAdm_ProductCatID.Text.Trim() == dt.Rows[i]["P_Category"].ToString())
                                    {
                                        if (txtAdm_ModelID.Text.Trim() == dt.Rows[i]["Model_No_ID"].ToString())
                                        {
                                            if (txtAdm_ColorID.Text.Trim() == dt.Rows[i]["Color_ID"].ToString())
                                            {
                                                //if ((txtAdm_DomainID.Text.Trim() == dt.Rows[i]["Domain_ID"].ToString()) && (txtAdm_ProductID.Text.Trim() == dt.Rows[i]["Product_ID"].ToString()) &&
                                                //    (txtAdm_BrandID.Text.Trim() == dt.Rows[i]["Brand_ID"].ToString()) && (txtAdm_ProductCatID.Text.Trim() == dt.Rows[i]["P_Category"].ToString()) &&
                                                //    (txtAdm_ModelID.Text.Trim() == dt.Rows[i]["Model_No_ID"].ToString()) && (txtAdm_ColorID.Text.Trim() == dt.Rows[i]["Color_ID"].ToString()))
                                                //string qry = "Select [ID],[Domain_ID],[Product_ID],[Brand_ID],[P_Category],[Model_No_ID],[Color_ID] From [StockDetails] Where [Domain_ID] = '" + txtAdm_DomainID.Text.Trim() + "' And [Product_ID] = '" + txtAdm_ProductID.Text.Trim() + "' And [Brand_ID] = '" + txtAdm_BrandID.Text.Trim() + "' And [P_Category] = '" + txtAdm_ProductCatID.Text.Trim() + "' And [Model_No_ID] = '" + txtAdm_ModelID.Text.Trim() + "' And [Color_ID] = '" + txtAdm_ColorID.Text.Trim() + "' ";
                                                string qry = "Select [ID],[Products123] From [StockDetails] Where  [Products123] = '" + lblProducts.Content.ToString() + "' ";
                                                
                                                SqlCommand cmd1 = new SqlCommand(qry, con);
                                                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                                                DataTable dt1 = new DataTable();
                                                adp.Fill(dt1);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    txtAdm_StockID.Text = dt.Rows[0]["ID"].ToString();
                                                }
                                                

                                                result = true;
                                                return result;
                                            }
                                            else
                                            {
                                                result = false;
                                            }
                                                                                        
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return result;
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

        public void Final_DealerDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[SalesID],P.[Dealer_ID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[FinalQty],P.[NetAmt],P.[SDefault],P.[ServiceIntervalMonth],P.[C_Date] " +
                      ",D.[DealerFirstName] + '' + D.[DealerLastName] AS [DealerName],D.[MobileNo],D.[PhoneNo] " +
                      ",DM.[Domain_Name] + ' , ' +  PM.[Product_Name] + ' , ' + B.[Brand_Name] + ' , ' + PC.[Product_Category] + ' , ' + MN.[Model_No] + ' , ' + C.[Color] AS [Products]" +
                      ",PP.[Price] " +
                      "FROM [Final_DealerDetails] P " +
                      "INNER JOIN [tbl_DealerEntry] D ON D.[ID] = P.[Dealer_ID] " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "INNER JOIN [Pre_Products] PP ON PP.[Model_No_ID]=P.[Model_No_ID] " +
                      "WHERE ";
                if ((dtpAdm_From_FinalDealer.SelectedDate != null) && (dtpAdm_To_FinalDealer.SelectedDate != null))
                {
                    DateTime StartDate = Convert.ToDateTime(dtpAdm_From_FinalDealer.Text.Trim() + " 00:00:00.000");
                    DateTime EndDate = Convert.ToDateTime(dtpAdm_To_FinalDealer.Text.Trim() + " 23:59:59.999");
                    str = str + "P.[C_Date] Between '" + StartDate + "' AND '" + EndDate + "'  AND ";
                }

                if (txtAdm_FDealerName_Search.Text.Trim() != "")
                {
                    str = str + "D.[DealerFirstName] LIKE ISNULL('" + txtAdm_FDealerName_Search.Text.Trim() + "',D.[DealerFirstName]) + '%' AND ";
                }

                if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                {
                    str = str + "D.[MobileNo] LIKE ISNULL('" + txtAdm_FDealerMN_Search.Text.Trim() + "',D.[MobileNo]) + '%' AND ";
                }
                str = str + " P.[S_Status] = 'Active' ORDER BY P.[C_Date] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_FDealerDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        #region Final Product Event
        private void dgvAdm_FinalProcurement_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //try
            //{
            //    //var id1 = (DataRowView)dgvAdm_FinalProcurement.SelectedItem; //get specific ID from          DataGrid after click on Edit button in DataGrid   
            //    //PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());
            //    //con.Open();
            //    ////string sqlquery = "SELECT * FROM tbl_DealerEntry where Id='" + PK_ID + "' ";

            //    //string sqlquery = "SELECT P.[ID],P.[DealerID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Warranty],P.[Quantity],P.[C_Date],P.[Net_Amount] " +
            //    //      ",D.[DealerFirstName] + '' + D.[DealerLastName] AS [DealerName],D.[MobileNo],D.[PhoneNo] " +
            //    //      ",DM.[Domain_Name] + ' , ' +  PM.[Product_Name] + ' , ' + B.[Brand_Name] + ' , ' + PC.[Product_Category] + ' , ' + MN.[Model_No] + ' , ' + C.[Color] AS [Products]" +
            //    //      ",PP.[Price] " +
            //    //      "FROM [Pre_Procurement] P " +
            //    //      "INNER JOIN [tbl_DealerEntry] D ON D.[ID] = P.[DealerID] " +
            //    //      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
            //    //      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
            //    //      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
            //    //      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
            //    //      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
            //    //      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
            //    //      "INNER JOIN [Pre_Products] PP ON PP.[Model_No_ID]=P.[Model_No_ID] " +
            //    //      "WHERE P.[ID]='" + PK_ID + "' ";
       
            //    //SqlCommand cmd = new SqlCommand(sqlquery, con);
            //    //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    //DataTable dt = new DataTable();
            //    //adp.Fill(dt);
            //    //if (dt.Rows.Count > 0)
            //    //{
            //    //    txtAdm_DealerID.Text = dt.Rows[0]["DealerID"].ToString();
            //    //    txtAdm_DomainID.Text = dt.Rows[0]["Domain_ID"].ToString();
            //    //    txtAdm_ProductID.Text = dt.Rows[0]["Product_ID"].ToString();
            //    //    txtAdm_BrandID.Text = dt.Rows[0]["Brand_ID"].ToString();
            //    //    txtAdm_ProductCatID.Text = dt.Rows[0]["P_Category"].ToString();
            //    //    txtAdm_ModelID.Text = dt.Rows[0]["Model_No_ID"].ToString();
            //    //    txtAdm_ColorID.Text = dt.Rows[0]["Color_ID"].ToString();

            //    //    lblProcDate.Content = dt.Rows[0]["C_Date"].ToString();
            //    //    lblProducts.Content = dt.Rows[0]["Products"].ToString();
            //    //    double Abc = Convert.ToDouble(dt.Rows[0]["Net_Amount"].ToString());
            //    //    lblProceNetAmt.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(Abc, "##,###.00"));
            //    //    double price = Convert.ToDouble(dt.Rows[0]["Price"].ToString());
            //    //    lblProcePrice.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(price, "##,###.00"));
            //    //    double qt = Convert.ToDouble(dt.Rows[0]["Quantity"].ToString());
            //    //    lblProceQty.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(qt, "##,###.00"));
            //    //}

            //    //grd_FinalizeProducts.Visibility = System.Windows.Visibility.Visible;
            //}
            //catch(Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    con.Close();
            //}
            //Salesid();

            ////Final_PreProcurement();
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txtPrice4.Text == "")
            {
                MessageBox.Show("Please Insert Price", caption, MessageBoxButton.OK);
                txtQuantity4.Text = 0.ToString();

            }
            else if (txtQuantity4.Text == "")
            {
                txtTotalPrice4.Text = txtPrice4.Text;
            }
            else if (txtPrice4.Text != "" && txtQuantity4.Text != "")
            {
                double tamt1;
                nfi = (NumberFormatInfo)nfi.Clone();
                nfi.CurrencySymbol = "";

                double prc = Convert.ToDouble(txtPrice4.Text);
                double qty = Convert.ToDouble(txtQuantity4.Text);
                double tamt = (prc * qty);
                txtTotalPrice4.Text = tamt.ToString();
                //  txtpreroundoff.Text = Math.Round(tamt).ToString();
                //roundoff Method
                if (txtTotalPrice4.Text.Trim().Length > 0)
                {
                    tamt1 = Convert.ToDouble(txtTotalPrice4.Text);
                }
                else
                {
                    tamt1 = 0;
                }
                double netAmt = Math.Round(tamt1);
                double roundDiff = netAmt - tamt1;
                double roundDiff1 = Math.Round(roundDiff, 2);
                
                txtNetAmount4.Text = String.Format(nfi, "{0:C}", Convert.ToDouble(netAmt));
                //txtRoundUp.Text = String.Format(nfi, "{0:C}", Convert.ToDouble(roundDiff));
                txtpreroundoff4.Text = Convert.ToString(roundDiff1);

            }

        }

        private void chkaddToSale_Checked(object sender, RoutedEventArgs e)
        {
            object item = dgvAdm_FinalProcurement.SelectedItem;
            string ID = (dgvAdm_FinalProcurement.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            //MessageBox.Show(ID);
            grd_FinalizeProducts.Visibility = Visibility;
            Salesid();
            //txtAdm_AvilableQty.Text = "0.00";

            try
            {
                con.Open();
                string sqlquery = "SELECT P.[ID],P.[DealerID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Warranty],P.[Quantity],P.[C_Date],P.[Net_Amount],P.[Have_Insurance] " +
                      ",D.[DealerFirstName] + '' + D.[DealerLastName] AS [DealerName],D.[MobileNo],D.[PhoneNo] " +
                      ",DM.[Domain_Name] + ' , ' +  PM.[Product_Name] + ' , ' + B.[Brand_Name] + ' , ' + PC.[Product_Category] + ' , ' + MN.[Model_No] + ' , ' + C.[Color] AS [Products]" +
                      ",PP.[Price] " +
                      "FROM [Pre_Procurement] P " +
                      "INNER JOIN [tbl_DealerEntry] D ON D.[ID] = P.[DealerID] " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "INNER JOIN [Pre_Products] PP ON PP.[Model_No_ID]=P.[Model_No_ID] " +
                      "WHERE P.[ID]='" + ID + "' ";

                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtAdm_DealerID.Text = dt.Rows[0]["DealerID"].ToString();
                    txtAdm_DomainID.Text = dt.Rows[0]["Domain_ID"].ToString();
                    txtAdm_ProductID.Text = dt.Rows[0]["Product_ID"].ToString();
                    txtAdm_BrandID.Text = dt.Rows[0]["Brand_ID"].ToString();
                    txtAdm_ProductCatID.Text = dt.Rows[0]["P_Category"].ToString();
                    txtAdm_ModelID.Text = dt.Rows[0]["Model_No_ID"].ToString();
                    txtAdm_ColorID.Text = dt.Rows[0]["Color_ID"].ToString();

                    lblProcDate.Content = dt.Rows[0]["C_Date"].ToString();
                    lblProducts.Content = dt.Rows[0]["Products"].ToString();
                    double Abc = Convert.ToDouble(dt.Rows[0]["Net_Amount"].ToString());
                    lblProceNetAmt.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(Abc, "##,###.00"));
                    double price = Convert.ToDouble(dt.Rows[0]["Price"].ToString());
                    lblProcePrice.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(price, "##,###.00"));
                    double qt = Convert.ToDouble(dt.Rows[0]["Quantity"].ToString());
                    lblProceQty.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(qt, "##,###.00"));
                    lblInsurance.Content = dt.Rows[0]["Have_Insurance"].ToString();
                }

                //grd_FinalizeProducts.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            //Salesid();
        }

        private void txtAdm_FDealerName_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Final_DealerDetails();
        }
        
        private void txtAdm_FDealerMN_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Final_DealerDetails();
        }

        private void dtpAdm_From_FinalDealer_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Final_DealerDetails();
        }

        private void dtpAdm_To_FinalDealer_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Final_DealerDetails();
        }

        private void txtQuantityF_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPrice.Text == "")
            {
                //MessageBox.Show("Please Insert Price", caption, MessageBoxButton.OK);
                txtQuantityF.Text = 0.ToString();

            }
            else if (txtQuantityF.Text == "")
            {
                txtTotalPrice.Text = txtPrice.Text;
            }
            else if (txtPrice.Text != "" && txtQuantityF.Text != "")
            {
                double tamt1;
                nfi = (NumberFormatInfo)nfi.Clone();
                nfi.CurrencySymbol = "";

                double prc = Convert.ToDouble(txtPrice.Text);
                double qty = Convert.ToDouble(txtQuantityF.Text);
                double tamt = (prc * qty);
                txtTotalPrice.Text = tamt.ToString();
                //  txtpreroundoff.Text = Math.Round(tamt).ToString();
                //roundoff Method
                if (txtTotalPrice.Text.Trim().Length > 0)
                {
                    tamt1 = Convert.ToDouble(txtTotalPrice.Text);
                }
                else
                {
                    tamt1 = 0;
                }
                double netAmt = Math.Round(tamt1);
                double roundDiff = netAmt - tamt1;
                double roundDiff1 = Math.Round(roundDiff, 2);

                txtNetAmount.Text = String.Format(nfi, "{0:C}", Convert.ToDouble(netAmt));
                //txtRoundUp.Text = String.Format(nfi, "{0:C}", Convert.ToDouble(roundDiff));
                txtpreroundoff.Text = Convert.ToString(roundDiff1);

            }
        }

        #endregion Final Product Event

        #region DealerSales
        public void Salesid()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from Final_DealerDetails", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            lblSalesNo.Content = "# Sales /" + id1.ToString();
            con.Close();


        }
        #endregion DealerSales

        #region FinalProcurement Button Event
        private void finaldealerDetails_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalDealerDetails.Visibility = System.Windows.Visibility.Visible;
            Final_DealerDetails();
        }

        private void btnAdm_FinalDealerExit_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalDealerDetails.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_FDealerRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtAdm_FDealerMN_Search.Text = "";
            txtAdm_FDealerName_Search.Text = "";
            Final_DealerDetails();
        }

        private void btnFinalProcurement_Close_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalizeProducts.Visibility = System.Windows.Visibility.Hidden;
            txtAdm_BrandID.Text = "";
            txtAdm_AvilableQty.Text = "";
            txtAdm_ColorID.Text = "";
            txtAdm_DomainID.Text = "";
            txtAdm_ProductCatID.Text = "";
            txtAdm_ProductID.Text = "";
            txtAdm_DealerID.Text = "";
            lblSalesNo.Content = "";
            lblProcDate.Content = "";
            lblProducts.Content = "";
            lblProceNetAmt.Content = "";
            lblProcePrice.Content = "";
            txtPrice.Text = "";
            txtQuantityF.Text = "";
            dtpFinalDate.Text = "";
            txtTotalPrice.Text = "";
            txtpreroundoff.Text = "";
            txtNetAmount.Text = "";
            txtAdm_StockID.Text = "";

        }

        private void btnFinalProcurement_Click(object sender, RoutedEventArgs e)
        {
            if (FinalPro_Validation() == true)
                return;

            if (CheckProduct() == true)
            {
                try
                {
                    bstockDet.Flag = 1;
                    bstockDet.SID = Convert.ToInt32(txtAdm_StockID.Text);
                    //bstockDet.Products123 = lblProducts.Content.ToString();
                    bstockDet.NewQty = txtQuantityF.Text;
                    bstockDet.FinalPrice = Convert.ToDouble(txtPrice.Text);
                    bstockDet.S_Status = "Active";
                    bstockDet.C_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
                    dstUpdate.AddStockDetailsUp_Insert_Update_Delete(bstockDet);
                    //MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

                AddQuantity_Check();
                AddQuantity();
                try
                {
                    bstockDet.Flag = 1;
                    bstockDet.SID = Convert.ToInt32(txtAdm_StockID.Text);
                    //bstockDet.Products123 = lblProducts.Content.ToString();
                    bstockDet.AvilableQty = Convert.ToString(add);
                    daddqty.AddQtyStockDetails_Insert_Update_Delete(bstockDet);
                    //MessageBox.Show("Quantity Save Succesfully...", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }


                try
                {
                    bstockDet.Flag = 1;
                    bstockDet.SID = Convert.ToInt32(txtAdm_StockID.Text);
                    //bstockDet.Products123 = lblProducts.Content.ToString();
                    bstockDet.AvilableQty = Convert.ToString(add);
                    daddqty.AddQtyStockDetails_Insert_Update_Delete(bstockDet);
                    //MessageBox.Show("Quantity Save Succesfully...", caption, MessageBoxButton.OK, MessageBoxImage.Information);
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
            else
            {
                try
                {
                    bstockDet.Flag = 1;
                    bstockDet.DomainID = Convert.ToInt32(txtAdm_DomainID.Text);
                    bstockDet.ProductID = Convert.ToInt32(txtAdm_ProductID.Text);
                    bstockDet.BrandID = Convert.ToInt32(txtAdm_BrandID.Text);
                    bstockDet.ProductCatID = Convert.ToInt32(txtAdm_ProductCatID.Text);
                    bstockDet.ModelID = Convert.ToInt32(txtAdm_ModelID.Text);
                    bstockDet.ColorId = Convert.ToInt32(txtAdm_ColorID.Text);
                    bstockDet.Products1234 = lblProducts.Content.ToString();

                    // bstockDet.Products123= lblProducts.Content.ToString();


                    bstockDet.Products1234 = lblProducts.Content.ToString();
                    //bstockDet.Products123= lblProducts.Content.ToString();
                    bstockDet.AvilableQty = txtQuantityF.Text;
                    bstockDet.SaleQty = txtSaleQuantity.Text;
                    bstockDet.NewQty = txtQuantityF.Text;
                    bstockDet.FinalPrice = Convert.ToDouble(txtPrice.Text);
                    bstockDet.Insurance = lblInsurance.Content.ToString();
                    bstockDet.S_Status = "Active";

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
                    bstockDet.C_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
                    dstockDet.AddStockDetails_Insert_Update_Delete(bstockDet);
                    //MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);

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

            string abc;

            if (chbDefault.IsChecked == true)
            {
                abc = "Default";
            }
            else
            {
                abc = "No";
            }

            //final dealer
            try
            {
                bfinaldealer1.Flag = 1;
                bfinaldealer1.FDealerID = Convert.ToInt32(txtAdm_DealerID.Text);
                bfinaldealer1.SalesID = lblSalesNo.Content.ToString();
                bfinaldealer1.Domain_ID = Convert.ToInt32(txtAdm_DomainID.Text);
                bfinaldealer1.Product_ID = Convert.ToInt32(txtAdm_ProductID.Text);
                bfinaldealer1.Brand_ID = Convert.ToInt32(txtAdm_BrandID.Text);
                bfinaldealer1.P_Category = Convert.ToInt32(txtAdm_ProductCatID.Text);
                bfinaldealer1.Model_No_ID = Convert.ToInt32(txtAdm_ModelID.Text);
                bfinaldealer1.Color_ID = Convert.ToInt32(txtAdm_ColorID.Text);
                bfinaldealer1.ProcNetAmt = Convert.ToDouble(lblProceNetAmt.Content.ToString());
                bfinaldealer1.ProcPrice = Convert.ToDouble(lblProcePrice.Content.ToString());
                bfinaldealer1.ProcQty = lblProceQty.Content.ToString();
                bfinaldealer1.FinalPrice = Convert.ToDouble(txtPrice.Text);
                bfinaldealer1.FinalQty = txtQuantityF.Text;
                bfinaldealer1.SubTotal = Convert.ToDouble(txtTotalPrice.Text);
                bfinaldealer1.RoundUp = Convert.ToDouble(txtpreroundoff.Text);
                bfinaldealer1.NetAmt = Convert.ToDouble(txtNetAmount.Text);
                //bfinaldealer.FinalDate = Convert.ToString(dtpFinalDate.Text);
                bfinaldealer1.SDefault = abc;
                bfinaldealer1.ServiceIntervalMonth = txtAdm_FinalMonths.Text;
                //bfinaldealer1.FMonths = lblFinal_Months.Content.ToString();
                bfinaldealer1.S_Status = "Active";

                //string STRTODAYDATE = System.DateTime.Now.ToShortDateString();
                //string time = System.DateTime.Now.ToShortTimeString();
                //string[] STRVAL = STRTODAYDATE.Split('-');
                //string STR_DATE1 = STRVAL[0];
                //string STR_MONTH = STRVAL[1];
                //string STR_YEAR = STRVAL[2];
                //string DATE = STR_DATE1 + "-" + STR_MONTH + "-" + STR_YEAR;
                ////txtdate.Text = DATE;
                ////txttime.Text = time;

                //bfinaldealer.C_Date =Convert .ToDateTime(dtpFinalDate.SelectedDate.ToString);
                bfinaldealer1.C_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
                dfinaldealer.FinalDealer_Insert_Update_Delete(bfinaldealer1);
                //MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            //AddQuantity_Check();
            //AddQuantity();

            //try
            //{
            //    bstockDet.Flag = 1;
            //    bstockDet.SID = Convert.ToInt32(txtAdm_StockID.Text);
            //    //bstockDet.Products123 = lblProducts.Content.ToString();
            //    bstockDet.AvilableQty = Convert.ToString(add);
            //    daddqty.AddQtyStockDetails_Insert_Update_Delete(bstockDet);
            //    //MessageBox.Show("Quantity Save Succesfully...", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    con.Close();
            //}


            //try
            //{
            //    bstockDet.Flag = 1;
            //    bstockDet.SID = Convert.ToInt32(txtAdm_StockID.Text);
            //    //bstockDet.Products123 = lblProducts.Content.ToString();
            //    bstockDet.AvilableQty = Convert.ToString(add);
            //    daddqty.AddQtyStockDetails_Insert_Update_Delete(bstockDet);
            //    //MessageBox.Show("Quantity Save Succesfully...", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    con.Close();
            //}

            try
            {
                bfinaldealer1.Flag = 1;
                bfinaldealer1.FDealerID = Convert.ToInt32(txtAdm_DealerID.Text);
                bfinaldealer1.S_Status = "DeActive";
                dFup.FinalUpdateD_Insert_Update_Delete(bfinaldealer1);
                //MessageBox.Show("Update Final Dealer Succesfully...", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);

            //txtAdm_DomainID.Text = "";
            //txtAdm_ProductID.Text = "";
            txtAdm_StockID.Text = "";
            txtAdm_BrandID.Text = "";
            txtAdm_AvilableQty.Text = "";
            txtAdm_ColorID.Text = "";
            txtAdm_DomainID.Text = "";
            txtAdm_ProductCatID.Text = "";
            txtAdm_ProductID.Text = "";
            txtAdm_DealerID.Text = "";
            lblSalesNo.Content = "";
            lblProcDate.Content = "";
            lblProducts.Content = "";
            lblProceNetAmt.Content = "";
            lblProcePrice.Content = "";
            txtPrice.Text = "";
            txtQuantityF.Text = "";
            dtpFinalDate.Text = "";
            txtTotalPrice.Text = "";
            txtpreroundoff.Text = "";
            txtNetAmount.Text = "";

            Final_PreProcurement();

            Salesid();
        }
        #endregion FinalProcuremrnt Button Event
        #endregion Final Pro

        #region Stock Details
        #region StockDetails Button Event
        private void smstock_Click(object sender, RoutedEventArgs e)
        {
            grd_StockDetails.Visibility = System.Windows.Visibility.Visible;
            LoadStockDetails();
            StockDetails();
        }

        private void btnAdm_StockDetails_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_StockDetails.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_StockD_Refresh_Click(object sender, RoutedEventArgs e)
        {
            txtAdm_Stock_Filter_Search.Text = "";
            txtAdm_Stock_Filter_Search_Price.Text = "";
            cmbAdm_StockFilter_Search.Text = "Select";
            StockDetails();
        }
        #endregion StockDetails Button Event

        #region StockDet
        int aviQty;
        int newQty;
        int add;

        public void AddQuantity_Check()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT [ID],[AvilableQty] From [StockDetails] Where [ID]='" + txtAdm_StockID.Text.Trim() + "' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    txtAdm_AvilableQty.Text = dt.Rows[0]["AvilableQty"].ToString();
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
        }

        public void AddQuantity()
        {
            try
            {         
                aviQty = Convert.ToInt32(txtAdm_AvilableQty.Text);
                newQty = Convert.ToInt32(txtQuantityF.Text);
                add = aviQty + newQty;
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

        public void StockDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Products123],P.[AvilableQty],P.[SaleQty],P.[NewQty],P.[FinalPrice] " +
                      ",DM.[Domain_Name],PM.[Product_Name], B.[Brand_Name] , PC.[Product_Category] ,MN.[Model_No] ,C.[Color] " +
                      ",PP.[Price] " +
                      "FROM [StockDetails] P " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "INNER JOIN [Pre_Products] PP ON PP.[Model_No_ID]=P.[Model_No_ID] " +
                      "WHERE ";

                if (txtAdm_Stock_Filter_Search_Price.Text.Trim() != "")
                {
                    str = str + "P.[FinalPrice] LIKE ISNULL('" + txtAdm_Stock_Filter_Search_Price.Text.Trim() + "',P.[FinalPrice]) + '%' AND ";
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Domain"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "DM.[Domain_Name] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',DM.[Domain_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Product Type"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "PM.[Product_Name] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',PM.[Product_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Brand"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "B.[Brand_Name] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',B.[Brand_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Product Category"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "PC.[Product_Category] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',PC.[Product_Category]) + '%' AND ";
                    }
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Model"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "MN.[Model_No] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',MN.[Model_No]) + '%' AND ";
                    }
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Color"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "C.[Color] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',C.[Color]) + '%' AND ";
                    }
                }
                
                str = str + " P.[S_Status] = 'Active' ORDER BY P.[C_Date] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_StockDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void LoadStockDetails()
        {
            cmbAdm_StockFilter_Search.Text = "Select";
            cmbAdm_StockFilter_Search.Items.Add("Domain");
            cmbAdm_StockFilter_Search.Items.Add("Product Type");
            cmbAdm_StockFilter_Search.Items.Add("Brand");
            cmbAdm_StockFilter_Search.Items.Add("Product Category");
            cmbAdm_StockFilter_Search.Items.Add("Model");
            cmbAdm_StockFilter_Search.Items.Add("Color");
       }

        //private void ChangeColor()
        //{
        //    DataSet ds = new DataSet();
        //    for (int i = 0; i < dgvAdm_StockDetails.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (Convert.ToDouble(dgvAdm_StockDetails.Rows[i].Cells["BalanceQuantity"].Value.ToString()) <= Convert.ToDouble(dgvDetails.Rows[i].Cells["ReorderQuantity"].Value.ToString()))
        //            {
        //                dgvDetails.Rows[i].DefaultCellStyle.BackColor = Color.Salmon;
        //                //dgvDetails.Rows[i].DefaultCellStyle.ForeColor = Color.White;
        //            }

        //            if (Convert.ToDouble(dgvDetails.Rows[i].Cells["BalanceQuantity"].Value.ToString()) > Convert.ToDouble(dgvDetails.Rows[i].Cells["MaxQuantity"].Value.ToString()))
        //            {
        //                dgvDetails.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
        //                //dgvDetails.Rows[i].DefaultCellStyle.ForeColor = Color.White;
        //            }

        //        }
        //        catch { }
        //    }
        //}
        #endregion StockDet

        #region Stock Event
        private void txtAdm_Stock_Filter_Search_Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            StockDetails();
        }

        private void txtAdm_Stock_Filter_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            StockDetails();
        }
        #endregion Stock Event       
        #endregion StockDetails

        #region AllProduct Function
        #region AllProducts Fun
        public void AllProducts_Details()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Price] " +
                      ",DM.[Domain_Name],PM.[Product_Name], B.[Brand_Name] , PC.[Product_Category] ,MN.[Model_No] ,C.[Color] " +
                      "FROM [Pre_Products] P " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "WHERE ";

                if (txtAdm_AllProducts_Search_Price.Text.Trim() != "")
                {
                    str = str + "P.[Price] LIKE ISNULL('" + txtAdm_AllProducts_Search_Price.Text.Trim() + "',P.[Price]) + '%' AND ";
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Domain"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "DM.[Domain_Name] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',DM.[Domain_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Product Type"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "PM.[Product_Name] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',PM.[Product_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Brand"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "B.[Brand_Name] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',B.[Brand_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Product Category"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "PC.[Product_Category] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',PC.[Product_Category]) + '%' AND ";
                    }
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Model"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "MN.[Model_No] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',MN.[Model_No]) + '%' AND ";
                    }
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Color"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "C.[Color] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',C.[Color]) + '%' AND ";
                    }
                }

                str = str + " P.[S_Status] = 'Active' ORDER BY DM.[Domain_Name] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_AllProducts.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void Load_AllProducts()
        {
            cmbAdm_AllProducts_Search.Text = "Select";
            cmbAdm_AllProducts_Search.Items.Add("Domain");
            cmbAdm_AllProducts_Search.Items.Add("Product Type");
            cmbAdm_AllProducts_Search.Items.Add("Brand");
            cmbAdm_AllProducts_Search.Items.Add("Product Category");
            cmbAdm_AllProducts_Search.Items.Add("Model");
            cmbAdm_AllProducts_Search.Items.Add("Color");
        }
        #endregion AllProducts Fun

        #region AllProducts_Button Event
        private void btnAdm_AllProducts_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_AllProduct_Details.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_AllProducts_Refresh_Click(object sender, RoutedEventArgs e)
        {
            txtAdm_AllProducts_Search_Price.Text = "";
            txtAdm_AllProducts_Search.Text = "";
            cmbAdm_AllProducts_Search.Text = "Select";
            AllProducts_Details();
        }
        #endregion AllProducts_Button Event

        #region AllProduct Event
        private void smviewproducts_Click(object sender, RoutedEventArgs e)
        {
            grd_AllProduct_Details.Visibility = System.Windows.Visibility.Visible;
            AllProducts_Details();
            Load_AllProducts();
        }

        private void txtAdm_AllProducts_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllProducts_Details();
        }

        private void txtAdm_AllProducts_Search_Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllProducts_Details();
        }
        #endregion AllProduct Event

        #endregion AllProduct Function


        private void smnewprocurement_Click(object sender, RoutedEventArgs e)
        {
            GRD_NewProcurement.Visibility = System.Windows.Visibility.Visible;
            // load_DSelect();
            PREPROCUREMENTid();
            load_DSelect();
            Fetch_Pre_Domain();
            load_Insurance();
            load_Followup();
            FetchDealarname();
            SetWarrantyYM();
            
        }

        #region SaleCustomerDet Function
        public void SaleCustomer_Details()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[Customer_ID],P.[Bill_No],P.[Payment_Mode],P.[Total_Price],P.[Paid_Amount],P.[Balance_Amount],P.[C_Date] " +
                      ",C.[Name],C.[Mobile_No], C.[Email_ID] " +
                      "FROM [tlb_Bill_No] P " +
                      "INNER JOIN [tlb_Customer] C ON C.[ID]=P.[Customer_ID] " +
                      "WHERE ";

                if (txtAdm_SaleCustBillNo_Search.Text.Trim() != "")
                {
                    str = str + "P.[Bill_No] LIKE ISNULL('" + txtAdm_SaleCustBillNo_Search.Text.Trim() + "',P.[Bill_No]) + '%' AND ";
                }
                if (txtAdm_SaleCustDetails_Search.Text.Trim() != "")
                {
                    str = str + "C.[Name] LIKE ISNULL('" + txtAdm_SaleCustDetails_Search.Text.Trim() + "',C.[Name]) + '%' AND ";
                }
                if (txtAdm_SaleCustMobileNo_Search.Text.Trim() != "")
                {
                    str = str + "C.[Mobile_No] LIKE ISNULL('" + txtAdm_SaleCustMobileNo_Search.Text.Trim() + "',C.[Mobile_No]) + '%' AND ";
                }
                str = str + " P.[S_Status] = 'Active' ORDER BY P.[Bill_No] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_SaleCustomerDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void SaleCustomer_ProductDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[Customer_ID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Per_Product_Price],P.[Qty],P.[C_Price] " +
                      ",DM.[Domain_Name],PM.[Product_Name], B.[Brand_Name] , PC.[Product_Category] ,MN.[Model_No] ,C.[Color] " +
                      "FROM [tlb_InvoiceDetails] P " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "WHERE P.[Customer_ID]= '" + Convert.ToInt32(txtSaleCustID.Text) + "' AND P.[S_Status] = 'Active' ORDER BY P.[Bill_No] ASC ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_SaleCustomer_ProductDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        private void smviewsalerecords_Click(object sender, RoutedEventArgs e)
        {
            grd_SaleCustDetails.Visibility = System.Windows.Visibility.Visible;

            SaleCustomer_Details();

        }

        #region SalecustDet Button Event
        private void btnAdm_SaleCustDetails_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_SaleCustDetails.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_SaleCustDetails_Refresh_Click(object sender, RoutedEventArgs e)
        {
            txtAdm_SaleCustBillNo_Search.Text = "";
            txtAdm_SaleCustDetails_Search.Text = "";
            txtAdm_SaleCustMobileNo_Search.Text = "";
            dgvAdm_SaleCustomerDetails.ItemsSource = null;
            dgvAdm_SaleCustomer_ProductDetails.ItemsSource = null;
            SaleCustomer_Details();
        }
        #endregion SalecustDet Button Event

        private void cmbAdm_DealerFilter_Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
       
        #region SaleCustomerDetails Event 
        private void txtAdm_SaleCustBillNo_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            //txtSaleCustID.Text = "";
            //dgvAdm_SaleCustomer_ProductDetails.ItemsSource = null;
            SaleCustomer_Details();
        }

        private void txtAdm_SaleCustDetails_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            //txtSaleCustID.Text = "";

            //var grid = dgvAdm_SaleCustomerDetails;
            //if (grid.SelectedIndex >= 0)
            //{
            //    for (int i = 0; i <= grid.SelectedItems.Count; i++)
            //    {
            //        grid.Items.Remove(grid.SelectedItems[i]);
            //    };
            //}

            //if (dgvAdm_SaleCustomerDetails.SelectedItem != null)
            //{
            //    ((DataRowView)(dgvAdm_SaleCustomerDetails.SelectedItem)).Row.Delete();
            //}

            //DataGridCellInfo cell = dgvAdm_SaleCustomerDetails.SelectedCells[0];
            //dgvAdm_SaleCustomer_ProductDetails.ItemsSource = null;
            //dgvAdm_SaleCustomerDetails.SelectedItem = null;
            SaleCustomer_Details();
        }

        private void txtAdm_SaleCustMobileNo_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            //txtSaleCustID.Text = "";
            //dgvAdm_SaleCustomer_ProductDetails.ItemsSource = null;
            SaleCustomer_Details();
        }

        private void dgvAdm_SaleCustomerDetails_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            object item = dgvAdm_SaleCustomerDetails.SelectedItem;
            string ID = (dgvAdm_SaleCustomerDetails.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            //MessageBox.Show(ID);
            try
            {
                con.Open();
                string sqlquery = "SELECT [ID],[Customer_ID] " +
                      "FROM [tlb_Bill_No] " +
                      "WHERE [Bill_No]='" + ID + "' ";

                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtSaleCustID.Text = dt.Rows[0]["Customer_ID"].ToString();
                }

                //grd_FinalizeProducts.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            SaleCustomer_ProductDetails();
        }
        #endregion SaleCustomerDetails Event
        #endregion SaleCustomerDet Function
      
        
        //----------add product
        #region AddProduct Function
        #region AddPro Fun
        public bool AddProduct_Validation()
        {
            bool result = false;
            if(cmbP_domain.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Domain", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbP_Product.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Product", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbP_Brand.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Brand", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbP_PCategory.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Product Categeory", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbP_ModelNo.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Model No", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbP_Color.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Color", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (txtP_Price.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Product Price", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return result;
        }
        
        public bool Domain_Validation()
        {
            bool result = false;
            if(txtdomain.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Domain Name", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return result;
        }

        public bool Product_Validation()
        {
            bool result = false;
            if(cmb_DomainProduct.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Domain", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (txtProductName.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Ptoduct Type", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return result;
        }

        public bool Brand_Validation()
        {
            bool result = false;
            if (cmbDomainBrand.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Domain", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbProductBrand.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Ptoduct Type", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (txtBrand.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Brand Name", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return result;
        }

        public bool ProductCategory_Validation()
        {
            bool result = false;
            if (cmbDomainPCategory.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Domain", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbProductPCategoryy.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Ptoduct Type", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbBrandPCategory.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Brand", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (txtPCategoy.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Product Category", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return result;
        }

        public bool ModelNo_Validation()
        {
            bool result = false;
            if (cmbDomainModelno.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Domain", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbProductModelno.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Ptoduct Type", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbBrandModelno.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Brand", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbPCategoryModelno.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Product Category", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (txtmodelno.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Model No", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return result;
        }

        public bool Color_Validation()
        {
            bool result = false;
            if (cmbDomainColor.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Domain", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbProductColor.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Ptoduct Type", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbBrandColor.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Brand", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbPCategoryColor.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Product Category", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if(cmbModelColor.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Product Model No", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (txtcolor.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Product Color", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return result;
        }

        public void Fetch_Product()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Domain_ID,Product_Name from tlb_Products where  Domain_ID='" + cmbP_domain.SelectedValue.GetHashCode() + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_Product.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbP_Product.ItemsSource = ds.Tables[0].DefaultView;
                    cmbP_Product.DisplayMemberPath = ds.Tables[0].Columns["Product_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }

        }

        public void fetch_Brand()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Brand_Name from tlb_Brand where Product_ID='" + cmbP_Product.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_Brand.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbP_Brand.ItemsSource = ds.Tables[0].DefaultView;
                    cmbP_Brand.DisplayMemberPath = ds.Tables[0].Columns["Brand_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }

        }

        public void Fetch_PC()
        {

            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT  ID,Product_Category from tlb_P_Category where Brand_ID='" + cmbP_Brand.SelectedValue.GetHashCode() + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_PCategory.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbP_PCategory.ItemsSource = ds.Tables[0].DefaultView;
                    cmbP_PCategory.DisplayMemberPath = ds.Tables[0].Columns["Product_Category"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void fetch_Model()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Model_No from tlb_Model where P_Category='" + cmbP_PCategory.SelectedValue.GetHashCode() + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_ModelNo.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbP_ModelNo.ItemsSource = ds.Tables[0].DefaultView;
                    cmbP_ModelNo.DisplayMemberPath = ds.Tables[0].Columns["Model_No"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void fetch_Color()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Color from tlb_Color where Model_No_ID='" + cmbP_ModelNo.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_Color.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbP_Color.ItemsSource = ds.Tables[0].DefaultView;
                    cmbP_Color.DisplayMemberPath = ds.Tables[0].Columns["Color"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_DomainP()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Domain_Name from tb_Domain ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmb_DomainProduct.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmb_DomainProduct.ItemsSource = ds.Tables[0].DefaultView;
                    cmb_DomainProduct.DisplayMemberPath = ds.Tables[0].Columns["Domain_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }

        }

        public void Load_DomainB()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Domain_Name from tb_Domain ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbDomainBrand.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbDomainBrand.ItemsSource = ds.Tables[0].DefaultView;
                    cmbDomainBrand.DisplayMemberPath = ds.Tables[0].Columns["Domain_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }

        }

        public void Load_PCDomain()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Domain_Name from tb_Domain ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbDomainPCategory.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbDomainPCategory.ItemsSource = ds.Tables[0].DefaultView;
                    cmbDomainPCategory.DisplayMemberPath = ds.Tables[0].Columns["Domain_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }

        }

        public void Load_MDomain()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Domain_Name from tb_Domain ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbDomainModelno.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbDomainModelno.ItemsSource = ds.Tables[0].DefaultView;
                    cmbDomainModelno.DisplayMemberPath = ds.Tables[0].Columns["Domain_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_CDomain()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Domain_Name from tb_Domain ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbDomainColor.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbDomainColor.ItemsSource = ds.Tables[0].DefaultView;
                    cmbDomainColor.DisplayMemberPath = ds.Tables[0].Columns["Domain_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_Domain()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Domain_Name from tb_Domain ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_domain.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbP_domain.ItemsSource = ds.Tables[0].DefaultView;
                    cmbP_domain.DisplayMemberPath = ds.Tables[0].Columns["Domain_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }

        }

        public void clearAllADDProducts()
        {
            cmbP_domain.SelectedValue = null;
            cmbP_Product.SelectedValue = null;
            cmbP_Brand.SelectedValue = null;
            cmbP_PCategory.SelectedValue = null;
            cmbP_ModelNo.SelectedValue = null;
            cmbP_Color.SelectedValue = null;
            Load_Domain();

        }

        public void Load_BrandProduct()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Domain_ID, Product_Name from tlb_Products where Domain_ID ='" + cmbDomainBrand.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbProductBrand.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbProductBrand.ItemsSource = ds.Tables[0].DefaultView;
                    cmbProductBrand.DisplayMemberPath = ds.Tables[0].Columns["Product_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }

        }

        public void Load_PCProduct()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Domain_ID, Product_Name from tlb_Products where Domain_ID='" + cmbDomainPCategory.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbProductPCategoryy.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbProductPCategoryy.ItemsSource = ds.Tables[0].DefaultView;
                    cmbProductPCategoryy.DisplayMemberPath = ds.Tables[0].Columns["Product_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_PCBrand()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Product_ID, Brand_Name from tlb_Brand where Product_ID='" + cmbProductPCategoryy.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbBrandPCategory.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbBrandPCategory.ItemsSource = ds.Tables[0].DefaultView;
                    cmbBrandPCategory.DisplayMemberPath = ds.Tables[0].Columns["Brand_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_MProduct()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Domain_ID, Product_Name from tlb_Products where Domain_ID='" + cmbDomainModelno.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbProductModelno.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbProductModelno.ItemsSource = ds.Tables[0].DefaultView;
                    cmbProductModelno.DisplayMemberPath = ds.Tables[0].Columns["Product_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_MBrand()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Product_ID,Brand_Name from tlb_Brand where Product_ID='" + cmbProductModelno.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbBrandModelno.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbBrandModelno.ItemsSource = ds.Tables[0].DefaultView;
                    cmbBrandModelno.DisplayMemberPath = ds.Tables[0].Columns["Brand_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_MPC()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Brand_ID, Product_Category from tlb_P_Category where Brand_ID='" + cmbBrandModelno.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPCategoryModelno.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbPCategoryModelno.ItemsSource = ds.Tables[0].DefaultView;
                    cmbPCategoryModelno.DisplayMemberPath = ds.Tables[0].Columns["Product_Category"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_CProduct()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Domain_ID, Product_Name from tlb_Products where Domain_ID='" + cmbDomainColor.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbProductColor.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbProductColor.ItemsSource = ds.Tables[0].DefaultView;
                    cmbProductColor.DisplayMemberPath = ds.Tables[0].Columns["Product_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_CBrand()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Product_ID, Brand_Name from tlb_Brand where Product_ID='" + cmbProductColor.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbBrandColor.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbBrandColor.ItemsSource = ds.Tables[0].DefaultView;
                    cmbBrandColor.DisplayMemberPath = ds.Tables[0].Columns["Brand_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_CPC()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Brand_ID, Product_Category from tlb_P_Category where Brand_ID='" + cmbBrandColor.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPCategoryColor.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbPCategoryColor.ItemsSource = ds.Tables[0].DefaultView;
                    cmbPCategoryColor.DisplayMemberPath = ds.Tables[0].Columns["Product_Category"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void Load_CModel()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,P_Category, Model_No from tlb_Model where P_Category='" + cmbPCategoryColor.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbModelColor.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbModelColor.ItemsSource = ds.Tables[0].DefaultView;
                    cmbModelColor.DisplayMemberPath = ds.Tables[0].Columns["Model_No"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }
        #endregion AddPro Fun

        #region AddProduct Event
        private void cmbP_domain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbP_Product.SelectedValue = null;
            cmbP_Brand.SelectedValue = null;
            cmbP_PCategory.SelectedValue = null;
            cmbP_ModelNo.SelectedValue = null;
            cmbP_Color.SelectedValue = null;
            Fetch_Product();
        }

        private void cmbP_Product_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbP_Brand.SelectedValue = null;
            cmbP_PCategory.SelectedValue = null;
            cmbP_ModelNo.SelectedValue = null;
            cmbP_Color.SelectedValue = null;
            fetch_Brand();
        }

        private void cmbP_Brand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbP_PCategory.SelectedValue = null;
            cmbP_ModelNo.SelectedValue = null;
            cmbP_Color.SelectedValue = null;
            Fetch_PC();
        }

        private void cmbP_PCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbP_ModelNo.SelectedValue = null;
            cmbP_Color.SelectedValue = null;
            fetch_Model();
        }

        private void cmbP_ModelNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbP_Color.SelectedValue = null;
            fetch_Color();
        }
        #endregion AddProduct Event

        #region AddPro Button Event
        private void btnP_AddDomain_Click(object sender, RoutedEventArgs e)
        {
            grd_Domain.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnP_AddProduct_Click_1(object sender, RoutedEventArgs e)
        {
            grd_Product.Visibility = Visibility;
            Load_DomainP();
        }

        private void btnP_AddBrand_Click(object sender, RoutedEventArgs e)
        {
            grd_Brand.Visibility = Visibility;
            Load_DomainB();
        }

        private void btnP_AddPCategory_Click(object sender, RoutedEventArgs e)
        {
            grd_ProductCategory.Visibility = Visibility;
            Load_PCDomain();
        }

        private void btnP_AddModelNo_Click(object sender, RoutedEventArgs e)
        {
            grd_ModelNo.Visibility = Visibility;
            Load_MDomain();
        }

        private void btnP_AddColor1_Click_1(object sender, RoutedEventArgs e)
        {
            grd_Color.Visibility = Visibility;
            Load_CDomain();
        }

        private void btnAdm_AddProductExit_Click(object sender, RoutedEventArgs e)
        {
            grd_U_AddProducts.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnP_Save_Click(object sender, RoutedEventArgs e)
        {
            if (AddProduct_Validation() == true)
                return;
            
            try
            {

                baddprd.Flag = 1;
                baddprd.Domain_ID = Convert.ToInt32(cmbP_domain.SelectedValue.GetHashCode());
                baddprd.Product_ID = Convert.ToInt32(cmbP_Product.SelectedValue.GetHashCode());
                baddprd.Brand_ID = Convert.ToInt32(cmbP_Brand.SelectedValue.GetHashCode());
                baddprd.P_Category = Convert.ToInt32(cmbP_PCategory.SelectedValue.GetHashCode());
                baddprd.Model_No_ID = Convert.ToInt32(cmbP_ModelNo.SelectedValue.GetHashCode());
                baddprd.Color_ID = Convert.ToInt32(cmbP_Color.SelectedValue.GetHashCode());
                baddprd.Narration = txtP_Narration.Text;
                baddprd.Price = Convert.ToDouble(txtP_Price.Text);
                baddprd.S_Status = "Active";
                baddprd.C_Date = System.DateTime.Now.ToShortDateString();
                dalprd.Save_Insert_Update_Delete(baddprd);
                MessageBox.Show("Data Save Successfully");
                txtP_Narration.Text = "";
                txtP_Price.Text = "";
                clearAllADDProducts();
                // Load_Domain();

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

        private void btnP_Clear_Click(object sender, RoutedEventArgs e)
        {
            clearAllADDProducts();
        }

        private void btnP_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_U_AddProducts.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion AddPro Button Event

        private void grd_U_AddProducts_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Domain();
        }

        #region Domain Button Event
        private void btndomainsave_Click(object sender, RoutedEventArgs e)
        {
            if (Domain_Validation() == true)
                return;

            try
            {
                string strpan, stradhar, strpass, straddress, strseventw, strfrm16, strdelerlic, strnoidpf, strnodoc, strcmpid;
                baddprd.Flag = 1;
                baddprd.Domain_Name = txtdomain.Text;
                if (chkpancard.IsChecked == true)
                {
                    strpan = "Yes";
                }
                else
                {
                    strpan = "No";
                }
                if (chkadharcard.IsChecked == true)
                {
                    stradhar = "Yes";
                }
                else
                {
                    stradhar = "No";
                }
                if (chkPassport.IsChecked == true)
                {
                    strpass = "Yes";
                }
                else
                {
                    strpass = "No";
                }
                if (chkaddress.IsChecked == true)
                {
                    straddress = "Yes";
                }
                else
                {
                    straddress = "No";
                }
                if (chkseventwelve.IsChecked == true)
                {
                    strseventw = "Yes";
                }
                else
                {
                    strseventw = "No";
                }
                if (chkform16.IsChecked == true)
                {
                    strfrm16 = "Yes";
                }
                else
                {
                    strfrm16 = "No";
                }
                if (chkdealerlisence.IsChecked == true)
                {
                    strdelerlic = "Yes";
                }
                else
                {
                    strdelerlic = "No";
                }
                if (chkotherid.IsChecked == true)
                {
                    strnoidpf = "Yes";
                }
                else
                {
                    strnoidpf = "No";
                }
                if (chknodocument.IsChecked == true)
                {
                    strnodoc = "Yes";
                }
                else { strnodoc = "No"; }
                if (chkcidproof.IsChecked == true)
                {
                    strcmpid = "Yes";
                }
                else
                {
                    strcmpid = "No";
                }
                baddprd.PAN_Card = strpan;
                baddprd.Adhar_Card = stradhar;
                baddprd.Passport = strpass;
                baddprd.Address_Proof = straddress;
                baddprd.Seven_Twevel = strseventw;
                baddprd.Form_16 = strfrm16;
                baddprd.Dealer_Lisence = strdelerlic;
                baddprd.Other_ID_Proof = strnoidpf;
                baddprd.No_Documents = strnodoc;
                baddprd.Cmp_ID_Proof = strcmpid;
                baddprd.S_Status = "Active";

                baddprd.C_Date = System.DateTime.Now.ToShortDateString();
                dalprd.AddDomain_Insert_Update_Delete(baddprd);
                MessageBox.Show("Data Save Successfully");
                txtdomain.Text = "";
                Load_Domain();
            }
            catch (Exception)
            {

                throw;
            }          
        }

        private void btndomainexit_Click(object sender, RoutedEventArgs e)
        {
            grd_Domain.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion Domain Button Event

        #region Product Button Event
        private void btnProductSave_Click(object sender, RoutedEventArgs e)
        {
            if (Product_Validation() == true)
                return;

            try
            {
                baddprd.Flag = 1;
                baddprd.Domain_ID = Convert.ToInt32(cmb_DomainProduct.SelectedValue.GetHashCode());
                baddprd.Product_Name = txtProductName.Text;
                baddprd.S_Status = "Active";


                baddprd.C_Date = System.DateTime.Now.ToShortDateString();
                dalprd.AddProducts_Insert_Update_Delete(baddprd);
                MessageBox.Show("Data Save Successfully");
                txtProductName.Text = "";
                Load_DomainP();
                //Fetch_Product();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnProduct_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_Product.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion Product Button Event

        #region Brand Button Event
        private void btnBrandSave_Click(object sender, RoutedEventArgs e)
        {
            if (Brand_Validation() == true)
                return;

            try
            {
                
                baddprd.Flag = 1;
                baddprd.Product_ID = Convert.ToInt32(cmbProductBrand.SelectedValue.GetHashCode());
                baddprd.Brand_Name = txtBrand.Text;
                baddprd.S_Status = "Active";
                baddprd.C_Date =System.DateTime.Now.ToShortDateString();
                dalprd.AddBrand_Insert_Update_Delete(baddprd);
                MessageBox.Show("Data Save Successfully");
                txtBrand.Text = "";
                cmbProductBrand.SelectedValue = null;
                Load_Domain();
                // fetch_Brand();
                // Load_DomainB();
                // Load_BrandProduct();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnBrandExit_Click(object sender, RoutedEventArgs e)
        {
            grd_Brand.Visibility = System.Windows.Visibility.Hidden;
        }

        private void cmbDomainBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbProductBrand.SelectedValue = null;
            Load_BrandProduct();
        }
        #endregion Brand Button Event

        #region ProductCategory Button Event
        private void cmbDomainPCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbBrandPCategory.SelectedValue = null;
            cmbProductPCategoryy.SelectedValue = null;
            Load_PCProduct();
        }

        private void cmbProductPCategoryy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load_PCBrand();
        }

        private void btnPCategorySave_Click(object sender, RoutedEventArgs e)
        {
            if (ProductCategory_Validation() == true)
                return;

            try
            {

                baddprd.Flag = 1;
                baddprd.Brand_ID = Convert.ToInt32(cmbBrandPCategory.SelectedValue.GetHashCode());
                baddprd.Product_Category = txtPCategoy.Text;
                baddprd.S_Status = "Active";
                baddprd.C_Date = System.DateTime.Now.ToShortDateString();
                dalprd.AddP_Category_Insert_Update_Delete(baddprd);
                MessageBox.Show("Data Save Successfully");
                txtPCategoy.Text = "";
                cmbBrandPCategory.SelectedValue = null;
                cmbProductPCategoryy.SelectedValue = null;
                cmbDomainPCategory.SelectedValue = null;
                Load_Domain();
                //  Load_PCDomain();
                // Fetch_PC();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnPCategoryExit_Click(object sender, RoutedEventArgs e)
        {
            grd_ProductCategory.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion ProductCategory Button Event

        #region ModelNo Button Event
        private void cmbDomainModelno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbProductModelno.SelectedValue = null;
            cmbBrandModelno.SelectedValue = null;
            cmbPCategoryModelno.SelectedValue = null;
            Load_MProduct();
        }

        private void cmbProductModelno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbBrandModelno.SelectedValue = null;
            cmbPCategoryModelno.SelectedValue = null;
            Load_MBrand();
        }

        private void cmbBrandModelno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbPCategoryModelno.SelectedValue = null;
            Load_MPC();
        }

        private void btnModelNoSave_Click(object sender, RoutedEventArgs e)
        {
            if (ModelNo_Validation() == true)
                return;

            try
            {

                baddprd.Flag = 1;
                baddprd.P_Category = Convert.ToInt32(cmbPCategoryModelno.SelectedValue.GetHashCode());
                baddprd.Model_No = txtmodelno.Text;
                baddprd.S_Status = "Active";
                baddprd.C_Date = System.DateTime.Now.ToShortDateString();
                dalprd.AddModel_Insert_Update_Delete(baddprd);
                MessageBox.Show("Data Save Successfully");
                txtmodelno.Text = "";
                cmbDomainModelno.SelectedValue = null;
                cmbProductModelno.SelectedValue = null;
                cmbBrandModelno.SelectedValue = null;
                cmbPCategoryModelno.SelectedValue = null;
                Load_Domain();

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnmodelnoexie_Click(object sender, RoutedEventArgs e)
        {
            grd_ModelNo.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion ModelNo Button Event

        #region Colour Button Event
        private void cmbDomainColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbProductColor.SelectedValue = null;
            cmbBrandColor.SelectedValue = null;
            cmbPCategoryColor.SelectedValue = null;
            cmbModelColor.SelectedValue = null;
            Load_CProduct();
        }

        private void cmbProductColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cmbProductColor.SelectedValue = null;
            cmbBrandColor.SelectedValue = null;
            cmbPCategoryColor.SelectedValue = null;
            cmbModelColor.SelectedValue = null;

            Load_CBrand();
        }

        private void cmbBrandColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbPCategoryColor.SelectedValue = null;
            cmbModelColor.SelectedValue = null;
            Load_CPC();
        }

        private void cmbPCategoryColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbModelColor.SelectedValue = null;
            Load_CModel();
        }

        private void btnColorSave_Click(object sender, RoutedEventArgs e)
        {
            if (Color_Validation() == true)
                return;

            try
            {

                baddprd.Flag = 1;
                baddprd.Model_No_ID = Convert.ToInt32(cmbModelColor.SelectedValue.GetHashCode());
                baddprd.Color = txtcolor.Text;
                baddprd.S_Status = "Active";
                baddprd.C_Date = System.DateTime.Now.ToShortDateString();
                dalprd.AddColor_Insert_Update_Delete(baddprd);
                MessageBox.Show("Data Save Successfully");
                txtcolor.Text = "";
                cmbDomainColor.SelectedValue = null;
                cmbProductColor.SelectedValue = null;
                cmbBrandColor.SelectedValue = null;
                cmbPCategoryColor.SelectedValue = null;
                cmbModelColor.SelectedValue = null;

                Load_Domain();
                // fetch_Color();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnColorExit_Click(object sender, RoutedEventArgs e)
        {
            grd_Color.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion Colour Button Event
        #endregion AddProduct Function

        //------------Pre procurment
        #region PreProcurment Function
        #region PrePro Fun
        public bool PrePro_Validation()
        {
            bool result = false;
            if(cmbPre_Pro_Salename.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Dealer Name", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbPreDomain.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Domain Name", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbPreProduct.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Product Name", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbPreBrand.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Brand", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbPrePCategory.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Product Category", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbPreModel.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Model No", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmd_PreColor.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Color", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (txtPrice4.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Price", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (txtQuantity4.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Quantity", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbPreFollowup.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Followup", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (cmbPreInsurance.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Insurance", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return result;
        }

        public void load_DSelect()
        {
            cmbPreDomain.Text = "--Select--";
            cmbPreProduct.Text = "--Select--";
            cmbPreBrand.Text = "--Select--";
            cmbPrePCategory.Text = "--Select--";
            cmbPreModel.Text = "--Select--";
            cmd_PreColor.Text = "--Select--";
        }

        public void Fetch_Pre_Domain()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Domain_Name from tb_Domain ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    // cmbPreDomain.Text = "--Select--";
                    cmbPreDomain.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbPreDomain.ItemsSource = ds.Tables[0].DefaultView;
                    cmbPreDomain.DisplayMemberPath = ds.Tables[0].Columns["Domain_Name"].ToString();
                    // cmbPreDomain.Items.Insert(0, "--Select--");
                    // cmbPreDomain.Items.Insert(0, new ListItem("--Select--", "0"));
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }

        }

        public void load_Insurance()
        {
            cmbPreInsurance.Text = "--Select--";
            cmbPreInsurance.Items.Add("Yes");
            cmbPreInsurance.Items.Add("No");

        }

        public void load_Followup()
        {
            cmbPreFollowup.Text = "--Select--";
            cmbPreFollowup.Items.Add("Default");
            cmbPreFollowup.Items.Add("Custom");

        }

        public void FetchDealarname()
        {
            try
            {
                con.Open();
                String str2 = "Select ID, [DealerFirstName]+' '+[DealerLastName] as [DealerName] from tbl_DealerEntry  where  S_Status='Active' ";
                cmd = new SqlCommand(str2, con);
                DataSet ds = new DataSet();
                // dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {


                    cmbPre_Pro_Salename.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbPre_Pro_Salename.ItemsSource = ds.Tables[0].DefaultView;
                    //string a = ds.Tables[0].Columns["DealerFirstName"].ToString();
                    //string b = ds.Tables[0].Columns["DealerLastName"].ToString();
                    cmbPre_Pro_Salename.DisplayMemberPath = ds.Tables[0].Columns["DealerName"].ToString();

                }

            }
            catch { throw; }
            finally { con.Close(); }
        }

        public void SetWarrantyYM()
        {
            cmbPreWarrantyYM.Text = "---Select---";
            cmbPreWarrantyYM.Items.Add("Month");
            cmbPreWarrantyYM.Items.Add("Year");
        }

        public void fetch_Documents()
        {

            try
            {
                con.Open();

                cmd = new SqlCommand("Select PAN_Card,Adhar_Card,Passport,Address_Proof,Seven_Twevel,Form_16,Dealer_Lisence,Other_ID_Proof,No_Documents,Cmp_ID_Proof  from tb_Domain where ID='" + cmbPreDomain.SelectedValue.GetHashCode() + "' ", con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string p = dr["PAN_Card"].ToString();
                    string ad = dr["Adhar_Card"].ToString();
                    string pa = dr["Passport"].ToString();
                    string addr = dr["Address_Proof"].ToString();
                    string st = dr["Seven_Twevel"].ToString();
                    string frm = dr["Form_16"].ToString();
                    string dl = dr["Dealer_Lisence"].ToString();
                    string oidp = dr["Other_ID_Proof"].ToString();
                    string nod = dr["No_Documents"].ToString();
                    string cmpid = dr["Cmp_ID_Proof"].ToString();
                    if (p == "Yes")
                    {
                        chkPANCARD.IsEnabled = true;
                        //chkPANCARD.IsChecked = true;
                    }
                    if (pa == "Yes")
                    {
                        chkPASSPORT.IsEnabled = true;
                    }
                    if (ad == "Yes")
                    {
                        CHKADHARC.IsEnabled = true;
                        //chkPANCARD.IsChecked = true;
                    }
                    if (addr == "Yes")
                    {
                        chkaddressproof.IsEnabled = true;
                    }
                    if (st == "Yes")
                    {
                        chk7_12.IsEnabled = true;
                    }
                    if (frm == "Yes")
                    {
                        chkform_16.IsEnabled = true;
                    }
                    if (dl == "Yes")
                    {
                        chkDEALERL.IsEnabled = true;
                    }
                    if (oidp == "Yes")
                    {
                        chkOTHERID.IsEnabled = true;
                    }
                    if (nod == "Yes")
                    {
                        chkNODOCS.IsEnabled = true;
                    }
                    if (cmpid == "Yes")
                    {
                        chkcmpid.IsEnabled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }


        }

        public void Fetch_Pre_Product()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Product_Name from tlb_Products where Domain_ID='" + cmbPreDomain.SelectedValue.GetHashCode() + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPreProduct.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbPreProduct.ItemsSource = ds.Tables[0].DefaultView;
                    cmbPreProduct.DisplayMemberPath = ds.Tables[0].Columns["Product_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }

        }

        public void fetch_Pre_Brand()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Brand_Name from tlb_Brand where Product_ID='" + cmbPreProduct.SelectedValue.GetHashCode() + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPreBrand.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbPreBrand.ItemsSource = ds.Tables[0].DefaultView;
                    cmbPreBrand.DisplayMemberPath = ds.Tables[0].Columns["Brand_Name"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }

        }

        public void Fetch_Pre_PC()
        {

            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT  ID,Product_Category from tlb_P_Category where Brand_ID='" + cmbPreBrand.SelectedValue.GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPrePCategory.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbPrePCategory.ItemsSource = ds.Tables[0].DefaultView;
                    cmbPrePCategory.DisplayMemberPath = ds.Tables[0].Columns["Product_Category"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void fetch_Pre_Model()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Model_No from tlb_Model where P_Category='" + cmbPrePCategory.SelectedValue.GetHashCode() + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPreModel.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbPreModel.ItemsSource = ds.Tables[0].DefaultView;
                    cmbPreModel.DisplayMemberPath = ds.Tables[0].Columns["Model_No"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void fetch_Pre_Color()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Color from tlb_Color where Model_No_ID='" + cmbPreModel.SelectedValue.GetHashCode() + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmd_PreColor.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmd_PreColor.ItemsSource = ds.Tables[0].DefaultView;
                    cmd_PreColor.DisplayMemberPath = ds.Tables[0].Columns["Color"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                con.Close();
            }
        }

        public void clearallPreProcurement()
        {
            cmbPreDomain.SelectedValue = null;
            cmbPreProduct.SelectedValue = null;
            cmbPrePCategory.SelectedValue = null;
            cmbPreBrand.SelectedValue = null;
            cmbPreModel.SelectedValue = null;
            cmd_PreColor.SelectedValue = null;
            //txtprephone.Text = "";
            txtPreFerbcost.Text = "";
            txtnarration.Text = "";
            //chkidproof.IsChecked = false;
            //chkNodoc.IsChecked = false;
            // chkAddress__Proof.IsChecked = false;
            // chketc.IsChecked = false;
            // chkForm16.IsChecked = false;
            chkNODOCS.IsEnabled = false;
            chkPANCARD.IsEnabled = false;
            chkPASSPORT.IsEnabled = false;
            CHKADHARC.IsEnabled = false;
            chkOTHERID.IsEnabled = false;
            chkform_16.IsEnabled = false;
            chkDEALERL.IsEnabled = false;
            chkaddressproof.IsEnabled = false;
            chk7_12.IsEnabled = false;
            chkNODOCS.IsEnabled = false;
            chk7_12.IsChecked = false;

            cmbPreInsurance.Items.Clear();
            cmbPreFollowup.Items.Clear();
            load_Insurance();
            load_Followup();
            txtPrice.Text = "";
            chkcmpid.IsEnabled = false;
            txtPreWarranty.Text = "";
        }

        public void PREPROCUREMENTid()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from Pre_Procurement", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            lblPro_no.Content = "# Pre_Proc/" + id1.ToString();
            con.Close();


        }
        #endregion PrePro Fun

        #region PrePro Event
        private void cmbPreDomain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // fetcdoc = cmbPreDomain.SelectedValue.GetHashCode();
            cmbPreProduct.SelectedValue = null;
            cmbPrePCategory.SelectedValue = null;
            cmbPreBrand.SelectedValue = null;
            cmbPreModel.SelectedValue = null;
            cmd_PreColor.SelectedValue = null;
            fetch_Documents();
            Fetch_Pre_Product();
        }

        private void cmbPreProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbPrePCategory.SelectedValue = null;
            cmbPreBrand.SelectedValue = null;
            cmbPreModel.SelectedValue = null;
            cmd_PreColor.SelectedValue = null;

            fetch_Pre_Brand();
        }

        private void cmbPreBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbPrePCategory.SelectedValue = null;
            cmbPreModel.SelectedValue = null;
            cmd_PreColor.SelectedValue = null;
            Fetch_Pre_PC();
        }

        private void cmbPrePCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cmbPreBrand.SelectedValue = null;
            cmbPreModel.SelectedValue = null;
            cmd_PreColor.SelectedValue = null;
            fetch_Pre_Model();
        }

        private void cmbPreModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmd_PreColor.SelectedValue = null;
            fetch_Pre_Color();
        }

        private void cmd_PreColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            con.Open();

            cmd = new SqlCommand("Select  Price from Pre_Products where Color_ID='" + cmd_PreColor.SelectedValue.GetHashCode() + "' ", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtPrice4.Text = dr["Price"].ToString();
            }
            con.Close();
        }
        #endregion PrePro Event

        #region PrePro Button Event
        private void btnPro_Save_Click(object sender, RoutedEventArgs e)
        {
            if (PrePro_Validation() == true)
                return;

            try
            {

                bpreproc.Flag = 1;
                bpreproc.DealerID = cmbPre_Pro_Salename.SelectedValue.GetHashCode(); //txtsalername.Text;

                //bpreproc.Phone_Id = txtprephone .Text ;
                bpreproc.Domain_ID = Convert.ToInt32(cmbPreDomain.SelectedValue.GetHashCode());
                bpreproc.Product_ID = Convert.ToInt32(cmbPreProduct.SelectedValue.GetHashCode());
                bpreproc.Brand_ID = Convert.ToInt32(cmbPreBrand.SelectedValue.GetHashCode());
                bpreproc.P_Category = Convert.ToInt32(cmbPrePCategory.SelectedValue.GetHashCode());
                bpreproc.Model_No_ID = Convert.ToInt32(cmbPreModel.SelectedValue.GetHashCode());
                bpreproc.Color_ID = Convert.ToInt32(cmd_PreColor.SelectedValue.GetHashCode());

                bpreproc.Procurment_Price = Convert.ToDouble(txtPrice4.Text);
                bpreproc.Quantity = Convert.ToDouble(txtQuantity4.Text);
                bpreproc.Total_Amount = Convert.ToDouble(txtTotalPrice4.Text);
                bpreproc.Net_Amount = Convert.ToDouble(txtNetAmount4.Text);
                bpreproc.Round_Off = Convert.ToDouble(txtpreroundoff4.Text);
                //    for (int i = 0; i < 5;i++ )
                //    { 
                //        if (chkidproof.IsChecked == true)
                //        {
                //            maincked = "ID Proof";
                //        }

                //    if(chkaddressproof  .IsChecked ==true )
                //    {
                //        maincked = "Address Proof";
                //    }
                //        string concate += ","+item maincked;
                //}
                string checkList = string.Join(",", checkedStuff.ToArray());
                if (checkList == null)
                { bpreproc.Reg_Document = "No"; }
                else if (checkList != null)
                {
                    bpreproc.Reg_Document = checkList;
                }

                bpreproc.Have_Insurance = cmbPreInsurance.SelectedValue.ToString();
                string a = (txtPreWarranty.Text) + "" + (cmbPreWarrantyYM.SelectedItem.ToString());
                bpreproc.Warranty = a;
                bpreproc.re_ferb_cost = Convert.ToDouble(txtPreFerbcost.Text);
                bpreproc.Follow_up = cmbPreFollowup.SelectedValue.ToString();
                bpreproc.Narration = txtnarration.Text;
                bpreproc.S_Status = "Active";
                bpreproc.C_Date =System.DateTime.Now.ToShortDateString();
                dpreproc.Pre_Procurement_Save_Insert_Update_Delete(bpreproc);
                MessageBox.Show("Data Save Successfully", caption);
                txtP_Narration.Text = txtnarration.Text;
                txtP_Price.Text = "";
                clearallPreProcurement();
                PREPROCUREMENTid();
                Fetch_Pre_Domain();


                //baddprd.Flag = 1;
                //baddprd.Domain_Name = cmbP_domain.SelectedValue.ToString ();
                //baddprd.Product_Name = cmbP_Product.SelectedValue.ToString();
                //baddprd.Brand_Name = cmbP_Brand.SelectedValue.ToString();
                //baddprd.Product_Category = cmbP_PCategory.SelectedValue.ToString();
                //baddprd.Model_No = cmbP_ModelNo.SelectedValue.ToString();
                //baddprd.Color = cmbP_Color.SelectedValue.ToString();
                //baddprd.Narration = txtP_Narration.Text;
                //baddprd.Price = Convert.ToDouble(txtP_Price.Text);
                //baddprd.S_Status = "Active";
                //baddprd.C_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                //dalprd.Save_Insert_Update_Delete(baddprd);
                //MessageBox.Show("Data Save Successfully");
                //txtP_Narration.Text = "";
                //txtP_Price.Text = "";
                // Load_Domain();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnPro_Clear_Click(object sender, RoutedEventArgs e)
        {
            clearallPreProcurement();
        }

        private void btnPro_Exit_Click(object sender, RoutedEventArgs e)
        {
            GRD_NewProcurement.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cbox = sender as CheckBox;
            string s = cbox.Content as string;

            if ((bool)cbox.IsChecked)
                checkedStuff.Add(s);
            else
                checkedStuff.Remove(s);
        }
        #endregion PrePro Button Event

        #endregion PreProcurment Function

        #region ChartFunction
        #region ChartFollowUp
        int folCount;
        int sealsCount;
        int finalPro;
        int baseCust;
        int highProduct;
        int highSingleProduct;
        int highSourceNPR;
        int abc;
        int checkhighSinglePro;
        int CChighProduct;
        

        public void Chart_Followup()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                str = "SELECT Count(ID) FROM [tlb_FollowUp] WHERE [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                
                folCount = Convert.ToInt32(cmd.ExecuteScalar());
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //dgvInsurance_Details.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void Chart_Seals()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                str = "SELECT Count(ID) FROM [tlb_InvoiceDetails] WHERE [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                sealsCount = Convert.ToInt32(cmd.ExecuteScalar());
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //dgvInsurance_Details.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void Chart_Procurment()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                str = "SELECT Count(ID) FROM [Final_DealerDetails] WHERE [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                finalPro = Convert.ToInt32(cmd.ExecuteScalar());
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //dgvInsurance_Details.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void Chart_CustomerBase()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                str = "SELECT Count(ID) FROM [tlb_Customer] WHERE [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                baseCust = Convert.ToInt32(cmd.ExecuteScalar());
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //dgvInsurance_Details.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void Chart_Check_HighestProduct()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                str = "SELECT Count(Brand_ID) FROM [tlb_InvoiceDetails] WHERE [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                CChighProduct = Convert.ToInt32(cmd.ExecuteScalar());
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //dgvInsurance_Details.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void Chart_HighestProduct()
        {
            try
            {
                Chart_Check_HighestProduct();
                if(CChighProduct > 0)
                {
                    String str;
                    con.Open();
                    DataSet ds = new DataSet();
                    str = "SELECT MAX(Brand_ID) FROM [tlb_InvoiceDetails] WHERE [S_Status]='Active'";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    highProduct = Convert.ToInt32(cmd.ExecuteScalar());
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //dgvInsurance_Details.ItemsSource = ds.Tables[0].DefaultView;
                    //}
                }
                else
                {
                    highProduct = 0;
                }
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

        public void Chart_Check_HighestSingleProduct()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                str = "SELECT Count(Model_No_ID) FROM [tlb_InvoiceDetails] WHERE [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                checkhighSinglePro = Convert.ToInt32(cmd.ExecuteScalar());
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //dgvInsurance_Details.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void Chart_HighestSingleProduct()
        {
            try
            {
                Chart_Check_HighestSingleProduct();
                if(checkhighSinglePro > 0)
                {
                    String str;
                    con.Open();
                    DataSet ds = new DataSet();
                    str = "SELECT MAX(Model_No_ID) FROM [tlb_InvoiceDetails] WHERE [S_Status]='Active'";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    highSingleProduct = Convert.ToInt32(cmd.ExecuteScalar());
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //dgvInsurance_Details.ItemsSource = ds.Tables[0].DefaultView;
                    //}
                }
                else
                {
                    highSingleProduct = 0;
                }
                
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

        public void Chart_Check_BestEnquerySource()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                //str = "SELECT distinct Count(I.C_Date) AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
                str = "SELECT Count(SourceEnqID) AS [SourceEnqID] FROM [tlb_Customer] WHERE [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                abc = Convert.ToInt32(cmd.ExecuteScalar());
                //abc = Convert.ToInt32(highSourceNPR);

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

        public void Chart_BestEnquerySource()
        {
            try
            {
                Chart_Check_BestEnquerySource();

                if(abc > 0)
                {
                    String str;
                    con.Open();
                    DataSet ds = new DataSet();
                    //str = "SELECT distinct Count(I.C_Date) AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
                    str = "SELECT MAX(SourceEnqID) AS [SourceEnqID] FROM [tlb_Customer] WHERE [S_Status]='Active'";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    highSourceNPR = Convert.ToInt32(cmd.ExecuteScalar());
                    //abc = Convert.ToInt32(highSourceNPR);
                }
                else
                {
                    highSourceNPR = 0;
                }
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
        
        private void LoadColumnChart_FollowUp()
        {
            ((ColumnSeries)mcChart.Series[0]).ItemsSource = new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string,int>("Walk ins", folCount),
                new KeyValuePair<string,int>("Sales", sealsCount),
                new KeyValuePair<string,int>("Procurements", finalPro),
                new KeyValuePair<string,int>("Highest Sold Item", highSingleProduct) ,
                new KeyValuePair<string,int>("Ever Green Top Brand", highProduct),
                new KeyValuePair<string,int>("Best Enquiry Source", highSourceNPR),
                new KeyValuePair<string,int>("Customer Base", baseCust)
                
            };
        }
        #endregion ChartFollowUp

        #region Chart SalesByProducts
        int salesProCount;

        public void Chart_SalesProducts()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                str = "SELECT distinct Count(I.Product_ID) AS [ProductID],B.[Product_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Products] B ON B.[ID]=I.[Product_ID] WHERE I.[S_Status]='Active' Group By B.[Product_Name]";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                Dictionary<string,int> data = new Dictionary<string,int>();

                //salesProCount = Convert.ToInt32(cmd.ExecuteScalar());
                //if (ds.Tables[0].Rows.Count > 0)
                //for (int i = 0; i <= ds.Tables[0].Rows.Count; i++ )
                foreach(DataRow drv in ds.Tables[0].Rows)
                {
                    //salesProCount = Convert.ToInt32(cmd.ExecuteScalar());
                    string strvalue = Convert.ToString(drv["Product_Name"]);
                    salesProCount = Convert.ToInt32(drv["ProductID"]);
                    data.Add(Convert.ToString(strvalue), Convert.ToInt32(salesProCount));
                    //LoadSales_Products_chart();
                }
                ((BarSeries)salesChartByProducts.Series[0]).ItemsSource = data;
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

        private void LoadSales_Products_chart()
        {
            ((BarSeries) salesChartByProducts.Series[0]).ItemsSource = new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string,int>("Products", salesProCount),
                //new KeyValuePair<string,int>("Sales", sealsCount),
                //new KeyValuePair<string,int>("Procurements", finalPro),
                //new KeyValuePair<string,int>("Highest Sold Item", highSingleProduct) ,
                //new KeyValuePair<string,int>("Ever Green Top Brand", highProduct),
                //new KeyValuePair<string,int>("Customer Base", baseCust)
            };
        }

        private void chart_SalesProducts_Click(object sender, RoutedEventArgs e)
        {
            grd_SalesByProducts.Visibility = System.Windows.Visibility.Visible;

            Chart_SalesProducts();
            //LoadSales_Products_chart();

        }

        private void btnChartSales_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_SalesByProducts.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion Chart SalesByProducts

        #region Chart SalesByBrand
        public void Chart_SalesBrand()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                str = "SELECT distinct Count(I.Brand_ID) AS [BrandID],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                Dictionary<string, int> data = new Dictionary<string, int>();

                //salesProCount = Convert.ToInt32(cmd.ExecuteScalar());
                //if (ds.Tables[0].Rows.Count > 0)
                //for (int i = 0; i <= ds.Tables[0].Rows.Count; i++ )
                foreach (DataRow drv in ds.Tables[0].Rows)
                {
                    //salesProCount = Convert.ToInt32(cmd.ExecuteScalar());
                    string strvalue = Convert.ToString(drv["Brand_Name"]);
                    int salesBrand = Convert.ToInt32(drv["BrandID"]);
                    data.Add(Convert.ToString(strvalue), Convert.ToInt32(salesBrand));
                    //LoadSales_Products_chart();
                }
                ((ColumnSeries) salesChartByBrand.Series[0]).ItemsSource = data;
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

        private void chart_SalesBrand_Click(object sender, RoutedEventArgs e)
        {
            grd_ChartSalesByBrand.Visibility = System.Windows.Visibility.Visible;
            Chart_SalesBrand();
        }

        private void btnChartDetails_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_ChartSalesByBrand.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion Chart SalesByBrand

        public void Chart_SalesProcurmentDuration()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                //str = "SELECT distinct Count(I.C_Date) AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
                str = "SELECT  I.C_Date AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                Dictionary<string, string> data = new Dictionary<string, string>();

                //salesProCount = Convert.ToInt32(cmd.ExecuteScalar());
                //if (ds.Tables[0].Rows.Count > 0)
                //for (int i = 0; i <= ds.Tables[0].Rows.Count; i++ )
                foreach (DataRow drv in ds.Tables[0].Rows)
                {
                    //salesProCount = Convert.ToInt32(cmd.ExecuteScalar());
                    string strvalue = Convert.ToString(drv["Brand_Name"]);
                    string salesPDu = Convert.ToString(drv["CDate"]);
                    data.Add(Convert.ToString(strvalue), Convert.ToString(salesPDu));
                    //LoadSales_Products_chart();
                }
                ((BarSeries) salesChartByProcurmentDutaion.Series[0]).ItemsSource = data;
                
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

        private void salesPRoDuraion_Click(object sender, RoutedEventArgs e)
        {
            grd_ChartProcurmentToSale.Visibility = System.Windows.Visibility.Visible;
            Chart_SalesProcurmentDuration();
        }

        private void btnSourceOfEnquery_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_ChartSourceOfEnquery.Visibility = System.Windows.Visibility.Hidden;
        }

        #region SourceOfEnquiry Finction
        int sourceIncCount;
        int sourceIncCountPos;
        int sourceIncCountRef;
        int sourceIncCountFC;
        int sourceIncCountNW;
        int sourceIncCountN;

        public void Chart_SourceOfEnqueiry()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                //str = "SELECT distinct Count(I.C_Date) AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
                str = "SELECT Count(SourceOfEnquiry) FROM [tlb_Customer] WHERE [SourceOfEnquiry]='Newspaper' AND [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                
                sourceIncCount = Convert.ToInt32(cmd.ExecuteScalar());

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

        public void Chart_SourceOfEnqueiry_Poster()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                //str = "SELECT distinct Count(I.C_Date) AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
                str = "SELECT Count(SourceOfEnquiry) FROM [tlb_Customer] WHERE [SourceOfEnquiry]='Poster' AND [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                sourceIncCountPos = Convert.ToInt32(cmd.ExecuteScalar());

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

        public void Chart_SourceOfEnqueiry_Reference()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                //str = "SELECT distinct Count(I.C_Date) AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
                str = "SELECT Count(SourceOfEnquiry) FROM [tlb_Customer] WHERE [SourceOfEnquiry]='Reference' AND [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                sourceIncCountRef = Convert.ToInt32(cmd.ExecuteScalar());

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

        public void Chart_SourceOfEnqueiry_FriendsColleagues()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                //str = "SELECT distinct Count(I.C_Date) AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
                str = "SELECT Count(SourceOfEnquiry) FROM [tlb_Customer] WHERE [SourceOfEnquiry]='Friends / Colleagues' AND [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                sourceIncCountFC = Convert.ToInt32(cmd.ExecuteScalar());

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

        public void Chart_SourceOfEnqueiry_NetWebsite()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                //str = "SELECT distinct Count(I.C_Date) AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
                str = "SELECT Count(SourceOfEnquiry) FROM [tlb_Customer] WHERE [SourceOfEnquiry]='Net / Website' AND [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                sourceIncCountNW = Convert.ToInt32(cmd.ExecuteScalar());

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

        public void Chart_SourceOfEnqueiry_Non()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                //str = "SELECT distinct Count(I.C_Date) AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
                str = "SELECT Count(SourceOfEnquiry) FROM [tlb_Customer] WHERE [SourceOfEnquiry]='Non' AND [S_Status]='Active'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                sourceIncCountN = Convert.ToInt32(cmd.ExecuteScalar());

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


        private void LoadColumnChart_SourceOfEnqeiry()
        {
            ((ColumnSeries)salesChartSourceOfEnquery.Series[0]).ItemsSource = new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string,int>("Newspaper", sourceIncCount),
                new KeyValuePair<string,int>("Poster", sourceIncCountPos),
                new KeyValuePair<string,int>("Friends/Colleagues", sourceIncCountFC),
                new KeyValuePair<string,int>("Net/Website", sourceIncCountNW),
                new KeyValuePair<string,int>("Reference", sourceIncCountRef),
                new KeyValuePair<string,int>("Non", sourceIncCountN)
            };
        }

        private void grd_ChartSourceOfEnquery_Loaded(object sender, RoutedEventArgs e)
        {
            Chart_SourceOfEnqueiry();
            Chart_SourceOfEnqueiry_Poster();
            Chart_SourceOfEnqueiry_FriendsColleagues();
            Chart_SourceOfEnqueiry_NetWebsite();
            Chart_SourceOfEnqueiry_Reference();
            Chart_SourceOfEnqueiry_Non();

            LoadColumnChart_SourceOfEnqeiry();
        }

        private void chart_SourceOfEnquiry_Click(object sender, RoutedEventArgs e)
        {
            grd_ChartSourceOfEnquery.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion SourceOfEnquiry Finction

        #region LaadAndSalesEmployee Details
        private void btnSalesByEmp_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_ChartSalesByEmployee.Visibility = System.Windows.Visibility.Hidden;
        }
               
        public void Chart_SalesByEmployee()
        {
            try
            {
                String str;
                con.Open();
                DataSet ds = new DataSet();
                str = "SELECT distinct Count(I.Employee_ID) AS [EmployeeID],B.[EmployeeName] FROM [tlb_Bill_No] I INNER JOIN [tbl_Employee] B ON B.[ID]=I.[Employee_ID] WHERE I.[S_Status]='Active' Group By B.[EmployeeName]";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                Dictionary<string, string> data = new Dictionary<string, string>();

                //salesProCount = Convert.ToInt32(cmd.ExecuteScalar());
                //if (ds.Tables[0].Rows.Count > 0)
                //for (int i = 0; i <= ds.Tables[0].Rows.Count; i++ )
                foreach (DataRow drv in ds.Tables[0].Rows)
                {
                    //salesProCount = Convert.ToInt32(cmd.ExecuteScalar());
                    string strvalue = Convert.ToString(drv["EmployeeName"]);
                    string salesEmp = Convert.ToString(drv["EmployeeID"]);
                    data.Add(Convert.ToString(strvalue), Convert.ToString(salesEmp));
                    //LoadSales_Products_chart();
                }
                ((PieSeries)salesChartSalesByEmployee.Series[0]).ItemsSource = data;

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

        private void grd_ChartSalesByEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            Chart_SalesByEmployee();
        }

        private void chart_LeadSalesEmployee_Click(object sender, RoutedEventArgs e)
        {
            grd_ChartSalesByEmployee.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion LaadAndSalesEmployee Details

        #region ProgressDetails Function
        private void btnProgressDetails_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_ProgressDetails.Visibility = System.Windows.Visibility.Hidden;
        }

        private void LoadColumnChart_FollowUp_ProcessDetails()
        {
            ((AreaSeries)salesChartProgressDetails.Series[0]).ItemsSource = new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string,int>("Walk Ins", folCount),
                new KeyValuePair<string,int>("Sales", sealsCount),
                //new KeyValuePair<string,int>("Procurements", finalPro),
                //new KeyValuePair<string,int>("Highest Sold Item", highSingleProduct) ,
                //new KeyValuePair<string,int>("Ever Green Top Brand", highProduct),
                //new KeyValuePair<string,int>("Customer Base", baseCust)
            };
        }

        private void grd_ProgressDetails_Loaded(object sender, RoutedEventArgs e)
        {
            Chart_Followup();
            Chart_Seals();
            LoadColumnChart_FollowUp_ProcessDetails();
        }

        private void chart_ProgressDetails_Click(object sender, RoutedEventArgs e)
        {
            grd_ProgressDetails.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion ProgressDetails Function
        #endregion ChartFunction

        #region Cheque Function
        #region ChequeClear Fun
        public bool CheckClear_Validation()
        {
            bool result = false;
            if (txtChequeID.Text == "")
            {
                result = true;
                MessageBox.Show("Please Select Check", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return result;
        }

        public void GetData_ChequeDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[Customer_ID],P.[Bill_No],P.[Total_Price],P.[Cheque_Amount],P.[Cheque_No],P.[Cheque_Date],P.[Cheque_Bank_Name] " +
                      ",C.[Name],C.[Mobile_No] " +
                      "FROM [tlb_Cheque] P " +
                      "INNER JOIN [tlb_Customer] C ON C.[ID] = P.[Customer_ID] " +
                      "WHERE ";
                if ((dtpFrom_ChequeSearch.SelectedDate != null) && (dtpTo_ChequeSearch.SelectedDate != null))
                {
                    DateTime StartDate = Convert.ToDateTime(dtpFrom_ChequeSearch.Text.Trim() + " 00:00:00.000");
                    DateTime EndDate = Convert.ToDateTime(dtpTo_ChequeSearch.Text.Trim() + " 23:59:59.999");
                    str = str + "P.[Cheque_Date] Between '" + StartDate + "' AND '" + EndDate + "'  AND ";
                }

                if (txtCustomerName_Cheque_Search.Text.Trim() != "")
                {
                    str = str + "C.[Name] LIKE ISNULL('" + txtCustomerName_Cheque_Search.Text.Trim() + "',C.[Name]) + '%' AND ";
                }

                str = str + " P.[IsClear] = 'Active' ORDER BY P.[Cheque_Date] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgv_ChequeDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void Cheque_FillData()
        {
            try
            {
                var id1 = (DataRowView)dgv_ChequeDetails.SelectedItem; //get specific ID from          DataGrid after click on Edit button in DataGrid   
                PK_ID = Convert.ToInt32(id1.Row["ID"].ToString());
                con.Open();
                //string sqlquery = "SELECT * FROM tbl_DealerEntry where Id='" + PK_ID + "' ";

                string sqlquery = "SELECT [ID],[Cheque_Date],[Cheque_No] FROM [tlb_Cheque] WHERE [ID]='" + PK_ID + "' ";

                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtChequeID.Text = dt.Rows[0]["ID"].ToString();
                    dtpChequeDate.SelectedDate = Convert.ToDateTime(dt.Rows[0]["Cheque_Date"].ToString());
                    txtChequeNo.Text = dt.Rows[0]["Cheque_No"].ToString();
                }
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

        public void ChequeClear()
        {
            bcheckUp.Flag = 1;
            bcheckUp.CheckID = Convert.ToInt32(txtChequeID.Text);
            bcheckUp.IsClear = "DeActive";
            dcheckUp.CheckUpdate_Insert_Update_Delete(bcheckUp);
            MessageBox.Show("Cheque Clear Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion ChequeClear Fun

        #region Cheque Load Event
        private void grd_CheckDetails_Loaded(object sender, RoutedEventArgs e)
        {
            GetData_ChequeDetails();
            
        }
        #endregion Cheque Load Event

        #region Cheque Button Event
        private void btnCheckCustomer_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_CheckDetails.Visibility = System.Windows.Visibility.Hidden;
            dtpChequeDate.Text = "";
            txtChequeID.Text = "";
            txtChequeNo.Text = "";
        }

        private void btnCheckCustomer_Refresh_Click(object sender, RoutedEventArgs e)
        {
           // GetData_ChequeDetails();
            txtCustomerName_Cheque_Search.Text = "";
            dtpFrom_ChequeSearch.Text = "";
            dtpTo_ChequeSearch.Text = "";
            dtpChequeDate.Text = "";
            txtChequeID.Text = "";
            txtChequeNo.Text = "";
        }

        private void btnCheckCustomer_ChequeClear_Click(object sender, RoutedEventArgs e)
        {
            if (CheckClear_Validation() == true)
                return;

            var result = MessageBox.Show("Do you want to Clear Cheque No.- \n" + txtChequeNo.Text.Trim() + "", caption, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            if (result == MessageBoxResult.Yes)
            {
                if (dtpChequeDate.SelectedDate <= DateTime.Now)
                {
                    ChequeClear();

                    //Cal_BalanceAmt();
                    //if (Convert.ToDouble(txtbalanceAmt.Text.Trim()) == Convert.ToDouble(0))
                    //{
                    //    Update_InstallmentPayment_IsPaid();
                    //}
                    //Update_SuspenseAccount_IsBounceClear();
                    //Update_InstallmentPayment_NotPaidClear();
                    //MessageBox.Show("Clear Cheque", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Can Not Clear Cheque Before Date", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //GetData();
                //CalTotalAmount();
                
            }
            else
            {
                MessageBox.Show("Error ..!", caption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dtpChequeDate.Text = "";
            txtChequeID.Text = "";
            txtChequeNo.Text = "";
            //dgv_ChequeDetails.UnselectAllCells();
            //GetData_ChequeDetails();
        }
        #endregion Cheque Button Event

        #region Cheque Event
        private void dgv_ChequeDetails_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Cheque_FillData();
        }

        private void txtCustomerName_Cheque_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetData_ChequeDetails();
        }

        private void dtpFrom_ChequeSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            GetData_ChequeDetails();
        }

        private void dtpTo_ChequeSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            GetData_ChequeDetails();
        }
        #endregion Cheque Event

        private void smCheckDetails_Click(object sender, RoutedEventArgs e)
        {
            grd_CheckDetails.Visibility = System.Windows.Visibility.Visible;
        }

        #endregion Cheque Function

        #region Insurance Function
        private void grd_InsuranceDetails_Loaded(object sender, RoutedEventArgs e)
        {
            //AllInsurance_ProductsDetails();
        }

        #region Insurance Fun
        public void Load_ProductInsuranceCustomerID()
        {
            //  cmbInstall_CustID.Text = "--Select--";
            string q = "SELECT [ID],[Cust_ID] FROM [tlb_Customer]  Order By [Cust_ID] ";
            cmd = new SqlCommand(q, con);
            // DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmbInsurance_CustomerID.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                cmbInsurance_CustomerID.ItemsSource = ds.Tables[0].DefaultView;
                cmbInsurance_CustomerID.DisplayMemberPath = ds.Tables[0].Columns["Cust_ID"].ToString();
            }
        }
        
        public void Load_CustomerDetails()
        {
            //  cmbInstall_CustID.Text = "--Select--";
            string q = "SELECT ID  ,Name FROM tlb_Customer ";
            cmd = new SqlCommand(q, con);
            // DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmbInsurance_CustName.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                cmbInsurance_CustName.ItemsSource = ds.Tables[0].DefaultView;
                cmbInsurance_CustName.DisplayMemberPath = ds.Tables[0].Columns["Name"].ToString();
            }
        }

        public void Products_CustomerDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[Customer_ID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Per_Product_Price],P.[Qty],P.[C_Price],P.[Tax_Name],P.[Tax],P.[Total_Price] " +
                      ",DM.[Domain_Name],PM.[Product_Name],B.[Brand_Name],PC.[Product_Category],MN.[Model_No],C.[Color] " +
                      ",S.[HaveInsurance] " +
                      "FROM [tlb_InvoiceDetails] P " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "INNER JOIN [StockDetails] S ON S.[Model_No_ID]=P.[Model_No_ID] " +
                      "WHERE ";
                if (cmbAdm_DealerFilter_Search.SelectedIndex > 0)
                {
                    str = str + "P.[Customer_ID] = '" + cmbInsurance_CustName.SelectedValue.GetHashCode() + "' AND ";
                }
                if (cmbInsurance_CustomerID.SelectedIndex >= 0)
                {
                    //str = str + "C.[Cust_ID] LIKE ISNULL('" + cmbAllInsurance_CustomerID.SelectedValue.GetHashCode() + "',C.[Cust_ID]) + '%' AND ";
                    str = str + "P.[Customer_ID] = '" + cmbInsurance_CustomerID.SelectedValue.GetHashCode() + "' AND ";
                }
                str = str + "P.[S_Status] = 'Active' ORDER BY DM.[Domain_Name] ASC ";

                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvInsurance_Details.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        //public void Load_AllInsuranceCustomerDetails()
        //{
        //    //  cmbInstall_CustID.Text = "--Select--";
        //    string q = "SELECT I.[ID ], distinct (I.[Customer_ID]) AS [Customer_ID] ,C.[Name] FROM [tlb_InsuranceEntry] I INNER JOIN [tlb_Customer] C ON C.[ID]=I.[Customer_ID] Group By C.[Name] ";
        //    cmd = new SqlCommand(q, con);
        //    // DataTable dt = new DataTable();
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);

        //    adp.Fill(ds);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        cmbAllInsurance_CustName.SelectedValuePath = ds.Tables[0].Columns["Customer_ID"].ToString();
        //        cmbAllInsurance_CustName.ItemsSource = ds.Tables[0].DefaultView;
        //        cmbAllInsurance_CustName.DisplayMemberPath = ds.Tables[0].Columns["Name"].ToString();
        //    }
        //}

        public void Load_AllInsuranceCustomerDetails()
        {
            //  cmbInstall_CustID.Text = "--Select--";
            string q = "SELECT [ID],[Name] FROM [tlb_Customer]  Order By [Name] ";
            cmd = new SqlCommand(q, con);
            // DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmbAllInsurance_CustName.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                cmbAllInsurance_CustName.ItemsSource = ds.Tables[0].DefaultView;
                cmbAllInsurance_CustName.DisplayMemberPath = ds.Tables[0].Columns["Name"].ToString();
            }
        }

        public void Load_AllInsuranceCustomerID()
        {
            //  cmbInstall_CustID.Text = "--Select--";
            string q = "SELECT [ID],[Cust_ID] FROM [tlb_Customer]  Order By [Cust_ID] ";
            cmd = new SqlCommand(q, con);
            // DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmbAllInsurance_CustomerID.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                cmbAllInsurance_CustomerID.ItemsSource = ds.Tables[0].DefaultView;
                cmbAllInsurance_CustomerID.DisplayMemberPath = ds.Tables[0].Columns["Cust_ID"].ToString();
            }
        }

        
        public void Insurance_ProductsDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT [ID],[Customer_ID],[InsuranceNo],[ProductName],[InsuranceAmt],[BankName],[InsuranceDate],[NoOfMonth],[NoOfYearMonths],[YearsMonths] AS [YearMonth],[IntervalMonths],[IntervalMonthY],[IntervalAmt],[NewInsuranceDate],[FirstPartyInsurance],[IsClear],[S_Status] " +
                      "FROM [tlb_InsuranceEntry] " +
                      "WHERE ";       //[Customer_ID]= '" + cmbInsurance_CustName.SelectedValue.GetHashCode() + "' AND [IsClear] = 'Active' ORDER BY [ProductName] ASC ";
                if (cmbAdm_DealerFilter_Search.SelectedIndex > 0)
                {
                    str = str + "[Customer_ID] = '" + cmbInsurance_CustName.SelectedValue.GetHashCode() + "' AND ";
                }
                if (cmbInsurance_CustomerID.SelectedIndex >= 0)
                {
                    //str = str + "C.[Cust_ID] LIKE ISNULL('" + cmbAllInsurance_CustomerID.SelectedValue.GetHashCode() + "',C.[Cust_ID]) + '%' AND ";
                    str = str + "[Customer_ID] = '" + cmbInsurance_CustomerID.SelectedValue.GetHashCode() + "' AND ";
                }
                str = str + "[IsClear] = 'Active' ORDER BY [ProductName] ASC ";

                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvInsurance_ProductDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
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

        public void AllInsurance_ProductsDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT I.[ID],I.[Customer_ID],I.[InsuranceNo],I.[ProductName],I.[InsuranceAmt],I.[BankName],I.[InsuranceDate],I.[NoOfMonth],I.[NoOfYearMonths],I.[YearsMonths] AS [YearMonth],I.[IntervalMonths],I.[IntervalMonthY],I.[IntervalAmt],I.[NewInsuranceDate],I.[FirstPartyInsurance],I.[IsClear],I.[S_Status] " +
                      ",C.[Cust_ID] " +
                      "FROM [tlb_InsuranceEntry] I " +
                      "INNER JOIN tlb_Customer C ON C.[ID]=I.[Customer_ID] " +
                      "WHERE ";       //[Customer_ID]= '" + cmbInsurance_CustName.SelectedValue.GetHashCode() + "' AND [IsClear] = 'Active' ORDER BY [ProductName] ASC ";
                if (cmbAllInsurance_CustName.SelectedIndex >= 0)
                {
                    str = str + "I.[Customer_ID] = '" + cmbAllInsurance_CustName.SelectedValue.GetHashCode() + "' AND ";

                    //str = str + "I.[Customer_ID] = '" + cmbAllInsurance_CustName.SelectedValue.GetHashCode() + "' AND ";
                    //str = str + "C.[Color] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',C.[Color]) + '%' AND ";
                }
                if (cmbAllInsurance_CustomerID.SelectedIndex >= 0)
                {
                    //str = str + "C.[Cust_ID] LIKE ISNULL('" + cmbAllInsurance_CustomerID.SelectedValue.GetHashCode() + "',C.[Cust_ID]) + '%' AND ";
                    str = str + "I.[Customer_ID] = '" + cmbAllInsurance_CustomerID.SelectedValue.GetHashCode() + "' AND ";
                }
                str = str + "I.[IsClear] = 'Active' ORDER BY I.[ProductName] ASC ";

                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAllInsuranceDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
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
        #endregion Insurance Fun

        #region Insurance Button Event
        private void btnInsurance_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_Insurance.Visibility = System.Windows.Visibility.Hidden;
            cmbInsurance_CustName.SelectedValue = null;
            dgvInsurance_Details.ItemsSource = null;

        }

        private void btnInsurance_Exit1_Click(object sender, RoutedEventArgs e)
        {
            grd_InsuranceDetails.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btndgv_InsuranceUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id1 = (DataRowView)dgvAllInsuranceDetails.SelectedItem; //get specific ID from          DataGrid after click on Edit button in DataGrid   
                PK_ID = Convert.ToInt32(id1.Row["ID"].ToString());
                con.Open();
                string sqlquery = "SELECT * FROM tlb_InsuranceEntry where ID='" + PK_ID + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtInsurance_ID.Text = dt.Rows[0]["ID"].ToString();
                    txtInsurance_CustID.Text = dt.Rows[0]["Customer_ID"].ToString();
                }

                frmInsurance obj = new frmInsurance();
                obj.InsuranceID(txtInsurance_ID.Text.Trim(), txtInsurance_CustID.Text.Trim());
                obj.Insurance_FillData();
                //obj.LoadNoOfYears1();
                //obj.LoadNoOfMonths1();
                obj.ShowDialog();

                // con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            //GetData_EmployeeDetails();
        }

        private void btnInsurance_Refresh_Click(object sender, RoutedEventArgs e)
        {
            cmbAllInsurance_CustomerID.SelectedItem = null;
            cmbAllInsurance_CustName.SelectedItem = null;
            AllInsurance_ProductsDetails();
        }
        #endregion Insurance Button Event

        #region Insurance Event
        private void cmbInsurance_CustomerID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Products_CustomerDetails();
            Insurance_ProductsDetails();
        }

        private void sminsuranceDetails_Click(object sender, RoutedEventArgs e)
        {
            grd_InsuranceDetails.Visibility = System.Windows.Visibility.Visible;
            Load_AllInsuranceCustomerDetails();
            Load_AllInsuranceCustomerID();
            //AllInsurance_ProductsDetails();
        }

        //private void sminsurance_Click(object sender, RoutedEventArgs e)
        //{
        //    grd_Insurance.Visibility = System.Windows.Visibility.Visible;
        //    Load_CustomerDetails();
        //}

        //private void cmbInsurance_CustName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Products_CustomerDetails();
        //    Insurance_ProductsDetails();
        //}

        //private void dgvInsurance_Details_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    //frmInsurance obj = new frmInsurance();
        //    //obj.ShowDialog();
        //    //obj.LoadYearsMonth();
        //    //obj.LoadInterval();
        //}

        //private void chkInsurance_Checked(object sender, RoutedEventArgs e)
        //{

        //    try
        //    {
        //        var Iid = (DataRowView)dgvInsurance_Details.SelectedItem; //get specific ID from          DataGrid after click on Edit button in DataGrid   
        //        PK_ID = Convert.ToInt32(Iid.Row["ID"].ToString());
        //        con.Open();
        //        string sqlquery = "SELECT * FROM tlb_InvoiceDetails where ID='" + PK_ID + "' ";
        //        SqlCommand cmd = new SqlCommand(sqlquery, con);
        //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        adp.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            txtInsurance_InvoiceID.Text = dt.Rows[0]["ID"].ToString();
        //        }

        //        frmInsurance obj = new frmInsurance();
        //        obj.InsuranceID(txtInsurance_InvoiceID.Text.Trim(), txtInsurance_CustID.Text.Trim());
        //        obj.Insurance_FillData();
        //        obj.ShowDialog();

        //        // con.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    AllInsurance_ProductsDetails();
        //}

        private void cmbAllInsurance_CustName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllInsurance_ProductsDetails();
        }

        private void sminsurance_Click(object sender, RoutedEventArgs e)
        {
            grd_Insurance.Visibility = System.Windows.Visibility.Visible;
            Load_CustomerDetails();
            Load_ProductInsuranceCustomerID();
        }

        private void cmbInsurance_CustName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Products_CustomerDetails();
            Insurance_ProductsDetails();
        }

        private void dgvInsurance_Details_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //frmInsurance obj = new frmInsurance();
            //obj.ShowDialog();
            //obj.LoadYearsMonth();
            //obj.LoadInterval();
        }

        private void chkInsurance_Checked(object sender, RoutedEventArgs e)
        {

            try
            {
                var Iid = (DataRowView)dgvInsurance_Details.SelectedItem; //get specific ID from          DataGrid after click on Edit button in DataGrid   
                PK_ID = Convert.ToInt32(Iid.Row["ID"].ToString());
                con.Open();
                string sqlquery = "SELECT * FROM tlb_InvoiceDetails where ID='" + PK_ID + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtInsurance_InvoiceID.Text = dt.Rows[0]["ID"].ToString();
                }

                frmInsurance obj = new frmInsurance();
                obj.InsuranceID(txtInsurance_ID.Text.Trim(), txtInsurance_CustID.Text.Trim());
                obj.Insurance_FillData();
                //obj.LoadNoOfYears1();
                //obj.LoadNoOfMonths1();
                obj.ShowDialog();

                // con.Close();
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
        #endregion Insurance Event
        #endregion Insurance Function

    }
}

