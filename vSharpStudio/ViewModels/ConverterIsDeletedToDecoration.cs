using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using vSharpStudio.common;

namespace vSharpStudio.ViewModels
{
    public class ConverterIsDeletedToDecoration : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is bool))
                return null;
            var node = (bool)value;
            if (node)
                return TextDecorations.Strikethrough;
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
