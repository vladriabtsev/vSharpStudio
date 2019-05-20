using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ValidationVisitor
    {
        public CancellationToken Token { get; set; }
        private void OnVisit(MsSql p) { }
        private void OnVisitEnd(MsSql p) { }
    }
}
