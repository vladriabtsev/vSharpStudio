using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.vm.ViewModels
{
    public partial interface INodeWithProperties
    {
        GroupListProperties GroupProperties { get; }
        GroupListPropertiesTabs GroupPropertiesTabs { get; }
    }
}
