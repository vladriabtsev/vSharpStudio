using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public interface IConfigObject : ISortingValue
    {
        string Guid { get; }
        string Name { get; set; }
        //void Create();
    }
}
