using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            Catalog c = new Catalog();
            this.Model = new ConfigRoot();
            this.Model.CatalogGroup.ListCatalogs.Add(c);
        }

        internal void OnSelectedItemChanged(object oldValue, object newValue)
        {
            this.Model.SelectedNode = (ITreeConfigNode)newValue;
        }

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
                };
            }
            get { return _Model; }
        }
        private ConfigRoot _Model;

        public vCommand CommandAddNew
        {
            get
            {
                return _CommandAddNew ?? (_CommandAddNew = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeAddNew(); },
                (o) => { return this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanAddNew(); }));
            }
        }
        private vCommand _CommandAddNew;
        public vCommand CommandAddNewChild
        {
            get
            {
                return _CommandAddNewChild ?? (_CommandAddNewChild = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeAddNewSubNode(); },
                (o) => { return this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanAddNewSubNode(); }));
            }
        }
        private vCommand _CommandAddNewChild;
        public vCommand CommandAddClone
        {
            get
            {
                return _CommandAddClone ?? (_CommandAddClone = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeAddClone(); },
                (o) => { return this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanAddClone(); }));
            }
        }
        private vCommand _CommandAddClone;
        public vCommand CommandMoveDown
        {
            get
            {
                return _CommandMoveDown ?? (_CommandMoveDown = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeMoveDown(); },
                (o) => { return this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanMoveDown(); }));
            }
        }
        private vCommand _CommandMoveDown;
        public vCommand CommandMoveUp
        {
            get
            {
                return _CommandMoveUp ?? (_CommandMoveUp = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeMoveUp(); },
                (o) => { return this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanMoveUp(); }));
            }
        }
        private vCommand _CommandMoveUp;
        public vCommand CommandDelete
        {
            get
            {
                return _CommandDelete ?? (_CommandDelete = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeRemove(); },
                (o) => { return this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanRemove(); }));
            }
        }
        private vCommand _CommandDelete;
        public vCommand CommandSelectionLeft
        {
            get
            {
                return _CommandSelectionLeft ?? (_CommandSelectionLeft = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeLeft(); },
                (o) => { return this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanLeft(); }));
            }
        }
        private vCommand _CommandSelectionLeft;
        public vCommand CommandSelectionRight
        {
            get
            {
                return _CommandSelectionRight ?? (_CommandSelectionRight = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeRight(); },
                (o) => { return this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanRight(); }));
            }
        }
        private vCommand _CommandSelectionRight;
        public vCommand CommandSelectionDown
        {
            get
            {
                return _CommandSelectionDown ?? (_CommandSelectionDown = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeDown(); },
                (o) => { return this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanDown(); }));
            }
        }
        private vCommand _CommandSelectionDown;
        public vCommand CommandSelectionUp
        {
            get
            {
                return _CommandSelectionUp ?? (_CommandSelectionUp = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeUp(); },
                (o) => { return this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanUp(); }));
            }
        }
        private vCommand _CommandSelectionUp;
    }
}
