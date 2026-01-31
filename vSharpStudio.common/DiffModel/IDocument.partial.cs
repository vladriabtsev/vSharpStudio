using System.Collections.Generic;

namespace vSharpStudio.common
{
    public partial interface IDocument : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName, IPKey
    {
        IGroupListDocuments ParentGroupListDocumentsI { get; }
        bool IsDocWithSharedProperties { get; }
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        EnumDocumentAccess GetRoleDocumentAccess(IRole role);
        EnumPrintAccess GetRoleDocumentPrint(IRole role);
        EnumPropertyAccess GetRolePropertyAccess(IRole role);
        EnumPrintAccess GetRolePropertyPrint(IRole role);
        IReadOnlyList<string> GetRolesByAccess(EnumDocumentAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        string FullName { get; } // name with config name
        string GetDebuggerDisplay(bool isOptimistic);
        IDocumentEnumeratorSequence? Sequence { get; }

        IForm GetForm(FormType ftype, string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false, bool isOnlyShared = false, bool isOnlyNotShared = true);
        IProperty GetDocNumberProperty(List<IProperty> lst);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
    }
}
