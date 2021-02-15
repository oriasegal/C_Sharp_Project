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
using System.Threading;



using BLAPI;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        private Timer timer;
        public AddBusWindow()
        {
            InitializeComponent();
        }

        private void bAddNewBus_Click(object sender, RoutedEventArgs e)
        {
            lAddingNewBusError.Content = "";
            lDoneAddingNewBus.Content = "";
            lThankYou.Content = "";

            try
            {
                if (bl.AddBus(txBusNumber.Text, dpBusDate.Text, double.Parse(txMilage.Text), double.Parse(txFuel.Text)) ) //returns true if the bus was added successfully
                {
                    lDoneAddingNewBus.Content = "Your bus was successfully added to the system";
                    lThankYou.Content = "Thank you!";
                }
            }

            catch (BO.BlBusException ex)
            {
                lAddingNewBusError.Content = ex.Message;
            }
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new BusViewWindow().Show();
            Close();
        }
    }
}
