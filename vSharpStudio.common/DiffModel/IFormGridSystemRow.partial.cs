using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IFormGridSystemRow : ITreeConfigNodeSortable
    {
        IFormGridSystem ParentFormGridSystemI { get; }
        IFormGridSystemColumn AddGridSystemColumn(string name = "");
    }
}
