using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ViewModelBase
{
  public class ViewModelEditable<T> : ViewModelBindable<T>, IEditableObjectExt
    where T : ViewModelEditable<T>
  {
    //protected T _dtoBackup;

    public ViewModelEditable()
		{
			//this._dtoBackup = this.Backup();
		}
		//public virtual void ResetAllChanges()
		//{
  //    this.Restore(_dtoBackup);
		//	IsChanged = false;
		//}
		protected virtual void Restore(T from) { throw new NotImplementedException(); }
    protected virtual T Backup() { throw new NotImplementedException(); }

		#region IEditableObject
		private T _dtoBackupTmp;
		public bool IsInEdit { get; private set; }
		public void BeginEdit()
		{
			if (IsInEdit)
				//return;
				throw new Exception("Already in EDIT mode");
			this._dtoBackupTmp = this.Backup();
			IsInEdit = true;
		}
		public void CancelEdit()
		{
			if (!IsInEdit)
				//return;
				throw new Exception("Already not in EDIT mode");
      this.Restore(_dtoBackupTmp);
			//this.Validate();
			IsInEdit = false;
		}
		public void EndEdit()
		{
			if (!IsInEdit)
				//return;
				throw new Exception("Already not in EDIT mode");
			IsInEdit = false;
		}
		#endregion
	}
}
