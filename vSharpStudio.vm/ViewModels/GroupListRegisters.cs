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
    public partial class GroupListRegisters : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListRegisters.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Model ParentModel { get { Debug.Assert(this.Parent != null); return (Model)this.Parent; } }
        [Browsable(false)]
        public IModel ParentModelI { get { Debug.Assert(this.Parent != null); return (IModel)this.Parent; } }

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
            return this.ParentModel.Children;
        }
        #endregion ITree

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Register node = null!;
            if (node_impl == null)
            {
                node = new Register(this);
            }
            else
            {
                node = (Register)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.RegisterName, node, this.ListRegisters);
            }
            var model = this.ParentModel;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<Register> Children { get { return this.ListRegisters; } }

        partial void OnCreated()
        {
            this._PrefixForDbTables = "Reg";
            this.IsEditable = false;
            this._ShortIdTypeForCacheKey = "rg";
            this._PropertyRegGuidGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocDateGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocDateSequenceGuid = System.Guid.NewGuid().ToString();
            this._PropertyVersionGuid = System.Guid.NewGuid().ToString();
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
            this.ListRegisters.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListRegisters.OnAddedAction = (t) =>
            {
                t.OnAdded();
                //t.InitRoles();
            };
            this.ListRegisters.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListRegisters.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            this._Name = Defaults.RegisterGroupName;
        }
        public int IndexOf(IRegister reg)
        {
            return this.ListRegisters.IndexOf((reg as Register)!);
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Description),
                nameof(this.Guid),
                nameof(this.NameUi),
                nameof(this.Parent),
                nameof(this.Children)
            };
            //if (!this.UseCodeProperty)
            //    lst.Add(nameof(this.PropertyCodeName));
            //if (!this.UseNameProperty)
            //    lst.Add(nameof(this.PropertyNameName));
            return lst.ToArray();
        }
        public Register AddRegister()
        {
            var node = new Register(this);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Register AddRegister(string name, string? guid = null)
        {
            var node = new Register(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Register AddRegister(string name, EnumRegisterType regType, string? guid = null)
        {
            var node = new Register(this) { Name = name, RegisterType = regType };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.NodeAddNewSubNode(node);
            return node;
        }

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
        public IReadOnlyList<IRegister> GetIncludedRegisters(string guidAppPrjGen)
        {
            var res = new List<IRegister>();
            foreach (var t in this.ListRegisters)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial)
        {
            Debug.Assert(!isExcludeSpecial, "not implemented yet");

            var lst = new List<IProperty>();
            var m = this.ParentModel;

            // Field PK
            var pId = m.GetPropertyPkId(this, this.Guid);
            pId.TagInList = "id";
            lst.Add(pId);

            // Field record version
            if (isOptimistic)
            {
                var pVer = m.GetPropertyVersion(this, this.PropertyVersionGuid);
                pVer.TagInList = "vr";
                lst.Add(pVer);
            }

            // Field register Guid
            var pDocGuid = (Property)m.GetPropertyGuid(this, this.PropertyRegGuidGuid, "RegGuid", false);
            pDocGuid.Position = 9;
            pDocGuid.TagInList = "rg";
            lst.Add(pDocGuid);

            // Field timeline value
            var pDocDatePost = (Property)m.GetPropertyDateTimeUtc(this, this.PropertyDocDateGuid, "DocDatePost", 10, false);
            pDocDatePost.TagInList = "pd";
            lst.Add(pDocDatePost);

            // Field timeline end
            var pDocDatePostSequenceEnd = (Property)m.GetPropertyDateTimeUtc(this, this.PropertyDocDateSequenceGuid, "DocDatePostSequenceEnd", 11, false);
            pDocDatePostSequenceEnd.TagInList = "se";
            lst.Add(pDocDatePostSequenceEnd);
            return lst;
        }
    }
}
