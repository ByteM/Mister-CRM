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
    /// Interaction logic for frmDefaultSMS.xaml
    /// </summary>
    public partial class frmDefaultSMS : Window
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        SqlDataReader dr;
        string caption = "Green Future Glob";

        BALDefaultSMS bdeafultSMS = new BALDefaultSMS();
        DAL_DefaultSMS ddefaultSMS = new DAL_DefaultSMS();

        #region Load Event
        public frmDefaultSMS()
        {
            InitializeComponent();

            Load_Category();
        }
        #endregion Load Event

        #region Button Event
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (DefaultSMS_Validation() == true)
                return;

            if(cmbSelectCategory.Text.Equals("Birthday"))
            {
                try
                {
                    bdeafultSMS.Flag = 1;
                    bdeafultSMS.SelectCategory = cmbSelectCategory.Text;
                    bdeafultSMS.DefaultSMSDate = dtpDate.Text;
                    bdeafultSMS.DefaultMessage = txtMessage.Text;
                    bdeafultSMS.S_Status = "Active";
                    bdeafultSMS.C_Date = System.DateTime.Now.ToShortDateString();
                    ddefaultSMS.DefaultSMS_Insert_Update_Delete(bdeafultSMS);
                    MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetText();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
            else if(cmbSelectCategory.Text.Equals("Balance"))
            {
                try
                {
                    bdeafultSMS.Flag = 1;
                    bdeafultSMS.SelectCategory = cmbSelectCategory.Text;
                    bdeafultSMS.DefaultSMSDate = dtpDate.Text;
                    bdeafultSMS.DefaultMessage = txtMessage.Text;
                    bdeafultSMS.S_Status = "Active";
                    bdeafultSMS.C_Date = System.DateTime.Now.ToShortDateString();
                    ddefaultSMS.BalanceDefaultSMS_Insert_Update_Delete(bdeafultSMS);
                    MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetText();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
            else if(cmbSelectCategory.Text.Equals("Waranty"))
            {
                try
                {
                    bdeafultSMS.Flag = 1;
                    bdeafultSMS.SelectCategory = cmbSelectCategory.Text;
                    bdeafultSMS.DefaultSMSDate = dtpDate.Text;
                    bdeafultSMS.DefaultMessage = txtMessage.Text;
                    bdeafultSMS.S_Status = "Active";
                    bdeafultSMS.C_Date = System.DateTime.Now.ToShortDateString();
                    ddefaultSMS.WarantyDefaultSMS_Insert_Update_Delete(bdeafultSMS);
                    MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetText();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
            else if(cmbSelectCategory.Text.Equals("Insurance"))
            {
                try
                {
                    bdeafultSMS.Flag = 1;
                    bdeafultSMS.SelectCategory = cmbSelectCategory.Text;
                    bdeafultSMS.DefaultSMSDate = dtpDate.Text;
                    bdeafultSMS.DefaultMessage = txtMessage.Text;
                    bdeafultSMS.S_Status = "Active";
                    bdeafultSMS.C_Date = System.DateTime.Now.ToShortDateString();
                    ddefaultSMS.InsuranceDefaultSMS_Insert_Update_Delete(bdeafultSMS);
                    MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetText();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
            else if(cmbSelectCategory.Text.Equals("Dealer Follow-up"))
            {
                try
                {
                    bdeafultSMS.Flag = 1;
                    bdeafultSMS.SelectCategory = cmbSelectCategory.Text;
                    bdeafultSMS.DefaultSMSDate = dtpDate.Text;
                    bdeafultSMS.DefaultMessage = txtMessage.Text;
                    bdeafultSMS.S_Status = "Active";
                    bdeafultSMS.C_Date = System.DateTime.Now.ToShortDateString();
                    ddefaultSMS.DealerFollowupDefaultSMS_Insert_Update_Delete(bdeafultSMS);
                    MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetText();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
            else if(cmbSelectCategory.Text.Equals("Customer Follow-up"))
            {
                try
                {
                    bdeafultSMS.Flag = 1;
                    bdeafultSMS.SelectCategory = cmbSelectCategory.Text;
                    bdeafultSMS.DefaultSMSDate = dtpDate.Text;
                    bdeafultSMS.DefaultMessage = txtMessage.Text;
                    bdeafultSMS.S_Status = "Active";
                    bdeafultSMS.C_Date = System.DateTime.Now.ToShortDateString();
                    ddefaultSMS.CustomerFollowupDefaultSMS_Insert_Update_Delete(bdeafultSMS);
                    MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetText();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetText();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion Button Event

        #region Function
        public void ResetText()
        {
            cmbSelectCategory.SelectedItem = null;
            dtpDate.Text = "";
            txtMessage.Text = "";
        }

        public bool DefaultSMS_Validation()
        {
            bool result = false;
            if(cmbSelectCategory.Text == "Select")
            {
                result = true;
                MessageBox.Show("Please Select Message Category", "Green Furture Glob", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(dtpDate.Text == "")
            {
                result = true;
                MessageBox.Show("Please Select Date", "Green Future Glob", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(txtMessage.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Default Message", "Green Future Glob", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return result;
        }

        public void Load_Category()
        {
            cmbSelectCategory.Text = "Select";
            cmbSelectCategory.Items.Add("Birthday");
            cmbSelectCategory.Items.Add("Balance");
            cmbSelectCategory.Items.Add("Waranty");
            cmbSelectCategory.Items.Add("Insurance");
            cmbSelectCategory.Items.Add("Dealer Follow-up");
            cmbSelectCategory.Items.Add("Customer Follow-up");
        }
        #endregion Function
    }
}