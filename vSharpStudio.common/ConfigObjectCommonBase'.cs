namespace vSharpStudio.common
{
    using System;
    using System.CodeDom;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using CommunityToolkit.Diagnostics;
    using FluentValidation;
    using FluentValidation.Results;
    //using JetBrains.Annotations;
    using Microsoft.Extensions.Logging;
    using ViewModelBase;
    using vSharpStudio.common;
    using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

    public partial class ConfigObjectCommonBase<T, TValidator> : VmValidatableWithSeverityAndAttributes<T, TValidator>, IComparable<T>, IEquatable<T>
        where TValidator : AbstractValidator<T>
        where T : ConfigObjectCommonBase<T, TValidator>, IComparable<T>, IEquatable<T>//, ISortingValue //, IGuid // , ITreeConfigNode
    {
        protected static ILogger? _logger;
        public ConfigObjectCommonBase(ITreeConfigNode? parent, TValidator? validator)
            : base(validator)
        {
            if (_logger == null)
                _logger = Logger.CreateLogger<T>();
            this._Parent = parent;
            this.ListInModels = new List<IModelRow>();
            this.PropertyChanged += ConfigObjectCommonBase_PropertyChanged;
        }
        private void ConfigObjectCommonBase_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsChanged":
                case "IsHasChanged":
                    this.NotifyPropertyChanged(nameof(this.IsChangedOrHasChanged));
                    break;
                case "IsNew":
                    this.NotifyPropertyChanged(nameof(this.NodeNameDecorations));
                    this.NotifyPropertyChanged(nameof(this.IsNewOrHasNew));
                    break;
                case "IsMarkedForDeletion":
                    this.NotifyPropertyChanged(nameof(this.NodeNameDecorations));
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
        public ITreeConfigNode? FindSiblingWithValidationMessage(FluentValidation.Severity severity)
        {
            return this.FindChildWithValidationMessage(this.GetListSiblings(), severity);
        }
        public ITreeConfigNode? FindChildWithValidationMessage(FluentValidation.Severity severity)
        {
            return this.FindChildWithValidationMessage(this.GetListChildren(), severity);
        }
        private ITreeConfigNode? FindChildWithValidationMessage(IChildrenCollection lst, FluentValidation.Severity severity)
        {
            foreach (var t in lst)
            {
                var p = (IValidatableWithSeverity)t;
                foreach (var v in p.ValidationCollection)
                {
                    if (v.Severity == severity)
                        return t as ITreeConfigNode;
                }
            }
            foreach (var t in lst)
            {
                var tt = this.FindChildWithValidationMessage(((ITree)t).GetListChildren(), severity);
                if (tt != null)
                    return tt;
            }
            return null;
        }
        public ValidationMessage? FindValidationMessage(FluentValidation.Severity severity = FluentValidation.Severity.Error)
        {
            // check this.CountXXXXX before calling this function
            Debug.Assert((severity == Severity.Error && this.CountErrors > 0)
                || (severity == Severity.Warning && this.CountWarnings > 0)
                || (severity == Severity.Info && this.CountInfos > 0));
            foreach (var v in this.ValidationCollection)
            {
                if (v.Severity == severity)
                    return v;
            }
            var node = this.FindChildWithValidationMessage(severity);
            Debug.Assert(node != null);
            foreach (var v in node.ValidationCollection)
            {
                if (v.Severity == severity)
                    return v;
            }
            throw new Exception($"Validation message with severity '{Enum.GetName(typeof(Severity), severity)}' is not found");
        }
        [Browsable(false)]
        public IChildrenCollection Children
        {
            get
            {
                return _Children;
            }
            protected set { _Children = value; }
        }
        private IChildrenCollection _Children;
        [Browsable(false)]
        public string IconStatus
        {
            get
            {
                string iconName = String.Empty;
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
                            //iconName = null;
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
                string iconName = String.Empty;
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
        public int CompareTo(T? other)
        {
            Debug.Assert(other != null);
            return this._SortingValue.CompareTo(other._SortingValue);
        }
        public bool Equals(T? other)
        {
            Debug.Assert(other != null && other is IGuid);
            return this.__Guid == (other as IGuid)!.Guid;
        }

        #region Sort
        [Browsable(false)]
        public ulong SortingWeight { get; set; }
        [Browsable(false)]
        public ulong _SortingNameValue { get; private set; }
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
                        ITreeConfigNode.IsSorting = true;
                        p.Parent.Sort(this.GetType());
                        ITreeConfigNode.IsSorting = false;
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
                if (this.__Guid == String.Empty)
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
        private string __Guid = String.Empty;
        protected void SetNewGuid()
        {
            this.__Guid = System.Guid.NewGuid().ToString();
        }
#if DEBUG
        [ReadOnly(true)]
        [Category("")]
        [PropertyOrderAttribute(-1)]
        [DisplayName("Model Type")]
#else
        [Browsable(false)]
#endif
        public string ModelPath
        {
            get
            {
                if (_ModelPath == String.Empty)
                {
                    _ModelPath = (this.Parent != null ? this.Parent.ModelPath + "." : "") + this.GetType().Name;
                }
                return _ModelPath;
            }
        }
        private string _ModelPath = String.Empty;
#if DEBUG
        [ReadOnly(true)]
        [Category("")]
        [PropertyOrderAttribute(0)]
        [DisplayName("Config.Object Name")]
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
                return this.Cfg.Name + "." + this._Name;
            }
        }
        [Browsable(false)]
        public System.Windows.TextDecorationCollection? NodeNameDecorations
        {
            get
            {
                if (this is IEditableNode)
                {
                    var myCollection = new System.Windows.TextDecorationCollection();
                    if (this is IProperty pp)
                    {
                        if (pp.IsStartNewTabControl || pp.IsStopTabControl || !string.IsNullOrWhiteSpace(pp.TabName))
                        {
                            myCollection.Add(System.Windows.TextDecorations.OverLine);
                        }
                    }
                    if (this is IEditableNode p && p.IsMarkedForDeletion)
                    {
                        myCollection.Add(System.Windows.TextDecorations.Strikethrough);
                    }
                    //if (p.IsNew)
                    //{
                    //    myCollection.Add(System.Windows.TextDecorations.Underline);
                    //}
                    if (myCollection.Count > 0)
                        return myCollection;
                    //if (p.IsNew || p.IsMarkedForDeletion)
                    //{
                    //    var myCollection = new System.Windows.TextDecorationCollection();
                    //    if (p.IsMarkedForDeletion)
                    //    {
                    //        myCollection.Add(System.Windows.TextDecorations.Strikethrough);
                    //    }
                    //    if (p.IsNew)
                    //    {
                    //        myCollection.Add(System.Windows.TextDecorations.Underline);
                    //    }
                    //    return myCollection;
                    //}
                }
                return null;
            }
        }
        public string GetRelativeToConfigDiskPath(string path)
        {
            if (string.IsNullOrEmpty(this.Cfg.CurrentCfgFolderPath))
                throw new Exception("Config is not saved yet");
#if NET48
            return vSharpStudio.common.Utils.GetRelativePath(cfg.CurrentCfgFolderPath, path);
#else
            return Path.GetRelativePath(this.Cfg.CurrentCfgFolderPath, path);
#endif
        }
        public string GetCombinedPath(string relative_path)
        {
            if (string.IsNullOrEmpty(this.Cfg.CurrentCfgFolderPath))
                return relative_path;
            return Path.Combine(this.Cfg.CurrentCfgFolderPath, relative_path);
        }
        private IConfig? _cfg;
        [Browsable(false)]
        public IConfig Cfg
        {
            get
            {
                if (this._cfg == null)
                    this._cfg = this.GetConfig();
                return this._cfg;
            }
        }
        private IConfig GetConfig()
        {
            if (this is IConfig tt)
                return tt;
            Debug.Assert(this.Parent != null);
            ITreeConfigNode p = this.Parent;
            while (p.Parent != null)
            {
                p = p.Parent;
            }
            Debug.Assert(p is IConfig);
            return (IConfig)p;
        }
        public T? GetPrevious()
        {
            T? res = null;
            Debug.Assert(this.Parent != null);
            if (this.Cfg.PrevStableConfig != null && this.Cfg.PrevStableConfig.DicNodes.ContainsKey(this.Parent.Guid))
            {
                res = (T)this.Cfg.PrevStableConfig.DicNodes[this.__Guid];
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
                    if (value != null)
                    {
                        this.__Name = value.Trim();
                        if (this.ValidateProperty("Name"))
                        {
                            this._SortingNameValue = this.EncodeNameToUlong(this.__Name);
                            this._SortingValue = _SortingNameValue + this.SortingWeight;
                            ITreeConfigNode p = (ITreeConfigNode)this;
                            if (p.Parent != null)
                            {
                                ITreeConfigNode.IsSorting = true;
                                p.Parent.Sort(this.GetType());
                                ITreeConfigNode.IsSorting = false;
                            }
                        }
                    }
                }
            }
        }
        protected string GetCompositeName()
        {
            List<ITreeConfigNode> lst = new List<ITreeConfigNode>();
            ITreeConfigNode? p = this.Parent;
            while (p != null)
            {
                lst.Insert(0, p);
                p = p.Parent;
            }
            var sb = new StringBuilder();
            sb.Append("");
            string prefix = "";
            if (this is IGroupListRegisters gr1)
            {
                prefix = gr1.PrefixForDbTables;
            }
            else
            {
                foreach (var t in lst)
                {
                    if (t is IDetail)
                    {
                        sb.Append(t.Name);
                    }
                    else if (t is IGroupListConstants)
                    {
                        sb.Append(t.Name);
                    }
                    else if (t is IGroupConstantGroups gcg)
                    {
                        prefix = gcg.PrefixForDbTables;
                    }
                    else if (t is ICatalog)
                    {
                        sb.Append(t.Name);
                    }
                    else if (t is IGroupListCatalogs glc)
                    {
                        prefix = glc.PrefixForDbTables;
                    }
                    else if (t is IDocument)
                    {
                        sb.Append(t.Name);
                    }
                    else if (t is IGroupDocuments gd)
                    {
                        prefix = gd.PrefixForDbTables;
                    }
                    else if (t is IGroupListRegisters gr)
                    {
                        prefix = gr.PrefixForDbTables;
                    }
                }
            }
            string composit = sb.ToString();
            sb = new StringBuilder();
            if (this.Cfg.Model.IsUseGroupPrefix)
                sb.Append(prefix);
            if (this.Cfg.Model.IsUseCompositeNames)
                sb.Append(composit);
            sb.Append(this._Name);
            return sb.ToString();
        }


        //public bool CheckIsCompositeNameUnique()
        //{
        //    var cfg = this.GetConfig();

        //    //foreach (var t in cfg.gr.ListCatalogs)
        //    //{
        //    //    if ((val.Guid != t.Guid) && (val.Name == t.Name))
        //    //    {
        //    //        return false;
        //    //    }
        //    //}
        //    return true;
        //}
        protected void SetSelected(ITreeConfigNode node)
        {
            if (this.Parent != null)
            {
                this.Cfg.SelectedNode = node;
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

        [Browsable(false)]
        public ITreeConfigNode? Parent
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
        private ITreeConfigNode? _Parent;
        protected virtual void OnParentChanged()
        {
        }
        [Browsable(false)]
        public bool IsSelected
        {
            get
            {
                return this._IsSelected;
            }
            set
            {
                SetProperty(ref this._IsSelected, value);
            }
        }
        private bool _IsSelected;
        [Browsable(false)]
        public bool IsExpanded
        {
            get
            {
                return this._IsExpanded;
            }
            set
            {
                if (SetProperty(ref this._IsExpanded, value))
                {
                    this.NotifyPropertyChanged(nameof(this.IconName));
                }
            }
        }
        private bool _IsExpanded;

        #region Commands
        public bool NodeCanAddNew()
        {
            if (this is ICanAddNode && this is IEditableNode)
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
            if (this is ICanAddSubNode tt)
            {
                return tt.CanAddSubNode();
            }
            return false;
        }
        public virtual ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node = null)
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
            if (this is INodeDeletable d)
            {
                d.Delete();
            }
            else if (this is IEditableNode p)
            {
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
            Debug.Assert(this.Parent != null);
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

        [Browsable(false)]
        public bool IsIncludableInModels { get; protected set; }
        [Browsable(false)]
        // support submodules (wip)
        public List<IModelRow> ListInModels { get; protected set; }
        public ITreeConfigNode? PrevCurrentVersion()
        {
            if (this.Cfg.PrevCurrentConfig == null || !this.Cfg.PrevCurrentConfig.DicNodes.ContainsKey(this.__Guid))
                return null;
            return this.Cfg.PrevCurrentConfig.DicNodes[this.__Guid];
        }
        public ITreeConfigNode? PrevStableVersion()
        {
            if (this.Cfg.PrevStableConfig == null || !this.Cfg.PrevStableConfig.DicNodes.ContainsKey(this.__Guid))
                return null;
            return this.Cfg.PrevStableConfig.DicNodes[this.__Guid];
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
        public virtual bool IsNewNode()
        {
            if (this is ICanAddNode tt)
            {
                if (tt.IsNew)
                    return true;
            }
            return false;
        }
        public bool IsDeleted()
        {
            if (this is IEditableNode tt)
            {
                var p = (IEditableNode?)this.PrevStableVersion();
                if (p != null && p.IsMarkedForDeletion && tt.IsMarkedForDeletion)
                    return true;
            }
            return false;
        }
        public bool IsDeprecated()
        {
            if (this is IEditableNode tt)
            {
                var p = (IEditableNode?)this.PrevStableVersion();
                if (p != null && !p.IsMarkedForDeletion && tt.IsMarkedForDeletion)
                    return true;
            }
            return false;
        }
        protected void OnRemoveChild()
        {
            if (IEditableNodeGroup.IsChangedNotPropagate)
                return;
            this.CheckChildrenIsOrHasChanged();
            this.CheckChildrenIsOrHasNew();
            this.CheckChildrenIsOrHasMarkedForDeletion();
        }
        public void RestoreIsHas()
        {
            if (this is IEditableNodeGroup pp)
            {
                bool isHasChanged = false, isHasNew = false, isHasMarked = false;
                foreach (var t in this.GetListChildren())
                {
                    if (t is IEditableNode p)
                    {
                        if (p.IsChanged)
                            isHasChanged = true;
                        if (t is ICanAddNode p2 && p2.IsNew)
                            isHasNew = true;
                        if (p.IsMarkedForDeletion)
                            isHasMarked = true;
                    }
                    if (t is IEditableNodeGroup pg)
                    {
                        pg.RestoreIsHas();
                        if (pg.IsHasChanged)
                            isHasChanged = true;
                        if (pg.IsHasNew)
                            isHasNew = true;
                        if (pg.IsHasMarkedForDeletion)
                            isHasMarked = true;
                    }
                }
                this._IsHasChanged = isHasChanged;
                this.NotifyPropertyChanged(nameof(this.IsHasChanged));
                this.NotifyPropertyChanged(nameof(this.IsChangedOrHasChanged));
                this._IsHasNew = isHasNew;
                this.NotifyPropertyChanged(nameof(this.IsHasNew));
                this.NotifyPropertyChanged(nameof(this.IsNewOrHasNew));
                this._IsHasMarkedForDeletion = isHasMarked;
                this.NotifyPropertyChanged(nameof(this.IsHasMarkedForDeletion));
            }
        }
        public void CheckChildrenIsOrHasChanged()
        {
            if (this is IEditableNodeGroup pp)
            {
                bool isHasChanged = false;
                foreach (var t in this.GetListChildren())
                {
                    if (t is IEditableObjectExt p)
                    {
                        if (p.IsChanged)
                        {
                            isHasChanged = true;
                            break;
                        }
                    }
                    if (t is IEditableNodeGroup pg2)
                    {
                        if (pg2.IsHasChanged)
                        {
                            isHasChanged = true;
                            break;
                        }
                    }
                }
                if (!isHasChanged)
                {
                    foreach (var p in this.GetEditableNodeSettings())
                    {
                        if (p.IsChanged)
                        {
                            isHasChanged = true;
                            break;
                        }
                    }
                }
                pp.IsHasChanged = isHasChanged;
            }
            if (this.Parent != null && this.Parent is IEditableNodeGroup pg)
            {
                pg.CheckChildrenIsOrHasChanged();
            }
        }
        public void CheckChildrenIsOrHasNew()
        {
            if (this is IEditableNodeGroup pp)
            {
                bool isHasNew = false;
                foreach (var t in this.GetListChildren())
                {
                    if (t is ICanAddNode p)
                    {
                        if (p.IsNew)
                        {
                            isHasNew = true;
                            break;
                        }
                    }
                    if (t is IEditableNodeGroup pg2)
                    {
                        if (pg2.IsHasNew)
                        {
                            isHasNew = true;
                            break;
                        }
                    }
                }
                pp.IsHasNew = isHasNew;
            }
            if (this.Parent != null && this.Parent is IEditableNodeGroup pg)
            {
                pg.CheckChildrenIsOrHasNew();
            }
        }
        public void CheckChildrenIsOrHasMarkedForDeletion()
        {
            if (this is IEditableNodeGroup pp)
            {
                bool isHasMarked = false;
                foreach (var t in this.GetListChildren())
                {
                    if (t is IEditableNode p)
                    {
                        if (p.IsMarkedForDeletion)
                        {
                            isHasMarked = true;
                            break;
                        }
                    }
                    if (t is IEditableNodeGroup pg2)
                    {
                        if (pg2.IsHasMarkedForDeletion)
                        {
                            isHasMarked = true;
                            break;
                        }
                    }
                }
                pp.IsHasMarkedForDeletion = isHasMarked;
            }
            if (this.Parent != null && this.Parent is IEditableNodeGroup pg)
            {
                pg.CheckChildrenIsOrHasMarkedForDeletion();
            }
        }
        public virtual List<IEditableObjectExt> GetEditableNodeSettings()
        {
            return new List<IEditableObjectExt>();
        }
        protected void OnNodeIsChangedChanged()
        {
            if (IEditableNodeGroup.IsChangedNotPropagate)
                return;
            this.CheckChildrenIsOrHasChanged();
        }
        protected void OnNodeIsNewChanged()
        {
            this.CheckChildrenIsOrHasNew();
        }
        protected void OnNodeIsMarkedForDeletionChanged()
        {
            this.CheckChildrenIsOrHasMarkedForDeletion();
        }

        [Browsable(false)]
        public bool IsChangedOrHasChanged
        {
            get { return this.IsChanged || this.IsHasChanged; }
        }
        [Browsable(false)]
        public bool IsNewOrHasNew
        {
            get { return this._IsHasNew || (this is ICanAddNode d && d.IsNew); }
        }
        [Browsable(false)]
        public bool IsHasNew
        {
            get { return this._IsHasNew; }
            set
            {
                this.OnIsHasNewChanging(ref value);
                if (SetProperty(ref this._IsHasNew, value))
                {
                    this.OnIsHasNewChanged();
                    this.NotifyPropertyChanged(nameof(this.IsNewOrHasNew));
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsHasNew;
        partial void OnIsHasNewChanging(ref bool to);
        private void OnIsHasNewChanged()
        {
            if (this is IConfig)
                return;
            if (this is IEditableNodeGroup p)
            {
                if (this.Parent is IEditableNodeGroup pp)
                {
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
        }
        [Browsable(false)]
        public bool IsHasChanged
        {
            get { return this._IsHasChanged; }
            set
            {
                if (SetProperty(ref this._IsHasChanged, value))
                {
                    this.OnIsHasChangedChanged();
                }
            }
        }
        private bool _IsHasChanged;
        //partial void OnIsHasChangedChanging(ref bool to);
        private void OnIsHasChangedChanged()
        {
            if (IEditableNodeGroup.IsChangedNotPropagate)
                return;
            if (this is IEditableNodeGroup p)
            {
                if (this.Parent is IEditableNodeGroup pp)
                {
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
            if (this is IConfig cfg)
            {
                if (cfg.IsHasChanged && !cfg.IsNeedCurrentUpdate)
                    cfg.SetIsNeedCurrentUpdate(true);
                return;
            }
        }
        [Browsable(false)]
        public bool IsHasMarkedForDeletion
        {
            get { return this._IsHasMarkedForDeletion; }
            set
            {
                this.OnIsHasMarkedForDeletionChanging(ref value);
                if (SetProperty(ref this._IsHasMarkedForDeletion, value))
                {
                    this.OnIsHasMarkedForDeletionChanged();
                    this.ValidateProperty();
                }
            }
        }
        private bool _IsHasMarkedForDeletion;
        partial void OnIsHasMarkedForDeletionChanging(ref bool to);
        private void OnIsHasMarkedForDeletionChanged()
        {
            if (this is IConfig)
                return;
            if (this is IEditableNodeGroup p)
            {
                if (this.Parent is IEditableNodeGroup pp)
                {
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
        }

        #region ITree
        public class DummyChildrenCollection : List<object>, IChildrenCollection { }
        public virtual IChildrenCollection GetListChildren()
        {
            return new DummyChildrenCollection();
        }
        public virtual IChildrenCollection GetListSiblings()
        {
            return new DummyChildrenCollection();
        }
        public virtual bool HasChildren()
        {
            return this.Children != null && this.Children.Count > 0;
        }
        #endregion ITree
        #region ITreeModel
        public IEnumerable GetChildren(object parent)
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
