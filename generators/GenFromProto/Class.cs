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
    
    #line 1 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class Class : ClassBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("// ");
            
            #line 6 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n//       IsWithParent: ");
            
            #line 7 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.IsWithParent));
            
            #line default
            #line hidden
            this.Write(" \r\n//      IsDefaultBase: ");
            
            #line 8 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.IsDefaultBase));
            
            #line default
            #line hidden
            this.Write(" \r\n// IsConfigObjectBase: ");
            
            #line 9 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.IsConfigObjectBase));
            
            #line default
            #line hidden
            this.Write(" \r\n//      IsGenSettings: ");
            
            #line 10 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.IsGenSettings));
            
            #line default
            #line hidden
            this.Write(" \r\n//     IsBindableBase: ");
            
            #line 11 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.IsBindableBase));
            
            #line default
            #line hidden
            this.Write(" \r\n//     IsEditableBase: ");
            
            #line 12 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.IsEditableBase));
            
            #line default
            #line hidden
            this.Write(" \r\n//  IsValidatableBase: ");
            
            #line 13 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.IsValidatableBase));
            
            #line default
            #line hidden
            this.Write(" \r\n//    IsISortingValue: ");
            
            #line 14 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.IsISortingValue));
            
            #line default
            #line hidden
            this.Write(" \r\npublic partial class ");
            
            #line 15 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator : ValidatorBase<");
            
            #line 15 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 15 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator> { } // ");
            
            #line 15 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 16 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.Comments));
            
            #line default
            #line hidden
            
            #line 16 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.Attributes));
            
            #line default
            #line hidden
            this.Write("public partial class ");
            
            #line 16 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            
            #line 16 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.BaseClass));
            
            #line default
            #line hidden
            this.Write(", I");
            
            #line 16 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 16 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    #region CTOR\r\n");
            
            #line 19 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (!this.Doc.IsWithParent) { 
            
            #line default
            #line hidden
            this.Write("    /*public ");
            
            #line 20 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("() // ");
            
            #line 20 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n        this.OnCreating();\r\n    }*/\r\n");
            
            #line 24 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            
            #line 25 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (this.Doc.IsWithParent) { 
            
            #line default
            #line hidden
            this.Write("    public ");
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.ParentTypeName));
            
            #line default
            #line hidden
            this.Write("? parent) // ");
            
            #line 26 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n        : base(parent, ");
            
            #line 27 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator.Validator)\r\n    {\r\n        this.IsNotifying = false;\r\n        this.IsVa" +
                    "lidate = false;\r\n        this.OnCreating();\r\n");
            
            #line 32 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
    foreach (var t in message.Fields.InDeclarationOrder()) { 
         if (t.IsRepeated) {
            
            #line default
            #line hidden
            
            #line 34 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (this.IsObservable(t)) { 
            
            #line default
            #line hidden
            this.Write("        this._");
            
            #line 35 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ObservableCollectionWithActions<");
            
            #line 35 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(">(); // ");
            
            #line 35 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 36 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("        this._");
            
            #line 37 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ConfigNodesCollection<");
            
            #line 37 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(">(this); // ");
            
            #line 37 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 38 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            
            #line 39 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
       } else if (t.IsMessage()) { if (t.IsCsSimple()) continue; 
            
            #line default
            #line hidden
            this.Write("        this._");
            
            #line 40 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 40 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("(this); // ");
            
            #line 40 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 41 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		 } else if (t.IsMap) { 
            
            #line default
            #line hidden
            this.Write("\t\tmap??? // ");
            
            #line 42 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 43 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		 } 
            
            #line default
            #line hidden
            
            #line 44 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        this.OnCreated();\r\n        this.IsValidate = true;\r\n        this.IsNotify" +
                    "ing = true;\r\n    }\r\n");
            
            #line 49 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("    public ");
            
            #line 50 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("()");
            
            #line 50 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (this.Doc.IsValidatableBase) { 
            
            #line default
            #line hidden
            this.Write(" \r\n        : base(");
            
            #line 51 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator.Validator)");
            
            #line 51 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 51 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n        this.IsValidate = false;\r\n        this.OnCreating();\r\n");
            
            #line 55 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 foreach (var t in message.Fields.InDeclarationOrder())	{ 
        if (t.IsCsSimple() && !t.IsRepeated)
           continue;
		if (t.IsRepeated) {
            
            #line default
            #line hidden
            
            #line 59 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (!t.IsMessage() || t.IsAny() || !JsonDoc.Files[root.Name].Messages[t.MessageType.Name].IsDefaultBase) { 
            
            #line default
            #line hidden
            this.Write("        this._");
            
            #line 60 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ObservableCollectionWithActions<");
            
            #line 60 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(">(); // ");
            
            #line 60 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 61 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("        this._");
            
            #line 62 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ConfigNodesCollection<");
            
            #line 62 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(">(this); // ");
            
            #line 62 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 63 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            
            #line 64 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		} else if (t.IsMessage()) { 
            
            #line default
            #line hidden
            
            #line 65 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (t.IsAny() || !JsonDoc.Files[root.Name].Messages[t.MessageType.Name].IsDefaultBase) { 
            
            #line default
            #line hidden
            this.Write("        this._");
            
            #line 66 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 66 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("(); // ");
            
            #line 66 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 67 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else if (t.IsNullable()) { 
            
            #line default
            #line hidden
            
            #line 68 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("        this._");
            
            #line 69 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 69 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("(this); // ");
            
            #line 69 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 70 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            
            #line 71 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		} else if (t.IsMap) { 
            
            #line default
            #line hidden
            this.Write("\t\tmap??? // ");
            
            #line 72 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 73 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		} 
            
            #line default
            #line hidden
            
            #line 74 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        this.OnCreated();\r\n        this.IsValidate = true;\r\n    }\r\n");
            
            #line 78 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    partial void OnCreating();\r\n    partial void OnCreated();\r\n    #endregion CTO" +
                    "R\r\n    #region Procedures\r\n");
            
            #line 83 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
	
	this.PushIndent("    ");
	
    var cloner = new Clone(root, message, nameSpace, protoNameSpace);
	this.Write(cloner.TransformText());

	var va = new AcceptNodeVisitor(root, message);
	this.Write(va.TransformText());

    /*if (is_config_base) {
	    var av = new AcceptValidator(root, message);
	    this.Write(av.TransformText());
    }*/

	this.PopIndent(); 
            
            #line default
            #line hidden
            this.Write("    #endregion Procedures\r\n    #region Properties\r\n");
            
            #line 100 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
	this.PushIndent("    ");
	foreach (var t in this.GetFields())
	{
		var p = new Property(root, message, t);
		this.Write(p.TransformText());
	}
	this.PopIndent(); 
            
            #line default
            #line hidden
            
            #line 107 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (this.Doc.IsWithParent) { 
            
            #line default
            #line hidden
            this.Write("    [BrowsableAttribute(false)]\r\n    public override bool IsChanged // ");
            
            #line 109 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write(@"
    { 
        get { return this._IsChanged; }
        set
        {
            if (VmBindable.IsNotifyingStatic && this.IsNotifying)
            {
                if (this._IsChanged != value)
                {
                    this.OnIsChangedChanging(ref value);
                    this._IsChanged = value;
                    this.OnIsChangedChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
    }
    partial void OnIsChangedChanging(ref bool v); // ");
            
            #line 126 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 127 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (this.Doc.IsConfigObjectBase) { 
            
            #line default
            #line hidden
            this.Write("    protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); } //" +
                    " ");
            
            #line 128 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 129 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("    //partial void OnIsChangedChanged(); // ");
            
            #line 130 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 131 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            
            #line 132 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (this.Doc.IsICanAddNode) { 
            
            #line default
            #line hidden
            this.Write("    partial void OnIsNewChanged() { OnNodeIsNewChanged(); } // ");
            
            #line 133 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 133 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name));
            
            #line default
            #line hidden
            this.Write("\r\n    partial void OnIsMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChan" +
                    "ged(); }\r\n");
            
            #line 135 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            
            #line 136 "D:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    #endregion Properties\r\n}\r\n");
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
    public class ClassBase
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
