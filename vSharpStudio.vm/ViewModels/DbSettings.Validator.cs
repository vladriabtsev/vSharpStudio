﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DbSettings
    {
        public partial class DbSettingsValidator
        {
            public DbSettingsValidator()
            {
                this.RuleFor(x => x.KeyName).NotEmpty().WithMessage(Config.ValidationMessages.PKEY_NAME_CANT_BE_EMPTY);
            }
        }
    }
}