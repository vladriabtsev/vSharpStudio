using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class UserSettings
    {
        public Action<UserSettingsOpenedConfig>? OnOpenRecentConfig { get; set; }
        public vCommand CommandOpenRecentConfig
        {
            get
            {
                return this._CommandOpenRecentConfig ?? (this._CommandOpenRecentConfig = vCommand.Create(
                (o) => { Debug.Assert(this.SelectedConfigHistory != null); Debug.Assert(this.OnOpenRecentConfig != null); this.OnOpenRecentConfig(this.SelectedConfigHistory); },
                (o) => { return this.SelectedConfigHistory != null; }));
            }
        }
        private vCommand? _CommandOpenRecentConfig;
        public vCommand CommandDeleteRecentConfig
        {
            get
            {
                return this._CommandDeleteRecentConfig ?? (this._CommandDeleteRecentConfig = vCommand.Create(
                (o) => { Debug.Assert(this.SelectedConfigHistory != null); this.ListOpenConfigHistory.Remove(this.SelectedConfigHistory); },
                (o) => { return this.SelectedConfigHistory != null; }));
            }
        }
        private vCommand? _CommandDeleteRecentConfig;
        [Browsable(false)]
        public UserSettingsOpenedConfig? SelectedConfigHistory
        {
            get { return this._SelectedConfigHistory; }
            set
            {
                if (this._SelectedConfigHistory != value)
                {
                    _SelectedConfigHistory = value;
                    this.NotifyPropertyChanged();
                    this.CommandOpenRecentConfig.RaiseCanExecuteChanged();
                    this.CommandDeleteRecentConfig.RaiseCanExecuteChanged();
                }
            }
        }
        private UserSettingsOpenedConfig? _SelectedConfigHistory;
    }
}
