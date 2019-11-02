using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public static class ConfigExt
    {
        public static Config AddNewConstant(this Config cfg, string name, EnumDataType enumDataType)
        {
            var p = new Constant(cfg.Model.GroupConstants, name, enumDataType);
            cfg.Model.GroupConstants.Add(p);
            return cfg;
        }
    }
}
