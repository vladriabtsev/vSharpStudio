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
        void GetSpecialProperties(List<IProperty> res, bool isSupportVersion);
        ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen);
        IForm GetForm(FormType ftype, string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();

        bool GetUseCodeProperty();
        bool GetUseCodePropertySeparateFolder();
        bool GetUseNameProperty();
        bool GetUseNamePropertySeparateFolder();
        bool GetUseDescriptionProperty();
        bool GetUseDescriptionPropertSeparateFoldery();
    }
}
