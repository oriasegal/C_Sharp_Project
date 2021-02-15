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
    /// Interaction logic for DeleteBusWindow.xaml
    /// </summary>
    public partial class DeleteBusWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public DeleteBusWindow()
        {
            InitializeComponent();
        }

        private void bDeleteBus_Click(object sender, RoutedEventArgs e)
        {
            lDeletingBusError.Content = "";
            lDoneDeletingBus.Content = "";
            lThankYou.Content = "";

            try
            {
                if (bl.DeleteBus((txBusNumber.Text))) //returns true if the bus was deleted successfully
                {
                    lDoneDeletingBus.Content = "Your bus was successfully deleted from the system";
                    lThankYou.Content = "Thank you!";
                }
            }

            catch (BO.BlBusException ex)
            {
                lDeletingBusError.Content = ex.Message;
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
