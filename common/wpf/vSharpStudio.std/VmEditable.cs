using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelBase
{
    public class VmEditable<T> : VmBindable, IEditableObjectExt
        where T : VmEditable<T>
    {
        public VmEditable()
        {
            //this.PropertyChanged += VmEditable_PropertyChanged;
            //this._dtoBackup = this.Backup();
        }

        //private void VmEditable_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    this.IsChanged = true;
        //}

        //public virtual void ResetAllChanges()
        //{
        //    this.Restore(_dtoBackup);
        //	IsChanged = false;
        //}
        public virtual void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public virtual T Backup() { throw new NotImplementedException("Please override Backup method"); }

        #region IEditableObject
        private T _dtoBackupTmp;
        [BrowsableAttribute(false)]
        public bool IsReadonly
        {
            get { return _IsReadonly; }
            set { if (SetProperty(ref _IsReadonly, value)) { IsEditable = !_IsReadonly; } }
        }
        private bool _IsReadonly = false;
        [BrowsableAttribute(false)]
        public bool IsEditable
        {
            get { return _IsEditable; }
            set { if (SetProperty(ref _IsEditable, value)) { IsReadonly = !_IsEditable; } }
        }
        private bool _IsEditable = true;
        [BrowsableAttribute(false)]
        public virtual bool IsChanged
        {
            get { return _IsChanged; }
            set
            {
                if (VmBindable.IsNotifyingStatic && IsNotifying)
                {
                    SetProperty(ref _IsChanged, value);
                    OnIsChangedChanged();
                    if (VmBindable.IsChangedNotificationDelay > 0)
                    {
                        lock (lockObject)
                        {
                            if (!isDelayActivated)
                            {
                                isDelayActivated = true;
                                var t = Task.Run(async delegate
                                {
                                    await Task.Delay(VmBindable.IsChangedNotificationDelay);
                                    VmBindable.InvokeOnUIThread(delegate
                                    {
                                        isDelayActivated = false;
                                        OnIsChangedChangedWithDelay();
                                    });
                                });
                            }
                        }
                    }
                }
            }
        }
        protected bool _IsChanged;
        private bool isDelayActivated = false;
        private object lockObject = new object();
        protected virtual void OnIsChangedChanged() { }
        protected virtual void OnIsChangedChangedWithDelay() { }
        [BrowsableAttribute(false)]
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
