using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.vm.Migration
{
    class ConfigModelBuilderVisitor : IVisitorConfig
    {
        private ModelBuilder _modelBuilder = null;
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
            var c = _modelBuilder.Entity(m.Name, x=> {
                foreach (var t in m.Properties.ListProperties)
                {
                    switch (t.DataType.EnumDataType)
                    {
                        case Proto.Config.proto_data_type.Types.EnumDataType.Any: // any document, catalog
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Catalog:
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Catalogs: // any catalog
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Constant:
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Document:
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Documents: // any document
                            break;
                        case Proto.Config.proto_data_type.Types.EnumDataType.Enum:
                            break;
                        //case Proto.Config.proto_data_type.Types.EnumDataType.Enums:
                        //    break;
                        default:
                            x.Property(t.ClrType, t.Name);
                            break;
                    }
                }
            });
        }

        void IVisitorConfig.Visit(Catalogs m)
        {
        }

        void IVisitorConfig.Visit(Document m)
        {
        }

        void IVisitorConfig.Visit(Documents m)
        {
        }
    }
}
