using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Markup;

namespace ViewModelBase
{
    public class VMHelpers
    {
        // https://msdn.microsoft.com/en-us/library/ms973852.aspx
        //public static void InternalNotifyPropertyChanged(string propertyName,
        //		object sender, PropertyChangedEventHandler propertyChanged, Dispatcher dispatcher)
        //{
        //	if (propertyChanged != null)
        //	{
        //              if (dispatcher == null)
        //                  return;
        //		PropertyChangedEventArgs arg = new PropertyChangedEventArgs(propertyName);
        //		// Fire the event on the UI thread
        //		if (dispatcher.CheckAccess())
        //			propertyChanged(sender, arg);
        //		else
        //			dispatcher.BeginInvoke(() => propertyChanged(sender, arg));
        //	}
        //}


        //public static void InternalNotifyPropertyChanged(string propertyName,
        //		object sender, PropertyChangedEventHandler propertyChanged, IDispatcher dispatcher)
        //{
        //	if (propertyChanged != null)
        //	{
        //		PropertyChangedEventArgs arg;
        //		lock (lockobject)
        //		{
        //			if (!s_EventArgs.TryGetValue(propertyName, out arg))
        //			{
        //				arg = new PropertyChangedEventArgs(propertyName);
        //				s_EventArgs[propertyName] = arg;
        //			}
        //		}
        //		// Fire the event on the UI thread
        //		if (dispatcher.CheckAccess())
        //			propertyChanged(sender, arg);
        //		else
        //			dispatcher.BeginInvoke(() => propertyChanged(sender, arg));
        //	}
        //}
        //static object lockobject = new object();
        //static readonly Dictionary<string, PropertyChangedEventArgs> s_EventArgs = new Dictionary<string, PropertyChangedEventArgs>(1000);


        //		public static Dictionary<string, PropertyChangedEventArgs> BuildEventArgsDictionary(Type type)
        //		{
        //#if NETFX_CORE
        //			var properties = type.GetRuntimeProperties();
        //			var result = new Dictionary<string, PropertyChangedEventArgs>(properties.Count());
        //#else
        //			var properties = type.GetProperties();
        //			var result = new Dictionary<string, PropertyChangedEventArgs>(properties.Length);
        //#endif
        //			foreach (var property in properties)
        //			{
        //				result[property.Name] = new PropertyChangedEventArgs(property.Name);
        //			}
        //			return result;
        //		}

        //public enum TrussLocationCase
        //{
        //	[Description("All Cases")]
        //	AllCases,
        //	[Description("1A")]
        //	OneA,
        //	[Description("1B")]
        //	OneB,
        //	[Description("2C")]
        //}

        //DataGridViewComboBoxColumn trussLocationComboBoxColumn = trussLocationColumn as DataGridViewComboBoxColumn;
        //trussLocationComboBoxColumn.DataSource = EnumUtils.GetEnumComboBox<TrussLocationCase>();
        //      trussLocationComboBoxColumn.DisplayMember = "Display";
        //      trussLocationComboBoxColumn.ValueMember = "Value";


        /// <summary>
        /// Return the contents of the enumeration as formatted for a combo box
        /// relying on the Description attribute containing the display value
        /// within the enum definition
        /// </summary>
        /// <typeparam name="Tenum">The type of the enum being retrieved</typeparam>
        /// <returns>The collection of enum values and description fields</returns>
        public static ICollection<ComboBoxLoader<Tenum>> GetEnumComboBox<Tenum>()
        {
            ICollection<ComboBoxLoader<Tenum>> result = new List<ComboBoxLoader<Tenum>>();
            foreach (Tenum e in Enum.GetValues(typeof(Tenum)))
            {
                ComboBoxLoader<Tenum> value = new ComboBoxLoader<Tenum>();
                try
                {
                    var fnam = e.ToString();
                    Debug.Assert(fnam != null);
                    MemberInfo? memberInfo = typeof(Tenum).GetMember(fnam).FirstOrDefault();
                    Debug.Assert(memberInfo != null);
                    var attributeObject = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                    if (attributeObject == null)
                    {
                        value.Display = string.Empty;
                        continue;
                    }
                    var attribute = (DescriptionAttribute)attributeObject;
                    Debug.Assert(attribute != null);
                    value.Display = attribute.Description;
                }
                catch (NullReferenceException)
                {
                    // This exception received when no Description attribute
                    // associated with Enum members
                    value.Display = e.ToString();
                }
                value.Value = e;
                result.Add(value);
            }
            return result;
        }
        public static Tattr? GetAttribute<Tattr>(Enum enumValue) where Tattr : Attribute
        {
            MemberInfo? memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
            if (memberInfo != null)
            {
                var attributeObject = memberInfo.GetCustomAttributes(typeof(Tattr), false).FirstOrDefault();
                Debug.Assert(attributeObject != null);
                return (Tattr)attributeObject;
            }
            return null;
        }
    }
    public class EnumBindingSourceExtension : MarkupExtension
    {
        private Type? _enumType;
        public Type? EnumType
        {
            get { return this._enumType; }
            set
            {
                if (value != this._enumType)
                {
                    if (null != value)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;

                        if (!enumType.IsEnum)
                            throw new ArgumentException("Type must be for an Enum.");
                    }

                    this._enumType = value;
                }
            }
        }
        public EnumBindingSourceExtension() { }
        public EnumBindingSourceExtension(Type enumType)
        {
            this.EnumType = enumType;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == this._enumType)
                throw new InvalidOperationException("The EnumType must be specified.");

            Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
            Array enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == this._enumType)
                return enumValues;

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }

    /// <summary>
    /// Class to provide assistance for separation of concern
    /// over contents of combo box where the
    /// displayed value does not match the ToString 
    /// support.
    /// </summary>
    /// <typeparam name="T">The type of the value the combo box supports</typeparam>
    [DebuggerDisplay("ComboBoxLoader {Display} {Value.ToString()}")]
    public class ComboBoxLoader<T>
    {
        /// <summary>
        /// The value to display in the combo box
        /// </summary>
        public string? Display { get; set; }

        /// <summary>
        /// The actual object associated with the combo box item
        /// </summary>
        public T? Value { get; set; }
    }
}

