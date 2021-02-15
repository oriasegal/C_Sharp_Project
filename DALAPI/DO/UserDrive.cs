using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class UserDrive
    {
        public int DriveIndex { get; set; } //runing value

        public string UserName { get; set; }

        public int BusLineIndex { get; set; } //runing value

        public int BoardingStation { get; set; }

        public DateTime BoardingTime { get; set; }

        public int GettingOffStation { get; set; }

        public DateTime GettingOffTime { get; set; }

    }
}
