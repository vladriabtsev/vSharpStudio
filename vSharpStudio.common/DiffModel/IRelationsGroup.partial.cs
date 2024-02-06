using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IRelationsGroup : ITreeConfigNodeSortable, IGetNodeSetting
    {
        //IGroupRelations ParentGroupRelationsI { get; }
        //int IndexOf(IManyToManyCatalogsRelation rel);
    }
}
