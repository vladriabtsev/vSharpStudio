using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Catalog : EntityObjectBaseWithGuid<Catalog, Catalog.CatalogValidator>, IEntityObject, ITreeNode
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
        #region ITreeNode
        public ITreeNode Parent => null;

        public IEnumerable<ITreeNode> SubNodes
        {
            get { return this._SubNodes; }
            set
            {
                this._SubNodes = value;
                NotifyPropertyChanged();
            }
        }
        private IEnumerable<ITreeNode> _SubNodes;
        void RecreateSubNodes() { SubNodes = new ITreeNode[] { this.Properties }; }
        partial void OnPropertiesChanged() { RecreateSubNodes(); }

        #region ITreeNodeWithValidation
        public int ValidationQty
        {
            set
            {
                if (_ValidationQty != value)
                {
                    _ValidationQty = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _ValidationQty; }
        }
        private int _ValidationQty;

        public Severity ValidationSeverity
        {
            set
            {
                if (_ValidationSeverity != value)
                {
                    _ValidationSeverity = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _ValidationSeverity; }
        }

        private Severity _ValidationSeverity;
        #endregion ITreeNodeWithValidation
        #endregion ITreeNode
    }
}
