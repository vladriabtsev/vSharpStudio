using System;
using System.Windows;
using System.Windows.Data;
using CommunityToolkit.Diagnostics;

namespace vSharpStudio.wpf.Converters
{
    public class ConverterToStaticResource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Guard.IsAssignableToType<string>(value);
            if (value is string s)
            {
                return Application.Current.FindResource(s);
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
