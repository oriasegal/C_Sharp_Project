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
    /// Interaction logic for LineViewWindow.xaml
    /// </summary>
    public partial class LineViewWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public static List<int> LinesNumbers = new List<int>(); //List of all the lines numbers in the system
        public static ObservableCollection<BO.BusLine> Lines; //List of all the lines in the system

        public LineViewWindow()
        {
            InitializeComponent();

            Lines = new ObservableCollection<BO.BusLine>(bl.GetBusLines());

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

        #region ShowStationsList

        public void ShowStationsList()
        {
            lbLinesList.DataContext = Lines;
        }

        #endregion

        private void lbLinesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListBox)sender;
            object item = list.SelectedItem;

            LineInfoWindow lineInfoWindow = new LineInfoWindow(item); //item == the picked line
            lineInfoWindow.Show(); //opens the LineInfoWindow when an item on the list gets doubleclicked
        }

        private void bAddLine_Click(object sender, RoutedEventArgs e)
        {
            new AddLineWindow().Show(); //opens AddLineWindow when "add" is clicked
            Close();
        }

        private void bDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            new DeleteLineWindow().Show(); //opens DeleteLineWindow when "delete" is clicked
            Close();
        }

        private void bUpdateLine_Click(object sender, RoutedEventArgs e)
        {
            new UpdateLineWindow().Show(); //opens UpdateLineWindow when "update" is clicked
            Close();
        }
    }
}
