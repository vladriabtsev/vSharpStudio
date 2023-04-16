using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IDetail : ITreeConfigNodeSortable, IGetNodeSetting, IDbTable, ILayoutParameters
    {
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isSupportVersion, bool isExcludeSpecial = false);
        //IReadOnlyList<IProperty> GetIncludedViewProperties(string guidAppPrjDbGen);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        void GetSpecialProperties(List<IProperty> res, bool isSupportVersion);
        ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen);
        IForm GetForm(FormType ftype, string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        IGroupListDetails ParentGroupListDetailsI { get; }

        bool GetUseCodeProperty();
        bool GetUseNameProperty();
        bool GetUseDescriptionProperty();
    }
}
