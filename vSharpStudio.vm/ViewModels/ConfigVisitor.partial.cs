using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigVisitor
    {
        public ConfigVisitor()
        {

        }

        private ILogger? _logger = null;

        public ConfigVisitor(CancellationToken cancellationToken, ILogger? logger = null)
        {
            this._cancellationToken = cancellationToken;
            this._logger = logger;
        }
    }
}
