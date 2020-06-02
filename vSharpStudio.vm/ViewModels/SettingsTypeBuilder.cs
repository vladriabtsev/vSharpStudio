using System;
using System.Collections.Generic;
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
        Stack<TypeBuilder> stackBuilders = new Stack<TypeBuilder>();
        TypeBuilder tb;
        object? obj;

        public object Run(ITreeConfigNode node)
        {
            var nds = (IGetNodeSetting)node;
            tb = SettingsTypeBuilder.GetTypeBuilder(); // type builder for solutions
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            Config cfg = (Config)node.GetConfig();
            var dic_sols = new Dictionary<string, object>();
            foreach (var t in cfg.GroupAppSolutions.ListAppSolutions)
            {
                string solName = "";
                if (cfg.GroupAppSolutions.ListAppSolutions.Count > 1)
                {
                    solName = t.Name;
                }
                SettingsTypeBuilder.CreateProperty(tb, t.Name, typeof(Object));
                stackBuilders.Push(tb);
                tb = SettingsTypeBuilder.GetTypeBuilder();  // type builder for projects
                var dic_prjs = new Dictionary<string, object>();
                foreach (var tt in t.ListAppProjects)
                {
                    string prjName = "";
                    if (t.ListAppProjects.Count > 1)
                    {
                        prjName = t.Name;
                    }
                    SettingsTypeBuilder.CreateProperty(tb, tt.Name, typeof(Object));
                    stackBuilders.Push(tb);
                    tb = SettingsTypeBuilder.GetTypeBuilder(); // type builder for app generators
                    var dic_apgs = new Dictionary<string, object>();
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        if (!string.IsNullOrWhiteSpace(ttt.PluginGuid))
                        {
                            if (!string.IsNullOrWhiteSpace(ttt.PluginGeneratorGuid))
                            {
                                SettingsTypeBuilder.CreateProperty(tb, ttt.Name, typeof(Object));

                                stackBuilders.Push(tb);
                                tb = SettingsTypeBuilder.GetTypeBuilder(); // type builder for generators
                                                                           //int len = 5;
                                                                           //CreateProperty(tb,
                                                                           //    solName.Substring(0, Math.Min(len, solName.Length)) +
                                                                           //    prjName.Substring(0, Math.Min(len, prjName.Length)) +
                                                                           //    ttt.Name, typeof(Object));
                                var dic_gs = new Dictionary<string, object>();
                                foreach (var tttt in ttt.ListGenerators)
                                {
                                    bool isFound = false;
                                    var lst = tttt.Generator.GetListNodeGenerationSettings();
                                    foreach (var ts in lst)
                                    {
                                        if (nds.ContainsSettings(ts.Guid))
                                            isFound = true;
                                    }
                                    if (!isFound)
                                        continue;
                                    SettingsTypeBuilder.CreateProperty(tb, tttt.Name, typeof(Object));

                                    stackBuilders.Push(tb);
                                    tb = SettingsTypeBuilder.GetTypeBuilder(); // type builder for generators
                                    foreach (var ts in lst)
                                    {
                                        if (nds.ContainsSettings(ts.Guid))
                                            SettingsTypeBuilder.CreateProperty(tb, ts.Name, typeof(Object));
                                    }
                                    Type nsType = tb.CreateType();
                                    obj = Activator.CreateInstance(nsType);
                                    foreach (var ts in lst)
                                    {
                                        if (nds.ContainsSettings(ts.Guid))
                                            nsType.InvokeMember(ts.Name, BindingFlags.SetProperty, null, obj, new object[] { ((IGetNodeSetting)node).GetSettings(ts.Guid) });
                                    }
                                    tb = stackBuilders.Pop();
                                    dic_gs[tttt.Name] = obj;

                                    SettingsTypeBuilder.CreateProperty(tb, tttt.Name, typeof(Object));
                                }
                                Type settingsType = tb.CreateType();
                                obj = Activator.CreateInstance(settingsType);
                                foreach (var dt in dic_gs)
                                {
                                    settingsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, obj, new object[] { dt.Value });
                                }
                                tb = stackBuilders.Pop();
                                dic_apgs[ttt.Name] = obj;
                            }
                        }
                    }
                    Type apgsType = tb.CreateType();
                    obj = Activator.CreateInstance(apgsType);
                    foreach (var dt in dic_apgs)
                    {
                        apgsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, obj, new object[] { dt.Value });
                    }
                    tb = stackBuilders.Pop();
                    dic_prjs[tt.Name] = obj;
                }
                Type prjsType = tb.CreateType();
                obj = Activator.CreateInstance(prjsType);
                foreach (var dt in dic_prjs)
                {
                    prjsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, obj, new object[] { dt.Value });
                }
                tb = stackBuilders.Pop();
                dic_sols[t.Name] = obj;
            }
            Type solsType = tb.CreateType();
            obj = Activator.CreateInstance(solsType);
            foreach (var dt in dic_sols)
            {
                solsType.InvokeMember(dt.Key, BindingFlags.SetProperty, null, obj, new object[] { dt.Value });
            }
            return obj;
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
            var myType = CompileNodesSettingResultType(lst);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type CompileNodesSettingResultType(SortedObservableCollection<PluginGeneratorNodeSettings> lst)
        {
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in lst)
                CreateProperty(tb, field.Name, typeof(Object));

            Type objectType = tb.CreateType();
            return objectType;
        }



        public static object CreateNewObject(ConfigNodesCollection<PluginGeneratorMainSettings> lst)
        {
            var myType = CompileResultType(lst);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type CompileResultType(ConfigNodesCollection<PluginGeneratorMainSettings> lst)
        {
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in lst)
                CreateProperty(tb, field.Name, typeof(Object));

            Type objectType = tb.CreateType();
            return objectType;
        }


        public static object CreateNewObject(ConfigNodesCollection<PluginGeneratorNodeSettings> lst)
        {
            var myType = CompileResultType(lst);
            var myObject = Activator.CreateInstance(myType);
            foreach (var field in lst)
                myType.InvokeMember(field.Name, BindingFlags.SetProperty, null, myObject, new object[] { field });
            return myObject;
        }
        public static Type CompileResultType(ConfigNodesCollection<PluginGeneratorNodeSettings> lst)
        {
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in lst)
                CreateProperty(tb, field.Name, typeof(Object));

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

        internal static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
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
            // Create the CustomAttribute instance of attribute of type 'MyAttribute1'.
            CustomAttributeBuilder attributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[0]);
            // Set the CustomAttribute 'MyAttribute1' to the Field.
            propertyBuilder.SetCustomAttribute(attributeBuilder);
        }
    }
}
