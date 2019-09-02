using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

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



        public List<ConnStringVM> ListConnectionStringVMs { get; set; }

    }
}
