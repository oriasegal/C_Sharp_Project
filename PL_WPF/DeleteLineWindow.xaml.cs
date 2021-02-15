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
    /// Interaction logic for DeleteLineWindow.xaml
    /// </summary>
    public partial class DeleteLineWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public DeleteLineWindow()
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
            new LineViewWindow().Show();
            Close();
        }

        private void bDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            lDeletingLineError.Content = "";
            lDoneDeletingLine.Content = "";
            lThankYou.Content = "";

            try
            {
                if (txLineIndex.Text == "" || int.Parse(txLineIndex.Text) < 0)
                    throw new BO.BlBusException("The line index you entered is invalid. Try again.");

                if (bl.DeleteBusLine(int.Parse(txLineIndex.Text))) //returns true if the line was deleted successfully
                {
                    lDoneDeletingLine.Content = "Your line was successfully deleted from the system";
                    lThankYou.Content = "Thank you!";
                }
            }

            catch (BO.BlBusException ex)
            {
                lDeletingLineError.Content = ex.Message;
            }
        }
    }
}
