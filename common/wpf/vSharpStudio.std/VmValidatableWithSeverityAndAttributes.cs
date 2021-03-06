﻿using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ViewModelBase
{
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
        public Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionCollection PropertyDefinitions { get { return this._PropertyDefinitions; } set { SetProperty(ref _PropertyDefinitions, value); } }
        private Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinitionCollection _PropertyDefinitions;
        protected void HidePropertiesForXceedPropertyGrid(string[] lstExclude=null)
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
                if (dic.ContainsKey(t.Name))
                    continue;
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
