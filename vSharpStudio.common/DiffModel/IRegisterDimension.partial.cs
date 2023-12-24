using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IRegisterDimension : ITreeConfigNodeSortable, IGetNodeSetting, ISortingValue
    {
        IGroupListRegisterDimensions ParentGroupListRegisterDimensionsI { get; }
    }
}
