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

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for EditorRoleCell.xaml
    /// </summary>
    public partial class EditorRoleCell : UserControl
    {
        public EditorRoleCell()
        {
            InitializeComponent();
            this.ddbEdit.Visibility = Visibility.Collapsed;
            this.ddbPrint.Visibility = Visibility.Collapsed;
        }
        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            this.ddbEdit.Visibility = Visibility.Visible;
        }
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            this.ddbEdit.Visibility = Visibility.Collapsed;
        }
        private void Grid_MouseEnterPrint(object sender, MouseEventArgs e)
        {
            this.ddbPrint.Visibility = Visibility.Visible;
        }
        private void Grid_MouseLeavePrint(object sender, MouseEventArgs e)
        {
            this.ddbPrint.Visibility = Visibility.Collapsed;
        }
    }
}
