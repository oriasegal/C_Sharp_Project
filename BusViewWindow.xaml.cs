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
    /// Interaction logic for BusViewWindow.xaml
    /// </summary>
    public partial class BusViewWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public static List<BO.Bus> BusesList = new List<BO.Bus>();
        public static List<int> BusesLiNumbers = new List<int>(); //List of all the buses license numbers in the system
        public static ObservableCollection<BO.Bus> Buses; //List of all the buses in the system

        public BusViewWindow()
        {
            Buses = new ObservableCollection<BO.Bus>(bl.GetBusses());

            //if we'll need the license numbers numbers of the buses, that's how to get them into a list:
            //foreach (var bus in Buses)
            //    BusesLiNumbers.Add(bus.BusNumber);

            InitializeComponent();
            ShowBusesList(); //prints all the buses in the system (buses numbers)
        }

        #region ShowBusesList

        public void ShowBusesList()
        {
            lbBusesList.DataContext = Buses;
        }

        #endregion

        private void lbBusesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListBox)sender;
            object item = list.SelectedItem;

            BusInfoWindow busInfoWindow = new BusInfoWindow(item); //item == the picked bus
            busInfoWindow.Show(); //opens the BusInfoWindow when an item on the list gets doubleclicked
        }

        private void bAddBus_Click(object sender, RoutedEventArgs e)
        {
            new AddBusWindow().Show(); //opens AddBusesWindow when "add" is clicked
            Close();
        }

        private void bDeleteBus_Click(object sender, RoutedEventArgs e)
        {
            new DeleteBusWindow().Show(); //opens DeletelbBusesWindow when "delete" is clicked
            Close();
        }

        private void bUpdateBus_Click(object sender, RoutedEventArgs e)
        {
            new UpdateBusWindow().Show(); //opens UpdateBusesWindow when "update" is clicked
            lbBusesList.Items.Refresh();
            Close();
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
    }
}

/*
 *   

       
        
        public void bRefuel_Click(object sender, RoutedEventArgs e)
        {
            cmd = (Button)sender;
            b = (Bus)cmd.DataContext;
            if (b.Status != "READY")
            {
                new StatusMessageWindow().Show();
                return;
            }

            b.Status = "REFUELING";

            (sender as Button).IsEnabled = false; //makes button unavailable

            if (cmd.DataContext is Bus)
            {
                Bus.busRefuel(b);
                BackgroundWorker refuel = new BackgroundWorker();
                refuel.DoWork += Refuel_DoWork;
                refuel.RunWorkerCompleted += Refuel_RunWorkerCompleted;
                refuel.RunWorkerAsync();
            }
        }

        private void Refuel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            new RefuelWasDoneWindow().Show();
            cmd.IsEnabled = true;
            b.Status = "READY";
        }

        private void Refuel_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(12000); //waits two hours, 12 secends
        }

        private void lbBusesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListBox)sender;
            object item = list.SelectedItem;

            BusInfoWindow busInfoWindow = new BusInfoWindow(item); //item == the picked bus
            busInfoWindow.Show(); //opens the BusInfoWindow when an item on the list gets doubleclicked
        }

        private void lbBusesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    } 
*/