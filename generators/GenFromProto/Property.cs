﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace GenFromProto
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class Property : PropertyBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\n");
            
            #line 7 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.Comments));
            
            #line default
            #line hidden
            
            #line 7 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.Attributes));
            
            #line default
            #line hidden
            
            #line 7 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (field.IsRepeated) { 
            
            #line default
            #line hidden
            
            #line 8 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (field.IsCsSimple() || field.IsAny() || (field.IsMessage() && !JsonDoc.Files[root.Name].Messages[field.MessageType.Name].IsDefaultBase)) { 
            
            #line default
            #line hidden
            this.Write("public ObservableCollection<");
            
            #line 9 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 9 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 9 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n\tset\r\n\t{\r\n\t\tif (_");
            
            #line 13 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" != value)\r\n\t\t{\r\n\t\t\tOn");
            
            #line 15 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changing();\r\n\t\t\t_");
            
            #line 16 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n\t\t\tOn");
            
            #line 17 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n\t\t\tNotifyPropertyChanged();\r\n");
            
            #line 19 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (this.DocMes.IsValidatableBase) { 
            
            #line default
            #line hidden
            this.Write("\t\t\tValidateProperty();\r\n");
            
            #line 21 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t}\r\n\t}\r\n\tget { return _");
            
            #line 24 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("; }\r\n}\r\nprivate ObservableCollection<");
            
            #line 26 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> _");
            
            #line 26 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(";\r\n[BrowsableAttribute(false)]\r\npublic IEnumerable<");
            
            #line 28 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (field.IsMessage()) { 
            
            #line default
            #line hidden
            this.Write("I");
            
            #line 28 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            
            #line 28 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 28 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("I { get { foreach (var t in _");
            
            #line 28 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(") yield return t; } }\r\n");
            
            #line 29 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } else if (field.IsMap) { 
            
            #line default
            #line hidden
            this.Write("public Dictionary<");
            
            #line 30 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 30 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 30 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n\tset\r\n\t{\r\n\t\tif (_");
            
            #line 34 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" != value)\r\n\t\t{\r\n\t\t\tOn");
            
            #line 36 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changing();\r\n\t\t\t_");
            
            #line 37 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n\t\t\tOn");
            
            #line 38 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n\t\t\tNotifyPropertyChanged();\r\n");
            
            #line 40 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (this.DocMes.IsValidatableBase) { 
            
            #line default
            #line hidden
            this.Write("\t\t\tValidateProperty();\r\n");
            
            #line 42 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t}\r\n\t}\r\n\tget { return _");
            
            #line 45 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("; }\r\n}\r\nprivate Dictionary<");
            
            #line 47 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> _");
            
            #line 47 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 48 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("public SortedObservableCollection<");
            
            #line 49 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 49 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("  // ");
            
            #line 49 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n\tset\r\n\t{\r\n\t\tif (_");
            
            #line 53 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" != value)\r\n\t\t{\r\n\t\t\tOn");
            
            #line 55 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changing();\r\n\t\t\t_");
            
            #line 56 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n\t\t\tOn");
            
            #line 57 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n\t\t\tNotifyPropertyChanged();\r\n");
            
            #line 59 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (this.DocMes.IsValidatableBase) { 
            
            #line default
            #line hidden
            this.Write("\t\t\tValidateProperty();\r\n");
            
            #line 61 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t}\r\n\t}\r\n\tget { return _");
            
            #line 64 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("; }\r\n}\r\nprivate SortedObservableCollection<");
            
            #line 66 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> _");
            
            #line 66 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(";\r\n[BrowsableAttribute(false)]\r\npublic IEnumerable<I");
            
            #line 68 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 68 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("I { get { foreach (var t in _");
            
            #line 68 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(") yield return t; } }\r\n");
            
            #line 69 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            
            #line 70 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (message.Name.EndsWith(field.Name)) { 
            
            #line default
            #line hidden
            this.Write("public ");
            
            #line 71 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" this[int index] { get { return (");
            
            #line 71 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(")this.");
            
            #line 71 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("[index]; } }\r\npublic void Add(");
            
            #line 72 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" item)  // ");
            
            #line 72 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n    this.");
            
            #line 74 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(".Add(item); \r\n    item.Parent = this;\r\n}\r\npublic void AddRange(IEnumerable<");
            
            #line 77 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("> items) \r\n{ \r\n    this.");
            
            #line 79 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(".AddRange(items); \r\n    foreach(var t in items)\r\n        t.Parent = this;\r\n}\r\npub" +
                    "lic int Count() \r\n{ \r\n    return this.");
            
            #line 85 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(".Count; \r\n}\r\npublic void Remove(");
            
            #line 87 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" item) \r\n{\r\n    this.");
            
            #line 89 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(".Remove(item); \r\n    item.Parent = null;\r\n}\r\n");
            
            #line 92 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            
            #line 93 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } else if (field.IsMessage() && !field.IsCsSimple() && !field.IsAny()) {
            
            #line default
            #line hidden
            this.Write("public ");
            
            #line 94 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 94 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 94 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n\tset\r\n\t{\r\n\t\tif (_");
            
            #line 98 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" != value)\r\n\t\t{\r\n\t\t\tOn");
            
            #line 100 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changing();\r\n            _");
            
            #line 101 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n\t\t\tOn");
            
            #line 102 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n\t\t\tNotifyPropertyChanged();\r\n");
            
            #line 104 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (this.DocMes.IsValidatableBase) { 
            
            #line default
            #line hidden
            this.Write("\t\t\tValidateProperty();\r\n");
            
            #line 106 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t}\r\n\t}\r\n\tget { return _");
            
            #line 109 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("; }\r\n}\r\nprivate ");
            
            #line 111 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(" _");
            
            #line 111 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(";\r\n[BrowsableAttribute(false)]\r\npublic I");
            
            #line 113 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 113 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("I { get { return _");
            
            #line 113 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("; }}\r\n");
            
            #line 114 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("public ");
            
            #line 115 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 115 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 115 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n\tset\r\n\t{\r\n\t\tif (_");
            
            #line 119 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" != value)\r\n\t\t{\r\n\t\t\tOn");
            
            #line 121 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changing();\r\n\t\t\t_");
            
            #line 122 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n\t\t\tOn");
            
            #line 123 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n\t\t\tNotifyPropertyChanged();\r\n");
            
            #line 125 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (this.DocMes.IsValidatableBase) { 
            
            #line default
            #line hidden
            this.Write("\t\t\tValidateProperty();\r\n");
            
            #line 127 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t}\r\n\t}\r\n\tget { return _");
            
            #line 130 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("; }\r\n}\r\nprivate ");
            
            #line 132 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(" _");
            
            #line 132 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            
            #line 132 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (field.ToTypeCs() == "string") { 
            
            #line default
            #line hidden
            this.Write(" = \"\"");
            
            #line 132 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 133 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            this.Write("partial void On");
            
            #line 134 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changing(); // ");
            
            #line 134 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\npartial void On");
            
            #line 135 "C:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Changed();");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class PropertyBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
