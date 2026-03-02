using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class RoleCatalogsSettings
    {
        //[Browsable(false)]
        //public GroupListReports ParentGroupListReports { get { Debug.Assert(this.Parent != null); return (GroupListReports)this.Parent; } }
        //[Browsable(false)]
        //public IGroupListReports ParentGroupListReportsI { get { Debug.Assert(this.Parent != null); return (IGroupListReports)this.Parent; } }

        partial void OnCreated()
        {
            this._CanEditFolders = true;
            this._CanEditDetails = true;
            this._CanEditFields = true;
            this._CanEditItems = true;
            this._CanMarkDel = true;
            this._CanMoveFolders = true;
            this._CanMoveItems = true;
            this._CanPrint = true;
            this._CanView = true;
            this._CanViewDetails = true;
            this._CanViewFields = true;
        }
    }
}
