using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IDocNumberCodeSequence : ITreeConfigNodeSortable
    {
        IGroupDocNumberListSequences ParentGroupListSequencesI { get; }
    }
}
