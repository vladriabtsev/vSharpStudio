using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class Clone
    {
        readonly string nameSpace;
        readonly string protoNameSpace;
        readonly FileDescriptor root;
        readonly MessageDescriptor message;
        readonly MessageDoc Doc;
        public Clone(FileDescriptor root, MessageDescriptor message, string destNS, string protoNS)
        {
            this.root = root;
            this.message = message;
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name];
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
        }
    }
}
