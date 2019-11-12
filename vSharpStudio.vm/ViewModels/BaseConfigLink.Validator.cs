using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class BaseConfigLink
    {
        public partial class BaseConfigLinkValidator
        {
            public BaseConfigLinkValidator()
            {
                this.RuleFor(x => x.RelativeConfigFilePath).NotEmpty();
                this.RuleFor(x => x.RelativeConfigFilePath).Must((o, file) =>
                    {
                        if (string.IsNullOrWhiteSpace(file))
                        {
                            return true;
                        }
                        return File.Exists(file);
                    })
                .WithMessage(Config.ValidationMessages.FILE_IS_NOT_EXISTS);
            }
        }
    }
}
