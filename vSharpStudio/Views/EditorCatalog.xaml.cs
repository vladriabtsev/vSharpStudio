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
    /// Interaction logic for EditorCatalog.xaml
    /// </summary>
    public partial class EditorCatalog : UserControl
    {
        public EditorCatalog()
        {
            InitializeComponent();
        }

        //private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    this.colFolder.DataContext = this.DataContext;
        //    this.colItem.DataContext = this.DataContext;
        //}
    }
}
