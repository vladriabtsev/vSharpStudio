using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorRegisterDimensionCatalogGuid : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Debug.Assert(propertyItem != null);
            RegisterDimension dt = null!;
            if (propertyItem.Instance is RegisterDimension rd)
                dt = rd;
            else
                throw new Exception();
            ComboBox cbx = new ComboBox
            {
                DisplayMemberPath = "Name",
                SelectedValuePath = "Guid"
            };
            var _binding_lst = new Binding("ListCatalogs")
            {
                Source = dt,
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
