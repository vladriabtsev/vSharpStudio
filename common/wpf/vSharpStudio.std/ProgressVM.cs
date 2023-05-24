using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;

namespace ViewModelBase
{
    public class ProgressVM : VmBindable
    {
        private CancellationToken? cancellationToken
        {
            get { return _cancellationToken; }
            set
            {
                UIDispatcher.Invoke(() =>
                {
                    _cancellationToken = value;
                    this.NotifyPropertyChanged();
                    if (_cancellationToken == null)
                        CancelVisibility = Visibility.Collapsed;
                    else
                        CancelVisibility = Visibility.Visible;
                });
            }
        }
        private CancellationToken? _cancellationToken;
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
        private PauseToken? pauseToken
        {
            get { return _pauseToken; }
            set
            {
                UIDispatcher.Invoke(() =>
                {
                    _pauseToken = value;
                    this.NotifyPropertyChanged();
                    if (_pauseToken == null)
                        PauseVisibility = Visibility.Collapsed;
                    else
                        PauseVisibility = Visibility.Visible;
                });
            }
        }
        private PauseToken? _pauseToken;

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
        /// <summary>
        /// Name for progress bar
        /// To indicate progress use methods ProgressStart, ProgressUpdate, ProgressUpdateSubTask, ProgressClose
        /// </summary>
        public string? Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    UIDispatcher.Invoke(() =>
                    {
                        _Title = value;
                        this.NotifyPropertyChanged();
                        if (_Title == null)
                            TitleVisibility = Visibility.Collapsed;
                        else
                            TitleVisibility = Visibility.Visible;
                    });
                }
            }
        }
        private string? _Title;
        public Visibility TitleVisibility
        {
            get { return _TitleVisibility; }
            set
            {
                if (_TitleVisibility != value)
                {
                    _TitleVisibility = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public Visibility _TitleVisibility = Visibility.Collapsed;
        /// <summary>
        /// Name for progress step
        /// To indicate progress use methods ProgressStart, ProgressUpdate, ProgressUpdateSubTask, ProgressClose
        /// </summary>
        public string? Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    UIDispatcher.Invoke(() =>
                    {
                        _Name = value;
                        this.NotifyPropertyChanged();
                        if (_Name == null)
                            NameVisibility = Visibility.Collapsed;
                        else
                            NameVisibility = Visibility.Visible;
                    });
                }
            }
        }
        private string? _Name;
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
        public Visibility _NameVisibility = Visibility.Collapsed;
        /// <summary>
        /// Progress in percent. Range from 0 to 100. If null progress bar will be collapsed
        /// To indicate progress use methods ProgressStart, ProgressUpdate, ProgressUpdateSubTask, ProgressClose
        /// </summary>
        public int Progress
        {
            get { return _Progress; }
            set
            {
                if (_Progress != value)
                {
                    UIDispatcher.Invoke(() =>
                    {
                        Debug.Assert(value >= 0 && value <= 100);
                        _Progress = value;
                        this.NotifyPropertyChanged();
                        if (_Progress == 0)
                            ProgressVisibility = Visibility.Collapsed;
                        else
                            ProgressVisibility = Visibility.Visible;
                    });
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
        public Visibility _ProgressVisibility = Visibility.Collapsed;
        /// <summary>
        /// Name for sub task progress bar step
        /// To indicate progress use methods ProgressStart, ProgressUpdate, ProgressUpdateSubTask, ProgressClose
        /// </summary>
        public string? SubName
        {
            get { return _SubName; }
            set
            {
                if (_SubName != value)
                {
                    UIDispatcher.Invoke(() =>
                    {
                        _SubName = value;
                        this.NotifyPropertyChanged();
                        //this.SubProgress = null;
                        if (_SubName == null)
                        {
                            SubNameVisibility = Visibility.Collapsed;
                        }
                        else
                        {
                            SubNameVisibility = Visibility.Visible;
                        }
                    });
                }
            }
        }
        private string? _SubName;
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
        public Visibility _SubNameVisibility = Visibility.Collapsed;
        /// <summary>
        /// Sub task percent of progress
        /// To indicate progress use methods ProgressStart, ProgressUpdate, ProgressUpdateSubTask, ProgressClose
        /// </summary>
        public int SubProgress
        {
            get { return _SubProgress; }
            set
            {
                if (_SubProgress != value)
                {
                    UIDispatcher.Invoke(() =>
                    {
                        Debug.Assert(value >= 0 && value <= 100);
                        _SubProgress = value;
                        this.NotifyPropertyChanged();
                        if (_SubProgress == 0)
                            SubProgressVisibility = Visibility.Collapsed;
                        else
                            SubProgressVisibility = Visibility.Visible;
                    });
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
        public Visibility _SubProgressVisibility = Visibility.Collapsed;
        /// <summary>
        /// Progress Start method
        /// </summary>
        /// <param name="taskName">Name of progress</param>
        /// <param name="progress">Percent of initial progress. Progress bar is hidden if null</param>
        /// <param name="subname">Sub task name. Hidden if null</param>
        /// <param name="subprogress">Percent of initial sub task progress. Sub task progress bar is hidden if null</param>
        /// <param name="cancellationToken">Cancellation token. If provided then 'Cancel' button will be visible</param>
        /// <param name="pauseToken">Pause token. If provided then 'Pause' button will be visible</param>
        public void ProgressStart(string taskName, int progress = 0, string? subname = null, int subprogress = 0, CancellationToken? cancellationToken = null, PauseToken? pauseToken = null)
        {
            this.Title = taskName;
            this.Progress = progress;
            this.SubName = subname;
            this.SubProgress = subprogress;
            this.cancellationToken = cancellationToken;
            this.pauseToken = pauseToken;
            this.Exception = null;
            this.IsBusy = true;
        }
        /// <summary>
        /// Close progress dialog
        /// </summary>
        public void ProgressClose()
        {
            this.IsBusy = false;
        }
        /// <summary>
        /// Update progress
        /// </summary>
        /// <param name="progress">Percent of progress. Progress bar is hidden if null</param>
        public void ProgressUpdate(int progress)
        {
            this.Progress = progress;
        }
        /// <summary>
        /// Update progress
        /// </summary>
        /// <param name="progress">Percent of progress. Progress bar is hidden if null</param>
        public void ProgressUpdate(string? stepName, int progress)
        {
            this.Name = stepName;
            this.Progress = progress;
        }
        /// <summary>
        /// Update progress of sub task
        /// </summary>
        /// <param name="subProgress">Percent of sub task progress. Sub task progress bar is hidden if null</param>
        public void ProgressUpdateSubTask(int subProgress)
        {
            this.SubProgress = subProgress;
        }
        /// <summary>
        /// Update progress of sub task
        /// </summary>
        /// <param name="subTaskName">Name of sub task progress bar. Name is hidden if null</param>
        /// <param name="subProgress">Percent of sub task progress. Sub task progress bar is hidden if null</param>
        public void ProgressUpdateSubTask(string? subTaskName, int subProgress)
        {
            this.SubName = subTaskName;
            this.SubProgress = subProgress;
        }
        public void From(ProgressVM other)
        {
            UIDispatcher.Invoke(() =>
                {
                    this.Title = other.Title;
                    this.Progress = other.Progress;
                    this.SubName = other.SubName;
                    this.SubProgress = other.SubProgress;
                });
        }

        public Exception? Exception
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
        public Exception? _Exception;
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
