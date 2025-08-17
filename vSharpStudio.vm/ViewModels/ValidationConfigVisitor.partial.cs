using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using ApplicationLogging;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ValidationConfigVisitor
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(ValidationConfigVisitor));
        public SortedObservableCollection<ValidationMessage> Result { get; private set; }
        public int CountTotalValidatableNodes;
        public int _CountCurrentValidatableNode;
        public int CountCurrentValidatableNode
        {
            get { return this._CountCurrentValidatableNode; }
            set
            {
                //#if DEBUG
                //                if (value == 0)
                //                {
                //                    hashValidatableNodeGuid.Clear();
                //                }
                //#endif
                this._CountCurrentValidatableNode = value;
            }
        }
        private readonly ProgressVM? progressVM;
        public bool IsCountOnly { get; set; } = true;

        private int _level = -1;

        public ValidationConfigVisitor(ProgressVM? progressVM, CancellationToken cancellationToken)
        {
            //_logger.Trace();
            this._cancellationToken = cancellationToken;
            this.progressVM = progressVM;
            this.CountCurrentValidatableNode = 0;
            this.Result = new SortedObservableCollection<ValidationMessage>
            {
                SortDirection = SortDirection.Descending
            };
        }

        public void UpdateMinusCounts(ITreeConfigNode p)
        {
            Debug.Assert(p != null);
            var pp = p;
            //_logger.Trace("Minus Counts. Node: {Name}, Err: {CountErrors}, Wrn: {CountWarnings}, Inf: {CountInfos},", pp.Name, pp.CountErrors, pp.CountWarnings, pp.CountInfos);
            while (pp.Parent != null)
            {
                pp = pp.Parent;
                pp.CountErrors -= p.CountErrors;
                pp.CountWarnings -= p.CountWarnings;
                pp.CountInfos -= p.CountInfos;
                //_logger.Trace("Updated Parent Counts. Node: {Name}, Err: {CountErrors}, Wrn: {CountWarnings}, Inf: {CountInfos},", pp.Name, pp.CountErrors, pp.CountWarnings, pp.CountInfos);
            }
        }

        private void UpdateAddCounts(ITreeConfigNode p, ValidationMessage m)
        {
            //_logger.Trace("Before Add Counts. Node: {Name}, Err: {CountErrors}, Wrn: {CountWarnings}, Inf: {CountInfos}", p.Name, p.CountErrors, p.CountWarnings, p.CountInfos);
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
                    throw new ArgumentException("Unsupported severity type: " + m.Severity.ToString(), nameof(m));
            }
            //_logger.Trace("After Add Counts. Node: {Name}, Err: {CountErrors}, Wrn: {CountWarnings}, Inf: {CountInfos}", p.Name, p.CountErrors, p.CountWarnings, p.CountInfos);
        }

        private void AddMessage(ITreeConfigNode p, ValidationMessage t)
        {
            //_logger?.Debug("Adding Message. Node: {model}, Property: {property}, Severity: {severity}, Message: {message}", p.ModelPath, t.PropertyName, t.SeverityName, t.Message);
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
        //#if DEBUG
        //        private HashSet<string> hashValidatableNodeGuid = new HashSet<string>();
        //#endif
        partial void OnVisit(IValidatableWithSeverity p)
        {
            if (p is IGuid pg)
            {
                //#if DEBUG
                //                string guid = pg.Guid;
                //                if (hashValidatableNodeGuid.Contains(guid))
                //                    Debug.Assert(false);
                //                hashValidatableNodeGuid.Add(guid);
                //#endif
                this.CountCurrentValidatableNode++;
            }
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
                //this._logger?.Trace(string.Empty.PadRight(this._level, ' ') + p.GetType().Name + ": " + pp.Name);
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
                //this._logger?.Trace(string.Empty.PadRight(this._level, ' ') + pp.GetType().Name + ": " + pp.Name);
                this._level--;
            }
        }
    }
}
