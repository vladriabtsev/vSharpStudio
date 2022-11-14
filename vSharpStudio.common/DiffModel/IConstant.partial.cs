using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IConstant : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName, ILayoutFieldParameters
    {
        IGroupListConstants ParentGroupListConstantsI { get; }
        //string DefaultValue { get; }
        object? Tag { get; set; }
        //static IConfig Config { get; set; }
    }
}
