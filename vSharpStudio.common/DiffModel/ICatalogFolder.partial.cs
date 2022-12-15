using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface ICatalogFolder : ITreeConfigNodeSortable, IGetNodeSetting, IDbTable
    {
        ICatalog ParentCatalogI { get; }
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        IForm GetForm(FormType ftype);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
    }
}
