using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModelBase
{
    public abstract class ValidationMessage : ISortingValue
    {
        public static int MultiplierShift = 16;
        // then higher weight than higher importance of the message
        public ValidationMessage(FluentValidation.Severity severity, string message, int weight)
        {
            Severity = severity;
            Message = message;
            SortingValue = (3 - (int)Severity) << MultiplierShift + weight;
        }
        public IComparable Model { get; protected set; }
        public FluentValidation.Severity Severity { get; }
        public string Message { get; }
        public int SortingValue { get; private set; }
    }
}
