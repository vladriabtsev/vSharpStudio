using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Catalog : IEntityObject
    {
        partial void OnInit()
        {
            this.Guid = System.Guid.NewGuid().ToString();
        }
        public Catalog(string name) : base(CatalogValidator.Validator)
        {
            this.Name = name;
        }
    }
}
