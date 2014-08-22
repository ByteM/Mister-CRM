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

namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for CRM_Admin_Dashbord.xaml
    /// </summary>
    public partial class CRM_MainDashbord: Window
    {
        public CRM_MainDashbord()
        {
            //InitializeComponent();
        }

        private void btnadminexit_Click(object sender, RoutedEventArgs e)
        {
          //  this.Close();
        }

        private void btnsales_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void mnulast_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnsales_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnadminlogin_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin al = new AdminLogin();
            al.Show();
            this.Hide();
        }

        private void btnmainexit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
