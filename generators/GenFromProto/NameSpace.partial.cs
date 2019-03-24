using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class NameSpace
    {
        FileDescriptor root;
        Dictionary<string, List<MessageDescriptor>> dicParents = new Dictionary<string, List<MessageDescriptor>>();
        public NameSpace(FileDescriptor root)
        {
            this.root = root;
            foreach (var t in root.MessageTypes)
            {
                if (t.Name.EndsWith("_nullable"))
                    continue;
                foreach (var tt in t.Fields.InDeclarationOrder())
                {
                    if (tt.FieldType != Google.Protobuf.Reflection.FieldType.Message)
                        continue;
                    if (tt.MessageType.Name.EndsWith("_nullable"))
                        continue;
                    if (tt.IsMap)
                    {
                        throw new NotImplementedException();
                    }
                    if (!dicParents.ContainsKey(tt.MessageType.Name))
                    {
                        dicParents[tt.MessageType.Name] = new List<MessageDescriptor>();
                    }
                    dicParents[tt.MessageType.Name].Add(t);
                }
            }
        }
    }
}
