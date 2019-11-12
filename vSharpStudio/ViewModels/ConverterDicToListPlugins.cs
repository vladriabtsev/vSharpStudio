using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.ViewModels
{
    public class ConverterDicToListPlugins : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null || !(parameter is vPluginLayerTypeEnum))
            {
                Trace.WriteLine("##### ERROR ##### Converter '" + typeof(ConverterDicToListPlugins).Name + "' is expecting to be used with parameter of type '" + typeof(vPluginLayerTypeEnum).Name + "'");
                return null;
            }
            if (value == null || !(value is Dictionary<vPluginLayerTypeEnum, List<PluginRow>>))
            {
                Trace.WriteLine("##### ERROR ##### Converter '" + typeof(ConverterDicToListPlugins).Name + "' is expecting to be used with object of type '" + typeof(Dictionary<vPluginLayerTypeEnum, List<PluginRow>>).Name + "'");
                return null;
            }
            Dictionary<vPluginLayerTypeEnum, List<PluginRow>> dic = value as Dictionary<vPluginLayerTypeEnum, List<PluginRow>>;
            if (dic.ContainsKey((vPluginLayerTypeEnum)parameter))
            {
                var lst = dic[(vPluginLayerTypeEnum)parameter];
                lst = lst.Distinct(new PluginComparer()).ToList();
                return lst;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
