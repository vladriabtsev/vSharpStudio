using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Reflection;

namespace GenVmFromProto
{
    public partial class ModelInterfaces
    {
        MessageDoc? MessageDoc;
        FieldDoc? FieldDoc;
        readonly FileDescriptor root;
        readonly string nameSpace;
        readonly string protoNameSpace;
        readonly List<MessageDescriptor> messages = new List<MessageDescriptor>();
        readonly Dictionary<string, List<MessageDescriptor>> dicParents;
        readonly string Setter = "";

        public ModelInterfaces(FileDescriptor root, List<MessageDescriptor> messages,
            Dictionary<string, List<MessageDescriptor>> dicParents,
            string destNS, string protoNS)
        {
            this.root = root;
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
            this.messages = messages;
            this.dicParents = dicParents;
            Debug.Assert(Program.RunOptions != null);
            if (!Program.RunOptions.IsReadonly)
            //    Setter = "internal set; ";
            //else
                Setter = "set; ";
            //var files = JsonDoc.Files;

            //var dic = JsonDoc.Files[root.Name].Enums;

            //foreach(var t in dic)
            //{
            //    foreach (var tt in t.Value.Values)
            //    {
            //        var s = tt.Value.value.Number;
            //    }
            //}

            //foreach (var t in FieldDoc.en)
            //foreach(var t in root.EnumTypes)
            //{
            //    t.
            //}
        }
        bool IsSkip(FieldDescriptor field)
        {
            Debug.Assert(MessageDoc != null);
            if (MessageDoc.IsConfigObjectBase)
            {
                if (field.Name == "guid")
                    return true;
                if (field.Name == "name")
                    return true;
                if (field.Name == "sorting_value")
                    return true;
            }
            return false;
        }
    }
}
