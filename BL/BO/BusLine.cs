using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class BusLine
    {
        //public List<BO.Station> LineStations; //List of all the line numbers in bLine
        //instead you can use GetStationNumbers to get all the numbers of the stations on this busline.

        public int BusLineIndex { get; set; } 

        public int LineNumber { get; set; }

        public DO.AREA Area { get; set; }

        public int FirstStation { get; set; }

        public int LastStation { get; set; }

        public override string ToString()
        {
            return "Line Number: " + this.LineNumber;
        }
    }
}
