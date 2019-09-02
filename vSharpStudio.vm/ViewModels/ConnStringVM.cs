using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class ConnStringVM : ViewModelValidatableWithSeverity<ConnStringVM, ConnStringVMValidator>
    {
        private ILogger logger;
        public ConnStringVM() : base(ConnStringVMValidator.Validator)
        {
        }
        public ConnStringVM(ILogger logger) : this()
        {
            this.logger = logger;
        }
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged();
            }
        }
        private string _Name;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set
            {
                _ConnectionString = value;
                NotifyPropertyChanged();
            }
        }
        private string _ConnectionString;
        public string Provider
        {
            get { return _Provider; }
            set
            {
                _Provider = value;
                NotifyPropertyChanged();
            }
        }
        private string _Provider;
    }
}
