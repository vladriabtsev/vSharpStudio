using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModelBase
{
    public class DictionaryExt<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private bool isReturnDefaultWhenNotInDictionary;
        public DictionaryExt(int initialSize = 100, bool isReturnDefaultWhenNotInDictionary = false, bool isActivateActions = false, Action<TKey, TValue> onAddValue = null,
            Action<TKey, TValue> onRemoveValue = null, Action onClear = null) : base(initialSize)
        {
            this.IsActivateActions = isActivateActions;
            this.OnAddValue = onAddValue;
            this.OnRemoveValue = onRemoveValue;
            this.OnClear = onClear;
            this.isReturnDefaultWhenNotInDictionary = isReturnDefaultWhenNotInDictionary;
        }
        public bool IsActivateActions { get; set; }
        public Action<TKey, TValue> OnAddValue { get; set; }
        public Action<TKey, TValue> OnRemoveValue { get; set; }
        public Action OnClear { get; set; }
        public new TValue this[TKey key]
        {
            get
            {
                if (isReturnDefaultWhenNotInDictionary)
                {
                    if (this.ContainsKey(key))
                        return base[key];
                    return default(TValue);
                }
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
