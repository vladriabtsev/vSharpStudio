using System.Windows;
using System.Windows.Controls;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for Editors.xaml
    /// </summary>
    public partial class Editors : UserControl
    {
        public Editors()
        {
            this.InitializeComponent();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.cc.Height = e.NewSize.Height;
            this.cc.Width = e.NewSize.Width;
        }
    }
}
