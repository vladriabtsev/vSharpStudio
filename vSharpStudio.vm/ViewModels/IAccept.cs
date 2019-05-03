using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public interface IAccept
    {
        void AcceptConfigNode(IVisitorConfigNode visitor);
    }
}
