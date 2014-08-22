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
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

       

        private void btnadmin_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            CRM_MainForm crm_frmm = new CRM_MainForm();
            crm_frmm.Show();
        }

      
        private void btna_login_Click(object sender, RoutedEventArgs e)
        {
           CRM_AdminDashbord crm_ad = new CRM_AdminDashbord();
            crm_ad.ShowDialog();
          
        }
    }
}
