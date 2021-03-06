﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Microsoft.Extensions.Logging
{
    public static class Logger
    {
        public static ServiceProvider ServiceProvider;
        public static ILoggerProvider LogerProvider;
        public static ILogger CreateLogger<T>() => Logger.LogerProvider.CreateLogger(typeof(T).Name);
        public static ILogger CreateLogger(object obj) => Logger.LogerProvider.CreateLogger(obj.GetType().Name);
    }
}
