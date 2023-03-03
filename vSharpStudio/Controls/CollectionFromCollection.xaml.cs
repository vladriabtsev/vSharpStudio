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
        //public bool IsEnableFrom
        //{
        //    get { return (bool)GetValue(IsEnableFromProperty); }
        //    set { SetValue(IsEnableFromProperty, value); }
        //}
        //public static readonly DependencyProperty IsEnableFromProperty =
        //    DependencyProperty.Register("IsEnableFrom", typeof(bool), typeof(CollectionFromCollection), new PropertyMetadata(false));

        public IProperty SelectedFrom
        {
            get { return (IProperty)GetValue(SelectedFromProperty); }
            set { SetValue(SelectedFromProperty, value); }
        }
        public static readonly DependencyProperty SelectedFromProperty =
            DependencyProperty.Register("SelectedFrom", typeof(IProperty), typeof(CollectionFromCollection), new PropertyMetadata(null));
        public IProperty SelectedTo
        {
            get { return (IProperty)GetValue(SelectedToProperty); }
            set { SetValue(SelectedToProperty, value); }
        }
        public static readonly DependencyProperty SelectedToProperty =
            DependencyProperty.Register("SelectedTo", typeof(IProperty), typeof(CollectionFromCollection), new PropertyMetadata(null));

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
            this.UpdateCommandStatuses();
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
            this.UpdateCommandStatuses();
        }
        public vButtonVM BtnUp
        {
            get
            {
                return this._BtnUp ?? (this._BtnUp = new vButtonVM(
                    () =>
                    {
                        var sel = this.SelectedTo;
                        this.ListSelected.MoveUp(sel);
                        this.ListSelected.Sort();
                        this.SelectedTo = sel;
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        if (this.SelectedTo == null)
                            return false;
                        return this.ListSelected.CanUp(this.SelectedTo);
                    }));
            }
        }
        private vButtonVM? _BtnUp;
        public vButtonVM BtnDown
        {
            get
            {
                return this._BtnDown ?? (this._BtnDown = new vButtonVM(
                    () =>
                    {
                        var sel = this.SelectedTo;
                        this.ListSelected.MoveDown(sel);
                        this.ListSelected.Sort();
                        this.SelectedTo = sel;
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        if (this.SelectedTo == null)
                            return false;
                        return this.ListSelected.CanDown(this.SelectedTo);
                    }));
            }
        }
        private vButtonVM? _BtnDown;
        public vButtonVM BtnLeftAll
        {
            get
            {
                return this._BtnLeftAll ?? (this._BtnLeftAll = new vButtonVM(
                    () =>
                    {
                        this.listSelectedTo.Clear();
                        foreach (var item in this.ListSelected)
                        {
                            this.ListAll.Add(item);
                        }
                        this.ListSelected.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        return this.ListSelected.Count > 0;
                    }));
            }
        }
        private vButtonVM? _BtnLeftAll;
        public vButtonVM BtnLeft
        {
            get
            {
                return this._BtnLeft ?? (this._BtnLeft = new vButtonVM(
                    () =>
                    {
                        foreach (var item in this.listSelectedTo.ToList())
                        {
                            this.ListAll.Add(item);
                            this.ListSelected.Remove(item);
                        }
                        this.listSelectedFrom.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        return this.listSelectedTo.Count > 0;
                    }));
            }
        }
        private vButtonVM? _BtnLeft;
        public vButtonVM BtnRight
        {
            get
            {
                return this._BtnRight ?? (this._BtnRight = new vButtonVM(
                    () =>
                    {
                        foreach (var item in this.listSelectedFrom.ToList())
                        {
                            this.ListSelected.Add(item);
                            this.ListAll.Remove(item);
                        }
                        this.listSelectedTo.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        return this.listSelectedFrom.Count > 0;
                    }));
            }
        }
        private vButtonVM? _BtnRight;
        public vButtonVM BtnRightAll
        {
            get
            {
                return this._BtnRightAll ?? (this._BtnRightAll = new vButtonVM(
                    () =>
                    {
                        this.listSelectedFrom.Clear();
                        foreach (var item in this.ListAll)
                        {
                            this.ListSelected.Add(item);
                        }
                        this.ListAll.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        return this.ListAll.Count > 0;
                    }));
            }
        }
        private vButtonVM? _BtnRightAll;
        private void UpdateCommandStatuses()
        {
            BtnUp.Command.NotifyCanExecuteChanged();
            BtnDown.Command.NotifyCanExecuteChanged();
            BtnLeftAll.Command.NotifyCanExecuteChanged();
            BtnLeft.Command.NotifyCanExecuteChanged();
            BtnRight.Command.NotifyCanExecuteChanged();
            BtnRightAll.Command.NotifyCanExecuteChanged();
        }
    }
}
