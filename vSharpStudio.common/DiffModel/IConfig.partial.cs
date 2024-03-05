using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IConfig : ITreeConfigNodeSortable, IEditableNodeGroup
    {
        ITreeConfigNode? SelectedNode { get; set; }
        IConfig? PrevCurrentConfig { get; }
        IConfig? PrevStableConfig { get; }
        IReadOnlyDictionary<string, ITreeConfigNode> DicNodes { get; }
        IReadOnlyDictionary<string, IvPluginGenerator> DicActiveAppProjectGenerators { get; }

        //List<IConfig> SetAnnotations(IConfig prev, IConfig old);
        IReadOnlyList<IConfig> GetListConfigs();
        string? CurrentCfgFolderPath { get; }
        void SetIsNeedCurrentUpdate(bool val);
        void SetIsNew(bool isNew);
        bool IsCanHaveChangesInTest();
    }
}
