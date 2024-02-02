using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IEnumeratorDocumentSequence : ITreeConfigNodeSortable, ITextValue
    {
        IGroupListEnumeratorSequences ParentGroupListSequencesI { get; }
    }
}
