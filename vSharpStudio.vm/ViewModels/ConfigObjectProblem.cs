using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public struct ConfigObjectProblem
    {
        public IValidatableWithSeverity EntityObject;
        public string Message;
    }
}
