using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

// https://www.codeproject.com/Tips/408241/Checkbox-Inside-ListBox-to-Select-Multiple-Items-i
namespace vSharpStudio.wpf.Controls
{
    public class ListCheckBox : ListBox
    {
        private static Style style = null;
        public ListCheckBox()
        {
            if (style == null)
            {
                string template = @"
<Style xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" TargetType=""ListBoxItem"">
    <Setter Property=""OverridesDefaultStyle"" Value=""true""/>
    <Setter Property=""SnapsToDevicePixels"" Value=""true""/>
    <Setter Property=""Template"">
        <Setter.Value>
            <ControlTemplate TargetType=""ListBoxItem"">
                <Border Background=""Transparent"" Margin=""{ TemplateBinding Padding}"">
                    <CheckBox Content=""{ Binding Name}""
                        IsChecked=""{Binding Path=IsSelected, Mode=TwoWay}""/>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>";
                StringReader str_reader = new StringReader(template);
                XmlReader xml_reader = XmlReader.Create(str_reader);
                style = (Style)XamlReader.Load(xml_reader);
            }
            this.Resources.Add(typeof(ListBoxItem), style);
        }
    }
}
