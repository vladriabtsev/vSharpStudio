using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace vSharpStudio.wpf.Converters
{
    public class ConverterNotNullToGridLength : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return new GridLength(0, GridUnitType.Pixel);
            if (parameter != null && parameter is string)
            {
                var s = (string)parameter;
                double d;
                if (s.Last() == '*')
                {
                    var ss = s.Substring(0, s.Length - 1);
                    if (ss == string.Empty)
                        return new GridLength(1, GridUnitType.Star);
                    if (double.TryParse(ss, out d))
                    {
                        if (d == 0)
                            return new GridLength(1, GridUnitType.Star);
                        return new GridLength(d, GridUnitType.Star);
                    }
                    else
                        Debug.Assert(false);
                }
                else
                {
                    if (double.TryParse(s, out d))
                    {
                        return new GridLength(d, GridUnitType.Pixel);
                    }
                    else
                        Debug.Assert(false);
                }
            }
            return new GridLength(0, GridUnitType.Pixel);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
