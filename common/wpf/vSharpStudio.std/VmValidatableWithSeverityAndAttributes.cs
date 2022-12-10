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
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ViewModelBase
{
    //public interface IHidePropertiesForXceedPropertyGrid
    //{
    //    void HidePropertiesForXceedPropertyGrid(string[] lstExclude = null);
    //}
    public class VmValidatableWithSeverityAndAttributes<T, TValidator> : VmValidatableWithSeverity<T, TValidator>
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
        public Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionCollection PropertyDefinitions
        {
            get
            {
                //if (this.inGetUpdatedPropertyDefinitions)
                //    return _PropertyDefinitions;
                //else // to react on data binding in PropertyGrid
                return this.GetUpdatedPropertyDefinitions();
            }
            set { SetProperty(ref _PropertyDefinitions, value); }
        }
        private Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionCollection _PropertyDefinitions = null;
        protected virtual string[] OnGetWhatHideOnPropertyGrid()
        {
            return null;
        }
        private Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionCollection GetUpdatedPropertyDefinitions()
        {
            string[] lstExclude = this.OnGetWhatHideOnPropertyGrid();
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

            if (this._PropertyDefinitions == null)
            {
                var descriptors = TypeDescriptor.GetProperties(this.GetType());
                foreach (PropertyDescriptor t in descriptors)
                {
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
                this._PropertyDefinitions = res;
                res = new Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionCollection();
            }
            foreach (var t in this._PropertyDefinitions)
            {
                Debug.Assert(t.TargetProperties.Count == 1);
                Debug.Assert(t.TargetProperties[0] is string);
                string nam = (string)t.TargetProperties[0];
#if _TRACE_
                Trace.Write("   ***>");
                Trace.Write(t.DependencyObjectType.Name);
                Trace.Write("   ");
                Trace.Write(t.DisplayName);
                Trace.Write(": ");
                //Trace.Write(t..DisplayName);
                //Trace.Write(".");
                Trace.Write(nam);
                if (dic.ContainsKey(nam))
                {
                    Trace.WriteLine("  --- skip ---");
                    continue;
                }
                Trace.WriteLine("");
#else
                if (dic.ContainsKey(nam))
                    continue;
#endif
                res.Add(t);
            }
            return res;
        }
    }
}
