using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IDocument : ITreeConfigNode, IGetNodeSetting, IDbTable
    {
        IGroupListDocuments IParent { get; }
        IReadOnlyList<IProperty> GetAllProperties(bool isSupportVersion);
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isSupportVersion);
        IReadOnlyList<IProperty> GetIncludedPropertiesWithShared(string guidAppPrjGen, bool isSupportVersion);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen);
    }
}
