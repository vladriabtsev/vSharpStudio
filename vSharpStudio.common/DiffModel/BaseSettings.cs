using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        [Browsable(false)]
        public ITreeConfigNode? Parent { get; set; }
        public BaseSettings(ITreeConfigNode? parent, TValidator? validator) : base(validator)
        {
            this.Parent = parent;
        }
        protected override void OnIsChangedChanged()
        {
            Debug.Assert(this.Parent != null);
            if (this.Parent is IEditableNodeGroup pp)
            {
                pp.CheckChildrenIsOrHasChanged();
            }
            else if (this.Parent is IEditableObjectExt ed)
            {
                if (this.IsChanged)
                    ed.IsChanged = true;
            }
            else
                throw new NotImplementedException("Parent object of setting error. Interface 'IEditableNodeGroup' or 'IEditableObjectExt' is not implemented");
        }
    }
    public class BaseSubSettings<T, TValidator> : VmValidatableWithSeverityAndAttributes<T, TValidator> //, IParentObject
        where TValidator : AbstractValidator<T>
        where T : VmValidatableWithSeverityAndAttributes<T, TValidator>//, IComparable<T>
    {
        [Browsable(false)]
        public IEditableObjectExt? Parent { get; set; }
        public BaseSubSettings(IEditableObjectExt? parent, TValidator validator) : base(validator)
        {
            this.Parent = parent;
        }
        protected override void OnIsChangedChanged()
        {
            Debug.Assert(this.Parent != null);
            //if (this.Parent is IEditableNodeGroup pp)
            //{
            //    pp.CheckChildrenIsOrHasChanged();
            //}
            //else 
            if (this.Parent is IEditableObjectExt ed)
            {
                if (this.IsChanged)
                    ed.IsChanged = true;
            }
            else
                throw new NotImplementedException("Parent object of setting error. Interface 'IEditableObjectExt' is not implemented");
        }
    }
}
