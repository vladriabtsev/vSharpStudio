using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IDocument : ITreeConfigNodeSortable, IGetNodeSetting, IDbTable
    {
        IGroupListDocuments ParentGroupListDocumentsI { get; }
        IReadOnlyList<IProperty> GetAllProperties(bool isSupportVersion);
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isSupportVersion);
        IReadOnlyList<IProperty> GetIncludedPropertiesWithShared(string guidAppPrjGen, bool isSupportVersion);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen);
        ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen);
        IForm GetForm(FormType ftype);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
    }
}
