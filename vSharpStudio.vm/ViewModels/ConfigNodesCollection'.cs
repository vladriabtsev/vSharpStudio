using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ConfigNodesCollection<T> : SortedObservableCollection<T>, IChildrenCollection
      where T : ITreeConfigNode, ISortingValue
    {
        private readonly Config? cfg;
        private readonly ITreeConfigNode? parent;
        private readonly bool isUseDicNodes = false;

        public ConfigNodesCollection(ITreeConfigNode? parent)
        {
            this.isUseDicNodes = !(typeof(T).Name == typeof(PluginGeneratorNodeSettings).Name);
            this.parent = parent;
            if (this.parent == null)
            {
            }
            else
                this.cfg = (Config)this.parent.Cfg;
        }
        public new void AddRange(IEnumerable<T> collection, ulong sortingWeight = 0)
        {
            Debug.Assert(collection != null);
            Debug.Assert(this.cfg != null);
            foreach (T item in collection)
            {
                item.Parent = this.parent;
                if (isUseDicNodes && this.cfg.IsInitialized)
                    this.cfg._DicNodes[item.Guid] = item;
            }
            base.AddRange(collection, sortingWeight);
        }

        public void Add(object item)
        {
            this.Add((T)item);
        }
        public new void Add(T item)
        {
            Debug.Assert(this.cfg != null);
            if (isUseDicNodes && this.cfg.IsInitialized)
                this.cfg._DicNodes[item.Guid] = item;
            base.Add(item, 0);
            item.Parent = this.parent;
        }

        public new void Add(T item, ulong sortingWeight)
        {
            Debug.Assert(this.cfg != null);
            item.Parent = this.parent;
            if (isUseDicNodes && this.cfg.IsInitialized)
                this.cfg._DicNodes[item.Guid] = item;
            base.Add(item, sortingWeight);
        }

        public new bool Remove(T item)
        {
            Debug.Assert(this.cfg != null);
            if (isUseDicNodes)
            {
                this.cfg._DicNodes.Remove(item.Guid);
            }
            int indx = -1;
            foreach (var t in this)
            {
                indx++;
                if (t.Guid == item.Guid)
                {
                    base.RemoveAt(indx);
                    return true;
                }
            }
            return false;
        }
    }
}
