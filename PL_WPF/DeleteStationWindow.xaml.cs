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
    /// Interaction logic for DeleteStationWindow.xaml
    /// </summary>
    public partial class DeleteStationWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        public DeleteStationWindow()
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
            new StationViewWindow().Show();
            Close();
        }

        private void bDeleteNewStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lDeletingStationError.Content = "";
                lDoneDeletingStation.Content = "";
                lThankYou.Content = "";

                if (bl.DeleteStation(int.Parse(txStationNumber.Text))) //returns true if the station was deleted successfully
                {
                    lDoneDeletingStation.Content = "Your station was successfully deleted from the system";
                    lThankYou.Content = "Thank you!";
                }
            }

            catch (BO.BlBusException ex)
            {
                lDeletingStationError.Content = ex.Message;
            }
        }
    }
}
