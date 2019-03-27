using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public interface ITreeNodeWithValidation
    {
        int ValidationQty { get; }
        FluentValidation.Severity ValidationSeverity { get; }
    }
}
