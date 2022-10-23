using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IGroupListDocuments : ITreeConfigNode, IGetNodeSetting
    {
        IGroupDocuments ParentGroupDocumentsI { get; }
        int IndexOf(IDocument doc);
    }
}
