#if DEBUG
#define _TRACE_n
#endif
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ViewModelBase
{
    public interface IHidePropertiesForXceedPropertyGrid
    {
        void HidePropertiesForXceedPropertyGrid(string[] lstExclude = null);
    }
    public class VmValidatableWithSeverityAndAttributes<T, TValidator> : VmValidatableWithSeverity<T, TValidator>, IHidePropertiesForXceedPropertyGrid
        where TValidator : AbstractValidator<T>
        where T : VmValidatableWithSeverityAndAttributes<T, TValidator>
    {
        public VmValidatableWithSeverityAndAttributes(TValidator validator) : base(validator)
        {
        }
        [BrowsableAttribute(false)]
        public bool AutoGenerateProperties { get { return this._AutoGenerateProperties; } set { SetProperty(ref _AutoGenerateProperties, value); } }
        private bool _AutoGenerateProperties = true;
        [BrowsableAttribute(false)]
        public Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionCollection PropertyDefinitions { get { return this._PropertyDefinitions; } set { SetProperty(ref _PropertyDefinitions, value); } }
        private Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionCollection _PropertyDefinitions;
        public void HidePropertiesForXceedPropertyGrid(string[] lstExclude=null)
        {
            //if (lstExclude.Count() > 0)
            //{
            this.AutoGenerateProperties = false;
            //}
            //else
            //{
            //    this.AutoGenerateProperties = true;
            //}
            var dic = new Dictionary<string, string>();
            if (lstExclude != null)
            {
                foreach (var t in lstExclude)
                {
                    dic[t] = null;
                }
            }
            var res = new Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionCollection();

            var descriptors = TypeDescriptor.GetProperties(this.GetType());
            foreach (PropertyDescriptor t in descriptors)
            {
#if _TRACE_
                Trace.Write("   ***>");
                Trace.Write(t.PropertyType.Name);
                Trace.Write("   ");
                Trace.Write(t.DisplayName);
                Trace.Write(": ");
                //Trace.Write(t..DisplayName);
                //Trace.Write(".");
                Trace.Write(t.Name);
                if (dic.ContainsKey(t.Name))
                {
                    Trace.WriteLine("  --- skip ---");
                    continue;
                }
                Trace.WriteLine("");
#else
                if (dic.ContainsKey(t.Name))
                    continue;
#endif
                var pd = new Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinition();
                pd.TargetProperties.Add(t.Name);

                bool is_skip = false;
                foreach (var tt in t.Attributes)
                {
                    var attrName = tt.GetType().Name;
                    switch (attrName)
                    {
                        case "BrowsableAttribute":
                            if (!(tt as BrowsableAttribute).Browsable)
                                is_skip = true;
                            break;
                        case "CategoryAttribute":
                            pd.Category = (tt as CategoryAttribute).Category;
                            break;
                        case "DescriptionAttribute":
                            pd.Description = (tt as DescriptionAttribute).Description;
                            break;
                        case "DisplayNameAttribute":
                            pd.DisplayName = (tt as DisplayNameAttribute).DisplayName;
                            break;
                        case "PropertyOrderAttribute":
                            pd.DisplayOrder = (tt as PropertyOrderAttribute).Order;
                            break;
                        case "ExpandableObjectAttribute":
                            pd.IsExpandable = true;
                            break;
                        default:
                            break;
                    }
                    if (is_skip)
                        continue;
                }
                if (is_skip)
                    continue;
                res.Add(pd);
            }
            this.PropertyDefinitions = res;
        }
    }
}
