using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public interface IEntityObject : IGuid, IComparable
    {
        string Name { get; set; }
    }
}
