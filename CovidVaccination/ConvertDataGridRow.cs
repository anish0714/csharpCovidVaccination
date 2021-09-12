using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CovidVaccination
{
    class ConvertDataGridRow : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Customer)
            {
                if (((Customer)value).CustVaccine == "COVIDSHIELD")
                {
                    return Brushes.LightPink;
                }
                else if (((Customer)value).CustVaccine == "MODERNA")
                {
                    return Brushes.LightYellow;
                }
                else
                {
                    return Brushes.LightCyan;
                }
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
