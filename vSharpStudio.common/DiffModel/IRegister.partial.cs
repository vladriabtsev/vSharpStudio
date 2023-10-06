using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IRegister : ITreeConfigNodeSortable, IGetNodeSetting, ISortingValue, IDbTable
    {
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic);
        IRegisterDimention AddDimention(string name, ICatalog c);
        string FullName { get; } // name with config name
    }
}
