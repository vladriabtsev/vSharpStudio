using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ConfigObjectSubBase<T, TValidator> : ConfigObjectBase<T, TValidator>
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectSubBase<T, TValidator>//, IComparable<T>, ISortingValue 
    {
        public ConfigObjectSubBase(ITreeConfigNode parent, TValidator validator)
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
            if (cfg == null)
                return;
            _logger.LogTrace("Try Add Node Settings. {Count}".CallerInfo(), cfg.DicAppGenerators.Count);
            foreach (var dg in cfg.DicAppGenerators)
            {
                var gen = dg.Value;
                SearchPathAndAdd(dg.Key, ngs, gen);
            }
        }
        private void SearchPathAndAdd(string keyguid, INodeGenSettings ngs, IvPluginGenerator gen)
        {
            foreach (var t in gen.DicPathTypes)
            {
                string ss = this.ModelPath;
                var sp = t.Key.Split(".*.");
                bool is_found = true;
                int indx = 0;
                foreach (var s in sp)
                {
                    var sd = "." + s;
                    if (ss.Contains(s))
                    {
                        indx = ss.IndexOf(sd) + sd.Length;
                        ss = ss.Substring(indx);
                    }
                    else
                    {
                        is_found = false;
                        break;
                    }
                }

                if (is_found)
                {
                    _logger.LogTrace("Adding Node Settings. {Path}".CallerInfo(), t.Key);
                    GeneratorSettings gs = new GeneratorSettings(this);
                    ngs.ListGeneratorsSettings.Add(gs);
                    gs.AppGeneratorGuid = keyguid;
                    foreach (var tt in t.Value)
                    {
                        TypeSettings ts = new TypeSettings(this);
                        gs.ListTypeSettings.Add(ts);
                        ts.FullTypeName = tt;
                        ts.SettingsVm = gen.GetNodeGenerationSettingsVmFromJson(ts.FullTypeName, ts.Settings);
                    }
                    break;
                }
            }
        }
        public void RestoreNodeAppGenSettingsVm()
        {
            _logger.LogTrace();
            var ngs = (INodeGenSettings)this;
            var cfg = (Config)this.GetConfig();
            foreach (var t in ngs.ListGeneratorsSettings)
            {
                if (cfg.DicAppGenerators.ContainsKey(t.AppGeneratorGuid))
                {
                    var gen = cfg.DicAppGenerators[t.AppGeneratorGuid];
                    foreach (var tt in t.ListTypeSettings)
                    {
                        tt.SettingsVm = gen.GetNodeGenerationSettingsVmFromJson(tt.FullTypeName, tt.Settings);
                    }
                }
            }
        }
        public void SaveNodeAppGenSettings()
        {
            _logger.LogTrace();
            var ngs = (INodeGenSettings)this;
            foreach (var t in ngs.ListGeneratorsSettings)
            {
                foreach (var tt in t.ListTypeSettings)
                {
                    tt.Settings = tt.SettingsVm.SettingsAsJson;
                }
            }
        }
        public void RemoveNodeAppGenSettings(string appGenGuid)
        {
            _logger.LogTrace();
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
            _logger.LogTrace();
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
            var gen = cfg.DicAppGenerators[appGenGuid];
            SearchPathAndAdd(appGenGuid, ngs, gen);
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
                foreach (var tt in t.ListTypeSettings)
                {
                    var propType = typeof(object);
                    var appGen = cfg.DicNodes[t.Guid];
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
                }
            }
            var dynamicType = typeBldr.CreateType();
            var newGenSettings = Activator.CreateInstance(dynamicType);
            dynamic newClass = newGenSettings;
            foreach (var t in p.ListGeneratorsSettings)
            {
                foreach (var tt in t.ListTypeSettings)
                {
                    newClass[tt.Name] = tt.SettingsVm;
                }
            }
            p.GenSettings = newGenSettings;
        }

        #endregion Node App Generator Settings
    }
}
