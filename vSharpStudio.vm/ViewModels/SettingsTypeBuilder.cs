﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Documents;
using ViewModelBase;
using vSharpStudio.common;

// https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.assemblybuilder?view=netcore-3.1
// https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.fieldbuilder.setcustomattribute?view=netcore-3.1
namespace vSharpStudio.vm.ViewModels
{
    internal class NodeSettings
    {
        public object Run(ITreeConfigNode node, bool isShortVersion)
        {
            TypeBuilder tbSol = SettingsTypeBuilder.GetTypeBuilder(); // type builder for solutions
            ConstructorBuilder constructor = tbSol.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            Config cfg = (Config)node.GetConfig();
            var dic_sols = new Dictionary<string, object>();
            foreach (var t in cfg.GroupAppSolutions.ListAppSolutions)
            {
                TypeBuilder tbPrj = SettingsTypeBuilder.GetTypeBuilder();  // type builder for projects
                var dic_prjs = new Dictionary<string, object>();
                foreach (var tt in t.ListAppProjects)
                {
                    Dictionary<string, object> dic_apgs = new Dictionary<string, object>();

                    var objAppGen = CreateSettingsForProject(node, tt, dic_apgs, isShortVersion);

                    if (dic_apgs.Count > 0)
                    {
                        dic_prjs[tt.Name] = objAppGen;
                        SettingsTypeBuilder.CreateProperty(tbPrj, tt.Name, objAppGen.GetType(), tt.NameUi, tt.Description);
                    }
                }
                SettingsTypeBuilder.CreateToString(tbPrj, "Solution");
                Type prjsType = tbPrj.CreateType();
                SettingsTypeBuilder.CreateProperty(tbSol, t.Name, prjsType, t.NameUi, t.Description);
                object objPrj = Activator.CreateInstance(prjsType);
                foreach (var dt in dic_prjs)
                {
                    prjsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objPrj, new object[] { dt.Value });
                }
                //if (dic_prjs.Count > 0)
                dic_sols[t.Name] = objPrj;
            }
            SettingsTypeBuilder.CreateToString(tbSol, "Solutions");
            Type solsType = tbSol.CreateType();
            object objSol = Activator.CreateInstance(solsType);
            foreach (var dt in dic_sols)
            {
                if (dic_sols.Count == 1) // remove solution level node in settings
                    return dt.Value;
                solsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objSol, new object[] { dt.Value });
            }
            if (dic_sols.Count == 0)
                return null;
            return objSol;
        }
        private static object CreateSettingsForProject(ITreeConfigNode node, IAppProject tt, Dictionary<string, object> dic_apgs, bool isShortVersion)
        {
            TypeBuilder tbAppGen = SettingsTypeBuilder.GetTypeBuilder(); // type builder for app generators
            foreach (var ttt in tt.ListAppProjectGenerators)
            {
                if (!string.IsNullOrWhiteSpace(ttt.PluginGuid))
                {
                    if (!string.IsNullOrWhiteSpace(ttt.PluginGeneratorGuid))
                    {
                        var nds = (IGetNodeSetting)node;
                        var nsettings = nds.GetSettings(ttt.Guid);
                        dic_apgs[ttt.Name] = nsettings;
                        SettingsTypeBuilder.CreateProperty(tbAppGen, ttt.Name, nsettings.GetType(), ttt.NameUi, ttt.Description);
                    }
                }
            }
            SettingsTypeBuilder.CreateToString(tbAppGen, "Project");
            Type apgsType = tbAppGen.CreateType();
            var objAppGen = Activator.CreateInstance(apgsType);
            foreach (var dt in dic_apgs)
            {
                apgsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objAppGen, new object[] { dt.Value });
            }
            return objAppGen;
        }
        //private static object CreateSettingsForProject(ITreeConfigNode node, AppProject tt, Dictionary<string, object> dic_apgs, bool isShortVersion)
        //{
        //    TypeBuilder tbAppGen = SettingsTypeBuilder.GetTypeBuilder(); // type builder for app generators
        //    foreach (var ttt in tt.ListAppProjectGenerators)
        //    {
        //        if (!string.IsNullOrWhiteSpace(ttt.PluginGuid))
        //        {
        //            if (!string.IsNullOrWhiteSpace(ttt.PluginGeneratorGuid))
        //            {
        //                Dictionary<string, object> dic_gs = new Dictionary<string, object>();

        //                object objGen = null;
        //                objGen = CreateSettingsForAppProjectGenerator(node, ttt, dic_gs, isShortVersion);

        //                if (dic_gs.Count > 0)
        //                {
        //                    dic_apgs[ttt.Name] = objGen;
        //                    SettingsTypeBuilder.CreateProperty(tbAppGen, ttt.Name, typeof(Object), ttt.NameUi, ttt.Description);
        //                }
        //            }
        //        }
        //    }
        //    SettingsTypeBuilder.CreateToString(tbAppGen, "Project");
        //    Type apgsType = tbAppGen.CreateType();
        //    var objAppGen = Activator.CreateInstance(apgsType);
        //    foreach (var dt in dic_apgs)
        //    {
        //        apgsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objAppGen, new object[] { dt.Value });
        //    }
        //    return objAppGen;
        //}
        private static object CreateSettingsForAppProjectGenerator(ITreeConfigNode node, AppProjectGenerator ttt, Dictionary<string, object> dic_gs, bool isShortVersion)
        {
            var nds = (IGetNodeSetting)node;
            TypeBuilder tbGen = SettingsTypeBuilder.GetTypeBuilder(); // type builder for generators
            foreach (var tttt in ttt.ListGenerators)
            {
                if (tttt.Generator == null)
                    continue;
                if (isShortVersion && (tttt.Guid != ttt.PluginGeneratorGuid))
                    continue;
                IvPluginGenerator gen = tttt.Generator;
                var nsettings = nds.GetSettings(ttt.Guid);
                if (nsettings == null)
                    continue;
                SettingsTypeBuilder.CreateProperty(tbGen, gen.Name, nsettings.GetType(), gen.NameUi, gen.Description);
                dic_gs[gen.Name] = nsettings;
            }
            SettingsTypeBuilder.CreateToString(tbGen, "Generator");
            Type settingsType = tbGen.CreateType();
            var objGen = Activator.CreateInstance(settingsType);
            foreach (var dt in dic_gs)
            {
                settingsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objGen, new object[] { dt.Value });
            }
            return objGen;
        }
        public object Run(AppSolution node)
        {
            TypeBuilder tbSettings = SettingsTypeBuilder.GetTypeBuilder(); // type builder for solutions
            ConstructorBuilder constructor = tbSettings.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            Config cfg = (Config)node.GetConfig();
            var dic_groups = new Dictionary<string, object>();
            foreach (var t in node.DicPluginsGroupSettings)
            {
                string groupName = t.Value.Name;
                SettingsTypeBuilder.CreateProperty(tbSettings, t.Value.Name, typeof(Object), t.Value.Name, t.Value.Description);
                dic_groups[groupName] = t.Value;
            }
            if (dic_groups.Count == 0)
                return null;
            Type settingsType = tbSettings.CreateType();
            object objSettings = Activator.CreateInstance(settingsType);
            foreach (var dt in dic_groups)
            {
                settingsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objSettings, new object[] { dt.Value });
            }
            return objSettings;
        }
        public object Run(AppProjectGenerator node)
        {
            if (!string.IsNullOrWhiteSpace(node.PluginGuid))
            {
                if (!string.IsNullOrWhiteSpace(node.PluginGeneratorGuid))
                {
                    Dictionary<string, object> dic_gs = new Dictionary<string, object>();
                    var cfg = (Config)node.GetConfig();
                    object objGen = CreateSettingsForAppProjectGenerator(cfg.Model, node, dic_gs, true);
                    return objGen;
                }
            }
            return null;
        }
        //public void Run(ConfigModel node)
        //{
        //    node.DynamicNodeDefaultSettings = this.Run((ITreeConfigNode)node, true);
        //}
    }
    public static class SettingsTypeBuilder
    {
        public class Field
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
        public static object CreateNodesSettingObject(SortedObservableCollection<PluginGeneratorNodeSettings> lst)
        {
            Contract.Requires(lst != null);
            var myType = CompileNodesSettingResultType(lst);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type CompileNodesSettingResultType(SortedObservableCollection<PluginGeneratorNodeSettings> lst)
        {
            Contract.Requires(lst != null);
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in lst)
                CreateProperty(tb, field.Name, typeof(Object), field.NameUi);

            Type objectType = tb.CreateType();
            return objectType;
        }
        public static object CreateNewObject(ObservableCollection<PluginGeneratorSettings> lst)
        {
            Contract.Requires(lst != null);
            var myType = CompileResultType(lst);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type CompileResultType(ObservableCollection<PluginGeneratorSettings> lst)
        {
            Contract.Requires(lst != null);
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in lst)
                CreateProperty(tb, field.Name, typeof(Object), field.NameUi);

            Type objectType = tb.CreateType();
            return objectType;
        }


        public static object CreateNewObject(SortedObservableCollection<PluginGeneratorNodeSettings> lst)
        {
            Contract.Requires(lst != null);
            var myType = CompileResultType(lst);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type CompileResultType(SortedObservableCollection<PluginGeneratorNodeSettings> lst)
        {
            Contract.Requires(lst != null);
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in lst)
                CreateProperty(tb, field.Name, typeof(Object), field.NameUi);

            Type objectType = tb.CreateType();
            return objectType;
        }

        internal static TypeBuilder GetTypeBuilder()
        {
            var typeSignature = "SettingsDynamicType";
            var an = new AssemblyName(typeSignature);
            // AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(typeSignature,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);
            return tb;
        }
        // https://en.wikipedia.org/wiki/Common_Intermediate_Language
        internal static void CreateToString(TypeBuilder tb, string description)
        {
            MethodBuilder mthdBldr = tb.DefineMethod("ToString",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final,
                typeof(string), Type.EmptyTypes);
            ILGenerator getIl = mthdBldr.GetILGenerator();
            getIl.Emit(OpCodes.Ldstr, description);
            getIl.Emit(OpCodes.Ret);
            tb.DefineMethodOverride(mthdBldr, typeof(object).GetMethod("ToString"));
        }
        internal static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType, string propertyNameUI = null, string propertyDescription = null)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);

            Type attr = typeof(Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ExpandableObjectAttribute);
            // Create a Constructorinfo object for attribute 'MyAttribute1'.
            //ConstructorInfo myConstructorInfo = attr.GetConstructor(               new Type[1] { typeof(string) });
            ConstructorInfo constructorInfo = attr.GetConstructor(new Type[0]);
            CustomAttributeBuilder attributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[0]);
            propertyBuilder.SetCustomAttribute(attributeBuilder);

            attr = typeof(ReadOnlyAttribute);
            constructorInfo = attr.GetConstructor(new Type[1] { typeof(bool) });
            attributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[1] { true });
            propertyBuilder.SetCustomAttribute(attributeBuilder);

            if (!string.IsNullOrWhiteSpace(propertyNameUI))
            {
                attr = typeof(DisplayNameAttribute);
                constructorInfo = attr.GetConstructor(new Type[1] { typeof(string) });
                attributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[1] { propertyNameUI });
                propertyBuilder.SetCustomAttribute(attributeBuilder);
            }
            if (!string.IsNullOrWhiteSpace(propertyDescription))
            {
                attr = typeof(DescriptionAttribute);
                constructorInfo = attr.GetConstructor(new Type[1] { typeof(string) });
                attributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[1] { propertyDescription });
                propertyBuilder.SetCustomAttribute(attributeBuilder);
            }
        }
    }
}
