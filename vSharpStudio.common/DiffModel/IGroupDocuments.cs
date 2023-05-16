using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IGroupDocuments : ITreeConfigNodeSortable
    {
        IModel ParentModelI { get; }
        IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        EnumDocumentAccess GetRoleDocumentAccess(IRole role);
        EnumPrintAccess GetRoleDocumentPrint(IRole role);
    }
}
