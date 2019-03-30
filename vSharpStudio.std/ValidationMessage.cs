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
        public static int MultiplierShift = 2; // only 4 severity sub levels per FluentValidation.Severity
        // than higher weight than higher importance of the message
        public ValidationMessage(string propertyName, FluentValidation.Severity severity, ushort weight, string message)
        {
            // weight has keep sub levels between main severity levels
            if (weight > (1 << MultiplierShift))
                throw new ArgumentException("parameter 'weight' expected to be less than " + (1 << MultiplierShift));
            this.PropertyName = propertyName;
            this.Severity = severity;
            this.Message = message;
            this.SortingValue = 1 << MultiplierShift * (3 - (int)Severity) + weight;
        }
        public IComparable Model { get; protected set; }
        public string PropertyName { get; private set; }
        public FluentValidation.Severity Severity { get; private set; }
        public string Message { get; private set; }
        public int SortingValue { get; private set; }
        /// <summary>
        /// Raise severity level for message. SortingValue will be increased by shifting to left. 
        /// </summary>
        /// <param name="shift"></param>
        public void RaiseSeverityLevel(ushort shiftLevel)
        {
            int n = 32 / (MultiplierShift * 3);
            if (shiftLevel > n)
                throw new ArgumentException("parameter 'weight' expected to be less or equal " + n);
            SortingValue = SortingValue << (MultiplierShift * 3 * shiftLevel);
        }
        public int CompareTo(object obj)
        {
            return SortingValue - (obj as ISortingValue).SortingValue;
        }
    }
}
