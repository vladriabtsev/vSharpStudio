using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListCatalogsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(GroupListCatalogsValidator));
        public GroupListCatalogsValidator()
        {
            this.GeneralRules();
            //this.RuleFor(x => x.ShortIdTypeKey).NotEmpty().WithMessage("Can't be empty");
            //this.RuleFor(x => x.ShortIdTypeKey)
            //    .Must((o, id) =>
            //    {
            //        if (string.IsNullOrEmpty(id))
            //            return true;
            //        return !char.IsDigit(id[id.Length - 1]);
            //    })
            //    .WithMessage("Short type ID can't contain digit as a last symbol");
        }
    }
}
