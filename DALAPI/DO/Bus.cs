using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus
    {
        public string LicenseNumber { get; set; }

        public DateTime StartDate { get; set; } //The date of starting operation
        
        public double Mileage { get; set; } //for each bus, counting upto 20,000 km
        
        public double Fuel { get; set; }

        public DateTime Treatment { get; set; } //sets the last treatment time for each bus

        public STATUS Status { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
