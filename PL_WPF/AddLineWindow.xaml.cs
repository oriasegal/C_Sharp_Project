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
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        static List<string> AreaList = new List<string>();
        
        public AddLineWindow()
        {
            InitializeComponent();
            CreateArea(AreaList);

            cbArea.ItemsSource = AreaList;
            cbArea.SelectedIndex = 0;
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

        public void CreateArea(List<string> AreaList)
        {
            if(AreaList.Count == 0)
            {
                AreaList.Add("Select area");
                AreaList.Add("GENERAL");
                AreaList.Add("NORTH");
                AreaList.Add("SOUTH");
                AreaList.Add("CENTER");
                AreaList.Add("JERUSALEM");
            }
        }

        static string Area;
        private void cbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Area = cbArea.SelectedValue.ToString();
        }

        private void bAddNewLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lAddingNewLineError.Content = "";
                lDoneAddingNewLine.Content = "";
                lThankYou.Content = "";

                if (bl.AddBusLine(int.Parse(txLineNumber.Text), Area, int.Parse(txFirstStation.Text), int.Parse(txLastStation.Text))) //returns true if the line was added successfully
                {
                    lDoneAddingNewLine.Content = "Your line was successfully added to the system";
                    lThankYou.Content = "Thank you!";
                }
            }

            catch (BO.BlBusException ex)
            {
                lAddingNewLineError.Content = ex.Message;
            }
        }
    }
}
