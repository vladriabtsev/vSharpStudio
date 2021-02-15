namespace vSharpStudio.common
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using FluentValidation;
    //using JetBrains.Annotations;
    using Microsoft.Extensions.Logging;
    using ViewModelBase;
    using vSharpStudio.common;
    using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

    public partial class ConfigObjectCommonBase<T, TValidator> : VmValidatableWithSeverityAndAttributes<T, TValidator>, IComparable<T>, IEquatable<T>
        where TValidator : AbstractValidator<T>
        where T : ConfigObjectCommonBase<T, TValidator>, IComparable<T>//, ISortingValue //, IGuid // , ITreeConfigNode
    {
        protected static ILogger _logger;
        public ConfigObjectCommonBase(ITreeConfigNode parent, TValidator validator)
            : base(validator)
        {
            if (_logger == null)
                _logger = Logger.CreateLogger<T>();
            this.IsNotifying = false;
            this.Parent = parent;
            this.ListInModels = new List<IModelRow>();
            this.PropertyChanged += ConfigObjectCommonBase_PropertyChanged;
            this.IsNotifying = true;
        }
        private void ConfigObjectCommonBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsNew":
                case "IsMarkedForDeletion":
                    this.NotifyPropertyChanged(() => this.NodeNameDecorations);
                    break;
            }
        }
        protected virtual void OnInitFromDto()
        {
        }
        private static int _maxlen = 0;
        protected override void OnCountErrorsChanged()
        {
            this.NotifyPropertyChanged(nameof(this.IconStatus));
        }
        protected override void OnCountWarningsChanged()
        {
            this.NotifyPropertyChanged(nameof(this.IconStatus));
        }
        protected override void OnCountInfosChanged()
        {
            this.NotifyPropertyChanged(nameof(this.IconStatus));
        }
        [Browsable(false)]
        public string IconStatus
        {
            get
            {
                string iconName = null;
                if (this.CountErrors > 0)
                {
                    iconName = "iconStatusCriticalError";
                }
                else
                {
                    if (this.CountWarnings > 0)
                    {
                        iconName = "iconStatusWarning";
                    }
                    else
                    {
                        if (this.CountInfos > 0)
                        {
                            iconName = "iconStatusInformation";
                        }
                        else
                        {
                            iconName = null;
                        }
                    }
                }
                return iconName;
            }
        }
        [Browsable(false)]
        public string IconName
        {
            get
            {
                string iconName = null;
                if (this.IsExpanded)
                {
                    iconName = "iconFolderOpen";
                }
                else
                {
                    iconName = "iconFolder";
                }
                return iconName;
            }
        }
        public int CompareTo(T other)
        {
            return this._SortingValue.CompareTo(other._SortingValue);
        }
        public bool Equals(T other)
        {
            return this.__Guid == (other as IGuid).Guid;
        }

        #region Sort
        [BrowsableAttribute(false)]
        public ulong SortingWeight { get; set; }
        protected ulong _SortingValue
        {
            get
            {
                return this.__SortingValue;
            }
            set
            {
                if (this.__SortingValue != value)
                {
                    this.__SortingValue = value;
                    ITreeConfigNode p = (ITreeConfigNode)this;
                    if (p.Parent != null)
                    {
                        p.Parent.Sort(this.GetType());
                    }
                }
            }
        }
        private ulong __SortingValue;
        public virtual void Sort(Type type)
        {
            throw new NotImplementedException();
        }
        #endregion Sort

        protected string _Guid
        {
            get
            {
                if (this.__Guid == null)
                {
                    this.SetNewGuid();
                }
                return this.__Guid;
            }
            set
            {
                this.__Guid = value;
            }
        }
        private string __Guid = null;
        protected void SetNewGuid()
        {
            this.__Guid = System.Guid.NewGuid().ToString();
        }
#if DEBUG
        [ReadOnly(true)]
#else
        [Browsable(false)]
#endif
        public string ModelPath
        {
            get
            {
                if (_ModelPath == null)
                {
                    _ModelPath = (this.Parent != null ? this.Parent.ModelPath + "." : "") + this.GetType().Name;
                }
                return _ModelPath;
            }
        }
        private string _ModelPath = null;
