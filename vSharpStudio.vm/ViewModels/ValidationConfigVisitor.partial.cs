using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ValidationConfigVisitor
    {
        public SortedObservableCollection<ValidationMessage> Result { get; private set; }
        public int CountTotalValidatableNodes;
        public int CountCurrentValidatableNode { get; set; }
        private ProgressVM? progressVM;
        public bool IsCountOnly { get; set; } = true;

        private int _level = -1;
        private ILogger? _logger = null;

        public ValidationConfigVisitor(CancellationToken cancellationToken, ProgressVM? progressVM, ILogger? logger = null)
        {
            this._cancellationToken = cancellationToken;
            this.progressVM = progressVM;
            this.CountCurrentValidatableNode = 0;
            this._logger = logger;
            this.Result = new SortedObservableCollection<ValidationMessage>();
            this.Result.SortDirection = SortDirection.Descending;
        }

        public void UpdateSubstructCounts(ITreeConfigNode p)
        {
            Debug.Assert(p != null);
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
            this.UpdateAddCounts(p, t);
            t.RaiseSeverityLevel(this._level);
            ulong weight = 0;
            ITreeConfigNode nnode = p;
            while (nnode.Parent != null)
            {
                weight++;
                nnode = nnode.Parent;
            }
            if (weight > VmBindable.MaxSortingWeight)
            {
                throw new Exception();
            }

            this.Result.Add(t, VmBindable.MaxSortingWeight - weight);
        }

        private void ValidateSubAndCollectErrors(ITreeConfigNode p, IValidatableWithSeverity sub)
        {
            if (IsCountOnly)
                return;
            if (p is ICanGoLeft || p is ICanGoRight) // is visible in the tree
            {
                this.node = p;
            }

            sub.Validate();
            foreach (var t in sub.ValidationCollection)
            {
                t.Model = this.node;
                this.AddMessage(p, t);
            }
        }
        // only for not ITreeConfigNode
        IValidatableWithSeverity? parent;
        partial void OnVisit(IValidatableWithSeverity p)
        {
            this.CountCurrentValidatableNode++;
            if (IsCountOnly)
                return;
            if (p is ITreeConfigNode pp)
            {
                if (pp == null)
                {
                    if (p is IParent ip)
                    {
                        Debug.Assert(ip.Parent != null);
                        pp = ip.Parent;
                    }
                    else
                        Debug.Assert(false);
                }
                else
                {
                    this._level++;
                }
                this.parent = p;
                if (this._logger != null)
                {
                    this._logger.LogInformation(string.Empty.PadRight(this._level, ' ') + p.GetType().Name + ": " + pp.Name);
                }
                UIDispatcher.Invoke(() =>
                {
                    p.ValidationCollection.Clear();
                    p.CountErrors = 0;
                    p.CountWarnings = 0;
                    p.CountInfos = 0;
                });

                p.Validate();

                UIDispatcher.Invoke(() =>
                {
                    foreach (var t in p.ValidationCollection.ToList())
                    {
                        // t.Model = node;
                        this.AddMessage(pp, t);
                    }
                });
            }
        }

        private object? node = null;

        partial void OnVisitEnd(IValidatableWithSeverity p)
        {
            if (IsCountOnly)
                return;
            this.progressVM?.ProgressUpdate(this.CountCurrentValidatableNode * 100 / this.CountTotalValidatableNodes);
            if (p is ITreeConfigNode pp)
            {
                if (this._logger != null)
                {
                    this._logger.LogInformation(string.Empty.PadRight(this._level, ' ') + pp.GetType().Name + ": " + pp.Name);
                }
                this._level--;
            }
        }
    }
}
