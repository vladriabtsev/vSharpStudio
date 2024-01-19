using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenVmFromProto
{
    public partial class NodeVisitor
    {
        readonly FileDescriptor root;
        readonly List<MessageDescriptor> messages = new List<MessageDescriptor>();
        public NodeVisitor(FileDescriptor root)
        {
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
    }
}
