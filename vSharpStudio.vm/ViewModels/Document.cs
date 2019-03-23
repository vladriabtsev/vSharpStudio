using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
  public partial class Document : EntityObjectBase<Document, Document.DocumentValidator>, IEntityObject
    {
    partial void OnInit()
    {
      this.Guid = System.Guid.NewGuid().ToString();
    }
  }
}
