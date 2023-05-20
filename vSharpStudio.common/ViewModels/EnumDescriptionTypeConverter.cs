using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace vSharpStudio.common.ViewModels
{
    public class EnumDescriptionTypeConverter : EnumConverter
    {
        public EnumDescriptionTypeConverter(Type type)
            : base(type)
        {
        }
        public override object ConvertTo(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    var nam = value.ToString();
                    Debug.Assert(nam != null);
                    FieldInfo? fi = EnumType.GetField(nam);
                    if (fi != null)
                    {
                        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (attributes.Length > 0 && !String.IsNullOrEmpty(attributes[0].Description))
                            return attributes[0].Description;
                        return nam;
                    }
                }
                return string.Empty;
            }
            var res = base.ConvertTo(context, culture, value, destinationType);
            Debug.Assert(res != null);
            return res;
        }
    }
}
