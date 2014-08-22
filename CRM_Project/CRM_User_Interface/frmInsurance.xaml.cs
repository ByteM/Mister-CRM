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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class frmInsurance : Window
    {

        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        SqlDataReader dr;
        string caption = "Green Future Glob";
        static int PK_ID;

        BAL_InsuranceEntry binsuranceEntry = new BAL_InsuranceEntry();
        DAL_InsuranceEntry dinsuranceEntry = new DAL_InsuranceEntry();

        public frmInsurance()
        {
            InitializeComponent();
            LoadYearsMonth();
            LoadInterval();
        }

        #region Button Event
        private void btnInExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Clear_ALL();
        }
        
        string SET_YEAR;
        string SET_MONTH, DATE1, DATE;
        int addNY, iadd, totMonth;
        int chsetYear, chsetMonth, chsetDate1;
        int newLpDate, newLPMonth;

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.Content == "Save")
            {
                try
                {
                    string FPInsurance;

                    binsuranceEntry.Flag = 1;
                    binsuranceEntry.CustomerID = Convert.ToInt32(txtCustomerID.Text);
                    binsuranceEntry.InsuranceNo = lblInsuranceNo.Content.ToString();
                    binsuranceEntry.ProductName = lblProductName.Content.ToString();
                    binsuranceEntry.InsuranceAmt = Convert.ToDouble(txtInsuranceAmt.Text);
                    binsuranceEntry.BankName = cmbBankIntegration.Text;
                    binsuranceEntry.InsuranceDate = dtpDate.Text;
                    binsuranceEntry.NoOfYearsMonths = Convert.ToInt32(txtValidity.Text);
                    binsuranceEntry.NoOfMonth = Convert.ToInt32(txtMonths.Text);
                    binsuranceEntry.YearsMonth = cmbValidity.Text;
                    binsuranceEntry.IntervalMonth = Convert.ToInt32(txtInterval.Text);
                    binsuranceEntry.IntervalMonthY = cmbInterval.Text;
                    binsuranceEntry.IntervalAmount = Convert.ToDouble(txtIntervalTotalAmt.Text);

                    string STRTODAYDATE = dtpDate.Text;

                    string[] STRVAL = STRTODAYDATE.Split('-');
                    string STR_DATE1 = STRVAL[0];
                    string STR_MONTH = STRVAL[1];
                    string STR_YEAR = STRVAL[2];

                    if (cmbValidity.SelectedItem.Equals("Year"))
                    {
                        int vlYear, vlNo, addNY;
                        vlYear = Convert.ToInt32(STR_YEAR);
                        vlNo = Convert.ToInt32(txtValidity.Text);
                        addNY = vlYear + vlNo;
                        SET_YEAR = Convert.ToString(addNY);

                        DATE1 = STR_DATE1 + "-" + STR_MONTH + "-" + SET_YEAR;
                    }
                    else
                        if (cmbValidity.SelectedItem.Equals("Month"))
                        {
                            int vlMonth, vlNo;
                            vlMonth = Convert.ToInt32(STR_MONTH);
                            vlNo = Convert.ToInt32(txtValidity.Text);

                            for (int i = 1; i <= vlNo; i++)
                            {
                                if (addNY == 12)
                                {
                                    int abc;
                                    abc = Convert.ToInt32(STR_YEAR) + 1;
                                    SET_YEAR = Convert.ToString(abc);
                                    int neMonth;
                                    addNY = 0;
                                    vlMonth = 0;
                                    vlNo = 1;
                                    totMonth = Convert.ToInt32(txtMonths.Text);
                                    neMonth = totMonth - iadd;
                                    for (int j = 1; j <= neMonth; j++)
                                    {
                                        addNY = vlMonth + j;
                                    }
                                }
                                else
                                {
                                    int k = 0;
                                    addNY = vlMonth + i;
                                    iadd = k + i;
                                }

                            }
                            SET_MONTH = Convert.ToString(addNY);

                            DATE = STR_DATE1 + "-" + SET_MONTH + "-" + SET_YEAR;

                            chsetYear = Convert.ToInt32(SET_YEAR);
                            chsetMonth = Convert.ToInt32(SET_MONTH);
                            chsetDate1 = Convert.ToInt32(STR_DATE1);
                            if (chsetYear % 4 == 0)
                            {
                                if (chsetMonth == 2)
                                {
                                    if (chsetDate1 == 29)
                                    {
                                        newLpDate = 01;
                                        newLPMonth = 03;
                                    }
                                }
                                DATE1 = newLpDate + "-" + newLPMonth + "-" + SET_YEAR;
                            }
                            else
                            {
                                DATE1 = STR_DATE1 + "-" + SET_MONTH + "-" + SET_YEAR;
                            }
                        }

                    dtpInstallmentDate.Text = DATE1;

                    string dt = dtpInstallmentDate.Text;
                    ////txttime.Text = time;

                    //baddprd.C_Date =Convert .ToDateTime( DATE);

                    binsuranceEntry.NewInsuranceDate = dt;
                    if (chbInsurance.IsChecked == true)
                    {
                        FPInsurance = "Yes";
                    }
                    else
                    {
                        FPInsurance = "No";
                    }
                    binsuranceEntry.FirstPartyInsurance = FPInsurance;
                    binsuranceEntry.IsClear = "Active";
                    binsuranceEntry.S_Status = "Active";
                    binsuranceEntry.C_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
                    dinsuranceEntry.InsuranceEntry_Insert_Update_Delete(binsuranceEntry);
                    MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
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
            else if (btnSave.Content == "Update")
            {
                try
                {
                    string FPInsurance;

                    binsuranceEntry.Flag = 2;
                    binsuranceEntry.InsuranceID = Convert.ToInt32(txtInsuranceID.Text);
                    binsuranceEntry.CustomerID = Convert.ToInt32(txtCustomerID.Text);
                    binsuranceEntry.InsuranceNo = lblInsuranceNo.Content.ToString();
                    binsuranceEntry.ProductName = lblProductName.Content.ToString();
                    binsuranceEntry.InsuranceAmt = Convert.ToDouble(txtInsuranceAmt.Text);
                    binsuranceEntry.BankName = cmbBankIntegration.Text;
                    binsuranceEntry.InsuranceDate = dtpDate.Text;
                    binsuranceEntry.NoOfYearsMonths = Convert.ToInt32(txtValidity.Text);
                    binsuranceEntry.NoOfMonth = Convert.ToInt32(txtMonths.Text);
                    binsuranceEntry.YearsMonth = cmbValidity.Text;
                    binsuranceEntry.IntervalMonth = Convert.ToInt32(txtInterval.Text);
                    binsuranceEntry.IntervalMonthY = cmbInterval.Text;
                    binsuranceEntry.IntervalAmount = Convert.ToDouble(txtIntervalTotalAmt.Text);

                    string STRTODAYDATE = dtpDate.Text;

                    string[] STRVAL = STRTODAYDATE.Split('-');
                    string STR_DATE1 = STRVAL[0];
                    string STR_MONTH = STRVAL[1];
                    string STR_YEAR = STRVAL[2];

                    if (cmbValidity.SelectedItem.Equals("Year"))
                    {
                        int vlYear, vlNo, addNY;
                        vlYear = Convert.ToInt32(STR_YEAR);
                        vlNo = Convert.ToInt32(txtValidity.Text);
                        addNY = vlYear + vlNo;
                        SET_YEAR = Convert.ToString(addNY);

                        DATE1 = STR_DATE1 + "-" + STR_MONTH + "-" + SET_YEAR;

                    }
                    else
                        if (cmbValidity.SelectedItem.Equals("Month"))
                        {
                            int vlMonth, vlNo;
                            vlMonth = Convert.ToInt32(STR_MONTH);
                            vlNo = Convert.ToInt32(txtValidity.Text);

                            for (int i = 1; i <= vlNo; i++)
                            {
                                if (addNY == 12)
                                {
                                    int abc;
                                    abc = Convert.ToInt32(STR_YEAR) + 1;
                                    SET_YEAR = Convert.ToString(abc);
                                    int neMonth;
                                    addNY = 0;
                                    vlMonth = 0;
                                    vlNo = 1;
                                    totMonth = Convert.ToInt32(txtMonths.Text);
                                    neMonth = totMonth - iadd;
                                    for (int j = 1; j <= neMonth; j++)
                                    {
                                        addNY = vlMonth + j;
                                    }
                                }
                                else
                                {
                                    int k = 0;
                                    addNY = vlMonth + i;
                                    iadd = k + i;
                                }

                            }
                            SET_MONTH = Convert.ToString(addNY);

                            DATE = STR_DATE1 + "-" + SET_MONTH + "-" + SET_YEAR;

                            chsetYear = Convert.ToInt32(SET_YEAR);
                            chsetMonth = Convert.ToInt32(SET_MONTH);
                            chsetDate1 = Convert.ToInt32(STR_DATE1);
                            if (chsetYear % 4 == 0)
                            {
                                if (chsetMonth == 2)
                                {
                                    if (chsetDate1 == 29)
                                    {
                                        newLpDate = 01;
                                        newLPMonth = 03;
                                    }
                                }
                                DATE1 = newLpDate + "-" + newLPMonth + "-" + SET_YEAR;
                            }
                            else
                            {
                                DATE1 = STR_DATE1 + "-" + SET_MONTH + "-" + SET_YEAR;
                            }
                        }

                    dtpInstallmentDate.Text = DATE1;

                    string dt = dtpInstallmentDate.Text;
                    ////txttime.Text = time;

                    //baddprd.C_Date =Convert .ToDateTime( DATE);

                    binsuranceEntry.NewInsuranceDate = dt;
                    if (chbInsurance.IsChecked == true)
                    {
                        FPInsurance = "Yes";
                    }
                    else
                    {
                        FPInsurance = "No";
                    }
                    binsuranceEntry.FirstPartyInsurance = FPInsurance;
                    binsuranceEntry.IsClear = "Active";
                    binsuranceEntry.S_Status = "Active";
                    binsuranceEntry.C_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
                    dinsuranceEntry.InsuranceEntry_Update_Delete(binsuranceEntry);
                    MessageBox.Show("Data Updated Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
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
            Clear_ALL();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion Event

        #region Insurance Function
        public void Clear_ALL()
        {
            txtCustomerID.Text = "";
            txtInsuranceID.Text = "";
            txtInsuranceAmt.Text = "";
            cmbBankIntegration.Text = "";
            dtpDate.Text = "";
            txtDate.Text = "";
            txtValidity.Text = "";
            cmbValidity.Text = "";
            txtMonths.Text = "";
            dtpInstallmentDate.Text = "";
            chbInsurance.IsChecked = false;
        }

        public void Load_BankName()
        {
            //  cmbInstall_CustID.Text = "--Select--";
            string q = "SELECT distinct(BankName) As BankName FROM tlb_InsuranceEntry ";
            cmd = new SqlCommand(q, con);
            // DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //cmbInsurance_CustName.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                cmbBankIntegration.ItemsSource = ds.Tables[0].DefaultView;
                cmbBankIntegration.DisplayMemberPath = ds.Tables[0].Columns["BankName"].ToString();
            }
        }

        public void InsuranceID(string iid, string cuID)
        {
            txtInsuranceID.Text = iid;
            txtCustomerID.Text = cuID;
        }

        public void LoadYearsMonth()
        {
            cmbValidity.Text = "Select";
            cmbValidity.Items.Add("Year");
            cmbValidity.Items.Add("Month");
        }

        public void LoadInterval()
        {
            cmbInterval.Text = "Select";
            cmbInterval.Items.Add("Year");
            cmbInterval.Items.Add("Month");
        }

        public void SelectYearMonth()
        {
            if(cmbValidity.SelectedItem == "Year")
            {
                if(txtValidity.Text == "1")
                {
                    txtMonths.Text = "12";
                }
                else if(txtValidity.Text == "2")
                {
                    txtMonths.Text = "24";
                }
                else if(txtValidity.Text == "3")
                {
                    txtMonths.Text = "36";
                }
                if (txtValidity.Text == "4")
                {
                    txtMonths.Text = "48";
                }
                else if (txtValidity.Text == "5")
                {
                    txtMonths.Text = "60";
                }
                else if (txtValidity.Text == "6")
                {
                    txtMonths.Text = "72";
                }
                if (txtValidity.Text == "7")
                {
                    txtMonths.Text = "84";
                }
                else if (txtValidity.Text == "8")
                {
                    txtMonths.Text = "96";
                }
                else if (txtValidity.Text == "9")
                {
                    txtMonths.Text = "108";
                }
                if (txtValidity.Text == "10")
                {
                    txtMonths.Text = "120";
                }
                else if (txtValidity.Text == "11")
                {
                    txtMonths.Text = "132";
                }
                else if (txtValidity.Text == "12")
                {
                    txtMonths.Text = "144";
                }
                else if (txtValidity.Text == "13")
                {
                    txtMonths.Text = "156";
                }
                else if (txtValidity.Text == "14")
                {
                    txtMonths.Text = "168";
                }
                else if (txtValidity.Text == "15")
                {
                    txtMonths.Text = "180";
                }
                if (txtValidity.Text == "16")
                {
                    txtMonths.Text = "192";
                }
                else if (txtValidity.Text == "17")
                {
                    txtMonths.Text = "204";
                }
                else if (txtValidity.Text == "18")
                {
                    txtMonths.Text = "216";
                }
                else if (txtValidity.Text == "19")
                {
                    txtMonths.Text = "228";
                }
                else if (txtValidity.Text == "20")
                {
                    txtMonths.Text = "240";
                }
            }
            if (cmbValidity.SelectedItem == "Month")
            {
                if (txtValidity.Text == "1")
                {
                    txtMonths.Text = "1";
                }
                else if (txtValidity.Text == "2")
                {
                    txtMonths.Text = "2";
                }
                else if (txtValidity.Text == "3")
                {
                    txtMonths.Text = "3";
                }
                if (txtValidity.Text == "4")
                {
                    txtMonths.Text = "4";
                }
                else if (txtValidity.Text == "5")
                {
                    txtMonths.Text = "5";
                }
                else if (txtValidity.Text == "6")
                {
                    txtMonths.Text = "6";
                }
                if (txtValidity.Text == "7")
                {
                    txtMonths.Text = "7";
                }
                else if (txtValidity.Text == "8")
                {
                    txtMonths.Text = "8";
                }
                else if (txtValidity.Text == "9")
                {
                    txtMonths.Text = "9";
                }
                if (txtValidity.Text == "10")
                {
                    txtMonths.Text = "10";
                }
                else if (txtValidity.Text == "11")
                {
                    txtMonths.Text = "11";
                }
            }
        }

        public bool Insurance_Validation()
        {
            bool result = false;
            if(txtInsuranceAmt.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Insurance Amount", "Green Future Glob", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if(cmbBankIntegration.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Bank Name", "Green Future Glob", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if(dtpDate.Text == "")
            {
                result = true;
                MessageBox.Show("Please Select Date", "Green Future Glob", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if((txtValidity.Text == "") || (cmbValidity.SelectedItem == null))
            {
                result = true;
                MessageBox.Show("Please Select Validity", "Green Future Glob", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            //else if((txtInterval.Text == "") || (cmbInterval.SelectedItem == null))
            //{
            //    result = true;
            //    MessageBox.Show("Please Enetr Interval", "Green Future Glob", MessageBoxButton.OK, MessageBoxImage.Stop);
            //}
            return result;
        }

        public void Cal_InstallmentMonth()
        {
            double insAmt, intMonth, intervalAmt, month, monthAmt;
            try
            {
                insAmt = Convert.ToDouble(txtInsuranceAmt.Text);
                intMonth = Convert.ToDouble(txtInterval.Text);
                month = Convert.ToDouble(txtMonths.Text);
                monthAmt = insAmt / month;
                intervalAmt = monthAmt * intMonth;
                txtIntervalTotalAmt.Text = (Microsoft.VisualBasic.Strings.Format(intervalAmt, "##,###.00"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insurance_FillData()
        {
            try
            {
                con.Open();
                string sqlquery = "SELECT I.[ID],I.[Customer_ID],I.[InsuranceNo],I.[ProductName],I.[InsuranceAmt],I.[BankName],I.[InsuranceDate],I.[NoOfYearMonths],I.[NoOfMonth],I.[YearsMonths],I.[IntervalMonths],I.[IntervalMonthY],I.[IntervalAmt],I.[NewInsuranceDate],I.[FirstPartyInsurance] " +
                                  ",C.[Name],C.[Mobile_No],C.[Email_ID]" +
                                  "FROM [tlb_InsuranceEntry] I " +
                                  "INNER JOIN [tlb_Customer] C ON C.[ID]=I.[Customer_ID] " +
                                  "where I.[ID]='" + txtInsuranceID.Text + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblInsuranceNo.Content = dt.Rows[0]["InsuranceNo"].ToString();
                    lblCustomerName.Content = dt.Rows[0]["Name"].ToString();
                    lblMobileNo.Content = dt.Rows[0]["Mobile_No"].ToString();
                    lblEmailID.Content = dt.Rows[0]["Email_ID"].ToString();
                    lblProductName.Content = dt.Rows[0]["ProductName"].ToString();
                    txtInsuranceAmt.Text = dt.Rows[0]["InsuranceAmt"].ToString();
                    cmbBankIntegration.Text = dt.Rows[0]["BankName"].ToString();
                    dtpDate.SelectedDate = Convert.ToDateTime(dt.Rows[0]["InsuranceDate"].ToString());
                    txtValidity.Text = dt.Rows[0]["NoOfYearMonths"].ToString();
                    cmbValidity.SelectedItem = dt.Rows[0]["YearsMonths"].ToString();
                    txtMonths.Text = dt.Rows[0]["NoOfMonth"].ToString();
                    dtpInstallmentDate.SelectedDate = Convert.ToDateTime(dt.Rows[0]["NewInsuranceDate"].ToString());
                    //chbInsurance.te = dt.Rows[0]["FirstPartyInsurance"].ToString();
                    //cmbAdm_Emp_YearExp.SelectedItem = dt.Rows[0]["NoOfYears"].ToString();
                    //lblYears.Content = dt.Rows[0]["Years"].ToString();
                    //cmbAdm_Emp_Months.SelectedItem = dt.Rows[0]["NoOfMonths"].ToString();
                    //lblMonths.Content = dt.Rows[0]["Months"].ToString();
                    //txtAdm_Emp_Salary.Text = dt.Rows[0]["Salary"].ToString();
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
            btnSave.Content = "Update";
        }
        
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
        #endregion Insurance Function

        private void cmbInterval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Cal_InstallmentMonth();
        }

        private void cmbValidity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectYearMonth();
        }

        private void txtInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        public void CalcDayMonthYear()
        {
            DateTime dayStart;
            DateTime dateEnd;

            dayStart = Convert.ToDateTime(dtpDate.Text);
            dateEnd = Convert.ToDateTime(dtpInstallmentDate.Text);
            TimeSpan ts = dateEnd - dayStart;

            double Years = Convert.ToDouble(ts.TotalDays) / 365;
            double Months = Years * 12;
            double Days = Convert.ToDouble(ts.TotalDays); 

        }

        private void dtpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txtDate.Text = dtpDate.Text;
        }
    }
}
