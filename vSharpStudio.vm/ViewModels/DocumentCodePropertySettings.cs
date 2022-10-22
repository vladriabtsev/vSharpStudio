using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DocumentCodePropertySettings : IParent
    {
        partial void OnCreated()
        {
            this.UniqueScope = common.EnumDocumentCodeUniqueScope.Year;
            this.Type = common.EnumCodeType.Number;
            this.Length = 5;
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
    }
}
