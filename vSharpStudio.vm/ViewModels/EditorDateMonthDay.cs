using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorDateMonthDay : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Debug.Assert(false, "Need converter Timestamp-Datetime");
            Debug.Assert(propertyItem != null);
            var ctrl = new DateTimePicker
            {
                Format = DateTimeFormat.MonthDay
            };
            var _binding = new Binding("Value")
            {
                Source = propertyItem,
                ValidatesOnExceptions = true,
                ValidatesOnDataErrors = true,
                Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay
            }; // bind to the Value property of the PropertyItem
            BindingOperations.SetBinding(ctrl, DateTimePicker.ValueProperty, _binding);
            return ctrl;
        }
    }
}
