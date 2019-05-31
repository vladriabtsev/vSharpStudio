using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("PluginGeneratorSettings:{Name,nq}")]
    public partial class PluginGeneratorSettings : ICanGoLeft, ICanAddNode, ICanRemoveNode
    {
        public PluginGeneratorSettings(IvPluginSettingsVM settingsVM) : this()
        {
            this.VM = settingsVM;
        }
        partial void OnInit()
        {
        }
        [ExpandableObjectAttribute()]
        public IvPluginSettingsVM VM { get; private set; }
        public void SetVM(IvPluginSettingsVM vm)
        {
            this.VM = vm;
        }

        public void RemoveNode()
        {
            (this.Parent as PluginGenerator).ListPluginGeneratorSettings.Remove(this);
            this.Parent = null;
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
