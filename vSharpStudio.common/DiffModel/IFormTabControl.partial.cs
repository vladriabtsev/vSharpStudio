using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IFormTabControl : ITreeConfigNodeSortable
    {
        IFormAutoLayoutBlock ParentFormAutoLayoutBlockI { get; }
    }
}
