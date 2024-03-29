using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IDocumentTimeline : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        IGroupDocuments ParentGroupDocumentsI { get; }
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial);
        int IndexOf(IProperty reg);
        string FullName { get; } // name with config name
    }
}
