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
        public BaseSettings(TValidator validator) : base(validator)
        {
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }
        public BaseSettings(ITreeConfigNode parent, TValidator validator) : base(validator)
        {
            this.Parent = parent;
        }
    }
    public class BaseSubSettings<T, TValidator> : VmValidatableWithSeverityAndAttributes<T, TValidator> //, IParentObject
        where TValidator : AbstractValidator<T>
        where T : VmValidatableWithSeverityAndAttributes<T, TValidator>//, IComparable<T>
    {
        public BaseSubSettings(TValidator validator) : base(validator)
        {
        }
        [BrowsableAttribute(false)]
        public BaseSettings<T, TValidator> Parent { get; set; }
        public BaseSubSettings(BaseSettings<T, TValidator> parent, TValidator validator) : base(validator)
        {
            this.Parent = parent;
        }
    }
}
