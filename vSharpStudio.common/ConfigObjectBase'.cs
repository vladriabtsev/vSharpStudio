namespace vSharpStudio.common
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using FluentValidation;
    using JetBrains.Annotations;
    using Microsoft.Extensions.Logging;
    using ViewModelBase;
    using vSharpStudio.common;
    using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

    public partial class ConfigObjectBase<T, TValidator> : ViewModelValidatableWithSeverity<T, TValidator>, IComparable<T>, ISortingValue, ITreeConfigNode, IObjectAnnotatable
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectBase<T, TValidator>, IComparable<T>, ISortingValue // , ITreeConfigNode
    {
        protected ILogger _logger;
        public ConfigObjectBase(ITreeConfigNode parent, TValidator validator)
            : base(validator)
        {
            this._logger = Logger.CreateLogger<T>();
            this.Parent = parent;
            this.ListInModels = new List<IModelRow>();
        }

        protected virtual void OnInitFromDto()
        {
            this._logger.LogTrace("".CallerInfo());
        }

        private static int _maxlen = 0;

        public override int CompareToById(T other)
        {
            ITreeConfigNode p = (ITreeConfigNode)this;
            return p.Guid.CompareTo(other.Guid);
        }
        public override void Restore(T from)
        {
            throw new NotImplementedException("Please override Restore method");
        }

        public override T Backup()
        {
            throw new NotImplementedException("Please override Backup method");
        }

        protected override void OnCountErrorsChanged()
        {
            this.NotifyPropertyChanged(p => p.IconStatus);
        }

        protected override void OnCountWarningsChanged()
        {
            this.NotifyPropertyChanged(p => p.IconStatus);
        }

        protected override void OnCountInfosChanged()
        {
            this.NotifyPropertyChanged(p => p.IconStatus);
        }

        [BrowsableAttribute(false)]
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

        [BrowsableAttribute(false)]
        public string IconFolder
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
            return this.SortingValue.CompareTo(other.SortingValue);
        }

        #region Sort

        [BrowsableAttribute(false)]
        public ulong SortingWeight { get; set; }

        [BrowsableAttribute(false)]
        public ulong SortingValue
        {
            get
            {
                return this._SortingValue;
            }

            set
            {
                if (this._SortingValue != value)
                {
                    this.OnSortingValueChanging();
                    this._SortingValue = value;
                    this.OnSortingValueChanged();
                    this.NotifyPropertyChanged();
                    // ValidateProperty();
                    ITreeConfigNode p = (ITreeConfigNode)this;
                    if (p.Parent != null)
                    {
                        p.Parent.Sort(this.GetType());
                    }
                }
            }
        }

        private ulong _SortingValue;

        public virtual void Sort(Type type)
        {
            throw new NotImplementedException();
        }

        partial void OnSortingValueChanging();

        partial void OnSortingValueChanged();

        #endregion Sort

        [ReadOnly(true)]
        public string Guid
        {
            get
            {
                if (this._Guid == null)
                {
                    this.SetNewGuid();
                    this.NotifyPropertyChanged(); // to recognize object was changed
                }
                return this._Guid;
            }

            protected set
            {
                this._Guid = value;
                this.NotifyPropertyChanged();
            }
        }

        private string _Guid = null;

        protected void SetNewGuid()
        {
            this._Guid = System.Guid.NewGuid().ToString();
        }
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
        public string FullName
        {
            get
            {
                if (this.Parent == null)
                {
                    return "MainConfig." + this.Name;
                }

                return this.GetConfig().Name + "." + this.Name;
            }
        }

        public IConfig GetConfig()
        {
            if (this._config == null)
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

                this._config = (IConfig)p;
            }
            return this._config;
        }

        private IConfig _config = null;

        public T GetPrevious()
        {
            T res = null;
            if (this.GetConfig()?.PrevStableConfig != null && this.GetConfig().PrevStableConfig.DicNodes.ContainsKey(this.Parent.Guid))
            {
                res = (T)this.GetConfig().PrevStableConfig.DicNodes[this.Guid];
            }

            return res;
        }

        [PropertyOrder(0)]
        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                if (this._Name != value)
                {
                    this._Name = value.Trim();
                    this.NotifyPropertyChanged();
                    if (this.ValidateProperty())
                    {
                        this.SortingValue = this.EncodeNameToUlong(this._Name) + this.SortingWeight;
                        ITreeConfigNode p = (ITreeConfigNode)this;
                        if (p.Parent != null)
                        {
                            p.Parent.Sort(this.GetType());
                        }
                        // SetSelected(this);
                    }
                }
            }
        }

        protected void SetSelected(ITreeConfigNode node)
        {
            if (this.Parent != null)
            {
                this.GetConfig().SelectedNode = node;
            }
        }

        private string _Name = string.Empty;

        [PropertyOrder(1)]
        [DisplayName("UI Name")]
        public string NameUi
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._NameUi) && !string.IsNullOrEmpty(this._Name))
                {
                    return this._Name;
                }

                return this._NameUi;
            }

            set
            {
                if (this._NameUi != value)
                {
                    this._NameUi = value.Trim();
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }

        private string _NameUi = string.Empty;

        protected ulong EncodeNameToUlong(string name)
        {
            const int step = 1 + '9' - '0' + 1 + 'Z' - 'A' + 1; // first is '_'
            if (_maxlen == 0)
            {
                _maxlen = (int)Math.Log(ViewModelBindable.SortingWeightBase, step);
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
                    this.NotifyPropertyChanged(p => p.IconFolder);
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
                return true;
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

        public virtual bool NodeCanRemove()
        {
            if (this is ICanRemoveNode)
            {
                return true;
            }

            return false;
        }

        public virtual void NodeRemove()
        {
            throw new NotImplementedException();
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

        #region IMutableAnnotatable

        public Annotation FindAnnotation(IAppProjectGenerator projectGenerator)
        {
            return this.FindAnnotation(projectGenerator.Guid);
        }
        private readonly SortedDictionary<string, Annotation> _annotations =
            new SortedDictionary<string, Annotation>();

        /// <summary>
        ///     Adds an annotation to this object. Throws if an annotation with the specified name already exists.
        /// </summary>
        /// <param name="name"> The key of the annotation to be added. </param>
        /// <param name="value"> The value to be stored in the annotation. </param>
        /// <returns> The newly added annotation. </returns>
        public virtual Annotation AddAnnotation(string name, object value)
        {
            // Check.NotEmpty(name, nameof(name));

            var annotation = this.CreateAnnotation(name, value);

            return this.AddAnnotation(name, annotation);
        }

        /// <summary>
        ///     Adds an annotation to this object. Throws if an annotation with the specified name already exists.
        /// </summary>
        /// <param name="name"> The key of the annotation to be added. </param>
        /// <param name="annotation"> The annotation to be added. </param>
        /// <returns> The added annotation. </returns>
        protected virtual Annotation AddAnnotation([NotNull] string name, [NotNull] Annotation annotation)
        {
            if (this.FindAnnotation(name) != null)
            {
                throw new InvalidOperationException();
            }

            this.SetAnnotation(name, annotation);

            return annotation;
        }

        /// <summary>
        ///     Sets the annotation stored under the given key. Overwrites the existing annotation if an
        ///     annotation with the specified name already exists.
        /// </summary>
        /// <param name="name"> The key of the annotation to be added. </param>
        /// <param name="value"> The value to be stored in the annotation. </param>
        public virtual void SetAnnotation(string name, object value)
            => this.SetAnnotation(name, this.CreateAnnotation(name, value));

        /// <summary>
        ///     Sets the annotation stored under the given key. Overwrites the existing annotation if an
        ///     annotation with the specified name already exists.
        /// </summary>
        /// <param name="name"> The key of the annotation to be added. </param>
        /// <param name="annotation"> The annotation to be set. </param>
        /// <returns> The annotation that was set. </returns>
        protected virtual Annotation SetAnnotation([NotNull] string name, [NotNull] Annotation annotation)
        {
            var oldAnnotation = this.FindAnnotation(name);

            this._annotations[name] = annotation;

            return oldAnnotation != null
                   && Equals(oldAnnotation.Value, annotation.Value)
                ? annotation
                : this.OnAnnotationSet(name, annotation, oldAnnotation);
        }

        /// <summary>
        ///     Runs the corresponding conventions when an annotation was set or removed.
        /// </summary>
        /// <param name="name"> The key of the set annotation. </param>
        /// <param name="annotation"> The annotation set. </param>
        /// <param name="oldAnnotation"> The old annotation. </param>
        /// <returns> The annotation that was set. </returns>
        protected virtual Annotation OnAnnotationSet(
            [NotNull] string name, [CanBeNull] Annotation annotation, [CanBeNull] Annotation oldAnnotation)
            => annotation;

        /// <summary>
        ///     Gets the annotation with the given name, returning null if it does not exist.
        /// </summary>
        /// <param name="name"> The key of the annotation to find. </param>
        /// <returns>
        ///     The existing annotation if an annotation with the specified name already exists. Otherwise, null.
        /// </returns>
        protected virtual Annotation FindAnnotation(string name)
        {
            // Check.NotEmpty(name, nameof(name));

            return this._annotations.TryGetValue(name, out var annotation)
                ? annotation
                : null;
        }

        /// <summary>
        ///     Removes the given annotation from this object.
        /// </summary>
        /// <param name="name"> The annotation to remove. </param>
        /// <returns> The annotation that was removed. </returns>
        public virtual Annotation RemoveAnnotation(string name)
        {
            // Check.NotNull(name, nameof(name));

            var annotation = this.FindAnnotation(name);
            if (annotation == null)
            {
                return null;
            }

            this._annotations.Remove(name);

            this.OnAnnotationSet(name, null, annotation);

            return annotation;
        }

        /// <summary>
        ///     Gets the value annotation with the given name, returning null if it does not exist.
        /// </summary>
        /// <param name="name"> The key of the annotation to find. </param>
        /// <returns>
        ///     The value of the existing annotation if an annotation with the specified name already exists.
        ///     Otherwise, null.
        /// </returns>
        public virtual object this[string name]
        {
            get => this.FindAnnotation(name)?.Value;
            set
            {
                // Check.NotEmpty(name, nameof(name));

                if (value == null)
                {
                    this.RemoveAnnotation(name);
                }
                else
                {
                    this.SetAnnotation(name, this.CreateAnnotation(name, value));
                }
            }
        }

        /// <summary>
        ///     Creates a new annotation.
        /// </summary>
        /// <param name="name"> The key of the annotation. </param>
        /// <param name="value"> The value to be stored in the annotation. </param>
        /// <returns> The newly created annotation. </returns>
        protected virtual Annotation CreateAnnotation([NotNull] string name, [CanBeNull] object value)
            => new Annotation(name, value);

        /// <summary>
        ///     Gets all annotations on the current object.
        /// </summary>
        IEnumerable<Annotation> IObjectAnnotatable.GetAnnotations() => this.GetAnnotations();

        /// <summary>
        ///     Gets the annotation with the given name, returning null if it does not exist.
        /// </summary>
        /// <param name="name"> The key of the annotation to find. </param>
        /// <returns>
        ///     The existing annotation if an annotation with the specified name already exists. Otherwise, null.
        /// </returns>
        Annotation IObjectAnnotatable.FindAnnotation(string name) => this.FindAnnotation(name);
        /// <summary>
        ///     Gets all annotations on the current object.
        /// </summary>
        public virtual IEnumerable<Annotation> GetAnnotations() =>
            this._annotations != null
                ? this._annotations.Values.Where(a => a.Value != null)
                : Enumerable.Empty<Annotation>();

        #endregion IMutableAnnotatable

        public virtual bool HasChildren(object parent)
        {
            return false;
        }

        public virtual IEnumerable<object> GetChildren(object parent)
        {
            throw new NotImplementedException();
        }

        [BrowsableAttribute(false)]
        public bool IsIncludableInModels { get; protected set; }

        public List<IModelRow> ListInModels { get; protected set; }
    }
}
