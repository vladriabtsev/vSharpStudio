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
using vSharpStudio.common;

namespace vSharpStudio.Views.Controls
{
    /// <summary>
    /// Interaction logic for CollectionFromCollection.xaml
    /// </summary>
    public partial class CollectionFromCollection : UserControl
    {
        public CollectionFromCollection()
        {
            InitializeComponent();
        }
        public object UpperRightContent
        {
            get { return (object)GetValue(UpperRightContentProperty); }
            set { SetValue(UpperRightContentProperty, value); }
        }
        public static readonly DependencyProperty UpperRightContentProperty =
            DependencyProperty.Register("UpperRightContent", typeof(object), typeof(CollectionFromCollection), new PropertyMetadata(null));


        public List<IProperty> ListAll
        {
            get { return (List<IProperty>)GetValue(ListAllProperty); }
            set { SetValue(ListAllProperty, value); }
        }
        public static readonly DependencyProperty ListAllProperty =
            DependencyProperty.Register("ListAll", typeof(List<IProperty>), typeof(CollectionFromCollection), new PropertyMetadata(null));

        public SortedObservableCollection<IProperty> ListSelected
        {
            get { return (SortedObservableCollection<IProperty>)GetValue(ListSelectedProperty); }
            set { SetValue(ListSelectedProperty, value); }
        }
        public static readonly DependencyProperty ListSelectedProperty =
            DependencyProperty.Register("ListSelected", typeof(SortedObservableCollection<IProperty>), typeof(CollectionFromCollection), new PropertyMetadata(null));


    }
}
