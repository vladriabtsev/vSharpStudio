using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class Property
    {
        FileDescriptor root;
        MessageDescriptor message;
        FieldDescriptor field;
        public Property(FileDescriptor root, MessageDescriptor message, FieldDescriptor field)
        {
            this.root = root;
            this.message = message;
            this.field = field;
        }
        public string GetAttrs()
        {
            string res = "";
            if (NameSpace.modelData.DicByClass.ContainsKey(message.Name.ToNameCs()))
            {
                var t = NameSpace.modelData.DicByClass[message.Name.ToNameCs()];
                if (t.DicByProperty.ContainsKey(field.Name.ToNameCs()))
                {
                    res = t.DicByProperty[field.Name.ToNameCs()];
                    res = res.Substring(0, res.LastIndexOf("\r\n"));
                }
            }
            return res;
        }
    }
}
