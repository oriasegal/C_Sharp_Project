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
    public partial class UpdateBusWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public UpdateBusWindow()
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
            new BusViewWindow().Show();
            Close();
        }

        private void bUpdateNewBus_Click(object sender, RoutedEventArgs e)
        {
            string NewBusNumber = "", Milage = "", Fuel = "";

            try
            {
                lUpdatingBusError.Content = "";
                lDoneUpdatingBus.Content = "";
                lThankYou.Content = "";

                if (txBusNumber.Text == "")
                    throw new BO.BlBusException("You didn't enter a bus number.");

                if (txNewBusNumber.Text == "")
                    NewBusNumber = "-1";
                else NewBusNumber = txNewBusNumber.Text;

                if (txMilage.Text == "")
                    Milage = "-1";
                else Milage = txMilage.Text;

                if (txFuel.Text == "")
                    Fuel = "-1";
                else Fuel = txFuel.Text;

                if (bl.UpdateBus(txBusNumber.Text, NewBusNumber, double.Parse(Milage), double.Parse(Fuel)))
                {
                    lDoneUpdatingBus.Content = "Your Bus was successfully updated";
                    lThankYou.Content = "Thank you!";
                }
            }

            catch (BO.BlBusException ex)
            {
                lUpdatingBusError.Content = ex.Message;
            }
        }
    }
}
