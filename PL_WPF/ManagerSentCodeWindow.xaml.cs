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
    /// Interaction logic for ManagerSentCodeWindow.xaml
    /// </summary>
    public partial class ManagerSentCodeWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        static string UserName, Password;
        public ManagerSentCodeWindow(string userName, string password)
        {
            InitializeComponent();

            UserName = userName;
            Password = password;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void bEnterCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lWrongCodeMessage.Content = "";

                if (txCode.Text == "12345") //if entered the correct code for manager
                {
                    if (bl.AddUser(UserName, Password, true)) //returns true if the user was added successfully
                    {
                        new ManagerViewWindow().Show();
                        Close();
                    }
                }

                else
                {
                    lWrongCodeMessage.Content = "Entered the wrong code. Please try again.";
                }
            }

            catch (BO.BlBusException ex)
            {
                lWrongCodeMessage.Content = ex.Message;
            }
        }

        //public void OnKeyDownEnter(object sender, KeyEventArgs e) //gets input by clicking the ENTER key
        //{
        //    try
        //    {
        //        lWrongCodeMessage.Content = "";

        //        if (txCode.Text == "12345") //if entered the correct code for manager
        //        {
        //            if (bl.AddUser(UserName, Password, true)) //returns true if the user was added successfully
        //            {
        //                new ManagerViewWindow().Show();
        //                Close();
        //            }
        //        }

        //        else
        //        {
        //            lWrongCodeMessage.Content = "Entered the wrong code. Please try again.";
        //        }
        //    }

        //    catch (BO.BlBusException ex)
        //    {
        //        lWrongCodeMessage.Content = ex.Message;
        //    }
        //}
    }
}
