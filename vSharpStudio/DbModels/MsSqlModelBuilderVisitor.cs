using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.DbModels
{
    public partial class MsSqlModelBuilderVisitor : ConfigVisitor
    {
        private ModelBuilder _modelBuilder = null;
        private Config _config = null;

        public MsSqlModelBuilderVisitor(CancellationToken cancellationToken, ILogger logger = null)
            : base(cancellationToken, logger)
        {
        }

        public Microsoft.EntityFrameworkCore.Metadata.IModel GetModel()
        {
            if (this._model == null)
            {
                this._model = this._modelBuilder.FinalizeModel();
            }
            return this._model;
        }

        private Microsoft.EntityFrameworkCore.Metadata.IModel _model = null;

        protected override void OnVisit(Config m)
        {
            this._model = null;
            this._config = m;

            // if (this._config.IdDbGenerator.IsSequenceHiLo ?? false)
            //    this._modelBuilder.ForSqlServerUseSequenceHiLo(this._config.IdDbGenerator.HiLoSequenceName, this._config.IdDbGenerator.HiLoSchema);
            // else
            //    this._modelBuilder.ForSqlServerUseIdentityColumns();
        }

        protected override void OnVisit(Property m)
        {
            throw new NotImplementedException();
        }

        protected override void OnVisit(Constant m)
        {
            throw new NotImplementedException();
        }

        protected override void OnVisit(Enumeration m)
        {
            throw new NotImplementedException();
        }

        protected override void OnVisit(Catalog m)
        {
            // this._modelBuilder.Entity(typeof(Catalog).Name + "." + m.Name, x =>
            //   {
            //       x.HasKey(new string[] { this._config.PrimaryKeyName }).ForSqlServerIsClustered(m.IsPrimaryKeyClustered ?? this._config.IsPrimaryKeyClustered);
            //       x.ForSqlServerIsMemoryOptimized(m.IsMemoryOptimized ?? this._config.IsMemoryOptimized);
            //       foreach (var t in m.ListProperties)
            //       {
            //           switch (t.DataType.DataTypeEnum)
            //           {
            //               case Proto.Config.proto_data_type.Types.EnumDataType.Any: // any document, catalog
            //                   break;
            //               case Proto.Config.proto_data_type.Types.EnumDataType.Catalog:
            //                   x.Property(t.ClrType, "Ref" + t.DataType.ObjectName);
            //                   break;
            //               case Proto.Config.proto_data_type.Types.EnumDataType.Catalogs: // any catalog
            //                   break;
            //               case Proto.Config.proto_data_type.Types.EnumDataType.Document:
            //                   x.Property(t.ClrType, "Ref" + t.DataType.ObjectName);
            //                   break;
            //               case Proto.Config.proto_data_type.Types.EnumDataType.Documents: // any document
            //                   x.Property(t.ClrType, t.Name);
            //                   break;
            //               case Proto.Config.proto_data_type.Types.EnumDataType.Enumeration:
            //                   break;
            //               default:
            //                   x.Property(t.ClrType, t.Name);
            //                   break;
            //           }
            //       }
            //   });
        }
    }
}
