using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public object Run(ITreeConfigNode node)
        {
            var nds = (IGetNodeSetting)node;
            TypeBuilder tbSol = SettingsTypeBuilder.GetTypeBuilder(); // type builder for solutions
            ConstructorBuilder constructor = tbSol.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            Config cfg = (Config)node.GetConfig();
            var dic_sols = new Dictionary<string, object>();
            foreach (var t in cfg.GroupAppSolutions.ListAppSolutions)
            {
                //string solName = "";
                //if (cfg.GroupAppSolutions.ListAppSolutions.Count > 1)
                //{
                //    solName = t.Name;
                //}
                SettingsTypeBuilder.CreateProperty(tbSol, t.Name, typeof(Object), t.NameUi, t.Description);
                TypeBuilder tbPrj = SettingsTypeBuilder.GetTypeBuilder();  // type builder for projects
                var dic_prjs = new Dictionary<string, object>();
                foreach (var tt in t.ListAppProjects)
                {
                    //string prjName = "";
                    //if (t.ListAppProjects.Count > 1)
                    //{
                    //    prjName = t.Name;
                    //}
                    TypeBuilder tbAppGen = SettingsTypeBuilder.GetTypeBuilder(); // type builder for app generators
                    var dic_apgs = new Dictionary<string, object>();
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        if (!string.IsNullOrWhiteSpace(ttt.PluginGuid))
                        {
                            if (!string.IsNullOrWhiteSpace(ttt.PluginGeneratorGuid))
                            {
                                TypeBuilder tbGen = SettingsTypeBuilder.GetTypeBuilder(); // type builder for generators
                                var dic_gs = new Dictionary<string, object>();
                                foreach (var tttt in ttt.ListGenerators)
                                {
                                    if (tttt.Generator == null)
                                        continue;
                                    IvPluginGenerator gen = tttt.Generator;
                                    if (gen is IvPluginDbConnStringGenerator)
                                    {
                                        gen = (gen as IvPluginDbConnStringGenerator).DbGenerator;
                                    }
                                    bool isFound = false;
                                    var lst = gen.GetListNodeGenerationSettings();
                                    foreach (var ts in lst)
                                    {
                                        if (nds.ContainsSettings(ttt.Guid, ts.Guid))
                                            isFound = true;
                                    }
                                    if (!isFound)
                                        continue;
                                    SettingsTypeBuilder.CreateProperty(tbGen, gen.Name, typeof(Object), gen.NameUi, gen.Description);
                                    TypeBuilder tbSet = SettingsTypeBuilder.GetTypeBuilder(); // type builder for generator settings
                                    foreach (var ts in lst)
                                    {
                                        if (nds.ContainsSettings(ttt.Guid, ts.Guid))
                                            SettingsTypeBuilder.CreateProperty(tbSet, ts.Name, typeof(Object));
                                    }
                                    SettingsTypeBuilder.CreateToString(tbSet, "Generator");
                                    Type nsType = tbSet.CreateType();
                                    object objSet = Activator.CreateInstance(nsType);
                                    foreach (var ts in lst)
                                    {
                                        if (nds.ContainsSettings(ttt.Guid, ts.Guid))
                                            nsType.InvokeMember(ts.Name, BindingFlags.SetProperty, null, objSet, new object[] { ((IGetNodeSetting)node).GetSettings(ttt.Guid, ts.Guid) });
                                    }
                                    dic_gs[gen.Name] = objSet;
                                }
                                SettingsTypeBuilder.CreateToString(tbGen, "Plugin");
                                Type settingsType = tbGen.CreateType();
                                object objGen = Activator.CreateInstance(settingsType);
                                foreach (var dt in dic_gs)
                                {
                                    settingsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objGen, new object[] { dt.Value });
                                }
                                if (dic_gs.Count > 0)
                                {
                                    dic_apgs[ttt.Name] = objGen;
                                    SettingsTypeBuilder.CreateProperty(tbAppGen, ttt.Name, typeof(Object), ttt.NameUi, ttt.Description);
                                }
                            }
                        }
                    }
                    SettingsTypeBuilder.CreateToString(tbAppGen, "Project");
                    Type apgsType = tbAppGen.CreateType();
                    object objAppGen = Activator.CreateInstance(apgsType);
                    foreach (var dt in dic_apgs)
                    {
                        apgsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, objAppGen, new object[] { dt.Value });
                    }
                    if (dic_apgs.Count > 0)
                    {
                        dic_prjs[tt.Name] = objAppGen;
                        SettingsTypeBuilder.CreateProperty(tbPrj, tt.Name, typeof(Object), tt.NameUi, tt.Description);
                    }
                }
                SettingsTypeBuilder.CreateToString(tbPrj, "Solution");
                Type prjsType = tbPrj.CreateType();
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

            return objSol;
        }
    }
    internal class GroupSettings
    {
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
    }
    //internal class NodeSettings : AppVisitorBase
    //{
    //    Dictionary<Object, Dictionary<string, object>> dic = new Dictionary<object, Dictionary<string, object>>();
    //    Dictionary<string, object> dicFields;
    //    Stack<TypeBuilder> stackBuilders = new Stack<TypeBuilder>();
    //    TypeBuilder tb;
    //    object? obj;
    //    public object Create(ITreeConfigNode node)
    //    {
    //        tb = SettingsTypeBuilder.GetTypeBuilder(); // type builder for solutions
    //        ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
    //        base.Run(node);
    //        Type objectType = tb.CreateType();
    //        var myObject = Activator.CreateInstance(objectType);
    //        dicFields = new Dictionary<string, object>();
    //        dic[myObject] = dicFields;
    //        //foreach (var field in lst)
    //        //    myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
    //        return myObject;
    //    }
    //    protected override void OnBeginCreateProperty(string name, AppSolution p)
    //    {
    //        //if (!string.IsNullOrWhiteSpace(name))
    //        //{
    //        SettingsTypeBuilder.CreateProperty(tb, p.Name, typeof(Object));
    //        stackBuilders.Push(tb);
    //        tb = SettingsTypeBuilder.GetTypeBuilder();  // type builder for projects
    //        //}
    //    }
    //    protected override void OnSetProperty(string name, AppSolution p)
    //    {
    //        Type objectType = tb.CreateType(); // create projects
    //        obj = Activator.CreateInstance(objectType);
    //        objectType.InvokeMember(p.Name, BindingFlags.SetProperty, null, obj, new object[] { field });
    //        tb = stackBuilders.Pop();
    //    }
    //    protected override void OnBeginCreateProperty(string name, AppProject p)
    //    {
    //        SettingsTypeBuilder.CreateProperty(tb, p.Name, typeof(Object));
    //        stackBuilders.Push(tb);
    //        tb = SettingsTypeBuilder.GetTypeBuilder(); // type builder for generators
    //    }
    //    protected override void OnSetProperty(string name, AppProject p)
    //    {
    //        Type objectType = tb.CreateType();
    //        val = Activator.CreateInstance(objectType);
    //        //objectType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
    //        tb = stackBuilders.Pop();
    //    }
    //    protected override void OnBeginCreateProperty(string name, AppProjectGenerator p)
    //    {
    //        SettingsTypeBuilder.CreateProperty(tb, p.Name, typeof(Object));
    //    }
    //    protected override void OnSetProperty(string name, AppProjectGenerator p)
    //    {
    //    }
    //}
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



        public static object CreateNewObject(ConfigNodesCollection<PluginGeneratorMainSettings> lst)
        {
            Contract.Requires(lst != null);
            var myType = CompileResultType(lst);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type CompileResultType(ConfigNodesCollection<PluginGeneratorMainSettings> lst)
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


        public static object CreateNewObject(ConfigNodesCollection<PluginGeneratorNodeSettings> lst)
        {
            Contract.Requires(lst != null);
            var myType = CompileResultType(lst);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type CompileResultType(ConfigNodesCollection<PluginGeneratorNodeSettings> lst)
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
