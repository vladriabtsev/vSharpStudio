using System.Collections.Generic;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IRegister : ITreeConfigNodeSortable, IGetNodeSetting, ISortingValue, ICompositeName, IPKey, INodeWithPositionProperties
    {
        IGroupListRegisters ParentGroupListRegistersI { get; }
        string FullName { get; } // name with config name
        string GetDebuggerDisplayTurnover(bool isOptimistic);
        string GetDebuggerDisplayBalance(bool isOptimistic);
        //IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial);
        IReadOnlyList<IProperty> GetIncludedTurnoverProperties(bool isOptimistic, bool isExcludeSpecial);
        IReadOnlyList<IProperty> GetIncludedBalanceProperties(bool isOptimistic, bool isExcludeSpecial);
        //IForm GetForm(FormType ftype, string guidAppPrjGen);
        string GetDebuggerDisplay(bool isOptimistic);
        IProperty PropertyMoneyAccumulator { get; }
        IProperty PropertyQtyAccumulator { get; }
    }
}
