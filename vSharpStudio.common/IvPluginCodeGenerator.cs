using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.common
{
    public interface IvPluginCodeGenerator : IvPluginGenerator
    {
        List<PreRenameData> GetListPreRename(IConfig config, Dictionary<string, string> dicRenamedNodes);
    }
}
