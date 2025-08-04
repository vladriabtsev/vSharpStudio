using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogging;
using Google.Protobuf.Reflection;
using Microsoft.Extensions.Logging;

namespace GenVmFromProto
{
    public partial class Clone
    {
        readonly ILogger? _logger;
        readonly string nameSpace;
        readonly string protoNameSpace;
        readonly FileDescriptor root;
        readonly MessageDescriptor message;
        readonly MessageDoc Doc;
        public Clone(FileDescriptor root, MessageDescriptor message, string destNS, string protoNS)
        {
            _logger = AppLogger.CreateLogger<Clone>();
            _logger?.Debug("Clone for '{root}' message '{message}'", root.Name, message.Name);
            this.root = root;
            this.message = message;
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name];
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
        }
    }
}
