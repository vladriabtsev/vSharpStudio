using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IConfig : IObjectAnnotatable
    {
        ITreeConfigNode SelectedNode { get; set; }
        IConfig PrevStableConfig { get; }

        IConfig OldStableConfig { get; }

        DictionaryExt<string, ITreeConfigNode> DicNodes { get; }
        DictionaryExt<string, IvPluginGenerator> DicActiveAppProjectGenerators { get; }

        //List<IConfig> SetAnnotations(IConfig prev, IConfig old);
        List<IConfig> GetListConfigs();
        string CurrentCfgFolderPath { get; }
    }
}
