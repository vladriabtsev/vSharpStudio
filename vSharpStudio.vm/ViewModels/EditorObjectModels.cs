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
using Xceed.Wpf.Toolkit;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorObjectModels : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public List<SubVm> ListModels { get; set; }
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            ITreeConfigNode obj = (ITreeConfigNode)propertyItem.Instance;
            IParent parent = (IParent)propertyItem.Instance;
            IParent p = parent;
            while (p.Parent != null)
                p = p.Parent;
            Config cfg = (Config)p;
            ListCheckBox clbx = new ListCheckBox();
            this.ListModels = new List<SubVm>();
            foreach (var t in cfg.GroupModels.ListModels)
            {
                var svm = new SubVm() { Name = t.Name, Model = t, Node = obj, Guid=t.Guid };
                svm.SetIsSelected(t.CheckIsIncluded(obj));
                this.ListModels.Add(svm);
            }
            var _binding_lst = new Binding("ListModels");
            _binding_lst.Source = this;
            _binding_lst.ValidatesOnExceptions = false;
            _binding_lst.ValidatesOnDataErrors = false;
            _binding_lst.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(clbx, ListCheckBox.ItemsSourceProperty, _binding_lst);
            clbx.SelectionMode = SelectionMode.Multiple;
            //clbx.Padding = new Thickness(5);
            clbx.BorderThickness = new Thickness(0);
            return clbx;
        }
    }
    public class SubVm : INotifyPropertyChanged
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected != value)
                {
                    Model.OnIsIncludedChanging(this.Node, value);
                    _IsSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }
        private bool _IsSelected;
        public void SetIsSelected(bool? val)
        {
            _IsSelected = val ?? false;
        }
        public Model Model { get; set; }
        public ITreeConfigNode Node;

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
