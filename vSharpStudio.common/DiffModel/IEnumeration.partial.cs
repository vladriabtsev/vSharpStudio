using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IEnumeration : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IGroupListEnumerations ParentGroupListEnumerationsI { get; }
        string GetClrBase();
        string DefaultValue { get; }
    }
}
