using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApplicationLogging
{
    public static class ApplicationLogging
    {
        // https://docs.microsoft.com/en-us/aspnet/core/migration/logging-nonaspnetcore?view=aspnetcore-2.2
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2

        // https://msdn.microsoft.com/en-us/magazine/mt694089.aspx

        // https://nblumhardt.com/2017/08/use-serilog/
        // https://andrewlock.net/creating-a-rolling-file-logging-provider-for-asp-net-core-2-0/
        // https://msdn.microsoft.com/en-us/magazine/mt830355.aspx EF
        // https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.DiagnosticSource/src/DiagnosticSourceUsersGuide.md
        //public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
        //public static ILogger CreateLogger<T>() =>
        //  ApplicationLogging.LoggerFactory.CreateLogger<T>();

        public static ILoggerProvider LogerProvider;
        public static ILogger CreateLogger<T>() => ApplicationLogging.LogerProvider.CreateLogger(typeof(T).Name);
        public static ILogger CreateLogger(string category) => ApplicationLogging.LogerProvider.CreateLogger(category);
    }
}
