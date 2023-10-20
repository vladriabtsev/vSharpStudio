using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Google.Protobuf;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class DbConnectionStringSettings : IvPluginGeneratorSettings
    {
        [Browsable(false)]
        public IAppProjectGenerator ParentAppProjectGenerator
        {
            get
            {
                Debug.Assert(this.Parent != null);
                return (IAppProjectGenerator)this.Parent;
            }
        }
        public DbConnectionStringSettings(ITreeConfigNode parent, string connectionString) : this(parent)
        {
            this.StringSettings = connectionString;
        }
        [Browsable(false)]
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
        partial void OnStringSettingsChanged()
        {
            ((IAppProjectGenerator)this.Parent).NotifyConnStrChanged();
        }
        public IvPluginGenerator? Generator { get; set; }
        public ValidationResult? ValidateSettings()
        {
            this.Validate();
            return this.ValidationResult;
        }
        public async Task<ValidationResult?> ValidateSettingsAsync()
        {
            await this.ValidateAsync();
            return this.ValidationResult;
        }
    }
}
