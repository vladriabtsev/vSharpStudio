using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Google.Protobuf;
using Proto.Plugin;
using vSharpStudio.common;

namespace vPlugin.Sample
{
    public partial class GeneratorDbSchemaNodeSettings : IvPluginGeneratorNodeSettings, IvPluginGeneratorNodeIncludable
    {
        public GeneratorDbSchemaNodeSettings(ITreeConfigNode node) : base(GeneratorDbSchemaNodeSettingsValidator.Validator)
        {
            //var lst = new List<string>();
            //if (!(node is IConstant))
            //{
            //    lst.Add(this.GetPropertyName(() => this.IsConstantParam1));
            //}
            //if (!(node.Parent is ICatalog) && !(node is IForm))
            //{
            //    lst.Add(this.GetPropertyName(() => this.IsCatalogFormParam1));
            //}
            //this.HidePropertiesForXceedPropertyGrid(lst.ToArray());
            this.DicNodeExcludedProperties = new Dictionary<string, string>();
            if (!(node is IConstant))
            {
                DicNodeExcludedProperties[this.GetPropertyName(() => this.IsConstantParam1)] = null; ;
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
        //public string Name { get { return "Schema"; } }
        [BrowsableAttribute(false)]
        public string SettingsAsJson
        {
            get
            {
                var proto = GeneratorDbSchemaNodeSettings.ConvertToProto(this);
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
                    var proto = GeneratorDbSchemaNodeSettings.ConvertToProto(new GeneratorDbSchemaNodeSettings());
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
    }
}
