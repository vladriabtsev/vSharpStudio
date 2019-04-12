using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Catalog : ConfigObjectBase<Catalog, Catalog.CatalogValidator>, ITreeConfigNode, IComparable<Catalog>
    {
        partial void OnInit()
        {
        }
        public void OnInitFromDto()
        {
            OnPropertiesChanged();
        }
        public Catalog(string name) : this()
        {
            this.Name = name;
        }
        public Catalog(string name, List<Property> listProperties) : this()
        {
            this.Name = name;
            foreach (var t in listProperties)
            {
                this.Properties.ListProperties.Add(t);
            }
        }
        public int CompareTo(Catalog other) { return this.SortingValue.CompareTo(other.SortingValue); }

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
        public ITreeConfigNode Parent { get; internal set; }
        public IEnumerable<ITreeConfigNode> SubNodes
        {
            get { return this._SubNodes; }
            set
            {
                this._SubNodes = value;
                NotifyPropertyChanged();
            }
        }
        private IEnumerable<ITreeConfigNode> _SubNodes;
        void RecreateSubNodes() { SubNodes = new ITreeConfigNode[] { this.Properties }; }
        partial void OnPropertiesChanged() { RecreateSubNodes(); }

        public override void OnIsExpandedChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        public string NodeText { get { return this.Name; } }
        #endregion ITreeNode
    }
}
