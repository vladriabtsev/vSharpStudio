using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public interface IGetNodeSetting
    {
        IvPluginGeneratorNodeSettings GetSettings(string guid);
        bool IsIncluded(string guid);
        bool ContainsSettings(string guid);
    }
}
