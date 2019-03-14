using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace vSharpStudio.Utils
{
    public class Logger : Microsoft.EntityFrameworkCore.Diagnostics.IDiagnosticsLogger<DbLoggerCategory.Scaffolding>
    {
        public ILoggingOptions Options
        {
            get
            {
                if (_options == null)
                    _options = new LoggingOptions();
                return _options;
            }
        }
        private ILoggingOptions _options;

        public System.Diagnostics.DiagnosticSource DiagnosticSource => throw new NotImplementedException();

        Microsoft.Extensions.Logging.ILogger IDiagnosticsLogger<DbLoggerCategory.Scaffolding>.Logger => throw new NotImplementedException();

        public WarningBehavior GetLogBehavior(Microsoft.Extensions.Logging.EventId eventId, Microsoft.Extensions.Logging.LogLevel logLevel)
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

        public void Initialize([NotNull] IDbContextOptions options)
        {
            throw new NotImplementedException();
        }

        public void Validate([NotNull] IDbContextOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
