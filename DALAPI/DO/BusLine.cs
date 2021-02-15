using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLine
    {
        public int BusLineIndex { get; set; } //runing value

        public int LineNumber { get; set; }

        public AREA Area { get; set; }

        public int FirstStation { get; set; }

        public int LastStation { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
