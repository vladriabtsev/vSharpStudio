using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Properties : EntityObjectBaseWithGuid<Properties, Properties.PropertiesValidator>, IEntityObject, ITreeNode
    {
        partial void OnInit()
        {
            this.Name = "Properties";
        }
        public void OnInitFromDto()
        {
        }

        #region ITreeNode
        public ITreeNode Parent { get; internal set; }
        public IEnumerable<ITreeNode> SubNodes => this.ListProperties;
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
            }
        }
        private bool _IsExpanded;
        public string NodeText { get { return this.Name + " " + this.ListProperties.Count; } }

        #endregion ITreeNode
    }
}
