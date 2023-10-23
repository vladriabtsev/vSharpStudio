using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using static System.Windows.Forms.AxHost;
using static Xceed.Wpf.Toolkit.Calculator;

namespace vSharpStudio.vm.ViewModels
{
    /// <summary>
    /// Expected object with second property name equals first property name plus 'Text'
    /// </summary>
    public class EditorPropertyGridDialog : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        /// <summary>
        /// Action to show generator setting dialog. Action rarameters: model node and app generator Guid.
        /// </summary>
        public static Action<PropertyGridDialogVm>? PropertyGridDialogAction { get; set; } = null;
        private PropertyGridDialogVm? val = null;
        //private string? guid = null;
        PropertyGridEditorTextBox? textBox;
        Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem? propItem;
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Debug.Assert(propertyItem != null);
            this.propItem = propertyItem;
            var objName = ((IName)propertyItem.Instance).Name; // name of object
            var displName = propertyItem.DisplayName; // label of property
            this.val = new PropertyGridDialogVm();
            this.val.Caption = $"{objName}->{displName}:";
            this.val.Value = this.propItem.Value;
            Grid grd = new Grid();
            var cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(1, GridUnitType.Star);
            var cd2 = new ColumnDefinition();
            cd2.Width = new GridLength(1, GridUnitType.Auto);
            grd.ColumnDefinitions.Add(cd1);
            grd.ColumnDefinitions.Add(cd2);

            textBox = new PropertyGridEditorTextBox();
            textBox.IsEnabled = false;
            //textBox.Watermark = propertyItem.Value.ToString();
            var _binding = new Binding($"Instance.{propertyItem.PropertyName}");
            _binding.Mode = BindingMode.OneWay;
            _binding.Source = propertyItem;
            _binding.ValidatesOnExceptions = true;
            _binding.ValidatesOnDataErrors = true;
            BindingOperations.SetBinding(textBox, PropertyGridEditorTextBox.WatermarkProperty, _binding);

            Button b = new Button();
            b.Content = "...";
            b.Click += B_Click;

            Grid.SetColumn(textBox, 0);
            Grid.SetColumn(b, 1);

            grd.Children.Add(textBox);
            grd.Children.Add(b);
            return grd;
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            Debug.Assert(this.val != null);
            EditorPropertyGridDialog.PropertyGridDialogAction?.Invoke(this.val);
        }
    }
    public partial class PropertyGridDialogVm : ObservableObject
    {
        [ObservableProperty]
        private string? caption;
        [ObservableProperty]
        private object? value;
    }
}
