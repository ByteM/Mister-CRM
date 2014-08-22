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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient ;


namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["connection1"].ToString());
        SqlCommand cmd;
        SqlDataAdapter adp;
        SqlDataReader dr;
        public MainWindow()
        {
            InitializeComponent();
            fetch();
        }
        public void fetch()
        {
            try
            {
                //cmbcustomercode.Text = "Select";
                con.Open();
                String str2 = "select * from Designation_Master ";
                cmd = new SqlCommand(str2, con);
              DataSet   ds = new DataSet();
                // dt = new DataTable();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                dg1 .ItemsSource = ds.Tables[0].DefaultView;
               // cmbcustomercode.DisplayMemberPath = ds.Tables[0].Columns["CustID"].ToString();
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
