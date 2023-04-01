using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Remotion.Linq.Clauses;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public class EditorGenSettingsDialog : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        /// <summary>
        /// Action to show generator setting dialog. Action rarameters: model node and app generator Guid.
        /// </summary>
        public static Action<ITreeConfigNode, string>? GenSettingsDialogAction { get; set; } = null;
        private ITreeConfigNode? node = null;
        private string? guid = null;
        PropertyGridEditorTextBox? textBox;
        Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem? propItem;
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Debug.Assert(propertyItem != null);
            this.propItem = propertyItem;
            this.guid = ((IvPluginGeneratorNodeSettings)propertyItem.Value).AppProjectGeneratorGuid;
            this.node = ((IParent)propertyItem.Value).Parent;
            Grid grd = new Grid();
            var cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(1, GridUnitType.Star);
            var cd2 = new ColumnDefinition();
            cd2.Width = new GridLength(1, GridUnitType.Auto);
            grd.ColumnDefinitions.Add(cd1);
            grd.ColumnDefinitions.Add(cd2);

            textBox = new PropertyGridEditorTextBox();
            textBox.Watermark = "Open Settings in Dialog";
            //var _binding = new Binding("Value"); //bind to the Value property of the PropertyItem
            //_binding.Source = propertyItem;
            //_binding.ValidatesOnExceptions = true;
            //_binding.ValidatesOnDataErrors = true;
            //_binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            //BindingOperations.SetBinding(textBox, PropertyGridEditorTextBox.TextProperty, _binding);

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
            Debug.Assert(this.node != null && this.guid != null);
            EditorGenSettingsDialog.GenSettingsDialogAction?.Invoke(this.node, this.guid);
        }
    }
}
