using System.Windows.Controls;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for ConfigTree.xaml
    /// </summary>
    public partial class ConfigTree : UserControl
    {
        public ConfigTree()
        {
            this.InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }
        }
    }
}
