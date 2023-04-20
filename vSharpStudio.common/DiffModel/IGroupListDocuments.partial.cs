using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IGroupListDocuments : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IGroupDocuments ParentGroupDocumentsI { get; }
        int IndexOf(IDocument doc);
        EnumDocumentAccess GetRoleDocumentAccess(string roleGuid);
        EnumPrintAccess GetRoleDocumentPrint(string roleGuid);
    }
}
