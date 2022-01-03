using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface ICatalog : ITreeConfigNode, IGetNodeSetting, IDbTable
    {
        IReadOnlyList<IProperty> GetAllProperties();
        IReadOnlyList<IProperty> GetAllFolderProperties();
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen);
        IReadOnlyList<IProperty> GetIncludedFolderProperties(string guidAppPrjGen);
        IReadOnlyList<IPropertiesTab> GetIncludedPropertiesTabs(string guidAppPrjGen);
        IReadOnlyList<IPropertiesTab> GetIncludedFolderPropertiesTabs(string guidAppPrjGen);
    }
}
