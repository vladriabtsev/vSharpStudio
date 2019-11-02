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
using vSharpStudio.common;
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
            // https://docs.microsoft.com/en-us/dotnet/api/system.type.gettype?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev16.query%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Type.GetType);k(SolutionItemsProject);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.7.2);k(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.8
            Type type = p.ClrType;
            _propertyBuilder = _entityBuilder.Property(type, name);
        }
        protected override void OnVisit(Constant p)
        {
            var pp = (Constant)p;
            VisitDataType(pp.DataType, pp.Name);
        }

        protected override void OnVisit(Catalog p)
        {
            _entityBuilder = _builder.Entity(p.Name);
        }
    }
}
