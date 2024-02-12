using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.common
{
    public partial interface IDocument : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        IGroupListDocuments ParentGroupListDocumentsI { get; }
        bool IsDocWithSharedProperties { get; }
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
        IDocumentEnumeratorSequence? Sequence { get; }

        IForm GetForm(FormType ftype, string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false);
        
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
    }
}
