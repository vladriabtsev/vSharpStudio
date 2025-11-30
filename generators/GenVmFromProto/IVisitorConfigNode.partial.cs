using System.Collections.Generic;
using ApplicationLogging;
using Google.Protobuf.Reflection;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace GenVmFromProto
{
    public partial class IVisitorConfigNode
    {
        private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(IVisitorConfigNode));
        readonly FileDescriptor root;
        readonly List<MessageDescriptor> messages = [];
        public IVisitorConfigNode(FileDescriptor root)
        {
            _logger?.Debug("Create for '{0}'", root.Name);
            this.root = root;
            CollectMessages(root);
        }
        private void CollectMessages(FileDescriptor root)
        {
            foreach (var t in root.MessageTypes)
            {
                if (t.Name.EndsWith("_nullable"))
                    continue;
                if (!JsonDoc.Files[root.Name].Messages[t.Name].IsDefaultBase)
                    continue;
                this.messages.Add(t);
            }
        }
    }
}
