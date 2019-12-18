using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorPluginGeneratorSelection : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {

            // this.Config.GroupPlugins.ListPlugins
            ITreeConfigNode instance = (ITreeConfigNode)propertyItem.Instance;

            //var type = propertyItem.Instance.GetType();
            //var descr = type.GetProperty("PluginGuid", BindingFlags.Public | BindingFlags.Static);
            //string guid = (string)descr.GetValue(propertyItem.Instance, null);

            //IConfig cnfg = instance.GetConfig();
            //var en = cnfg.IGroupPlugins.IListPlugins.GetEnumerator();
            //IPlugin plg = null;
            //while (en.MoveNext())
            //{
            //    if (en.Current.Guid == guid)
            //    {
            //        plg = en.Current;
            //        break;
            //    }
            //}

            //if (plg==null)
            //propertyItem.Value = plg.Guid;
            if (EditorPluginSelection.ListGenerators.Count == 1)
                propertyItem.Value = EditorPluginSelection.ListGenerators[0].Guid;
            ComboBox cbx = new ComboBox();
            cbx.DisplayMemberPath = "Name";
            cbx.SelectedValuePath = "Guid";
            var _binding_lst = new Binding(); // bind to the Value property of the PropertyItem
            _binding_lst.Source = EditorPluginSelection.ListGenerators;
            _binding_lst.ValidatesOnExceptions = false;
            _binding_lst.ValidatesOnDataErrors = false;
            _binding_lst.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(cbx, ComboBox.ItemsSourceProperty, _binding_lst);

            var _binding = new Binding("Value"); // bind to the Value property of the PropertyItem
            _binding.Source = propertyItem;
            _binding.ValidatesOnExceptions = true;
            _binding.ValidatesOnDataErrors = true;
            _binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(cbx, ComboBox.SelectedValueProperty, _binding);
            return cbx;
        }
    }
}
