using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorPluginSelection : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public static ObservableCollectionExt<PluginGenerator> ListGenerators = new();
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Debug.Assert(propertyItem != null);
            // this.Config.GroupPlugins.ListPlugins
            ITreeConfigNode instance = (ITreeConfigNode)propertyItem.Instance;
            ComboBox cbx = new ComboBox
            {
                DisplayMemberPath = "Name",
                SelectedValuePath = "Guid"
            };
            var _binding_lst = new Binding("ListPlugins")
            {
                Source = instance.Cfg.GroupPlugins,
                ValidatesOnExceptions = false,
                ValidatesOnDataErrors = false,
                Mode = BindingMode.OneWay
            }; // bind to the Value property of the PropertyItem
            BindingOperations.SetBinding(cbx, ComboBox.ItemsSourceProperty, _binding_lst);
            // 
            //var en = cnfg.GroupPlugins.ListPlugins.GetEnumerator();
            //if (en.MoveNext())
            //{
            //    var plg = en.Current;
            //    if (!en.MoveNext())
            //    {
            //        propertyItem.Value = plg.Guid;
            //    }
            //}
            var _binding = new Binding("Value")
            {
                Source = propertyItem,
                ValidatesOnExceptions = true,
                ValidatesOnDataErrors = true,
                Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay
            }; // bind to the Value property of the PropertyItem
            BindingOperations.SetBinding(cbx, ComboBox.SelectedValueProperty, _binding);
            return cbx;
        }
    }
}
