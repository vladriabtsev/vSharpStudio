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
            this.SequenceType = common.EnumCodeType.AutoText;
            this.MaxSequenceLength = 5;
            this.Prefix = "";
            this.UniqueScope = common.EnumCatalogCodeUniqueScope.code_unique_in_whole_catalog;
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
