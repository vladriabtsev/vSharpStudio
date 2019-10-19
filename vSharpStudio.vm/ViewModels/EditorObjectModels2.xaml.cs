using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    /// <summary>
    /// Interaction logic for EditorObjectModels2.xaml
    /// </summary>
    public partial class EditorObjectModels2 : UserControl, ITypeEditor
    {
        public EditorObjectModels2()
        {
            InitializeComponent();
        }

        public ObservableCollection<SubVm2> ListModels
        {
            get { return (ObservableCollection<SubVm2>)GetValue(ListModelsProperty); }
            set { SetValue(ListModelsProperty, value); }
        }
        public static readonly DependencyProperty ListModelsProperty =
            DependencyProperty.Register("ListModels", typeof(ObservableCollection<SubVm2>), typeof(EditorObjectModels2), new PropertyMetadata(0));

        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            ITreeConfigNode obj = (ITreeConfigNode)propertyItem.Instance;
            IParent parent = (IParent)propertyItem.Instance;
            IParent p = parent;
            while (p.Parent != null)
                p = p.Parent;
            Config cfg = (Config)p;

            this.ListModels = new ObservableCollection<SubVm2>();
            foreach (var t in cfg.GroupModels.ListModels)
            {
                var svm = new SubVm2() { Name = t.Name, Model = t, Node = obj, Guid = t.Guid };
                svm.SetIsSelected(t.CheckIsIncluded(obj));
                this.ListModels.Add(svm);
            }
            return this;
        }
        public class SubVm2 : INotifyPropertyChanged
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
}
