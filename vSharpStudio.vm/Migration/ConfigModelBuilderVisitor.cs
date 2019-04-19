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

        void IVisitorConfig.Visit(DataType m)
        {
        }

        void IVisitorConfig.Visit(Properties m)
        {
        }

        void IVisitorConfig.Visit(Constant m)
        {
        }

        void IVisitorConfig.Visit(Constants m)
        {
        }

        void IVisitorConfig.Visit(Enumeration m)
        {
        }

        void IVisitorConfig.Visit(Enumerations m)
        {
        }

        void IVisitorConfig.Visit(Catalog m)
        {
            var c = _modelBuilder.Entity((m as ITreeConfigNode).Name, x =>
            {
                foreach (var t in m.ListProperties)
                {
                    switch (t.DataType.DataTypeEnum)
                    {
                        case Proto.Config.proto_data_type.Types.EnumDataType.Any: // any document, catalog
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Catalog:
                            x.Property(t.ClrType, "Ref" + t.DataType.ObjectName);
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Catalogs: // any catalog
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Document:
                            x.Property(t.ClrType, "Ref" + t.DataType.ObjectName);
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Documents: // any document
                            x.Property(t.ClrType, (t as ITreeConfigNode).Name);
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Enumeration:
                            break;
                        default:
                            x.Property(t.ClrType, (t as ITreeConfigNode).Name);
                            break;
                    }
                }
            });
        }

        void IVisitorConfig.Visit(Catalogs m)
        {
        }

        void IVisitorConfig.Visit(EnumerationPair m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Documents p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(Journals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.Visit(GroupPropertiesTree p)
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

        void IVisitorConfig.VisitEnd(Properties m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Constant m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Constants m)
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

        void IVisitorConfig.VisitEnd(Enumerations m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Catalog m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Catalogs m)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Document p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Documents p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Journal p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(Journals p)
        {
            throw new NotImplementedException();
        }

        void IVisitorConfig.VisitEnd(GroupPropertiesTree p)
        {
            throw new NotImplementedException();
        }
    }
}
