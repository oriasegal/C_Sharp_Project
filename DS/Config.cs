using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DS
{
    public static class Config
    {
        static int busLineIndex = 0;
        public static int BusLineIndex => ++busLineIndex;

        static int consecutiveStationsIndex = 0;
        public static int ConsecutiveStationsIndex => ++consecutiveStationsIndex;

        static int drivingBusIndex = 0;
        public static int DrivingBusIndex => ++drivingBusIndex;

        static int leavingLineIndex = 0;
        public static int LeavingLineIndex => ++leavingLineIndex;

        static int lineTimingIndex = 0;
        public static int LineTimingIndex => ++lineTimingIndex;





        static int driveIndex = 0; //UserDrive
        public static int DriveIndex => ++driveIndex;

        static int lineInUse = 0; //DrivingBus
        public static int LineInUse => ++lineInUse;
    }
}
