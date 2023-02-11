using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AccountApp2023.Converter
{
    class DayNetConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            decimal Debit;
            decimal Credit;
            decimal net = 0;
            if (decimal.TryParse(values[0].ToString(), out Debit) && decimal.TryParse(values[1].ToString(), out Credit))
            {
                net = Debit - Credit;
                
                    return net.ToString();
            }
            else
                return (net = 0).ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
