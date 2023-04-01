using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.ViewModels
{
    public class ConverterForPropertyGridWidthGenSettings : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return new GridLength(0, GridUnitType.Pixel);
            return new GridLength(GenSettingsVm.PropertyGridWidthStatic, GridUnitType.Pixel);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
