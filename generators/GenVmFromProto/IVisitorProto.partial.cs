using ApplicationLogging;
using Google.Protobuf.Reflection;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace GenVmFromProto
{
    public partial class IVisitorProto
    {
        private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(IVisitorProto));
        readonly FileDescriptor root;
        readonly string nameSpace;
        readonly string protoNameSpace;

        public IVisitorProto(FileDescriptor root, string destNS, string protoNS)
        {
            _logger?.Debug("Create proto visitor for '{0}'", root.Name);
            this.root = root;
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
        }
    }
}
