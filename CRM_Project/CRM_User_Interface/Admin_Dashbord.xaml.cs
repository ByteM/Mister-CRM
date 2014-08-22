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
using CRM_User_Interface.Add_Product;


namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for Admin_Dashbord.xaml
    /// </summary>
    public partial class Admin_Dashbord : Window
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        SqlDataAdapter adp;
        SqlDataReader dr;
        BAL_AddProduct baproduct = new BAL_AddProduct();
        DAL_AddProduct daproduct = new DAL_AddProduct();

        public Admin_Dashbord()
        {
            InitializeComponent();
            fetchpid();
        }
        public void fetchpid()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from ADD_Product", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            //txtpid .Text = "PRODUCT/" + id1.ToString();
            con.Close();


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saddproduct_Click(object sender, RoutedEventArgs e)
        {
            //AddProductgrd.Visibility = Visibility;
        }

        private void btnexit_Click(object sender, RoutedEventArgs e)
        {
            //AddProductgrd.Visibility = System.Windows.Visibility.Hidden;
            
        }

        private void addptype_Click(object sender, RoutedEventArgs e)
        {
            //addptypegrd.Visibility = Visibility;
        }

        private void btnpexit_Click(object sender, RoutedEventArgs e)
        {
            //addptypegrd.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnthankexit_Click(object sender, RoutedEventArgs e)
        {
            Addthankgrd .Visibility = System.Windows.Visibility.Hidden;
        }

        private void sthank_Click(object sender, RoutedEventArgs e)
        {
            Addthankgrd.Visibility = Visibility;
        }

        private void btnfollowupexit_Click(object sender, RoutedEventArgs e)
        {
            AddtFollowupgrd.Visibility = System.Windows.Visibility.Hidden;
        }

        private void sfollow_Click(object sender, RoutedEventArgs e)
        {
            AddtFollowupgrd.Visibility = Visibility;
        }

        private void btnbirthexit_Click(object sender, RoutedEventArgs e)
        {
            Addbirthgrd .Visibility = System.Windows.Visibility.Hidden;
        }

        private void sbirth_Click(object sender, RoutedEventArgs e)
        {
            Addbirthgrd.Visibility = Visibility;
        }

        private void btnempexit_Click(object sender, RoutedEventArgs e)
        {
            Addemployeegrd.Visibility = Visibility.Hidden;
        }

        private void addemp_Click(object sender, RoutedEventArgs e)
        {
            Addemployeegrd.Visibility = Visibility;
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            baproduct.Flag = 1;
           // baproduct.Product_ID  = txtpid .Text;
           // baproduct.Product_Name  = txtpname .Text;
            baproduct.S_Status = "Active";
            baproduct.C_Date = System.DateTime.Now.ToShortDateString();
            daproduct.AddDomain_Insert_Update_Delete(baproduct);
            addPorductcleaar();
        }
        public void addPorductcleaar()
        {// txtpid.Text = "";
        //txtpname.Text = "";
        fetchpid();
        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            addPorductcleaar();
        }

        private void btnpclear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnpsave_Click(object sender, RoutedEventArgs e)
        {

        }
        public void producttypeclear()
        {
          //  txtpid.Text = "";
          //  txtpname.Text = "";
           // txtprice.Text = "";
             //txtptname .Text = "";
           // txt

        }

        private void smprocurement_Click(object sender, RoutedEventArgs e)
        {
            newprocurementgrd.Visibility = Visibility;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            Add_Product.Add_Products ad = new Add_Product.Add_Products();
            ad.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
