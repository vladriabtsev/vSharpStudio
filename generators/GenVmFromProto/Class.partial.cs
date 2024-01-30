﻿using ApplicationLogging;
using Google.Protobuf.Reflection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace GenVmFromProto
{
    public partial class Class
    {
        readonly FileDescriptor root;
        readonly MessageDescriptor message;
        readonly MessageDoc Doc;
        readonly string nameSpace;
        readonly string protoNameSpace;

        readonly Dictionary<string, List<MessageDescriptor>> dicParents;
        public Class(FileDescriptor root, MessageDescriptor message, Dictionary<string, List<MessageDescriptor>> dicParents,
            string destNS, string protoNS, string defaultBaseClass)
        {
            var _logger = Logger.CreateLogger(this);
            this.root = root;
            this.message = message;
            this.dicParents = dicParents;
            this.nameSpace = destNS;
            this.protoNameSpace = protoNS;
            _logger?.Information("Message {0}", message.Name);
            if (!JsonDoc.Files.ContainsKey(root.Name))
            {
            }
            if (!JsonDoc.Files[root.Name].Messages.ContainsKey(message.Name))
            {
            }
            this.Doc = JsonDoc.Files[root.Name].Messages[message.Name];
            _logger?.Information("Base class from doc '{Name}'", this.Doc.BaseClass);
            if (this.Doc.BaseClass == "")
            {
                this.Doc.BaseClass = " : " + defaultBaseClass + "<" + message.Name.ToNameCs() + ", " +
                    message.Name.ToNameCs() + "Validator>, IComparable<" + message.Name.ToNameCs() + ">, I" + root.Package.ToNameCs() + "AcceptVisitor";
            }
            else if (this.Doc.BaseClass == "VmBindable")
            {
                //this.Doc.BaseClass = " : " + this.Doc.BaseClass + "<" + message.Name.ToNameCs() + ">";
                this.Doc.BaseClass = " : VmBindable";
            }
            else if (this.Doc.BaseClass == "VmEditable")
            {
                this.Doc.BaseClass = " : VmEditable<" + message.Name.ToNameCs() + ">";
            }
            else if (this.Doc.BaseClass == "VmValidatable")
            {
                this.Doc.BaseClass = " : VmValidatable<" + message.Name.ToNameCs() + ", " +
                    message.Name.ToNameCs() + "Validator>";
            }
            else if (this.Doc.BaseClass == "VmValidatableWithSeverity")
            {
                this.Doc.BaseClass = " : VmValidatableWithSeverity<" + message.Name.ToNameCs() + ", " +
                    message.Name.ToNameCs() + "Validator>";
            }
            else if (this.Doc.BaseClass == "VmValidatableWithSeverityAndAttributes")
            {
                this.Doc.BaseClass = " : VmValidatableWithSeverityAndAttributes<" + message.Name.ToNameCs() + ", " +
                    message.Name.ToNameCs() + "Validator>";
            }
            else if (this.Doc.BaseClass == "BaseSettings")
            {
                this.Doc.BaseClass = " : BaseSettings<" + message.Name.ToNameCs() + ", " +
                    message.Name.ToNameCs() + "Validator>";
            }
            else if (this.Doc.BaseClass == "BaseSubSettings")
            {
                this.Doc.BaseClass = " : BaseSubSettings<" + message.Name.ToNameCs() + ", " +
                    message.Name.ToNameCs() + "Validator>";
            }
            else if (this.Doc.BaseClass == "ConfigObjectCommonBase")
            {
                this.Doc.BaseClass = " : ConfigObjectCommonBase<" + message.Name.ToNameCs() + ", " +
                    message.Name.ToNameCs() + "Validator>, IComparable<" + message.Name.ToNameCs() + ">, I" + root.Package.ToNameCs() + "AcceptVisitor";
            }
            else if (this.Doc.BaseClass == "ConfigObjectVmBase")
            {
                this.Doc.BaseClass = " : ConfigObjectVmBase<" + message.Name.ToNameCs() + ", " +
                    message.Name.ToNameCs() + "Validator>, IComparable<" + message.Name.ToNameCs() + ">, I" + root.Package.ToNameCs() + "AcceptVisitor";
            }
            else if (this.Doc.BaseClass == "ConfigObjectVmGenSettings")
            {
                this.Doc.BaseClass = " : ConfigObjectVmGenSettings<" + message.Name.ToNameCs() + ", " +
                    message.Name.ToNameCs() + "Validator>, IComparable<" + message.Name.ToNameCs() + ">, I" + root.Package.ToNameCs() + "AcceptVisitor";
            }
            else if (this.Doc.BaseClass == "Object") // for generating simple classes
            {
                this.Doc.BaseClass = " : Object";
            }
            else
            {

            }
        }
        private bool IsObservable(FieldDescriptor field)
        {
            if (field.IsCsSimple() || field.IsAny())
                return true;
            var doc = JsonDoc.Files[root.Name].Messages[field.MessageType.Name];
            return !doc.IsISortingValue;
        }
        private IReadOnlyList<FieldDescriptor> GetFields()
        {
            var lst = new List<FieldDescriptor>();
            foreach (var t in message.Fields.InDeclarationOrder())
            {
                //if (this.Doc.IsDefaultBase || this.Doc.IsBaseWithParent || this.Doc.IsGenSettings)
                //{
                //    //if (t.Name == "guid") continue;
                //    if (t.Name == "name") continue;
                //    if (t.Name == "name_ui") continue;
                //    if (t.Name == "sorting_value") continue;
                //}
                lst.Add(t);
            }
            return lst;
        }

    }
}
