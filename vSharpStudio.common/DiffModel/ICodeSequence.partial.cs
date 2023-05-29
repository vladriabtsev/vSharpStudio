using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface ICodeSequence : ITreeConfigNodeSortable, ITextValue
    {
        IGroupListSequences ParentGroupListSequencesI { get; }
    }
}
