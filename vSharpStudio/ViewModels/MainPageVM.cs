using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Google.Protobuf;
using Microsoft.Win32;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.ViewModels
{
    public class MainPageVM : ViewModelValidatableWithSeverity<MainPageVM, MainPageVMValidator>
    {
        public MainPageVM() : base(MainPageVMValidator.Validator)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                //Catalog c = new Catalog();
                //this.Model = new ConfigRoot();
                //this.Model.Catalogs.ListCatalogs.Add(c);
                return;
            }
            if (File.Exists(CFG_PATH))
            {
                string json = File.ReadAllText(CFG_PATH);
                this.Model = new ConfigRoot(json);
            }
            else
                this.Model = new ConfigRoot();
        }
        private const string CFG_PATH = @".\current.vcfg";
        //internal void OnSelectedItemChanged(object oldValue, object newValue)
        //{
        //    this.Model.SelectedNode = (ITreeConfigNode)newValue;
        //}

        public ConfigRoot Model
        {
            set
            {
                _Model = value;
                NotifyPropertyChanged();
                ValidateProperty();
                _Model.OnSelectedNodeChanged = () =>
                {
                    CommandAddNew.RaiseCanExecuteChanged();
                    CommandAddNewChild.RaiseCanExecuteChanged();
                    CommandAddClone.RaiseCanExecuteChanged();
                    CommandMoveDown.RaiseCanExecuteChanged();
                    CommandMoveUp.RaiseCanExecuteChanged();
                    CommandDelete.RaiseCanExecuteChanged();
                    CommandSelectionLeft.RaiseCanExecuteChanged();
                    CommandSelectionRight.RaiseCanExecuteChanged();
                    CommandSelectionDown.RaiseCanExecuteChanged();
                    CommandSelectionUp.RaiseCanExecuteChanged();
                    //_Model.ValidateSubTreeFromNode(_Model.SelectedNode);
                    _Model.ValidateSubTreeFromNode(_Model.SelectedNode);
                };
            }
            get { return _Model; }
        }
        private ConfigRoot _Model;

        #region Main

        public vCommand CommandConfigSave
        {
            get
            {
                return _CommandConfigSave ?? (_CommandConfigSave = vCommand.Create(
                (o) => { this.Save(); },
                (o) => { return this.Model != null; }));
            }
        }
        private vCommand _CommandConfigSave;
        internal void Save()
        {
            var json = JsonFormatter.Default.Format(Config.ConvertToProto(_Model));
            File.WriteAllText(CFG_PATH, json);
        }
        public vCommand CommandConfigSaveAs
        {
            get
            {
                return _CommandConfigSaveAs ?? (_CommandConfigSaveAs = vCommand.Create(
                (o) => { this.SaveAs(); },
                (o) => { return this.Model != null; }));
            }
        }
        private vCommand _CommandConfigSaveAs;
        internal void SaveAs()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=netframework-4.8
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "vConfig files (*.vcfg)|*.vcfg|All files (*.*)|*.*";
            //openFileDialog.InitialDirectory = @"c:\temp\";
            //openFileDialog.Multiselect = true;
            if (!string.IsNullOrEmpty(_FilePathSaveAs))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(_FilePathSaveAs);
            }
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathSaveAs = openFileDialog.FileName;
                var json = JsonFormatter.Default.Format(Config.ConvertToProto(_Model));
                File.WriteAllText(FilePathSaveAs, json);
            }
        }

        public string FilePathSaveAs
        {
            get { return _FilePathSaveAs; }
            set
            {
                _FilePathSaveAs = value;
                NotifyPropertyChanged();
                SaveToolTip = _saveBaseToolTip + " as " + _FilePathSaveAs;
            }
        }
        private string _FilePathSaveAs;
        public string SaveToolTip
        {
            get { return _SaveToolTip; }
            set
            {
                _SaveToolTip = value;
                NotifyPropertyChanged();
            }
        }
        private string _SaveToolTip = _saveBaseToolTip;
        private const string _saveBaseToolTip = "Ctrl-S - save config";

        #endregion Main

        #region ConfigTree

        public vCommand CommandAddNew
        {
            get
            {
                return _CommandAddNew ?? (_CommandAddNew = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeAddNew(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanAddNew(); }));
            }
        }
        private vCommand _CommandAddNew;
        public vCommand CommandAddNewChild
        {
            get
            {
                return _CommandAddNewChild ?? (_CommandAddNewChild = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeAddNewSubNode(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanAddNewSubNode(); }));
            }
        }
        private vCommand _CommandAddNewChild;
        public vCommand CommandAddClone
        {
            get
            {
                return _CommandAddClone ?? (_CommandAddClone = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeAddClone(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanAddClone(); }));
            }
        }
        private vCommand _CommandAddClone;
        public vCommand CommandMoveDown
        {
            get
            {
                return _CommandMoveDown ?? (_CommandMoveDown = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeMoveDown(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanMoveDown(); }));
            }
        }
        private vCommand _CommandMoveDown;
        public vCommand CommandMoveUp
        {
            get
            {
                return _CommandMoveUp ?? (_CommandMoveUp = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeMoveUp(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanMoveUp(); }));
            }
        }
        private vCommand _CommandMoveUp;
        public vCommand CommandDelete
        {
            get
            {
                return _CommandDelete ?? (_CommandDelete = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeRemove(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanRemove(); }));
            }
        }
        private vCommand _CommandDelete;
        public vCommand CommandSelectionLeft
        {
            get
            {
                return _CommandSelectionLeft ?? (_CommandSelectionLeft = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeLeft(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanLeft(); }));
            }
        }
        private vCommand _CommandSelectionLeft;
        public vCommand CommandSelectionRight
        {
            get
            {
                return _CommandSelectionRight ?? (_CommandSelectionRight = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeRight(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanRight(); }));
            }
        }
        private vCommand _CommandSelectionRight;
        public vCommand CommandSelectionDown
        {
            get
            {
                return _CommandSelectionDown ?? (_CommandSelectionDown = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeDown(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanDown(); }));
            }
        }
        private vCommand _CommandSelectionDown;
        public vCommand CommandSelectionUp
        {
            get
            {
                return _CommandSelectionUp ?? (_CommandSelectionUp = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeUp(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanUp(); }));
            }
        }
        private vCommand _CommandSelectionUp;

        #endregion ConfigTree
        public CollectionView kuku2;
        public ICollectionView kuku;
        public vCommand CommandFromErrorToSelection
        {
            get
            {
                return _CommandFromErrorToSelection ?? (_CommandFromErrorToSelection = vCommand.Create(
                (o) => { if (o == null) return; this.Model.SelectedNode = (ITreeConfigNode)(o as ValidationMessage).Model; },
                (o) => { return this.Model != null; }));
            }
        }
        private vCommand _CommandFromErrorToSelection;

    }
}
