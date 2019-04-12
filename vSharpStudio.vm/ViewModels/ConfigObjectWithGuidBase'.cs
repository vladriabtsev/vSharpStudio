using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class ConfigObjectWithGuidBase<T, TValidator> : ConfigObjectBase<T, TValidator>
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectWithGuidBase<T, TValidator>, ITreeConfigNode
    {
        public ConfigObjectWithGuidBase(TValidator validator)
            : base(validator)
        {
            this.PropertyChanged += ConfigObjectWithGuidBase_PropertyChanged;
        }
        private void ConfigObjectWithGuidBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                IConfigObject p = (IConfigObject)this;
                p.SortingValue = EncodeNameToUlong(p.Name);
            }
        }
        public string Guid
        {
            get
            {
                if (_Guid == null)
                {
                    _Guid = System.Guid.NewGuid().ToString();
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
            IConfigObject p = (IConfigObject)this;
            return p.Guid.CompareTo(other.Guid);
        }
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }
        protected override void OnCountErrorsChanged() { }
        protected override void OnCountWarningsChanged() { }
        protected override void OnCountInfosChanged() { }
    }
}
