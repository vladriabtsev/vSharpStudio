using System;
using System.Windows.Data;

namespace vSharpStudio.wpf.Converters
{
    public class ConverterCountToWidth : IValueConverter
    {
        private static readonly int _step_width = 2;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is int)
            {
                int p = (int)value;
                return _step_width * LogInt(p);
            }
            return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private int LogInt(int count)
        {
            int res = 0;
            int i = 1;
            while (i <= count)
            {
                res++;
                i <<= i;
            }
            return res;
        }
    }

}
