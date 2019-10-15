using System;
using System.Collections.Generic;
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
                p = p.Parent;
            //if (p is Config)
            cfg = (Config)p;
        }

        public new void AddRange(IEnumerable<T> collection, ulong sortingWeight = 0)
        {
            if (cfg == null)
                GetConfig(parent);
            foreach (T item in collection)
            {
                item.Parent = parent;
                cfg.DicNodes[item.Guid] = item;
            }
            base.AddRange(collection, sortingWeight);
        }
        public new void Add(T item)
        {
            if (cfg == null)
                GetConfig(parent);
            item.Parent = parent;
            cfg.DicNodes[item.Guid] = item;
            base.Add(item, 0);
        }
        public new void Add(T item, ulong sortingWeight)
        {
            if (cfg == null)
                GetConfig(parent);
            item.Parent = parent;
            cfg.DicNodes[item.Guid] = item;
            base.Add(item, sortingWeight);
        }
        public new bool Remove(T item)
        {
            cfg.DicNodes.Remove(item.Guid);
            return base.Remove(item);
        }
    }
}
