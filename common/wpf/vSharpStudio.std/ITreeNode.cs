using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelBase
{
    public interface ITreeNode2 : IValidatableWithSeverity
    {
        ITreeNode2 Parent { get; }
        string NodeText { get; }
        bool IsSelected { get; set; }
        bool IsExpanded { get; set; }
        IEnumerable<ITreeNode2> SubNodes { get; }
    }
}
