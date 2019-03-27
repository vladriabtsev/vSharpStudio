using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using FluentValidation;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.vm.Migration;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config : EntityObjectBaseWithGuid<Config, Config.ConfigValidator>, IEntityObject, IMigration
    {
        protected IMigration _migration = null;
        public string ConnectionString = null;
        partial void OnInit()
        {
            if (string.IsNullOrWhiteSpace(this.DbSchema))
                this.DbSchema = "v";
        }
        public void OnInitFromDto()
        {
            RecreateSubNodes();
        }
        public Config(string configJson, SortedObservableCollection<ValidationMessage> validationCollection = null)
            : base(ConfigValidator.Validator, validationCollection)
        {
            var pconfig = Proto.Config.proto_config.Parser.ParseJson(configJson);
            ProtoToVM.ConvertToVM(pconfig, validationCollection, this);
        }
        public string ExportToJson()
        {
            var pconfig = ProtoToVM.ConvertToProto(this);
            var res = JsonFormatter.Default.Format(pconfig);
            return res;
        }
        public List<EntityObjectProblem> GetUpdateDbProblems()
        {
            return _migration.GetUpdateDbProblems();
        }
        public void UpdateDb()
        {
            throw new NotImplementedException();
        }
        public virtual void InitMigration()
        {
            throw new NotImplementedException();
        }
        public bool IsDatabaseServiceOn()
        {
            return _migration.IsDatabaseServiceOn();
        }
        public bool IsDatabaseExist()
        {
            throw new NotImplementedException();
        }
        public bool CreateDatabase()
        {
            throw new NotImplementedException();
        }
        public DatabaseModel GetDatabaseModel()
        {
            return _migration.GetDatabaseModel();
        }
        #region ITreeNode
        public ITreeNode Parent => null;

        public IEnumerable<ITreeNode> SubNodes
        {
            get { return this._SubNodes; }
            set
            {
                this._SubNodes = value;
                NotifyPropertyChanged();
            }
        }
        private IEnumerable<ITreeNode> _SubNodes;
        void RecreateSubNodes() { SubNodes = new ITreeNode[] { this.Constants, this.Enumerators, this.Catalogs }; }
        partial void OnConstantsChanged() { RecreateSubNodes(); }
        partial void OnCatalogsChanged() { RecreateSubNodes(); }
        partial void OnEnumeratorsChanged() { RecreateSubNodes(); }

        #region ITreeNodeWithValidation
        public int ValidationQty
        {
            set
            {
                if (_ValidationQty != value)
                {
                    _ValidationQty = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _ValidationQty; }
        }
        private int _ValidationQty;

        public Severity ValidationSeverity
        {
            set
            {
                if (_ValidationSeverity != value)
                {
                    _ValidationSeverity = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _ValidationSeverity; }
        }

        private Severity _ValidationSeverity;
        #endregion ITreeNodeWithValidation
        #endregion ITreeNode
    }
}
