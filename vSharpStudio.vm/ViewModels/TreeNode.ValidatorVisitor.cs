﻿using System;
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
        private bool _isCollectMessages = false;
        private ITreeNode _startNode;
        private ILogger _logger = null;
        public TreeNodeValidatorVisitor(CancellationToken cancellationToken, ITreeNode startNode, ILogger logger = null)
        {
            this._cancellationToken = cancellationToken;
            this._startNode = startNode;
            this._logger = logger;
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
        private void UpdateValidation(ITreeNode p)
        {
            p.CountErrors = 0;
            p.CountWarnings = 0;
            p.CountInfos = 0;
            p.Validate();
            foreach (var t in p.ValidationCollection)
            {
                UpdateCounts(p, t);
                if (_isCollectMessages)
                {
                    t.RaiseSeverityLevel(_level);
                    Result.Add(t);
                }
            }
        }
        private void OnVisit(ITreeNode p)
        {
            _level++;
            if (_logger != null)
            {
                _logger.LogInformation("".PadRight(_level) + p.GetType().Name + ": " + p.NodeText);
            }
            if (_startNode == p)
                _isCollectMessages = true;
            UpdateValidation(p);
        }
        private void OnVisitEnd(ITreeNode p)
        {
            if (_startNode == p)
                _isCollectMessages = false;
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

        void IVisitorConfig.Visit(DataType p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.Visit(Properties p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.Visit(Constant m)
        {
            OnVisit(m);
        }

        void IVisitorConfig.Visit(Constants p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.Visit(Enumeration p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.Visit(Enumerations p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.Visit(Catalog p)
        {
            OnVisit(p);
        }

        void IVisitorConfig.Visit(Catalogs p)
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

        void IVisitorConfig.VisitEnd(DataType m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.VisitEnd(Properties m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.VisitEnd(Constant m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.VisitEnd(Constants m)
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

        void IVisitorConfig.VisitEnd(Enumerations m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.VisitEnd(Catalog m)
        {
            OnVisitEnd(m);
        }

        void IVisitorConfig.VisitEnd(Catalogs m)
        {
            OnVisitEnd(m);
        }

    }
}