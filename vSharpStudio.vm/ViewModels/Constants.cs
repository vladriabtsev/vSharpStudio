﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Constants : EntityObjectBase<Constants, Constants.ConstantsValidator>, ITreeNode
    {
        partial void OnInit()
        {
            this.Name = "Constants";
        }
        public void OnInitFromDto()
        {
        }
        #region ITreeNode
        #region status icon
        public string StatusIcon
        {
            get
            {
                string iconName = null;
                if (this.IsExpanded)
                {
                    if (this.CountErrors > 0)
                        iconName = "iconFolderOpenError";
                    else
                    {
                        if (this.CountWarnings > 0)
                            iconName = "iconFolderOpenWarning";
                        else
                        {
                            if (this.CountInfos > 0)
                                iconName = "iconFolderOpenInformation";
                            else
                                iconName = "iconFolderOpen";
                        }
                    }
                }
                else
                {
                    if (this.CountErrors > 0)
                        iconName = "iconFolderError";
                    else
                    {
                        if (this.CountWarnings > 0)
                            iconName = "iconFolderWarning";
                        else
                        {
                            if (this.CountInfos > 0)
                                iconName = "iconFolderInformation";
                            else
                                iconName = "iconFolder";
                        }
                    }
                }
                return iconName;
            }
        }
        protected override void OnCountErrorsChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        protected override void OnCountWarningsChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        protected override void OnCountInfosChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        #endregion status icon
        public ITreeNode Parent { get; private set; }

        public IEnumerable<ITreeNode> SubNodes => this.ListConstants;
        public bool IsSelected
        {
            get { return this._IsSelected; }
            set
            {
                this._IsSelected = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsSelected;
        public bool IsExpanded
        {
            get { return this._IsExpanded; }
            set
            {
                this._IsExpanded = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(p => p.StatusIcon);
            }
        }
        private bool _IsExpanded;
        public string NodeText { get { return this.Name + " " + this.ListConstants.Count; } }

        #endregion ITreeNode
    }
}
