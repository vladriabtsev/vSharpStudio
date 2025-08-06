using System.Collections.Generic;

namespace vSharpStudio.common
{
    public partial interface IDocumentTimeline : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        IGroupDocuments ParentGroupDocumentsI { get; }
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial);
        int IndexOf(IProperty reg);
        string FullName { get; } // name with config name
    }
}
