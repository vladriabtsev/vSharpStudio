using System;
using System.Windows.Data;

namespace vSharpStudio.ViewModels
{
    public class ConverterObjectToMinWidth : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            return 15;
        }
        public object? ConvertBack(object? value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
