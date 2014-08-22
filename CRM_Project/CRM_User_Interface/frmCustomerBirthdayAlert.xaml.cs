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
        public string cid_CAB = "", cname_CAB = "", cphone_CAB = "", cdob_CAB = "", message_Boday;
        public string caption = "Green Future Glob";
        public frmCustomerBirthdayAlert()
        {
            InitializeComponent();
        }

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
                this.Close();
            }
            else if (result == "Cancel")
            {
                MessageBox.Show("Please Click on Exit Button For Further Process");
            }
        }
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
            if (cid_CAB != "" && cname_CAB != "" && cphone_CAB != "" && cdob_CAB != "")
            {
                lblCBA_CID.Content = cid_CAB;
                lblCBA_CName.Content = cname_CAB;
                lblCBA_CDOB.Content = cdob_CAB;
                txtCBA_To.Text = cphone_CAB;
            }
            else
            {
                MessageBox.Show("Data Not Select Properly");
            }
        }
        public void Preview()
        {
            message_Boday = " Dear Customer " + lblCBA_CName.Content + "," + "\n" +
         " Dated " + lblCBA_CDOB.Content + "\n" +
        "" + txtCBA_Message.Text + "\n" +
         "From :" + lblCBA_From.Content + "\n";

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
       
    }
}