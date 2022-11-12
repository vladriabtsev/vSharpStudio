using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IFormAutoLayoutSubBlock : ITreeConfigNodeSortable
    {
        IForm? ParentFormI { get; }
        IFormAutoLayoutBlock? ParentFormAutoLayoutBlockI { get; }
        IFormTabControlTab? ParentFormTabControlTabI { get; }
        IFormGridSystemColumn? ParentFormGridSystemColumnI { get; }
    }
}
