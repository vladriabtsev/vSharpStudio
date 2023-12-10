using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IRegister : ITreeConfigNodeSortable, IGetNodeSetting, ISortingValue, IItemWithSubItems
    {
        IGroupListRegisters ParentGroupListRegistersI { get; }
        string FullName { get; } // name with config name
        string GetDebuggerDisplayTurnover(bool isOptimistic);
        string GetDebuggerDisplayBalance(bool isOptimistic);
        IReadOnlyList<IProperty> GetIncludedTurnoverProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial);
        IReadOnlyList<IProperty> GetIncludedBalanceProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial);
    }
}
