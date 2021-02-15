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
    /// Interaction logic for UpdateStationWindow.xaml
    /// </summary>
    public partial class UpdateStationWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public UpdateStationWindow()
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

        private void bUpdateNewStation_Click(object sender, RoutedEventArgs e)
        {
            string NewStationNumber = "", StationName = "", Latitude = "", Longitude = "", Address = "";

            try
            {
                lUpdatingStationError.Content = "";
                lDoneUpdatingStation.Content = "";
                lThankYou.Content = "";

                if (txStationNumber.Text == "")
                    throw new BO.BlBusException("You didn't enter a station number.");

                if (txNewStationNumber.Text == "")
                    NewStationNumber = "-1";
                else NewStationNumber = txNewStationNumber.Text;

                if (txStationName.Text == "")
                    StationName = "0";
                else StationName = txStationName.Text;

                if (txLatitude.Text == "")
                    Latitude = "0";
                else Latitude = txLatitude.Text;

                if (txLongitude.Text == "")
                    Longitude = "0";
                else Longitude = txLongitude.Text;

                if (txAddress.Text == "")
                    Address = "0";
                else Address = txAddress.Text;

                if(bl.UpdateStation(int.Parse(txStationNumber.Text), int.Parse(NewStationNumber), StationName, double.Parse(Latitude), double.Parse(Longitude), Address))
                {
                    lDoneUpdatingStation.Content = "Your station was successfully updated";
                    lThankYou.Content = "Thank you!";
                }
            }

            catch (BO.BlBusException ex)
            {
                lUpdatingStationError.Content = ex.Message;
            }
        }
    }
}
