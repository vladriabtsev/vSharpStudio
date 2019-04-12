using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public interface ITreeConfigNode : IValidatableWithSeverity
    {
        string Guid { get; }
        string Name { get; set; }

        ITreeConfigNode Parent { get; }
        string NodeText { get; }
        bool IsSelected { get; set; }
        bool IsExpanded { get; set; }
        IEnumerable<ITreeConfigNode> SubNodes { get; }
        //void Sort();
        //bool CanUp();
        //void Up();
        //bool CanDown();
        //void Down();
        //bool CanAdd();
        //void AddNew();
        //// Clone selected. Name is same + suffix 'New'
        //void AddClone(); 
        //bool CanRemove();
        //void Remove();
        //bool CanLeft();
        //void Left();
        //bool CanRight();
        //void Right();
    }
}
