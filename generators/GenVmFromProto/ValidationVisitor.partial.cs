using System.Collections.Generic;
using ApplicationLogging;
using Google.Protobuf.Reflection;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace GenVmFromProto
{
    public partial class ValidationVisitor
    {
        private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(ValidationVisitor));
        readonly FileDescriptor root;
        readonly List<MessageDescriptor> messages = new List<MessageDescriptor>();
        public ValidationVisitor(FileDescriptor root)
        {
            _logger?.Debug("Create validation visitors for '{0}'", root.Name);
            this.root = root;
            CollectMessages(root);
        }
        private void CollectMessages(FileDescriptor root)
        {
            foreach (var t in root.MessageTypes)
            {
                if (t.Name.EndsWith("_nullable"))
                    continue;
                //if (!JsonDoc.Files[root.Name].Messages[t.Name].BaseClass.StartsWith(" : ConfigObjectBase"))
                //    continue;
                this.messages.Add(t);
            }
        }
        private bool IsBaseWithParent(string mesName)
        {
            var res = JsonDoc.Files[root.Name].Messages[mesName].IsConfigObjectBase;
            return res;
        }
    }
}
