using System.Collections.Generic;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.common
{
    public interface IvPluginCodeGenerator : IvPluginGenerator
    {
        List<PreRenameData> GetListPreRename(IConfig config, Dictionary<string, string?> dicRenamedNodes);
    }
}
