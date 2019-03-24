using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace ViewModelBase
{
    public class ValidationMessage<T> : ValidationMessage
      where T : IComparable
    {
        // then higher weight than higher importance of the message
        public ValidationMessage(T model, string propertyName, FluentValidation.Severity severity, byte weight, string message)
            : base(propertyName, severity, weight, message)
        {
            this.Model = model;
        }
        public string FullMessage { get; }
    }

    public abstract class ValidationMessage : ISortingValue, IComparable
    {
        public static int MultiplierShift = 8;
        // than higher weight than higher importance of the message
        public ValidationMessage(string propertyName, FluentValidation.Severity severity, byte weight, string message)
        {
            this.PropertyName = propertyName;
            this.Severity = severity;
            this.Message = message;
            this.SortingValue = (3 - (int)Severity) << MultiplierShift + weight;
        }
        public IComparable Model { get; protected set; }
        public string PropertyName { get; private set; }
        public FluentValidation.Severity Severity { get; private set; }
        public string Message { get; private set; }
        public int SortingValue { get; private set; }

        public int CompareTo(object obj)
        {
            return SortingValue - (obj as ISortingValue).SortingValue;
        }
    }
}
