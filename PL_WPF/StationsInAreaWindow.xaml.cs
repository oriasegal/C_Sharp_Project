using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for StationsInAreaWindow.xaml
    /// </summary>
    public partial class StationsInAreaWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public static ObservableCollection<BO.Station> Stations; //List of all the stations in the system
        string area;

        public StationsInAreaWindow(string Area)
        {
            Stations = new ObservableCollection<BO.Station>(bl.GetStationsInArea(Area));
            area = Area;

            InitializeComponent();
            ShowStationsList(); //prints all the stations in the system (station numbers)
        }

        public void ShowStationsList()
        {
            if (Stations.Count == 0)
                lAllStations.Content = "         No available stations to show.";
            else 
                lbStationsList.DataContext = Stations;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new UserViewWindow().Show();
            Close();
        }

        private void lbStationsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListBox)sender;
            object item = list.SelectedItem;

            new StationPanelsWindow(item, area).Show(); //opens the StationPanelsWindow when an item on the list gets doubleclicked
            Close();
        }
    }
}
