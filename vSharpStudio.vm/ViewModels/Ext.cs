using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public static class Ext
    {
        public static Proto.Attr.DicClassPropAttrs GetDicAttributes()
        {
            if (Ext.res != null)
                return Ext.res;
            Ext.res = new Proto.Attr.DicClassPropAttrs();
            Ext.res.DicByClass[typeof(Catalog).Name] = Catalog.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(Config).Name] = Config.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(Constant).Name] = Constant.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(DataType).Name] = DataType.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(Document).Name] = Document.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(Enumeration).Name] = Enumeration.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(EnumerationPair).Name] = EnumerationPair.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(GroupCatalogs).Name] = GroupCatalogs.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(GroupSubCatalogs).Name] = GroupSubCatalogs.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(GroupConstants).Name] = GroupConstants.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(GroupDocuments).Name] = GroupDocuments.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(GroupEnumerations).Name] = GroupEnumerations.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(GroupJournals).Name] = GroupJournals.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(GroupProperties).Name] = GroupProperties.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(GroupPropertiesTree).Name] = GroupPropertiesTree.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(Journal).Name] = Journal.GetDicPropertyAttributes();
            Ext.res.DicByClass[typeof(Property).Name] = Property.GetDicPropertyAttributes();
            return Ext.res;
        }
        private static Proto.Attr.DicClassPropAttrs res = null;


        internal static StringBuilder BrowsableAttribute(this StringBuilder prev, bool flag)
        {
            prev.Append("[BrowsableAttribute(");
            prev.Append(flag);
            prev.AppendLine(")]");
            return prev;
        }
        internal static StringBuilder CategoryAttribute(this StringBuilder prev, string category)
        {
            prev.Append("[Category(\"");
            prev.Append(category);
            prev.AppendLine("\"]");
            return prev;
        }
        internal static StringBuilder CategoryOrderAttribute(this StringBuilder prev, string category, int order)
        {
            prev.Append("[CategoryOrder(\"");
            prev.Append(category);
            prev.Append("\", ");
            prev.Append(order);
            prev.AppendLine("]");
            return prev;
        }
        internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, bool value)
        {
            prev.Append("[DefaultValue(");
            prev.Append(value.ToString());
            prev.AppendLine(")]");
            return prev;
        }
        internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, byte value)
        {
            prev.Append("[DefaultValue(");
            prev.Append(value);
            prev.AppendLine(")]");
            return prev;
        }
        internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, char value)
        {
            prev.Append("[DefaultValue('");
            prev.Append(value.ToString());
            prev.AppendLine("')]");
            return prev;
        }
        internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, double value)
        {
            prev.Append("[DefaultValue(");
            prev.Append(value.ToString());
            prev.AppendLine(")]");
            return prev;
        }
        internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, short value)
        {
            prev.Append("[DefaultValue(");
            prev.Append(value.ToString());
            prev.AppendLine(")]");
            return prev;
        }
        internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, int value)
        {
            prev.Append("[DefaultValue(");
            prev.Append(value.ToString());
            prev.AppendLine(")]");
            return prev;
        }
        internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, long value)
        {
            prev.Append("[DefaultValue(");
            prev.Append(value.ToString());
            prev.AppendLine(")]");
            return prev;
        }
        //internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, object value)
        //{
        //    prev.Append("[DefaultValue(");
        //    prev.Append(value);
        //    prev.AppendLine(")]");
        //    return prev;
        //}
        internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, float value)
        {
            prev.Append("[DefaultValue(");
            prev.Append(value.ToString());
            prev.AppendLine(")]");
            return prev;
        }
        internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, string value)
        {
            prev.Append("[DefaultValue(\"");
            prev.Append(value);
            prev.AppendLine("\")]");
            return prev;
        }
        internal static StringBuilder DefaultValueAttribute(this StringBuilder prev, Type type, string value)
        {
            prev.Append("[DefaultValue(");
            prev.Append(type.GetType().Name);
            prev.Append(", \"");
            prev.Append(value);
            prev.AppendLine("\")]");
            return prev;
        }
        internal static StringBuilder DescriptionAttribute(this StringBuilder prev, string description)
        {
            prev.Append("[Description(\"");
            prev.Append(description);
            prev.AppendLine("\")]");
            return prev;
        }
        internal static StringBuilder DisplayNameAttribute(this StringBuilder prev, string displayName)
        {
            prev.Append("[DisplayName(\"");
            prev.Append(displayName);
            prev.AppendLine("\")]");
            return prev;
        }
        internal static StringBuilder EditorAttribute(this StringBuilder prev)
        {
            prev.AppendLine("[Editor()]");
            return prev;
        }
        internal static StringBuilder EditorAttribute(this StringBuilder prev, string typeName, string baseTypeName)
        {
            prev.Append("[Editor(");
            prev.Append(typeName);
            prev.Append(", ");
            prev.Append(baseTypeName);
            prev.AppendLine(")]");
            return prev;
        }
        internal static StringBuilder EditorAttribute(this StringBuilder prev, string typeName, Type baseType)
        {
            prev.Append("[Editor(");
            prev.Append(typeName);
            prev.Append(", ");
            prev.Append(baseType.GetType().Name);
            prev.AppendLine(")]");
            return prev;
        }
        internal static StringBuilder EditorAttribute(this StringBuilder prev, Type type, Type baseType)
        {
            prev.Append("[Editor(");
            prev.Append(type.GetType().Name);
            prev.Append(", ");
            prev.Append(baseType.GetType().Name);
            prev.AppendLine(")]");
            return prev;
        }
        internal static StringBuilder ExpandableObjectAttribute(this StringBuilder prev)
        {
            prev.AppendLine("[ExpandableObject()]");
            return prev;
        }
        internal static StringBuilder PropertyOrderAttribute(this StringBuilder prev, int order)
        {
            prev.Append("[PropertyOrder(");
            prev.Append(order);
            prev.AppendLine(")]");
            return prev;
        }
    }
}
