using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IProperty : IParent, ITreeConfigNode, IGetNodeSetting
    {
        string DefaultValue { get; }
        bool IsPKey { get; set; }
        bool IsComputed { get; set; }
    }
}
