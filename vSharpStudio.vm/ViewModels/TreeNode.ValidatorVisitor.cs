using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.ViewModels
{
    public partial class TreeNodeValidatorVisitor : IVisitorConfigNode
    {
        public SortedObservableCollection<ValidationMessage> Result { get; private set; }
        CancellationToken IVisitorConfigNode.Token => _cancellationToken;
        private CancellationToken _cancellationToken;
        private int _level = -1;
        private ILogger _logger = null;
        public TreeNodeValidatorVisitor(CancellationToken cancellationToken, ILogger logger = null)
        {
            this._cancellationToken = cancellationToken;
            this._logger = logger;
            this.Result = new SortedObservableCollection<ValidationMessage>();
            this.Result.SortDirection = SortDirection.Descending;
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
        public void UpdateSubstructCounts(ITreeConfigNode p)
        {
            var pp = p;
            while (pp.Parent != null)
            {
                pp = pp.Parent;
                pp.CountErrors-= p.CountErrors;
                pp.CountWarnings -= p.CountWarnings;
                pp.CountInfos -= p.CountInfos;
            }
        }
        private void OnVisit(ITreeConfigNode p)
        {
            _level++;
            if (_logger != null)
            {
                _logger.LogInformation("".PadRight(_level) + p.GetType().Name + ": " + p.NodeText);
            }
            p.ValidationCollection.Clear();
            p.CountErrors = 0;
            p.CountWarnings = 0;
            p.CountInfos = 0;

            p.Validate();

            foreach (var t in p.ValidationCollection)
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
        }
        private void OnVisitEnd(ITreeConfigNode p)
        {
            if (_logger != null)
            {
                _logger.LogInformation("".PadRight(_level) + p.GetType().Name + ": " + p.NodeText);
            }
            _level--;
        }
        void IVisitorConfigNode.Visit(Config p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.Visit(Property p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.Visit(Constant m)
        {
            OnVisit(m);
        }

        void IVisitorConfigNode.Visit(Enumeration p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.Visit(Catalog p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.Visit(EnumerationPair m)
        {
            OnVisit(m);
        }

        void IVisitorConfigNode.VisitEnd(Config m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfigNode.VisitEnd(Property m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfigNode.VisitEnd(Constant m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfigNode.VisitEnd(EnumerationPair m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfigNode.VisitEnd(Enumeration m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfigNode.VisitEnd(Catalog m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfigNode.Visit(Document p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(Document p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(Journal p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(Journal p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupPropertiesTree p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupPropertiesTree p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupDocuments p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupDocuments p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(ConfigTree p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(ConfigTree p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupConfigs p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupConfigs p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupListDocuments p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupListDocuments p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(Form p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(Form p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Report p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(Report p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupListCatalogs p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupListCatalogs p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupPropertyTab p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupPropertyTab p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupPropertyTabs p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupPropertyTabs p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupPropertyTabsTree p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupPropertyTabsTree p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupListProperties p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupListProperties p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupListConstants p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupListConstants p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupListEnumerations p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupListEnumerations p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupListJournals p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupListJournals p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupListForms p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupListForms p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfigNode.Visit(GroupListReports p)
        {
            OnVisit(p);
        }

        void IVisitorConfigNode.VisitEnd(GroupListReports p)
        {
            OnVisitEnd(p);
        }

        //void IVisitorConfigNode.Visit(IdDbGenerator p)
        //{
        //    OnVisit(p);
        //}

        //void IVisitorConfigNode.VisitEnd(IdDbGenerator p)
        //{
        //    OnVisitEnd(p);
        //}

        //void IVisitorConfigNode.Visit(DataType p)
        //{
        //    OnVisit(p);
        //}

        //void IVisitorConfigNode.VisitEnd(DataType p)
        //{
        //    OnVisitEnd(p);
        //}
    }
}
