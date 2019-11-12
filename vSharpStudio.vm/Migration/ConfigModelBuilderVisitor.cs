using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.Migration
{
    // public partial class ConfigModelBuilderVisitor : IVisitorConfigNode
    // {
    //    private ModelBuilder _modelBuilder = null;

    // CancellationToken IVisitorConfigNode.Token => throw new NotImplementedException();

    // //public ConfigModelBuilderVisitor(ModelBuilder modelBuilder)
    //    //{
    //    //    _modelBuilder = modelBuilder;
    //    //}
    //    void IVisitorConfigNode.Visit(Config m)
    //    {
    //    }

    // void IVisitorConfigNode.Visit(Property m)
    //    {
    //    }

    // void IVisitorConfigNode.Visit(Constant m)
    //    {
    //    }

    // void IVisitorConfigNode.Visit(Enumeration m)
    //    {
    //    }

    // void IVisitorConfigNode.Visit(Catalog m)
    //    {
    //        //var c = _modelBuilder.Entity((m as ITreeConfigNode).Name, x =>
    //        //{
    //        //    foreach (var t in m.ListProperties)
    //        //    {
    //        //        switch (t.DataType.DataTypeEnum)
    //        //        {
    //        //            case Proto.Config.proto_data_type.Types.EnumDataType.Any: // any document, catalog
    //        //                break;
    //        //            case Proto.Config.proto_data_type.Types.EnumDataType.Catalog:
    //        //                x.Property(t.ClrType, "Ref" + t.DataType.ObjectName);
    //        //                break;
    //        //            case Proto.Config.proto_data_type.Types.EnumDataType.Catalogs: // any catalog
    //        //                break;
    //        //            case Proto.Config.proto_data_type.Types.EnumDataType.Document:
    //        //                x.Property(t.ClrType, "Ref" + t.DataType.ObjectName);
    //        //                break;
    //        //            case Proto.Config.proto_data_type.Types.EnumDataType.Documents: // any document
    //        //                x.Property(t.ClrType, (t as ITreeConfigNode).Name);
    //        //                break;
    //        //            case Proto.Config.proto_data_type.Types.EnumDataType.Enumeration:
    //        //                break;
    //        //            default:
    //        //                x.Property(t.ClrType, (t as ITreeConfigNode).Name);
    //        //                break;
    //        //        }
    //        //    }
    //        //});
    //    }

    // void IVisitorConfigNode.Visit(EnumerationPair m)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Document p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Journal p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupDocuments p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(ConfigTree p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupConfigs p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListDocuments p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Form p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(Report p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListCatalogs p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListProperties p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListConstants p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListEnumerations p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListJournals p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListForms p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListReports p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupPropertiesTab p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.Visit(GroupListPropertiesTabs p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Config m)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Property m)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Constant m)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(EnumerationPair m)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Enumeration m)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Catalog m)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Document p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Journal p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupDocuments p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(ConfigTree p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupConfigs p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListDocuments p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Form p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(Report p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListCatalogs p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListProperties p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListConstants p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListEnumerations p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListJournals p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListForms p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListReports p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupPropertiesTab p)
    //    {
    //        throw new NotImplementedException();
    //    }

    // void IVisitorConfigNode.VisitEnd(GroupListPropertiesTabs p)
    //    {
    //        throw new NotImplementedException();
    //    }
    // }
}
