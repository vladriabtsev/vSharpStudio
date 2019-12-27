using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace GenFromProto
{
    public static class LoggerInit
    {
        public static void Init()
        {
            if (Logger.LogerProvider != null)
                return;
            //if (Logger.ServiceProvider != null)
            //    return;
            //var providers = new LoggerProviderCollection();
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File("log.txt", rollingInterval: Serilog.RollingInterval.Day)
                // .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
            //var services = new ServiceCollection();
            //services.AddSingleton(providers);
            //services.AddSingleton<ILoggerFactory>(sc =>
            //{
            //    var providerCollection = sc.GetService<LoggerProviderCollection>();
            //    var factory = new SerilogLoggerFactory(null, true, providerCollection);

            //    foreach (var provider in sc.GetServices<ILoggerProvider>())
            //        factory.AddProvider(provider);

            //    return factory;
            //});
            //services.AddLogging(l => l.AddSerilog());
            //var serviceProvider = services.BuildServiceProvider();
            ////var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            //Logger.ServiceProvider = serviceProvider;

            Logger.LogerProvider = new SerilogLoggerProvider(Log.Logger);
        }
    }
}
