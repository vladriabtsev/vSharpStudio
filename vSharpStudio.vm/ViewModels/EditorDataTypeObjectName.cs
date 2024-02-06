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
            ComboBox cbx = new ComboBox();
            cbx.DisplayMemberPath = "Name";
            cbx.SelectedValuePath = "Guid";
            var _binding_lst = new Binding("ListObjects"); // bind to the Value property of the PropertyItem
            _binding_lst.ValidatesOnExceptions = false;
            _binding_lst.ValidatesOnDataErrors = false;
            _binding_lst.Mode = BindingMode.OneWay;
            if (propertyItem.Instance is DataType)
                _binding_lst.Source = (DataType)propertyItem.Instance;
            else if (propertyItem.Instance is Property)
                _binding_lst.Source = ((Property)propertyItem.Instance).DataType;
            else if (propertyItem.Instance is Constant)
                _binding_lst.Source = ((Constant)propertyItem.Instance).DataType;
            else if (propertyItem.Instance is RelationManyToMany)
                _binding_lst.Source = propertyItem.Instance;
            else if (propertyItem.Instance is ManyToManyDocumentsRelation)
                _binding_lst.Source = propertyItem.Instance;
            else
                throw new Exception();
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
