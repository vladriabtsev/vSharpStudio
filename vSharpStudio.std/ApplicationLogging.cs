using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace vSharpStudio.std
{
    public static class ApplicationLogging
    {
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
        public static ILogger CreateLogger<T>() =>
          ApplicationLogging.LoggerFactory.CreateLogger<T>();
    }
}
