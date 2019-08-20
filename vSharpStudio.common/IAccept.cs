using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public interface IAccept
    {
        void AcceptConfigNode(IVisitorConfigNode visitor);
    }
}
