using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus
    {
        public string LicenseNumber { get; set; }

        public DateTime StartDate { get; set; } //The date of starting operation

        public double Mileage { get; set; } //for each bus, counting upto 20,000 km

        public double Fuel { get; set; }

        public BO.STATUS Status { get; set; }

        public DateTime Treatment { get; set; }
        public override string ToString()
        {
            string result = "\n";

            string one, two, three; //The parts of the license number

            if (StartDate.Year >= 2018)   //The pattern is XXX-XX-XXX
            {
                one = LicenseNumber.Substring(0, 3);
                two = LicenseNumber.Substring(3, 2);
                three = LicenseNumber.Substring(5, 3);

                result += String.Format("{0}-{1}-{2}", one, two, three);
            }

            else ////The pattern is XX-XXX-XX
            {
                one = LicenseNumber.Substring(0, 2);
                two = LicenseNumber.Substring(2, 3);
                three = LicenseNumber.Substring(5, 2);

                result += String.Format("{0}-{1}-{2}", one, two, three);
            }

            result += "\n";

            return result;
        }
    }

}

