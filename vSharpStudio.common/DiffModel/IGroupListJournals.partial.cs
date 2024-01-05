using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IGroupListJournals : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        IGroupDocuments ParentGroupDocumentsI { get; }
        string GetDebuggerDisplay(bool isOptimistic);
    }
}
