using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.DbModels
{
    public partial class MsSqlModelBuilderVisitor : IVisitorConfigNode
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

        CancellationToken IVisitorConfigNode.Token => throw new NotImplementedException();

        void IVisitorConfigNode.Visit(Config m)
        {
            this._model = null;
            this._config = m;
            if (this._config.IdDbGenerator.IsSequenceHiLo ?? false)
                this._modelBuilder.ForSqlServerUseSequenceHiLo(this._config.IdDbGenerator.HiLoSequenceName, this._config.IdDbGenerator.HiLoSchema);
            else
                this._modelBuilder.ForSqlServerUseIdentityColumns();
        }

        void IVisitorConfigNode.Visit(Property m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Constant m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Enumeration m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Catalog m)
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

        void IVisitorConfigNode.Visit(EnumerationPair m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Config m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Property m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Constant m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(EnumerationPair m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Enumeration m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Catalog m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupPropertiesTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupPropertiesTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(ConfigTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(ConfigTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupConfigs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupConfigs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Form p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Form p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(Report p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(Report p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListCatalogs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListCatalogs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupPropertyTab p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupPropertyTab p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupPropertyTabs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupPropertyTabs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupPropertyTabsTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupPropertyTabsTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListProperties p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListProperties p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListConstants p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListConstants p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListEnumerations p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListEnumerations p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListJournals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListJournals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListForms p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListForms p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.Visit(GroupListReports p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfigNode.VisitEnd(GroupListReports p)
        {
            throw new NotImplementedException();
        }
    }
}
