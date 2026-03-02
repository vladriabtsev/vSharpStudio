using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class RoleDocumentsSettings
    {
        //[Browsable(false)]
        //public GroupListReports ParentGroupListReports { get { Debug.Assert(this.Parent != null); return (GroupListReports)this.Parent; } }
        //[Browsable(false)]
        //public IGroupListReports ParentGroupListReportsI { get { Debug.Assert(this.Parent != null); return (IGroupListReports)this.Parent; } }

        partial void OnCreated()
        {
            this._CanEdit = true;
            this._CanEditDetails = true;
            this._CanEditFields = true;
            this._CanMarkDel = true;
            this._CanPost = true;
            this._CanPrint = true;
            this._CanUnpost = true;
            this._CanView = true;
            this._CanViewDetails = true;
            this._CanViewFields = true;
            this._CanViewPostData = true;
        }
    }
}
