using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModelBase
{
    public class DictionaryExt<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public DictionaryExt(Action<TKey, TValue> onAddValue, Action<TKey, TValue> onRemoveValue, Action onClear)
        {
            this.OnAddValue = onAddValue;
            this.OnRemoveValue = onRemoveValue;
            this.OnClear = onClear;
        }
        public bool IsActivateActions { get; set; }
        public Action<TKey, TValue> OnAddValue { get; set; }
        public Action<TKey, TValue> OnRemoveValue { get; set; }
        public Action OnClear { get; set; }
        public new TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                if (this.IsActivateActions && OnAddValue != null)
                    OnAddValue(key, value);
                base[key] = value;
            }
        }
        public new void Add(TKey key, TValue value)
        {
            if (this.IsActivateActions && OnAddValue != null)
                OnAddValue(key, value);
            base.Add(key, value);
        }
        public new bool Remove(TKey key)
        {
            if (this.IsActivateActions && OnRemoveValue != null)
            {
                TValue value = base[key];
                OnRemoveValue(key, value);
            }
            return base.Remove(key);
        }
        public new void Clear()
        {
            if (this.IsActivateActions && OnClear != null)
                OnClear();
            base.Clear();
        }
    }
}
