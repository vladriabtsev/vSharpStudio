using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ConfigNodesCollection<T> : SortedObservableCollection<T>
      where T : ITreeConfigNode
    {
        private Config cfg = null;
        private ITreeConfigNode parent = null;

        public ConfigNodesCollection(ITreeConfigNode parent)
        {
            this.parent = parent;
        }

        private void GetConfig(ITreeConfigNode parent)
        {
            ITreeConfigNode p = parent;
            while (p.Parent != null)
            {
                p = p.Parent;
            }
            // if (p is Config)
            this.cfg = (Config)p;
        }

        public new void AddRange(IEnumerable<T> collection, ulong sortingWeight = 0)
        {
            Contract.Requires(collection != null);
            if (this.cfg == null)
            {
                this.GetConfig(this.parent);
            }

            foreach (T item in collection)
            {
                item.Parent = this.parent;
                this.cfg.DicNodes[item.Guid] = item;
            }
            base.AddRange(collection, sortingWeight);
        }

        public new void Add(T item)
        {
            if (this.cfg == null)
            {
                this.GetConfig(this.parent);
            }

            item.Parent = this.parent;
            this.cfg.DicNodes[item.Guid] = item;
            base.Add(item, 0);
        }

        public new void Add(T item, ulong sortingWeight)
        {
            if (this.cfg == null)
            {
                this.GetConfig(this.parent);
            }

            item.Parent = this.parent;
            this.cfg.DicNodes[item.Guid] = item;
            base.Add(item, sortingWeight);
        }

        public new bool Remove(T item)
        {
            this.cfg.DicNodes.Remove(item.Guid);
            int indx = -1;
            foreach(var t in this)
            {
                indx++;
                if (t.Guid==item.Guid)
                {
                    base.RemoveAt(indx);
                    return true;
                }
            }
            return false;
        }
    }
}
