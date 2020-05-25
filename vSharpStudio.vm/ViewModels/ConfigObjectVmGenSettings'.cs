using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public class ConfigObjectVmGenSettings<T, TValidator> : ConfigObjectVmBase<T, TValidator>
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectVmGenSettings<T, TValidator>//, IComparable<T>, ISortingValue 
    {
        public ConfigObjectVmGenSettings(ITreeConfigNode parent, TValidator validator)
            : base(parent, validator)
        {
        }
        //[BrowsableAttribute(false)]
        //public ConfigNodesCollection<PluginGeneratorMainSettings> ListGeneratorsSettings
        //{
        //    get
        //    {
        //        if (this._ListGeneratorsSettings == null)
        //            this._ListGeneratorsSettings = new ConfigNodesCollection<PluginGeneratorMainSettings>(this);
        //        return this._ListGeneratorsSettings;
        //    }
        //    set
        //    {
        //        if (this._ListGeneratorsSettings != value)
        //        {
        //            this._ListGeneratorsSettings = value;
        //            this.NotifyPropertyChanged();
        //            //this.ValidateProperty();
        //        }
        //    }
        //}
        //private ConfigNodesCollection<PluginGeneratorMainSettings> _ListGeneratorsSettings;

        // Settings workflow:
        // 1. When Config is loaded: init all generators settings VMs on all model nodes
        // 2. When model node is added: init all generators settings VMs on this node
        // 3. When new generator is selected: old generator has to be removed from all model nodes, 
        //     and new generator settings has to be added for all model nodes
        // 4. When saving Config: convert all model nodes generators settings to string representations

        // 2. When model node is added: init all generators settings VMs on this node
        //protected override void OnNodeAdded(ITreeConfigNode node)
        //{
        //    base.OnNodeAdded(node);
        //    AddAllAppGenSettingsVmsToNode();
        //}
        public void AddAllAppGenSettingsVmsToNode()
        {
            var ngs = this as INodeGenSettings;
            if (ngs == null)
                return;
            var cfg = (Config)this.GetConfig();
            if (cfg == null || cfg.GroupAppSolutions == null)
                return;
            _logger.LogTrace("Try Add Node Settings. {Count}".CallerInfo(), cfg.DicActiveAppProjectGenerators.Count);
            foreach (var t in cfg.GroupAppSolutions.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        if (cfg.DicActiveAppProjectGenerators.ContainsKey(ttt.Guid))
                        {
                            ngs.AddNodeAppGenSettings(ttt.Guid);
                        }
                    }
                }
            }
        }

        #region Node App Generator Settings

        //App project generator Guid,
        private void SearchPathAndAdd(AppProjectGenerator appgen, INodeGenSettings ngs, IvPluginGenerator gen)
        {
            var lst = gen.GetListNodeGenerationSettings();
            foreach (var t in lst)
            {
                string modelPath = this.ModelPath;
                var searchPattern = t.SearchPathInModel;
                var is_found = SearchInModelPathByPattern(modelPath, searchPattern);

                if (is_found)
                {
                    PluginGeneratorNodeSettings gs = new PluginGeneratorNodeSettings(this);
                    gs.Name = appgen.Name;
                    gs.NodeSettingsVmGuid = t.Guid;
                    gs.AppProjectGeneratorGuid = appgen.Guid;
                    _logger.LogTrace("Adding Node Settings. {Path} NodeSettingsVmGuid={NodeSettingsVmGuid} Name={Name}".CallerInfo(), t.SearchPathInModel, gs.NodeSettingsVmGuid, appgen.Name);
#if DEBUG
                    foreach (var ttt in ngs.ListNodeGeneratorsSettings)
                    {
                        if (ttt.NodeSettingsVmGuid == gs.NodeSettingsVmGuid)
                            throw new Exception();
                    }
#endif
                    ngs.ListNodeGeneratorsSettings.Add(gs);
                    gs.SettingsVm = t.GetAppGenerationNodeSettingsVm(gs.Settings);
                }
            }
        }
