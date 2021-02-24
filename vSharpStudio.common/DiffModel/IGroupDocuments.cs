using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IGroupDocuments : ITreeConfigNode
    {
        IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen);
    }
}
