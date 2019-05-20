using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using vSharpStudio.std;

namespace vSharpStudio.start
{
    public sealed class StartServices
    {
        public static StartServices Singleton { get { return _Singleton; } }
        private static StartServices _Singleton = new StartServices();

        private IServiceCollection services;
        private ServiceProvider serviceProvider;
        public ServiceProvider ServiceProvider { get { return serviceProvider; } }
        private StartServices()
        {
            services = new ServiceCollection();

            services.AddSingleton<ILoggerFactory>(ApplicationLogging.LoggerFactory)
                .AddSingleton<IOperationReporter, OperationReporter>()
                .AddSingleton<ICandidateNamingService, CandidateNamingService>()
                .AddSingleton<IPluralizer, NullPluralizer>()
                .AddSingleton<ICSharpUtilities, CSharpUtilities>()
                .AddSingleton<IScaffoldingTypeMapper, ScaffoldingTypeMapper>()
                .AddSingleton<IChangeDetector, ChangeDetector>();


            serviceProvider = services.BuildServiceProvider();
            //var myService = serviceProvider.GetService<MyService>();
        }
    }
}
