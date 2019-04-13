using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigObjectBase<T, TValidator> : ViewModelValidatableWithSeverity<T, TValidator>, ITreeConfigNode
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectBase<T, TValidator>, ITreeConfigNode
    {
        public ConfigObjectBase(TValidator validator)
            : base(validator)
        {
            this.PropertyChanged += ConfigObjectWithGuidBase_PropertyChanged;
        }
        private void ConfigObjectWithGuidBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SortingValue")
            {
                ITreeConfigNode p = (ITreeConfigNode)this;
                if (p.Parent != null)
                    p.Parent.Sort(this.GetType());
            }
        }

        private static int _maxlen = 0;
        protected ulong EncodeNameToUlong(string name)
        {
            const int step = 1 + '9' - '0' + 1 + 'Z' - 'A' + 1; // first is '_'
            if (_maxlen == 0)
                _maxlen = (int)Math.Log(ulong.MaxValue, step);
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
        public override int CompareToById(T other)
        {
            ITreeConfigNode p = (ITreeConfigNode)this;
            return p.Guid.CompareTo(other.Guid);
        }
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }
        protected override void OnCountErrorsChanged() { }
        protected override void OnCountWarningsChanged() { }
        protected override void OnCountInfosChanged() { }

        #region ITreeConfigNode

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
                    ValidateProperty();
                }
            }
            get { return _SortingValue; }
        }
        private ulong _SortingValue;
        partial void OnSortingValueChanging();
        partial void OnSortingValueChanged();

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
        public string Name
        {
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                    this.SortingValue = EncodeNameToUlong(this.Name);
                }
            }
            get { return _Name; }
        }
        private string _Name = "";
        public string NodeText { get { return this.Name; } }
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
        public virtual void OnIsExpandedChanged() { }
        public ITreeConfigNode Parent { get; set; }
        public IEnumerable<ITreeConfigNode> SubNodes => throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        public void NodeMoveUp()
        {
            OnNodeMoveUp();
        }
        protected virtual void OnNodeMoveUp()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanMoveDown()
        {
            return OnNodeCanMoveDown();
        }
        protected virtual bool OnNodeCanMoveDown()
        {
            throw new NotImplementedException();
        }
        public void NodeMoveDown()
        {
            OnNodeMoveDown();
        }
        protected virtual void OnNodeMoveDown()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanAdd()
        {
            return OnNodeCanAdd();
        }
        protected virtual bool OnNodeCanAdd()
        {
            return true;
        }
        public ITreeConfigNode NodeAddNew()
        {
            return OnNodeAddNew();
        }
        protected virtual ITreeConfigNode OnNodeAddNew()
        {
            throw new NotImplementedException();
        }
        public ITreeConfigNode NodeAddClone()
        {
            return OnNodeAddClone();
        }
        protected virtual ITreeConfigNode OnNodeAddClone()
        {
            throw new NotImplementedException();
        }
        public bool NodeCanRemove()
        {
            return OnNodeCanRemove();
        }
        protected bool OnNodeCanRemove()
        {
            return true;
        }
        public void NodeRemove()
        {
            OnNodeRemove();
        }
        protected virtual void OnNodeRemove()
        {
            throw new NotImplementedException();
        }

        public bool NodeCanLeft()
        {
            throw new NotImplementedException();
        }
        public void NodeLeft()
        {
            OnNodeCanLeft();
        }
        protected virtual bool OnNodeCanLeft()
        {
            return true;
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
