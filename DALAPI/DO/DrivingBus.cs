using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DrivingBus
    {
        public int DrivingBusIndex { get; set; } //runing value

        public string LicenseNumber { get; set; }

        public int LineInUse { get; set; } //runing value

        public DateTime LeavingTimeFormal { get; set; }

        public DateTime LeavingTimeActual { get; set; }

        public int LastStationPassed { get; set; }

        public DateTime LastStationTime { get; set; }

        public DateTime NextStationArrivalTime { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
