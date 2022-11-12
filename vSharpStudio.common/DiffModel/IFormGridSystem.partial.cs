using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IFormGridSystem : ITreeConfigNodeSortable
    {
        IForm? ParentFormI { get; }
        IFormAutoLayoutBlock? ParentFormAutoLayoutBlockI { get; }
        IFormGridSystemRow AddGridRow(string name);
    }
}
