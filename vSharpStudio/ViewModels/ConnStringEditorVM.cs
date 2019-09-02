using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;
using Xceed.Wpf.Toolkit;

namespace vSharpStudio.ViewModels
{
    public class ConnStringEditorVM : ViewModelValidatableWithSeverity<ConnStringEditorVM, ConnStringEditorVMValidator>
    {
        private ILogger logger;
        public MainPageVM MainVM { get; private set; }
        public ConnStringEditorVM() : base(ConnStringEditorVMValidator.Validator)
        {
        }
        public ConnStringEditorVM(MainPageVM mainVM) : this()
        {
            this.MainVM = mainVM;
            this.logger = mainVM.Logger;
        }

        #region Commands

        public vCommand CommandAddDbConnection
        {
            get
            {
                return _CommandAddDbConnection ?? (_CommandAddDbConnection = vCommand.Create(
                    (o) => { /*this.SaveAs();*/ },
                    (o) => { return true; }));
            }
        }
        private vCommand _CommandAddDbConnection;
        public vCommand CommandDelDbConnection
        {
            get
            {
                return _CommandDelDbConnection ?? (_CommandDelDbConnection = vCommand.Create(
                    (o) => {
                        var res = MessageBox.Show("Are you going to delete connection string?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                        if (res == System.Windows.MessageBoxResult.OK)
                            this.MainVM.Model.ListConnectionStringVMs.Remove(this.SelectedConnectionStringVM);
                    },
                    (o) => { return this.SelectedConnectionStringVM != null; }));
            }
        }
        private vCommand _CommandDelDbConnection;
        public vCommand CommandDbConnectionsSave
        {
            get
            {
                return _CommandDbConnectionsSave ?? (_CommandDbConnectionsSave = vCommand.Create(
                    (o) => { this.SaveAll(); },
                    (o) => { 
                        foreach(var t in this.MainVM.Model.ListConnectionStringVMs)
                        {
                            if (t.IsChanged)
                                return true;
                        }
                        return false;
                    }));
            }
        }
        private vCommand _CommandDbConnectionsSave;
        private void SaveAll()
        {

        }

        #endregion Commands

        #region Properties

        public bool IsSelectedByDbProvider
        {
            get { return _IsSelectedByDbProvider; }
            set
            {
                _IsSelectedByDbProvider = value;
                NotifyPropertyChanged();
                if (!this._IsSelectedByDbProvider)
                    this.SelectedDbProvider = null;
            }
        }
        private bool _IsSelectedByDbProvider;
        public string SelectedDbProvider
        {
            get { return _SelectedDbProvider; }
            set
            {
                _SelectedDbProvider = value;
                NotifyPropertyChanged();
            }
        }
        private string _SelectedDbProvider;
        public ConnStringVM SelectedConnectionStringVM
        {
            get { return _SelectedConnectionStringVM; }
            set
            {
                _SelectedConnectionStringVM = value;
                NotifyPropertyChanged();
            }
        }
        private ConnStringVM _SelectedConnectionStringVM;
        public object PluginConnectionStringVM
        {
            get { return _PluginConnectionStringVM; }
            set
            {
                _PluginConnectionStringVM = value;
                NotifyPropertyChanged();
            }
        }
        private object _PluginConnectionStringVM;

        #endregion Properties
    }
}
