using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public struct EntityObjectProblem
    {
        public IValidatableWithSeverity EntityObject;
        public string Message;
    }
}
