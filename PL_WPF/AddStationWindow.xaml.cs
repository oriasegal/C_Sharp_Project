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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        public AddStationWindow()
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

        private void bAddNewStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lAddingNewStationError.Content = "";
                lDoneAddingNewStation.Content = "";
                lThankYou.Content = "";

                if (bl.AddStation(int.Parse(txStationNumber.Text), txStationName.Text, double.Parse(txLatitude.Text), double.Parse(txLongitude.Text), txAddress.Text)) //returns true if the station was added successfully
                {
                    lDoneAddingNewStation.Content = "Your station was successfully added to the system";
                    lThankYou.Content = "Thank you!";
                }
            }

            catch (BO.BlBusException ex)
            {
                lAddingNewStationError.Content = ex.Message;
            }
        }
    }
}
