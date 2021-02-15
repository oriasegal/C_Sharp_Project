using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;

namespace PL_WPF
{
    public class StatusToBGColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Status = (string)value;

            if (Status == "READY")
                return Brushes.Gainsboro; //gray

            if (Status == "DRIVING")
                return Brushes.MediumAquamarine; //blue

            if (Status == "REFUELING")
                return Brushes.Tomato; //red

            if (Status == "TREATMENT")
                return Brushes.SandyBrown; //orange

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
