using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.common
{
    public class BaseSettings<T, TValidator> : VmValidatableWithSeverityAndAttributes<T, TValidator>, IParent //, IParentObject
        where TValidator : AbstractValidator<T>
        where T : VmValidatableWithSeverityAndAttributes<T, TValidator>//, IComparable<T>
    {
        [BrowsableAttribute(false)]
        public ITreeConfigNode? Parent { get; set; }
        public BaseSettings(ITreeConfigNode? parent, TValidator validator) : base(validator)
        {
            this.Parent = parent;
        }
    }
    public class BaseSubSettings<T, TValidator> : VmValidatableWithSeverityAndAttributes<T, TValidator> //, IParentObject
        where TValidator : AbstractValidator<T>
        where T : VmValidatableWithSeverityAndAttributes<T, TValidator>//, IComparable<T>
    {
        [BrowsableAttribute(false)]
        public IEditableObjectExt? Parent { get; set; }
        public BaseSubSettings(IEditableObjectExt? parent, TValidator validator) : base(validator)
        {
            this.Parent = parent;
        }
    }
}
