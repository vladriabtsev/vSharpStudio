using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorDateMonthDayUpDown : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Debug.Assert(propertyItem != null);
            var ctrl = new DateTimeUpDown();
            ctrl.Format = DateTimeFormat.MonthDay;
            var _binding = new Binding("Value"); // bind to the Value property of the PropertyItem
            _binding.Source = propertyItem;
            _binding.ValidatesOnExceptions = true;
            _binding.ValidatesOnDataErrors = true;
            _binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(ctrl, DateTimeUpDown.ValueProperty, _binding);
            return ctrl;
        }
    }
}
