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

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for ManagerViewWindow.xaml
    /// </summary>
    public partial class ManagerViewWindow : Window
    {
        public ManagerViewWindow()
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

        private void bBusViewWindow_Click(object sender, RoutedEventArgs e)
        {
            new BusViewWindow().Show();
            Close();
        }

        private void bStationViewWindow_Click(object sender, RoutedEventArgs e)
        {
            new StationViewWindow().Show();
            Close();
        }

        private void bLineViewWindow_Click(object sender, RoutedEventArgs e)
        {
            new LineViewWindow().Show();
            Close();
        }
    }
}
