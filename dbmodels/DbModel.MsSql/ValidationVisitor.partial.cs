using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace vPlugin.DbModel.MsSql
{
    public partial class ValidationVisitor
    {
        private void OnVisit(MsSqlConnectionSettings p) { }
        private void OnVisitEnd(MsSqlConnectionSettings p) { }
        private void OnVisit(MsSqlDesignGeneratorSettings p) { }
        private void OnVisitEnd(MsSqlDesignGeneratorSettings p) { }
    }
}
