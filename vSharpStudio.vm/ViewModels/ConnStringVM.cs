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
        private readonly ILogger? logger;

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
                SetProperty(ref this._Name, value);
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
                SetProperty(ref this._ConnectionString, value);
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
                SetProperty(ref this._Provider, value);
            }
        }
        private string? _Provider;
    }
}
