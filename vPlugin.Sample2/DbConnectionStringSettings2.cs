using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Google.Protobuf;
using vSharpStudio.common;

namespace vPlugin.Sample2
{
    public partial class DbConnectionStringSettings2 : IvPluginGeneratorSettings
    {
        [BrowsableAttribute(false)]
        public IAppProjectGenerator ParentAppProjectGenerator { get { Debug.Assert(this.Parent != null); return (IAppProjectGenerator)this.Parent; } }
        public DbConnectionStringSettings2(ITreeConfigNode parent, string connectionString) : this(parent)
        {
            this.StringSettings = connectionString;
        }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = DbConnectionStringSettings2.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        public string GenerateCode(IConfig cfg, IAppSolution sln, IAppProject prj, IAppProjectGenerator prjGen)
        {
            return this.StringSettings;
        }
        public IvPluginGenerator? Generator { get; set; }
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
