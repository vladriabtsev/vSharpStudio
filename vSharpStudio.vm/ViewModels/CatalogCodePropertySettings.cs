using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public partial class CatalogCodePropertySettings
    {
        partial void OnInit()
        {
            this.UniqueScope = common.EnumCatalogCodeUniqueScope.Catalog;
            this.Type = common.EnumCodeType.Number;
            this.Length = 5;
        }
        [BrowsableAttribute(false)]
        public Catalog Parent { get; set; }
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
    }
}
