using System;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupDocumentsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(GroupDocumentsValidator));
        public GroupDocumentsValidator()
        {
            this.GeneralRules();
        }
    }
}
