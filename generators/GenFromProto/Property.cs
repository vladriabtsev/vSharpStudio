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
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
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
            
            #line 7 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.Comments));
            
            #line default
            #line hidden
            
            #line 7 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.Attributes));
            
            #line default
            #line hidden
            
            #line 7 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (this.IsCollection) { 
            
            #line default
            #line hidden
            this.Write("public ");
            
            #line 8 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CollectionName()));
            
            #line default
            #line hidden
            this.Write("<");
            
            #line 8 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 8 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 8 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n    get { return this._");
            
            #line 10 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("; }\r\n    set\r\n    {\r\n        if (this._");
            
            #line 13 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" != value)\r\n        {\r\n            this.On");
            
            #line 15 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changing(value);\r\n            _");
            
            #line 16 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n            this.On");
            
            #line 17 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n            this.NotifyPropertyChanged();\r\n");
            
            #line 19 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
     if (this.DocMes.IsValidatableBase) { 
            
            #line default
            #line hidden
            this.Write("            this.ValidateProperty();\r\n");
            
            #line 21 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
     } 
            
            #line default
            #line hidden
            this.Write("        }\r\n    }\r\n}\r\nprivate ");
            
            #line 25 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CollectionName()));
            
            #line default
            #line hidden
            this.Write("<");
            
            #line 25 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> _");
            
            #line 25 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(";\r\nIReadOnlyList<");
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
if (!IsSimple) {
            
            #line default
            #line hidden
            this.Write("I");
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
}
            
            #line default
            #line hidden
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldType));
            
            #line default
            #line hidden
            this.Write("> I");
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" { get { return (this as ");
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(").");
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("; } } // ");
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\npartial void On");
            
            #line 27 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changing(ObservableCollection<");
            
            #line 27 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> to); // ");
            
            #line 27 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\npartial void On");
            
            #line 28 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n");
            
            #line 29 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
   if (this.IsSelfCollection) { 
            
            #line default
            #line hidden
            this.Write("public ");
            
            #line 30 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" this[int index] { get { return (");
            
            #line 30 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(")this.");
            
            #line 30 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("[index]; } }\r\nI");
            
            #line 31 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" I");
            
            #line 31 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(".this[int index] { get { return (");
            
            #line 31 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(")this.");
            
            #line 31 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("[index]; } }\r\npublic void Add(");
            
            #line 32 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" item) // ");
            
            #line 32 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n    Debug.Assert(item != null);\r\n    this.");
            
            #line 35 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(".Add(item); \r\n    item.Parent = this;\r\n}\r\npublic void AddRange(IEnumerable<");
            
            #line 38 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("> items) \r\n{ \r\n    Debug.Assert(items != null);\r\n    this.");
            
            #line 41 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(".AddRange(items); \r\n    foreach (var t in items)\r\n        t.Parent = this;\r\n}\r\npu" +
                    "blic int Count() { return this.");
            
            #line 45 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(".Count; }\r\nint I");
            
            #line 46 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(".Count() { return this.Count(); }\r\npublic void Remove(");
            
            #line 47 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" item) \r\n{\r\n    Debug.Assert(item != null);\r\n    this.");
            
            #line 50 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(".Remove(item); \r\n    item.Parent = null;\r\n}\r\n");
            
            #line 53 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
   } 
            
            #line default
            #line hidden
            
            #line 54 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } else { //if (field.IsMessage() && !field.IsCsSimple() && !field.IsAny()) {
            
            #line default
            #line hidden
            this.Write("public ");
            
            #line 55 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 55 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 55 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{ \r\n    get { return this._");
            
            #line 57 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("; }\r\n    set\r\n    {\r\n        if (this._");
            
            #line 60 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" != value)\r\n        {\r\n            this.On");
            
            #line 62 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changing(ref value);\r\n            this._");
            
            #line 63 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n            this.On");
            
            #line 64 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n            this.NotifyPropertyChanged();\r\n");
            
            #line 66 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
   if (this.DocMes.IsValidatableBase && FieldName!="IsHasChanged") { 
            
            #line default
            #line hidden
            this.Write("            this.ValidateProperty();\r\n            this.IsChanged = true;\r\n");
            
            #line 69 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
   } 
            
            #line default
            #line hidden
            this.Write("        }\r\n    }\r\n}\r\n");
            
            #line 73 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (IsNotSkip) { 
            
            #line default
            #line hidden
            this.Write("private ");
            
            #line 74 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(" _");
            
            #line 74 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            
            #line 74 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (field.ToTypeCs() == "string" && field.FieldType == Google.Protobuf.Reflection.FieldType.String) { 
            
            #line default
            #line hidden
            this.Write(" = string.Empty");
            
            #line 74 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 75 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            
            #line 76 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (IsMessage) { 
            
            #line default
            #line hidden
            this.Write("I");
            
            #line 77 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldType));
            
            #line default
            #line hidden
            this.Write(" I");
            
            #line 77 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 77 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" { get { return (this as ");
            
            #line 77 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(").");
            
            #line 77 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("; } } // ");
            
            #line 77 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 78 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            this.Write("partial void On");
            
            #line 79 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changing(ref ");
            
            #line 79 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldType));
            
            #line default
            #line hidden
            this.Write(" to); // ");
            
            #line 79 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\npartial void On");
            
            #line 80 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("Changed();\r\n");
            
            #line 81 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (field.IsMessage()) { 
            
            #line default
            #line hidden
            
            #line 82 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 if (!this.IsNullable) { 
            
            #line default
            #line hidden
            this.Write("//I");
            
            #line 82 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            
            #line 82 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldType));
            
            #line default
            #line hidden
            this.Write(" I");
            
            #line 82 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 82 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write(" { get { return this._");
            
            #line 82 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FieldName));
            
            #line default
            #line hidden
            this.Write("; } }\r\n");
            
            #line 83 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
 } 
            
            #line default
            #line hidden
            
            #line 84 "D:\dev\vSharpStudio\generators\GenFromProto\Property.tt"
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
