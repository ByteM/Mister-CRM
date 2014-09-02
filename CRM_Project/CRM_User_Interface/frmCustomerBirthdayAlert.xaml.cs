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
using System.IO;
using System.Net;
using System.Collections.Specialized;

using CRM_BAL;
using CRM_DAL;


namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for frmCustomerBirthdayAlert.xaml
    /// </summary>
    public partial class frmCustomerBirthdayAlert : Window
    {
         public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        SqlDataReader dr;
        public string cid_CAB = "", cname_CAB = "", cphone_CAB = "", cdob_CAB = "",camt_CAB="", cdob_CProduct = "",camt_DCMessage="", message_Boday;
        public string caption = "Green Future Glob";
        public string dmMesg = "", date;
        public frmCustomerBirthdayAlert()
        {
            InitializeComponent();
        }
        BAL_Alerts balart = new BAL_Alerts();
        DAL_Alerts dalart = new DAL_Alerts();
       // public string alerttype = camt_DCMessage;
        private void btncustomerBirthdayExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //frmCustomerBirthdayAlert cba = new frmCustomerBirthdayAlert();
            //cba.Close();
        }

        private void btnCBA_Send_Click(object sender, RoutedEventArgs e)
        { if (txtCBA_Message.Text == "")
            {
                MessageBox.Show("Please Enter Message....");
            }
        else if (txtCBA_Message.Text != "")
        {
            Preview();
            SendSMS();
            string result = (MessageBox.Show("Message Send Successfully", caption, MessageBoxButton.OKCancel)).ToString();

            if (result == "OK")
            {
                Save_Alerts();
                this.Close();
            }
            else if (result == "Cancel")
            {
                MessageBox.Show("Please Click on Exit Button For Further Process");
            }
        }

        }

        public void Save_Alerts()
        {
            balart.Flag = 1;
            balart.C_F_Id = lblCBA_CID.Content.ToString ();
            balart.Name = lblCBA_CName.Content.ToString();
            balart.To_Mobile_No = txtCBA_To.Text;
            if(rdoSendAlertMessageDefault .IsChecked ==true )
            {
                balart.Message_Type = "Default";
            }
            else if(rdoSendAlertMessageCustom .IsChecked ==true )
            {
                balart.Message_Type = "Custome";
            }
            balart.SMS = txtCBA_Message.Text;
            balart.OnDate = System.DateTime.Now.ToShortDateString();
            balart.OnTime = System.DateTime.Now.ToShortTimeString();
            balart.Alert_Type = camt_DCMessage;
            balart.From_Type = lblCBA_From.Content.ToString();
            balart.S_Status = "Active";
            balart.C_Date = System.DateTime.Now.ToShortDateString();
            dalart.Save_Alert_SMSTransaction(balart);
            MessageBox .Show ("Alerts Added Sucsessfully",caption , MessageBoxButton .OK );
        }
        public void SendSMS()
        {

            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                String sendToPhoneNumber =txtCBA_To .Text ;
                String userid = "2000134498";
                String passwd = "Ypz9O0pCJ";
                String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?method=sendMessage&send_to=" + sendToPhoneNumber + " &msg=" + message_Boday + "  &userid=" + userid + "&password=" + passwd +
                             "&v=1.1" + "&msg_type=TEXT&auth_scheme=plain ";
                request = WebRequest.Create(url);
                //in case u work behind proxy, uncomment the 
                //commented code and provide correct details 
                /*WebProxy proxy = new WebProxy("http://proxy:80/",true); 
                proxy.Credentials =    new 
                NetworkCredential("userId","password", "Domain"); 
                request.Proxy = proxy;*/
                // Send the 'HttpWebRequest' and wait for response.
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new
                System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                Console.WriteLine(result);
                reader.Close();
                stream.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
           // MessageBox.Show("Message Send Successfully", "Byte Machine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             date = System.DateTime.Now.ToShortDateString();
            if (cid_CAB != "" && cname_CAB != "" && cphone_CAB != "" && cdob_CAB != "" && camt_DCMessage !="" )
            {
                lblCBA_CID.Content = cid_CAB;
                lblCBA_CName.Content = cname_CAB;
                lblCBA_CDOB.Content = cdob_CAB;
                lblCBA_CProduct.Content  = cdob_CProduct;
                txtCBA_To.Text = cphone_CAB;
                dmMesg = camt_DCMessage;
            }
            else
            {
                MessageBox.Show("Data Not Select Properly");
            }
        }
        public void Preview()
        {
            message_Boday = " Dear Customer " + lblCBA_CName.Content + "," + "\n" +   
       " " + txtCBA_Message.Text + "\n" +
         "From :" + lblCBA_From.Content + "\n"+
              " Dated " + lblCBA_CDOB.Content + "\n" ;

        }

        private void btnCBA_Preview_Click(object sender, RoutedEventArgs e)
        {
            if (txtCBA_Message.Text == "")
            {
                MessageBox.Show("Please Enter Message....");
            }
            else if (txtCBA_Message.Text != "")
            {
                Preview();
                MessageBox.Show(message_Boday );
              //  txtCBA_MessagePreview.Visibility = Visibility;
               // txtCBA_MessagePreview.Text = message_Boday;
            }

        }

        private void btnCBA_Clear_Click(object sender, RoutedEventArgs e)
        {
            txtCBA_Message.Text = "";
        }

        private void rdoSendAlertMessageDefault_Checked(object sender, RoutedEventArgs e)
        {
            if(dmMesg =="CB")
            {
                FetchMsg_CB();
            }
            else if (dmMesg == "CF")
            {
                FetchMsg_CF();
            }
            else if (dmMesg == "CCB")
            {
                FetchMsg_CCB();
            }
            else if (dmMesg == "CChB")
            {
                FetchMsg_CChB();
            }
            else if (dmMesg == "DB")
            {
                FetchMsg_DB();
            }
            else if (dmMesg == "EB")
            {
                FetchMsg_EB();
            }
            else if (dmMesg == "Install")
            {
                FetchMsg_Install();
            }

        }
       public void FetchMsg_CB()
        {
            try
            {
                con.Open();
                string sqlquery1 = "Select ID,SMSMessage from tlb_DefaultCustomerBirthdaySMS where S_Status='Active' ";
                SqlCommand cmd = new SqlCommand(sqlquery1, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
               if(dt.Rows .Count >0)
               {
                   txtCBA_Message.Text = dt.Rows[0]["SMSMessage"].ToString();
               }

                    
            }
            catch (Exception)
            {
                
                throw;
            }
           finally {con.Close ();}
        }
       public void FetchMsg_CF()
       {
           try
           {
               con.Open();
               string sqlquery1 = "Select ID,SMSMessage from tlb_CustomerFollowupDefaultSMS where S_Status='Active' ";
               SqlCommand cmd = new SqlCommand(sqlquery1, con);
               SqlDataAdapter adp = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               adp.Fill(dt);
               if (dt.Rows.Count > 0)
               {
                   txtCBA_Message.Text = dt.Rows[0]["SMSMessage"].ToString();
               }


           }
           catch (Exception)
           {

               throw;
           }
           finally { con.Close(); }
       }
       public void FetchMsg_CCB()
       {
           try
           {
               con.Open();
               string sqlquery1 = "Select ID,SMSMessage from tlb_CustomerBalance where S_Status='Active' ";
               SqlCommand cmd = new SqlCommand(sqlquery1, con);
               SqlDataAdapter adp = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               adp.Fill(dt);
               if (dt.Rows.Count > 0)
               {
                   txtCBA_Message.Text = dt.Rows[0]["SMSMessage"].ToString();
               }


           }
           catch (Exception)
           {

               throw;
           }
           finally { con.Close(); }
       }

       public void FetchMsg_CChB()// remaining change table of cheque table
       {
           try
           {
               con.Open();
               string sqlquery1 = "Select ID,SMSMessage from tlb_CustomerBalance where S_Status='Active' ";
               SqlCommand cmd = new SqlCommand(sqlquery1, con);
               SqlDataAdapter adp = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               adp.Fill(dt);
               if (dt.Rows.Count > 0)
               {
                   txtCBA_Message.Text = dt.Rows[0]["SMSMessage"].ToString();
               }


           }
           catch (Exception)
           {

               throw;
           }
           finally { con.Close(); }
       }

       public void FetchMsg_DB()
       {
           try
           {
               con.Open();
               string sqlquery1 = "Select ID,SMSMessage from tlb_DefaultCustomerBirthdaySMS where S_Status='Active' ";
               SqlCommand cmd = new SqlCommand(sqlquery1, con);
               SqlDataAdapter adp = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               adp.Fill(dt);
               if (dt.Rows.Count > 0)
               {
                   txtCBA_Message.Text = dt.Rows[0]["SMSMessage"].ToString();
               }


           }
           catch (Exception)
           {

               throw;
           }
           finally { con.Close(); }
       }

       public void FetchMsg_EB()
       {
           try
           {
               con.Open();
               string sqlquery1 = "Select ID,SMSMessage from tlb_DefaultCustomerBirthdaySMS where S_Status='Active' ";
               SqlCommand cmd = new SqlCommand(sqlquery1, con);
               SqlDataAdapter adp = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               adp.Fill(dt);
               if (dt.Rows.Count > 0)
               {
                   txtCBA_Message.Text = dt.Rows[0]["SMSMessage"].ToString();
               }


           }
           catch (Exception)
           {

               throw;
           }
           finally { con.Close(); }
       }
       public void FetchMsg_Install()
       {
           try
           {
               con.Open();
               string sqlquery1 = "Select ID,SMSMessage from tlb_CustomerBalance where S_Status='Active' ";
               SqlCommand cmd = new SqlCommand(sqlquery1, con);
               SqlDataAdapter adp = new SqlDataAdapter(cmd);
               DataTable dt = new DataTable();
               adp.Fill(dt);
               if (dt.Rows.Count > 0)
               {
                   txtCBA_Message.Text = dt.Rows[0]["SMSMessage"].ToString();
               }


           }
           catch (Exception)
           {

               throw;
           }
           finally { con.Close(); }
       }
       private void rdoSendAlertMessageCustom_Checked(object sender, RoutedEventArgs e)
       {
           txtCBA_Message.Text = "";
       }
    }
}