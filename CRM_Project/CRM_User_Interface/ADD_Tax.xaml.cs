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
    /// Interaction logic for ADD_Tax.xaml
    /// </summary>
    public partial class ADD_Tax : Window
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;

        public ADD_Tax()
        {
            InitializeComponent();
            FetchtaxDetails();
        }
        BAL_Tax baltax = new BAL_Tax();
        DAL_Tax daltax = new DAL_Tax();
        string caption = "GREEN FUTURE GLOB";
        private void btnTaxMain_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object item = dgrd_Tax.SelectedItem;
            string ID = (dgrd_Tax.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            MessageBox.Show(ID);

            con.Open();
            cmd = new SqlCommand("Update tlb_AddTax set S_Status='DeActive' where ID='" + ID + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Do You Want to Delete Record?",caption ,MessageBoxButton.YesNo );

          
        }

        private void btnTax_AddTax_Click(object sender, RoutedEventArgs e)
        {
            if (btnTax_AddTax.Content.ToString ()== "Tax")
            {
                baltax.Flag = 1;
                baltax.Tax_Type = txtTax_TName.Text;
                double price = Convert.ToDouble(txtTax_TPercent.Text);
                baltax.Tax_Percentage = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(price, "##,###.00"));

                baltax.S_Status = "Active";
                baltax.C_Date = System.DateTime.Now.ToShortDateString();
                daltax.Add_TAX_Save(baltax);
                MessageBox.Show("Tax Added Successfully", caption, MessageBoxButton.OK);
                clear();
                FetchtaxDetails();
            }

            else  if (btnTax_AddTax.Content.ToString () == "Update")
                {
                    //Tax_Type ,Tax_Percentage  from tlb_AddTax where ID='" + ID + "'",
                    object item = dgrd_Tax.SelectedItem;
                    string ID = (dgrd_Tax.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    con.Open();
                    cmd = new SqlCommand("Update tlb_AddTax set Tax_Type='"+txtTax_TName .Text +"' ,Tax_Percentage='"+txtTax_TPercent .Text +"' where ID='" + ID + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Data Updated Successfully", caption, MessageBoxButton.OK );
                    clear();
                    FetchtaxDetails();
                    btnTax_AddTax.Content = "Tax";
            }
            
        }
        public void clear()
        {
            txtTax_TName.Text = "";
            txtTax_TPercent.Text = "";
        }
        public void FetchtaxDetails()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("select ID, Tax_Type,Tax_Percentage from tlb_AddTax  where  S_Status='Active'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgrd_Tax.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }


        }

       
        private void btntaxoptions_Click(object sender, RoutedEventArgs e)
        {
            object item = dgrd_Tax.SelectedItem;
            string ID = (dgrd_Tax.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            MessageBox.Show(ID);
            try
            {
                con.Open();


                // DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("Select Tax_Type ,Tax_Percentage  from tlb_AddTax where ID='" + ID + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // DGRD_SaleFollowup.ItemsSource = ds.Tables[0].DefaultView;
                    txtTax_TName.Text = dt.Rows[0]["Tax_Type"].ToString();
                    txtTax_TPercent.Text = dt.Rows[0]["Tax_Percentage"].ToString();

                }
            }
            catch { throw; }
            finally { con.Close();
            btnTax_AddTax.Content = "Update";
            }

        }
    }
}
