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
    [DebuggerDisplay("PluginGenerator:{Name,nq}")]
    public partial class PluginGenerator : ICanGoLeft, ICanGoRight, INewAndDeleteion
    {
        public PluginGenerator(ITreeConfigNode parent, IvPluginGenerator plugin)
            : this(parent)
        {
            Contract.Requires(plugin != null);
            this.Guid = plugin.Guid.ToString();
            this.Name = plugin.Name;
            this.Description = plugin.Description;
            this.Generator = plugin;
            this.Generator.Parent = this;
            this.IsEditable = false;
        }

        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnInit()
        {
            this.IsEditable = false;
        }

        [BrowsableAttribute(false)]
        public IvPluginGenerator Generator { get; private set; }

        public void SetGenerator(IvPluginGenerator generator)
        {
            Contract.Requires(generator != null);
            this.Generator = generator;
            this.Generator.Parent = this;
        }

        #region Tree operations
        //public bool CanAddSubNode() { return false; }
        //public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
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

        public override bool NodeCanRemove()
        {
            return this.Generator == null || string.IsNullOrWhiteSpace(this.Guid);
        }

        public override void NodeRemove(bool ask = true)
        {
            (this.Parent as Plugin).ListGenerators.Remove(this);
        }
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        partial void OnIsMarkedForDeletionChanged()
        {
            if (this.IsMarkedForDeletion)
            {
                (this.Parent as INewAndDeleteion).IsMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as Plugin);
                bool isMarked = false;
                foreach (var t in p.ListGenerators)
                {
                    if (t.IsMarkedForDeletion)
                    {
                        isMarked = true;
                        break;
                    }
                }
                p.IsMarkedForDeletion = isMarked;
            }
        }
        partial void OnIsNewChanged()
        {
            if (this.IsNew)
            {
                (this.Parent as INewAndDeleteion).IsNew = true;
            }
            else
            {
                var p = (this.Parent as Plugin);
                bool isNew = false;
                foreach (var t in p.ListGenerators)
                {
                    if (t.IsNew)
                    {
                        isNew = true;
                        break;
                    }
                }
                p.IsNew = isNew;
            }
        }
        #endregion Tree operations

    }
}
