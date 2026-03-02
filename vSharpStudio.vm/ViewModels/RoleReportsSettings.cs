using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class RoleReportsSettings
    {
        //[Browsable(false)]
        //public GroupListReports ParentGroupListReports { get { Debug.Assert(this.Parent != null); return (GroupListReports)this.Parent; } }
        //[Browsable(false)]
        //public IGroupListReports ParentGroupListReportsI { get { Debug.Assert(this.Parent != null); return (IGroupListReports)this.Parent; } }

        partial void OnCreated()
        {
            this._CanPrint = true;
            this._CanView = true;
        }
    }
}
