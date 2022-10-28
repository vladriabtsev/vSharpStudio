using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IDetail : ITreeConfigNode, IGetNodeSetting, IDbTable
    {
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isSupportVersion);
        IReadOnlyList<IProperty> GetIncludedViewProperties(string guidAppPrjDbGen);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
    }
}
