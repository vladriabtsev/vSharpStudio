using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IConfig : IObjectAnnotatable
    {
        ITreeConfigNode SelectedNode { get; set; }
        IConfig PrevStableConfig { get; }

        IConfig OldStableConfig { get; }

        Dictionary<string, ITreeConfigNode> DicNodes { get; }

        List<IConfig> ListAnnotated { get; }
    }
}
