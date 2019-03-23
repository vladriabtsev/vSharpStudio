using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
  public partial class Constant : EntityObjectBase<Constant, Constant.ConstantValidator>, IEntityObject
    {
    partial void OnInit()
    {
      this.Guid = System.Guid.NewGuid().ToString();
    }
  }
}
