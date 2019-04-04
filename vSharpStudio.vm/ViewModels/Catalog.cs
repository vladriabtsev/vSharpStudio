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

        public ITreeNode Parent { get; internal set; }
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
        public bool IsExpended
        {
            get { return this._IsExpended; }
            set
            {
                this._IsExpended = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsExpended;

        #endregion ITreeNode
    }
}
