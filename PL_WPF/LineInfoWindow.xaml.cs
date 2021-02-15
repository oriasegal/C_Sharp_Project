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
    /// Interaction logic for LineInfoWindow.xaml
    /// </summary>
    public partial class LineInfoWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        static BO.BusLine b = new BO.BusLine();
        public LineInfoWindow(object item)
        {
            InitializeComponent();

            b = (BO.BusLine)item;

            lLineIndex.Content = b.BusLineIndex;
            lLineNumber.Content = b.LineNumber;
            lArea.Content = bl.GetArea(b);
            lFirstStation.Content = b.FirstStation;
            lLastStation.Content = b.LastStation;

            List<int> stationNumbers = bl.GetStationsOnLineByNumber(b.BusLineIndex);
            lStationNumbers.DataContext = stationNumbers; //prints out all station numbers for this line
        }

        private void lStationNumbers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListBox)sender;
            object item = list.SelectedItem;

            StationOnLineInfoWindow stationOnLineInfoWindow = new StationOnLineInfoWindow(b.BusLineIndex, item); //item == the picked station
            stationOnLineInfoWindow.Show(); //opens the StationOnLineInfoWindow when an item on the list gets doubleclicked
        }
    }
}
