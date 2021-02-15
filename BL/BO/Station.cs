using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        public static List<int> StationNumbers;  //saves the numbers of the stations in the system (only numbers).
        public int StationNumber { get; set; }

        public string StationName { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            return this.StationName + "\n" + "ID: " + this.StationNumber;
        }
    }
}
