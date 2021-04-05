using System;
using System.Globalization;
using System.Windows.Data;

namespace COMPort.App.Converters
{
    internal class OpenCloseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool b)
            {
                return b ? "Close" : "Open";
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
