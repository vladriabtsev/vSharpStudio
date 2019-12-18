using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PluginsGroup
    {
        //public override string ToString()
        //{
        //    if (string.IsNullOrWhiteSpace(this.PluginsGroupInfo))
        //        return "Select plugin group";
        //    return this.PluginsGroupInfo;
        //}
        //public List<Plugin> ListPluginsGroups
        //{
        //    get
        //    {
        //        var cfg = (Config)this.GetConfig();
        //        return (from p in cfg.GroupPlugins.ListPlugins select p).Distinct(new PluginGroupEqualityComparer()).ToList();
        //    }
        //}
    }
    class PluginGroupEqualityComparer : IEqualityComparer<Plugin>
    {
        public bool Equals(Plugin b1, Plugin b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null || b2 == null)
                return false;
            else if (b1.GroupGuid == b2.GroupGuid)
                return true;
            else
                return false;
        }

        public int GetHashCode(Plugin bx)
        {
            return bx.GroupGuid.GetHashCode();
        }
    }
}
