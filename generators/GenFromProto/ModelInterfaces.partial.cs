using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Reflection;

namespace GenFromProto
{
    public partial class ModelInterfaces
    {
        MessageDoc MessageDoc;
        FieldDoc FieldDoc;
        FileDescriptor root;
        string nameSpace;
        string protoNameSpace;
        List<MessageDescriptor> messages = new List<MessageDescriptor>();
        Dictionary<string, List<MessageDescriptor>> dicParents;
        string Setter = "";

        public ModelInterfaces(FileDescriptor root, List<MessageDescriptor> messages,
            Dictionary<string, List<MessageDescriptor>> dicParents,
            string destNS, string protoNS)
        {
            this.root = root;
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
            this.messages = messages;
            this.dicParents = dicParents;
            if (!Program.RunOptions.IsReadonly)
                Setter = "set; ";
        }
    }
}
