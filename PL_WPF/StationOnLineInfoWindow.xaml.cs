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
    /// Interaction logic for StationOnLineInfoWindow.xaml
    /// </summary>
    public partial class StationOnLineInfoWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        public StationOnLineInfoWindow(object item1, object item2)
        {
            InitializeComponent();

            int LineIndex = (int)item1;
            int StationNumber = (int)item2;

            lPlacement.Content = bl.GetStationInfo(LineIndex, StationNumber, "Placement");
            lPreviousStation.Content = bl.GetStationInfo(LineIndex, StationNumber, "PreviousStation");
            lNextStation.Content = bl.GetStationInfo(LineIndex, StationNumber, "NextStation");
            lDistPreviousStation.Content = bl.GetStationInfo(LineIndex, StationNumber, "DistPreviousStation");
            lDistNextStation.Content = bl.GetStationInfo(LineIndex, StationNumber, "DistNextStation");
            lTravelPreviousStation.Content = bl.GetStationInfo(LineIndex, StationNumber, "TravelPreviousStation");
            lTravelNextStation.Content = bl.GetStationInfo(LineIndex, StationNumber, "TravelNextStation");
        }
    }
}
