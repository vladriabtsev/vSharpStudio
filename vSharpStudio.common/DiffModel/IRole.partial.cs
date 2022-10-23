using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IRole : ITreeConfigNode
    {
        IGroupListRoles ParentGroupListRolesI { get; }
    }
}
