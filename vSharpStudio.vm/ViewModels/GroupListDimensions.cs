using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CommunityToolkit.Diagnostics;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListDimensions : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{this.ListDimensions.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Register ParentRegister { get { Debug.Assert(this.Parent != null); return (Register)this.Parent; } }
        [Browsable(false)]
        public IRegister ParentRegisterI { get { Debug.Assert(this.Parent != null); return (IRegister)this.Parent; } }

        [PropertyOrder(100)]
        [ReadOnly(true)]
        [DisplayName("Composite")]
        [Description("Composite name based on IsCompositeNames and IsUseGroupPrefix model parameters")]
        public string CompositeName
        {
            get
            {
                return GetCompositeName();
            }
        }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentRegister.Children;
        }
        #endregion ITree

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public uint GetNextPosition()
        {
            return this.ParentRegister.GetNextPosition();
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            RegisterDimension node = null!;
            if (node_impl == null)
            {
                node = new RegisterDimension(this);
                node.Position = this.GetNextPosition();
            }
            else
            {
                node = (RegisterDimension)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.RegisterDimensionName, node, this.ListDimensions);
            }
            //var model = this.ParentRegister;
            //node.ShortId = model.LastRegisterShortId + 1;
            //model.LastRegisterShortId = node.ShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<RegisterDimension> Children { get { return this.ListDimensions; } }

        partial void OnCreated()
        {
            this._Name = Defaults.RegisterDimensionsGroupName;
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }

        private void Init()
        {
            //if (this.Parent is Catalog)
            //{
            //    this.NameUi = "Sub Catalogs";
            //}
            this.ListDimensions.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListDimensions.OnAddedAction = (t) =>
            {
                t.OnAdded();
                //t.InitRoles();
            };
            this.ListDimensions.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListDimensions.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public int IndexOf(IRegisterDimension dim)
        {
            return this.ListDimensions.IndexOf((dim as RegisterDimension)!);
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                this.GetPropertyName(() => this.Description),
                this.GetPropertyName(() => this.Guid),
                this.GetPropertyName(() => this.NameUi),
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
            };
            //if (!this.UseCodeProperty)
            //    lst.Add(this.GetPropertyName(() => this.PropertyCodeName));
            //if (!this.UseNameProperty)
            //    lst.Add(this.GetPropertyName(() => this.PropertyNameName));
            return lst.ToArray();
        }
//        public Register AddRegister()
//        {
//            var node = new Register(this);
//            this.NodeAddNewSubNode(node);
//            return node;
//        }
//        public Register AddRegister(string name, string? guid = null)
//        {
//            var node = new Register(this) { Name = name };
//#if DEBUG
//            if (guid != null) // for test model generation
//            {
//                if (this.Cfg.DicNodes.ContainsKey(guid))
//                    return node;
//                node.Guid = guid;
//            }
//#endif
//            this.NodeAddNewSubNode(node);
//            return node;
//        }

        #region Roles
        //public EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role)
        //{
        //    return role.DefaultCatalogEditAccessSettings;
        //}
        //public EnumPrintAccess GetRoleCatalogPrint(IRole role)
        //{
        //    return role.DefaultCatalogPrintAccessSettings;
        //}
        #endregion Roles

        //public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial)
        //{
        //    Debug.Assert(!isExcludeSpecial, "not implemented yet");

        //    var lst = new List<IProperty>();
        //    var m = this.ParentModel;

        //    // Field PK
        //    var pId = m.GetPropertyPkId(this, this.Guid);
        //    pId.TagInList = "id";
        //    lst.Add(pId);

        //    // Field record version
        //    if (isOptimistic)
        //    {
        //        var pVer = m.GetPropertyVersion(this, this.PropertyVersionGuid);
        //        pVer.TagInList = "vr";
        //        lst.Add(pVer);
        //    }

        //    // Field register Guid
        //    var pDocGuid = (Property)m.GetPropertyGuid(this, this.PropertyRegGuidGuid, "RegGuid", false);
        //    pDocGuid.Position = 9;
        //    pDocGuid.TagInList = "rg";
        //    lst.Add(pDocGuid);

        //    // Field timeline value
        //    var pDocDatePost = (Property)m.GetPropertyDate(this, this.PropertyDocDateGuid, "DocDatePost", false);
        //    pDocDatePost.Position = 10;
        //    pDocDatePost.TagInList = "pd";
        //    lst.Add(pDocDatePost);

        //    // Field timeline end
        //    var pDocDatePostSequenceEnd = (Property)m.GetPropertyDate(this, this.PropertyDocDateSequenceGuid, "DocDatePostSequenceEnd", false);
        //    pDocDatePostSequenceEnd.Position = 11;
        //    pDocDatePostSequenceEnd.TagInList = "se";
        //    lst.Add(pDocDatePostSequenceEnd);
        //    return lst;
        //}
    }
}
