using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class Class
    {
        FileDescriptor root;
        MessageDescriptor message;
        string baseClass = "";
        string interfaces = "";
        bool isNewBaseClass = false;

        Dictionary<string, List<MessageDescriptor>> dicParents;
        public Class(FileDescriptor root, MessageDescriptor message, Dictionary<string, List<MessageDescriptor>> dicParents)
        {
            this.root = root;
            this.message = message;
            this.dicParents = dicParents;
            //foreach (var t in message.Fields.InDeclarationOrder())
            //{
            //    t.Declaration.ToLeadingDetachedComments("");
            //}
            this.baseClass = "ConfigObjectBase<" + message.Name.ToNameCs() + ", " + message.Name.ToNameCs() + "." + 
                message.Name.ToNameCs() + "Validator>, IComparable<" + message.Name.ToNameCs() + ">, IAccept";
            if (NameSpace.modelData.DicByClass.ContainsKey(message.Name.ToNameCs()))
            {
                var t = NameSpace.modelData.DicByClass[message.Name.ToNameCs()];
                if (!string.IsNullOrWhiteSpace(t.BaseClass))
                {
                    this.baseClass = t.BaseClass;
                    this.isNewBaseClass = true;
                }
                if (!string.IsNullOrWhiteSpace(t.Interfaces))
                {
                    this.interfaces = t.Interfaces;
                }
            }
        }
    }
}
