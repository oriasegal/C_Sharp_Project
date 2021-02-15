using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for StationPanelsWindow.xaml
    /// </summary>
    public partial class StationPanelsWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        static BO.Station ChoosenStation = new BO.Station();
        public static List<BO.BusLine> Lines; //List of all the lines in the station
        List<int> LineNumbers = new List<int>(); //List of all the lines in the station - only numbers
        List<string> FinalStations = new List<string>(); //List of all the names of the last stations on all the lines in the station
        List<BO.LineTiming> LinesToShow = new List<BO.LineTiming>(); //all lines that will be showen on the dinamic panel
        ObservableCollection<BO.LineTiming> OCLinesToShow = new ObservableCollection<BO.LineTiming>(); //will be sent to window
        static string Area;
        bool color = true; //true = black,start , false = white,stop
        
        public StationPanelsWindow(object item1, string area)
        {
            ChoosenStation = (BO.Station)item1;
            Area = area;
            Lines = new List<BO.BusLine>(bl.GetStationPassingLines(ChoosenStation)); //List of all the lines in the station

            foreach (var line in Lines)
            {
                FinalStations.Add(bl.GetFinalStationName(line.BusLineIndex));
                LineNumbers.Add(line.LineNumber);
            }

            InitializeComponent();
            ShowLinesList(); //prints all the lines that pass this station (yellow panel)

            lStationName.Content = ChoosenStation.StationName;
            lStationID.Content = "ID: " + ChoosenStation.StationNumber;

            bStartStop.Content = "START";
            txHours.Text = DateTime.Now.Hour.ToString();
            txMinutes.Text = DateTime.Now.Minute.ToString();
            txSeconds.Text = DateTime.Now.Second.ToString(); 
        }

        public void ShowLinesList() //for yellow panel
        {
            lbLinesList.DataContext = LineNumbers;
            lbFinalStations.DataContext = FinalStations;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new StationsInAreaWindow(Area).Show();
            Close();
        }


        BackgroundWorker timer;
        BackgroundWorker lines;
        int hours, minutes, seconds, speed, Rate;
        TimeSpan CurrentTime, time;
        bool start; //true when START is clicked


        private void bStartStop_Click(object sender, RoutedEventArgs e)
        {
            timer = new BackgroundWorker(); //controls the clock
            lines = new BackgroundWorker(); //controls the dinamic panel

            if (color == true) //starting the simulation
            {
                bStartStop.Content = "STOP";
                bStartStop.Background = Brushes.White;
                bStartStop.Foreground = Brushes.Black;
                color = false;
                start = true;

                hours = int.Parse(txHours.Text);
                minutes = int.Parse(txMinutes.Text);
                seconds = int.Parse(txSeconds.Text);
                speed = int.Parse(txSpeed.Text);

                CurrentTime = new TimeSpan(hours, minutes, seconds);
                Rate = 1000 / speed;

                txHours.IsReadOnly = true;
                txMinutes.IsReadOnly = true;
                txSeconds.IsReadOnly = true;
                txSpeed.IsReadOnly = true;

                timer.DoWork += Timer_DoWork;
                timer.WorkerReportsProgress = true;
                timer.ProgressChanged += Timer_ProgressChanged;

                lines.DoWork += Lines_DoWork; ;
                lines.WorkerReportsProgress = true;
                lines.ProgressChanged += Lines_ProgressChanged; ;
            }

            else //if(color == false) - stoping the simulation
            {
                bStartStop.Content = "START";
                bStartStop.Background = Brushes.Black;
                bStartStop.Foreground = Brushes.White;
                color = true;
                start = false;

                txHours.IsReadOnly = false;
                txMinutes.IsReadOnly = false;
                txSeconds.IsReadOnly = false;
                txSpeed.IsReadOnly = false;

                timer.RunWorkerCompleted += Timer_RunWorkerCompleted;
                lines.RunWorkerCompleted += Lines_RunWorkerCompleted;
            }

            timer.RunWorkerAsync();
            lines.RunWorkerAsync();
        }


        private void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            while(start)
            {
                timer.ReportProgress(1);
                Thread.Sleep(Rate);
            }
        }

        private void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentTime += TimeSpan.FromSeconds(1); //adds one second to the current time
            txHours.Text = CurrentTime.Hours.ToString();
            txMinutes.Text = CurrentTime.Minutes.ToString();
            txSeconds.Text = CurrentTime.Seconds.ToString();
        }

        private void Timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //bl.StopSimulator();
        }



        private void Lines_DoWork(object sender, DoWorkEventArgs e)
        {
            while(start)
            {
                if(CurrentTime >= (time + TimeSpan.FromSeconds(60)))
                {
                    time = CurrentTime;
                    lines.ReportProgress(1);
                    Thread.Sleep(Rate);
                }
            }
        }
        
        private void Lines_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OCLinesToShow.Clear();
            LinesToShow = bl.GetAllLinesToShow(ChoosenStation, CurrentTime);
            
            foreach (var line in LinesToShow)
            {
                OCLinesToShow.Add(line);
            }

            lbInfo.DataContext = OCLinesToShow.OrderBy(o => o.ExpectedArrivel).ToList();
        }

        private void Lines_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
