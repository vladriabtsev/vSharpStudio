using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public interface IGetNodeSetting
    {
        IvPluginNodeSettings GetSettings(string guid);
    }
}
