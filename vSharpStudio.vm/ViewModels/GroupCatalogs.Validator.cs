using System;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupCatalogsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(GroupDocumentsValidator));
        public GroupCatalogsValidator()
        {
            this.GeneralRules();
        }
    }
}
