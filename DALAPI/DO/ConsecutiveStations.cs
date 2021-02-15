using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class ConsecutiveStations
    {
        public int BusLineIndex { get; set; } //runing value
        
        public int ConsecutiveStationsIndex { get; set; } //runing value

        public int FirstStation { get; set; }

        public int SecondStation { get; set; }

        public int Distance { get; set; }

        public TimeSpan AvgTravelTime { get; set; }
    }
}
