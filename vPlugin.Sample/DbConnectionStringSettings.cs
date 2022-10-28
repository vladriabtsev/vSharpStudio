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
    public partial class DbConnectionStringSettings : IvPluginGeneratorSettings
    {
        [BrowsableAttribute(false)]
        public IAppProjectGenerator ParentAppProjectGenerator { get { return (IAppProjectGenerator)this.Parent; } }
        public DbConnectionStringSettings(string connectionString) : this()
        {
            this.StringSettings = connectionString;
        }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = DbConnectionStringSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        public string GenerateCode(IConfig cfg, IAppSolution sln, IAppProject prj, IAppProjectGenerator prjGen)
        {
            return this.StringSettings;
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
