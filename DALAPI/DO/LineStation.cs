using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation
    {
        public int BusLineIndex { get; set; } //runing value

        public int StationNumber { get; set; }

        public int NumberOnLine { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
