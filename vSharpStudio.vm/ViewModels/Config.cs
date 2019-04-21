using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.vm.Migration;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config : IMigration
    {
        public static readonly string DefaultName = "Config";

        protected IMigration _migration = null;
        public string ConnectionString = null;
        partial void OnInit()
        {
            this.SubNodes = new SortedObservableCollection<ITreeConfigNode>();
#if DEBUG
            //SubNodes.Add(this.GroupConstants, 1);
#endif
            SubNodes.Add(this.GroupConstants, 7);
            SubNodes.Add(this.GroupEnumerations, 8);
            SubNodes.Add(this.GroupCatalogs, 9);
            SubNodes.Add(this.GroupDocuments, 10);
            SubNodes.Add(this.GroupJournals, 11);
            if (string.IsNullOrWhiteSpace(this.DbSchema))
                this.DbSchema = "v";
        }
        protected override void OnInitFromDto()
        {
            RecreateSubNodes();
        }
        public Config(string configJson)
            : base(ConfigValidator.Validator)
        {
            var pconfig = Proto.Config.proto_config.Parser.ParseJson(configJson);
            Config.ConvertToVM(pconfig, this);
        }
        public string ExportToJson()
        {
            var pconfig = Config.ConvertToProto(this);
            var res = JsonFormatter.Default.Format(pconfig);
            return res;
        }


        #region IMigration

        //public virtual void InitMigration()
        //{
        //    // overriden in ConfigRoot class
        //    throw new NotImplementedException();
        //}
        bool IMigration.IsDatabaseServiceOn()
        {
            return _migration.IsDatabaseServiceOn();
        }
        Task<bool> IMigration.IsDatabaseServiceOnAsync(CancellationToken cancellationToken)
        {
            return _migration.IsDatabaseServiceOnAsync(cancellationToken);
        }
        bool IMigration.IsDatabaseExists()
        {
            return _migration.IsDatabaseExists();
        }
        Task<bool> IMigration.IsDatabaseExistsAsync(CancellationToken cancellationToken)
        {
            return _migration.IsDatabaseExistsAsync(cancellationToken);
        }
        void IMigration.CreateDatabase()
        {
            _migration.CreateDatabase();
        }
        Task IMigration.CreateDatabaseAsync(CancellationToken cancellationToken)
        {
            return _migration.CreateDatabaseAsync(cancellationToken);
        }
        void IMigration.DropDatabase()
        {
            _migration.DropDatabase();
        }
        Task IMigration.DropDatabaseAsync(CancellationToken cancellationToken)
        {
            return _migration.DropDatabaseAsync(cancellationToken);
        }

        #endregion IMigration

        #region ITreeNode

        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> SubNodes
        {
            get { return this._SubNodes; }
            set
            {
                this._SubNodes = value;
                NotifyPropertyChanged();
            }
        }
        private SortedObservableCollection<ITreeConfigNode> _SubNodes;

        void RecreateSubNodes()
        {
        }
        partial void OnGroupConstantsChanged() { RecreateSubNodes(); }
        partial void OnGroupCatalogsChanged() { RecreateSubNodes(); }
        partial void OnGroupEnumerationsChanged() { RecreateSubNodes(); }
        protected override bool OnNodeCanLeft()
        {
            return false;
        }

        #endregion ITreeNode

        public ITreeConfigNode SelectedNode
        {
            set
            {
                if (_SelectedNode != value)
                {
                    _SelectedNode = value;
                    NotifyPropertyChanged();
                    if (OnSelectedNodeChanged != null)
                        OnSelectedNodeChanged();
                }
            }
            get { return _SelectedNode; }
        }
        private ITreeConfigNode _SelectedNode;
        public Action OnSelectedNodeChanged;
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            Config t = new Config();
            StringBuilder sb = new StringBuilder();
            Proto.Attr.DicPropAttrs res = new Proto.Attr.DicPropAttrs();
            t.PropertyNameAction(p => p.NameUi, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(2).ToString();
            });
            t.PropertyNameAction(p => p.Description, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(3).ToString();
            });
            return res;
        }
    }
}
