﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("NodeSettings:{Name,nq} Path:{ModelPath,nq} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class PluginGeneratorNodeSettings : ISortingValue, ITreeConfigNode
    {
        public IvPluginGeneratorNodeSettings? SettingsVm { get; set; }
        //public PluginGeneratorNodeSettings(ITreeConfigNode parent, string appProjectGeneratorGuid, IvPluginGeneratorNodeSettings t) : this(parent)
        //{
        //    Debug.Assert(t != null);
        //    this.Name = t.Name;
        //    this.NodeSettingsVmGuid = t.Guid;
        //    this.AppProjectGeneratorGuid = appProjectGeneratorGuid;
        //    this.SettingsVm = t.GetAppGenerationNodeSettingsVm(this.Settings);
        //    //this.SettingsVm = t.GetAppGenerationNodeSettingsVm(this.Settings, true);
        //    //var cfg = this.GetConfig();
        //    //cfg.Model.DicGenNodeSettings[this.NodeSettingsVmGuid] = this.SettingsVm;
        //}
        partial void OnIsNewChanged();
        partial void OnIsHasNewChanged();
        partial void OnIsMarkedForDeletionChanged();
        partial void OnIsHasMarkedForDeletionChanged();
    }
}
