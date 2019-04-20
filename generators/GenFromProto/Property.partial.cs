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
    public partial class Property
    {
        private static Proto.Attr.DicClassPropAttrs dicAttr = null;
        FileDescriptor root;
        MessageDescriptor message;
        FieldDescriptor field;
        public Property(FileDescriptor root, MessageDescriptor message, FieldDescriptor field)
        {
            this.root = root;
            this.message = message;
            this.field = field;
            if (dicAttr == null)
            {
                string path = "../../Attr.json";
                dicAttr = new Proto.Attr.DicClassPropAttrs();
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    dicAttr = Proto.Attr.DicClassPropAttrs.Parser.ParseJson(json);
                }
            }
        }
        public string GetAttrs()
        {
            string res = "";
            if (dicAttr.DicByClass.ContainsKey(message.Name.ToNameCs()))
            {
                var t = dicAttr.DicByClass[message.Name.ToNameCs()];
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
