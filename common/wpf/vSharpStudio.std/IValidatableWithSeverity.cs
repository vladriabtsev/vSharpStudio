using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModelBase
{
    public interface IValidatableWithSeverity : IValidatable
    {
        //IValidatableWithSeverity Parent { get; set; }
        int CountErrors { get; set; }
        int CountWarnings { get; set; }
        int CountInfos { get; set; }
        SortedObservableCollection<ValidationMessage> ValidationCollection { get; set; }
    }
}
