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
using CRM_User_Interface;

namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for Main_Login.xaml
    /// </summary>
    public partial class Main_Login : Window
    {
        public Main_Login()
        {
            InitializeComponent();
        }

        private void btnexit_Click(object sender, RoutedEventArgs e)
        {
       
            this.Close();
        }

        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            CRM_MainForm crm_frm = new CRM_MainForm();
            crm_frm.Show();
        }

        private void btnexit_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown ();
          // this.Close();
            
            
        }
    }
}
