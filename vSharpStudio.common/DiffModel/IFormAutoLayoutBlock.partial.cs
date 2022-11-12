using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IFormAutoLayoutBlock : ITreeConfigNodeSortable
    {
        IForm? ParentFormI { get; }
        IFormAutoLayoutBlock? ParentFormAutoLayoutBlockI { get; }
        IFormTabControlTab? ParentFormTabControlTabI { get; }
        IFormGridSystemColumn? ParentFormGridSystemColumnI { get; }

        IFormAutoLayoutBlock AddAutoLayoutBlock(string name = "");
        IFormDataGrid AddDataGrid(string name = "");
        IFormField AddField(string name = "");
        IFormGridSystem AddGridSystem(string name = "");
        IFormTabControl AddTabControl(string name = "");
        IFormTree AddTree(string name = "");
    }
}
