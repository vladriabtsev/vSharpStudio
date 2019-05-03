using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.Migration
{
    public partial class ConfigModelBuilderVisitor : IVisitorConfig
    {
        private ModelBuilder _modelBuilder = null;

        CancellationToken IVisitorConfig.Token => throw new NotImplementedException();

        //public ConfigModelBuilderVisitor(ModelBuilder modelBuilder)
        //{
        //    _modelBuilder = modelBuilder;
        //}
        void IVisitorConfig.Visit(Config m)
        {
        }

        void IVisitorConfig.Visit(Property m)
        {
        }

        void IVisitorConfig.Visit(Constant m)
        {
        }

        void IVisitorConfig.Visit(Enumeration m)
        {
        }

        void IVisitorConfig.Visit(Catalog m)
        {
            //var c = _modelBuilder.Entity((m as ITreeConfigNode).Name, x =>
            //{
            //    foreach (var t in m.ListProperties)
            //    {
            //        switch (t.DataType.DataTypeEnum)
            //        {
            //            case Proto.Config.proto_data_type.Types.EnumDataType.Any: // any document, catalog
            //                break;
            //            case Proto.Config.proto_data_type.Types.EnumDataType.Catalog:
            //                x.Property(t.ClrType, "Ref" + t.DataType.ObjectName);
            //                break;
            //            case Proto.Config.proto_data_type.Types.EnumDataType.Catalogs: // any catalog
            //                break;
            //            case Proto.Config.proto_data_type.Types.EnumDataType.Document:
            //                x.Property(t.ClrType, "Ref" + t.DataType.ObjectName);
            //                break;
            //            case Proto.Config.proto_data_type.Types.EnumDataType.Documents: // any document
            //                x.Property(t.ClrType, (t as ITreeConfigNode).Name);
            //                break;
            //            case Proto.Config.proto_data_type.Types.EnumDataType.Enumeration:
            //                break;
            //            default:
            //                x.Property(t.ClrType, (t as ITreeConfigNode).Name);
            //                break;
            //        }
            //    }
            //});
        }

        void IVisitorConfig.Visit(EnumerationPair m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupPropertiesTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(ConfigTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupConfigs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Form p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Report p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListCatalogs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupPropertyTab p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupPropertyTabs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupPropertyTabsTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListProperties p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListConstants p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListEnumerations p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListJournals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListForms p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupListReports p)
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

        void IVisitorConfig.VisitEnd(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupPropertiesTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(ConfigTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupConfigs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListDocuments p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Form p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Report p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListCatalogs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupPropertyTab p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupPropertyTabs p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupPropertyTabsTree p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListProperties p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListConstants p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListEnumerations p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListJournals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListForms p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupListReports p)
        {
            throw new NotImplementedException();
        }

    }
}
