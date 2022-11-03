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
            //Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListRoles.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListRoles.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListRoles.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListRoles.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
    }
}
