using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IRegister : ITreeConfigNodeSortable, IGetNodeSetting, ISortingValue, IItemWithSubItems
    {
        IGroupListRegisters ParentGroupListRegistersI { get; }
        IRegisterDimension AddDimension(string name, ICatalog c);
        IProperty AddAttachedProperty(string name, EnumDataType type = EnumDataType.STRING, uint length = 0, uint accuracy = 0, string? guid = null);
        string FullName { get; } // name with config name
    }
}
