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
using Microsoft.Extensions.Logging;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Migration
{
    public class ConfigToModelVisitor : ConfigVisitor
    {
        public IMutableModel Result { get { return _builder.Model; } }
        ModelBuilder _builder;
        EntityTypeBuilder _entityBuilder;
        PropertyBuilder _propertyBuilder;

        public ConfigToModelVisitor()
        {
            _builder = new ModelBuilder(new ConventionSet());
        }
        public ConfigToModelVisitor(CancellationToken cancellationToken, ILogger logger = null) : base(cancellationToken, logger)
        {
            _builder = new ModelBuilder(new ConventionSet());
        }

        protected override void OnVisit(GroupListConstants p)
        {
            _entityBuilder = _builder.Entity(p.Name);
        }

        void VisitDataType(DataType p, string name)
        {
            var type = Type.GetType(p.ClrTypeName);
            _propertyBuilder = _entityBuilder.Property(type, name);
        }
        protected override void OnVisit(Constant p)
        {
            VisitDataType(p.DataType, p.Name);
        }

        protected override void OnVisit(Catalog p)
        {
            _entityBuilder = _builder.Entity(p.Name);
        }
    }
}
