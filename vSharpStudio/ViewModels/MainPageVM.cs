using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            this.Model.CatalogGroup.ListCatalogs.Add(c);
        }

        private ITreeConfigNode SelectedNode;
        internal void OnSelectedItemChanged(object oldValue, object newValue)
        {
            this.SelectedNode = (ITreeConfigNode)newValue;
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

        //public ICommand CommandAdd { get; } = new RelayCommand((p) =>
        //{
        //    MainPageVM vm = (MainPageVM)p;
        //    return vm.SelectedNode != null && vm.SelectedNode is IConfigObject;
        //}, (o) => {
        //    MainPageVM vm = (MainPageVM)o;
        //    (vm.SelectedNode as IConfigObject).Create();
        //});
    }
}
