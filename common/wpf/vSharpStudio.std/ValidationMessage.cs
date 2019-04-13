using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentValidation;

namespace ViewModelBase
{
    [DebuggerDisplay("{SeverityName,nq}, {SeverityWeightName,nq}, {SortingValue,nq}: {Message,nq}")]
    public class ValidationMessage<T> : ValidationMessage
      where T : class
    {
        // then higher weight than higher importance of the message
        public ValidationMessage(T model, string propertyName, FluentValidation.Severity severity, SeverityWeight weight, string message)
            : base(propertyName, severity, weight, message)
        {
            this.Model = model;
        }
        public string FullMessage { get; }
    }
    public enum SeverityWeight { VeryLow, Low, Normal, High, VeryHigh }
    public abstract class ValidationMessage : ISortingValue, IComparable<ValidationMessage>
    {
        private static int _lenSeverity = Enum.GetNames(typeof(FluentValidation.Severity)).Length;
        private static int _lenSeverityWeight = Enum.GetNames(typeof(SeverityWeight)).Length;
        // than higher weight than higher importance of the message
        public ValidationMessage(string propertyName, FluentValidation.Severity severity, SeverityWeight weight, string message)
        {
            // weight has keep sub levels between main severity levels
            //if (weight > (1 << MultiplierShift))
            //    throw new ArgumentException("parameter 'weight' expected to be less than " + (1 << MultiplierShift));
            this.PropertyName = propertyName;
            this.Severity = severity;
            this.SeverityWeight = weight;
            this.Message = message;
            this.SortingValue = (ulong)(ValidationMessage._lenSeverityWeight * (2 - (int)Severity) + (int)weight);
        }
        public object Model { get; protected set; }
        public string PropertyName { get; private set; }
        public FluentValidation.Severity Severity { get; private set; }
        public string SeverityName { get { return Enum.GetName(typeof(FluentValidation.Severity), (int)Severity); } }
        public SeverityWeight SeverityWeight { get; private set; }
        public string SeverityWeightName { get { return Enum.GetName(typeof(SeverityWeight), (int)SeverityWeight); } }
        public string Message { get; private set; }
        public ulong SortingValue { get; set; }
        public ulong SortingWeight { get; set; }
        /// <summary>
        /// Raise severity level for message. SortingValue will be increased by shifting to left. 
        /// </summary>
        /// <param name="shift"></param>
        public void RaiseSeverityLevel(int shiftLevel)
        {
            if (shiftLevel < 0)
                throw new ArgumentException("parameter 'shiftLevel' expected to be greater or equal 0");
            int n = int.MaxValue / (ValidationMessage._lenSeverityWeight * ValidationMessage._lenSeverity);
            if (shiftLevel > n)
                throw new ArgumentException("parameter 'shiftLevel' expected to be less or equal " + n);
            SortingValue += (ulong)(ValidationMessage._lenSeverityWeight * ValidationMessage._lenSeverity * shiftLevel);
        }
        public int CompareTo(ValidationMessage other)
        {
            return this.SortingValue.CompareTo(other.SortingValue);
        }
    }
}
