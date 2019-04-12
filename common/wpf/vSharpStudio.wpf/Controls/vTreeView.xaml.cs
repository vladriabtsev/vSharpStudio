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
using ViewModelBase;

namespace vSharpStudio.wpf.Controls
{
    /// <summary>
    /// Interaction logic for vTreeView.xaml
    /// </summary>
    public partial class vTreeView : UserControl
    {
        public vTreeView()
        {
            InitializeComponent();
        }
        //public ITreeNode SelectedItem
        //{
        //    get { return (ITreeNode)GetValue(SelectedItemProperty); }
        //    set { SetValue(SelectedItemProperty, value); }
        //}
        //public static readonly DependencyProperty SelectedItemProperty =
        //    DependencyProperty.Register("SelectedItem", typeof(ITreeNode), typeof(vTreeView), new PropertyMetadata(null));


    }
}
