using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Consts
    {
        //Station 
        private const int MAXDIGITS = 1000000;  //deteminate the range of the station's location
        private const int MIN_LAT = -90; //double 31
        private const int MAX_LAT = 90; //double 33.3
        private const int MIN_LON = -180; //double 34.3
        private const int MAX_LON = 180; //double 35.5

        //Bus
        public double TotalMileage = 0; //total mileage of one bus, since the day it started running, starts form 0
        private const double tank = 1200;  //sets the full tank capacity

    }
}
