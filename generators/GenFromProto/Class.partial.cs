using Google.Protobuf.Reflection;
using Microsoft.Extensions.Logging;
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
        MessageDoc Doc;
        string nameSpace;
        string protoNameSpace;

        Dictionary<string, List<MessageDescriptor>> dicParents;
        public Class(FileDescriptor root, MessageDescriptor message, Dictionary<string, List<MessageDescriptor>> dicParents, 
            string destNS, string protoNS, string defaultBaseClass)
        {
            var _logger = Logger.CreateLogger(this);
            this.root = root;
            this.message = message;
            this.dicParents = dicParents;
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
            //_logger.LogInformation("Message {Name}".CallerInfo(), message.Name);
            _logger.LogInformation("Message {0}".CallerInfo(), message.Name);
            if (!JsonDoc.Files.ContainsKey(root.Name))
            {
            }
            if (!JsonDoc.Files[root.Name].Messages.ContainsKey(message.Name))
            {
            }
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name];
            //TODO implement generation standard base class name from simple name for: ViewModelBindable, ViewModelEditable, ViewModelValidatable, ViewModelValidatableWithSeverity
            _logger.LogInformation("Base class from doc '{Name}'".CallerInfo(), this.Doc.BaseClass);
            if (this.Doc.BaseClass == "")
            {
                this.Doc.BaseClass = " : "+ defaultBaseClass + "<" + message.Name.ToNameCs() + ", " + message.Name.ToNameCs() + "." +
                    message.Name.ToNameCs() + "Validator>, IComparable<" + message.Name.ToNameCs() + ">, I" + root.Package.ToNameCs() + "AcceptVisitor";
            }
            else if (this.Doc.BaseClass == "ViewModelBindable")
            {
                this.Doc.BaseClass = " : " + this.Doc.BaseClass + "<" + message.Name.ToNameCs() + ">";
            }
            else if (this.Doc.BaseClass == "ViewModelEditable")
            {
                this.Doc.BaseClass = " : " + this.Doc.BaseClass + "<" + message.Name.ToNameCs() + ">";
            }
            else if (this.Doc.BaseClass == "ViewModelValidatable")
            {
                this.Doc.BaseClass = " : " + this.Doc.BaseClass + "<" + message.Name.ToNameCs() + ", " + message.Name.ToNameCs() + "." +
                    message.Name.ToNameCs() + "Validator>";
            }
            else if (this.Doc.BaseClass == "ViewModelValidatableWithSeverity")
            {
                this.Doc.BaseClass = " : " + this.Doc.BaseClass + "<" + message.Name.ToNameCs() + ", " + message.Name.ToNameCs() + "." +
                    message.Name.ToNameCs() + "Validator>";
            }
            else
            {

            }
        }
    }
}
