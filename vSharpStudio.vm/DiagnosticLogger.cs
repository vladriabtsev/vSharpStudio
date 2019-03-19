using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using vSharpStudio.std;

namespace vSharpStudio.vm
{
    public class DiagnosticLogger<TCategory> : IDiagnosticsLogger<TCategory>
            where TCategory : LoggerCategory<TCategory>, new()
    {
        public ILoggingOptions Options => _iLoggingOptions;
        private ILoggingOptions _iLoggingOptions = new LoggingOptions();
        public DiagnosticSource DiagnosticSource => throw new NotImplementedException();

        ILogger IDiagnosticsLogger<TCategory>.Logger => _ilogger;
        private ILogger _ilogger = ApplicationLogging.CreateLogger<TCategory>();

        public WarningBehavior GetLogBehavior(EventId eventId, LogLevel logLevel)
        {
            return WarningBehavior.Log;
        }

        public bool ShouldLogSensitiveData()
        {
            return true;
        }
    }
    public class LoggingOptions : ILoggingOptions
    {
        public LoggingOptions(bool isSensitiveDataLoggingEnabled = false, bool isSensitiveDataLoggingWarned = true)
        {
            this.IsSensitiveDataLoggingEnabled = isSensitiveDataLoggingEnabled;
            this.IsSensitiveDataLoggingWarned = isSensitiveDataLoggingWarned;
            //this.WarningsConfiguration = new WarningsConfiguration();
        }
        public bool IsSensitiveDataLoggingEnabled { get; }

        public bool IsSensitiveDataLoggingWarned { get; set; }

        public WarningsConfiguration WarningsConfiguration => throw new NotImplementedException();

        public void Initialize(IDbContextOptions options)
        {
            throw new NotImplementedException();
        }

        public void Validate(IDbContextOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
