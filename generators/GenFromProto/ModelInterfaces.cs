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
    
    #line 1 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class ModelInterfaces : ModelInterfacesBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing Google.Protobuf.WellKnown" +
                    "Types;\r\nusing ViewModelBase;\r\n\r\nnamespace vSharpStudio.common // ");
            
            #line 11 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n");
            
            #line 13 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 foreach (var t in root.EnumTypes) { 
     this.PushIndent("    "); 
            
            #line default
            #line hidden
            this.Write("public enum ");
            
            #line 15 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(t.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 15 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n");
            
            #line 17 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
   foreach (var tt in t.Values) { 
            
            #line default
            #line hidden
            this.Write("    ");
            
            #line 18 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(tt.Name));
            
            #line default
            #line hidden
            this.Write(" = ");
            
            #line 18 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(tt.Number));
            
            #line default
            #line hidden
            this.Write(",\r\n");
            
            #line 19 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
   } 
            
            #line default
            #line hidden
            this.Write("}\r\n");
            
            #line 21 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
   this.PopIndent(); 
            
            #line default
            #line hidden
            
            #line 22 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 }
   foreach (var message in messages) { 
     if (message.Name.EndsWith("_nullable"))
       continue;
     this.MessageDoc = JsonDoc.Files[root.Name].Messages[message.Name]; 
     this.PushIndent("    "); 
            
            #line default
            #line hidden
            
            #line 28 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.MessageDoc.Comments));
            
            #line default
            #line hidden
            this.Write("\r\npublic partial interface I");
            
            #line 29 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(message.Name.ToNameCs()));
            
            #line default
            #line hidden
            
            #line 29 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 if (MessageDoc.IsDefaultBase) { 
            
            #line default
            #line hidden
            this.Write(" : IGuid, IName");
            
            #line 29 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 } 
            
            #line default
            #line hidden
            this.Write(" // ");
            
            #line 29 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n");
            
            #line 31 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 if (MessageDoc.IsDefaultBase) { 
            
            #line default
            #line hidden
            this.Write("    //IvPluginGeneratorNodeSettings GetSettings(string guidAppPrjGen, string guid" +
                    "Settings); // ");
            
            #line 32 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 33 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 } 
            
            #line default
            #line hidden
            
            #line 34 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
    foreach (var field in message.Fields.InDeclarationOrder()) {
        this.FieldDoc = JsonDoc.Files[root.Name].Messages[message.Name].Fields[field.Name];  
         this.PushIndent("\t"); 
            
            #line default
            #line hidden
            
            #line 37 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.FieldDoc.Comments));
            
            #line default
            #line hidden
            
            #line 37 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 if (field.IsRepeated) { 
            
            #line default
            #line hidden
            
            #line 38 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 if (field.IsMap()) { 
            
            #line default
            #line hidden
            this.Write("IReadOnlyList<");
            
            #line 39 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 39 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" { get; } // ");
            
            #line 39 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 40 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 } else if (field.IsCsSimple()) { 
            
            #line default
            #line hidden
            this.Write("IReadOnlyList<");
            
            #line 41 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 41 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" { get; } // ");
            
            #line 41 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 42 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("IReadOnlyList<I");
            
            #line 43 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 43 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" { get; } // ");
            
            #line 43 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 44 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
   if (message.Name.EndsWith(field.Name)) { 
            
            #line default
            #line hidden
            this.Write("I");
            
            #line 45 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.MessageType.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" this[int index] { get; }\r\nint Count();\r\n");
            
            #line 47 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
   } 
            
            #line default
            #line hidden
            
            #line 48 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
 } 
            
            #line default
            #line hidden
            
            #line 49 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
       } else if (field.IsCsSimple()) { 
            
            #line default
            #line hidden
            
            #line 50 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 50 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" { get; ");
            
            #line 50 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Setter));
            
            #line default
            #line hidden
            this.Write("} // ");
            
            #line 50 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 51 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
       } else if (field.IsAny() || field.IsNullable()) { 
            
            #line default
            #line hidden
            
            #line 52 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 52 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" { get; } // ");
            
            #line 52 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 53 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
       } else { 
            
            #line default
            #line hidden
            this.Write("I");
            
            #line 54 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ToTypeCs()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 54 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name.ToNameCs()));
            
            #line default
            #line hidden
            this.Write(" { get; } // ");
            
            #line 54 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Helper.FilePos()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 55 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
       } 
            
            #line default
            #line hidden
            
            #line 56 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
       this.PopIndent(); 
            
            #line default
            #line hidden
            
            #line 57 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
   } 
            
            #line default
            #line hidden
            this.Write("}\r\n");
            
            #line 59 "C:\dev\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt"
    
      this.PopIndent();
   } 

            
            #line default
            #line hidden
            this.Write("}");
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
    public class ModelInterfacesBase
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
