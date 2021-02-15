using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station
    {
        //private static List<int> StationNumbers;  //saves the numbers choosen for the stations.
        public int StationNumber { get; set; }

        public string StationName { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
      
        public string Address { get; set; }

        public bool Active { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}


