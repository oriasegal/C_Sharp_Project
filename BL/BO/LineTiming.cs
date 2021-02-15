using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTiming
    {
        public int LineTimingIndex { get; set; } 
        public TimeSpan StatringTime { get; set; }
        public int BusLineIndex { get; set; }
        public int LineNumber { get; set; }
        public string FinalStationName { get; set; }
        public TimeSpan ExpectedArrivel { get; set; }

        public override string ToString()
        {
            return "    " + ExpectedArrivel + "          " + FinalStationName + "             " + LineNumber;
        }
    }
}