#if DEBUG
#else
        [Browsable(false)]
#endif
        public string FullName
        {
            get
            {
                if (this.Parent == null)
                {
                    return "MainConfig." + this._Name;
                }
                return this.GetConfig().Name + "." + this._Name;
            }
        }
        [BrowsableAttribute(false)]
        public System.Windows.TextDecorationCollection NodeNameDecorations
        {
            get
            {
                if (this is IEditableNode)
                {
                    var p = this as IEditableNode;
                    if (p.IsNew || p.IsMarkedForDeletion)
                    {
                        var myCollection = new System.Windows.TextDecorationCollection();
                        if (p.IsMarkedForDeletion)
                        {
                            myCollection.Add(System.Windows.TextDecorations.Strikethrough);
                        }
                        if (p.IsNew)
                        {
                            myCollection.Add(System.Windows.TextDecorations.Underline);
                        }
                        return myCollection;
                    }
                }
                return null;
            }
        }
        public string GetRelativeToConfigDiskPath(string path)
        {
            var cfg = this.GetConfig();
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                throw new Exception("Config is not saved yet");
#if NET48
            return vSharpStudio.common.Utils.GetRelativePath(cfg.CurrentCfgFolderPath, path);
#else
            return Path.GetRelativePath(cfg.CurrentCfgFolderPath, path);
#endif
        }
        public string GetCombinedPath(string relative_path)
        {
            var cfg = this.GetConfig();
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                return relative_path;
            return Path.Combine(cfg.CurrentCfgFolderPath, relative_path);
        }
        public IConfig GetConfig()
        {
            ITreeConfigNode p = this.Parent;
            if (p == null)
            {
                return null;
            }
            while (p.Parent != null)
            {
                p = p.Parent;
            }
            return (IConfig)p;
        }
        public T GetPrevious()
        {
            T res = null;
            if (this.GetConfig()?.PrevStableConfig != null && this.GetConfig().PrevStableConfig.DicNodes.ContainsKey(this.Parent.Guid))
            {
                res = (T)this.GetConfig().PrevStableConfig.DicNodes[this.__Guid];
            }
            return res;
        }

        protected string _Name
        {
            get
            {
                return this.__Name;
            }
            set
            {
                if (this.__Name != value)
                {
                    this.__Name = value.Trim();
                    if (this.ValidateProperty("Name"))
                    {
                        this._SortingValue = this.EncodeNameToUlong(this.__Name) + this.SortingWeight;
                        ITreeConfigNode p = (ITreeConfigNode)this;
                        if (p.Parent != null)
                        {
                            p.Parent.Sort(this.GetType());
                        }
                    }
                }
            }
        }
        protected string GetCompositeName()
        {
            List<ITreeConfigNode> lst = new List<ITreeConfigNode>();
            ITreeConfigNode p = this.Parent;
            while (p != null)
            {
                lst.Insert(0, p);
                p = p.Parent;
            }
            var sb = new StringBuilder();
            sb.Append("");
            string prefix = "";
            foreach (var t in lst)
            {
                if (t is IPropertiesTab)
                    sb.Append(t.Name);
                else if (t is ICatalog)
                {
                    sb.Append(t.Name);
                }
                else if (t is IGroupListCatalogs)
                {
                    prefix = (t as IGroupListCatalogs).PrefixForDbTables;
                }
                else if (t is IDocument)
                {
                    sb.Append(t.Name);
                }
                else if (t is IGroupDocuments)
                {
                    prefix = (t as IGroupDocuments).PrefixForDbTables;
                }
            }
            string composit = sb.ToString();
            sb = new StringBuilder();
            var cfg = this.GetConfig();
            if (cfg.Model.IsUseGroupPrefix)
                sb.Append(prefix);
            if (cfg.Model.IsUseCompositeNames)
                sb.Append(composit);
            sb.Append(this._Name);
            return sb.ToString();
        }


        public bool CheckIsCompositeNameUnique()
        {
            var cfg = this.GetConfig();

            //foreach (var t in cfg.gr.ListCatalogs)
            //{
            //    if ((val.Guid != t.Guid) && (val.Name == t.Name))
            //    {
            //        return false;
            //    }
            //}
            return true;
        }
        protected void SetSelected(ITreeConfigNode node)
        {
            if (this.Parent != null)
            {
                this.GetConfig().SelectedNode = node;
            }
        }
        private string __Name = string.Empty;
        protected string _NameUi
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.__NameUi) && !string.IsNullOrEmpty(this.__Name))
                {
                    return this.__Name;
                }
                return this.__NameUi;
            }
            set
            {
                if (this.__NameUi != value)
                {
                    this.__NameUi = value.Trim();
                }
            }
        }
        private string __NameUi = string.Empty;
        protected ulong EncodeNameToUlong(string name)
        {
            const int step = 1 + '9' - '0' + 1 + 'Z' - 'A' + 1; // first is '_'
            if (_maxlen == 0)
            {
                _maxlen = (int)Math.Log(VmBindable.SortingWeightBase, step);
                ulong val = 1;
                for (int i = 0; i < _maxlen; i++)
                {
                    val *= step;
                }
            }
            int len = Math.Min(_maxlen, name.Length);
            ulong res = 0;
            for (int i = 0; i < len; i++)
            {
                var c = char.ToUpper(name[i]);
                int ci = 0;
                if (char.IsDigit(c))
                {
                    ci = c - '0' + 1;
                }
                else if (c == '_')
                {
                    ci = 0;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    ci = c - 'A' + 11;
                }
                // else
                //    throw new ArgumentException("Unexpected char value: '" + c + "'");
                ulong pow = 1;
                for (int j = 0; j < _maxlen - i - 1; j++)
                {
                    pow *= step;
                }
                res += (ulong)ci * pow;
            }
            return res;
        }
        protected void GetUniqueName(string defName, ITreeConfigNode configObject, IEnumerable<ITreeConfigNode> lst)
        {
            if (!string.IsNullOrWhiteSpace(configObject.Name))
            {
                return;
            }
            int i = 0;
            foreach (var tt in lst)
            {
                if (tt == configObject)
                {
                    continue;
                }
                if (tt.Name.StartsWith(defName))
                {
                    string s = tt.Name.Remove(0, defName.Length);
                    int ii;
                    if (int.TryParse(s, out ii))
                    {
                        if (ii > i)
                        {
                            i = ii;
                        }
                    }
                }
            }
            i++;
            configObject.Name = defName + i;
        }

        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent
        {
            get
            {
                return this._Parent;
            }
            set
            {
                this._Parent = value;
                this.OnParentChanged();
                if (this._Parent != null)
                {
                    OnNodeIsNewChanged();
                    OnNodeIsChangedChanged();
                    OnNodeIsMarkedForDeletionChanged();
                }
            }
        }
        private ITreeConfigNode _Parent;
        protected virtual void OnParentChanged()
        {
        }
        [BrowsableAttribute(false)]
        public bool IsSelected
        {
            get
            {
                return this._IsSelected;
            }
            set
            {
                if (this._IsSelected != value)
                {
                    this._IsSelected = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsSelected;
        [BrowsableAttribute(false)]
        public bool IsExpanded
        {
            get
            {
                return this._IsExpanded;
            }
            set
            {
                if (this._IsExpanded != value)
                {
                    this._IsExpanded = value;
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged(nameof(this.IconName));
                }
            }
        }
        private bool _IsExpanded;

        #region Commands
        public bool NodeCanAddNew()
        {
            if (this is ICanAddNode)
            {
                return true;
            }
            return false;
        }
        public virtual ITreeConfigNode NodeAddNew()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanAddNewSubNode()
        {
            if (this is ICanAddSubNode)
            {
                return (this as ICanAddSubNode).CanAddSubNode();
            }
            return false;
        }
        public virtual ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node = null)
        {
            throw new NotImplementedException();
        }
        public bool NodeCanAddClone()
        {
            if (this is ICanAddNode)
            {
                return true;
            }
            return false;
        }
        public virtual ITreeConfigNode NodeAddClone()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanMoveDown()
        {
            if (!(this is ICanAddNode))
            {
                return false;
            }
            return this.NodeCanDown();
        }
        public virtual void NodeMoveDown()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanMoveUp()
        {
            if (!(this is ICanAddNode))
            {
                return false;
            }
            return this.NodeCanUp();
        }
        public virtual void NodeMoveUp()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanMarkForDeletion()
        {
            if (this is IEditableNode)
                return true;
            return false;
        }
        public void NodeMarkForDeletion()
        {
            if (this is IEditableNode)
            {
                var p = (IEditableNode)this;
                p.IsMarkedForDeletion = !p.IsMarkedForDeletion;
            }
        }
        public bool NodeCanLeft()
        {
            if (this is ICanGoLeft)
            {
                return true;
            }
            return false;
        }
        public void NodeLeft()
        {
            this.SetSelected(this.Parent);
        }
        public bool NodeCanRight()
        {
            if (this is ICanGoRight)
            {
                return true;
            }
            return false;
        }
        private bool IsIListNodesGen(object obj)
        {
            bool res = false;
            foreach (var t in obj.GetType().GetInterfaces())
            {
                if (t.Name.StartsWith("IListNodes`"))
                {
                    return true;
                }
            }
            return res;
        }
        public virtual void NodeRight()
        {
            throw new NotImplementedException();
        }
        public virtual bool NodeCanDown()
        {
            return false;
        }
        public virtual void NodeDown()
        {
            throw new NotImplementedException();
        }
        public virtual bool NodeCanUp()
        {
            return false;
        }
        public virtual void NodeUp()
        {
            throw new NotImplementedException();
        }
        #endregion Commands

        [BrowsableAttribute(false)]
        public bool IsIncludableInModels { get; protected set; }
        public List<IModelRow> ListInModels { get; protected set; }
        public ITreeConfigNode PrevCurrentVersion()
        {
            var cfg = this.GetConfig();
            if (cfg.PrevCurrentConfig == null || !cfg.PrevCurrentConfig.DicNodes.ContainsKey(this.__Guid))
                return null;
            return cfg.PrevCurrentConfig.DicNodes[this.__Guid];
        }
        public ITreeConfigNode PrevStableVersion()
        {
            var cfg = this.GetConfig();
            if (cfg.PrevStableConfig == null || !cfg.PrevStableConfig.DicNodes.ContainsKey(this.__Guid))
                return null;
            return cfg.PrevStableConfig.DicNodes[this.__Guid];
        }
        public bool IsRenamed(bool isStable)
        {
            if (this is IEditableNode)
            {
                if (isStable)
                {
                    var p = this.PrevStableVersion();
                    if (p != null && p.Name != this._Name)
                        return true;
                }
                else
                {
                    var p = this.PrevCurrentVersion();
                    if (p != null && p.Name != this._Name)
                        return true;
                }
            }
            return false;
        }
        virtual public bool IsNewNode()
        {
            if (this is IEditableNode)
            {
                if ((this as IEditableNode).IsNew)
                    return true;
            }
            return false;
        }
        public bool IsDeleted()
        {
            if (this is IEditableNode)
            {
                var p = (IEditableNode)this.PrevStableVersion();
                if (p != null && p.IsMarkedForDeletion && (this as IEditableNode).IsMarkedForDeletion)
                    return true;
            }
            return false;
        }
        public bool IsDeprecated()
        {
            if (this is IEditableNode)
            {
                var p = (IEditableNode)this.PrevStableVersion();
                if (p != null && !p.IsMarkedForDeletion && (this as IEditableNode).IsMarkedForDeletion)
                    return true;
            }
            return false;
        }
        protected void OnRemoveChild()
        {
            if (!VmBindable.IsNotifyingStatic)
                return;
            if (!this.IsNotifying)
                return;
            bool isHasNew = false, isHasMarked = false, isHasChanged = false;
            var pp = (IEditableNodeGroup)this;
            foreach (var t in this.GetListChildren())
            {
                var p = (IEditableNode)t;
                if (p.IsChanged)
                    isHasChanged = true;
                if (p.IsMarkedForDeletion)
                    isHasMarked = true;
                if (p.IsNew)
                    isHasNew = true;
            }
            pp.IsHasChanged = isHasChanged;
            pp.IsHasMarkedForDeletion = isHasMarked;
            pp.IsHasNew = isHasNew;
        }
        protected void OnNodeIsNewChanged()
        {
            if (!VmBindable.IsNotifyingStatic)
                return;
            if (!this.IsNotifying)
                return;
            if (this is IConfig)
                return;
            if (this is IEditableNode)
            {
                var pp = (IEditableNodeGroup)this.Parent;
                var p = (IEditableNode)this;
                if (p.IsNew)
                {
                    pp.IsHasNew = true;
                }
                else
                {
                    foreach (var t in this.GetListSiblings())
                    {
                        var pt = (IEditableNode)t;
                        if (pt.IsNew)
                        {
                            pp.IsHasNew = true;
                            return;
                        }
                    }
                    pp.IsHasNew = false;
                }
            }
        }
        protected void OnNodeIsChangedChanged()
        {
            if (!VmBindable.IsNotifyingStatic)
                return;
            if (!this.IsNotifying)
                return;
            if (this is IConfig)
                return;
            if (this is IEditableNode)
            {
                if (this.Parent is IEditableNodeGroup)
                {
                    var pp = (IEditableNodeGroup)this.Parent;
                    var p = (IEditableNode)this;
                    if (p.IsChanged)
                    {
                        pp.IsHasChanged = true;
                    }
                    else
                    {
                        foreach (var t in this.GetListSiblings())
                        {
                            var pt = (IEditableNode)t;
                            if (pt.IsChanged)
                            {
                                pp.IsHasChanged = true;
                                return;
                            }
                        }
                        pp.IsHasChanged = false;
                    }
                }
            }
            else if (this is IEditableNodeGroup)
            {
                if (this.Parent is IEditableNodeGroup)
                {
                    var pp = (IEditableNodeGroup)this.Parent;
                    if (this.IsChanged)
                    {
                        pp.IsHasChanged = true;
                    }
                    else
                    {
                        foreach (var t in this.GetListSiblings())
                        {
                            if (t is IEditableNode)
                            {
                                var pt = (IEditableNode)t;
                                if (pt.IsChanged)
                                {
                                    pp.IsHasChanged = true;
                                    return;
                                }
                            }
                            else if (t is IEditableNodeGroup)
                            {
                                var pt = (IEditableNodeGroup)t;
                                if (pt.IsHasChanged)
                                {
                                    pp.IsHasChanged = true;
                                    return;
                                }
                            }
                        }
                        pp.IsHasChanged = false;
                    }
                }
            }
        }
        protected void OnNodeIsMarkedForDeletionChanged()
        {
            if (!VmBindable.IsNotifyingStatic)
                return;
            if (!this.IsNotifying)
                return;
            if (this is IConfig)
                return;
            if (this is IEditableNode)
            {
                var pp = (IEditableNodeGroup)this.Parent;
                var p = (IEditableNode)this;
                if (p.IsMarkedForDeletion)
                {
                    pp.IsHasMarkedForDeletion = true;
                }
                else
                {
                    foreach (var t in this.GetListSiblings())
                    {
                        var pt = (IEditableNode)t;
                        if (pt.IsMarkedForDeletion)
                        {
                            pp.IsHasMarkedForDeletion = true;
                            return;
                        }
                    }
                    pp.IsHasMarkedForDeletion = false;
                }
            }
        }

        [BrowsableAttribute(false)]
        public bool IsHasNew
        {
            get { return this._IsHasNew; }
            set
            {
                if (this._IsHasNew != value)
                {
                    this.OnIsHasNewChanging(ref value);
                    this._IsHasNew = value;
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to);
        private void OnIsHasNewChanged()
        {
            if (!VmBindable.IsNotifyingStatic)
                return;
            if (!this.IsNotifying)
                return;
            if (this is IConfig)
                return;
            if (this is IEditableNodeGroup)
            {
                var p = (IEditableNodeGroup)this;
                var pp = (IEditableNodeGroup)this.Parent;
                if (p.IsHasNew)
                {
                    pp.IsHasNew = true;
                }
                else
                {
                    foreach (var t in this.GetListSiblings())
                    {
                        var pt = (IEditableNodeGroup)t;
                        if (pt.IsHasNew)
                        {
                            pp.IsHasNew = true;
                            return;
                        }
                    }
                    pp.IsHasNew = false;
                }
            }
        }
        [BrowsableAttribute(false)]
        public bool IsHasChanged
        {
            get { return this._IsHasChanged; }
            set
            {
                if (this._IsHasChanged != value)
                {
                    //this.OnIsHasChangedChanging(ref value);
                    this._IsHasChanged = value;
                    this.OnIsHasChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsHasChanged;
        //partial void OnIsHasChangedChanging(ref bool to);
        private void OnIsHasChangedChanged()
        {
            if (!VmBindable.IsNotifyingStatic)
                return;
            if (!this.IsNotifying)
                return;
            if (this is IConfig)
            {
                var cfgn = this as IConfig;
                if ((cfgn as IEditableNodeGroup).IsHasChanged && !cfgn.IsNeedCurrentUpdate)
                    cfgn.SetIsNeedCurrentUpdate(true);
                return;
            }
            if (this is IEditableNodeGroup)
            {
                var p = (IEditableNodeGroup)this;
                var pp = (IEditableNodeGroup)this.Parent;
                if (p.IsHasChanged)
                {
                    pp.IsHasChanged = true;
                }
                else
                {
                    foreach (var t in this.GetListSiblings())
                    {
                        var pt = (IEditableNodeGroup)t;
                        if (pt.IsHasChanged)
                        {
                            pp.IsHasChanged = true;
                            return;
                        }
                    }
                    pp.IsHasChanged = false;
                }
            }
        }
        [BrowsableAttribute(false)]
        public bool IsHasMarkedForDeletion
        {
            get { return this._IsHasMarkedForDeletion; }
            set
            {
                if (this._IsHasMarkedForDeletion != value)
                {
                    this.OnIsHasMarkedForDeletionChanging(ref value);
                    this._IsHasMarkedForDeletion = value;
                    this.OnIsHasMarkedForDeletionChanged();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to);
        private void OnIsHasMarkedForDeletionChanged()
        {
            if (!VmBindable.IsNotifyingStatic)
                return;
            if (!this.IsNotifying)
                return;
            if (this is IConfig)
                return;
            if (this is IEditableNodeGroup)
            {
                var p = (IEditableNodeGroup)this;
                var pp = (IEditableNodeGroup)this.Parent;
                if (p.IsHasMarkedForDeletion)
                {
                    pp.IsHasMarkedForDeletion = true;
                }
                else
                {
                    foreach (var t in this.GetListSiblings())
                    {
                        var pt = (IEditableNodeGroup)t;
                        if (pt.IsHasMarkedForDeletion)
                        {
                            pp.IsHasMarkedForDeletion = true;
                            return;
                        }
                    }
                    pp.IsHasMarkedForDeletion = false;
                }
            }
        }

        #region ITree
        public virtual IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            throw new NotImplementedException();
        }
        public virtual IEnumerable<ITreeConfigNode> GetListChildren()
        {
            throw new NotImplementedException();
        }
        public virtual bool HasChildren()
        {
            throw new NotImplementedException();
        }
        #endregion ITree
        #region ITreeModel
        public IEnumerable<object> GetChildren(object parent)
        {
            var p = (ITreeConfigNode)parent;
            return p.GetListChildren();
        }
        public bool HasChildren(object parent)
        {
            var p = (ITreeConfigNode)parent;
            return p.HasChildren(); ;
        }
        #endregion ITreeModel
    }
}
