using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Reflection;

namespace GenFromProto
{
    partial class Program
    {
        private static List<MessageDescriptor> CollectMessages(FileDescriptor root, Dictionary<string, List<MessageDescriptor>> dicParents)
        {
            List<MessageDescriptor> messages = new List<MessageDescriptor>();
            foreach (var t in root.MessageTypes)
            {
                if (t.Name.EndsWith("_nullable"))
                    continue;
                messages.Add(t);
                foreach (var tt in t.Fields.InDeclarationOrder())
                {
                    if (!tt.IsMessage())
                        continue;
                    if (tt.MessageType.Name.EndsWith("_nullable"))
                        continue;
                    if (tt.IsMap)
                        continue;
                    if (!dicParents.ContainsKey(tt.MessageType.Name))
                    {
                        dicParents[tt.MessageType.Name] = new List<MessageDescriptor>();
                    }
                    dicParents[tt.MessageType.Name].Add(t);
                }
            }
            return messages;
        }
    }
}
