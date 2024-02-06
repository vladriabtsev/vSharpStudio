using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IRelationsManyToManyGroup : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IRelationsGroup ParentGroupRelationsI { get; }
        int IndexOf(IRelationManyToMany rel);
    }
}
