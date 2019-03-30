using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public interface IAccept
    {
        void Accept(IVisitorConfig visitor);
    }
}
