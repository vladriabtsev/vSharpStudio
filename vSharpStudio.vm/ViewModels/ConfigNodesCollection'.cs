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
    public class ConfigNodesCollection<T> : SortedObservableCollection<T>, System.Collections.IList
      where T : ITreeConfigNode, ISortingValue
    {
        private Config cfg;
        private ITreeConfigNode parent;
        private bool isUseDicNodes = false;

        public ConfigNodesCollection(ITreeConfigNode parent)
        {
            this.isUseDicNodes = !(typeof(T).Name == typeof(PluginGeneratorNodeSettings).Name);
            this.parent = parent;
        }

        private void GetConfig(ITreeConfigNode parent)
        {
            ITreeConfigNode p = parent;
            while (p.Parent != null)
            {
                p = p.Parent;
            }
            if (!(p is Config))
                throw new Exception();
            this.cfg = (Config)p;
        }

        public new void AddRange(IEnumerable<T> collection, ulong sortingWeight = 0)
        {
            Debug.Assert(collection != null);
            if (this.cfg == null)
            {
                this.GetConfig(this.parent);
            }
            Debug.Assert(this.cfg != null);
            foreach (T item in collection)
            {
                item.Parent = this.parent;
                if (isUseDicNodes && this.cfg.IsInitialized)
                    this.cfg._DicNodes[item.Guid] = item;
            }
            base.AddRange(collection, sortingWeight);
        }

        public new void Add(T item)
        {
            if (this.cfg == null)
            {
                this.GetConfig(this.parent);
            }
            Debug.Assert(this.cfg != null);
            if (isUseDicNodes && this.cfg.IsInitialized)
                this.cfg._DicNodes[item.Guid] = item;
            base.Add(item, 0);
            item.Parent = this.parent;
        }

        public new void Add(T item, ulong sortingWeight)
        {
            if (this.cfg == null)
            {
                this.GetConfig(this.parent);
            }
            item.Parent = this.parent;
            Debug.Assert(this.cfg != null);
            if (isUseDicNodes && this.cfg.IsInitialized)
                this.cfg._DicNodes[item.Guid] = item;
            base.Add(item, sortingWeight);
        }

        public new bool Remove(T item)
        {
            if (isUseDicNodes)
                this.cfg._DicNodes.Remove(item.Guid);
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
