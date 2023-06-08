using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class CatalogCodePropertySettings : IParent
    {
        public override string ToString()
        {
            string unique = "";
            switch (this.UniqueScope)
            {
                case EnumCatalogCodeUniqueScope.code_uniqueness_by_folder_settings:
                    unique = "Unique Folder";
                    break;
                case EnumCatalogCodeUniqueScope.code_unique_in_whole_catalog:
                    unique = "Unique";
                    break;
                case EnumCatalogCodeUniqueScope.code_not_unique_settings:
                    unique = "Not Unique";
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                switch (this.SequenceType)
                {
                    case EnumCodeType.Number:
                        return $"Number{this.Prefix}{this.MaxSequenceLength}-{unique}";
                    case EnumCodeType.AutoNumber:
                        return $"AutoNumber{this.Prefix}{this.MaxSequenceLength}-{unique}";
                    case EnumCodeType.Text:
                        return $"Text{this.Prefix}{this.MaxSequenceLength}-{unique}";
                    case EnumCodeType.AutoText:
                        return $"AutoText{this.Prefix}{this.MaxSequenceLength}-{unique}";
                    default:
                        throw new NotImplementedException();
                }
            }
            if (this.ParentCatalog != null)
                return $"{this.ParentCatalog.Cfg.DicNodes[this.SequenceGuid].Name}-{unique}";
            else if (this.ParentCatalogFolder != null)
                return $"{this.ParentCatalogFolder.Cfg.DicNodes[this.SequenceGuid].Name}-{unique}";
            throw new NotImplementedException();
        }
        [Browsable(false)]
        public Catalog? ParentCatalog
        {
            get
            {
                Debug.Assert(this.Parent != null);
                if (this.Parent is Catalog)
                    return (Catalog)this.Parent;
                else
                    return null;
            }
        }
        [Browsable(false)]
        public CatalogFolder? ParentCatalogFolder
        {
            get
            {
                Debug.Assert(this.Parent != null);
                if (this.Parent is CatalogFolder)
                    return (CatalogFolder?)this.Parent;
                else
                    return null;
            }
        }
        [Browsable(false)]
        public ICatalog ParentCatalogI { get { Debug.Assert(this.Parent != null); return (ICatalog)this.Parent; } }
        partial void OnCreated()
        {
            this.SequenceType = common.EnumCodeType.AutoText;
            this.MaxSequenceLength = 5;
            this.Prefix = "";
            this.UniqueScope = common.EnumCatalogCodeUniqueScope.code_unique_in_whole_catalog;
            //Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListRoles.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListRoles.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListRoles.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListRoles.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
        partial void OnMaxSequenceLengthChanged()
        {
            this.ParentCatalog?.NotifyCodePropertySettingsChanged();
            this.ParentCatalogFolder?.NotifyCodePropertySettingsChanged();
        }
        partial void OnPrefixChanged()
        {
            this.ParentCatalog?.NotifyCodePropertySettingsChanged();
            this.ParentCatalogFolder?.NotifyCodePropertySettingsChanged();
        }
        partial void OnSequenceTypeChanged()
        {
            this.ParentCatalog?.NotifyCodePropertySettingsChanged();
            this.ParentCatalogFolder?.NotifyCodePropertySettingsChanged();
        }
        partial void OnUniqueScopeChanged()
        {
            this.ParentCatalog?.NotifyCodePropertySettingsChanged();
            this.ParentCatalogFolder?.NotifyCodePropertySettingsChanged();
        }
        partial void OnSequenceGuidChanged()
        {
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
            this.ParentCatalog?.NotifyCodePropertySettingsChanged();
            this.ParentCatalogFolder?.NotifyCodePropertySettingsChanged();
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            if (!string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                lst.Add(this.GetPropertyName(() => this.SequenceType));
                lst.Add(this.GetPropertyName(() => this.MaxSequenceLength));
                lst.Add(this.GetPropertyName(() => this.Prefix));
            }
            return lst.ToArray();
        }
        public string GetCodeClrTypeName()
        {
            IConfig? cfg = null;
            string? propertyCodeGuid = null;
            GroupListProperties? groupListProperties = null;
            if (this.ParentCatalog!= null)
            {
                cfg = this.ParentCatalog.Cfg;
                propertyCodeGuid = this.ParentCatalog.PropertyCodeGuid;
                groupListProperties = this.ParentCatalog.GroupProperties;
            }
            else
            {
                cfg = this.ParentCatalogFolder?.Cfg;
                propertyCodeGuid = this.ParentCatalogFolder?.PropertyCodeGuid;
                groupListProperties = this.ParentCatalogFolder?.GroupProperties;
            }
            Debug.Assert(cfg != null);
            Debug.Assert(propertyCodeGuid != null);
            Debug.Assert(groupListProperties != null);
            IProperty? prp = null;
            switch (this.SequenceType)
            {
                case EnumCodeType.Number:
                    prp = cfg.Model.GetPropertyCatalogCodeInt(groupListProperties, propertyCodeGuid,
                        this.MaxSequenceLength);
                    break;
                case EnumCodeType.Text:
                    prp = cfg.Model.GetPropertyCatalogCode(groupListProperties, propertyCodeGuid,
                        this.MaxSequenceLength);
                    break;
                case EnumCodeType.AutoNumber:
                    prp = cfg.Model.GetPropertyCatalogCodeInt(groupListProperties, propertyCodeGuid,
                        this.MaxSequenceLength);
                    break;
                case EnumCodeType.AutoText:
                    prp = cfg.Model.GetPropertyCatalogCode(groupListProperties, propertyCodeGuid,
                        this.MaxSequenceLength + (uint)this.Prefix.Length);
                    break;
                default:
                    throw new NotImplementedException();
            }
            Debug.Assert(prp != null);
            return prp.DataType.ClrTypeName;
        }
    }
}
