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
    public partial class TreeNodeValidatorVisitor : IVisitorConfig
    {
        public SortedObservableCollection<ValidationMessage> Result { get; private set; }
        CancellationToken IVisitorConfig.Token => _cancellationToken;
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
        void IVisitorConfig.Visit(Config p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.Visit(Property p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.Visit(Constant m)
        {
            OnVisit(m);
        }

        void IVisitorConfig.Visit(Enumeration p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.Visit(Catalog p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.Visit(EnumerationPair m)
        {
            OnVisit(m);
        }

        void IVisitorConfig.VisitEnd(Config m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.VisitEnd(Property m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.VisitEnd(Constant m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.VisitEnd(EnumerationPair m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.VisitEnd(Enumeration m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.VisitEnd(Catalog m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.Visit(Document p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(Document p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(Journal p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(Journal p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupPropertiesTree p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupPropertiesTree p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupDocuments p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupDocuments p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(ConfigTree p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(ConfigTree p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupConfigs p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupConfigs p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupListDocuments p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupListDocuments p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(Form p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(Form p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Report p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(Report p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupListCatalogs p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupListCatalogs p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupPropertyTab p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupPropertyTab p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupPropertyTabs p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupPropertyTabs p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupPropertyTabsTree p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupPropertyTabsTree p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupListProperties p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupListProperties p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupListConstants p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupListConstants p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupListEnumerations p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupListEnumerations p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupListJournals p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupListJournals p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupListForms p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupListForms p)
        {
            OnVisitEnd(p);
        }

        void IVisitorConfig.Visit(GroupListReports p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.VisitEnd(GroupListReports p)
        {
            OnVisitEnd(p);
        }

        //void IVisitorConfig.Visit(IdDbGenerator p)
        //{
        //    OnVisit(p);
        //}

        //void IVisitorConfig.VisitEnd(IdDbGenerator p)
        //{
        //    OnVisitEnd(p);
        //}

        //void IVisitorConfig.Visit(DataType p)
        //{
        //    OnVisit(p);
        //}

        //void IVisitorConfig.VisitEnd(DataType p)
        //{
        //    OnVisitEnd(p);
        //}
    }
}
