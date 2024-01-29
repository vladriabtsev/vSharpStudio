using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using CommunityToolkit.Diagnostics;
using ApplicationLogging;

namespace GenVmFromProto
{
    public static class LoggerInit
    {
        public static void Init(string? logFilePath)
        {
            if (Logger.LoggerProvider != null)
                return;
            //if (Logger.ServiceProvider != null)
            //    return;
            //var providers = new LoggerProviderCollection();
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            if (logFilePath != null)
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.File(logFilePath, Serilog.Events.LogEventLevel.Verbose, rollingInterval: Serilog.RollingInterval.Day)
                    .WriteTo.Debug(Serilog.Events.LogEventLevel.Warning)
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Warning()
                    .WriteTo.Debug()
                    .CreateLogger();
            }
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

            Logger.LoggerProvider = new SerilogLoggerProvider(Log.Logger);
        }
    }
}
