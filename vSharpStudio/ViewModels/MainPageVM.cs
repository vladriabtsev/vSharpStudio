using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.ViewModels
{
    public class MainPageVM : ViewModelValidatableWithSeverity<MainPageVM, MainPageVMValidator>
    {
        public MainPageVM() : base(MainPageVMValidator.Validator)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                //Catalog c = new Catalog();
                //this.Model = new ConfigRoot();
                //this.Model.Catalogs.ListCatalogs.Add(c);
                return;
            }
            Catalog c = new Catalog();
            this.Model = new ConfigRoot();
            this.Model.Catalogs.ListCatalogs.Add(c);
        }

        private ITreeNode SelectedNode;
        internal void OnSelectedItemChanged(object oldValue, object newValue)
        {
            this.SelectedNode = (ITreeNode)newValue;
        }

        public ConfigRoot Model
        {
            set
            {
                _Model = value;
                NotifyPropertyChanged();
                ValidateProperty();
            }
            get { return _Model; }
        }
        private ConfigRoot _Model;
    }
}
