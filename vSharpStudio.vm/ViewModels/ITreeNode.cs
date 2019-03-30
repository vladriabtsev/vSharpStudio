using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public interface ITreeNode : IValidatableWithSeverity
    {
        ITreeNode Parent { get; }
        IEnumerable<ITreeNode> SubNodes { get; }
        //string Guid { get; }
        string Name { get; set; }
    }
}
