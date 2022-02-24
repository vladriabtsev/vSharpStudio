using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace vSharpStudio.Controls
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




        public ObservableCollection<IProperty> ListAll
        {
            get { return (ObservableCollection<IProperty>)GetValue(ListAllProperty); }
            set { SetValue(ListAllProperty, value); }
        }
        public static readonly DependencyProperty ListAllProperty =
            DependencyProperty.Register("ListAll", typeof(ObservableCollection<IProperty>), typeof(CollectionFromCollection), new PropertyMetadata(null));
        public bool IsEnableFrom
        {
            get { return (bool)GetValue(IsEnableFromProperty); }
            set { SetValue(IsEnableFromProperty, value); }
        }
        public static readonly DependencyProperty IsEnableFromProperty =
            DependencyProperty.Register("IsEnableFrom", typeof(bool), typeof(CollectionFromCollection), new PropertyMetadata(false));



        public SortedObservableCollection<IProperty> ListSelected
        {
            get { return (SortedObservableCollection<IProperty>)GetValue(ListSelectedProperty); }
            set { SetValue(ListSelectedProperty, value); }
        }
        public static readonly DependencyProperty ListSelectedProperty =
            DependencyProperty.Register("ListSelected", typeof(SortedObservableCollection<IProperty>), typeof(CollectionFromCollection), new PropertyMetadata(null));
        public List<IProperty> ListSelectedFrom
        {
            get { return listSelectedFrom; }
        }
        private readonly List<IProperty> listSelectedFrom = new List<IProperty>();
        public List<IProperty> ListSelectedTo
        {
            get { return listSelectedTo; }
        }
        private readonly List<IProperty> listSelectedTo = new List<IProperty>();
        public bool IsEnableTo
        {
            get { return (bool)GetValue(IsEnableToProperty); }
            set { SetValue(IsEnableToProperty, value); }
        }
        public static readonly DependencyProperty IsEnableToProperty =
            DependencyProperty.Register("IsEnableTo", typeof(bool), typeof(CollectionFromCollection), new PropertyMetadata(false));



        public bool IsCanUp
        {
            get { return (bool)GetValue(IsCanUpProperty); }
            set { SetValue(IsCanUpProperty, value); }
        }
        public static readonly DependencyProperty IsCanUpProperty =
            DependencyProperty.Register("IsCanUp", typeof(bool), typeof(CollectionFromCollection), new PropertyMetadata(false));
        private void Button_Up_Click(object sender, RoutedEventArgs e)
        {
            this.ListSelected.MoveUp(this.listSelectedTo[0]);
            this.ListSelected.Sort();
            CheckCanUpDown();
        }
        private void CheckCanUpDown()
        {
            if (this.listSelectedTo.Count == 1)
            {
                if (this.ListSelected.CanUp(this.listSelectedTo[0]))
                    IsCanUp = true;
                else
                    IsCanUp = false;
                if (this.ListSelected.CanDown(this.listSelectedTo[0]))
                    IsCanDown = true;
                else
                    IsCanDown = false;
            }
            else
            {
                IsCanUp = false;
                IsCanDown = false;
            }
        }
        public bool IsCanDown
        {
            get { return (bool)GetValue(IsCanDownProperty); }
            set { SetValue(IsCanDownProperty, value); }
        }
        public static readonly DependencyProperty IsCanDownProperty =
            DependencyProperty.Register("IsCanDown", typeof(bool), typeof(CollectionFromCollection), new PropertyMetadata(false));
        private void Button_Down_Click(object sender, RoutedEventArgs e)
        {
            this.ListSelected.MoveDown(this.listSelectedTo[0]);
            this.ListSelected.Sort();
            CheckCanUpDown();
        }
        private void Button_AllLeft_Click(object sender, RoutedEventArgs e)
        {
            this.listSelectedTo.Clear();
            foreach (var item in this.ListSelected)
            {
                this.ListAll.Add(item);
            }
            this.ListSelected.Clear();
        }
        private void Button_Left_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.listSelectedTo.ToList())
            {
                this.ListAll.Add(item);
                this.ListSelected.Remove(item);
            }
            this.listSelectedFrom.Clear();
        }
        private void Button_Right_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.listSelectedFrom.ToList())
            {
                this.ListSelected.Add(item);
                this.ListAll.Remove(item);
            }
            this.listSelectedTo.Clear();
        }
        private void Button_AllRight_Click(object sender, RoutedEventArgs e)
        {
            this.listSelectedFrom.Clear();
            foreach (var item in this.ListAll)
            {
                this.ListSelected.Add(item);
            }
            this.ListAll.Clear();
        }

        private void ListBoxFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var t in e.RemovedItems)
            {
                this.listSelectedFrom.Remove((IProperty)t);
            }
            foreach (var t in e.AddedItems)
            {
                this.listSelectedFrom.Add((IProperty)t);
            }
            if (this.listSelectedFrom.Count > 0)
            {
                IsEnableFrom = true;
            }
            else
            {
                IsEnableFrom = false;
            }
        }
        private void ListBoxTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var t in e.RemovedItems)
            {
                this.listSelectedTo.Remove((IProperty)t);
            }
            foreach (var t in e.AddedItems)
            {
                this.listSelectedTo.Add((IProperty)t);
            }
            CheckCanUpDown();
            if (this.listSelectedTo.Count > 0)
            {
                IsEnableTo = true;
            }
            else
            {
                IsEnableTo = false;
            }
        }
    }
}
