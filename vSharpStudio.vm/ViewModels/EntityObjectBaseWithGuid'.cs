using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class EntityObjectBaseWithGuid<T, TValidator> : EntityObjectBase<T, TValidator>, IGuid, IComparable
      where TValidator : AbstractValidator<T>
      where T : EntityObjectBaseWithGuid<T, TValidator>
    {
        public EntityObjectBaseWithGuid(TValidator validator, SortableObservableCollection<ValidationMessage> validationCollection )
            : base(validator, validationCollection)
        {
        }
        public string Guid { get; protected set; }
        public int CompareTo(object obj)
        {
            if (obj.GetType().FullName != typeof(T).FullName)
                return 1;
            if (((T)obj).Guid != this.Guid)
                return 1;
            return 0;
        }
    }
}
