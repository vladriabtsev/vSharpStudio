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
        public MainPageVM()
            : base(MainPageVMValidator.Validator)
        {
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
