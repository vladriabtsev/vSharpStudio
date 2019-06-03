using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigObjectBase<T, TValidator> : ViewModelValidatableWithSeverity<T, TValidator>, IComparable<T>, ISortingValue, ITreeConfigNode
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectBase<T, TValidator>, IComparable<T>, ISortingValue //, ITreeConfigNode
    {
        public ConfigObjectBase(TValidator validator)
            : base(validator)
        {
        }
        protected virtual void OnInitFromDto()
        {
        }

        private static int _maxlen = 0;
        public override int CompareToById(T other)
        {
            ITreeConfigNode p = (ITreeConfigNode)this;
            return p.Guid.CompareTo(other.Guid);
        }
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }
        protected override void OnCountErrorsChanged()
        {
            NotifyPropertyChanged(p => p.IconStatus);
        }
        protected override void OnCountWarningsChanged()
        {
            NotifyPropertyChanged(p => p.IconStatus);
        }
        protected override void OnCountInfosChanged()
        {
            NotifyPropertyChanged(p => p.IconStatus);
        }
        [BrowsableAttribute(false)]
        public string IconStatus
        {
            get
            {
                string iconName = null;
                if (this.CountErrors > 0)
                    iconName = "iconStatusCriticalError";
                else
                {
                    if (this.CountWarnings > 0)
                        iconName = "iconStatusWarning";
                    else
                    {
                        if (this.CountInfos > 0)
                            iconName = "iconStatusInformation";
                        else
                            iconName = null;
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
                    iconName = "iconFolderOpen";
                else
                    iconName = "iconFolder";
                return iconName;
            }
        }

        public int CompareTo(T other) { return this.SortingValue.CompareTo(other.SortingValue); }

        #region Sort

        [BrowsableAttribute(false)]
        public ulong SortingWeight { get; set; }
        [BrowsableAttribute(false)]
        public ulong SortingValue
        {
            set
            {
                if (_SortingValue != value)
                {
                    OnSortingValueChanging();
                    _SortingValue = value;
                    OnSortingValueChanged();
                    NotifyPropertyChanged();
                    //ValidateProperty();
                    ITreeConfigNode p = (ITreeConfigNode)this;
                    if (p.Parent != null)
                        p.Parent.Sort(this.GetType());
                }
            }
            get { return _SortingValue; }
        }
        private ulong _SortingValue;
        public virtual void Sort(Type type)
        {
            throw new NotImplementedException();
        }
        partial void OnSortingValueChanging();
        partial void OnSortingValueChanged();

        #endregion Sort

        [Editable(false)]
        public string Guid
        {
            get
            {
                if (_Guid == null)
                {
                    SetNewGuid();
                    NotifyPropertyChanged(); // to recognize object was changed
                }
                return _Guid;
            }
            protected set
            {
                _Guid = value;
                NotifyPropertyChanged();
            }
        }
        private string _Guid = null;
        protected void SetNewGuid()
        {
            _Guid = System.Guid.NewGuid().ToString();
        }
        [PropertyOrder(0)]
        public string Name
        {
            set
            {
                if (_Name != value)
                {
                    _Name = value.Trim();
                    NotifyPropertyChanged();
                    if (ValidateProperty())
                    {
                        this.SortingValue = EncodeNameToUlong(this._Name) + this.SortingWeight;
                        ITreeConfigNode p = (ITreeConfigNode)this;
                        if (p.Parent != null)
                            p.Parent.Sort(this.GetType());
                        SetSelected(this);
                    }
                }
            }
            get { return _Name; }
        }

        protected void SetSelected(ITreeConfigNode node)
        {
            if (this.Parent != null)
            {
                ITreeConfigNode config = this.Parent;
                while (config.Parent != null)
                    config = config.Parent;
                if (config is Config)
                    (config as Config).SelectedNode = node;
                //else
                //    throw new Exception();
            }
        }

        private string _Name = "";
        [PropertyOrder(1)]
        [DisplayName("UI Name")]
        public string NameUi
        {
            set
            {
                if (_NameUi != value)
                {
                    _NameUi = value.Trim();
                    NotifyPropertyChanged();
                    ValidateProperty();
                }
            }
            get
            {
                if (string.IsNullOrWhiteSpace(_NameUi) && !string.IsNullOrEmpty(_Name))
                    return _Name;
                return _NameUi;
            }
        }
        private string _NameUi = "";
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
                    ci = c - '0' + 1;
                else if (c == '_')
                    ci = 0;
                else if (c >= 'A' && c <= 'Z')
                    ci = c - 'A' + 11;
                else
                    throw new ArgumentException("Unexpected char value: '" + c + "'");
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
                return;
            int i = 0;
            foreach (var tt in lst)
            {
                if (tt == configObject)
                    continue;
                if (tt.Name.StartsWith(defName))
                {
                    string s = tt.Name.Remove(0, defName.Length);
                    int ii;
                    if (int.TryParse(s, out ii))
                    {
                        if (ii > i) i = ii;
                    }
                }
            }
            i++;
            configObject.Name = defName + i;
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent
        {
            get { return _Parent; }
            set
            {
                _Parent = value;
                OnParentChanged();
            }
        }
        private ITreeConfigNode _Parent;
        protected virtual void OnParentChanged() { }
        [BrowsableAttribute(false)]
        public bool IsSelected
        {
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _IsSelected; }
        }
        private bool _IsSelected;
        [BrowsableAttribute(false)]
        public bool IsExpanded
        {
            set
            {
                if (_IsExpanded != value)
                {
                    _IsExpanded = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(p => p.IconFolder);
                }
            }
            get { return _IsExpanded; }
        }
        private bool _IsExpanded;

        #region Commands

        public bool NodeCanAddNew()
        {
            if (this is ICanAddNode)
                return true;
            return false;
        }
        public virtual ITreeConfigNode NodeAddNew()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanAddNewSubNode()
        {
            if (this is ICanAddSubNode)
                return true;
            return false;
        }
        public virtual ITreeConfigNode NodeAddNewSubNode()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanAddClone()
        {
            if (this is ICanAddNode)
                return true;
            return false;
        }
        public virtual ITreeConfigNode NodeAddClone()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanMoveDown()
        {
            if (!(this is ICanAddNode))
                return false;
            return NodeCanDown();
        }
        public virtual void NodeMoveDown()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanMoveUp()
        {
            if (!(this is ICanAddNode))
                return false;
            return NodeCanUp();
        }
        public virtual void NodeMoveUp()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanRemove()
        {
            if (this is ICanAddNode)
                return true;
            return false;
        }
        public virtual void NodeRemove()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanLeft()
        {
            if (this is ICanGoLeft)
                return true;
            return false;
        }
        public void NodeLeft()
        {
            SetSelected(this.Parent);
        }
        public bool NodeCanRight()
        {
            if (this is ICanGoRight)
                return true;
            return false;
        }
        private bool IsIListNodesGen(object obj)
        {
            bool res = false;
            foreach (var t in obj.GetType().GetInterfaces())
            {
                if (t.Name.StartsWith("IListNodes`"))
                    return true;
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
    }
}
