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

namespace vSharpStudio.vm.ViewModels
{
    public class EditorObjectModels : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            ITreeConfigNode obj = (ITreeConfigNode)propertyItem.Instance;
            IParent parent = (IParent)propertyItem.Instance;
            IParent p = parent;
            while (p.Parent != null)
                p = p.Parent;
            Config cfg = (Config)p;
            CheckListBox clbx = new CheckListBox();
            List<SubVm> lst = new List<SubVm>();
            foreach (var t in cfg.GroupSubModels.ListModels)
            {
                var svm = new SubVm() { Name = t.Name, SubModel = t, Node = obj };
                svm.SetIsSelected(t.CheckIsSelected(obj));
                lst.Add(svm);
            }
            clbx.DisplayMemberPath = "Name";
            clbx.IsSelectAllActive = true;
            clbx.SelectedMemberPath = "IsSelected";
            clbx.Delimiter = ", ";
            clbx.ItemsSource = lst;
            return clbx;
        }
        public class SubVm : INotifyPropertyChanged
        {
            public string Name { get; set; }
            public bool? IsSelected
            {
                get { return _IsSelected; }
                set
                {
                    if (_IsSelected != value)
                    {
                        var prev = _IsSelected;
                        _IsSelected = value;
                        OnPropertyChanged("IsSelected");
                        SubModel.IsSelectedChanged(this.Node.Guid, prev, _IsSelected);
                    }
                }
            }
            private bool? _IsSelected;
            public void SetIsSelected(bool? val)
            {
                _IsSelected = val;
            }
            public Model SubModel;
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
}
