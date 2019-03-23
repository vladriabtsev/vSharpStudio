using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Catalog : EntityObjectBase<Catalog, Catalog.CatalogValidator>, IEntityObject
    {
        partial void OnInit()
        {
            this.Guid = System.Guid.NewGuid().ToString();
        }
        public Catalog(string name) : this()
        {
            this.Name = name;
        }
        public Catalog(string name, List<Property> listProperties ) : this()
        {
            this.Name = name;
            foreach (var t in listProperties)
            {
                this.Properties.ListProperties.Add(t);
            }
        }
    }
}
