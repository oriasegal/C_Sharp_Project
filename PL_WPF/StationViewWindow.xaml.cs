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
    /// Interaction logic for StationViewWindow.xaml
    /// </summary>
    public partial class StationViewWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public static ObservableCollection<BO.Station> Stations; //List of all the stations in the system

        public StationViewWindow()
        {
            Stations = new ObservableCollection<BO.Station>(bl.GetStations());

            InitializeComponent();
            ShowStationsList(); //prints all the stations in the system (station numbers)
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new ManagerViewWindow().Show();
            Close();
        }

        public void ShowStationsList()
        {
            lbStationsList.DataContext = Stations;
        }

        private void lbStationsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListBox)sender;
            object item = list.SelectedItem;

            StationInfoWindow stationInfoWindow = new StationInfoWindow(item); //item == the picked station
            stationInfoWindow.Show(); //opens the StationInfoWindow when an item on the list gets doubleclicked
        }

        private void bAddStation_Click(object sender, RoutedEventArgs e)
        {
            new AddStationWindow().Show(); //opens AddStationWindow when "add" is clicked
            Close();
        }

        private void bDeleteStation_Click(object sender, RoutedEventArgs e)
        {
            new DeleteStationWindow().Show(); //opens DeleteStationWindow when "delete" is clicked
            Close();
        }

        private void bUpdateStation_Click(object sender, RoutedEventArgs e)
        {
            new UpdateStationWindow().Show(); //opens UpdateStationWindow when "update" is clicked
            Close();
        }
    }
}
