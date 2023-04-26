/*************************************************************************************
   
   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
  public class SourceComboBoxEditor : ComboBoxEditor
  {
    ICollection _collection;
    TypeConverter _typeConverter;

    public SourceComboBoxEditor( ICollection collection, TypeConverter typeConverter )
    {
      _collection = collection;
      _typeConverter = typeConverter;
    }

    protected override IEnumerable CreateItemsSource( PropertyItem propertyItem )
    {
      return _collection;
    }

    protected override IValueConverter CreateValueConverter()
    {
            //When using a stringConverter, we need to convert the value
            if (_typeConverter != null)
            {
                if (_typeConverter is StringConverter)
                    return new SourceComboBoxEditorConverter(_typeConverter);
                //if (_typeConverter is EnumConverter)
                //    return new EnumDescriptionTypeConverter(_typeConverter);
            }
            return null;
    }
  }
    public class EnumDescriptionTypeConverter : EnumConverter
    {
        public EnumDescriptionTypeConverter(Type type)
            : base(type)
        {
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    FieldInfo fi = value.GetType().GetField(value.ToString());
                    if (fi != null)
                    {
                        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (attributes.Length > 0 && !String.IsNullOrEmpty(attributes[0].Description))
                            return attributes[0].Description;
                        return value.ToString();
                    }
                }
                return string.Empty;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal class SourceComboBoxEditorConverter : IValueConverter
  {
    private TypeConverter _typeConverter;

    internal SourceComboBoxEditorConverter( TypeConverter typeConverter )
    {
      _typeConverter = typeConverter;
    }

    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( _typeConverter != null )
      {
        if( _typeConverter.CanConvertTo( typeof(string) ) )
          return _typeConverter.ConvertTo( value, typeof(string) );
      }
      return value;
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( _typeConverter != null )
      {
        if( _typeConverter.CanConvertFrom( value.GetType() ) )
          return _typeConverter.ConvertFrom( value );
      }
      return value;
    }
  }
}
