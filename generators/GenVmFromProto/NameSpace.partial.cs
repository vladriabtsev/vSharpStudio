using System.Collections.Generic;
using ApplicationLogging;
using Google.Protobuf.Reflection;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace GenVmFromProto
{
    public partial class NameSpace
    {
        private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(NameSpace));
        readonly FileDescriptor root;
        readonly string nameSpace;
        readonly string defaultBaseClass = "ConfigObjectVmGenSettings";
        readonly string protoNameSpace;
        readonly List<MessageDescriptor> messages = new List<MessageDescriptor>();
        readonly Dictionary<string, List<MessageDescriptor>> dicParents = new Dictionary<string, List<MessageDescriptor>>();

        public NameSpace(FileDescriptor root, List<MessageDescriptor> messages,
            Dictionary<string, List<MessageDescriptor>> dicParents,
            string destNS, string protoNS, string? defaultBaseClass)
        {
            _logger?.Debug("Create models for '{0}'", root.Name);
            this.root = root;
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
            if (defaultBaseClass != null)
                this.defaultBaseClass = defaultBaseClass;
            this.messages = messages;
            this.dicParents = dicParents;
        }
    }
}
