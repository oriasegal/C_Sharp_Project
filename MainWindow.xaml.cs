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
using System.Windows.Navigation;
using System.Windows.Shapes;

using BLAPI;

namespace PL_WPF
{
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void bCreateAccountUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lCreatingUserError.Content = "";

                if (bl.AddUser(txCreateAccountUN.Text, pbCreateAccountP.Password, false)) //returns true if the user was added successfully
                {
                    new UserViewWindow().Show(); //opens UserViewWindow when all fields are correct
                    Close();
                }
            }

            catch(BO.BlBusException ex)
            {
                lCreatingUserError.Content = ex.Message;
            }
        }

        private void bCreateAccountManagement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lCreatingUserError.Content = "";

                //To add a manager you first need to enter the code. To enter the code you first need to make sure your input is correct. Thats why we added anuther if loop here.
                if ((txCreateAccountUN.Text.Any(char.IsUpper) && txCreateAccountUN.Text.Any(char.IsLower) && txCreateAccountUN.Text.Length >= 5) && (pbCreateAccountP.Password.Any(char.IsUpper) && pbCreateAccountP.Password.Any(char.IsLower) && pbCreateAccountP.Password.Any(char.IsDigit) && pbCreateAccountP.Password.Length >= 5)) //returns true if the user was added successfully
                {
                    new ManagerSentCodeWindow(txCreateAccountUN.Text, pbCreateAccountP.Password).Show(); //opens ManagerSentCodeWindow when all fields are correct
                    Close();
                }
                
                else throw new BO.BlBusException("You enter wrong values for your new user account. Please try again");
            }

            catch (BO.BlBusException ex)
            {
                lCreatingUserError.Content = ex.Message;
            }
        }

        private void bLogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lEnteringUserError.Content = "";

                BO.User LogIn = bl.ReadUser(txLogInUN.Text, pbLogInP.Password); //gets the matching user - with the same username
                
                if (LogIn.Permission == true) //if its a manager
                {
                    new ManagerViewWindow().Show(); //opens ManagerViewWindow when all fields are correct
                    Close();
                }

                else if (LogIn.Permission == false) //if its a user
                {
                    new UserViewWindow().Show(); //opens UserViewWindow when all fields are correct
                    Close();
                }
            }

            catch (BO.BlBusException ex)
            {
                lEnteringUserError.Content = ex.Message;
            }
        }
    }
}
