using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    /// <summary>
    /// Interaction logic for ObjectSubModelsEditor.xaml
    /// </summary>
    public partial class ObjectSubModelsEditor : UserControl, ITypeEditor
    {
        public ObjectSubModelsEditor()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", 
            typeof(List<string>), typeof(ObjectSubModelsEditor), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public List<string> Value
        {
            get { return (List<string>)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Binding binding = new Binding("Value");
            binding.Source = propertyItem;
            binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(this, ObjectSubModelsEditor.ValueProperty, binding);


            DataType dt = (DataType)propertyItem.Instance;
            ComboBox cbx = new ComboBox();
            cbx.DisplayMemberPath = "Name";
            cbx.SelectedValuePath = "Guid";
            var _binding_lst = new Binding("ListObjects"); //bind to the Value property of the PropertyItem
            _binding_lst.Source = dt;
            _binding_lst.ValidatesOnExceptions = false;
            _binding_lst.ValidatesOnDataErrors = false;
            _binding_lst.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(cbx, ComboBox.ItemsSourceProperty, _binding_lst);
            var _binding = new Binding("Value"); //bind to the Value property of the PropertyItem
            _binding.Source = propertyItem;
            _binding.ValidatesOnExceptions = true;
            _binding.ValidatesOnDataErrors = true;
            _binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(cbx, ComboBox.SelectedValueProperty, _binding);
            return cbx;



            return this;
        }
    }
}
