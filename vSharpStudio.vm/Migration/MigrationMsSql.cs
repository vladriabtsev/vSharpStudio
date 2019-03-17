using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace vSharpStudio.vm.Migration
{
    class MigrationMsSql
    {
        //protected override MigrationsModelDiffer CreateModelDiffer(IModel model)
        //{
        //    var ctx = TestHelpers.CreateContext(
        //        TestHelpers.AddProviderOptions(new DbContextOptionsBuilder())
        //            .UseModel(model).EnableSensitiveDataLogging().Options);
        //    return new MigrationsModelDiffer(
        //        new TestRelationalTypeMappingSource(
        //            TestServiceFactory.Instance.Create<TypeMappingSourceDependencies>(),
        //            TestServiceFactory.Instance.Create<RelationalTypeMappingSourceDependencies>()),
        //        new MigrationsAnnotationProvider(
        //            new MigrationsAnnotationProviderDependencies()),
        //        ctx.GetService<IChangeDetector>(),
        //        ctx.GetService<StateManagerDependencies>(),
        //        ctx.GetService<CommandBatchPreparerDependencies>());
        //}
    }
}
