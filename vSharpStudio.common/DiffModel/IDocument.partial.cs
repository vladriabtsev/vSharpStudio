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
        IReadOnlyList<IProperty> GetAllProperties();
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetIncludedPropertiesWithShared(string guidAppPrjGen);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen);
    }
}
