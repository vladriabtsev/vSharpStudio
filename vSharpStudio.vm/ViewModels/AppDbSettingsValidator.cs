using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppDbSettings
    {
        public partial class AppDbSettingsValidator
        {
            public AppDbSettingsValidator()
            {
                RuleFor(x => x.ConnGuid).NotEmpty().When(x => x.PluginName.Length > 0).WithMessage("Please select DB connection string name");
                RuleFor(x => x.ConnGuid).Must((o, name) => { return IsNotExistsConn(o); }).WithMessage(Config.ValidationMessages.PLUGIN_GENERATOR_SETTINGS_WAS_NOT_FOUND);
                RuleFor(x => x.PluginGenGuid).Must((o, name) => { return IsNotExistsGen(o); }).WithMessage(Config.ValidationMessages.PLUGIN_GENERATOR_WAS_NOT_FOUND);
                RuleFor(x => x.PluginGuid).Must((o, name) => { return IsNotExistsPlugin(o); }).WithMessage(Config.ValidationMessages.PLUGIN_WAS_NOT_FOUND);
            }
            private bool IsNotExistsPlugin(AppDbSettings val)
            {
                if (val.PluginGuid.Length == 0)
                    return true;
                IParent p = (IParent)val;
                while (p.Parent != null)
                    p = p.Parent;
                Config cfg = (Config)p;
                foreach (var t in cfg.GroupPlugins.ListPlugins)
                {
                    if (t.Guid == val.PluginGuid)
                        return true;
                }
                return false;
            }
            private bool IsNotExistsGen(AppDbSettings val)
            {
                if (val.PluginGenGuid.Length == 0)
                    return true;
                IParent p = (IParent)val;
                while (p.Parent != null)
                    p = p.Parent;
                Config cfg = (Config)p;
                foreach (var t in cfg.GroupPlugins.ListPlugins)
                {
                    foreach (var tt in t.ListGenerators)
                    {
                        if (tt.Guid == val.PluginGenGuid)
                            return true;
                    }
                }
                return false;
            }
            private bool IsNotExistsConn(AppDbSettings val)
            {
                if (val.ConnGuid.Length==0)
                    return true;
                IParent p = (IParent)val;
                while (p.Parent != null)
                    p = p.Parent;
                Config cfg = (Config)p;
                foreach (var t in cfg.GroupPlugins.ListPlugins)
                {
                    foreach (var tt in t.ListGenerators)
                    {
                        foreach (var ttt in tt.ListSettings)
                        {
                            if (ttt.Guid == val.ConnGuid)
                                return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}
