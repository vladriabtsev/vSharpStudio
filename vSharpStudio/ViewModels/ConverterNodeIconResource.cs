using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace vSharpStudio.ViewModels
{
    public class ConverterNodeIconResource : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            var type = value.GetType().Name;
            var res = Application.Current.FindResource("icon" + type);
            return res;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
