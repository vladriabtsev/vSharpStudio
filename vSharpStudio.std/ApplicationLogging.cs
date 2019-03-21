using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace vSharpStudio.std
{
    public static class ApplicationLogging
    {
        // https://msdn.microsoft.com/en-us/magazine/mt830355.aspx EF
        // https://msdn.microsoft.com/en-us/magazine/mt694089.aspx
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
        public static ILogger CreateLogger<T>() =>
          ApplicationLogging.LoggerFactory.CreateLogger<T>();
    }
}
