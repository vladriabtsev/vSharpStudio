using System.Collections.Generic;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IPKey
    {
        IProperty? GetDateTimeUtcProperty(bool? isRegisterBalance = null);
        IReadOnlyList<IProperty> GetListIdPKeyProperties(bool? isRegisterBalance = null);
    }
}
