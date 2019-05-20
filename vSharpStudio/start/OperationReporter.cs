using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.Extensions.Logging;

namespace vSharpStudio.start
{
    public class OperationReporter : IOperationReporter
    {
        ILogger _Logger;
        public OperationReporter(ILoggerFactory loggerFactory)
        {
            _Logger=loggerFactory.CreateLogger<OperationReporter>();
        }
        public void WriteError(string message)
        {
            _Logger.LogError(message);
        }

        public void WriteInformation(string message)
        {
            _Logger.LogInformation(message);
        }

        public void WriteVerbose(string message)
        {
            _Logger.LogTrace(message);
        }

        public void WriteWarning(string message)
        {
            _Logger.LogWarning(message);
        }
    }
}
