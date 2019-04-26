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
        internal static Proto.Attr.ModelData modelData = null;
        FileDescriptor root;
        List<MessageDescriptor> messages = new List<MessageDescriptor>();
        Dictionary<string, List<MessageDescriptor>> dicParents = new Dictionary<string, List<MessageDescriptor>>();
        public NameSpace(FileDescriptor root)
        {
            this.root = root;
            //var kuku = root.FindTypeByName<MessageDescriptor>("CommentMessage");
            CollectMessages(root);
            if (modelData == null)
            {
                string path = "../../Attr.json";
                modelData = new Proto.Attr.ModelData();
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    modelData = Proto.Attr.ModelData.Parser.ParseJson(json);
                }
            }
        }
        private void CollectMessages(FileDescriptor root)
        {
            foreach (var t in root.MessageTypes)
            {
                if (t.Name.EndsWith("_nullable"))
                    continue;
                messages.Add(t);
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
