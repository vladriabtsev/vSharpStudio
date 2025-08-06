using System.Windows;
using System.Windows.Controls;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for GenSettingsPropertyGrid.xaml
    /// </summary>
    public partial class GenSettingsPropertyGrid : UserControl
    {
        public GenSettingsPropertyGrid()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext != null)
            {
                this.Visibility = Visibility.Visible;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
