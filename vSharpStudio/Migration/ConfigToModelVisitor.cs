using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Migration
{
    public class ConfigToModelVisitor : IVisitorConfigNode
    {
        public IMutableModel Result { get { return _builder.Model; } }
        ModelBuilder _builder;
        EntityTypeBuilder _entityBuilder;
        PropertyBuilder _propertyBuilder;

        public ConfigToModelVisitor()
        {
            _builder = new ModelBuilder(new ConventionSet());
        }
        CancellationToken IVisitorConfigNode.Token => throw new NotImplementedException();

        void IVisitorConfigNode.Visit(GroupConfigs p)
        {
        }

        void IVisitorConfigNode.Visit(ConfigTree p)
        {
        }

        void IVisitorConfigNode.Visit(Config p)
        {
        }

        void IVisitorConfigNode.Visit(GroupListPropertiesTabs p)
        {
        }

        void IVisitorConfigNode.Visit(GroupPropertiesTab p)
        {
        }

        void IVisitorConfigNode.Visit(GroupListProperties p)
        {
        }

        void IVisitorConfigNode.Visit(Property p)
        {
            VisitDataType(p.DataType, p.Name);
        }

        void IVisitorConfigNode.Visit(GroupListConstants p)
        {
            _entityBuilder = _builder.Entity(p.Name);
        }

        void VisitDataType(DataType p, string name)
        {
            var type = Type.GetType(p.ClrType);
            _propertyBuilder = _entityBuilder.Property(type, name);
        }
        void IVisitorConfigNode.Visit(Constant p)
        {
            VisitDataType(p.DataType, p.Name);
        }

        void IVisitorConfigNode.Visit(GroupListEnumerations p)
        {
        }

        void IVisitorConfigNode.Visit(Enumeration p)
        {
        }

        void IVisitorConfigNode.Visit(EnumerationPair p)
        {
        }

        void IVisitorConfigNode.Visit(Catalog p)
        {
            _entityBuilder = _builder.Entity(p.Name);
        }

        void IVisitorConfigNode.Visit(GroupListCatalogs p)
        {
        }

        void IVisitorConfigNode.Visit(GroupDocuments p)
        {
        }

        void IVisitorConfigNode.Visit(Document p)
        {
        }

        void IVisitorConfigNode.Visit(GroupListDocuments p)
        {
        }

        void IVisitorConfigNode.Visit(GroupListJournals p)
        {
        }

        void IVisitorConfigNode.Visit(Journal p)
        {
        }

        void IVisitorConfigNode.Visit(GroupListForms p)
        {
        }

        void IVisitorConfigNode.Visit(Form p)
        {
        }

        void IVisitorConfigNode.Visit(GroupListReports p)
        {
        }

        void IVisitorConfigNode.Visit(Report p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupConfigs p)
        {
        }

        void IVisitorConfigNode.VisitEnd(ConfigTree p)
        {
        }

        void IVisitorConfigNode.VisitEnd(Config p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupListPropertiesTabs p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupPropertiesTab p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupListProperties p)
        {
        }

        void IVisitorConfigNode.VisitEnd(Property p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupListConstants p)
        {
        }

        void IVisitorConfigNode.VisitEnd(Constant p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupListEnumerations p)
        {
        }

        void IVisitorConfigNode.VisitEnd(Enumeration p)
        {
        }

        void IVisitorConfigNode.VisitEnd(EnumerationPair p)
        {
        }

        void IVisitorConfigNode.VisitEnd(Catalog p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupListCatalogs p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupDocuments p)
        {
        }

        void IVisitorConfigNode.VisitEnd(Document p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupListDocuments p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupListJournals p)
        {
        }

        void IVisitorConfigNode.VisitEnd(Journal p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupListForms p)
        {
        }

        void IVisitorConfigNode.VisitEnd(Form p)
        {
        }

        void IVisitorConfigNode.VisitEnd(GroupListReports p)
        {
        }

        void IVisitorConfigNode.VisitEnd(Report p)
        {
        }
    }
}
