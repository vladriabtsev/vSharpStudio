﻿using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
  public partial class Enumeration : EntityObjectBaseWithGuid<Enumeration, Enumeration.EnumerationValidator>, IEntityObject
    {
    partial void OnInit()
    {
      this.Guid = System.Guid.NewGuid().ToString();
    }
  }
}
