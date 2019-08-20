using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ValidationConfigVisitor
    {
        public SortedObservableCollection<ValidationMessage> Result { get; private set; }
        private int _level = -1;
        private ILogger _logger = null;
        public ValidationConfigVisitor(CancellationToken cancellationToken, ILogger logger = null)
        {
            this._cancellationToken = cancellationToken;
            this._logger = logger;
            this.Result = new SortedObservableCollection<ValidationMessage>();
            this.Result.SortDirection = SortDirection.Descending;
        }
        public void UpdateSubstructCounts(ITreeConfigNode p)
        {
            var pp = p;
            while (pp.Parent != null)
            {
                pp = pp.Parent;
                pp.CountErrors -= p.CountErrors;
                pp.CountWarnings -= p.CountWarnings;
                pp.CountInfos -= p.CountInfos;
            }
        }
        private void UpdateAddCounts(ITreeConfigNode p, ValidationMessage m)
        {
            switch (m.Severity)
            {
                case FluentValidation.Severity.Error:
                    p.CountErrors++;
                    while (p.Parent != null)
                    {
                        p = p.Parent;
                        p.CountErrors++;
                    }
                    break;
                case FluentValidation.Severity.Warning:
                    p.CountWarnings++;
                    while (p.Parent != null)
                    {
                        p = p.Parent;
                        p.CountWarnings++;
                    }
                    break;
                case FluentValidation.Severity.Info:
                    p.CountInfos++;
                    while (p.Parent != null)
                    {
                        p = p.Parent;
                        p.CountInfos++;
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        private void AddMessage(ITreeConfigNode p, ValidationMessage t)
        {
            UpdateAddCounts(p, t);
            t.RaiseSeverityLevel(_level);
            ulong weight = 0;
            ITreeConfigNode nnode = p;
            while (nnode.Parent != null)
            {
                weight++;
                nnode = nnode.Parent;
            }
            if (weight > ViewModelBindable.MaxSortingWeight)
                throw new Exception();
            Result.Add(t, ViewModelBindable.MaxSortingWeight - weight);
        }
        private void ValidateSubAndCollectErrors(ITreeConfigNode p, IValidatableWithSeverity sub)
        {
            if (p is ICanGoLeft || p is ICanGoRight) // is visible in the tree
                node = p;
            sub.Validate();
            foreach (var t in sub.ValidationCollection)
            {
                t.Model = node;
                AddMessage(p, t);
            }
        }
        partial void OnVisit(IValidatableWithSeverity p)
        {
            //if (p is ICanGoLeft || p is ICanGoRight) // is visible in the tree
            //    node = p;
            _level++;
            if (!(p is ITreeConfigNode))
                throw new ArgumentException();
            var pp = p as ITreeConfigNode;
            if (_logger != null)
            {
                _logger.LogInformation("".PadRight(_level, ' ') + pp.GetType().Name + ": " + pp.Name);
            }
            p.ValidationCollection.Clear();
            p.CountErrors = 0;
            p.CountWarnings = 0;
            p.CountInfos = 0;

            p.Validate();

            foreach (var t in p.ValidationCollection)
            {
                //                t.Model = node;
                AddMessage(pp, t);
            }
        }
        private object node = null;
        partial void OnVisitEnd(IValidatableWithSeverity p)
        {
            if (!(p is ITreeConfigNode))
                throw new ArgumentException();
            var pp = p as ITreeConfigNode;
            if (_logger != null)
            {
                _logger.LogInformation("".PadRight(_level, ' ') + pp.GetType().Name + ": " + pp.Name);
            }
            _level--;
        }
    }
}