#if NET48
                public static string[] Split(string text, string separator)
                {
                    List<string> lst = new List<string>();
                    int ibeg = 0;
                    while(true)
                    {
                        int i = text.IndexOf(separator, ibeg);
                        if (i > 0)
                        {
                            lst.Add(text.Substring(ibeg, i - ibeg));
                            ibeg = i + separator.Length;
                            continue;
                        }
                        lst.Add(text.Substring(ibeg));
                        break;
                    }
                    string[] res = new string[lst.Count];
                    int ii = 0;
                    foreach(var t in lst)
                        res[ii++] = t;
                    return res;
                }
#endif
        public static bool SearchInModelPathByPattern(string modelPath, string searchPattern)
        {
            var subPatterns = searchPattern.Split(';');
            if (subPatterns.Count()==1)
            {
                if (string.IsNullOrWhiteSpace(subPatterns[0]) || subPatterns[0] == "*")
                    return true;
            }
            bool is_found = true;
            foreach (var t in subPatterns)
            {
#if NET48
                        var sp = Split(t, ".*.");
#else
                var sp = t.Split(".*.");
#endif
                is_found = true;
                int indx = 0;
                foreach (var s in sp)
                {
                    var sd = "." + s;
                    if (modelPath.Contains(s))
                    {
                        indx = modelPath.IndexOf(sd) + sd.Length;
                        modelPath = modelPath.Substring(indx);
                    }
                    else
                    {
                        is_found = false;
                        break;
                    }
                }
                if (modelPath.Length > 0)
                    is_found = false;
                if (is_found)
                    break;
            }
            return is_found;
        }
        public void RestoreNodeAppGenSettingsVm()
        {
            _logger.Trace();
            var ngs = (INodeGenSettings)this;
            var cfg = (Config)this.GetConfig();
            foreach (var t in ngs.ListNodeGeneratorsSettings)
            {
                if (cfg.DicActiveAppProjectGenerators.ContainsKey(t.AppProjectGeneratorGuid))
                {
                    var gen = cfg.DicActiveAppProjectGenerators[t.AppProjectGeneratorGuid];
                    var appgen = (AppProjectGenerator)(cfg.DicNodes[t.AppProjectGeneratorGuid]);
                    bool is_found = false;
                    foreach (var tt in gen.GetListNodeGenerationSettings())
                    {
                        if (tt.Guid == t.NodeSettingsVmGuid)
                        {
                            t.SettingsVm = tt.GetAppGenerationNodeSettingsVm(t.Settings);
                            is_found = true;
                            break;
                        }
                    }
                    if (!is_found)
                        throw new Exception();
                }
                else
                    throw new Exception();
            }
        }
        public void SaveNodeAppGenSettings()
        {
            _logger.Trace();
            var ngs = (INodeGenSettings)this;
            foreach (var t in ngs.ListNodeGeneratorsSettings)
            {
                t.Settings = t.SettingsVm.SettingsAsJson;
            }
        }
        public void RemoveNodeAppGenSettings(string appGenGuid)
        {
            _logger.Trace();
            var ngs = (INodeGenSettings)this;
            for (int i = ngs.ListNodeGeneratorsSettings.Count - 1; i > -1; i--)
            {
                var t = ngs.ListNodeGeneratorsSettings[i];
                if (t.AppProjectGeneratorGuid == appGenGuid)
                {
                    ngs.ListNodeGeneratorsSettings.RemoveAt(i);
                    break;
                }
            }
        }
        public void AddNodeAppGenSettings(string appGenGuid)
        {
            _logger.Trace();
            //var cnfg = this.GetConfig();
            var ngs = (INodeGenSettings)this;
#if DEBUG
            if (ngs == null)
                throw new Exception();
            foreach (var t in ngs.ListNodeGeneratorsSettings)
            {
                if (t.AppProjectGeneratorGuid == appGenGuid)
                    throw new Exception();
            }
#endif
            var cfg = (Config)this.GetConfig();
            //var gen = cfg.DicActiveAppProjectGenerators[appGenGuid];
            //var set = new PluginGeneratorMainSettings(this);
            //set.AppGeneratorGuid = appGenGuid;
            //set.SettingsVm = gen.GetAppGenerationSettingsVmFromJson("");
            //ngs.ListNodeGeneratorsSettings.Add(set);
            var appgen = (AppProjectGenerator)cfg.DicNodes[appGenGuid];
            var gen = cfg.DicActiveAppProjectGenerators[appGenGuid];
            SearchPathAndAdd(appgen, ngs, gen);
        }
        //public void CreatePropertyGridNodeGenSettings(INodeGenSettings p)
        //{
        //    // create a dynamic assembly and module 
        //    AssemblyBuilder assemblyBldr = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("tmpAssembly"), AssemblyBuilderAccess.Run);
        //    //AssemblyBuilder assemblyBldr = Thread.GetDomain().DefineDynamicAssembly(new AssemblyName("tmpAssembly"), AssemblyBuilderAccess.Run);
        //    ModuleBuilder moduleBldr = assemblyBldr.DefineDynamicModule("tmpModule");
        //    // create a new type builder
        //    TypeBuilder typeBldr = moduleBldr.DefineType("GenSettings", TypeAttributes.Public | TypeAttributes.Class, null);

        //    var cfg = (Config)this.GetConfig();
        //    foreach (var t in p.ListGeneratorsSettings)
        //    {
        //        var propType = typeof(object);
        //        var appGen = cfg.DicNodes[t.AppGeneratorGuid];
        //        // Generate a private field for the property
        //        FieldBuilder fldBldr = typeBldr.DefineField("_" + t.Name, propType, FieldAttributes.Private);
        //        // Generate a public property
        //        PropertyBuilder prptyBldr = typeBldr.DefineProperty(t.Name, PropertyAttributes.None, propType, new Type[] { propType });

        //        // The property set and property get methods need the following attributes:
        //        MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.HideBySig;
        //        // Define the "get" accessor method for newly created private field.
        //        MethodBuilder currGetPropMthdBldr = typeBldr.DefineMethod("get_value", GetSetAttr, propType, null);

        //        // Intermediate Language stuff... as per MS
        //        ILGenerator currGetIL = currGetPropMthdBldr.GetILGenerator();
        //        currGetIL.Emit(OpCodes.Ldarg_0);
        //        currGetIL.Emit(OpCodes.Ldfld, fldBldr);
        //        currGetIL.Emit(OpCodes.Ret);

        //        // Define the "set" accessor method for the newly created private field.
        //        MethodBuilder currSetPropMthdBldr = typeBldr.DefineMethod("set_value", GetSetAttr, null, new Type[] { propType });

        //        // More Intermediate Language stuff...
        //        ILGenerator currSetIL = currSetPropMthdBldr.GetILGenerator();
        //        currSetIL.Emit(OpCodes.Ldarg_0);
        //        currSetIL.Emit(OpCodes.Ldarg_1);
        //        currSetIL.Emit(OpCodes.Stfld, fldBldr);
        //        currSetIL.Emit(OpCodes.Ret);

        //        // Assign the two methods created above to the PropertyBuilder's Set and Get
        //        prptyBldr.SetGetMethod(currGetPropMthdBldr);
        //        prptyBldr.SetSetMethod(currSetPropMthdBldr);

        //        ConstructorInfo cons = typeof(ExpandableObjectAttribute).GetConstructor(new Type[] { });
        //        CustomAttributeBuilder attribute = new CustomAttributeBuilder(cons, new object[] { }, new FieldInfo[] { }, new object[] { });
        //        prptyBldr.SetCustomAttribute(attribute);

        //        cons = typeof(DescriptionAttribute).GetConstructor(new Type[] { typeof(string) });
        //        attribute = new CustomAttributeBuilder(cons, new object[] { t.Description }, new FieldInfo[] { }, new object[] { });
        //        prptyBldr.SetCustomAttribute(attribute);
        //    }
        //    var dynamicType = typeBldr.CreateType();
        //    var instance = Activator.CreateInstance(dynamicType);
        //    foreach (var t in p.ListGeneratorsSettings)
        //    {
        //        PropertyInfo prop = dynamicType.GetProperty(t.Name);
        //        prop.SetValue(instance, t.SettingsVm, null);
        //    }
        //    p.GenSettings = instance;
        //}

        #endregion Node App Generator Settings
    }
}
