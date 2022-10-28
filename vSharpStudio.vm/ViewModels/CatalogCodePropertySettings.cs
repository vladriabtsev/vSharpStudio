using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class CatalogCodePropertySettings : IParent
    {
        partial void OnCreated()
        {
            this.UniqueScope = common.EnumCatalogCodeUniqueScope.Catalog;
            this.Type = common.EnumCodeType.Number;
            this.Length = 5;
        }
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
    }
}
