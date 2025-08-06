using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Diagnostics;

namespace vSharpStudio.wpf.Controls
{
    public class IconContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate IconTemplate { get; set; }
        public override DataTemplate SelectTemplate(object value, DependencyObject container)
        {
            Guard.IsAssignableToType<string>(value);
            DataTemplate? res = null;
            if (value is string s)
            {
                var obj = Application.Current.TryFindResource(s);
                if (obj == null)
                    ThrowHelper.ThrowArgumentException($"Resource '{s}' is not found", nameof(value));
                res = obj as DataTemplate;
                if (res == null)
                    ThrowHelper.ThrowArgumentException($"DataTemplate resource type is expected for resource '{s}'", nameof(value));
                return res;
            }
            ThrowHelper.ThrowArgumentException($"Expected string as parameter", nameof(value));
            return res;
        }
    }
}
