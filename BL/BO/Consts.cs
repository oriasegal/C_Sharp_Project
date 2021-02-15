using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Consts
    {
        //Station 
        public const int MAXDIGITS = 1000000;  //deteminate the range of the station's location
        public const double MIN_LAT = 31; //double 31
        public const double MAX_LAT = 33.3; //double 33.3
        public const double MIN_LON = 34.3; //double 34.3
        public const double MAX_LON = 35.5; //double 35.5

        //Bus
        public double TotalMileage = 0; //total mileage of one bus, since the day it started running, starts form 0
        public const double tank = 1200;  //sets the full tank capacity

    }
}
