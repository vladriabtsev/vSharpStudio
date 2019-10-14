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
        private ITreeConfigNode parent;
        public ConfigNodesCollection(ITreeConfigNode parent)
        {
            this.parent = parent;
        }
        public new void Add(T item)
        {
            item.Parent = parent;
            this.Add(item, 0);
        }
    }
}
