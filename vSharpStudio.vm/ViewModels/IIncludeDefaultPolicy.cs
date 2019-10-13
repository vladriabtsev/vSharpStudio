using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public interface IIncludeDefaultPolicy
    {
        EnumIncludeDefaultPolicy EnumDefaultInclusion { get; }
    }
}
