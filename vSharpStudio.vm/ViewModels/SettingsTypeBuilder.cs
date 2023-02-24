using System;
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
        public object? Run(ITreeConfigNode node, bool isShortVersion)
        {
            TypeBuilder tbSol = SettingsTypeBuilder.GetTypeBuilder(); // type builder for solutions
            ConstructorBuilder constructor = tbSol.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            Config cfg = (Config)node.Cfg;
            var dic_sols = new Dictionary<string, object>();
            bool is_projects = false;
            bool is_generators = false;
            {
                var sln_with_gen = 0;
                var prj_with_gen = 0;
                var with_gen = 0;
                foreach (var t in cfg.GroupAppSolutions.ListAppSolutions)
                {
                    var j = 0;
                    foreach (var tt in t.ListAppProjects)
                    {
                        if (tt.ListAppProjectGenerators.Count > 0)
                        {
                            prj_with_gen++;
                            with_gen++;
                            j++;
                        }
                    }
                    if (j > 0)
                        sln_with_gen++;
                }
                if (sln_with_gen > 1) { }
                else if (prj_with_gen > 1)
                    is_projects = true;
                else if (with_gen > 0)
                    is_generators = true;
                else
                    return null;
            }
            foreach (var t in cfg.GroupAppSolutions.ListAppSolutions)
            {
                TypeBuilder tbPrjs = SettingsTypeBuilder.GetTypeBuilder();  // type builder for projects
                var dic_prjs = new Dictionary<string, object>();
                foreach (var tt in t.ListAppProjects)
                {
                    Dictionary<string, object> dic_apgs = new Dictionary<string, object>();

                    var objAppGens = CreateSettingsForProject(node, tt, dic_apgs, isShortVersion);
                    if (is_generators) // only generators level
                        return objAppGens;
                    if (objAppGens != null)
                    {
                        dic_prjs[tt.Name] = objAppGens;
                        SettingsTypeBuilder.CreateProperty(tbPrjs, tt.Name, typeof(object), tt.NameUi, tt.Description);
                    }
                }
                if (is_projects && dic_prjs.Count == 0) // projects level
                    return null;
                SettingsTypeBuilder.CreateToString(tbPrjs, $"Apps->{t.Name}->");
                Type? prjsType = tbPrjs.CreateType();
                Debug.Assert(prjsType != null);
                object? objPrjs = Activator.CreateInstance(prjsType);
                Debug.Assert(objPrjs != null);
                foreach (var dt in dic_prjs)
                {
                    prjsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objPrjs, new object[] { dt.Value });
                }
                if (is_projects) // projects level
                    return objPrjs;
                dic_sols[t.Name] = objPrjs;
                SettingsTypeBuilder.CreateProperty(tbSol, t.Name, typeof(object), t.NameUi, t.Description);
            }
            if (dic_sols.Count == 0) // solutions level
                return null;
            SettingsTypeBuilder.CreateToString(tbSol, "Apps->");
            Type? solsType = tbSol.CreateType();
            Debug.Assert(solsType != null);
            object? objSol = Activator.CreateInstance(solsType);
            Debug.Assert(objSol != null);
            foreach (var dt in dic_sols)
            {
                solsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objSol, new object[] { dt.Value });
            }
            return objSol;
        }
        private static object? CreateSettingsForProject(ITreeConfigNode node, IAppProject tt, Dictionary<string, object> dic_apgs, bool isShortVersion)
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
                        if (nsettings != null)
                        {
                            dic_apgs[ttt.Name] = nsettings;
                            SettingsTypeBuilder.CreateProperty(tbAppGen, ttt.Name, nsettings.GetType(), ttt.NameUi, ttt.Description);
                        }
                    }
                }
            }
            if (dic_apgs.Count == 0)
                return null;
            SettingsTypeBuilder.CreateToString(tbAppGen, $"Apps->{tt.ParentAppSolutionI.Name}->{tt.Name}->");
            Type? apgsType = tbAppGen.CreateType();
            Debug.Assert(apgsType != null);
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
        private static object? CreateSettingsForAppProjectGenerator(ITreeConfigNode node, AppProjectGenerator ttt, Dictionary<string, object> dic_gs, bool isShortVersion)
        {
            var nds = (IGetNodeSetting)node;
            TypeBuilder tbGen = SettingsTypeBuilder.GetTypeBuilder(); // type builder for generators
            Debug.Assert(ttt.ListGenerators != null);
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
            Type? settingsType = tbGen.CreateType();
            Debug.Assert(settingsType != null);
            var objGen = Activator.CreateInstance(settingsType);
            Debug.Assert(objGen != null);
            foreach (var dt in dic_gs)
            {
                settingsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objGen, new object[] { dt.Value });
            }
            return objGen;
        }
        public object? Run(AppSolution node)
        {
            TypeBuilder tbSettings = SettingsTypeBuilder.GetTypeBuilder(); // type builder for solutions
            ConstructorBuilder constructor = tbSettings.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            var dic_groups = new Dictionary<string, object>();
            foreach (var t in node.DicPluginsGroupSettings)
            {
                Debug.Assert(t.Value != null);
                string groupName = t.Value.Name;
                SettingsTypeBuilder.CreateProperty(tbSettings, t.Value.Name, typeof(Object), t.Value.Name, t.Value.Description);
                dic_groups[groupName] = t.Value;
            }
            if (dic_groups.Count == 0)
                return null;
            Type? settingsType = tbSettings.CreateType();
            Debug.Assert(settingsType != null);
            var objSettings = Activator.CreateInstance(settingsType);
            Debug.Assert(objSettings != null);
            foreach (var dt in dic_groups)
            {
                settingsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objSettings, new object[] { dt.Value });
            }
            return objSettings;
        }
        public object? Run(AppProject node)
        {
            TypeBuilder tbSettings = SettingsTypeBuilder.GetTypeBuilder(); // type builder for solutions
            ConstructorBuilder constructor = tbSettings.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            var dic_groups = new Dictionary<string, object>();
            foreach (var t in node.DicPluginsGroupSettings)
            {
                Debug.Assert(t.Value != null);
                string groupName = t.Value.Name;
                SettingsTypeBuilder.CreateProperty(tbSettings, t.Value.Name, typeof(Object), t.Value.Name, t.Value.Description);
                dic_groups[groupName] = t.Value;
            }
            if (dic_groups.Count == 0)
                return null;
            Type? settingsType = tbSettings.CreateType();
            Debug.Assert(settingsType != null);
            var objSettings = Activator.CreateInstance(settingsType);
            Debug.Assert(objSettings != null);
            foreach (var dt in dic_groups)
            {
                settingsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objSettings, new object[] { dt.Value });
            }
            return objSettings;
        }
        public object? Run(AppProjectGenerator node)
        {
            if (!string.IsNullOrWhiteSpace(node.PluginGuid))
            {
                if (!string.IsNullOrWhiteSpace(node.PluginGeneratorGuid))
                {
                    Dictionary<string, object> dic_gs = new Dictionary<string, object>();
                    var objGen = CreateSettingsForAppProjectGenerator(node.ParentAppProject.ParentAppSolution.ParentGroupListAppSolutions.ParentConfig.Model, node, dic_gs, true);
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
            public string? Name { get; set; }
            public object? Value { get; set; }
        }
        public static object? CreateNodesSettingObject(SortedObservableCollection<PluginGeneratorNodeSettings> lst)
        {
            Debug.Assert(lst != null);
            var myType = CompileNodesSettingResultType(lst);
            Debug.Assert(myType != null);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type? CompileNodesSettingResultType(SortedObservableCollection<PluginGeneratorNodeSettings> lst)
        {
            Debug.Assert(lst != null);
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in lst)
                CreateProperty(tb, field.Name, typeof(Object), field.NameUi);

            Type? objectType = tb.CreateType();
            return objectType;
        }
        public static object? CreateNewObject(ObservableCollection<PluginGeneratorSettings> lst)
        {
            Debug.Assert(lst != null);
            var myType = CompileResultType(lst);
            Debug.Assert(myType != null);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type? CompileResultType(ObservableCollection<PluginGeneratorSettings> lst)
        {
            Debug.Assert(lst != null);
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in lst)
                CreateProperty(tb, field.Name, typeof(Object), field.NameUi);

            Type? objectType = tb.CreateType();
            return objectType;
        }


        public static object? CreateNewObject(SortedObservableCollection<PluginGeneratorNodeSettings> lst)
        {
            Debug.Assert(lst != null);
            var myType = CompileResultType(lst);
            Debug.Assert(myType != null);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type? CompileResultType(SortedObservableCollection<PluginGeneratorNodeSettings> lst)
        {
            Debug.Assert(lst != null);
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in lst)
                CreateProperty(tb, field.Name, typeof(Object), field.NameUi);

            Type? objectType = tb.CreateType();
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
            var minfo = typeof(object).GetMethod("ToString");
            Debug.Assert(minfo != null);
            tb.DefineMethodOverride(mthdBldr, minfo);
        }
        internal static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType, string? propertyNameUI = null, string? propertyDescription = null)
        {
            //if (propertyName.Contains('.'))
            //    propertyName = propertyName.Substring(0, propertyName.IndexOf('.'));
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
            ConstructorInfo? constructorInfo = attr.GetConstructor(new Type[0]);
            Debug.Assert(constructorInfo != null);
            CustomAttributeBuilder attributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[0]);
            propertyBuilder.SetCustomAttribute(attributeBuilder);

            attr = typeof(ReadOnlyAttribute);
            constructorInfo = attr.GetConstructor(new Type[1] { typeof(bool) });
            Debug.Assert(constructorInfo != null);
            attributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[1] { true });
            propertyBuilder.SetCustomAttribute(attributeBuilder);

            if (!string.IsNullOrWhiteSpace(propertyNameUI))
            {
                attr = typeof(DisplayNameAttribute);
                constructorInfo = attr.GetConstructor(new Type[1] { typeof(string) });
                Debug.Assert(constructorInfo != null);
                attributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[1] { propertyNameUI });
                propertyBuilder.SetCustomAttribute(attributeBuilder);
            }
            if (!string.IsNullOrWhiteSpace(propertyDescription))
            {
                attr = typeof(DescriptionAttribute);
                constructorInfo = attr.GetConstructor(new Type[1] { typeof(string) });
                Debug.Assert(constructorInfo != null);
                attributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[1] { propertyDescription });
                propertyBuilder.SetCustomAttribute(attributeBuilder);
            }
        }
    }
}
