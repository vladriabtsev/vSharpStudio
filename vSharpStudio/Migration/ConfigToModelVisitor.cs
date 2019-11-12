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
        public IMutableModel Result
        {
            get
            {
                return this._builder.Model;
            }
        }

        private ModelBuilder _builder;
        private EntityTypeBuilder _entityBuilder;
        private PropertyBuilder _propertyBuilder;

        public ConfigToModelVisitor()
        {
            this._builder = new ModelBuilder(new ConventionSet());
        }

        public ConfigToModelVisitor(CancellationToken cancellationToken, ILogger logger = null)
            : base(cancellationToken, logger)
        {
            this._builder = new ModelBuilder(new ConventionSet());
        }

        protected override void OnVisit(GroupListConstants p)
        {
            this._entityBuilder = this._builder.Entity(p.Name);
        }

        protected override void OnVisit(Constant p)
        {
            var pp = (Constant)p;
            this.VisitDataType(pp.DataType, pp.Name);
        }

        protected override void OnVisit(Catalog p)
        {
            this._entityBuilder = this._builder.Entity(p.Name);
        }

        private void VisitDataType(DataType p, string name)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.type.gettype?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev16.query%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Type.GetType);k(SolutionItemsProject);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.7.2);k(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.8
            Type type = p.ClrType;
            this._propertyBuilder = this._entityBuilder.Property(type, name);
        }
    }
}
