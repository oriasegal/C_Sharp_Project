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
    /// Interaction logic for UpdateStationOnLineWindow.xaml
    /// </summary>
    public partial class UpdateStationOnLineWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        
        static string LI, SN;
        public UpdateStationOnLineWindow(string LineIndex, string StationNumber)
        {
            InitializeComponent();
            LI = LineIndex;
            SN = StationNumber;

            lStationNumber.Content = SN;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new UpdateLineWindow().Show();
            Close();
        }

        private void bUpdateNewStation_Click(object sender, RoutedEventArgs e)
        {
            int Placement = -1, DistancePrevious = 0, DistanceNext = 0, TravelTimePrevious = 0, TravelTimeNext = 0;

            try
            {
                lUpdatingStationError.Content = "";
                lDoneUpdatingStation.Content = "";
                lThankYou.Content = "";

                if (!(txPlacement.Text == ""))
                    Placement = int.Parse(txPlacement.Text);

                if (!(txDistancePrevious.Text == ""))
                    DistancePrevious = int.Parse(txDistancePrevious.Text);

                if (!(txDistanceNext.Text == ""))
                    DistanceNext = int.Parse(txDistanceNext.Text);

                if (!(txTravelTimePrevious.Text == ""))
                    TravelTimePrevious = int.Parse(txTravelTimePrevious.Text);

                if (!(txTravelTimeNext.Text == ""))
                    TravelTimeNext = int.Parse(txTravelTimeNext.Text);

                if (bl.UpdateStationOnLine(int.Parse(LI), int.Parse(SN), Placement, DistancePrevious, DistanceNext, TravelTimePrevious, TravelTimeNext))
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
