﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
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
    using vSharpStudio.common;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class Property : PropertyBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\n");
            
            #line 8 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.Comments));
            
            #line default
            #line hidden
            
            #line 8 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.Attributes));
            
            #line default
            #line hidden
            this.Write("public ");
            
            #line 8 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PropType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 8 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 8 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n    get { return this._");
            
            #line 10 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("; }\r\n    set\r\n    {\r\n        // Use \'On");
            
            #line 13 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changing\' to change \'value\' before setting property. It is a patial method and ex" +
                    "pected will be implemented not often.\r\n");
            
            #line 14 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
   if (this.DocMes.IsBindableBase && this.isSetPropertyByRef) { 
            
            #line default
            #line hidden
            this.Write("        if (SetProperty(this._");
            
            #line 15 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(", value, (t) => { /*this.On");
            
            #line 15 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changing(");
            
            #line 15 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(refstr));
            
            #line default
            #line hidden
            this.Write("value);*/ this._");
            
            #line 15 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" = value; this.On");
            
            #line 15 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changed(); })) // ");
            
            #line 15 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n        {\r\n");
            
            #line 17 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
     if (this.DocMes.IsValidatableBase) { 
            
            #line default
            #line hidden
            this.Write("            this.ValidateProperty(); // ");
            
            #line 18 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 19 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
     } 
            
            #line default
            #line hidden
            
            #line 20 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
     if (this.DocMes.IsEditableBase && IsSimple) { 
            
            #line default
            #line hidden
            this.Write("            this.IsChanged = true; // ");
            
            #line 21 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 22 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
     } 
            
            #line default
            #line hidden
            this.Write("        }\r\n");
            
            #line 24 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
   } else { 
            
            #line default
            #line hidden
            this.Write("        if (this._");
            
            #line 25 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" != value) // ");
            
            #line 25 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n        {\r\n            //this.On");
            
            #line 27 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changing(");
            
            #line 27 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(refstr));
            
            #line default
            #line hidden
            this.Write("value);\r\n            _");
            
            #line 28 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n            this.On");
            
            #line 29 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n");
            
            #line 30 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
     if (this.DocMes.IsValidatableBase) { 
            
            #line default
            #line hidden
            this.Write("            this.ValidateProperty(); // ");
            
            #line 31 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 32 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
     } 
            
            #line default
            #line hidden
            
            #line 33 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
     if (this.DocMes.IsEditableBase && IsSimple) { 
            
            #line default
            #line hidden
            this.Write("            this.IsChanged = true; // ");
            
            #line 34 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 35 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
     } 
            
            #line default
            #line hidden
            this.Write("        }\r\n");
            
            #line 37 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
   } 
            
            #line default
            #line hidden
            this.Write("    }\r\n}\r\n");
            
            #line 40 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
 if (IsNotSkip) { 
            
            #line default
            #line hidden
            this.Write("private ");
            
            #line 41 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PropType));
            
            #line default
            #line hidden
            this.Write(" _");
            
            #line 41 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            
            #line 41 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(defstr));
            
            #line default
            #line hidden
            this.Write("; // ");
            
            #line 41 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 42 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            this.Write("//partial void On");
            
            #line 43 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changing(");
            
            #line 43 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(refstr));
            
            #line default
            #line hidden
            
            #line 43 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PropType));
            
            #line default
            #line hidden
            this.Write(" to); // ");
            
            #line 43 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\npartial void On");
            
            #line 44 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n");
            
            #line 45 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
 if (this.IsCollection /*this.IsSelfCollection*/) { 
            
            #line default
            #line hidden
            this.Write("IReadOnlyList<");
            
            #line 46 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
if (!IsSimple) {
            
            #line default
            #line hidden
            this.Write("I");
            
            #line 46 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
}
            
            #line default
            #line hidden
            
            #line 46 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldType));
            
            #line default
            #line hidden
            this.Write("> I");
            
            #line 46 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 46 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" { get { return (this as ");
            
            #line 46 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(").");
            
            #line 46 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("; } } // ");
            
            #line 46 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n");
            
            #line 48 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
   if (this.IsSelfCollection) { 
            
            #line default
            #line hidden
            this.Write("public ");
            
            #line 49 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" this[int index] { get { return (");
            
            #line 49 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(")this.");
            
            #line 49 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("[index]; } }\r\nI");
            
            #line 50 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" I");
            
            #line 50 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(".this[int index] { get { return (");
            
            #line 50 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(")this.");
            
            #line 50 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("[index]; } }\r\npublic void Add(");
            
            #line 51 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" item) // ");
            
            #line 51 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n    Debug.Assert(item != null);\r\n    this.");
            
            #line 54 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(".Add(item); \r\n    item.Parent = this;\r\n}\r\npublic void AddRange(IEnumerable<");
            
            #line 57 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("> items) \r\n{ \r\n    Debug.Assert(items != null);\r\n    this.");
            
            #line 60 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(".AddRange(items); \r\n    foreach (var t in items)\r\n        t.Parent = this;\r\n}\r\npu" +
                    "blic int Count() { return this.");
            
            #line 64 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(".Count; }\r\nint I");
            
            #line 65 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(".Count() { return this.Count(); }\r\npublic void Remove(");
            
            #line 66 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" item) \r\n{\r\n    Debug.Assert(item != null);\r\n    this.");
            
            #line 69 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(".Remove(item); \r\n    item.Parent = null;\r\n}\r\n");
            
            #line 72 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
   } // if (this.IsSelfCollection) 
            
            #line default
            #line hidden
            
            #line 73 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
 } if (!this.IsCollection) { 
            
            #line default
            #line hidden
            
            #line 74 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
   if (IsMessage) { 
            
            #line default
            #line hidden
            this.Write("I");
            
            #line 75 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldType));
            
            #line default
            #line hidden
            this.Write(" I");
            
            #line 75 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 75 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" { get { return (this as ");
            
            #line 75 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(").");
            
            #line 75 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("; } } // ");
            
            #line 75 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t4.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 76 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
   } 
            
            #line default
            #line hidden
            
            #line 77 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            
            #line 78 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
 if (FieldName == "SortingValue") {
            
            #line default
            #line hidden
            this.Write("public void SetSortingValueField(ulong sortValue)\r\n{\r\n    this._SortingValue = so" +
                    "rtValue;\r\n}\r\n");
            
            #line 83 "D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
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
        public System.Text.StringBuilder GenerationEnvironment
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
