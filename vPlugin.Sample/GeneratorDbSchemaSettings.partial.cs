using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Google.Protobuf;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbSchemaSettings : IvPluginGeneratorSettings
    {
        [BrowsableAttribute(false)]
        public IAppProjectGenerator ParentAppProjectGenerator { get { return (IAppProjectGenerator)this.Parent; } }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbSchemaSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        public string GenerateCode(IConfig model, IAppSolution sln, IAppProject prj, IAppProjectGenerator prjGen)
        {
            string s = "";
            //if (this.IsAccessParam1)
            //{
            //    s = "kuku";
            //}
            //var visitor = new MyModelVisitor(this);
            //visitor.Run(model.Model);
            //return visitor.Result;
            return s;
        }
        public IvPluginGenerator Generator { get; set; }
        public ValidationResult ValidateSettings()
        {
            this.Validate();
            return this.ValidationResult;
        }
        public async Task<ValidationResult> ValidateSettingsAsync()
        {
            await this.ValidateAsync();
            return this.ValidationResult;
        }
    }
}
