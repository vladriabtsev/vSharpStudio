using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Windows;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigObjectVmBase<T, TValidator> : ConfigObjectCommonBase<T, TValidator>
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectVmBase<T, TValidator> //, IComparable<T>, ISortingValue 
    {
        public ConfigObjectVmBase(ITreeConfigNode? parent, TValidator validator)
            : base(parent, validator)
        {
        }
        //public object NodeIcon
        //{
        //    get
        //    {
        //        if (this.nodeIcon == null)
        //        {
        //            if (Application.Current.Resources.MergedDictionaries[0].Contains(this.GetNodeIconName()))
        //            {
        //                this.nodeIcon = Application.Current.Resources.MergedDictionaries[0][this.GetNodeIconName()];
        //            }
        //        }
        //        return this.nodeIcon;
        //    }
        //}
        //private object nodeIcon;
        //protected virtual string GetNodeIconName() { throw new Exception(); }

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
