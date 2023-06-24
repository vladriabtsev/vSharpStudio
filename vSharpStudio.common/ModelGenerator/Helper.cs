using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace vSharpStudio.common.ModelGenerator
{
    public class Helper
    {
        //public static string FilePos(string text = "",
        //                        [CallerFilePath] string file = "",
        //                        [CallerMemberName] string member = "",
        //                        [CallerLineNumber] int line = 0)
        //{
        //    return text + Path.GetFileName(file) + " Line: " + line;
        //}
        // public ModelBuilder CreateConventionBuilder()
        // {
        //    var contextServices = CreateContextServices();

        // var conventionSetBuilder = new CompositeConventionSetBuilder(
        //        contextServices.GetRequiredService<IEnumerable<IConventionSetBuilder>>().ToList());
        //    var conventionSet = contextServices.GetRequiredService<ICoreConventionSetBuilder>().CreateConventionSet();
        //    conventionSet = conventionSetBuilder.AddConventions(conventionSet);
        //    return new ModelBuilder(conventionSet);
        // }
        // public IServiceProvider CreateContextServices()
        //    => ((IInfrastructure<IServiceProvider>)CreateContext()).Instance;
        // public DbContext CreateContext()
        //    => new DbContext(CreateOptions(CreateServiceProvider()));
        // public DbContextOptions CreateOptions(IServiceProvider serviceProvider = null)
        // {
        //    var optionsBuilder = new DbContextOptionsBuilder()
        //        .UseInternalServiceProvider(serviceProvider);

        // UseProviderOptions(optionsBuilder);

        // return optionsBuilder.Options;
        // }
        // public IServiceProvider CreateServiceProvider(IServiceCollection customServices = null)
        //    => CreateServiceProvider(customServices, AddProviderServices);
        // public abstract IServiceCollection AddProviderServices(IServiceCollection services);
        // protected abstract void UseProviderOptions(DbContextOptionsBuilder optionsBuilder);
    }
}
