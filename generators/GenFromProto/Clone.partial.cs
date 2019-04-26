using Google.Protobuf.Reflection;
using Proto.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFromProto
{
    public partial class Clone
    {
        FileDescriptor root;
        MessageDescriptor message;
        bool isNewBaseClass = false;
        public Clone(FileDescriptor root, MessageDescriptor message)
        {
            this.root = root;
            this.message = message;
            if (NameSpace.modelData.DicByClass.ContainsKey(message.Name.ToNameCs()))
            {
                var t = NameSpace.modelData.DicByClass[message.Name.ToNameCs()];
                if (!string.IsNullOrWhiteSpace(t.BaseClass))
                {
                    this.isNewBaseClass = true;
                }
            }
        }
    }
}
