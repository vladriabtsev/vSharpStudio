using System.Windows;
using System.Windows.Controls;
using vSharpStudio.ViewModels;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for ValidationListForSelectedNode.xaml
    /// </summary>
    public partial class ValidationListForSelectedNode : UserControl
    {
        private readonly MainPageVM? vm = null;

        public ValidationListForSelectedNode()
        {
            this.InitializeComponent();
            this.vm = (MainPageVM)this.DataContext;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null && this.DataContext is MainPageVM mpvm)
                mpvm.validationListForSelectedNode = this;
        }
    }
}
