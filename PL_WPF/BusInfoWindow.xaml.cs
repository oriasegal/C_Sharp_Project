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
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Threading;
using System.Globalization;

using BLAPI;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for BusInfoWindow.xaml
    /// </summary>
    public partial class BusInfoWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        static BO.Bus b = new BO.Bus();
        public static Button cmd;
        const double tank = 1200;  //sets the full tank capacity


        public BusInfoWindow(object item)
        {
            InitializeComponent();

            b = (BO.Bus)item;
            lBusNumber.Content = b.ToString();
            lDate.Content = b.StartDate;
            lMilage.Content = b.Mileage;
            lFuel.Content = b.Fuel;
            lNextTreatment.Content = b.Treatment;

        }

        private void bSendBusToTreatment_Click(object sender, RoutedEventArgs e)
        {
            lErrorTreatment.Content = "";

            if (b.Status.ToString() == "READY")
            {
                (sender as Button).IsEnabled = false; //treatment button
                b.Status = BO.STATUS.TREATMENT;

                BackgroundWorker treatment = new BackgroundWorker();
                treatment.DoWork += Treatment_DoWork;
                treatment.RunWorkerCompleted += Treatment_RunWorkerCompleted;
                treatment.RunWorkerAsync();
                bl.BusTreatment(b);
                lBusNumber.Content = b.ToString();
                lDate.Content = b.StartDate;
                lMilage.Content = b.Mileage + " km";
                lFuel.Content = b.Fuel + " liters";
                lNextTreatment.Content = b.Treatment;
            }

            else
                lErrorTreatment.Content = "The bus is not ready to treatment.";
        }

        private void bSendBusToRefuel_Click(object sender, RoutedEventArgs e)
        {
            lErrorRefuel.Content = "";

            if (b.Status.ToString() == "READY")
            {
                (sender as Button).IsEnabled = false; //makes button unavailable
                b.Status = BO.STATUS.REFUELING;

                BackgroundWorker refuel = new BackgroundWorker();
                refuel.DoWork += Refuel_DoWork;
                refuel.RunWorkerCompleted += Refuel_RunWorkerCompleted;
                refuel.RunWorkerAsync();
                bl.BusRefuel(b);
                lBusNumber.Content = b.ToString();
                lDate.Content = b.StartDate;
                lMilage.Content = b.Mileage + " km";
                lFuel.Content = b.Fuel + " liters";
                lNextTreatment.Content = b.Treatment;
            }

            else
                lErrorRefuel.Content = "The bus is not ready to refuel.";
        }

        private void Treatment_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lDoneTreatment.Content = "Treatment has be done successfully. ";
            b.Status = BO.STATUS.READY;
        }

        private void Treatment_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(144); //waits one day, 144 secends
        }

        private void Refuel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lDoneRefuel.Content = "Refuel has be done successfuly. ";
            b.Status = BO.STATUS.READY;
        }

        private void Refuel_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(120); //waits two hours, 12 secends
        }
    }
}


