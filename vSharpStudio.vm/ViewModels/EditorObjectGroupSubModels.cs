using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorObjectGroupSubModels : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            IParent dt = (IParent)propertyItem.Instance;
            IParent p = dt;
            while (p.Parent != null)
                p = p.Parent;
            Config cfg = (Config)p;
            //ISubModelsList smlst = (ISubModelsList)dt;

            //Dictionary<string, string> dicIncl = new Dictionary<string, string>();
            //foreach (var t in smlst.SubModelsI.ListIncludedSubModelsI)
            //{
            //    dicIncl[t] = null;
            //}
            //Dictionary<string, string> dicExcl = new Dictionary<string, string>();
            //foreach (var t in smlst.SubModelsI.ListExcludedSubModelsI)
            //{
            //    dicExcl[t] = null;
            //}

            ComboBox cbx = new ComboBox();
            List<SubVm> lst = new List<SubVm>();
            //foreach (var t in cfg.GroupSubModels.ListSubModels)
            //{
            //    var svm = new SubVm() { Name = t.Name };
            //    if (dicIncl.ContainsKey(t.Guid))
            //        svm.SetIsIncluded(true);
            //    lst.Add(svm);
            //}
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
        }
        public class SubVm : INotifyPropertyChanged
        {
            public string Name { get; set; }
            public bool? IsIncluded
            {
                get { return _IsIncluded; }
                set
                {
                    _IsIncluded = value;
                    OnPropertyChanged("IsIncluded");
                }
            }
            private bool? _IsIncluded;
            public void SetIsIncluded(bool? val)
            {
                _IsIncluded = val;
            }
            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged(string name)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

            #endregion
        }
    }
}
