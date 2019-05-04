using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public interface IParent
    {
        ITreeConfigNode Parent { get; set; }
    }
    public interface IChildren
    {
        SortedObservableCollection<ITreeConfigNode> Children { get; }
    }
}
