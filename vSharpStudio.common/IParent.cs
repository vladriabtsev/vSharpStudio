using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public interface IParent //: ITreeModel
    {
        ITreeConfigNode Parent { get; set; }
    }
}
