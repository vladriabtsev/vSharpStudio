using System;
using System.Diagnostics;
using CommunityToolkit.Diagnostics;
using FluentValidation;

namespace ViewModelBase
{
    //[DebuggerDisplay("{SeverityName,nq}, {SeverityWeightName,nq}, {SortingValue,nq}: {Message,nq}")]
    //public class ValidationMessage<T> : ValidationMessage
    //  where T : class
    //{
    //    // then higher weight than higher importance of the message
    //    public ValidationMessage(T model, string propertyName, FluentValidation.Severity severity, SeverityWeight weight, string message)
    //        : base(propertyName, severity, weight, message)
    //    {
    //        this.Model = model;
    //    }
    //    public string FullMessage { get; }
    //}
    public enum SeverityWeight { VeryLow, Low, Normal, High, VeryHigh }
    [DebuggerDisplay("{SeverityName,nq}, {SeverityWeightName,nq}, {SortingValue,nq}: {Message,nq}")]
    public class ValidationMessage : ISortingValue, IComparable<ValidationMessage>
    {
        public SortedObservableCollection<K> GetCollection<K>() where K : ISortingValue
        {
            throw new NotImplementedException();
        }
        public bool IsCanSortByName { get { return false; } }
        public string NameToCompare { get { return string.Empty; } }
        private static readonly int _lenSeverity = Enum.GetNames(typeof(FluentValidation.Severity)).Length;
        private static readonly int _lenSeverityWeight = Enum.GetNames(typeof(SeverityWeight)).Length;
        // than higher weight than higher importance of the message
        public ValidationMessage(object model, string propertyName, FluentValidation.Severity severity, SeverityWeight weight, string message)
        {
            // weight has keep sub levels between main severity levels
            //if (weight > (1 << MultiplierShift))
            //    throw new ArgumentException("parameter 'weight' expected to be less than " + (1 << MultiplierShift));
            this.Model = model;
            this.PropertyName = propertyName;
            this.Severity = severity;
            this.SeverityWeight = weight;
            this.Message = message;
            this.ExplicitSortingPosition = ValidationMessage._lenSeverityWeight * (2 - (int)Severity) + (int)weight;
        }
        public object? Model { get; set; }
        public string PropertyName { get; private set; }
        public FluentValidation.Severity Severity { get; private set; }
        public string? SeverityName { get { return Enum.GetName(typeof(FluentValidation.Severity), (int)Severity); } }
        public string IconName
        {
            get
            {
                switch (Severity)
                {
                    case Severity.Error:
                        return "iconStatusCriticalError";
                    case Severity.Warning:
                        return "iconStatusWarning";
                    case Severity.Info:
                        return "iconStatusInformation";
                }
                return "iconStatusInvalid";
            }
        }
        public SeverityWeight SeverityWeight { get; private set; }
        public string SeverityWeightName
        {
            get
            {
                switch (SeverityWeight)
                {
                    case SeverityWeight.VeryHigh:
                        return "!!";
                    case SeverityWeight.High:
                        return "!";
                    case SeverityWeight.Normal:
                        return "";
                    case SeverityWeight.Low:
                        return "?";
                    case SeverityWeight.VeryLow:
                        return "??";
                }
                return "";
            }
        }
        public string Message { get; private set; }
        public int ExplicitSortingPosition { get; set; }
        /// <summary>
        /// Raise severity level for message. SortingValue will be increased by shifting to left. 
        /// </summary>
        /// <param name="shift"></param>
        public void RaiseSeverityLevel(int shiftLevel)
        {
            Guard.IsBetweenOrEqualTo(shiftLevel, 0, int.MaxValue / (ValidationMessage._lenSeverityWeight * ValidationMessage._lenSeverity));
            ExplicitSortingPosition += ValidationMessage._lenSeverityWeight * ValidationMessage._lenSeverity * shiftLevel;
        }
        public int CompareTo(ValidationMessage? other)
        {
            if (other == null)
                return -1;
            return this.ExplicitSortingPosition.CompareTo(other.ExplicitSortingPosition);
        }
        public void SetExplicitSortingPosition(int sortPosition)
        {
            this.ExplicitSortingPosition = sortPosition;
        }
    }
}
