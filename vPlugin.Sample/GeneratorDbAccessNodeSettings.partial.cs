using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FluentValidation.Results;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessNodeSettings : IvPluginGeneratorNodeSettings, IvPluginGeneratorNodeIncludable
    {
        partial void OnCreated()
        {
            this.DicNodeExcludedProperties = new Dictionary<string, string?>();
            if (!(this.Parent is IProperty))
            {
                DicNodeExcludedProperties[this.GetPropertyName(() => this.IsPropertyParam1)] = null; ;
            }
            if (!(this.Parent != null && this.Parent.Parent is ICatalog) && !(this.Parent is IForm))
            {
                DicNodeExcludedProperties[this.GetPropertyName(() => this.IsCatalogFormParam1)] = null;
            }
        }
        [Browsable(false)]
        public Dictionary<string, string?>? DicNodeExcludedProperties { get; private set; }
        //[BrowsableAttribute(false)]
        //[ReadOnly(true)]
        //public string Name { get { return "Access"; } }
        [Browsable(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbAccessNodeSettings.ConvertToProto(this);
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
                    var proto = GeneratorDbAccessNodeSettings.ConvertToProto(new GeneratorDbAccessNodeSettings(this.Parent));
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
