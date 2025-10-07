using System.Collections.Generic;

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
        bool IsInitialized { get; }
        void AddToDicNodes(ITreeConfigNode node);
        void RemoveFromDicNodes(ITreeConfigNode node);
    }
}
