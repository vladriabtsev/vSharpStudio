using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class ValidationMessage<T> : ValidationMessage
      where T : ViewModelBindable<T>, IComparable
    {
        // then higher weight than higher importance of the message
        public ValidationMessage(T model, FluentValidation.Severity severity, string message, int weight) 
            : base(severity, message, weight)
        {
            Model = model;
        }
        public string FullMessage { get; }
    }
}
