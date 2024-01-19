using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FluentValidation.Results;
using Google.Protobuf;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbSchemaNodeSettings : IvPluginGeneratorNodeSettings, IvPluginGeneratorNodeIncludable
    {
        [BrowsableAttribute(false)]
        public string AppProjectGeneratorGuid { get; set; } = string.Empty;
        partial void OnCreated()
        {
            this.DicNodeExcludedProperties = new Dictionary<string, string?>();
            if (!(this.Parent is IConstant))
            {
                DicNodeExcludedProperties[nameof(this.IsConstantParam1)] = null; ;
            }
            if (!(this.Parent?.Parent is ICatalog) && !(this.Parent is IForm))
            {
                DicNodeExcludedProperties[nameof(this.IsCatalogFormParam1)] = null;
            }
        }
        [Browsable(false)]
        public Dictionary<string, string?>? DicNodeExcludedProperties { get; private set; }
        //[BrowsableAttribute(false)]
        //[ReadOnly(true)]
        //public string Name { get { return "Schema"; } }
        [Browsable(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbSchemaNodeSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        [Browsable(false)]
        public string SettingsAsJsonDefault
        {
            get
            {
                if (settingsAsJsonDefault == null)
                {
                    var proto = GeneratorDbSchemaNodeSettings.ConvertToProto(new GeneratorDbSchemaNodeSettings(this.Parent));
                    settingsAsJsonDefault = JsonFormatter.Default.Format(proto);
                }
                return settingsAsJsonDefault;
            }
        }
        private static string? settingsAsJsonDefault = null;
        public static string SearchPath = "";
        [Browsable(false)]
        [ReadOnly(true)]
        public string SearchPathInModel { get { return SearchPath; } }
        public string[] GetListPropertiesToHideOnNodeSettings(ITreeConfigNode modelNode)
        {
            throw new NotImplementedException();
        }
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
