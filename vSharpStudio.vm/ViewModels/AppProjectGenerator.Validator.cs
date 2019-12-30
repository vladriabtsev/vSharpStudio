using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppProjectGenerator
    {
        public partial class AppProjectGeneratorValidator
        {
            public AppProjectGeneratorValidator()
            {
                this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
                this.RuleFor(x => x.Name).Must(Enumeration.EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
                this.RuleFor(x => x.Name).Must(Enumeration.EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
                this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            }

            private bool IsUnique(AppProjectGenerator val)
            {
                if (val.Parent == null)
                {
                    return true;
                }

                if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
                {
                    return true;
                }

                AppProject p = (AppProject)val.Parent;
                foreach (var t in p.ListAppProjectGenerators)
                {
                    if ((val.Guid != t.Guid) && (val.Name == t.Name))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public static readonly string DefaultName = "Generator";
        [ExpandableObjectAttribute()]
        public object Settings
        {
            get
            {
                return this._Settings;
            }
            set
            {
                if (this._Settings != value)
                {
                    this._Settings = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private object _Settings;
        public void CreateGenSettings()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
                {
                    this.GeneratorSettings = string.Empty;
                    return;
                }
                Config cnfg = (Config)this.GetConfig();
                PluginGenerator gen = (PluginGenerator)cnfg.DicNodes[this.PluginGeneratorGuid];
                this.Settings = gen?.Generator?.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        partial void OnPluginGuidChanged()
        {
            this.PluginGeneratorGuid = "";
            Config cnfg = (Config)this.GetConfig();
            Plugin plg = (Plugin)cnfg.DicNodes[this.PluginGuid];
            EditorPluginSelection.ListGenerators.Clear();
            EditorPluginSelection.ListGenerators.AddRange(plg.ListGenerators);
            this.DescriptionPlugin = plg.Description;

            if (!Config.IsLoading)
            {
                var nv = new ModelVisitorNodeGenSettings();
                nv.NodeGenSettingsApplyAction(this.GetConfig(), (p) =>
                {
                    p.RemoveNodeAppGenSettings(this.Guid);
                });
            }
        }
        partial void OnPluginGeneratorGuidChanged()
        {
            if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
            {
                this.GeneratorSettings = string.Empty;
                return;
            }
            Config cfg = (Config)this.GetConfig();
            PluginGenerator gen = (PluginGenerator)cfg.DicNodes[this.PluginGeneratorGuid];
            try
            {
                this.Settings = gen?.Generator?.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
                this.DescriptionGenerator = gen.Description;
                if (!Config.IsLoading)
                {
                    //DicAppGenerators[this.Guid] = cnfg.DicGenerators[this.PluginGeneratorGuid];
                    var nv = new ModelVisitorNodeGenSettings();
                    nv.NodeGenSettingsApplyAction(cfg, (p) =>
                    {
                        p.RemoveNodeAppGenSettings(this.Guid);
                    });
                    if (!cfg.DicAppGenerators.ContainsKey(this.Guid))
                    {
                        AppProjectGenerator g = (AppProjectGenerator)cfg.DicNodes[this.Guid];
                        cfg.DicAppGenerators[this.Guid] = cfg.DicGenerators[g.PluginGeneratorGuid];
                    }
                    //this.RefillDicGenerators();
                    nv.NodeGenSettingsApplyAction(cfg, (p) =>
                    {
                        p.AddNodeAppGenSettings(this.Guid);
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as AppProject).ListAppProjectGenerators.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (AppProject)(this.Parent as AppProject).ListAppProjectGenerators.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as AppProject).ListAppProjectGenerators.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as AppProject).ListAppProjectGenerators.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (AppProject)(this.Parent as AppProject).ListAppProjectGenerators.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as AppProject).ListAppProjectGenerators.MoveDown(this);
            this.SetSelected(this);
        }

        public override void NodeRemove()
        {
            var cfg = (Config)this.GetConfig();
            var nv = new ModelVisitorNodeGenSettings();
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.RemoveNodeAppGenSettings(this.Guid);
            });
            (this.Parent as AppProject).ListAppProjectGenerators.Remove(this);
            this.Parent = null;
            cfg.DicAppGenerators.Remove(this.Guid);
            //this.RefillDicGenerators();
            //    this.RemoveNodeAppGenSettings(item.Guid);
            //    var cfg = (Config)this.GetConfig();
            //    cfg.DicAppGenerators.Remove(item.Guid);
            //    _logger.LogTrace("{DicAppGenerators}", cfg.DicAppGenerators);
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = AppProjectGenerator.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as AppProject).ListAppProjectGenerators.Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new AppProjectGenerator(this.Parent);
            (this.Parent as AppProject).ListAppProjectGenerators.Add(node);
            this.GetUniqueName(AppProjectGenerator.DefaultName, node, (this.Parent as AppProject).ListAppProjectGenerators);
            this.SetSelected(node);
            return node;
        }

        #endregion Tree operations
    }
}
