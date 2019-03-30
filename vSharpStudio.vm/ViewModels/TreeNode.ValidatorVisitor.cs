using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.ViewModels
{
    public class TreeNodeValidatorVisitor : IVisitorConfig
    {
        public SortedObservableCollection<ValidationMessage> Result { get; private set; }
        CancellationToken IVisitorConfig.Token => _cancellationToken;
        private CancellationToken _cancellationToken;
        private ushort _level = 0;
        // private bool _isForConfig = false;
        private ITreeNode _startNode;
        public TreeNodeValidatorVisitor(CancellationToken cancellationToken, ITreeNode startNode)
        {
            this._cancellationToken = cancellationToken;
            this._startNode = startNode;
            this.Result = new SortedObservableCollection<ValidationMessage>();
        }
        private void UpdateCounts(ITreeNode p, ValidationMessage m)
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
        private void UpdateValidation(IValidatableWithSeverity m)
        {
            ITreeNode p = null;
            if (m is ITreeNode)
            {
                p = m as ITreeNode;
                p.CountErrors = 0;
                p.CountWarnings = 0;
                p.CountInfos = 0;
            }
            m.Validate();
            foreach (var t in m.ValidationCollection)
            {
                if (p != null)
                    UpdateCounts(p, t);
                t.RaiseSeverityLevel(_level);
                Result.Add(t);
            }
        }
        void IVisitorConfig.Visit(Config m)
        {
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(Property m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(DataType m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(Properties m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(Constant m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(Constants m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(Enumeration m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(Enumerations m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(Catalog m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(Catalogs m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(Document m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(Documents m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.Visit(EnumerationPair m)
        {
            _level++;
            UpdateValidation(m);
        }

        void IVisitorConfig.VisitEnd(Config m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(Property m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(DataType m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(Properties m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(Constant m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(Constants m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(EnumerationPair m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(Enumeration m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(Enumerations m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(Catalog m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(Catalogs m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(Document m)
        {
            _level--;
        }

        void IVisitorConfig.VisitEnd(Documents m)
        {
            _level--;
        }
    }
}
