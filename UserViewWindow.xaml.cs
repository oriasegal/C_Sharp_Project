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

using BLAPI;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for UserViewWindow.xaml
    /// </summary>
    public partial class UserViewWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        public UserViewWindow()
        {
            InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        static string Area;

        private void bNorth_Click(object sender, RoutedEventArgs e)
        {
            Area = "North";
            new StationsInAreaWindow(Area).Show();
            Close();
        }

        private void bCenter_Click(object sender, RoutedEventArgs e)
        {
            Area = "Center";
            new StationsInAreaWindow(Area).Show();
            Close();
        }

        private void bGeneral_Click(object sender, RoutedEventArgs e)
        {
            Area = "General";
            new StationsInAreaWindow(Area).Show();
            Close();
        }

        private void bJerusalem_Click(object sender, RoutedEventArgs e)
        {
            Area = "Jerusalem"; 
            new StationsInAreaWindow(Area).Show();
            Close();
        }

        private void bSouth_Click(object sender, RoutedEventArgs e)
        {
            Area = "South";
            new StationsInAreaWindow(Area).Show();
            Close();
        }
    }
}
