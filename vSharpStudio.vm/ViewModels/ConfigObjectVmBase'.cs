using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public class ConfigObjectVmBase<T, TValidator> : ConfigObjectCommonBase<T, TValidator>
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectVmBase<T, TValidator>//, IComparable<T>, ISortingValue 
    {
        public ConfigObjectVmBase(ITreeConfigNode parent, TValidator validator)
            : base(parent, validator)
        {
        }
        //protected virtual void OnNodeAdded(ITreeConfigNode node) { }
        //protected void OnAddRemoveNode(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    var cfg = this.GetConfig();
        //    switch (e.Action)
        //    {
        //        case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
        //            cfg.DicNodes[this.Guid] = this;
        //            this.OnNodeAdded(this);
        //            break;
        //        case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
        //            cfg.DicNodes.Remove(this.Guid);
        //            break;
        //    }
        //}
    }
}
