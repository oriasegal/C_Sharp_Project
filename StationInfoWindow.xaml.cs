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
    /// Interaction logic for StationInfoWindow.xaml
    /// </summary>
    public partial class StationInfoWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        static BO.Station s = new BO.Station();
        public StationInfoWindow(object item)
        {
            InitializeComponent();

            s = (BO.Station)item;

            lStationNumber.Content = s.StationNumber;
            lStationName.Content = s.StationName;
            lLatitude.Content = s.Latitude;
            lLongitude.Content = s.Longitude;
            lAddress.Content = s.Address;

            List<int> lineNumbers = new List<int>();

            foreach (var busline in bl.GetBusLines()) //gets all the lines that pass this station by number
            {
                if (bl.GetStationNumbers(busline).Contains(s.StationNumber))
                {
                    lineNumbers.Add(busline.LineNumber);
                }
            }

            lLines.DataContext = lineNumbers; //prints out all line numbers for this station
        }
    }
}
