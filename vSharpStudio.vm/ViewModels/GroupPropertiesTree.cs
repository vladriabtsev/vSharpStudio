using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{ListProperties.Count,nq} sub:{ListSubPropertiesGroups.Count,nq}")]
    public partial class GroupPropertiesTree : IChildren, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        #region ITreeConfigNode


        #endregion ITreeConfigNode
    }
}
