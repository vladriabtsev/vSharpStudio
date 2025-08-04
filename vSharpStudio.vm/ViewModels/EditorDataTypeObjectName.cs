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
    public class EditorDataTypeObjectName : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Debug.Assert(propertyItem != null);
            ComboBox cbx = new ComboBox
            {
                DisplayMemberPath = "Name",
                SelectedValuePath = "Guid"
            };
            var _binding_lst = new Binding("ListObjects")
            {
                ValidatesOnExceptions = false,
                ValidatesOnDataErrors = false,
                Mode = BindingMode.OneWay
            }; // bind to the Value property of the PropertyItem
            if (propertyItem.Instance is DataType)
                _binding_lst.Source = (DataType)propertyItem.Instance;
            else if (propertyItem.Instance is Property)
                _binding_lst.Source = ((Property)propertyItem.Instance).DataType;
            else if (propertyItem.Instance is Constant)
                _binding_lst.Source = ((Constant)propertyItem.Instance).DataType;
            else if (propertyItem.Instance is RelationManyToMany)
            {
                if (propertyItem.PropertyName == "GuidObj1")
                    _binding_lst = new Binding("ListObjectsNode1"); // bind to the Value property of the PropertyItem
                else
                    _binding_lst = new Binding("ListObjectsNode2"); // bind to the Value property of the PropertyItem
                _binding_lst.ValidatesOnExceptions = false;
                _binding_lst.ValidatesOnDataErrors = false;
                _binding_lst.Mode = BindingMode.OneWay;
                _binding_lst.Source = propertyItem.Instance;
            }
            else if (propertyItem.Instance is RelationOneToOne)
                _binding_lst.Source = propertyItem.Instance;
            else if (propertyItem.Instance is RelationNode)
                _binding_lst.Source = propertyItem.Instance;
            else
                throw new Exception();
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
