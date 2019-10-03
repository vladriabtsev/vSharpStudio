using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.vm.ViewModels
{
    public static class ConfigExt
    {
        public static Config AdNew(this Config cfg, Constant p)
        {
            cfg.Model.GroupConstants.Add(p);
            return cfg;
        }
    }
}
