using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IGroupRegisters : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial);
        IReadOnlyList<IRegister> GetIncludedRegisters(string guidAppPrjGen);
        int IndexOf(IRegister reg);
        string FullName { get; } // name with config name
    }
}
