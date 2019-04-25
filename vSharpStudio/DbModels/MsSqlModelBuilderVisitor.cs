﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.DbModels
{
    public partial class MsSqlModelBuilderVisitor : IVisitorConfig
    {
        private ModelBuilder _modelBuilder = null;
        private Config _config = null;
        public MsSqlModelBuilderVisitor()
        {
        }
        public Microsoft.EntityFrameworkCore.Metadata.IModel GetModel()
        {
            if (this._model == null)
            {
                this._model = _modelBuilder.FinalizeModel();
            }
            return this._model;
        }
        private Microsoft.EntityFrameworkCore.Metadata.IModel _model = null;

        CancellationToken IVisitorConfig.Token => throw new NotImplementedException();

        void IVisitorConfig.Visit(Config m)
        {
            this._model = null;
            this._config = m;
            if (this._config.IsSequenceHiLo)
                this._modelBuilder.ForSqlServerUseSequenceHiLo(this._config.HiLoSequenceName, this._config.HiLoSchema);
            else
                this._modelBuilder.ForSqlServerUseIdentityColumns();
        }

        void IVisitorConfig.Visit(Property m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(DataType m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Constant m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Enumeration m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Catalog m)
        {
            //this._modelBuilder.Entity(typeof(Catalog).Name + "." + m.Name, x =>
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

        void IVisitorConfig.Visit(EnumerationPair m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Config m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Property m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(DataType m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Constant m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(EnumerationPair m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Enumeration m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Catalog m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupPropertiesTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupPropertiesTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(ConfigTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(ConfigTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupConfigs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupConfigs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Form p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Form p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Report p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Report p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListCatalogs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListCatalogs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupPropertyTab p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupPropertyTab p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupPropertyTabs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupPropertyTabs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupPropertyTabsTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupPropertyTabsTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListProperties p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListProperties p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListConstants p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListConstants p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListEnumerations p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListEnumerations p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListJournals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListJournals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListForms p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListForms p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListReports p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListReports p)
        {
            throw new NotImplementedException();
        }
    }
}
