using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.common
{
    public partial interface IDocument : ITreeConfigNodeSortable, IGetNodeSetting, IItemWithDetails
    {
        IGroupListDocuments ParentGroupListDocumentsI { get; }
        IReadOnlyList<IProperty> GetProperties();
        IReadOnlyList<IProperty> GetAllProperties(bool isOptimistic);
        IReadOnlyList<IProperty> GetIncludedPropertiesWithShared(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false);
        IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen);
        bool IsDocWithSharedProperties { get; }
        void GetSpecialProperties(List<IProperty> res, bool isOptimistic);
        ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        EnumPropertyAccess GetRolePropertyAccess(IRole role);
        EnumPrintAccess GetRolePropertyPrint(IRole role);
        EnumDocumentAccess GetRoleDocumentAccess(IRole role);
        EnumPrintAccess GetRoleDocumentPrint(IRole role);
        IReadOnlyList<string> GetRolesByAccess(EnumDocumentAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        string FullName { get; } // name with config name
        string GetDebuggerDisplay(bool isOptimistic);
        IDocumentEnumeratorSequence Sequence { get; }
    }
}
