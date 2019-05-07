using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class DataTypeObjectNameEditor : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            DataType dt = (DataType)propertyItem.Instance;
            ComboBox cbx = new ComboBox();
            cbx.Tag = dt;
            cbx.GotFocus += Cbx_GotFocus;
            var _binding = new Binding("Value"); //bind to the Value property of the PropertyItem
            _binding.Source = propertyItem;
            _binding.ValidatesOnExceptions = true;
            _binding.ValidatesOnDataErrors = true;
            _binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(cbx, ComboBox.TextProperty, _binding);
            cbx.DisplayMemberPath = "Name";
            return cbx;
        }

        private void Cbx_GotFocus(object sender, RoutedEventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            DataType dt = (DataType)cbx.Tag;
            ITreeConfigNode config = dt.Parent;
            while (config.Parent != null)
                config = config.Parent;
            SortedObservableCollection<ITreeConfigNode> lst = new SortedObservableCollection<ITreeConfigNode>();
            switch (dt.DataTypeEnum)
            {
                case Proto.Config.proto_data_type.Types.EnumDataType.Catalog:
                    lst = (config as Config).GroupCatalogs.Children;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Document:
                    lst = (config as Config).GroupDocuments.Children;
                    break;
                default:
                    break;
            }
            cbx.ItemsSource = lst;
        }
    }
}
