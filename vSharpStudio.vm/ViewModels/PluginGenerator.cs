using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("PluginGenerator:{Name,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class PluginGenerator : ICanGoLeft, ICanGoRight
    {
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Plugin ParentPlugin { get { Debug.Assert(this.Parent != null); return (Plugin)this.Parent; } }
        [Browsable(false)]
        public IPlugin ParentPluginI { get { Debug.Assert(this.Parent != null); return (IPlugin)this.Parent; } }
        public PluginGenerator(ITreeConfigNode parent, IvPluginGenerator plugin)
            : this(parent)
        {
            Debug.Assert(plugin != null);
            this.Guid = plugin.Guid.ToString();
            this._Name = plugin.Name;
            this.Description = plugin.Description;
            this.Generator = plugin;
            this.Generator.Parent = this;
            this.IsEditable = false;
        }
        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            this.IsEditable = false;
            //    Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListMainViewForms.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListMainViewForms.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListMainViewForms.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListMainViewForms.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}

        [Browsable(false)]
        public IvPluginGenerator? Generator { get; private set; }

        public void SetGenerator(IvPluginGenerator generator)
        {
            Debug.Assert(generator != null);
            this.Generator = generator;
            this.Generator.Parent = this;
        }

        #region Tree operations
        //public bool CanAddSubNode() { return false; }
        //public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        //{
        //    PluginGeneratorSettings pgs = null;
        //    // switch (this.Generator.PluginGeneratorType)
        //    // {
        //    //    case vPluginLayerTypeEnum.DbDesign:
        //    var settings = this.Generator.GetAppGenerationSettingsVmFromJson(null);
        //    pgs = new PluginGeneratorSettings(this, settings);
        //    pgs.SetGuid(this.Generator.Guid.ToString());
        //    this.GetUniqueName(this.Generator.DefaultSettingsName, pgs, this.ListSettings);
        //    pgs.Parent = this;
        //    this.ListSettings.Add(pgs);
        //    this.SetSelected(pgs);
        //    // break;
        //    //    default:
        //    //        throw new NotImplementedException();
        //    // }
        //    return pgs;
        //}

        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                this.GetPropertyName(() => this.Parent)
            };
            return lst.ToArray();
        }
    }
}
