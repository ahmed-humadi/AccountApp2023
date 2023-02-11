using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace AccountApp2023.Converter
{
    [ValueConversion(/*sourceType*/ typeof(Boolean), /*targetType*/ typeof(Visibility))]
    public class BooleanToVisibilityConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Only convert to Visibility...
            if (targetType != typeof(Visibility)) { return null; }
            // DANGER! After 25, it's all downhill...
            if ((bool)value == true)
                return Visibility.Hidden;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
