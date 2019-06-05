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
        public PluginGeneratorSettings(IvPluginGeneratorSettingsVM settingsVM) : this()
        {
            this.VM = settingsVM;
        }
        partial void OnInit()
        {
        }
        public void SetGuid(string guid)
        {
            this.Guid = guid;
        }
        [ExpandableObjectAttribute()]
        public IvPluginGeneratorSettingsVM VM { get; private set; }
        public void SetVM(IvPluginGeneratorSettingsVM vm)
        {
            this.VM = vm;
        }
        public void RemoveNode()
        {
            (this.Parent as PluginGenerator).ListSettings.Remove(this);
            this.Parent = null;
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as PluginGenerator).ListSettings.CanUp(this))
                    return true;
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (PluginGeneratorSettings)(this.Parent as PluginGenerator).ListSettings.GetPrev(this);
            if (prev == null)
                return;
            SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            (this.Parent as PluginGenerator).ListSettings.MoveUp(this);
            SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as PluginGenerator).ListSettings.CanDown(this))
                    return true;
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (PluginGeneratorSettings)(this.Parent as PluginGenerator).ListSettings.GetNext(this);
            if (next == null)
                return;
            SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            (this.Parent as PluginGenerator).ListSettings.MoveDown(this);
            SetSelected(this);
        }
        public override void NodeRemove()
        {
            (this.Parent as PluginGenerator).ListSettings.Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = PluginGeneratorSettings.Clone(this, true, true);
            node.Parent = this.Parent;
            (this.Parent as PluginGenerator).ListSettings.Add(node);
            this.Name = this.Name + "2";
            SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = (this.Parent as PluginGenerator).NodeAddNewSubNode();
            return node;
        }
        #endregion Tree operations
    }
}
