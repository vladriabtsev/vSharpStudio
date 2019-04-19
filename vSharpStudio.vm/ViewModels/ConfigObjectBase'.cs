using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using FluentValidation;
using ViewModelBase;
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
            //            this.PropertyChanged += ConfigObjectWithGuidBase_PropertyChanged;
        }
        //private void ConfigObjectWithGuidBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "SortingValue")
        //    {
        //        ITreeConfigNode p = (ITreeConfigNode)this;
        //        if (p.Parent != null)
        //            p.Parent.Sort(this.GetType());
        //    }
        //}
        protected virtual void OnInitFromDto()
        {
        }

        private static int _maxlen = 0;
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
        public override int CompareToById(T other)
        {
            ITreeConfigNode p = (ITreeConfigNode)this;
            return p.Guid.CompareTo(other.Guid);
        }
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }
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
        public virtual void OnIsExpandedChanged()
        {
            NotifyPropertyChanged(p => p.StatusIcon);
        }
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

        public int CompareTo(T other) { return this.SortingValue.CompareTo(other.SortingValue); }

        #region ITreeConfigNode

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
        partial void OnSortingValueChanging();
        partial void OnSortingValueChanged();
        [BrowsableAttribute(false)]
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
                        this.SortingValue = EncodeNameToUlong(this.Name) + this.SortingWeight;
                        ITreeConfigNode p = (ITreeConfigNode)this;
                        if (p.Parent != null)
                            p.Parent.Sort(this.GetType());
                    }
                }
            }
            get { return _Name; }
        }
        private string _Name = "";
        [BrowsableAttribute(false)]
        public string NodeText { get { return this.Name; } }
        [BrowsableAttribute(false)]
        public bool IsSelected
        {
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    NotifyPropertyChanged();
                    OnIsSelectedChanged();
                }
            }
            get { return _IsSelected; }
        }
        private bool _IsSelected;
        public virtual void OnIsSelectedChanged() { }
        [BrowsableAttribute(false)]
        public bool IsExpanded
        {
            set
            {
                if (_IsExpanded != value)
                {
                    _IsExpanded = value;
                    NotifyPropertyChanged();
                    OnIsExpandedChanged();
                }
            }
            get { return _IsExpanded; }
        }
        private bool _IsExpanded;
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }
        public virtual void Sort(Type type)
        {
            throw new NotImplementedException();
        }
        public bool NodeCanMoveUp()
        {
            return OnNodeCanMoveUp();
        }
        protected virtual bool OnNodeCanMoveUp()
        {
            throw new NotImplementedException("Has to be overriden");
        }
        public void NodeMoveUp()
        {
            OnNodeMoveUp();
        }
        protected virtual void OnNodeMoveUp()
        {
            throw new NotImplementedException("Has to be overriden");
        }
        public bool NodeCanMoveDown()
        {
            return OnNodeCanMoveDown();
        }
        protected virtual bool OnNodeCanMoveDown()
        {
            throw new NotImplementedException("Has to be overriden");
        }
        public void NodeMoveDown()
        {
            OnNodeMoveDown();
        }
        protected virtual void OnNodeMoveDown()
        {
            throw new NotImplementedException("Has to be overriden");
        }
        public bool NodeCanAddNew()
        {
            return OnNodeCanAddNew();
        }
        protected virtual bool OnNodeCanAddNew()
        {
            return true;
        }
        public ITreeConfigNode NodeAddNew()
        {
            return OnNodeAddNew();
        }
        protected virtual ITreeConfigNode OnNodeAddNew()
        {
            throw new NotImplementedException("Has to be overriden");
        }
        public bool NodeCanAddNewSubNode()
        {
            return OnNodeCanAddNewSubNode();
        }
        protected virtual bool OnNodeCanAddNewSubNode()
        {
            return false;
        }
        public ITreeConfigNode NodeAddNewSubNode()
        {
            return OnNodeAddNewSubNode();
        }
        protected virtual ITreeConfigNode OnNodeAddNewSubNode()
        {
            throw new NotImplementedException("Has to be overriden");
        }
        public bool NodeCanAddClone()
        {
            return OnNodeCanAddClone();
        }
        protected virtual bool OnNodeCanAddClone()
        {
            return true;
        }
        public ITreeConfigNode NodeAddClone()
        {
            return OnNodeAddClone();
        }
        protected virtual ITreeConfigNode OnNodeAddClone()
        {
            throw new NotImplementedException("Has to be overriden");
        }
        public bool NodeCanRemove()
        {
            return OnNodeCanRemove();
        }
        protected virtual bool OnNodeCanRemove()
        {
            return true;
        }
        public void NodeRemove()
        {
            OnNodeRemove();
        }
        protected virtual void OnNodeRemove()
        {
            throw new NotImplementedException("Has to be overriden");
        }

        public bool NodeCanLeft()
        {
            return OnNodeCanLeft();
        }
        protected virtual bool OnNodeCanLeft()
        {
            return true;
        }
        public void NodeLeft()
        {
            OnNodeLeft();
        }
        protected virtual void OnNodeLeft()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanRight()
        {
            return OnNodeCanRight();
        }
        protected virtual bool OnNodeCanRight()
        {
            return true;
        }

        public void NodeRight()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanUp()
        {
            return OnNodeCanUp();
        }
        protected virtual bool OnNodeCanUp()
        {
            return true;
        }
        public void NodeUp()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanDown()
        {
            return OnNodeCanDown();
        }
        protected virtual bool OnNodeCanDown()
        {
            return true;
        }

        public void NodeDown()
        {
            throw new NotImplementedException();
        }

        #endregion ITreeConfigNode
    }
}
