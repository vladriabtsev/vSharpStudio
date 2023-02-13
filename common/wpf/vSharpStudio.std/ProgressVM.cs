using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;

namespace ViewModelBase
{
    public class ProgressVM : VmBindable
    {
        private CancellationToken? cancellationToken;
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
        private PauseToken? pauseToken;
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
        public Visibility NameVisibility
        {
            get { return _NameVisibility; }
            set
            {
                if (_NameVisibility != value)
                {
                    _NameVisibility = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public Visibility _NameVisibility;
        public int Progress
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
        private int _Progress;
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
                    this.SubProgress = 0;
                    if (_SubName == null)
                    {
                        SubNameVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        SubNameVisibility = Visibility.Visible;
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
        public int SubProgress
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
        private int _SubProgress;
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
        public void Start(string taskName, int? progress = null, string subname = null, int? subprogress = null, CancellationToken? cancellationToken = null, PauseToken? pauseToken = null)
        {
            this.Name = taskName;
            if (progress.HasValue)
            {
                this.Progress = progress.Value;
                this.ProgressVisibility = Visibility.Visible;
            }
            else
                this.ProgressVisibility = Visibility.Collapsed;
            this.SubName = subname;
            if (subprogress.HasValue)
            {
                this.SubProgress = subprogress.Value;
                this.SubProgressVisibility = Visibility.Visible;
            }
            else
                this.SubProgressVisibility = Visibility.Collapsed;
            this.cancellationToken = cancellationToken;
            if (!this.cancellationToken.HasValue)
                CancelVisibility = Visibility.Collapsed;
            else
                CancelVisibility = Visibility.Visible;
            this.pauseToken = pauseToken;
            if (!this.pauseToken.HasValue)
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
        public void UpdateProgress(int? progress)
        {
            if (progress.HasValue)
                this.Progress = progress.Value;
        }
        public void UpdateSubProgress(string subTaskName, int? subProgress)
        {
            if (subTaskName != null)
                this.SubName = subTaskName;
            if (subProgress.HasValue)
                this.SubProgress = subProgress.Value;
        }
        public void From(ProgressVM other)
        {
            UIDispatcher.Invoke(() =>
                {
                    this.Name = other.Name;
                    this.Progress = other.Progress;
                    this.SubName = other.SubName;
                    this.SubProgress = other.SubProgress;
                });
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
    }
}
