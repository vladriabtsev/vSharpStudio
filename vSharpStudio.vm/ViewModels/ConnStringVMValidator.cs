﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConnStringVMValidator : ValidatorBase<ConnStringVM, ConnStringVMValidator>
    {
        public ConnStringVMValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Provider).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY).WithSeverity(Severity.Warning);
            this.RuleFor(x => x.ConnectionString).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY).WithSeverity(Severity.Warning);
            // RuleFor(x => x.ConnectionString).Must((o, name) => { return IsDbExists(o); }).WithMessage("Can't connect to database").WithSeverity(Severity.Info);
            // RuleFor(x => x.ConnectionString).Must((o, name) => { return IsCanConnectServer(o); }).WithMessage("Can't connect to server").WithSeverity(Severity.Info);
            // RuleFor(x => x.Provider).Must((o, name) => { return IsProviderPluginExists(o); }).WithMessage("There is no DB plugin to support this provider");
            // RuleFor(x => x.Provider).Must((o, name) => { return IsManyProviders(o); }).WithMessage("There are more than one DB plugin to support this provider").WithSeverity(Severity.Warning);
        }
    }
}
