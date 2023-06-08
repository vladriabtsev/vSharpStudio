using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class NameSpace
    {
        readonly FileDescriptor root;
        readonly string nameSpace;
        readonly string defaultBaseClass = "ConfigObjectVmGenSettings";
        readonly string protoNameSpace;
        readonly List<MessageDescriptor> messages = new List<MessageDescriptor>();
        readonly Dictionary<string, List<MessageDescriptor>> dicParents = new Dictionary<string, List<MessageDescriptor>>();

        public NameSpace(FileDescriptor root, List<MessageDescriptor> messages,
            Dictionary<string, List<MessageDescriptor>> dicParents,
            string destNS, string protoNS, string defaultBaseClass)
        {
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
