using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessNodeSettings : IvPluginGeneratorNodeSettings, IvPluginGeneratorNodeIncludable
    {
        public GeneratorDbAccessNodeSettings(ITreeConfigNode node) : base(GeneratorDbAccessNodeSettingsValidator.Validator)
        {
            //var lst = new List<string>();
            //if (!(node is IProperty))
            //{
            //    lst.Add(this.GetPropertyName(() => this.IsPropertyParam1));
            //}
            //if (!(node.Parent is ICatalog) && !(node is IForm))
            //{
            //    lst.Add(this.GetPropertyName(() => this.IsCatalogFormParam1));
            //}
            //this.HidePropertiesForXceedPropertyGrid(lst.ToArray());
            this.DicNodeExcludedProperties = new Dictionary<string, string>();
            if (!(node is IProperty))
            {
                DicNodeExcludedProperties[this.GetPropertyName(() => this.IsPropertyParam1)] = null; ;
            }
            if (!(node.Parent is ICatalog) && !(node is IForm))
            {
                DicNodeExcludedProperties[this.GetPropertyName(() => this.IsCatalogFormParam1)] = null;
            }
        }
        [BrowsableAttribute(false)]
        public Dictionary<string, string> DicNodeExcludedProperties { get; private set; }
        //[BrowsableAttribute(false)]
        //[ReadOnly(true)]
        //public string Name { get { return "Access"; } }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbAccessNodeSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }
        [BrowsableAttribute(false)]
        public string SettingsAsJsonDefault
        {
            get
            {
                if (settingsAsJsonDefault == null)
                {
                    var proto = GeneratorDbAccessNodeSettings.ConvertToProto(new GeneratorDbAccessNodeSettings());
                    settingsAsJsonDefault = JsonFormatter.Default.Format(proto);
                }
                return settingsAsJsonDefault;
            }
        }
        private static string settingsAsJsonDefault = null;
        public static string SearchPath = "";
        [BrowsableAttribute(false)]
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
        public Task<ValidationResult> ValidateSettingsAsync()
        {
            return Task.FromResult(this.ValidationResult);
        }
    }
}
