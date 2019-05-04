using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public interface ITreeConfigNode : IParent, IValidatableWithSeverity, ISortingValue
    {
        string Guid { get; }
        string Name { get; set; }
        string NodeText { get; }
        bool IsSelected { get; set; }
        bool IsExpanded { get; set; }
        //ITreeConfigNode Parent { get; set; }
        void Sort(Type type);
        bool NodeCanMoveUp();
        void NodeMoveUp();
        bool NodeCanMoveDown();
        void NodeMoveDown();
        bool NodeCanAddNew();
        ITreeConfigNode NodeAddNew();
        bool NodeCanAddNewSubNode();
        ITreeConfigNode NodeAddNewSubNode();
        // Clone selected. Name is same + suffix 'New'
        bool NodeCanAddClone();
        ITreeConfigNode NodeAddClone();
        bool NodeCanRemove();
        void NodeRemove();
        bool NodeCanLeft();
        void NodeLeft();
        bool NodeCanRight();
        void NodeRight();
        bool NodeCanUp();
        void NodeUp();
        bool NodeCanDown();
        void NodeDown();
    }
}
