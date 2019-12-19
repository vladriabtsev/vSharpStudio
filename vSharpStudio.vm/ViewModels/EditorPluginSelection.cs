﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorPluginSelection : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public static ObservableCollectionExt<PluginGenerator> ListGenerators = new ObservableCollectionExt<PluginGenerator>();
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            // this.Config.GroupPlugins.ListPlugins
            ITreeConfigNode instance = (ITreeConfigNode)propertyItem.Instance;
            IConfig cnfg = instance.GetConfig();
            ComboBox cbx = new ComboBox();
            cbx.DisplayMemberPath = "Name";
            cbx.SelectedValuePath = "Guid";
            var _binding_lst = new Binding("IListPlugins"); // bind to the Value property of the PropertyItem
            _binding_lst.Source = cnfg.IGroupPlugins;
            _binding_lst.ValidatesOnExceptions = false;
            _binding_lst.ValidatesOnDataErrors = false;
            _binding_lst.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(cbx, ComboBox.ItemsSourceProperty, _binding_lst);
            var en = cnfg.IGroupPlugins.IListPlugins.GetEnumerator();
            if (en.MoveNext())
            {
                var plg = en.Current;
                if (!en.MoveNext())
                {
                    propertyItem.Value = plg.Guid;
                }
            }
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