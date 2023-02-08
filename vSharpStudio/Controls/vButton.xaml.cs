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

namespace vSharpStudio.Controls
{
    /// <summary>
    /// Interaction logic for vButton.xaml
    /// </summary>
    public partial class vButton : UserControl
    {
        public vButton()
        {
            InitializeComponent();
        }

        //public static readonly DependencyProperty DateProperty =
        //    DependencyProperty.Register("Date", typeof(DateTime), typeof(DaiesContainer),
        //    new UIPropertyMetadata(DateTime.Now));
        //public DateTime Date
        //{
        //    get { return (DateTime)GetValue(DateProperty); }
        //    set { SetValue(DateProperty, value); }
        //}

        public static readonly DependencyProperty IconControlTemplateProperty =
            DependencyProperty.Register("IconControlTemplate", typeof(ControlTemplate), typeof(vButton),
            new UIPropertyMetadata(new ControlTemplate()));
        public ControlTemplate IconControlTemplate
        {
            get { return (ControlTemplate)GetValue(IconControlTemplateProperty); }
            set { SetValue(IconControlTemplateProperty, value); }
        }
    }
}
