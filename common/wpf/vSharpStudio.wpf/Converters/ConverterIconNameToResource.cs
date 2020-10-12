using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ViewModelBase;

namespace vSharpStudio.wpf.Converters
{
    public class ConverterIconNameToResource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            if (!(value is string))
                Trace.WriteLine("##### ERROR ##### Converter '" + typeof(ConverterIconNameToResource).Name + "' is expecting to be used with object of type 'string'");
            string iconName = value as string;
            //if (Application.Current.Resources.MergedDictionaries[0].Contains(iconName))
            //    return Application.Current.Resources.MergedDictionaries[0][iconName];
            var res = Application.Current.FindResource(iconName);
            return res;
            Trace.WriteLine("##### ERROR ##### Converter '" + typeof(ConverterIconNameToResource).Name + "'. Application resorces doesn't contain icon resource with name: " + iconName);
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
