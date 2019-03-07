using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
  public interface IEntityObject
  {
    string Guid { get; }
    string Name { get; set; }
  }
}
