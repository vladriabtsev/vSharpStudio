using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;

namespace ViewModelBase
{
    public class ProgressVM : VmBindable
    {
        private CancellationToken cancellationToken;
        public Visibility CancelVisibility
        {
            get { return _CancelVisibility; }
            set
            {
                if (_CancelVisibility != value)
                {
                    _CancelVisibility = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public Visibility _CancelVisibility;
        private PauseToken pauseToken;
        public Visibility PauseVisibility
        {
            get { return _PauseVisibility; }
            set
            {
                if (_PauseVisibility != value)
                {
                    _PauseVisibility = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public Visibility _PauseVisibility;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        private bool _IsBusy;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        private string _Name;
        public int? Progress
        {
            get { return _Progress; }
            set
            {
                if (_Progress != value)
                {
                    _Progress = value;
                    this.NotifyPropertyChanged();
                    if (_Progress == null)
                        ProgressVisibility = Visibility.Collapsed;
                    else
                        ProgressVisibility = Visibility.Visible;
                }
            }
        }
        private int? _Progress;
        public Visibility ProgressVisibility
        {
            get { return _ProgressVisibility; }
            set
            {
                if (_ProgressVisibility != value)
                {
                    _ProgressVisibility = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public Visibility _ProgressVisibility;
        public string SubName
        {
            get { return _SubName; }
            set
            {
                if (_SubName != value)
                {
                    _SubName = value;
                    this.NotifyPropertyChanged();
                    if (_SubName == null)
                    {
                        SubNameVisibility = Visibility.Collapsed;
                        this.SubProgress = null;
                    }
                    else
                    {
                        SubNameVisibility = Visibility.Visible;
                        this.SubProgress = 0;
                    }
                }
            }
        }
        private string _SubName;
        public Visibility SubNameVisibility
        {
            get { return _SubNameVisibility; }
            set
            {
                if (_SubNameVisibility != value)
                {
                    _SubNameVisibility = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public Visibility _SubNameVisibility;
        public int? SubProgress
        {
            get { return _SubProgress; }
            set
            {
                if (_SubProgress != value)
                {
                    _SubProgress = value;
                    this.NotifyPropertyChanged();
                    if (_SubProgress == null)
                        SubProgressVisibility = Visibility.Collapsed;
                    else
                        SubProgressVisibility = Visibility.Visible;
                }
            }
        }
        private int? _SubProgress;
        public Visibility SubProgressVisibility
        {
            get { return _SubProgressVisibility; }
            set
            {
                if (_SubProgressVisibility != value)
                {
                    _SubProgressVisibility = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public Visibility _SubProgressVisibility;
        public void Start(string taskName, int? progress = null, string subname = null, int? subprogress = null, CancellationToken cancellationToken = default(CancellationToken), PauseToken pauseToken = default(PauseToken))
        {
            this.Name = taskName;
            this.Progress = progress;
            this.SubName = subname;
            this.SubProgress = subprogress;
            this.cancellationToken = cancellationToken;
            if (cancellationToken == default(CancellationToken))
                CancelVisibility = Visibility.Collapsed;
            else
                CancelVisibility = Visibility.Visible;
            this.pauseToken = pauseToken;
            if (pauseToken.IsNull())
                PauseVisibility = Visibility.Collapsed;
            else
                PauseVisibility = Visibility.Visible;
            this.Exception = null;
            this.IsBusy = true;
        }
        public void End()
        {
            this.IsBusy = false;
        }
        public void Update(string taskName)
        {
            this.Name = taskName;
            this.Progress = null;
            this.SubName = null;
            this.SubProgress = null;
        }
        public void From(ProgressVM other)
        {
            this.Name = other.Name;
            this.Progress = other.Progress;
            this.SubName = other.SubName;
            this.SubProgress = other.SubProgress;
        }

        public Exception Exception
        {
            get { return _Exception; }
            set
            {
                if (_Exception != value)
                {
                    _Exception = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public Exception _Exception;
    }
}
