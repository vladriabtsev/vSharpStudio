using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("PluginGenerator:{Name,nq}")]
    public partial class PluginGenerator : ICanGoLeft, ICanGoRight, ICanAddSubNode
    {
        public PluginGenerator(IvPluginCodeGenerator plugin) : this()
        {
            this.Guid = plugin.Guid.ToString();
            this.Name = plugin.Name;
            this.Description = plugin.Description;
            this.Generator = plugin;
            this.IsEditable = false;
        }
        partial void OnInit()
        {
            this.IsEditable = false;
        }

        [BrowsableAttribute(false)]
        public IvPluginCodeGenerator Generator { get; private set; }
        public void SetGenerator(IvPluginCodeGenerator generator)
        {
            this.Generator = generator;
        }

        #region IConfigObject
        //public void Create()
        //{
        //    GroupListConstants vm = (GroupListConstants)this.Parent;
        //    int icurr = vm.Children.IndexOf(this);
        //    vm.Children.Add(new Constant() { Parent = this.Parent });
        //}
        #endregion IConfigObject
    }
}
