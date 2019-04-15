using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Catalog:{Name,nq} props:{listProperties.Count,nq}")]
    public partial class Catalog : ConfigObjectBase<Catalog, Catalog.CatalogValidator>
    {
        partial void OnInit()
        {
        }
        public void OnInitFromDto()
        {
//            OnPropertyGroupChanged();
        }
        public Catalog(string name) : this()
        {
            (this as ITreeConfigNode).Name = name;
        }
        public Catalog(string name, List<Property> listProperties) : this()
        {
            (this as ITreeConfigNode).Name = name;
            foreach (var t in listProperties)
            {
                this.ListProperties.Add(t);
            }
        }

        #region ITreeConfigNode

        protected override bool OnNodeCanMoveUp()
        {
            return (this.Parent as Catalogs).ListCatalogs.IndexOf(this) > 0;
        }
        protected override void OnNodeMoveUp()
        {
            var p = this.Parent as Catalogs;
            var i = p.ListCatalogs.IndexOf(this);
            if (i > 0)
            {
                this.SortingValue = p.ListCatalogs[i - 1].SortingValue - 1;
            }
        }
        protected override bool OnNodeCanMoveDown()
        {
            return (this.Parent as Catalogs).ListCatalogs.IndexOf(this) < ((this.Parent as Catalogs).ListCatalogs.Count - 1);
        }
        protected override void OnNodeMoveDown()
        {
            var p = this.Parent as Catalogs;
            var i = p.ListCatalogs.IndexOf(this);
            if (i < p.ListCatalogs.Count - 1)
            {
                this.SortingValue = p.ListCatalogs[i + 1].SortingValue + 1;
            }
        }
        protected override void OnNodeRemove()
        {
            (this.Parent as Catalogs).ListCatalogs.Remove(this);
        }
        protected override ITreeConfigNode OnNodeAddNew()
        {
            var res = new Catalog();
            (this.Parent as Catalogs).ListCatalogs.Add(res);
            GetUniqueName("Catalog", res, (this.Parent as Catalogs).ListCatalogs);
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }
        protected override ITreeConfigNode OnNodeAddClone()
        {
            var res = Catalog.Clone(this.Parent, this, true, true);
            (this.Parent as Catalogs).ListCatalogs.Add(res);
            this.Name = this.Name + "2";
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }

        #endregion ITreeConfigNode

        #region ITreeNode

        #region status icon
        [BrowsableAttribute(false)]
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
        //void RecreateSubNodes()
        //{
        //    SubNodes.Clear();
        //    SubNodes.Add(this.PropertyGroup);
        //}
        //partial void OnPropertyGroupChanged() { RecreateSubNodes(); }

        public override void OnIsExpandedChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
        #endregion ITreeNode
    }
}
