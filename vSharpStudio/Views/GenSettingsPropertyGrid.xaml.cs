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
using CommunityToolkit.Mvvm.ComponentModel;

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
                this.Visibility= Visibility.Collapsed;
            }
        }
    }
}
