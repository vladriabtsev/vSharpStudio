﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorReferenceProjectSelection : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Debug.Assert(propertyItem != null);
            // this.Config.GroupPlugins.ListPlugins
            var prj = (AppProject)propertyItem.Instance;
            var gr = prj.ParentAppSolution.ParentGroupListAppSolutions;
            var lst = new List<AppProject>();
            foreach(var t in gr.ListAppSolutions)
            {
                foreach(var tt in t.ListAppProjects)
                {
                    if (prj.Guid == tt.Guid)
                        continue;
                    lst.Add(tt);
                }
            }
            //if (instance.ListGenerators.Count == 1)
            //    propertyItem.Value = instance.ListGenerators[0].Guid;
            ComboBox cbx = new ComboBox();
            cbx.DisplayMemberPath = "Name";
            cbx.SelectedValuePath = "Guid";
            var _binding_lst = new Binding(); // bind to the Value property of the PropertyItem
            _binding_lst.Source = lst;
            _binding_lst.ValidatesOnExceptions = false;
            _binding_lst.ValidatesOnDataErrors = false;
            _binding_lst.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(cbx, ComboBox.ItemsSourceProperty, _binding_lst);

            var _binding = new Binding("Value"); // bind to the Value property of the PropertyItem
            _binding.Source = propertyItem;
            _binding.ValidatesOnExceptions = true;
            _binding.ValidatesOnDataErrors = true;
            _binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(cbx, ComboBox.SelectedValueProperty, _binding);
            return cbx;
        }
    }
}
