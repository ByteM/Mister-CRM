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
using System.Globalization;
using System.ComponentModel;
using CRM_User_Interface.Add_Product;
using System.Windows.Controls.DataVisualization.Charting;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Win32;
using CRM_BAL;
using CRM_DAL;


namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for CRM_MainForm.xaml
    /// </summary>
    /// 
      
    public partial class CRM_MainForm : Window
    {
        string asd = System.DateTime.Now.ToString ();
        string CommonDate = System.DateTime.Now.ToShortDateString();
        DateTime azx =Convert .ToDateTime ( System.DateTime.Now.ToShortDateString());

        NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
        string caption = "Green Future Glob" ;
        int cid,I,ID,i;
        double y1,m1,o,p,availqty;
        string yarvalue, year, month, g, pm_c, pm_ch, pm_f, pm_ins, monthvalue,occu, dob ;
        public Button targetButton;
       
        public CRM_MainForm()
        {
            InitializeComponent();
           
          DateTime  s =Convert .ToDateTime ( System.DateTime.Now.ToShortDateString());
          Birthday_Notification();
          Chart_Followup();
          Chart_Seals();
          Chart_Procurment();
          Chart_CustomerBase();
          Chart_HighestSingleProduct();
          Chart_HighestProduct();
          Chart_BestEnquerySource();

          LoadColumnChart_FollowUp();

            //Load_Domain();
            checkedStuff = new List<string>();
            PREPROCUREMENTid();
            
           
        }
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        SqlDataReader dr;
        BAL_AddProduct baddprd = new BAL_AddProduct();
        DAL_AddProduct dalprd = new DAL_AddProduct();
        string maincked,CName,soe;
        string bpg, cid1;
        int fetcdoc, Cust_id;
        int exist,vsoe;
        List<string> checkedStuff;
        static DataTable dtstat = new DataTable();
        double MA;


        BAL_Pre_Procurement bpreproc = new BAL_Pre_Procurement();
        DAL_Pre_Procurement dpreproc = new DAL_Pre_Procurement();

        BAL_Followup balfollow = new BAL_Followup();
        DAL_Followup dalfollow = new DAL_Followup();

        BAL_Customer balc = new BAL_Customer();
        DAL_Customer dalc = new DAL_Customer();

        BAL_InvoiceDetails binvd = new BAL_InvoiceDetails();
        DAL_InvoiceDetails dinvd = new DAL_InvoiceDetails();

        BAL_PaymentModes balpm = new BAL_PaymentModes();
        DAL_PaymentMode dalpm = new DAL_PaymentMode();

        BAL_Installment bins = new BAL_Installment();
        DAL_Installment dins = new DAL_Installment();

        BAL_C_Installment bcins = new BAL_C_Installment();
        DAL_C_Installment dcins = new DAL_C_Installment();
        public void PREPROCUREMENTid()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from Pre_Procurement", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            lblPro_no.Content  = "# Pre_Proc/" + id1.ToString();
            con.Close();


        }

        public void FolloupID_fetch()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from tlb_FollowUp", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            lblwalkin .Content = "# Followup/" + id1.ToString();
            con.Close();


        }
        
        public void CustomerID_fetch()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from tlb_Customer", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            txtvalueid.Text  = "# Customer/" + id1.ToString();
            con.Close();


        }
        public void BillID_fetch()
        {
           
            int id1 = 0;
            
           con.Open();
            SqlCommand cmd = new SqlCommand("Select (COUNT(ID)) from tlb_Bill_No", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;

            lblbillno .Content   = "Bill No/" + id1.ToString();


           // txtvalueid.Text = "Bill No 786/ " + id1.ToString();
          //  txtvalueid.Text = "Bill No 786/ " + id1.ToString();

            con.Close();

        }
        private void btnadminlogin_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminLogin al = new AdminLogin();
            al.Show();
        }

        private void btnmainexit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Main_Login mn = new Main_Login();
        }

        private void btnP_Exit_Click(object sender, RoutedEventArgs e)
        {
            clearAllADDProducts();
            grd_U_AddProducts.Visibility = Visibility.Hidden;
            

        }

        private void smaddproduct_Click(object sender, RoutedEventArgs e)
        {

            grd_U_AddProducts. Visibility = Visibility;
        }

        private void btnP_AddDomain_Click(object sender, RoutedEventArgs e)
        {
            grd_Domain.Visibility = Visibility;
        }

        private void btndomainexit_Click(object sender, RoutedEventArgs e)
        {
            grd_Domain.Visibility = Visibility.Hidden ;
        }

        private void btnProduct_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_Product.Visibility = Visibility.Hidden; 
        }

        private void btnP_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBrandExit_Click(object sender, RoutedEventArgs e)
        {
            grd_Brand.Visibility = Visibility.Hidden; 
        }

        private void btnP_AddBrand_Click(object sender, RoutedEventArgs e)
        {
           
            grd_Brand.Visibility = Visibility;
            Load_DomainB();

        }

        private void btnPCategoryExit_Click(object sender, RoutedEventArgs e)
        {
            grd_ProductCategory.Visibility = Visibility.Hidden; 
            
        }

        private void btnP_AddPCategory_Click(object sender, RoutedEventArgs e)
        {
            grd_ProductCategory.Visibility = Visibility;
            Load_PCDomain();
        }

        private void btnmodelnoexie_Click(object sender, RoutedEventArgs e)
        {
            grd_ModelNo.Visibility = Visibility.Hidden;  
        }

        private void btnP_AddModelNo_Click(object sender, RoutedEventArgs e)
        {
            grd_ModelNo.Visibility = Visibility;
            Load_MDomain();
        }

        private void btnColorExit_Click(object sender, RoutedEventArgs e)
        {
            grd_Color.Visibility = Visibility.Hidden; 
        }

      
        private void btnP_AddColor1_Click(object sender, RoutedEventArgs e)
        {
            grd_Color.Visibility = Visibility;
        }

        private void btnP_AddColor1_Click_1(object sender, RoutedEventArgs e)
        {
            grd_Color.Visibility = Visibility;
            Load_CDomain();
        }

        private void btndomainsave_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    string strpan, stradhar, strpass, straddress, strseventw, strfrm16, strdelerlic, strnoidpf, strnodoc, strcmpid;
                    baddprd.Flag = 1;
                    baddprd.Domain_Name = txtdomain.Text;
                    if(chkpancard.IsChecked ==true )
                    {
                        strpan = "Yes";
                    }
                    else
                    {
                        strpan = "No";
                    }
                    if(chkadharcard.IsChecked == true)
                    {
                        stradhar = "Yes";
                    }
                    else
                    {
                        stradhar = "No";
                    }
                    if(chkPassport.IsChecked == true)
                    {
                        strpass = "Yes";
                    }
                    else
                    {
                        strpass = "No";
                    }
                    if(chkaddress.IsChecked == true)
                    {
                        straddress = "Yes";
                    }
                    else
                    {
                        straddress = "No";
                    }
                    if(chkseventwelve.IsChecked == true)
                    {
                        strseventw = "Yes";
                    }
                    else
                    {
                        strseventw = "No";
                    }
                    if(chkform16.IsChecked == true)
                    {
                        strfrm16 = "Yes";
                    }
                    else
                    {
                        strfrm16 = "No";
                    }
                    if(chkdealerlisence.IsChecked == true)
                    {
                        strdelerlic = "Yes";
                    }
                    else
                    {
                        strdelerlic = "No";
                    }
                    if(chkotherid.IsChecked == true)
                    {
                        strnoidpf  = "Yes";
                    }
                    else
                    {
                        strnoidpf = "No";
                    }
                    if (chknodocument .IsChecked ==true )
                    {
                        strnodoc = "Yes";
                    }
                    else { strnodoc = "No"; }
                    if(chkcidproof.IsChecked == true)
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
                    MessageBox.Show ("Data Save Successfully");
                    txtdomain.Text = "";
                    Load_Domain();
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
                    cmbP_domain.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
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
                    cmb_DomainProduct .ItemsSource = ds.Tables[0].DefaultView;
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
        public void Fetch_Product()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Domain_ID,Product_Name from tlb_Products where  Domain_ID='" + cmbP_domain .SelectedValue.GetHashCode() + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_Product.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbP_Product .ItemsSource = ds.Tables[0].DefaultView;
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
                    cmbProductBrand .ItemsSource = ds.Tables[0].DefaultView;
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
                    cmbDomainBrand .ItemsSource = ds.Tables[0].DefaultView;
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
        public void fetch_Brand()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Brand_Name from tlb_Brand where Product_ID='"+cmbP_Product .SelectedValue .GetHashCode ()+"'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_Brand.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbP_Brand .ItemsSource = ds.Tables[0].DefaultView;
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
        private void grdMainFormGrid_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void grd_U_AddProducts_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Domain();
            //Load_DomainP();
            //fetch_Brand();
            //Fetch_PC();
            //fetch_Model();
            //fetch_Color();
        }

        private void btnProductSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                baddprd.Flag = 1;
                baddprd.Domain_ID =Convert .ToInt32 ( cmb_DomainProduct.SelectedValue.GetHashCode());
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

        private void btnadminl_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin adl = new AdminLogin();
            adl.Show();
        }

        private void btnP_AddProduct_Click_1(object sender, RoutedEventArgs e)
        {
            grd_Product.Visibility = Visibility;
            Load_DomainP();
        }

        private void btnBrandSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                baddprd.Flag = 1;
                baddprd.Product_ID  = Convert.ToInt32(cmbProductBrand .SelectedValue.GetHashCode());
                baddprd.Brand_Name  = txtBrand.Text;
                baddprd.S_Status = "Active";
                baddprd.C_Date = System.DateTime.Now.ToShortDateString();
                dalprd.AddBrand_Insert_Update_Delete (baddprd);
                MessageBox.Show("Data Save Successfully");
                txtBrand .Text = "";
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

        private void cmbDomainBrand_DropDownClosed(object sender, EventArgs e)
        {
           // Load_BrandProduct();
           // Load_DomainB();
        }

        private void btnPCategorySave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                baddprd.Flag = 1;
                baddprd.Brand_ID = Convert.ToInt32(cmbBrandPCategory .SelectedValue.GetHashCode());
                baddprd.Product_Category  = txtPCategoy .Text;
                baddprd.S_Status = "Active";
                baddprd.C_Date = System.DateTime.Now.ToShortDateString();
                dalprd.AddP_Category_Insert_Update_Delete (baddprd);
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
                    cmbDomainPCategory.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbDomainPCategory .ItemsSource = ds.Tables[0].DefaultView;
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
                    cmbProductPCategoryy .SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
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
                    cmbBrandPCategory .SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
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
        public void Fetch_PC()
        {

            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT  ID,Product_Category from tlb_P_Category where Brand_ID='"+cmbP_Brand .SelectedValue .GetHashCode ()+"' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_PCategory.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbP_PCategory .ItemsSource = ds.Tables[0].DefaultView;
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

        private void cmbDomainPCategory_DropDownClosed(object sender, EventArgs e)
        {
           
        }

        private void cmbProductPCategoryy_DropDownClosed(object sender, EventArgs e)
        {
            
        }

        private void btnModelNoSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                baddprd.Flag = 1;
                baddprd.P_Category   = Convert.ToInt32(cmbPCategoryModelno.SelectedValue.GetHashCode());
                baddprd.Model_No  = txtmodelno.Text;
                baddprd.S_Status = "Active";
                baddprd.C_Date = System.DateTime.Now.ToShortDateString();
                dalprd. AddModel_Insert_Update_Delete(baddprd);
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
                    cmbDomainModelno.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbDomainModelno .ItemsSource = ds.Tables[0].DefaultView;
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
        public void Load_MProduct()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Domain_ID, Product_Name from tlb_Products where Domain_ID='" + cmbDomainModelno.SelectedValue .GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbProductModelno.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbProductModelno .ItemsSource = ds.Tables[0].DefaultView;
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
                    cmbBrandModelno.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbBrandModelno .ItemsSource = ds.Tables[0].DefaultView;
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
                cmd = new SqlCommand("Select DISTINCT ID,Brand_ID, Product_Category from tlb_P_Category where Brand_ID='" + cmbBrandModelno.SelectedValue .GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPCategoryModelno.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbPCategoryModelno .ItemsSource = ds.Tables[0].DefaultView;
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
        public void fetch_Model()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Model_No from tlb_Model where P_Category='"+cmbP_PCategory .SelectedValue .GetHashCode()+"' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_ModelNo.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbP_ModelNo  .ItemsSource = ds.Tables[0].DefaultView;
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

        private void btnColorSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                baddprd.Flag = 1;
                baddprd.Model_No_ID  = Convert.ToInt32(cmbModelColor.SelectedValue.GetHashCode());
                baddprd.Color  = txtcolor.Text;
                baddprd.S_Status = "Active";
                baddprd.C_Date = System.DateTime.Now.ToShortDateString();
                dalprd.AddColor_Insert_Update_Delete (baddprd);
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
                    cmbDomainColor.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbDomainColor .ItemsSource = ds.Tables[0].DefaultView;
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
        public void Load_CProduct()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID,Domain_ID, Product_Name from tlb_Products where Domain_ID='" + cmbDomainColor.SelectedValue .GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbProductColor.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbProductColor .ItemsSource = ds.Tables[0].DefaultView;
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
                    cmbBrandColor .ItemsSource = ds.Tables[0].DefaultView;
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
                cmd = new SqlCommand("Select DISTINCT ID,Brand_ID, Product_Category from tlb_P_Category where Brand_ID='" + cmbBrandColor.SelectedValue .GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPCategoryColor.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbPCategoryColor .ItemsSource = ds.Tables[0].DefaultView;
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
                cmd = new SqlCommand("Select DISTINCT ID,P_Category, Model_No from tlb_Model where P_Category='" + cmbPCategoryColor.SelectedValue .GetHashCode() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbModelColor.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
                    cmbModelColor .ItemsSource = ds.Tables[0].DefaultView;
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
        public void fetch_Color()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Color from tlb_Color where Model_No_ID='"+cmbP_ModelNo .SelectedValue .GetHashCode()+"'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbP_Color.SelectedValuePath  = ds.Tables[0].Columns["ID"].ToString();
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

        private void cmbDomainColor_DropDownClosed(object sender, EventArgs e)
        {
                    }

        private void cmbProductColor_DropDownClosed(object sender, EventArgs e)
        {
            Load_CBrand();
        }

        private void cmbBrandColor_DropDownClosed(object sender, EventArgs e)
        {
            Load_CPC();
        }

        private void cmbPCategoryColor_DropDownClosed(object sender, EventArgs e)
        {
            Load_CModel();
        }

        private void cmbDomainModelno_DropDownClosed(object sender, EventArgs e)
        {
                    }

        private void cmbProductModelno_DropDownClosed(object sender, EventArgs e)
        {
                   }

        private void cmbBrandModelno_DropDownClosed(object sender, EventArgs e)
        {
                   }
        //Main Save 
        private void btnP_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                baddprd.Flag = 1;
                baddprd.Domain_ID  = Convert.ToInt32 (cmbP_domain.SelectedValue .GetHashCode ());
                baddprd.Product_ID = Convert.ToInt32(cmbP_Product.SelectedValue.GetHashCode());
                baddprd.Brand_ID = Convert.ToInt32(cmbP_Brand.SelectedValue.GetHashCode());
                baddprd.P_Category = Convert.ToInt32(cmbP_PCategory.SelectedValue.GetHashCode());
                baddprd.Model_No_ID = Convert.ToInt32(cmbP_ModelNo.SelectedValue.GetHashCode());
                baddprd.Color_ID = Convert.ToInt32(cmbP_Color.SelectedValue.GetHashCode());
                baddprd.Narration = txtP_Narration.Text;
                baddprd.Price =Convert .ToDouble ( txtP_Price.Text);
                baddprd.S_Status = "Active";
                baddprd.C_Date =System.DateTime.Now.ToShortDateString();
                dalprd.Save_Insert_Update_Delete(baddprd);
                MessageBox.Show("Data Save Successfully");
                txtP_Narration .Text = "";
                txtP_Price.Text = "";
                clearAllADDProducts();
               // Load_Domain();
               
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void cmbP_domain_DropDownClosed(object sender, EventArgs e)
        {
            
        }

        private void cmbP_Product_DropDownClosed(object sender, EventArgs e)
        {
            
        }

        private void cmbP_Brand_DropDownClosed(object sender, EventArgs e)
        {
            Fetch_PC();
        }

        private void cmbP_PCategory_DropDownClosed(object sender, EventArgs e)
        {
           

        }

        private void cmbP_ModelNo_DropDownClosed(object sender, EventArgs e)
        {
            
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
        public void SetWarrantyYM()
        {
            cmbPreWarrantyYM.Text = "---Select---";
            cmbPreWarrantyYM.Items.Add("Month");
            cmbPreWarrantyYM.Items.Add("Year");
        }
        private void smnewprocurement_Click(object sender, RoutedEventArgs e)
        {
            GRD_NewProcurement.Visibility = Visibility;
           // load_DSelect();
            load_DSelect();
            Fetch_Pre_Domain();
            load_Insurance();
            load_Followup();
            FetchDealarname();
            SetWarrantyYM(); 
          

        }

        private void cmbDomainBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbProductBrand.SelectedValue = null;
            Load_BrandProduct();

        }

        private void cmbDomainPCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbBrandPCategory.SelectedValue = null;
            cmbPrePCategory.SelectedValue = null;
            Load_PCProduct();
        }

        private void cmbProductPCategoryy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load_PCBrand();
        }

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

        private void cmbPCategoryModelno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

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
            
            //fetch_Model();
           
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

        private void btnPro_Exit_Click(object sender, RoutedEventArgs e)
        {
            GRD_NewProcurement.Visibility = Visibility.Hidden;
            //clearallPreProcurement();
        }
        //=============== new pre procuremnet=================
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cbox = sender as CheckBox;
            string s = cbox.Content as string;

            if ((bool)cbox.IsChecked)
                checkedStuff.Add(s);
            else
                checkedStuff.Remove(s);
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
                if(ds.Tables[0].Rows .Count >0)
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
        public bool PrePro_Validation()
        {
            bool result = false;
            if (cmbPre_Pro_Salename.SelectedItem == null)
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
            else if (txtPrice.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Price", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (txtQuantity.Text == "")
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

                    bpreproc.Procurment_Price = Convert.ToDouble(txtPrice.Text);
                    bpreproc.Quantity = Convert.ToDouble(txtQuantity.Text);
                    bpreproc.Total_Amount = Convert.ToDouble(txtTotalPrice.Text);
                    bpreproc.Net_Amount = Convert.ToDouble(txtNetAmount.Text);
                    bpreproc.Round_Off = Convert.ToDouble(txtpreroundoff.Text);
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
                    bpreproc.C_Date = System.DateTime.Now.ToShortDateString();
                    dpreproc.Pre_Procurement_Save_Insert_Update_Delete(bpreproc);
                    MessageBox.Show("Data Save Successfully");
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
        public void fetch_Documents()
        {

            try
            { 
                con.Open();
             
                cmd = new SqlCommand("Select PAN_Card,Adhar_Card,Passport,Address_Proof,Seven_Twevel,Form_16,Dealer_Lisence,Other_ID_Proof,No_Documents,Cmp_ID_Proof  from tb_Domain where ID='" + cmbPreDomain.SelectedValue.GetHashCode() + "' ", con);
              
                SqlDataReader dr = cmd.ExecuteReader();
             
                while(dr.Read ())
             
                {
                    string p = dr["PAN_Card"].ToString ();
                    string ad = dr["Adhar_Card"].ToString();
                    string pa = dr["Passport"].ToString();
                    string addr = dr["Address_Proof"].ToString();
                    string st = dr["Seven_Twevel"].ToString();
                    string frm = dr["Form_16"].ToString();
                    string dl = dr["Dealer_Lisence"].ToString();
                    string oidp = dr["Other_ID_Proof"].ToString();
                    string nod = dr["No_Documents"].ToString();
                    string cmpid = dr["Cmp_ID_Proof"].ToString();
                 if(p=="Yes")
                    {
                        chkPANCARD.IsEnabled = true;
                        //chkPANCARD.IsChecked = true;
                    }
                 if (pa == "Yes")
                 {
                     chkPASSPORT .IsEnabled = true;
                 }
                 if (ad == "Yes")
                 {
                     CHKADHARC .IsEnabled = true;
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
                     chkform_16 .IsEnabled = true;
                 }
                 if (dl == "Yes")
                 {
                     chkDEALERL .IsEnabled = true;
                 }
                 if (oidp == "Yes")
                 {
                     chkOTHERID .IsEnabled = true;
                 }
                 if (nod == "Yes")
                 {
                     chkNODOCS .IsEnabled = true;
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
                    cmbPreDomain .SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
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
        public void Fetch_Pre_Product()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("Select DISTINCT ID, Product_Name from tlb_Products where Domain_ID='"+cmbPreDomain .SelectedValue .GetHashCode ()+"' ", con);
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
                cmd = new SqlCommand("Select DISTINCT ID, Brand_Name from tlb_Brand where Product_ID='"+cmbPreProduct .SelectedValue .GetHashCode ()+"' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPreBrand .SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
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
                cmd = new SqlCommand("Select DISTINCT  ID,Product_Category from tlb_P_Category where Brand_ID='"+cmbPreBrand .SelectedValue .GetHashCode ()+"'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPrePCategory .SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
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
                cmd = new SqlCommand("Select DISTINCT ID, Model_No from tlb_Model where P_Category='"+cmbPrePCategory .SelectedValue .GetHashCode ()+"' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbPreModel .SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
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
                cmd = new SqlCommand("Select DISTINCT ID, Color from tlb_Color where Model_No_ID='"+cmbPreModel .SelectedValue .GetHashCode()+"' ", con);
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
        public void load_DSelect()
          { cmbPreDomain.Text = "--Select--";
          cmbPreProduct.Text = "--Select--";
          cmbPreBrand.Text = "--Select--";
          cmbPrePCategory.Text = "--Select--";
          cmbPreModel.Text = "--Select--";
          cmd_PreColor.Text = "--Select--";
        }
        private void cmbPrePCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cmbPreBrand.SelectedValue = null;
            cmbPreModel.SelectedValue = null;
            cmd_PreColor.SelectedValue = null;
            fetch_Pre_Model();
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

        private void cmbPreModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmd_PreColor.SelectedValue = null;
            fetch_Pre_Color();
        }

        private void btnP_Clear_Click(object sender, RoutedEventArgs e)
        {
            clearAllADDProducts();
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
           // chkForm16.IsChecked = null;
            chkNODOCS.IsEnabled = false;
            chkPANCARD .IsEnabled = false;
            chkPASSPORT.IsEnabled = false;
            CHKADHARC.IsEnabled = false;
            chkOTHERID.IsEnabled = false;
            chkform_16.IsEnabled = false;
            chkDEALERL.IsEnabled = false;
            chkaddressproof.IsEnabled = false;
            chk7_12.IsEnabled = false;
            chkNODOCS.IsEnabled = false;
            chk7_12.IsChecked = false;
          
            cmbPreInsurance.Items .Clear ();
            cmbPreFollowup.Items.Clear ();
            load_Insurance();
            load_Followup();
            txtPrice.Text = "";
            chkcmpid .IsEnabled = false;
            txtPreWarranty.Text = "";
        }
        private void btnPro_Clear_Click(object sender, RoutedEventArgs e)
        {
            clearallPreProcurement();
        }

        private void cmd_PreColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             con.Open();

             cmd = new SqlCommand("Select  Price from Pre_Products where Color_ID='" + cmd_PreColor.SelectedValue.GetHashCode() + "' ", con);
              
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    txtPrice.Text  =dr["Price"].ToString ();
                }
                con.Close();
        }
        //===================code for customer and followup form===========================

        public void loadSourceofEnq()
        {
            cmbCSourceofEnq.Text = "Select Source of Enquiry";
            cmbCSourceofEnq.Items.Add("Newspaper");
            cmbCSourceofEnq.Items.Add("Friends/Colleagues");
            cmbCSourceofEnq.Items.Add("Net/Website");

        
        }
        private void btnCSave_Click(object sender, RoutedEventArgs e)
        {
            if (rdoCCustom.IsChecked == true)
       {
            AddCustomerFollowup1();
            //Followup_Method();
        }
            else if (rdoCFolloup .IsChecked == true)
        {
        //    AddCustomerWalkin();
                 AddCustomerFollowup2();
        //    Walking_Method();
        }
        }

        private void GRD_Follwup_Loaded(object sender, RoutedEventArgs e)
        {
          //  GRD_Follwupandnew.Visibility = Visibility;
            load_Followup();
           FolloupID_fetch();
           loadSourceofEnq();
             
        }
        public void AddCustomerFollowup1()
        {
            balfollow.Flag = 1;
            balfollow.Followup_ID = lblwalkin .Content.ToString();
            balfollow.Name = txtCName.Text;
            balfollow.Mobile_No = txtCMobile.Text;
            balfollow.Date_Of_Birth = dp_Dob.Text  ;
            balfollow.Email_ID = txtCEmailid.Text;
            balfollow.Address = txtCAddress.Text;
            if(rdoBusiness .IsChecked ==true )
            {
                 bpg="Business";
            }
            else if (rdoCPrivate .IsChecked ==true )
            {
                bpg="Private Employee";
            }
                else if(rdoCgovt .IsChecked==true )
                {
                     bpg="Govt Employee";

                }
           
             balfollow.Occupation =bpg   ;
             balfollow.Product_Details = txtCProductDetails.Text;
             //balfollow.Followup_Walkin_Option = "Followup"; 
            
            //if(rdoCFolloup .IsChecked  ==true )
            //{
         //  balfollow.Followup_Type = "Default";
            //    //balfollow.F_Date = Convert.ToDateTime(dp_Cdate .SelectedDate =null);
            //   // balfollow.F_Message = "";
            //}
             if (rdoCCustom .IsChecked == true )
            {
                balfollow.Followup_Type = "Custom";
                balfollow.F_Date = dp_Cdate.Text;
                balfollow.F_Message = txtCMessage.Text;
            }
            balfollow.Folloup_Update = "F_Active";
            balfollow.S_Status = "Active";
            balfollow.C_Date = System.DateTime.Now.ToShortDateString();
            dalfollow.Follwup1_Save_Insert_Update_Delete(balfollow);
            MessageBox .Show ("Customer Added sucsessfully ",caption , MessageBoxButton.OK);
            clearfunctionforfollowup();

        }
        public void AddCustomerFollowup2()
        {
            balfollow.Flag = 1;
            balfollow.Followup_ID = lblwalkin.Content.ToString();
            balfollow.Name = txtCName.Text;
            balfollow.Mobile_No = txtCMobile.Text;
            balfollow.Date_Of_Birth = dp_Dob.Text;
            balfollow.Email_ID = txtCEmailid.Text;
            balfollow.Address = txtCAddress.Text;
            if (rdoBusiness.IsChecked == true)
            {
                bpg = "Business";
            }
            else if (rdoCPrivate.IsChecked == true)
            {
                bpg = "Private Employee";
            }
            else if (rdoCgovt.IsChecked == true)
            {
                bpg = "Govt Employee";

            }

            balfollow.Occupation = bpg;
            balfollow.Product_Details = txtCProductDetails.Text;
            //balfollow.Followup_Walkin_Option = "Followup"; 

            if (rdoCFolloup.IsChecked == true)
            {
               balfollow.Followup_Type = "Default";
                //balfollow.F_Date = Convert.ToDateTime(dp_Cdate .SelectedDate =null);
                // balfollow.F_Message = "";
            }
            //else if (rdoCCustom.IsChecked == true)
            //{
            //    balfollow.Followup_Type = "Custom";
            //    balfollow.F_Date = dp_Cdate.SelectedDate.Value;
            //    balfollow.F_Message = txtCMessage.Text;
            //}
            balfollow.Folloup_Update = "F_Active";
            balfollow.S_Status = "Active";
            balfollow.C_Date = System.DateTime.Now.ToShortDateString();
            dalfollow.Follwup2_Save_Insert_Update_Delete(balfollow);
            MessageBox.Show("Customer Added sucsessfully ", caption, MessageBoxButton.OK);
            clearfunctionforfollowup();

        }

        private void smaddfolloup_Click(object sender, RoutedEventArgs e)
        {
            GRD_Follwupandnew.Visibility = Visibility;
            FolloupID_fetch();
        }
        //public void AddCustomerWalkin()
        //{
        //    balfollow.Flag = 2;
        //    balfollow.Followup_ID = LBLWALKIN.Content.ToString();
        //    balfollow.Name = txtCName.Text;
        //    balfollow.Mobile_No = txtCMobile.Text;
        //    balfollow.Date_Of_Birth = dp_Dob.SelectedDate.Value;
        //    balfollow.Email_ID = txtCEmailid.Text;
        //    balfollow.Address = txtCAddress.Text;
        //    if (rdoBusiness.IsChecked == true)
        //    {
        //        bpg = "Business";
        //    }
        //    else if (rdoCPrivate.IsChecked == true)
        //    {
        //        bpg = "Private Employee";
        //    }
        //    else if (rdoCgovt.IsChecked == true)
        //    {
        //        bpg = "Govt Employee";

        //    }

        //    balfollow.Occupation = bpg;
        //    balfollow.Followup_Walkin_Option = "Walkin";
        //   // balfollow.Followup_Type = cmbCFollowup.SelectedValue.ToString();
        //    balfollow.Walkins = "New_Walkin";
        //    balfollow.Folloup_Update = "W_IN";
        //    balfollow.S_Status = "Active";
        //    balfollow.C_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
        //    dalfollow.Customer_Walking_Save_Insert_Update_Delete (balfollow);
        //    MessageBox.Show("Customer Added sucsessfully ", caption, MessageBoxButton.OK);
        //}
    //public void Followup_Method()
    //    {
    //        balfollow.Flag = 3;
    //        balfollow.Cust_ID = "";
    //        balfollow.Followup_Type = cmbCFollowup.SelectedValue.ToString();
    //        if (cmbCFollowup.SelectedValue == "Default")
    //        {

    //        }
    //        else if (cmbCFollowup.SelectedValue == "Custom")
    //        {
    //            balfollow.F_Date = dp_Cdate.SelectedDate.Value;
    //            balfollow.F_Message = txtCMessage.Text;
    //        }
    //        balfollow.S_Status = "Active";
    //        balfollow.C_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
    //        dalfollow.Followup_Save_Insert_Update_Delete(balfollow );
    //        MessageBox.Show("Follow_up Added sucsessfully ", caption, MessageBoxButton.OK);
    //    }
    //    public void Walking_Method()
    //{
    //    balfollow.Flag = 3;
    //    balfollow.Cust_ID = "";
    //    balfollow.Walkins = "W_IN";
    //    balfollow.S_Status = "Active";
    //    balfollow.C_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
    //    dalfollow.walkin_Save_Insert_Update_Delete(balfollow);
    //    MessageBox.Show("Walk_in  Added sucsessfully ", caption, MessageBoxButton.OK);
    //}
        public void clearfunctionforfollowup()
        {
            FolloupID_fetch();
            txtCName.Text = "";
            txtCMobile.Text = "";
            txtCAddress.Text = "";
            txtCEmailid.Text  = "";
            txtCMessage.Text = "";
            txtCMobile.Text = "";
            txtCProductDetails.Text = "";
            dp_Dob.SelectedDate = null;
            dp_Cdate.SelectedDate = null;
            dp_Cdate.Visibility = Visibility.Hidden ;
            txtCMessage.Visibility = Visibility.Hidden ;
            rdoCFolloup.IsChecked = false;
            rdoCCustom.IsChecked = false;
            rdoBusiness.IsChecked = false;
            rdoCgovt.IsChecked = false;
            rdoCPrivate.IsChecked = false;
            cmbCSourceofEnq.ItemsSource = null;
            loadSourceofEnq();



        }

        private void btnCClear_Click(object sender, RoutedEventArgs e)
        {
            clearfunctionforfollowup();
        }

        private void rdoCCustom_Checked(object sender, RoutedEventArgs e)
        {
           // dp_Cdate.IsEnabled = true;
           // txtCMessage.IsEnabled = true;
            dp_Cdate.Visibility = Visibility;
            txtCMessage.Visibility = Visibility;
        }

        private void btnCExit_Click(object sender, RoutedEventArgs e)
        {
            GRD_Follwupandnew.Visibility = Visibility.Hidden;
        }

        private void rdoCFolloup_Checked(object sender, RoutedEventArgs e)
        {
            dp_Cdate.Visibility = Visibility.Hidden ;
            txtCMessage.Visibility = Visibility.Hidden ;
            fetch_FollowupDetails();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //===========================end followup code=========================
        private void rdosalefollowupcustomer_Checked(object sender, RoutedEventArgs e)
        {
                clear_CustomerFields();         
                txtsalesearchcname.IsEnabled = true;
                txtSalecustomerno.IsEnabled = true;
                DGRD_SaleFollowup.IsEnabled = true;
                //cmbsalecustomerftype.IsEnabled = true;
                GRD_FollowupCostomer.Visibility = Visibility;
                DGRD_SaleFollowup.Visibility = Visibility;
                //load_Followup_type();
                FetchallDetails();

                grd_OldCustomerDetails.Visibility = System.Windows.Visibility.Hidden;
                GRD_Customer_Billing.Visibility = System.Windows.Visibility.Hidden;
               
           
        }

        private void rdoSaleNewcustomer_Checked(object sender, RoutedEventArgs e)
        {
            clear_CustomerFields();
            DGRD_SaleFollowup.Visibility = System.Windows.Visibility.Hidden;
            grd_OldCustomerDetails.Visibility = System.Windows.Visibility.Hidden;
            GRD_Customer_Billing.Visibility = System.Windows.Visibility.Visible;
            CustomerID_fetch();
            txtSaleCustomerOccupation.IsEnabled = false;
            rdoSaleCustomerBusiness.IsEnabled = true;
            rdoSaleCustomergovt.IsEnabled = true;
            rdoSaleCustomerPrivate.IsEnabled = true;
            btnSaleCustomerEditoccu.IsEnabled = false;

        }
       
        public void FetchallDetails()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("select ID, Followup_ID,Name,Mobile_No,Date_Of_Birth,Email_ID,Address,Product_Details,Followup_Type,F_Date,C_Date from tlb_FollowUp  where  S_Status='Active'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DGRD_SaleFollowup.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
          

        }
        
        public void fetch_FollowupDetails()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("select ID, Followup_ID,Name,Mobile_No,Date_Of_Birth,Email_ID,Address,Product_Details,Followup_Type,F_Date,C_Date from tlb_FollowUp  where Name LIKE ISNULL ('" + txtsalesearchcname.Text + "',Name) + '%'  and S_Status='Active' ORDER BY Name ASC ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DGRD_SaleFollowup.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            finally { con.Close(); }
          

        }
       
        public void fetch_FollowupDetailsbymobile()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("select ID, Followup_ID,Name,Mobile_No,Date_Of_Birth,Email_ID,Address,Product_Details,Followup_Type,F_Date,C_Date from tlb_FollowUp  where Mobile_No LIKE ISNULL ('" + txtSalecustomerno.Text + "',Mobile_No) + '%'  and S_Status='Active' ORDER BY Name ASC ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DGRD_SaleFollowup.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }


        }
       
        public void loadbyallfield_Followup()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string strf = "select ID, Followup_ID , Name , Mobile_No , Date_Of_Birth , Email_ID , Address , Product_Details , Followup_Type , F_Date , C_Date " +
                    "from tlb_FollowUp " +
                    "where  ";
                if (txtsalesearchcname.Text.Trim() != "")
                {
                    strf = strf + " Name LIKE ISNULL('" + txtsalesearchcname.Text.Trim() + "',[Name]) + '%' AND ";
                }
                if (txtSalecustomerno.Text.Trim() != "")
                {
                    strf = strf + " Mobile_No LIKE ISNULL('" + txtSalecustomerno.Text.Trim() + "',[Mobile_No]) + '%' AND ";
                }
                strf = strf + " S_Status = 'Active' ORDER BY [Name] ASC ";
                cmd = new SqlCommand(strf, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                // if (ds.Tables[0].Rows.Count > 0)
                // {
                DGRD_SaleFollowup.ItemsSource = ds.Tables[0].DefaultView;
                // }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }



        }
            
      
        public  void txtsalesearchcname_TextChanged(object sender, TextChangedEventArgs e)
        {
         
            
            loadbyallfield_Followup();
         
        }

        private void txtSalecustomerno_TextChanged(object sender, TextChangedEventArgs e)
        {
            
               loadbyallfield_Followup();
         

        }

        private void cmbsalecustomerftype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void txtsalesearchcname_KeyDown(object sender, KeyEventArgs e)
        {
            //loadbyallfield_Followup();
        }

        private void DGRD_SaleFollowup_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
        }

        private void btnsaleExit_Click(object sender, RoutedEventArgs e)
        {
            GRD_Sales.Visibility =Visibility .Hidden ;
        }

        private void rdoSaleOldCustomer_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void rdoSaleOldCustomer(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaleCustomerEditoccu_Click(object sender, RoutedEventArgs e)
        {
            rdoSaleCustomerBusiness.IsEnabled = true;
            rdoSaleCustomergovt.IsEnabled = true;
            rdoSaleCustomerPrivate.IsEnabled = true;

        }

        private void smnewwalkin_Click(object sender, RoutedEventArgs e)
        {
            GRD_Sales.Visibility = System.Windows.Visibility.Visible;
            FetchallDetails();
           
        }

        private void rdoSaleOldCustomer1_Checked(object sender, RoutedEventArgs e)
        {
            clear_CustomerFields();
           // txtsalesearchcname.IsEnabled = false;
           // txtSalecustomerno.IsEnabled = false;
           // DGRD_SaleFollowup.IsEnabled = false;
           // cmbsalecustomerftype.IsEnabled = false;
           // grd_OldCustomerDetails.Visibility = Visibility;
           
            //load_Followup_type();
            GRD_FollowupCostomer.Visibility = Visibility.Hidden;
           
            grd_OldCustomerDetails.Visibility =Visibility;
            OldCustomer_Details();
         
            GRD_Customer_Billing.Visibility = System.Windows.Visibility.Hidden;
          
        }

        private void btnSaleCustomerEdit_Click(object sender, RoutedEventArgs e)
        {
            btnSaleCustomerEditoccu.IsEnabled = true;
            btnSaleCustomerUpdate.IsEnabled = true;
            btnSaleCustomerDelete.IsEnabled = true;
        }

        private void btnSaleCustomerBack_Click(object sender, RoutedEventArgs e)
        {
            GRD_Customer_Billing.Visibility = Visibility.Hidden;
            DGRD_SaleFollowup.Visibility = Visibility;
        

        }

        private void DGRD_SaleFollowup_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            object item = DGRD_SaleFollowup.SelectedItem;
            string ID = (DGRD_SaleFollowup.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            MessageBox.Show(ID);
            // DGRD_SaleFollowup;
            GRD_Customer_Billing.Visibility = Visibility;
            CustomerID_fetch();

            //txtvalueid.Text = ID;
            lblfollowupidfetch.Content = ID;


            try
            {
                con.Open();
               

                // DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("Select Name ,Mobile_No,Date_Of_Birth,Email_ID,Address,Occupation from tlb_FollowUp where ID='" + ID + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // DGRD_SaleFollowup.ItemsSource = ds.Tables[0].DefaultView;
                    txtSalecustomerName.Text = dt.Rows[0]["Name"].ToString();
                    txtSaleCustomerMobileno.Text = dt.Rows[0]["Mobile_No"].ToString();
                    dpSaleCustomerDOB.Text = dt.Rows[0]["Date_Of_Birth"].ToString();
                    txtSaleCustomerEmailID.Text = dt.Rows[0]["Email_ID"].ToString();
                    txtSaleCustomerAddress.Text = dt.Rows[0]["Address"].ToString();
                    txtSaleCustomerOccupation.Text = dt.Rows[0]["Occupation"].ToString();


                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
       
        public void clear_CustomerFields()
        {
            txtvalueid.Text = "";
            txtSalecustomerName.Text ="";
            txtSaleCustomerMobileno.Text ="";
                txtSaleCustomerEmailID.Text ="";
                txtSaleCustomerAddress.Text ="";
                    dpSaleCustomerDOB.Text ="";
                    txtSaleCustomerOccupation.Text ="";
                        rdoSaleCustomerBusiness.IsChecked  =null;
                        rdoSaleCustomerPrivate.IsChecked  =null;
                            rdoSaleCustomergovt.IsChecked  =null;
                            lblfollowupidfetch.Content = "";
        }
        
        private void btnSaleCustomerGenrateBill_Click(object sender, RoutedEventArgs e)
        {
            clearAllAddedProducts();
            if (rdosalefollowupcustomer.IsChecked ==true )
            {
                targetButton = btnInvoice_Cash;
                Save_FollowupCustomer();
                UpdateFollowupStatus();
                Grd_genratebill.Visibility = System.Windows.Visibility.Visible;
                loadStockProducts();
                FetchtaxDetails();
                BillID_fetch();
                Load_EmployeeDetails();

              


            }
            else if (rdoSaleOldCustomer1.IsChecked ==true )
            {
               
                Grd_genratebill.Visibility = Visibility;
                
                // LoadTax();
                FetchtaxDetails();
                loadStockProducts();
                BillID_fetch();
                Load_EmployeeDetails();

            }
            else if (rdoSaleNewcustomer.IsChecked ==true )
            {
                if (NewCustomer_Validation() == true)
                    return;

                Save_NewCustomer();
                Grd_genratebill.Visibility = System.Windows.Visibility.Visible;
                //Save_NewCustomer();
                // LoadTax();
                FetchtaxDetails();
                loadStockProducts();
                BillID_fetch();
                Load_EmployeeDetails();

            }
            
        }

        private void cmbPre_Pro_Salename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
     //========================Invoice=====================================
        private void txtInvoice_InvcTotalAmount_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnInvoice_Cash_Click(object sender, RoutedEventArgs e)
        {
            GRDInvoce_Cash.Visibility = Visibility;
            txtInvoice_C_InvcTotalAmount .Text = txtInvoice_InvcTotalAmount.Text;
        }

        private void btnInvoice_Cheque_Click(object sender, RoutedEventArgs e)
        {
            GRDInvoice_Cheque.Visibility = Visibility;
        }

        private void btnInvoice_CH_Exit_Click(object sender, RoutedEventArgs e)
        {
            GRDInvoice_Cheque.Visibility = Visibility.Hidden ;
        }

        private void btnInvoice_Finance_Click(object sender, RoutedEventArgs e)
        {
            GRDInvoice_Finance.Visibility = Visibility; 
        }

        private void btnInvoice_InstalExit_Click(object sender, RoutedEventArgs e)
        {
            GRDInvoice_Installment.Visibility = Visibility.Hidden;
            Clear_SaveInstallment();
        }

        private void btnInvoice_Installment_Click(object sender, RoutedEventArgs e)
        {
            
            //if(txtInvoice_InstalTotalAmount .Text =="")
            //{
            //    MessageBox.Show("Please Select Atleast One Product",caption , MessageBoxButton.OK );
            //}
            //else { GRDInvoice_Installment.Visibility = Visibility; }
            GRDInvoice_Installment.Visibility = Visibility;
        }

        private void rdo_Invoice_Yearlyinstallment_Checked(object sender, RoutedEventArgs e)
        {
           
            if (rdo_Invoice_Yearlyinstallment.IsChecked == true)
            {
                cmdInvoice_InstalYear.Visibility = Visibility;
                loadyear();
                cmdInvoice_InstalMonth.Visibility = Visibility.Hidden;
               // rdo_Invoice_Yearlyinstallment. = true;
                //  rdoInvoice_rdo_Invoice_Monthlyinstallment.IsEnabled = false ;
               
            }
            else if (rdo_Invoice_Yearlyinstallment.IsChecked == false )
            {
                loadyear();
                txtInvoice_InstalAmountPermonth.Text = "";

            }
        }

        private void rdoInvoice_rdo_Invoice_Monthlyinstallment_Checked(object sender, RoutedEventArgs e)
        {
            if (rdoInvoice_rdo_Invoice_Monthlyinstallment.IsChecked ==true )
            {
                cmdInvoice_InstalMonth.Visibility = Visibility;
            cmdInvoice_InstalYear.Visibility = Visibility.Hidden ;
            loadMonth();
            }
            else if(rdoInvoice_rdo_Invoice_Monthlyinstallment.IsChecked ==false )
            {
                loadMonth();
                txtInvoice_InstalAmountPermonth.Text = "";

            }

        }
        
        public void loadyear()
        {
            cmdInvoice_InstalYear.Text = "---Select---";
            cmdInvoice_InstalYear.Items.Add("1 Year");
            cmdInvoice_InstalYear.Items.Add("2 Year");
            cmdInvoice_InstalYear.Items.Add("3 Year");
            cmdInvoice_InstalYear.Items.Add("4 Year");
            cmdInvoice_InstalYear.Items.Add("5 Year");

        }

        public void SourceOfEnquiry()
        {
            cmbSourceOfEnquery.Text = "---Select---";
            cmbSourceOfEnquery.Items.Add("Newspaper");
            cmbSourceOfEnquery.Items.Add("Poster");
            cmbSourceOfEnquery.Items.Add("Reference");
            cmbSourceOfEnquery.Items.Add("Friends / Colleagues");
            cmbSourceOfEnquery.Items.Add("Net / Website");
            cmbSourceOfEnquery.Items.Add("Non");
        }

        public void loadMonth()
        {
            cmdInvoice_InstalMonth.Text = "---Select---";
            cmdInvoice_InstalMonth.Items.Add("1 Month");
            cmdInvoice_InstalMonth.Items.Add("2 Month");
            cmdInvoice_InstalMonth.Items.Add("3 Month");
            cmdInvoice_InstalMonth.Items.Add("4 Month");
            cmdInvoice_InstalMonth.Items.Add("5 Month");
            cmdInvoice_InstalMonth.Items.Add("6 Month");
            cmdInvoice_InstalMonth.Items.Add("7 Month");
            cmdInvoice_InstalMonth.Items.Add("8 Month");
            cmdInvoice_InstalMonth.Items.Add("9 Month");
            cmdInvoice_InstalMonth.Items.Add("10 Month");
            cmdInvoice_InstalMonth.Items.Add("11 Month");

        }

        private void txtInvoice_InstalPaidAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( txtInvoice_InstalPaidAmount.Text == "")
            {
                txtInvoice_InstalBalanceAmount.Text = txtInvoice_InstalTotalAmount.Text;
            }
            else if (txtInvoice_InstalTotalAmount.Text != "" && txtInvoice_InstalPaidAmount.Text != "")
            {
            double invoice_TAmount =Convert .ToDouble ( txtInvoice_InstalTotalAmount.Text);
            double invoice_PAmount = Convert.ToDouble(txtInvoice_InstalPaidAmount .Text);
            double invoice_BAmount = invoice_TAmount - invoice_PAmount;
            txtInvoice_InstalBalanceAmount.Text = invoice_BAmount.ToString();
                }
        }

        private void cmdInvoice_InstalYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calculateinstallment_By_Year();

        }
        public void calculateinstallment_By_Year()
        {
            year = cmdInvoice_InstalYear.SelectedItem.ToString();
            if (year == "1 Year")
            {
                 y1=12;
            }
             if (year == "2 Year")
            {
                 y1=24;
            }
             if (year == "3 Year")
            {
                 y1=36;
            }
             if (year == "4 Year")
            {
                 y1=48;
            }
             if (year == "5 Year")
            {
                 y1=60;
            }
           
             double balamt =Convert .ToDouble ( txtInvoice_InstalBalanceAmount.Text);
            double calculateamt=Convert .ToDouble(balamt / y1);
            txtInvoice_InstalAmountPermonth.Text = Microsoft.VisualBasic.Strings.Format(calculateamt, "##,###.00");
           
        }

        private void cmdInvoice_InstalMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calculateinstallment_By_Month();
        }
        public void calculateinstallment_By_Month()
        {
            month = cmdInvoice_InstalMonth .SelectedItem.ToString();
            if (month == "1 Month")
            {
                m1 = 1;
            }
            if (month == "2 Month")
            {
                m1 = 2;
            }
            if (month == "3 Month")
            {
                m1 = 3;
            }
            if (month == "4 Month")
            {
                m1 = 4;
            }
            if (month == "5 Month")
            {
                m1 = 5;
            }
            if (month == "6 Month")
            {
                m1 = 6;
            }
            if (month == "7 Month")
            {
                m1 = 7;
            }
            if (month == "8 Month")
            {
                m1 = 8;
            }
            if (month == "9 Month")
            {
                m1 = 9;
            }
            if (month == "10 Month")
            {
                m1 = 10;
            }
            if (month == "11 Month")
            {
                m1 = 11;
            }
            double balamt2 = Convert.ToDouble(txtInvoice_InstalBalanceAmount.Text);
            double calculateamt2 = Convert.ToDouble(balamt2 / m1);
            txtInvoice_InstalAmountPermonth.Text = Microsoft.VisualBasic.Strings.Format(calculateamt2, "##,###.00");
        }

        private void btnInvoice_MainExit_Click(object sender, RoutedEventArgs e)
        {
            if(rdosalefollowupcustomer.IsChecked ==true)
            {
                clearAllAddedProducts();
                GRD_FollowupCostomer.Visibility = Visibility;
                Grd_genratebill.Visibility = Visibility.Hidden;
            }
            else if(rdoSaleOldCustomer1.IsChecked ==true)
            {
                clearAllAddedProducts();
                Grd_genratebill.Visibility = Visibility.Hidden;
                 GRD_Customer_Billing.Visibility = Visibility;
            }
            else if (rdoSaleNewcustomer.IsChecked == true)
            {
                clearAllAddedProducts();
                Grd_genratebill.Visibility = Visibility.Hidden ;
                 GRD_Customer_Billing.Visibility = Visibility;
            }
            else { 
            Grd_genratebill.Visibility = Visibility.Hidden;
            }
           
           
           // clearAllAddedProducts();
          
            //clear_CustomerFields();
        }

        private void cmbInvoice_Tax_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cmbInvoice_Tax1.SelectedValue == "+ <ADD Tax>")
            //{
            //    ADD_Tax tax = new ADD_Tax();
            //    tax.Show();
            //}
        }

      public void calculatetax()
        {
            if (txtInvoice_TotalPriceofQty.Text == "")
          {
              MessageBox.Show("Please Enter Quantity ");
          }
          else if (txtInvoice_TotalPriceofQty.Text != "" && cmbInvoice_Tax1.SelectedItem.ToString() != "")
          {
              double totprice = Convert.ToDouble(txtInvoice_TotalPriceofQty.Text);
              double tx = Convert.ToDouble(cmbInvoice_Tax1.SelectedValue.ToString ());
              double stot = ((totprice * tx) / 100);
              txtInvoice_SubToatal.Text = (totprice + stot).ToString();
          }
         
          
        }
        public void FetchtaxDetails()
        {
            cmbInvoice_Tax1.Text = "---Select---";
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("select Tax_Type, Tax_Percentage from tlb_AddTax  where S_Status='Active'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                   
                   // cmbInvoice_Tax1.Items.Insert(0, "---Select---");
                       
                    cmbInvoice_Tax1 .ItemsSource = ds.Tables[0].DefaultView;

                    cmbInvoice_Tax1.DisplayMemberPath = ds.Tables[0].Columns["Tax_Type"].ToString();
                    cmbInvoice_Tax1.SelectedValuePath = ds.Tables[0].Columns["Tax_Percentage"].ToString();
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }


        }

       
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        public void FetchAvailableQty()
        {
            try
            {
                con.Open();
               
                DataTable dt = new DataTable();
               // cmd = new SqlCommand(qry,con);
                string qry = "select ID,AvilableQty,FinalPrice  from StockDetails where [ID]= '" + cmbInvoiceStockProducts.SelectedValue.GetHashCode() + "'";
               cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    // cmbInvoice_Tax1.Items.Insert(0, "---Select---");

                    txtInvoice_AvailabeQty.Text = dt.Rows[0]["AvilableQty"].ToString();
                    txtInvoiceActualPrice.Text = dt.Rows[0]["FinalPrice"].ToString();
                   

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }


        }
        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {

            if(txtPrice .Text =="" )
            {
                MessageBox .Show ("Please Insert Price",caption , MessageBoxButton.OK );
                txtQuantity.Text = 0.ToString();

            }
            else if (txtQuantity.Text =="")
            {
                txtTotalPrice.Text = txtPrice.Text;
            }
            else if (txtPrice.Text !="" && txtQuantity .Text !="")
            {
                double tamt1;
                nfi = (NumberFormatInfo)nfi.Clone();
                nfi.CurrencySymbol = "";

                double prc = Convert.ToDouble(txtPrice.Text);
                double qty = Convert.ToDouble(txtQuantity.Text);
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

        private void txtInvoice_C_Exit_Click(object sender, RoutedEventArgs e)
        {
            GRDInvoce_Cash.Visibility = Visibility.Hidden;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ADD_Tax adt = new ADD_Tax();
            adt.ShowDialog();
            FetchtaxDetails();
        }
   public void loadStockProducts()
        {
            cmbInvoiceStockProducts.Text = "---Select---";
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string qry = "Select  S.ID,S.Domain_ID , S.Product_ID ,S.Brand_ID ,S.P_Category ,S.Model_No_ID ,S.Color_ID  " +
                             ",D.Domain_Name + ' , ' + P.Product_Name + ' , ' + B.Brand_Name + ' , ' + PC.Product_Category + ' , ' + M.Model_No + ' , ' + C.Color AS Products " +
                             "From StockDetails S " +
                             "INNER JOIN tb_Domain D on D.ID=S.Domain_ID " +
                             "INNER JOIN tlb_Products P on P.ID=S.Product_ID " +
                             "INNER JOIN tlb_Brand B on B.ID=S.Brand_ID " +
                             "INNER JOIN tlb_P_Category PC on PC.ID=S.P_Category " +
                             "INNER JOIN tlb_Model M on M.ID=S.Model_No_ID " +
                             "INNER JOIN tlb_Color C on C.ID=S.Color_ID " +
                             "where S.S_Status='Active' ORDER BY S.C_Date ASC ";
                cmd = new SqlCommand(qry , con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                if (ds.Tables [0].Rows .Count  > 0)
                {
                    cmbInvoiceStockProducts.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                    cmbInvoiceStockProducts.ItemsSource = ds.Tables[0].DefaultView;
                    cmbInvoiceStockProducts.DisplayMemberPath =ds.Tables[0].Columns["Products"].ToString();

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }


        }

   private void txtInvoice_Qty_TextChanged(object sender, TextChangedEventArgs e)
  {
       double   d=0;
       if (txtInvoice_Qty.Text == "" )
   {
       txtInvoice_TotalPriceofQty.Text = txtInvoiceActualPrice.Text;
   }
       else if (txtInvoiceActualPrice.Text != "" && txtInvoice_Qty.Text != "")
       {
           o = Convert.ToDouble (txtInvoice_AvailabeQty.Text);
           p = Convert.ToDouble(txtInvoice_Qty.Text);
           if (o >= p)
           {
               double actualprice = Convert.ToDouble(txtInvoiceActualPrice.Text);
               double q = Convert.ToDouble(txtInvoice_Qty.Text);
               double tprice = actualprice * q;
               txtInvoice_TotalPriceofQty.Text = tprice.ToString();
           }
           else
           {
               MessageBox.Show("Please Select within the range of Available Quantity");
               txtInvoice_Qty.Text = "";
           }
        
     }
       else if (txtInvoice_Qty.Text ==d.ToString ())
       {
           txtInvoice_TotalPriceofQty.Text = txtInvoiceActualPrice.Text;
       }
       double  rem = o - p;
       txtInvoice_remainingqty.Content = rem.ToString();
     }

   private void cmbInvoice_Tax1_DropDownClosed(object sender, EventArgs e)
   {
       calculatetax();
   }
public void clearAllAddedProducts()
   {
       txtInvoice_AvailabeQty.Text = "";
       txtInvoice_Qty.Text = "";
       txtInvoiceActualPrice.Text = "";
       txtInvoice_TotalPriceofQty.Text = "";
       txtInvoice_SubToatal.Text = "";
       cmbInvoice_Tax1.ItemsSource = null;
       FetchtaxDetails();
       cmbInvoiceStockProducts.ItemsSource = null;
       loadStockProducts();
       Dgrd_InvoiceADDProducts.ItemsSource = null;
       txtInvoice_remainingqty.Content = "";
       txtInvoice_InvcTotalAmount.Text  = "";



   }
        public void clearAddText()
{
    txtInvoice_AvailabeQty.Text = "";
    txtInvoice_Qty.Text = "";
    txtInvoiceActualPrice.Text = "";
    txtInvoice_TotalPriceofQty.Text = "";
    txtInvoice_SubToatal.Text = "";
    cmbInvoice_Tax1.ItemsSource = null;
    FetchtaxDetails();
    cmbInvoiceStockProducts.ItemsSource = null;
    loadStockProducts();
   // Dgrd_InvoiceADDProducts.ItemsSource = null;
    txtInvoice_remainingqty.Content = "";
   // txtInvoice_InvcTotalAmount.Text = "";

}
        public bool Invoic_Add_Validation()
        {
            bool res = false;
            if (cmbInvoice_Tax1.SelectedValue  == null )
            {
                res = true;
                MessageBox.Show("Please Select TAX ", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
            if (txtInvoice_Qty.Text =="")
            {
                res = true;
                MessageBox.Show("Please Insert Quantity ", caption,MessageBoxButton .OK , MessageBoxImage.Error );

            }
            else if (cmbInvoiceStockProducts.Text =="")
            {
                res = true;
                MessageBox.Show("Please Select Products ", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }

        public void ckeck_addProduct()
        {
            if (dtstat.Rows.Count >0)
            {
                if (dtstat.Columns[1].ToString ()  == cmbInvoiceStockProducts.Text)
                {
                    MessageBox.Show("Already Present");
                }
            }
        }
        private void btninvoice_addProduct_Click(object sender, RoutedEventArgs e)
        {
            if (Invoic_Add_Validation() == true)
                return;
            ckeck_addProduct();
                if (dtstat.Rows.Count == 0)
                {
                    dtstat.Columns.Add("SrNo");

                    dtstat.Columns.Add("Products");
                    dtstat.Columns.Add("ID");
                    dtstat.Columns.Add("RatePer_Product");
                    dtstat.Columns.Add("Qty");
                    dtstat.Columns.Add("Total_Price");
                    dtstat.Columns.Add("Tax Name");
                    dtstat.Columns.Add("Taxes %");
                    dtstat.Columns.Add("SubTotal");
                    dtstat.Columns.Add("availqty");
                 
                    // dtstat.Columns["availqty"].Visible = false;
                }

                DataRow dr = dtstat.NewRow();
                dr["SrNo"] = lblinvoiceSr.Content;
                dr["Products"] = cmbInvoiceStockProducts.Text;
                dr["ID"] = cmbInvoiceStockProducts.SelectedValue.GetHashCode();
                dr["RatePer_Product"] = txtInvoiceActualPrice.Text;
                dr["Qty"] = txtInvoice_Qty.Text;

                dr["Total_Price"] = txtInvoice_TotalPriceofQty.Text;
                dr["Tax Name"] = cmbInvoice_Tax1.Text;
                dr["Taxes %"] = cmbInvoice_Tax1.SelectedValue.ToString();

                dr["SubTotal"] = txtInvoice_SubToatal.Text;
                dr["availqty"] = txtInvoice_AvailabeQty.Text;
                //  availqty =Convert .ToDouble ( txtInvoice_AvailabeQty.Text);
                dtstat.Rows.Add(dr);

                lblinvoiceSr.Content = (Convert.ToInt32(lblinvoiceSr.Content) + 1).ToString();
                // txtProduct.Text = "";
                // cmb1();
                //cmb2();
                //cmbtShortCode.SelectedIndex = 1;
                //txtQty.Text = "";
                // txtRateAndUnit.Text = "";
                //txtSubTotal.Text = "";

                Dgrd_InvoiceADDProducts.ItemsSource = dtstat.DefaultView;
            Dgrd_InvoiceADDProducts .Columns [2].Visibility =Visibility .Hidden ;
                Dgrd_InvoiceADDProducts.Columns[9].Visibility = Visibility.Hidden;
                // Dgrd_InvoiceADDProducts.Columns[0].Visibility = Visibility.Hidden;

                if (dtstat.Rows.Count > 0)
                {
                    double invamt = 0.00;
                    foreach (DataRow drow in dtstat.Rows)
                    {

                        invamt += Convert.ToDouble(drow["SubTotal"].ToString());
                    }

                    txtInvoice_InvcTotalAmount.Text = Convert.ToString(invamt);

                }
                clearAddText();

            
        }

   private void cmbInvoiceStockProducts_DropDownClosed(object sender, EventArgs e)
   {
       txtInvoice_AvailabeQty.Text = "";
       txtInvoice_Qty.Text = "";
       txtInvoiceActualPrice.Text = "";
       txtInvoice_TotalPriceofQty.Text = "";
       txtInvoice_SubToatal.Text = "";
       cmbInvoice_Tax1.ItemsSource = null;
       FetchtaxDetails();
       FetchAvailableQty();
       

   }

   private void btninvoice_clearProduct_Click(object sender, RoutedEventArgs e)
   {
       clearAllAddedProducts();

   }
public void Save_FollowupCustomer()
   {
       string caption1 = "Confirmation";
     
     MessageBoxResult res=  MessageBox.Show("Do You Want To Move Follow_up To Customer ?",caption1 , MessageBoxButton.YesNo );
    if (res ==MessageBoxResult.Yes  )
    {
        balc.Flag = 1;
       // balc.Employee_ID = .SelectedValue.GetHashCode();
        balc.Cust_ID = txtvalueid.Text;
        balc.Name = txtSalecustomerName.Text;
        balc.Mobile_No = txtSaleCustomerMobileno.Text;
        balc.Date_Of_Birth = dpSaleCustomerDOB.Text;
        balc.Email_ID = txtSaleCustomerEmailID.Text;
        balc.Address = txtSaleCustomerAddress.Text;
        balc.Occupation = txtSaleCustomerOccupation.Text;
        balc.Source_OF_Enquiry = cmbSourceOfEnquery.Text;
        soe = cmbSourceOfEnquery.SelectedValue.ToString();
        if (soe == "Newspaper")
        {
            vsoe = 1;
        }
        else if (soe == "Poster")
        {
            vsoe = 2;
        }
        else if (soe == "Reference")
        {
            vsoe = 3;

        }
        else if (soe == "Friends / Colleagues")
        {
            vsoe = 3;

        }
        else if (soe == "Net / Website")
        {
            vsoe = 4;

        }
        else if (soe == "Non")
        {
            vsoe = 5;

        }

        balc.SourceEnqID = vsoe;
        balc.S_Status = "Active";
        balc.C_Date = System.DateTime.Now.ToShortDateString();
        dalc.Customer_Save_Insert_Update_Delete(balc);
        MessageBox.Show("New Customer Added Successfully..",caption , MessageBoxButton.OK );
    }
    else if (res == MessageBoxResult.No)
    {
        MessageBox.Show(" You Have To First Add Customer Than Create Bill", caption, MessageBoxButton.OK);
   }
   }
public void Save_NewCustomer()
{
    string caption1 = "Confirmation";

    MessageBoxResult res = MessageBox.Show("Do You Want To Add New Customer ?", caption1, MessageBoxButton.YesNo);
    if (res == MessageBoxResult.Yes)
    {
        balc.Flag = 1;
      //  balc.Employee_ID = cmbCustomer_EmployeeName.SelectedValue.GetHashCode();
        balc.Cust_ID = txtvalueid.Text;
        balc.Name = txtSalecustomerName.Text;
        balc.Mobile_No = txtSaleCustomerMobileno.Text;
        balc.Date_Of_Birth = dpSaleCustomerDOB.Text;
        balc.Email_ID = txtSaleCustomerEmailID.Text;
        balc.Address = txtSaleCustomerAddress.Text;
        if(rdoSaleCustomerBusiness.IsChecked ==true )
        {
            occu="Business";
        }
        else if(rdoSaleCustomerPrivate.IsChecked ==true )
        {
            occu="Private Employee";
        }
        else if (rdoSaleCustomergovt.IsChecked ==true )
        {
            occu ="GOVT Employee";
        }
        balc.Occupation = occu;
        balc.Source_OF_Enquiry = cmbSourceOfEnquery.SelectedValue .ToString ();
        soe=cmbSourceOfEnquery.SelectedValue .ToString ();
        if (soe == "Newspaper")
        {
            vsoe = 1;
        }
        else if (soe == "Poster")
        {
            vsoe = 2;
        }
        else if (soe == "Reference")
        {
            vsoe = 3;

        }
        else if (soe == "Friends / Colleagues")
        {
            vsoe = 3;

        }
        else if (soe == "Net / Website")
        {
            vsoe = 4;

        }
        else if (soe == "Non")
        {
            vsoe = 5;

        }

        balc.SourceEnqID = vsoe;
        balc.S_Status = "Active";
        balc.C_Date = System.DateTime.Now.ToShortDateString();
        dalc.Customer_Save_Insert_Update_Delete(balc);
      //  dalc.Customer_Save_Insert_Update_Delete(balc);
        MessageBox.Show("New Customer Added Successfully..", caption, MessageBoxButton.OK);
    }
    else if (res == MessageBoxResult.No)
    {
        MessageBox.Show(" You Have To First Add Customer Than Create Bill", caption, MessageBoxButton.OK);
    }
}

public void UpdateFollowupStatus()
{
    balc.Flag = 1;
    balc.F_ID =Convert.ToInt32 (lblfollowupidfetch.Content);
    balc.S_Status = "DeActive";
    balc.C_Date = System.DateTime.Now.ToShortDateString();
    dalc.Customer_Update(balc);
    MessageBox.Show("Follow_up Customer DeActivated",caption ,MessageBoxButton.OK );
}

private void txtInvoice_C_PaidAmount_TextChanged(object sender, TextChangedEventArgs e)
{
    if(txtInvoice_C_PaidAmount.Text =="")
    {
        double zero = 0;
        txtInvoice_C_BalanceAmount.Text = zero .ToString();
    }
    else if (txtInvoice_C_PaidAmount.Text != "")
    {
        double tcamt =Convert.ToDouble ( txtInvoice_C_InvcTotalAmount.Text);
        double pcamt =Convert.ToDouble( txtInvoice_C_PaidAmount.Text);
        double btamt = (tcamt - pcamt);
        txtInvoice_C_BalanceAmount.Text = btamt.ToString(); ;
    }
    else if (txtInvoice_C_InvcTotalAmount.Text==txtInvoice_C_PaidAmount.Text)
    {

        double zero = 0;
        txtInvoice_C_BalanceAmount.Text = zero.ToString();
    }
}

 public void FetchCustomerID()
{
    string q = "Select ID from tlb_Customer where Cust_ID='" + txtvalueid.Text + "'";
    cmd = new SqlCommand(q,con );
    DataTable dt = new DataTable();
    SqlDataAdapter adp = new SqlDataAdapter(cmd);
     
    adp.Fill(dt);
            if(dt.Rows .Count >0)
            {
               I =Convert .ToInt32 ( dt.Rows[0]["ID"]);
            }
    
}
private void btnInvoice_C_SaveandPrint_Click(object sender, RoutedEventArgs e)
{
    FetchCustomerID();
    SaveInvoiceDetails();
    Save_CommonBill();
    SaveCash();
    updateQuantity();
    clear_CustomerFields();
    clearAllAddedProducts();
}
        public void SaveInvoiceDetails()
        {
            if (dtstat.Rows.Count > 0)
            {
                for ( i = 0; i < dtstat.Rows .Count; i++)
                {
                  //  int rowCount = ((DataTable)this.Dgrd_InvoiceADDProducts.DataSource).Rows.Count;

                    g = dtstat.Rows[i]["Products"].ToString();
                    FetchProductsID();
                    // string s = "  Select  S.ID,S.Domain_ID , S.Product_ID ,S.Brand_ID ,S.P_Category ,S.Model_No_ID ,S.Color_ID  From StockDetails S where ID='"+cmbInvoiceStockProducts .SelectedItem .GetHashCode ()+"' and  S.S_Status='Active' ORDER BY S.C_Date ASC";
                    DataSet ds = new DataSet();
                    string qry = "Select  Domain_ID , Product_ID ,Brand_ID ,P_Category ,Model_No_ID ,Color_ID From StockDetails S where ID='" + txtid.Text + "' and  S.S_Status='Active' ";
                    cmd = new SqlCommand(qry, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    // con.Open();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtd.Text = ds.Tables[0].Rows[0]["Domain_ID"].ToString();
                        txtP.Text = ds.Tables[0].Rows[0]["Product_ID"].ToString();
                        txtB.Text = ds.Tables[0].Rows[0]["Brand_ID"].ToString();
                        txtPC.Text = ds.Tables[0].Rows[0]["P_Category"].ToString();
                        txtM.Text = ds.Tables[0].Rows[0]["Model_No_ID"].ToString();
                        txtC.Text = ds.Tables[0].Rows[0]["Color_ID"].ToString();
                    }

                    binvd.Flag = 1;
                    binvd.Customer_ID = I;
                    binvd.Bill_No = lblbillno.Content.ToString ();
                    binvd.Domain_ID = Convert.ToInt32(txtd.Text);
                    binvd.Product_ID = Convert.ToInt32(txtP.Text);
                    binvd.Brand_ID = Convert.ToInt32(txtB.Text);
                    binvd.P_Category = Convert.ToInt32(txtPC.Text);
                    binvd.Model_No_ID = Convert.ToInt32(txtM.Text);
                    binvd.Color_ID = Convert.ToInt32(txtC.Text);
                    binvd.Products123 = dtstat.Rows[i]["Products"].ToString();
                    binvd.Per_Product_Price = Convert.ToDouble(dtstat.Rows[i]["RatePer_Product"].ToString());
                    binvd.Qty = Convert.ToDouble(dtstat.Rows[i]["Qty"].ToString());
                    binvd.C_Price = Convert.ToDouble(dtstat.Rows[i]["Total_Price"].ToString());
                    binvd.Tax_Name = dtstat.Rows[i]["Tax Name"].ToString();
                    binvd.Tax = Convert.ToDouble(dtstat.Rows[i]["Taxes %"].ToString());
                    binvd.Total_Price = Convert.ToDouble(dtstat.Rows[i]["SubTotal"].ToString());
                    if (pm_c == "Cash")
                    {
                        binvd.Payment_Mode = "Cash";
                    }
                    else if (pm_ch =="Cheque")
                    {
                         binvd.Payment_Mode = "Cheque";
                    }
                    else if( pm_f =="Finance")
                    {
                        binvd .Payment_Mode ="Finance";
                    }
                    else if(pm_ins =="Installment")
                    {
                        binvd .Payment_Mode ="Installment";
                    }
                    binvd.S_Status = "Active";
                    binvd.C_Date = System.DateTime.Now.ToShortDateString();
                    dinvd.InvoiceDetails_Save(binvd);
                    MessageBox.Show("Done");
                    updateQuantity();
                }
            }
       
          }
        public void updateQuantity()
        {
            for (i = 0; i < dtstat.Rows.Count; i++)
            {
                binvd.Flag = 1;
                binvd.Products123 = g;
                binvd.ID = Convert.ToInt32(dtstat.Rows[i]["ID"].ToString());
                 binvd.Bill_No = lblbillno.Content.ToString();
               double d = Convert.ToDouble(dtstat .Rows [i]["availqty"].ToString ());
                double q = Convert.ToDouble(dtstat.Rows[i]["Qty"].ToString());
                double tq = d - q;
                binvd.AvilableQty = tq;
                binvd.SaleQty = Convert.ToDouble(dtstat.Rows[i]["Qty"].ToString());
                binvd.S_Status = "Active";
                binvd.C_Date = System.DateTime.Now.ToShortDateString();
                dinvd.Update_QTY(binvd);
                MessageBox.Show("Quantity updated ");
            }
        }
        public void FetchProductsID()
        {
           
            DataSet ds = new DataSet();
            string qry = "Select ID from StockDetails where Products123='" + g + "' and  S_Status='Active'  ";
            cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // con.Open();
            da.Fill(ds);

            if (ds.Tables[0].Rows .Count > 0)
            {
                txtid.Text = ds.Tables[0].Rows[0]["ID"].ToString();
            }
        }
        public void SaveCash()
        {
            balpm.Flag = 1;
            balpm.Customer_ID = I;
            balpm.Bill_No = lblbillno.Content.ToString();
            balpm.Total_Price =Convert .ToDouble ( txtInvoice_C_PaidAmount.Text);
            balpm.Paid_Amount = Convert.ToDouble(txtInvoice_C_PaidAmount.Text);
            balpm.Balance_Amount = Convert.ToDouble(txtInvoice_C_BalanceAmount.Text);
            balpm.S_Status = "Active";
            balpm.C_Date = System.DateTime.Now.ToShortDateString();
            dalpm.Save_Cash(balpm);
            MessageBox.Show("Cash Value Added Successfully");
        }
        public void Save_CommonBill()
        {
            binvd.Flag = 1;
            binvd.Customer_ID = I;
            binvd.Employee_ID = cmbInvoiceEmployeename.SelectedValue.GetHashCode();
            binvd.Bill_No = lblbillno .Content.ToString () ;
            if (pm_c == "Cash")
                    {
                        binvd.Payment_Mode = "Cash";
                        binvd.Total_Price = Convert.ToDouble(txtInvoice_C_PaidAmount.Text);
                        binvd.Paid_Amount = Convert.ToDouble(txtInvoice_C_PaidAmount.Text);
                        binvd.Balance_Amount = Convert.ToDouble(txtInvoice_C_BalanceAmount.Text);
                    }
                    else if (pm_ch =="Cheque")
                    {
                         binvd.Payment_Mode = "Cheque";
                         binvd.Total_Price = Convert.ToDouble(btnInvoice_CH_InvcTAmount.Text);
                         binvd.Paid_Amount =0 ;
                         binvd.Balance_Amount = 0;
                    }
                    else if( pm_f =="Finance")
                    {
                        binvd .Payment_Mode ="Finance";
                    }
                    else if(pm_ins =="Installment")
                    {
                        binvd .Payment_Mode ="Installment";
                        binvd.Total_Price = Convert.ToDouble(txtInvoice_InstalTotalAmount.Text);
                        binvd.Paid_Amount = Convert.ToDouble(txtInvoice_InstalPaidAmount.Text);
                        binvd.Balance_Amount = Convert.ToDouble(txtInvoice_InstalBalanceAmount.Text);
                    }
            binvd.S_Status = "Active";
            binvd.C_Date = System.DateTime.Now.ToShortDateString();
            dinvd.CommonBillNo_Save(binvd);
            MessageBox.Show("Common bill Added",caption , MessageBoxButton.OK);

        }

        private void PaymentMode_Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button) == btnInvoice_Cash)
            {

                btnInvoice_Cash.Visibility = Visibility;
                GRDInvoce_Cash.Visibility = Visibility;
                pm_c = "Cash";
 txtInvoice_C_InvcTotalAmount.Text = txtInvoice_InvcTotalAmount.Text;
            }
            else if((sender as Button) == btnInvoice_Cheque)
            {
                GRDInvoice_Cheque.Visibility =Visibility ;
                btnInvoice_Cheque.Visibility =Visibility ;
                pm_ch ="Cheque";
                 btnInvoice_CH_InvcTAmount.Text = txtInvoice_InvcTotalAmount.Text;
                 FetchBankName();
            }
            else if((sender as Button) == btnInvoice_Finance)
            {
                btnInvoice_Finance.Visibility =Visibility ;
                 GRDInvoice_Finance.Visibility = Visibility; 
                pm_f ="Finance";
                //Text = txtInvoice_InvcTotalAmount.Text;

            }
            else if((sender as Button) == btnInvoice_Installment)
            {
                GRDInvoice_Installment.Visibility =Visibility ;
                btnInvoice_Installment.Visibility =Visibility ;
                pm_ins ="Installment";
                 txtInvoice_InstalTotalAmount.Text = txtInvoice_InvcTotalAmount.Text;
            }

              
            else
            {

                MessageBox.Show("Wrong");

            }
        }
        public bool cash_Validation()
        {
            bool rc = false;
            if(btnInvoice_CH_Amount.Text =="")
            {
                rc = true;
                MessageBox.Show("Please Inter Cheque Amount ", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (btnInvoice_CH_chequeno.Text =="")
            {
                rc = true;
                MessageBox.Show("Please Inter Cheque Number ", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (dpInvoice_CH_ChequeDate.Text == "")
            {
                rc = true;
                MessageBox.Show("Please Select Date ", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cmbInvoic_CH_BankName.SelectedItem == "")
            {
                rc = true;
                MessageBox.Show("Please Select Bank Name ", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return rc;
        }
        private void btnInvoice_CH_SaveandPrint_Click(object sender, RoutedEventArgs e)
        {
            if (cash_Validation() == true)
                return;
            FetchCustomerID();
            SaveInvoiceDetails();
            Save_CommonBill();
            SaveCheque();
          // updateQuantity();
            clear_CustomerFields();
            clearAllAddedProducts();
            clear_AllCheque();

        }
        public void SaveCheque()
        {
            if (cash_Validation() == true)
                return;
            balpm.Flag = 1;
            balpm.Customer_ID = I;
            balpm.Bill_No = lblbillno.Content.ToString();
            balpm.Total_Price = Convert.ToDouble(btnInvoice_CH_InvcTAmount.Text);
            balpm.Cheque_Amount  = Convert.ToDouble(btnInvoice_CH_Amount.Text);
            balpm.Cheque_No = btnInvoice_CH_chequeno.Text;
            balpm.Cheque_Date  =dpInvoice_CH_ChequeDate .SelectedDate .ToString ();
            balpm.Cheque_Bank_Name = cmbInvoic_CH_BankName.Text;
            balpm.IsClear = "Active";
            balpm.S_Status = "Active";
            balpm.C_Date = System.DateTime.Now.ToShortDateString();
            dalpm.Save_Cheque(balpm);
            MessageBox.Show("Cheque details added succssfully");
            FetchBankName();
        }
        public void FetchBankName()
        {
            cmbInvoic_CH_BankName.Text = "Select Bank Name";
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                cmd = new SqlCommand("select Distinct ID,Cheque_Bank_Name from tlb_Cheque  where S_Status='Active'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                // con.Open();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbInvoic_CH_BankName.ItemsSource = ds.Tables[0].DefaultView;
                    cmbInvoic_CH_BankName.DisplayMemberPath = ds.Tables[0].Columns["Cheque_Bank_Name"].ToString();
                    cmbInvoic_CH_BankName.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                   // cmbInvoic_CH_BankName.SelectedValuePath = dt.Rows[0]["ID"].GetHashCode;
                   // cmbInvoic_CH_BankName.ItemsSource = dt.DefaultView;
                    //cmbInvoic_CH_BankName.DisplayMemberPath  = dt.Rows[0]["Cheque_Bank_Name"].ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }
        public bool Installment_Validation()
        {
            bool inst = false;
            if(txtInvoice_InstalPaidAmount.Text =="")
            {
                inst =true ;
                MessageBox.Show("Please Enter Paid Amount", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (dpInvoice_Instalpermonth.Text == "")
            { inst =true ;
            MessageBox.Show("Please Select Date", caption, MessageBoxButton.OK, MessageBoxImage.Error);

            }
            // else if(rdo_Invoice_Yearlyinstallment. =="")
            //{ inst =true ;
            //MessageBox.Show("Please Enter Paid Amount", caption, MessageBoxButton.OK, MessageBoxImage.Error);

            //}
            // else if(dpInvoice_Instalpermonth.Text =="")
            //{ inst =true ;
            //MessageBox.Show("Please Enter Paid Amount", caption, MessageBoxButton.OK, MessageBoxImage.Error);

            //}
            return inst;
        }
        private void btnInvoice_InstalSaveandPrint_Click(object sender, RoutedEventArgs e)
        {
            if (Installment_Validation() == true)
                return;
            FetchCustomerID();
            SaveInvoiceDetails();
            Save_CommonBill();
            SaveInstallment();
            Clear_SaveInstallment();
            clear_CustomerFields();
            clearAllAddedProducts();
           
        }
        public void Clear_SaveInstallment()
        {
            txtInvoice_InstalTotalAmount.Text = "";
            txtInvoice_InstalPaidAmount.Text = "";
            txtInvoice_InstalBalanceAmount.Text = "";
            txtInvoice_InstalAmountPermonth.Text = "";
            dpInvoice_Instalpermonth.Text = "";
            rdo_Invoice_Yearlyinstallment.IsChecked = false;
            rdoInvoice_rdo_Invoice_Monthlyinstallment.IsChecked = false;
            cmdInvoice_InstalYear.Visibility = Visibility .Hidden ;
            cmdInvoice_InstalMonth.Visibility = Visibility.Hidden;
        }
        public void SaveInstallment()
        {
            bins.Flag = 1;
            bins.Customer_ID = I;
            bins.Bill_No = lblbillno.Content.ToString();
            bins.Total_Price = Convert.ToDouble(txtInvoice_InstalTotalAmount.Text);
            bins.Paid_Amount = Convert.ToDouble(txtInvoice_InstalPaidAmount.Text);
            bins.Balance_Amount = Convert.ToDouble(txtInvoice_InstalBalanceAmount.Text);
            bins.Monthly_Amount = Convert.ToDouble(txtInvoice_InstalAmountPermonth.Text);
            if (rdo_Invoice_Yearlyinstallment.IsChecked ==true )
            {string yearins=cmdInvoice_InstalYear.SelectedValue.ToString();
            
            if (yearins == "1 Year")
            {
                yarvalue = "12";
            }
            if (yearins == "2 Year")
            {
                yarvalue = "24";
            }
            if (yearins == "3 Year")
            {
                yarvalue = "36";
            }
            if (yearins == "4 Year")
            {
                yarvalue = "48";
            }
            if (yearins == "5 Year")
            {
                yarvalue = "60";
            }

            bins.Installment_Year = yarvalue;
            bins.Installment_Month ="NO";

            }
            else if (rdoInvoice_rdo_Invoice_Monthlyinstallment.IsChecked==true)
            {
                string monthins = cmdInvoice_InstalMonth.SelectedValue.ToString();
                if (monthins == "1 Month")
                {
                    monthvalue = "1";
                }
                if (monthins == "2 Month")
                {
                    monthvalue = "2";
                }
                if (monthins == "3 Month")
                {
                    monthvalue = "3";
                }
                if (monthins == "4 Month")
                {
                    monthvalue = "4";
                }
                if (monthins == "5 Month")
                {
                    monthvalue = "5";
                }
                if (monthins == "6 Month")
                {
                    monthvalue = "6";
                }
                if (monthins == "7 Month")
                {
                    monthvalue = "7";
                }
                if (monthins == "8 Month")
                {
                    monthvalue = "8";
                }
                if (monthins == "9 Month")
                {
                    monthvalue = "9";
                }
                if (monthins == "10 Month")
                {
                    monthvalue = "10";
                }
                if (monthins == "11 Month")
                {
                    monthvalue = "11";
                }
                bins.Installment_Year = "NO";
                bins.Installment_Month = monthvalue;

            }
           
           
            bins.Installment_Date = dpInvoice_Instalpermonth.SelectedDate .ToString();
            bins.S_Status = "Active";
            bins.C_Date = System.DateTime.Now.ToShortDateString();
            bins.Ins = "Not_Nill";
            dins.Save_Installment(bins);
            MessageBox.Show("Installment Added Succsessfully ");

        }

        private void smaddinstallment_Click(object sender, RoutedEventArgs e)
        {
            GRD_Installment.Visibility = Visibility;
            Load_InstallmentCustomers();
            fetchCustomerID();
            Load_YearMonth();
        }

        private void btnInstall_ExitInstallment_Click(object sender, RoutedEventArgs e)
        {
            GRD_Installment.Visibility = Visibility.Hidden;
        }
        public void fetchCustomerID()
        {
            cmbInstall_CustID.Text = "--Select--";
            string q = "Select tlb_Customer.Cust_ID from tlb_Customer Inner join tlb_MainInstallment On tlb_MainInstallment.Customer_ID =tlb_Customer.ID where tlb_MainInstallment. S_Status='Active'";
            cmd = new SqlCommand(q, con);
            // DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
               // cmbInstall_CustID.SelectedValuePath = ds.Tables[0].Columns["Customer_ID"].ToString();
                cmbInstall_CustID.ItemsSource = ds.Tables[0].DefaultView;
                cmbInstall_CustID.DisplayMemberPath = ds.Tables[0].Columns["Cust_ID"].ToString();
            }
        }
        public void Load_InstallmentCustomers()
        {
          //  cmbInstall_CustID.Text = "--Select--";
            string q = "SELECT tlb_MainInstallment.ID  ,tlb_Customer.Cust_ID,tlb_Customer.Name ,tlb_MainInstallment.Bill_No ,tlb_MainInstallment.Total_Price ,tlb_MainInstallment.Paid_Amount ,tlb_MainInstallment.Balance_Amount ,tlb_MainInstallment.Monthly_Amount,tlb_MainInstallment.Installment_Year ,tlb_MainInstallment.Installment_Month ,tlb_MainInstallment.Installment_Date FROM tlb_MainInstallment INNER JOIN tlb_Customer ON tlb_MainInstallment.Customer_ID =tlb_Customer.ID  and tlb_MainInstallment.S_Status='Active' and tlb_MainInstallment.Ins !='Nill' ";
            cmd = new SqlCommand(q, con);
            // DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // cmbInstall_CustID.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
                DGRD_InstallmentCust.ItemsSource = ds.Tables[0].DefaultView;
                // cmbInstall_CustID.DisplayMemberPath = ds.Tables[0].Columns["Customer_ID"].ToString();
            }
        }
        public void Load_YearMonth()
        {
            cmbInstall_Year_Month.Text = "--Select--";
            cmbInstall_Year_Month.Items.Add("Year");
            cmbInstall_Year_Month.Items.Add("Month");
        }

        private void txtInstall_CustName_TextChanged(object sender, TextChangedEventArgs e)
        {
            InstallmentCustomerDetails_LoadData();
        }
        public void InstallmentCustomerDetails_LoadData()
        {
            try
            {
                String str,str1;
                //str1 = cmbInstall_CustID.SelectedValue.ToString () ;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT tlb_MainInstallment.ID  ,tlb_Customer.Cust_ID,tlb_Customer.Name ,tlb_MainInstallment.Bill_No ,tlb_MainInstallment.Total_Price ,tlb_MainInstallment.Paid_Amount " +
                ",tlb_MainInstallment.Balance_Amount ,tlb_MainInstallment.Monthly_Amount,tlb_MainInstallment.Installment_Year ,tlb_MainInstallment.Installment_Month ,tlb_MainInstallment.Installment_Date " +
                " FROM tlb_MainInstallment " +
                " INNER JOIN tlb_Customer ON tlb_Customer.ID = tlb_MainInstallment.Customer_ID  " +
                //and tlb_MainInstallment.S_Status='Active'
               // str = "SELECT [ID],[DealerEntryID],[CompanyName],[DealerFirstName] + ' ' + [DealerLastName] AS [DealerName],[DateOfBirth],[MobileNo],[PhoneNo],[DealerAddress] " +
                            // "FROM [tbl_DealerEntry] " +
                             "WHERE ";
                if (txtInstall_CustName.Text.Trim() != "")
                {
                    str = str + "tlb_Customer.Name LIKE ISNULL('" + txtInstall_CustName.Text.Trim() + "',tlb_Customer.Name) + '%' AND ";
                }
                //if (cmbInstall_CustID.Text.Equals("--Select--"))
                //{
                //    str = str + " tlb_Customer.Cust_ID LIKE ISNULL('" + cmbInstall_CustID.Text.Trim() + "',tlb_Customer.Cust_ID) + '%' AND ";
                //}
                //if (cmbInstall_CustID.Text ==str1 )
                //{
                //    str = str + " tlb_Customer.Cust_ID LIKE ISNULL('" + cmbInstall_CustID.Text.Trim() + "',tlb_Customer.Cust_ID) + '%' AND ";
                //}
                //if (cmbInstall_Year_Month.Text .Trim() != string.Empty)
                //{
                //    str = str + "[MobileNo] LIKE ISNULL('" + cmbInstall_Year_Month.Text.Trim() + "',MobileNo) + '%' AND ";
                //}
                str = str + " tlb_MainInstallment.S_Status = 'Active' ORDER BY tlb_Customer.Name ASC ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                DGRD_InstallmentCust.ItemsSource = ds.Tables[0].DefaultView;
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
        public void load_CInstallment()
        {
            con.Open();
            DataSet ds = new DataSet();
            string load = "Select ci.ID,c.Cust_ID ,ci.Bill_No ,ci.Total_Price ,ci.Paid_Amount ,ci.Balance_Amount,ci.Monthly_Amount ,ci.Total_Installment_Month ,ci.Current_Installment_No,ci.Remaining_Installments ,ci.Current_Installment_Amount ,ci.CInstallment_Date ,ci.Paid_Unpaid  from tlb_Customer_Installment ci inner join tlb_Customer c ON c.ID = ci.Customer_ID and c_Ins = 'Not_Nill'  ";
            SqlCommand cmd = new SqlCommand(load, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if(ds.Tables [0].Rows .Count >0)
            {
            DGRD_Installment.ItemsSource = ds.Tables[0].DefaultView;
            }
            con.Close();
        }
        private void cmbInstall_CustID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InstallmentCustomerDetails_LoadData();
        }

        #region Old Customer Details
        public void OldCustomer_Details()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT  Distinct [ID],[Cust_ID],[Name],[Mobile_No],[Date_Of_Birth], [Email_ID],[Address],[Occupation] " +
                      "FROM [tlb_Customer]  " +
                      "WHERE ";

                if (txtOldCustomer_Search.Text.Trim() != "")
                {
                    str = str + "[Name] LIKE ISNULL('" + txtOldCustomer_Search.Text.Trim() + "',[Name]) + '%' AND ";
                }
                if (txtOlad_CustomerMobile_Search.Text.Trim() != "")
                {
                    str = str + "[Mobile_No] LIKE ISNULL('" + txtOlad_CustomerMobile_Search.Text.Trim() + "',[Mobile_No]) + '%' AND ";
                }
                str = str + " [S_Status] = 'Active'  ORDER BY [Name] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                DGRD_SaleOldCustomer.ItemsSource = ds.Tables[0].DefaultView;
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

        #region Old Cust Button Event
        private void btnOldCustomer_Refresh_Click(object sender, RoutedEventArgs e)
        {
            txtOldCustomer_Search.Text = "";
            txtOlad_CustomerMobile_Search.Text = "";

            OldCustomer_Details();
        }
        #endregion Old Cust Button Event

        private void txtOlad_CustomerMobile_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            OldCustomer_Details();
        }
        #endregion Old Customer Details

        private void txtOldCustomer_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            OldCustomer_Details();
        }

        private void btndgv_InstEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = DGRD_InstallmentCust.SelectedItem;
                ID =Convert .ToInt32 ( (DGRD_InstallmentCust.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

                cid1 = (DGRD_InstallmentCust.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                MA = Convert.ToDouble((DGRD_InstallmentCust.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text);
                con.Open();
                string w = "Select ID,Cust_ID from tlb_Customer Where Cust_ID='" + cid1 + "' ";
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(w, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                   Cust_id =Convert .ToInt32 (dt.Rows[0]["ID"]);
                }
                con.Close();
               int result=CheckExsistance();
                if(result ==0)
                {
                    string CName = (DGRD_InstallmentCust.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                    double TP = Convert.ToDouble((DGRD_InstallmentCust.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
                    double PA = Convert.ToDouble((DGRD_InstallmentCust.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text);
                    double BA = Convert.ToDouble((DGRD_InstallmentCust.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text);
                  
                    string y = (DGRD_InstallmentCust.SelectedCells[8].Column.GetCellContent(item) as TextBlock).Text;

                    string m = (DGRD_InstallmentCust.SelectedCells[9].Column.GetCellContent(item) as TextBlock).Text;
                    MessageBox.Show(ID.ToString());
                    BillID_fetchnew();
                    GRD_InstallmentProcess.Visibility = Visibility;
                    lbl_Instal_CustomerID.Content = Cust_id;
                    txt_InstalCustomerName.Text = CName;
                    txt_InstalTotalAmount.Text = TP.ToString();
                    txt_InstalPaidAmount.Text = PA.ToString();
                    txt_InstalBalanceAmount.Text = BA.ToString();
                    txtInstalAmountPermonth.Text = MA.ToString();
                    if (y != "NO" && m == "NO")
                    {
                        lbl_InstalY_M.Content = y;
                    }
                    else if (y == "NO" && m != "NO")
                    {
                        lbl_InstalY_M.Content = m.ToString();
                    }
                    txt_Installemntno.Text = 1.ToString();
                    int b =Convert .ToInt32 ( txt_Installemntno.Text);
                    int c = Convert .ToInt32 (lbl_InstalY_M.Content.ToString ());
                    int a = c - b;
                    txt_InstallmentRemaining.Text = a.ToString();

                }
                else if(result ==1)
                {
                    load_CInstallment();
                }
              

            }
            catch (Exception)
            {
                
                throw;
            }
            finally { con.Close(); }
           
        }
        public int CheckExsistance()
        {
            try
            {
               int  exist;
                con.Open();
                DataTable dt = new DataTable();
                string chk = "Select Customer_ID from tlb_Customer_Installment where Customer_ID='" + Cust_id + "' and S_Status='Active' and c_Ins ='Not_Nill' ";
                SqlCommand cmd = new SqlCommand(chk, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    exist = 1;
                }
                else
                {
                    exist = 0;
                }
                return exist;
            }
            catch (Exception)
            {
                
                throw;
            }
            finally { con.Close(); }
        }
        private void btn_InstalExit_Click(object sender, RoutedEventArgs e)
        {
            GRD_InstallmentProcess.Visibility = Visibility.Hidden;
            Clear_Installmentsgrd();
            DGRD_Installment.ItemsSource = "";
            InstallmentCustomerDetails_LoadData();
            DGRD_InstallmentCust.ItemsSource = null;
            Load_InstallmentCustomers();
        }

      public void Save_Customer_Installment()
        {
            try
            {
                bcins.Flag = 1;
                bcins.Customer_ID = Cust_id;
                bcins.Bill_No = lblbillnoInstall.Content.ToString();
                bcins.Total_Price = Convert.ToDouble(txt_InstalTotalAmount.Text);
                bcins.Paid_Amount = Convert.ToDouble(txt_InstalPaidAmount.Text);
                bcins.Balance_Amount = Convert.ToDouble(txt_InstalBalanceAmount.Text);
                bcins.Monthly_Amount = Convert.ToDouble(txtInstalAmountPermonth.Text);
                bcins.Total_Installment_Month = lbl_InstalY_M.Content.ToString();
                bcins.Current_Installment_No = txt_Installemntno.Text ;
                bcins.Remaining_Installments = txt_InstallmentRemaining.Text ;
                bcins.Current_Installment_Amount = Convert.ToDouble(txt_InstallmentAmount.Text);
                bcins.CInstallment_Date = dp_Instalpermonth.Text;
                bcins.Paid_Unpaid = "Paid";
                bcins.S_Status = "Active";
                bcins.C_Date = System.DateTime.Now.ToShortDateString();
                bcins.c_Ins = "Not_Nill";
                dcins.Save_C_Installment(bcins);
                MessageBox.Show("Installment Added Succsessfully", caption, MessageBoxButton.OK);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
      public void BillID_fetchnew()
      {

          int id1 = 0;

          con.Open();
          SqlCommand cmd = new SqlCommand("Select (COUNT(ID)) from tlb_Bill_No", con);
          id1 = Convert.ToInt32(cmd.ExecuteScalar());
          id1 = id1 + 1;

          lblbillnoInstall.Content = "Bill No/" + id1.ToString();


          // txtvalueid.Text = "Bill No 786/ " + id1.ToString();
          //  txtvalueid.Text = "Bill No 786/ " + id1.ToString();

          con.Close();

      }
      private void btn_InstalSaveandPrint_Click(object sender, RoutedEventArgs e)
      {
          Save_Customer_Installment();
          Save_CommonBillNew();
          if ((txt_InstalBalanceAmount.Text == "0") || (txt_InstalBalanceAmount.Text =="0.00") || (txt_InstalBalanceAmount.Text ==""))
          {
              con.Open();
              string udt = "update  tlb_MainInstallment set Ins='Nill' where  Customer_ID ='" + Cust_id + "'  ";
              cmd = new SqlCommand(udt, con);
              cmd.ExecuteNonQuery();
              con.Close();
              MessageBox.Show("Data Updated Successfully", caption, MessageBoxButton.OK);

              con.Open();
              string udtc = "update  tlb_Customer_Installment set c_Ins='Nill' where  Customer_ID ='" + Cust_id + "' and Bill_No='" + lblbillnoInstall.Content.ToString() + "' ";
              cmd = new SqlCommand(udtc, con);
              cmd.ExecuteNonQuery();
              con.Close();
              MessageBox.Show("Data Updated Successfully", caption, MessageBoxButton.OK);
          }
          Clear_Installmentsgrd();
          GRD_InstallmentProcess.Visibility = Visibility.Hidden;
      }
     public void Save_CommonBillNew()
      {
          binvd.Flag = 1;
          binvd.Customer_ID = Cust_id;
          binvd.Bill_No = lblbillnoInstall.Content.ToString();
          binvd.Payment_Mode = "Installment_Nos";
          binvd.Total_Price =Convert .ToDouble ( txt_InstalTotalAmount.Text);
          binvd.Paid_Amount = Convert.ToDouble(txt_InstalPaidAmount.Text);
          binvd.Balance_Amount = Convert.ToDouble(txt_InstalBalanceAmount.Text);
          binvd.S_Status = "Active";
          binvd.C_Date = System.DateTime.Now.ToShortDateString();
          dinvd.CommonBillNo_Save(binvd);
          MessageBox.Show("Common bill Added", caption, MessageBoxButton.OK);
      }
      private void txt_InstallmentAmount_TextChanged(object sender, TextChangedEventArgs e)
     {
      //    double amt = Convert.ToDouble(txt_InstallmentAmount.Text);
      //    double pamt = Convert.ToDouble(txt_InstalPaidAmount.Text);
      //    double upamt = amt + pamt;
      //    txt_InstalPaidAmount.Text = upamt.ToString();
      //    double bamt = Convert.ToDouble(txt_InstalBalanceAmount .Text );
      //    double ubamt = bamt - amt;
      //    txt_InstalBalanceAmount.Text = ubamt.ToString();
      }

      private void txt_InstallmentAmount_LostFocus(object sender, RoutedEventArgs e)
      {
          double amt = Convert.ToDouble(txt_InstallmentAmount.Text);
          double pamt = Convert.ToDouble(txt_InstalPaidAmount.Text);
          double upamt = amt + pamt;
          txt_InstalPaidAmount.Text = upamt.ToString();
          double bamt = Convert.ToDouble(txt_InstalBalanceAmount.Text);
          double ubamt = bamt - amt;
          txt_InstalBalanceAmount.Text = ubamt.ToString();
      }

      private void btndgv_InstCustEdit_Click(object sender, RoutedEventArgs e)
      {
          try
          {
              object item = DGRD_Installment.SelectedItem;
              ID = Convert.ToInt32((DGRD_Installment.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
              cid1 = (DGRD_Installment.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
              con.Open();
              string w = "Select ID,Cust_ID from tlb_Customer Where Cust_ID='" + cid1 + "' ";
              DataTable dt = new DataTable();
              SqlCommand cmd = new SqlCommand(w, con);
              SqlDataAdapter da = new SqlDataAdapter(cmd);
              da.Fill(dt);
              if (dt.Rows.Count > 0)
              {
                  Cust_id = Convert.ToInt32(dt.Rows[0]["ID"]);
              }
              con.Close();
            //  string CName = (DGRD_Installment.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
              string bno = (DGRD_Installment.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
              double TP1 = Convert.ToDouble((DGRD_Installment.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text);
              double PA1 = Convert.ToDouble((DGRD_Installment.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
              double BA1 = Convert.ToDouble((DGRD_Installment.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text);
             // double MA1 = Convert.ToDouble((DGRD_Installment.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text);
             // string tinsno = (DGRD_Installment.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;
              int cino=Convert .ToInt32 ( (DGRD_Installment.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text);
                string rino=(DGRD_Installment.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;
                string cinamt=(DGRD_Installment.SelectedCells[8].Column.GetCellContent(item) as TextBlock).Text;
                string cinod=(DGRD_Installment.SelectedCells[9].Column.GetCellContent(item) as TextBlock).Text;
                string paid=(DGRD_Installment.SelectedCells[10].Column.GetCellContent(item) as TextBlock).Text;
             
                MessageBox.Show(ID.ToString());
                BillID_fetchnew();
                GRD_InstallmentProcess.Visibility = Visibility;
                lbl_Instal_CustomerID.Content = Cust_id;
                txt_InstalCustomerName.Text = CName;
                txt_InstalTotalAmount.Text = TP1.ToString();
                txt_InstalPaidAmount.Text = PA1.ToString();
                txt_InstalBalanceAmount.Text = BA1.ToString();
                txtInstalAmountPermonth.Text = MA.ToString();
               cino = cino + 1;
                int r = Convert.ToInt32(rino);
                txt_Installemntno.Text = cino.ToString ();
               
                rino = (r- 1).ToString ();
              txt_InstallmentRemaining.Text = rino;
          }
          catch (Exception)
          {
              
              throw;
          }
      }
   public void Clear_Installmentsgrd()
      {
          lbl_Instal_CustomerID.Content = "";
          txt_InstalCustomerName.Text = "";
          txt_InstalTotalAmount.Text = "";
          txt_InstalPaidAmount.Text = "";
          txt_InstalBalanceAmount.Text = "";
          txtInstalAmountPermonth.Text = "";
          lblbillnoInstall.Content = "";
          txt_Installemntno.Text = "";        
          txt_InstallmentRemaining.Text = "";
          txt_InstallmentAmount.Text = "";
          dp_Instalpermonth.Text = "";
      }

   private void btn_InstalClear_Click(object sender, RoutedEventArgs e)
   {
       Clear_Installmentsgrd();
   }

   private void btnInvoice_InstalClear_Click(object sender, RoutedEventArgs e)
   {
       Clear_SaveInstallment();
   }

   private void chkisinterested_Checked(object sender, RoutedEventArgs e)
   {
       object item = DGRD_SaleOldCustomer.SelectedItem;
            string ID = (DGRD_SaleOldCustomer.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            MessageBox.Show(ID);
            // DGRD_SaleFollowup;
            grd_OldCustomerDetails.Visibility = Visibility.Hidden;
            GRD_Customer_Billing.Visibility = Visibility;
           // CustomerID_fetch();

            //txtvalueid.Text = ID;
            lblfollowupidfetch.Content = ID;


            try
            {
                con.Open();
               

                // DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("Select Cust_ID, Name ,Mobile_No,Date_Of_Birth,Email_ID,Address,Occupation from tlb_Customer where ID='" + ID + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // DGRD_SaleFollowup.ItemsSource = ds.Tables[0].DefaultView;
                    txtvalueid.Text =dt.Rows [0]["Cust_ID"].ToString ();
                    txtSalecustomerName.Text = dt.Rows[0]["Name"].ToString();
                    txtSaleCustomerMobileno.Text = dt.Rows[0]["Mobile_No"].ToString();
                    dpSaleCustomerDOB.Text = dt.Rows[0]["Date_Of_Birth"].ToString();
                    txtSaleCustomerEmailID.Text = dt.Rows[0]["Email_ID"].ToString();
                    txtSaleCustomerAddress.Text = dt.Rows[0]["Address"].ToString();
                    txtSaleCustomerOccupation.Text = dt.Rows[0]["Occupation"].ToString();


                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }

   private void txtOldCustomer_Search_TextChanged_1(object sender, TextChangedEventArgs e)
   {
       OldCustomer_Details();
   }

   private void txtOlad_CustomerMobile_Search_TextChanged_1(object sender, TextChangedEventArgs e)
   {
       OldCustomer_Details();
   }

   private void btnsaleRefresh_Click(object sender, RoutedEventArgs e)
   {
       txtsalesearchcname.Text = "";
       txtSalecustomerno.Text = "";
       FetchallDetails();
   }

   private void GRD_Customer_Billing_Loaded(object sender, RoutedEventArgs e)
   {
       grd_OldCustomerDetails.Visibility = Visibility.Hidden;
       GRD_FollowupCostomer.Visibility = Visibility.Hidden;
       if (rdosalefollowupcustomer.IsChecked ==true )
       {
           btnSaleCustomerUpdate.IsEnabled = false;
           btnSaleCustomerDelete.IsEnabled = false;
           btnSaleNewCustomerSale.IsEnabled = false;
           btnSaleCustomerEditoccu.IsEnabled = false;


       }
       else if (rdoSaleOldCustomer1.IsChecked ==true )
       {
           btnSaleCustomerUpdate.IsEnabled = false;
           btnSaleCustomerDelete.IsEnabled = false;
           btnSaleNewCustomerSale.IsEnabled = false;
           btnSaleCustomerEditoccu.IsEnabled = false;
       }
       else if (rdoSaleNewcustomer.IsChecked==true )
       {
           
       }
       SourceOfEnquiry();
      //Load_EmployeeDetails();
   }

   public void Load_EmployeeDetails()
   {
       //  cmbInstall_CustID.Text = "--Select--";
       string q = "SELECT ID ,EmployeeName FROM tbl_Employee ";
       cmd = new SqlCommand(q, con);
       // DataTable dt = new DataTable();
       DataSet ds = new DataSet();
       SqlDataAdapter adp = new SqlDataAdapter(cmd);

       adp.Fill(ds);
       if (ds.Tables[0].Rows.Count > 0)
       {
           cmbInvoiceEmployeename.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();
           cmbInvoiceEmployeename.ItemsSource = ds.Tables[0].DefaultView;
           cmbInvoiceEmployeename.DisplayMemberPath = ds.Tables[0].Columns["EmployeeName"].ToString();
       }
   }

   public bool NewCustomer_Validation()
   {
       bool result = false;
       //if(cmbCustomer_EmployeeName.SelectedItem == null)
       //{
       //    result = true;
       //    MessageBox.Show("Please Select Employee Name", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
       //}
       //else
       if (txtSalecustomerName.Text == "")
       {
           result = true;
           MessageBox.Show("Please Enter Customer Name", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
       }
       else if (txtSaleCustomerMobileno.Text == "")
       {
           result = true;
           MessageBox.Show("Please Enter Customer Mobile No", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
       }
       else if (txtSaleCustomerAddress.Text == "")
       {
           result = true;
           MessageBox.Show("Please Enter Customer Address", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
       }
       else if (dpSaleCustomerDOB.Text == "")
       {
           result = true;
           MessageBox.Show("Please Select Customer Date of Birth", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
       }
       else if (cmbSourceOfEnquery.SelectedItem == null)
       {
           result = true;
           MessageBox.Show("Please Select Source Of Enquiry", caption, MessageBoxButton.OK, MessageBoxImage.Stop);
       }
       return result;
   }

   #region ChartFunction
   int folCount = 0;
   int sealsCount = 0;
   int finalPro = 0;
   int baseCust = 0;
   int highProduct = 0;
   int highSingleProduct = 0;
   string highSourceNPR;
   int abc = 0;
   int checkhighProduct;
   int chhighProduct;

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

           chhighProduct = Convert.ToInt32(cmd.ExecuteScalar());
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
           if(chhighProduct > 0)
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

   public void Chart_Ceck_HighestSingleProduct()
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
           checkhighProduct = Convert.ToInt32(cmd.ExecuteScalar());
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
           Chart_Ceck_HighestSingleProduct();
           if(checkhighProduct > 0)
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

   public void Chart_BestEnquerySource()
   {
       try
       {
           String str;
           con.Open();
           DataSet ds = new DataSet();
           //str = "SELECT distinct Count(I.C_Date) AS [CDate],B.[Brand_Name] FROM [tlb_InvoiceDetails] I INNER JOIN [tlb_Brand] B ON B.[ID]=I.[Brand_ID] WHERE I.[S_Status]='Active' Group By B.[Brand_Name]";
           str = "SELECT MAX(SourceOfEnquiry) FROM [tlb_Customer] WHERE [S_Status]='Active'";
           SqlCommand cmd = new SqlCommand(str, con);
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           da.Fill(ds);

           highSourceNPR = Convert.ToString(cmd.ExecuteScalar());
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


   private void LoadColumnChart_FollowUp()
   {
       ((ColumnSeries)mcChart.Series[0]).ItemsSource = new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string,int>("Walk ins", folCount),
                new KeyValuePair<string,int>("Sales", sealsCount),
                new KeyValuePair<string,int>("Procurements", finalPro),
                new KeyValuePair<string,int>("Highest Sold Item", highSingleProduct) ,
                new KeyValuePair<string,int>("Ever Green Top Brand", highProduct),
                //new KeyValuePair<string,int>("Best Enquiry Source", abc),
                new KeyValuePair<string,int>("Customer Base", baseCust)
                
            };
   }

   #endregion ChartFunction

   private void btnSaleCustomerExit_Click(object sender, RoutedEventArgs e)
   {
       GRD_Customer_Billing.Visibility = Visibility.Hidden;
   }

   private void DGRD_Installment_SelectionChanged(object sender, SelectionChangedEventArgs e)
   {

   }

   private void cmbInstall_Year_Month_SelectionChanged(object sender, SelectionChangedEventArgs e)
   {

   }

   private void btnInvoice_CH_Clear_Click(object sender, RoutedEventArgs e)
   {
       clear_AllCheque();
   }
public void clear_AllCheque()
   {
       btnInvoice_CH_InvcTAmount.Text = "";
       btnInvoice_CH_chequeno.Text = "";
       btnInvoice_CH_Amount.Text = "";
       dpInvoice_CH_ChequeDate.Text = "";
    cmbInvoic_CH_BankName.ItemsSource =null;
    FetchBankName();
   }

private void cmbInvoic_CH_BankName_SelectionChanged(object sender, SelectionChangedEventArgs e)
{

}

private void rdo_ViewWalkinsAll_Checked(object sender, RoutedEventArgs e)
{
    if(rdo_ViewWalkinsAll .IsChecked ==true )
    {
        clear_VW();
        dp_View_walkinTodate.IsEnabled = false;
        dp_View_walkinFromdate.IsEnabled = false;
        btn_View_walkinRefresh.IsEnabled = false;
      
        txt_View_walkinName.IsEnabled = false;
        txt_View_walkinNumber.IsEnabled = false;
        FetchallDetailsofFollowupwalkins();
    }
}

private void rdo_ViewWalkinsName_Checked(object sender, RoutedEventArgs e)
{
    if (rdo_ViewWalkinsName.IsChecked==true )
    {
        clear_VW();
        txt_View_walkinName.IsEnabled = true ;
        txt_View_walkinNumber.IsEnabled = true ;
        dp_View_walkinTodate.IsEnabled = false;
        dp_View_walkinFromdate.IsEnabled = false;
        btn_View_walkinRefresh.IsEnabled = true ;
        
        FetchallDetailsofFollowupwalkins();

    }
}

private void rdo_View_WalkinsDate_Checked(object sender, RoutedEventArgs e)
{
    if (rdo_View_WalkinsDate.IsChecked ==true )
    {
        clear_VW();
        txt_View_walkinName.IsEnabled = false ;
        txt_View_walkinNumber.IsEnabled = false ;
        dp_View_walkinTodate.IsEnabled = true ;
        dp_View_walkinFromdate.IsEnabled = true ;
        btn_View_walkinRefresh.IsEnabled = true;
        
        FetchallDetailsofFollowupwalkins();
    }
}

private void btn_view_walkinExit_Click(object sender, RoutedEventArgs e)
{
    Grd_View_Walkins.Visibility = Visibility.Hidden ;
}

private void smviewwalkin_Click(object sender, RoutedEventArgs e)
{
    Grd_View_Walkins.Visibility = Visibility;
    if (rdo_ViewWalkinsAll.IsChecked == true)
    {
        FetchallDetailsofFollowupwalkins();
    }
    else if (rdo_ViewWalkinsName.IsChecked == true)
    {
        txt_View_walkinName.IsEnabled = true;
        txt_View_walkinNumber.IsEnabled = true;
        dp_View_walkinTodate.IsEnabled = false;
        dp_View_walkinFromdate.IsEnabled = false;
        btn_View_walkinRefresh.IsEnabled = true;
        
        FetchallDetailsofFollowupwalkins();
    }
    else if (rdo_View_WalkinsDate.IsChecked == true)
    {
        txt_View_walkinName.IsEnabled = false;
        txt_View_walkinNumber.IsEnabled = false;
        dp_View_walkinTodate.IsEnabled = true;
        dp_View_walkinFromdate.IsEnabled = true;
        btn_View_walkinRefresh.IsEnabled = true;
        
        FetchallDetailsofFollowupwalkins();
    }
   
}
public void FetchallDetailsofFollowupwalkins()
{
    try
    {
        con.Open();
        DataSet ds = new DataSet();
        cmd = new SqlCommand("select ID, Followup_ID,Name,Mobile_No,Date_Of_Birth,Email_ID,Address,Product_Details,Followup_Type,F_Date,C_Date from tlb_FollowUp  where  S_Status='Active'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // con.Open();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GRD_View_Walkin.ItemsSource = ds.Tables[0].DefaultView;
        }
    }
    catch (Exception)
    {

        throw;
    }
    finally { con.Close(); }


}

private void txt_View_walkinName_TextChanged(object sender, TextChangedEventArgs e)
{
    loadbynamenno_Followupvw();
}

private void txt_View_walkinNumber_TextChanged(object sender, TextChangedEventArgs e)
{
    loadbynamenno_Followupvw();
}
public void loadbynamenno_Followupvw()
{
    try
    {
        con.Open();
        DataSet ds = new DataSet();
        string strf = "select ID, Followup_ID , Name , Mobile_No , Date_Of_Birth , Email_ID , Address , Product_Details , Followup_Type , F_Date , C_Date " +
            "from tlb_FollowUp " +
            "where  ";
        if (txt_View_walkinName.Text.Trim() != "")
        {
            strf = strf + " Name LIKE ISNULL('" + txt_View_walkinName.Text.Trim() + "',[Name]) + '%' AND ";
        }
        if (txt_View_walkinNumber.Text.Trim() != "")
        {
            strf = strf + " Mobile_No LIKE ISNULL('" + txt_View_walkinNumber.Text.Trim() + "',[Mobile_No]) + '%' AND ";
        }
        strf = strf + " S_Status = 'Active' ORDER BY [Name] ASC ";
        cmd = new SqlCommand(strf, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // con.Open();
        da.Fill(ds);

        // if (ds.Tables[0].Rows.Count > 0)
        // {
        GRD_View_Walkin.ItemsSource = ds.Tables[0].DefaultView;
        // }
    }
    catch (Exception)
    {

        throw;
    }
    finally { con.Close(); }
}
        public void clear_VW()
{
    txt_View_walkinName.Text = "";
    txt_View_walkinNumber.Text = "";
    dp_View_walkinTodate.Text = "";
    dp_View_walkinFromdate.Text = "";
   
}

        private void btn_View_walkinRefresh_Click(object sender, RoutedEventArgs e)
        {
            if( btn_View_walkinRefresh.IsEnabled == true)
            {
                if(rdo_ViewWalkinsName.IsChecked == true)
                {
                    txt_View_walkinName.Text = "";
                    txt_View_walkinNumber.Text = "";

                }
                else if (rdo_View_WalkinsDate.IsChecked == true)
                {
                    dp_View_walkinTodate.Text = "";
                    dp_View_walkinFromdate.Text = "";
                }
            }
        }
        public void fetchByDate_VW()
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string strf = "select ID, Followup_ID , Name , Mobile_No , Date_Of_Birth , Email_ID , Address , Product_Details , Followup_Type , F_Date , C_Date " +
                    "from tlb_FollowUp " +
                    "where  ";
                //if (dp_View_walkinFromdate.Text.Trim() != "")
                //{
                //    strf = strf + " C_Date LIKE ISNULL('" + dp_View_walkinFromdate.Text.Trim() + "',[C_Date]) + '%' AND ";
                //}
                //if (dp_View_walkinTodate.Text.Trim() != "")
                //{
                //    strf = strf + " C_Date LIKE ISNULL('" + dp_View_walkinTodate.Text.Trim() + "',[C_Date]) + '%' AND ";
                //}
                if (dp_View_walkinFromdate.Text.Trim() != "" && dp_View_walkinTodate.Text.Trim() != "")
                {
                    strf = strf + " C_Date BETWEEN  ('" + dp_View_walkinFromdate.Text + "') AND ( '" + dp_View_walkinTodate.Text + "') + '%' AND  ";
                }
                strf = strf + " S_Status = 'Active' ORDER BY [Name] ASC ";
                cmd = new SqlCommand(strf, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // con.Open();
                da.Fill(ds);

                // if (ds.Tables[0].Rows.Count > 0)
                // {
                GRD_View_Walkin.ItemsSource = ds.Tables[0].DefaultView;
                // }
            }
            catch (Exception)
            {

                throw;
            }
            finally { con.Close(); }
        }

        private void dp_View_walkinFromdate_TextInput(object sender, TextCompositionEventArgs e)
        {
           
        }

        private void dp_View_walkinFromdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FetchallDetailsofFollowupwalkins();
            fetchByDate_VW();
        }

        private void dp_View_walkinTodate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FetchallDetailsofFollowupwalkins();
            fetchByDate_VW();
        }

        private void smviewsale_Click(object sender, RoutedEventArgs e)
        {
            grd_SaleDetails.Visibility = Visibility;
           

            SaleCustomer_Details();
        }

        private void btnAdm_SaleCustDetails_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_SaleDetails.Visibility = System.Windows.Visibility.Hidden;
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

        private void txtAdm_SaleCustBillNo_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaleCustomer_Details();
        }

        private void txtAdm_SaleCustDetails_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaleCustomer_Details();
        }

        private void txtAdm_SaleCustMobileNo_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
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

        private void dgvAdm_FinalProcurement_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

       //======== Load Notification start ===========
        public void Birthday_Notification()
        {
            fetch_C_Birthdays();
           // Calculate_CBday();
        }
        public void fetch_C_Birthdays()
        {
            try
            {int cnt1 = 0;
                con.Open();
                string sqlquery1 = "SELECT ID,Cust_ID,Name,Date_Of_Birth from tlb_Customer where S_Status='Active' ";
                SqlCommand cmd = new SqlCommand(sqlquery1, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                for (int i = 0; i < dt.Rows .Count ; i++)
                {
                    
                     int id =Convert .ToInt32 ( dt.Rows[0]["ID"].ToString());
                    string cid = dt.Rows[0]["Cust_ID"].ToString();
                    string nam = dt.Rows[0]["Name"].ToString();
                    dob = dt.Rows[0]["Date_Of_Birth"].ToString();
                    Calculate_CBday();
                    if (txtdiffdate.Text == "0")
                    {
                        MessageBox.Show("After 2 days  '" + nam + "' have a Birthday Dated on '" + dob + "'");
                        cnt1 = cnt1 + 1;
                    }
                }

                txtnoti.Text = cnt1.ToString();
                lblcalert.Content = cnt1.ToString();
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

        }
        public void Calculate_CBday()
                    {
            DateTime commondate1=Convert .ToDateTime (CommonDate);
            DateTime dob1 = Convert.ToDateTime(dob);
           //CRM_DAL.
            DateDiff dateDifference = new DateDiff(commondate1, dob1);
      txtdiffdate.Text =  dateDifference.ToString();

            //DateDiff dateDifference = new DateDiff(this.dateTimeTo.Value, this.dateTimeFrom.Value);
            //this.lblOutput.Text = "Difference between " + this.dateTimeFrom.Value.ToShortDateString()
            //                    + " and " + this.dateTimeTo.Value.ToShortDateString() + " is :\n"
            //                    + dateDifference.ToString();
        }

        private void rdo_AlertsCustomer_Checked(object sender, RoutedEventArgs e)
        {
           
          
        }

        private void rdo_AlertsDealer_Checked(object sender, RoutedEventArgs e)
        {
         
        }

        private void rdo_AlertsEmployee_Checked(object sender, RoutedEventArgs e)
        {
          
        }

        private void btnAlertExit_Click(object sender, RoutedEventArgs e)
        {
            GRD_AllertCustomer.Visibility = Visibility.Hidden;
        }
        public void FetchCustomerBday_Alert()
        {
            try
            {
                string str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT  Distinct [ID],[Cust_ID],[Name],[Mobile_No],[Date_Of_Birth], [Email_ID],[Address],[Occupation] " +
                      "FROM [tlb_Customer]  " +
                      "WHERE ";
                str = str + " [S_Status] = 'Active' and [Date_Of_Birth]= '" + CommonDate + "'  ORDER BY [Name] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                DGRD_AlertCust.ItemsSource = ds.Tables[0].DefaultView;
                }
                else { MessageBox.Show("No Rows Found !!!!!"); }
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

        private void malerts_Click(object sender, RoutedEventArgs e)
        {
          
           
        }

        private void rdo_AlertsCustomerBirthday_Checked(object sender, RoutedEventArgs e)
        {
            DGRD_AlertCust.Visibility = Visibility;
            FetchCustomerBday_Alert();
        }

        private void chkSend_Message_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void chkSend_MessageC_Checked(object sender, RoutedEventArgs e)
        {
             
        }

        private void btnDefaultBirthAlert_Click(object sender, RoutedEventArgs e)
        {
            object item = DGRD_AlertCust.SelectedItem;
            frmCustomerBirthdayAlert fcba = new frmCustomerBirthdayAlert();
            fcba.cid_CAB = (DGRD_AlertCust.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            fcba. cname_CAB = (DGRD_AlertCust.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
            fcba. cphone_CAB = (DGRD_AlertCust.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
            fcba .cdob_CAB = (DGRD_AlertCust.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
            fcba.Show();

         //   MessageBox.Show(ID);

        }

        private void smcustomer_Click(object sender, RoutedEventArgs e)
        {
            GRD_AllertCustomer.Visibility = Visibility;
        }

        private void smdealer_Click(object sender, RoutedEventArgs e)
        {
            GRD_AlertsDealer.Visibility = Visibility;
        }

        private void smcemployee_Click(object sender, RoutedEventArgs e)
        {
            GRD_AlertEmployee.Visibility = Visibility;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
   }
   
}



