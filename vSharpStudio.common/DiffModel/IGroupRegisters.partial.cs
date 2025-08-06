using System.Collections.Generic;

namespace vSharpStudio.common
{
    public partial interface IGroupRegisters : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        IGroupDocuments ParentGroupDocumentsI { get; }
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial);
        IReadOnlyList<IRegister> GetIncludedRegisters(string guidAppPrjGen);
        int IndexOf(IRegister reg);
        string FullName { get; } // name with config name
    }
}
