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
    
    #line 1 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class Class : ClassBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("public partial class ");
            
            #line 6 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator : ValidatorBase<");
            
            #line 6 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 6 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator> { } // ");
            
            #line 6 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 7 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.Comments));
            
            #line default
            #line hidden
            
            #line 7 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.Attributes));
            
            #line default
            #line hidden
            this.Write("public partial class ");
            
            #line 7 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            
            #line 7 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Doc.BaseClass));
            
            #line default
            #line hidden
            this.Write(", I");
            
            #line 7 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 7 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    #region CTOR\r\n");
            
            #line 10 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (this.IsBaseWithParent) { 
            
            #line default
            #line hidden
            this.Write("    public ");
            
            #line 11 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("() : this(default(ITreeConfigNode))\r\n    {\r\n    }\r\n    public ");
            
            #line 14 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("(ITreeConfigNode parent) \r\n        : base(parent, ");
            
            #line 15 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator.Validator) // ");
            
            #line 15 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n        this.IsNotifying = false;\r\n        this.IsValidate = false;\r\n   " +
                    "     this.OnInitBegin();\r\n");
            
            #line 20 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
    foreach (var t in message.Fields.InDeclarationOrder())	{ 
         if (t.IsCsSimple())
            continue;
         if (t.IsRepeated) {
            
            #line default
            #line hidden
            
            #line 24 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (this.IsObservable(t)) { 
            
            #line default
            #line hidden
            this.Write("        this.");
            
            #line 25 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ObservableCollection<");
            
            #line 25 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(">(); // ");
            
            #line 25 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 26 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("        this.");
            
            #line 27 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ConfigNodesCollection<");
            
            #line 27 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(">(this); // ");
            
            #line 27 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 28 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            
            #line 29 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
       } else if (t.IsMessage()) { 
            
            #line default
            #line hidden
            
            #line 30 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
          if (t.IsAny() || !JsonDoc.Files[root.Name].Messages[t.MessageType.Name].IsDefaultBase) { 
            
            #line default
            #line hidden
            this.Write("        this.");
            
            #line 31 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 31 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("(); // ");
            
            #line 31 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 32 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
          } else { 
            
            #line default
            #line hidden
            this.Write("        this.");
            
            #line 33 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 33 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("(this); // ");
            
            #line 33 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 34 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
          } 
            
            #line default
            #line hidden
            
            #line 35 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		 } else if (t.IsMap) { 
            
            #line default
            #line hidden
            this.Write("\t\tmap??? // ");
            
            #line 36 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 37 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		} 
            
            #line default
            #line hidden
            
            #line 38 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        this.OnInit();\r\n        this.IsValidate = true;\r\n        this.IsNotifying" +
                    " = true;\r\n    }\r\n");
            
            #line 43 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("    public ");
            
            #line 44 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("()");
            
            #line 44 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (this.Doc.IsValidatableBase) { 
            
            #line default
            #line hidden
            this.Write(" \r\n        : base(");
            
            #line 45 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator.Validator)");
            
            #line 45 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 45 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n        this.IsValidate = false;\r\n        this.OnInitBegin();\r\n");
            
            #line 49 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 foreach (var t in message.Fields.InDeclarationOrder())	{ 
        if (t.IsCsSimple() && !t.IsRepeated)
           continue;
		if (t.IsRepeated) {
            
            #line default
            #line hidden
            
            #line 53 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (!t.IsMessage() || t.IsAny() || !JsonDoc.Files[root.Name].Messages[t.MessageType.Name].IsDefaultBase) { 
            
            #line default
            #line hidden
            this.Write("        this.");
            
            #line 54 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ObservableCollection<");
            
            #line 54 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(">(); // ");
            
            #line 54 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 55 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("        this.");
            
            #line 56 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ConfigNodesCollection<");
            
            #line 56 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(">(this); // ");
            
            #line 56 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 57 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            
            #line 58 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		} else if (t.IsMessage()) { 
            
            #line default
            #line hidden
            
            #line 59 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (t.IsAny() || !JsonDoc.Files[root.Name].Messages[t.MessageType.Name].IsDefaultBase) { 
            
            #line default
            #line hidden
            this.Write("        this.");
            
            #line 60 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 60 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("(); // ");
            
            #line 60 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 61 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else if (t.IsNullable()) { 
            
            #line default
            #line hidden
            
            #line 62 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("        this.");
            
            #line 63 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 63 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("(this); // ");
            
            #line 63 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 64 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            
            #line 65 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		} else if (t.IsMap) { 
            
            #line default
            #line hidden
            this.Write("\t\tmap??? // ");
            
            #line 66 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 67 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		} 
            
            #line default
            #line hidden
            
            #line 68 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        this.OnInit();\r\n        this.IsValidate = true;\r\n    }\r\n");
            
            #line 72 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    partial void OnInitBegin();\r\n    partial void OnInit();\r\n    #endregion CTOR\r" +
                    "\n    #region Procedures\r\n");
            
            #line 77 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
	
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
            
            #line 94 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
	this.PushIndent("    ");
	foreach (var t in this.GetFields())
	{
		var p = new Property(root, message, t);
		this.Write(p.TransformText());
	}
	this.PopIndent(); 
            
            #line default
            #line hidden
            
            #line 101 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (this.IsBaseWithParent) { 
            
            #line default
            #line hidden
            this.Write("    [BrowsableAttribute(false)]\r\n    override public bool IsChanged // ");
            
            #line 103 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write(@"
    { 
        get { return this._IsChanged; }
        set
        {
            if (VmBindable.IsNotifyingStatic && this.IsNotifying && this._IsChanged != value)
            {
                this.OnIsChangedChanging(ref value);
                this._IsChanged = value;
                this.OnIsChangedChanged();
                this.NotifyPropertyChanged();
            }
        }
    }
    partial void OnIsChangedChanging(ref bool v); // ");
            
            #line 117 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n    protected override void OnIsChangedChanged() { OnNodeIsChangedChanged(); }\r" +
                    "\n");
            
            #line 119 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 if (!(message.Name.Contains("group") || message.Name.StartsWith("proto_config"))) { 
            
            #line default
            #line hidden
            this.Write("    partial void OnIsNewChanged() { OnNodeIsNewChanged(); }\r\n    partial void OnI" +
                    "sMarkedForDeletionChanged() { OnNodeIsMarkedForDeletionChanged(); }\r\n");
            
            #line 122 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            
            #line 123 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
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
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
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
