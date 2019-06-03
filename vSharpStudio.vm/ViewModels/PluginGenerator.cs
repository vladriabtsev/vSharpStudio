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

        #region Tree operations
        public override ITreeConfigNode NodeAddNewSubNode()
        {
            PluginGeneratorSettings pgs = null;
            switch (this.Generator.PluginType)
            {
                case vPluginTypeEnum.DbDesign:
                    var settings = this.Generator.GetSettingsMvvm(null);
                    pgs = new PluginGeneratorSettings(settings);
                    pgs.SetGuid(this.Generator.Guid.ToString());
                    GetUniqueName(this.Generator.DefaultSettingsName, pgs, this.ListSettings);
                    this.ListSettings.Add(pgs);
                    SetSelected(pgs);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return pgs;
        }
        #endregion Tree operations
    }
}
