using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Documents;
using ViewModelBase;

// https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.assemblybuilder?view=netcore-3.1
// https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.fieldbuilder.setcustomattribute?view=netcore-3.1
namespace vSharpStudio.vm.ViewModels
{
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

        private static TypeBuilder GetTypeBuilder()
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

        private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
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
