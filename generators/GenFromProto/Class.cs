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
    using vSharpStudio.proto;
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
            
            #line 7 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Declaration.ToLeadingComments()));
            
            #line default
            #line hidden
            this.Write("\r\npublic partial class ");
            
            #line 8 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" : IAccept\r\n{\r\n");
            
            #line 10 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Declaration.ToTrailingComments("\t")));
            
            #line default
            #line hidden
            this.Write("\r\n\tpublic partial class ");
            
            #line 11 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator : ValidatorBase<");
            
            #line 11 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 11 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator> { }\r\n\t#region CTOR\r\n\tpublic ");
            
            #line 13 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("() : base(");
            
            #line 13 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("Validator.Validator)\r\n\t{\r\n");
            
            #line 15 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 foreach (var t in message.Fields.InDeclarationOrder())	{ 
       if (t.FieldType != Google.Protobuf.Reflection.FieldType.Message)
         continue;
       if (t.MessageType.Name.EndsWith("_nullable"))
         continue;
		if (t.IsRepeated) {
            
            #line default
            #line hidden
            this.Write("\t\tthis.");
            
            #line 21 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new SortedObservableCollection<");
            
            #line 21 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(">();\r\n\t\tthis.");
            
            #line 22 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(".CollectionChanged += ");
            
            #line 22 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("_CollectionChanged;\r\n");
            
            #line 23 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		} else if (t.FieldType == Google.Protobuf.Reflection.FieldType.Message) { 
            
            #line default
            #line hidden
            this.Write("\t\tthis.");
            
            #line 24 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 24 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("(this);\r\n");
            
            #line 25 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		} else if (t.IsMap) { 
            
            #line default
            #line hidden
            this.Write("\t\tmap???\r\n");
            
            #line 27 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
		} 
            
            #line default
            #line hidden
            
            #line 28 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\tOnInit();\r\n\t}\r\n\tpublic ");
            
            #line 31 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write("(ITreeConfigNode parent) : this()\r\n    {\r\n        this.Parent = parent;\r\n    }\r\n");
            
            #line 35 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 foreach (var t in message.Fields.InDeclarationOrder()) { if (!t.IsRepeated) continue; 
            
            #line default
            #line hidden
            this.Write("\tprivate void ");
            
            #line 36 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(@"_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
	{
        switch(e.Action)
        {
            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset: // on .Clear()
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
	    		foreach (var t in e.NewItems)
	    			(t as ");
            
            #line 48 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(").Parent = this;\r\n                break;\r\n            default:\r\n                t" +
                    "hrow new Exception();\r\n\t\t}\r\n\t}\r\n");
            
            #line 54 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\tpartial void OnInit();\r\n\t#endregion CTOR\r\n\t#region Procedures\r\n");
            
            #line 58 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
	
	this.PushIndent("\t");
	var cloner = new Clone(root, message);
	this.Write(cloner.TransformText());

	var va = new VisitorAccept(root, message);
	this.Write(va.TransformText());
	this.PopIndent(); 
            
            #line default
            #line hidden
            this.Write("\t#endregion Procedures\r\n\t#region Properties\r\n");
            
            #line 68 "C:\dev\vSharpStudio\generators\GenFromProto\Class.tt"
	this.PushIndent("\t");
	foreach (var t in message.Fields.InDeclarationOrder())
	{
        if (t.Name == "guid") continue;
        if (t.Name == "name") continue;
        if (t.Name == "sorting_value") continue;
		var p = new Property(root, message, t);
		this.Write(p.TransformText());
	}
	this.PopIndent();

            
            #line default
            #line hidden
            this.Write("\t#endregion Properties\r\n}\r\n");
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
