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
using GCO.Service;

namespace GCOTestUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.UserAccess.AccessToken.Length == 0)
                MessageBox.Show("No AccessToken set in App.xaml.cs");
        }

        private void btnUserInfos_Click(object sender, RoutedEventArgs e)
        {
            RetrieveUserInfos();
        }

        private async void RetrieveUserInfos()
        {
            App.User = await App.GeocachingService.GetOwnUserDetails();
            
            App.UserAccess.UserName = App.User.UserName;
            
            txtUser.DataContext = App.User;
            txtUserProperties.DataContext = App.User;
        }
    }
}
