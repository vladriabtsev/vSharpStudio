using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using vSharpStudio.ViewModels;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for ValidationListForSelectedNode.xaml
    /// </summary>
    public partial class ValidationListForSelectedNode : UserControl
    {
        private MainPageVM vm = null;

        public ValidationListForSelectedNode()
        {
            this.InitializeComponent();
            this.vm = (MainPageVM)this.DataContext;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MainPageVM).validationListForSelectedNode = this;
        }
    }
}
