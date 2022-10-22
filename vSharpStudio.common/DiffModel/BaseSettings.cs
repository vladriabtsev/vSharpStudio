using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.common
{
    public class BaseSettings<T, TValidator> : VmValidatableWithSeverityAndAttributes<T, TValidator>, IParent
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
}
