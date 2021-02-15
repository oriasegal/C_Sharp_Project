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
    /// Interaction logic for UpdateLineWindow.xaml
    /// </summary>
    public partial class UpdateLineWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        static List<string> AreaList = new List<string>();
        static List<string> ActionList = new List<string>();
        public UpdateLineWindow()
        {
            InitializeComponent();

            CreateArea(AreaList);
            cbArea.ItemsSource = AreaList;
            cbArea.SelectedIndex = 0;

            CreateAction(ActionList);
            cbStation.ItemsSource = ActionList;
            cbStation.SelectedIndex = 0;
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

        public void CreateAction(List<string> ActionList)
        {
            if(ActionList.Count == 0)
            {
                ActionList.Add("Select action");
                ActionList.Add("Add to line");
                ActionList.Add("Delete from line");
                ActionList.Add("Update details");
            }
        }

        static string Area;
        private void cbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Area = cbArea.SelectedValue.ToString();
        }

        static string Action;
        private void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Action = cbStation.SelectedValue.ToString();
        }

        private void bUpdateLine_Click(object sender, RoutedEventArgs e)
        {
            lUpdatingLineError.Content = "";
            lDoneUpdatingLine.Content = "";
            lThankYou.Content = "";

            string LineNumberToChange = "", StationNumber = "";

            try
            {
                if (txLineIndex.Text == "" || int.Parse(txLineIndex.Text) < 0)
                    throw new BO.BlBusException("The line index you entered is invalid. Try again.");

                if (txLineNumberToChange.Text == "")
                    LineNumberToChange = "-1";
                else LineNumberToChange = txLineNumberToChange.Text;

                if (txStationNumber.Text == "")
                    StationNumber = "-1";
                else StationNumber = txStationNumber.Text;
                
                if (bl.UpdateBusLine(int.Parse(txLineIndex.Text), int.Parse(LineNumberToChange), Area, int.Parse(StationNumber), Action))
                {
                    if (Action == "Update details")
                    {
                        if (bl.OnLine(int.Parse(txLineIndex.Text), int.Parse(StationNumber)))
                        {
                            new UpdateStationOnLineWindow(txLineIndex.Text, StationNumber).Show();
                            Close();
                        }
                    }
                    
                    lDoneUpdatingLine.Content = "The line was updated successfully";
                    lThankYou.Content = "Thank you!";
                }

            }

            catch (BO.BlBusException ex)
            {
                lUpdatingLineError.Content = ex.Message;
            }
        }
    }
}
