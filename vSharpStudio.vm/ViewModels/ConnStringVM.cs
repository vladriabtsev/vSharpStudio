using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class ConnStringVM : VmValidatableWithSeverity<ConnStringVM, ConnStringVMValidator>
    {
        private ILogger? logger;

        public ConnStringVM()
            : base(ConnStringVMValidator.Validator)
        {
        }

        public ConnStringVM(ILogger? logger)
            : this()
        {
            this.logger = logger;
        }

        public string? Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                this._Name = value;
                this.NotifyPropertyChanged();
            }
        }
        private string? _Name;

        public string? ConnectionString
        {
            get
            {
                return this._ConnectionString;
            }

            set
            {
                this._ConnectionString = value;
                this.NotifyPropertyChanged();
            }
        }
        private string? _ConnectionString;

        public string? Provider
        {
            get
            {
                return this._Provider;
            }

            set
            {
                this._Provider = value;
                this.NotifyPropertyChanged();
            }
        }
        private string? _Provider;
    }
}
