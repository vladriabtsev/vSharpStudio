using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorGenNodeSettings : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Debug.Assert(propertyItem != null);
            INodeGenSettings instance = (INodeGenSettings)propertyItem.Instance;
            ComboBox cbx = new ComboBox
            {
                DisplayMemberPath = "GroupInfo",
                SelectedValuePath = "GroupGuid"
            };
            var _binding_lst = new Binding("ListPluginsGroups")
            {
                Source = instance,
                ValidatesOnExceptions = false,
                ValidatesOnDataErrors = false,
                Mode = BindingMode.OneWay
            }; // bind to the Value property of the PropertyItem
            BindingOperations.SetBinding(cbx, ComboBox.ItemsSourceProperty, _binding_lst);
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
