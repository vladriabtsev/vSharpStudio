using System.IO;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class BaseConfigLinkValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(BaseConfigLinkValidator));
        public BaseConfigLinkValidator()
        {
            this.GeneralRules();
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
            this.RuleFor(x => x.RelativeConfigFilePath).Must((o, file) =>
            {
                if (string.IsNullOrWhiteSpace(file))
                {
                    return true;
                }
                return Path.GetExtension(file) == ".vcfg";
            })
            .WithMessage("Expected file extention '.vcfg'");
        }
    }
}
