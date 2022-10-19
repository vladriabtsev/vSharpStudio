using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface ICatalog : ITreeConfigNode, IGetNodeSetting, IDbTable
    {
        IReadOnlyList<IProperty> GetIncludedViewProperties(string guidAppPrjDbGen);
        IReadOnlyList<IProperty> GetIncludedFolderViewProperties(string guidAppPrjDbGen);
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isSupportVersion);
        IReadOnlyList<IProperty> GetIncludedFolderProperties(string guidAppPrjDbGen, bool isSupportVersion);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjDbGen);
        IReadOnlyList<IDetail> GetIncludedFolderDetails(string guidAppPrjDbGen);
        ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen);
    }
}
