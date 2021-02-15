using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LeavingLine
    {
        public int LeavingLineIndex { get; set; } //runing value
        public int BusLineIndex { get; set; } //runing value

        public TimeSpan Start { get; set; }

        //public int Frequency { get; set; }

        //public TimeSpan End { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
