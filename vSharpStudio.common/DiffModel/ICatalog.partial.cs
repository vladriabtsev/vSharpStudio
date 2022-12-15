using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface ICatalog : ITreeConfigNodeSortable, IGetNodeSetting, ISortingValue, IDbTable
    {
        IGroupListCatalogs ParentGroupListCatalogsI { get; }
        //IReadOnlyList<IProperty> GetIncludedViewProperties(string guidAppPrjDbGen);
        //IReadOnlyList<IProperty> GetIncludedFolderViewProperties(string guidAppPrjDbGen);
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isSupportVersion);
        IReadOnlyList<IProperty> GetIncludedFolderProperties(string guidAppPrjDbGen, bool isSupportVersion);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjDbGen);
        IReadOnlyList<IDetail> GetIncludedFolderDetails(string guidAppPrjDbGen);
        ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen);
        IForm GetForm(FormType ftype);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
    }
}
