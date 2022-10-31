using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IFormGridSystem : ITreeConfigNode
    {
        IForm ParentFormI { get; }
        IFormAutoLayoutBlock ParentFormAutoLayoutBlockI { get; }
    }
}
