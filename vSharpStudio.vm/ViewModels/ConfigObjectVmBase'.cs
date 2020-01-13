using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class ConfigObjectVmBase<T, TValidator> : ConfigObjectCommonBase<T, TValidator>
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectVmBase<T, TValidator>//, IComparable<T>, ISortingValue 
    {
        public ConfigObjectVmBase(ITreeConfigNode parent, TValidator validator)
            : base(parent, validator)
        {
        }
        protected void OnAddRemoveNode(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var cfg = this.GetConfig();
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    cfg.DicNodes[this.Guid] = this;
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    cfg.DicNodes.Remove(this.Guid);
                    break;
            }
        }

        #region Node App Generator Settings

        // App project generator Guid, 
        public void AddAllAppGenSettingsVmsToNewNode()
        {
            var ngs = (INodeGenSettings)this;
#if DEBUG
            if (ngs.ListGeneratorsSettings.Count > 0)
                throw new Exception();
#endif
            var cfg = (Config)this.GetConfig();
            if (cfg == null || cfg.GroupAppSolutions == null)
                return;
            _logger.LogTrace("Try Add Node Settings. {Count}".CallerInfo(), cfg.DicAppGenerators.Count);
            foreach (var t in cfg.GroupAppSolutions.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        if (cfg.DicAppGenerators.ContainsKey(ttt.Guid))
                        {
                            var gen = (IvPluginGenerator)cfg.DicAppGenerators[ttt.Guid];
                            SearchPathAndAdd(ttt, ngs, gen);
                        }
                    }
                }
            }
        }
        private void SearchPathAndAdd(AppProjectGenerator appgen, INodeGenSettings ngs, IvPluginGenerator gen)
        {
            foreach (var t in gen.GetListNodeGenerationSettings())
            {
                string modelPath = this.ModelPath;
                var searchPattern = t.SearchPathInModel;
                var is_found = SearchInModelPathByPattern(modelPath, searchPattern);

                if (is_found)
                {
                    GeneratorSettings gs = new GeneratorSettings(this);
                    gs.NodeSettingsVmGuid = t.Guid;
                    gs.AppGeneratorGuid = appgen.Guid;
                    _logger.LogTrace("Adding Node Settings. {Path} NodeSettingsVmGuid={NodeSettingsVmGuid} Name={Name}".CallerInfo(), t.SearchPathInModel, gs.NodeSettingsVmGuid, appgen.Name);
#if DEBUG
                    foreach (var ttt in ngs.ListGeneratorsSettings)
                    {
                        if (ttt.AppGeneratorGuid == appgen.Guid)
                            throw new Exception();
                    }
#endif
                    ngs.ListGeneratorsSettings.Add(gs);
                    gs.SettingsVm = t.GetAppGenerationNodeSettingsVm(gs.Settings);
                    break;
                }
            }
        }
        public static bool SearchInModelPathByPattern(string modelPath, string searchPattern)
        {
            var subPatterns = searchPattern.Split(";");
            bool is_found = true;
            foreach (var t in subPatterns)
            {
                var sp = t.Split(".*.");
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
            foreach (var t in ngs.ListGeneratorsSettings)
            {
                if (cfg.DicAppGenerators.ContainsKey(t.AppGeneratorGuid))
                {
                    var gen = cfg.DicAppGenerators[t.AppGeneratorGuid];
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
            foreach (var t in ngs.ListGeneratorsSettings)
            {
                t.Settings = t.SettingsVm.SettingsAsJson;
            }
        }
        public void RemoveNodeAppGenSettings(string appGenGuid)
        {
            _logger.Trace();
            var ngs = (INodeGenSettings)this;
            for (int i = ngs.ListGeneratorsSettings.Count - 1; i > 0; i--)
            {
                var t = ngs.ListGeneratorsSettings[i];
                if (t.AppGeneratorGuid == appGenGuid)
                {
                    ngs.ListGeneratorsSettings.RemoveAt(i);
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
            foreach (var t in ngs.ListGeneratorsSettings)
            {
                if (t.AppGeneratorGuid == appGenGuid)
                    throw new Exception();
            }
#endif
            var cfg = (Config)this.GetConfig();
            var appgen = (AppProjectGenerator)cfg.DicNodes[appGenGuid];
            var gen = cfg.DicAppGenerators[appGenGuid];
            SearchPathAndAdd(appgen, ngs, gen);
        }
        public void CreatePropertyGridNodeGenSettings(INodeGenSettings p)
        {
            // create a dynamic assembly and module 
            AssemblyBuilder assemblyBldr = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("tmpAssembly"), AssemblyBuilderAccess.Run);
            //AssemblyBuilder assemblyBldr = Thread.GetDomain().DefineDynamicAssembly(new AssemblyName("tmpAssembly"), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBldr = assemblyBldr.DefineDynamicModule("tmpModule");
            // create a new type builder
            TypeBuilder typeBldr = moduleBldr.DefineType("GenSettings", TypeAttributes.Public | TypeAttributes.Class, null);

            var cfg = (Config)this.GetConfig();
            foreach (var t in p.ListGeneratorsSettings)
            {
                var propType = typeof(object);
                var appGen = cfg.DicNodes[t.AppGeneratorGuid];
                // Generate a private field for the property
                FieldBuilder fldBldr = typeBldr.DefineField("_" + t.Name, propType, FieldAttributes.Private);
                // Generate a public property
                PropertyBuilder prptyBldr = typeBldr.DefineProperty(t.Name, PropertyAttributes.None, propType, new Type[] { propType });

                // The property set and property get methods need the following attributes:
                MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.HideBySig;
                // Define the "get" accessor method for newly created private field.
                MethodBuilder currGetPropMthdBldr = typeBldr.DefineMethod("get_value", GetSetAttr, propType, null);

                // Intermediate Language stuff... as per MS
                ILGenerator currGetIL = currGetPropMthdBldr.GetILGenerator();
                currGetIL.Emit(OpCodes.Ldarg_0);
                currGetIL.Emit(OpCodes.Ldfld, fldBldr);
                currGetIL.Emit(OpCodes.Ret);

                // Define the "set" accessor method for the newly created private field.
                MethodBuilder currSetPropMthdBldr = typeBldr.DefineMethod("set_value", GetSetAttr, null, new Type[] { propType });

                // More Intermediate Language stuff...
                ILGenerator currSetIL = currSetPropMthdBldr.GetILGenerator();
                currSetIL.Emit(OpCodes.Ldarg_0);
                currSetIL.Emit(OpCodes.Ldarg_1);
                currSetIL.Emit(OpCodes.Stfld, fldBldr);
                currSetIL.Emit(OpCodes.Ret);

                // Assign the two methods created above to the PropertyBuilder's Set and Get
                prptyBldr.SetGetMethod(currGetPropMthdBldr);
                prptyBldr.SetSetMethod(currSetPropMthdBldr);

                ConstructorInfo cons = typeof(ExpandableObjectAttribute).GetConstructor(new Type[] { });
                CustomAttributeBuilder attribute = new CustomAttributeBuilder(cons, new object[] { }, new FieldInfo[] { }, new object[] { });
                prptyBldr.SetCustomAttribute(attribute);

                cons = typeof(DescriptionAttribute).GetConstructor(new Type[] { typeof(string) });
                attribute = new CustomAttributeBuilder(cons, new object[] { t.Description }, new FieldInfo[] { }, new object[] { });
                prptyBldr.SetCustomAttribute(attribute);
            }
            var dynamicType = typeBldr.CreateType();
            var instance = Activator.CreateInstance(dynamicType);
            foreach (var t in p.ListGeneratorsSettings)
            {
                PropertyInfo prop = dynamicType.GetProperty(t.Name);
                prop.SetValue(instance, t.SettingsVm, null);
            }
            p.GenSettings = instance;
        }

        #endregion Node App Generator Settings
    }
}
