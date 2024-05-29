using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorCodeSequenceSelection : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Debug.Assert(propertyItem != null);
            // this.Config.GroupPlugins.ListPlugins
            ITreeConfigNode? instance = (ITreeConfigNode?)((IParent)propertyItem.Instance).Parent;
            Debug.Assert(instance != null);
            ComboBox cbx = new ComboBox();
            cbx.DisplayMemberPath = "Text";
            cbx.SelectedValuePath = "Value";
            var _binding_lst = new Binding(); // bind to the Value property of the PropertyItem
            ObservableCollectionExt<ITextValue> listSequences = new();
            listSequences.Add(new TextValue() { Text="<Not selected>" });
            foreach (var t in instance.Cfg.Model.GroupDocuments.GroupListSequences.ListEnumeratorSequences)
            {
                listSequences.Add(t);
            }
            _binding_lst.Source = listSequences;
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
