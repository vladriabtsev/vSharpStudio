﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Config : ConfigObjectBase<Config, Config.ConfigValidator>, IMigration, IComparable<Config>
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

        void RecreateSubNodes()
        {
            SubNodes.Clear();
            SubNodes.Add(this.ConstantGroup);
            SubNodes.Add(this.EnumerationGroup);
            SubNodes.Add(this.CatalogGroup);
            //foreach (var t in this.ListConstantsGroups)
            //    SubNodes.Add(t, 1);
            //foreach (var t in this.ListEnumerationsGroups)
            //    SubNodes.Add(t, 2);
            //foreach (var t in this.ListCatalogsGroups)
            //    SubNodes.Add(t, 3);
        }
        partial void OnConstantGroupChanged() { RecreateSubNodes(); }
        partial void OnCatalogGroupChanged() { RecreateSubNodes(); }
        partial void OnEnumerationGroupChanged() { RecreateSubNodes(); }

        int IComparable<Config>.CompareTo(Config other)
        {
            throw new NotImplementedException();
        }
        protected override bool OnNodeCanLeft()
        {
            return false;
        }

        #endregion ITreeNode
    }
}
