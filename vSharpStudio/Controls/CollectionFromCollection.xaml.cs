using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public ObservableCollection<ISortingValue>? ListAll
        {
            get { return (ObservableCollection<ISortingValue>?)GetValue(ListAllProperty); }
            set { SetValue(ListAllProperty, value); }
        }
        public static readonly DependencyProperty ListAllProperty =
            DependencyProperty.Register("ListAll", typeof(ObservableCollection<ISortingValue>), typeof(CollectionFromCollection), new PropertyMetadata(null));
        public ISortingValue? SelectedFrom
        {
            get { return (ISortingValue?)GetValue(SelectedFromProperty); }
            set { SetValue(SelectedFromProperty, value); }
        }
        public static readonly DependencyProperty SelectedFromProperty =
            DependencyProperty.Register("SelectedFrom", typeof(ISortingValue), typeof(CollectionFromCollection), new PropertyMetadata(null));
        public ISortingValue? SelectedTo
        {
            get { return (ISortingValue?)GetValue(SelectedToProperty); }
            set { SetValue(SelectedToProperty, value); }
        }
        public static readonly DependencyProperty SelectedToProperty =
            DependencyProperty.Register("SelectedTo", typeof(ISortingValue), typeof(CollectionFromCollection), new PropertyMetadata(null));
        public SortedObservableCollection<ISortingValue>? ListSelected
        {
            get { return (SortedObservableCollection<ISortingValue>?)GetValue(ListSelectedProperty); }
            set { SetValue(ListSelectedProperty, value); }
        }
        public static readonly DependencyProperty ListSelectedProperty =
            DependencyProperty.Register("ListSelected", typeof(SortedObservableCollection<ISortingValue>), typeof(CollectionFromCollection), new PropertyMetadata(null));
        /// <summary>
        /// Multi selection support
        /// </summary>
        public List<ISortingValue> ListSelectedFrom
        {
            get { return listSelectedFrom; }
        }
        private readonly List<ISortingValue> listSelectedFrom = new List<ISortingValue>();
        /// <summary>
        /// Multi selection support
        /// </summary>
        public List<ISortingValue> ListSelectedTo
        {
            get { return listSelectedTo; }
        }
        private readonly List<ISortingValue> listSelectedTo = new List<ISortingValue>();
        private void ListBoxFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var t in e.RemovedItems)
            {
                this.listSelectedFrom.Remove((ISortingValue)t);
            }
            foreach (var t in e.AddedItems)
            {
                this.listSelectedFrom.Add((ISortingValue)t);
            }
            if (this.listSelectedFrom.Count == 1)
                this.SelectedFrom = this.listSelectedFrom[0];
            else
                this.SelectedFrom = null;
            this.listSelectedTo.Clear();
            this.UpdateCommandStatuses();
        }
        private void ListBoxTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var t in e.RemovedItems)
            {
                this.listSelectedTo.Remove((ISortingValue)t);
            }
            foreach (var t in e.AddedItems)
            {
                this.listSelectedTo.Add((ISortingValue)t);
            }
            if (this.listSelectedTo.Count == 1)
                this.SelectedTo = this.listSelectedTo[0];
            else
                this.SelectedTo = null;
            this.listSelectedFrom.Clear();
            this.UpdateCommandStatuses();
        }
        public vButtonVM BtnUp
        {
            get
            {
                return this._BtnUp ??= new vButtonVM(
                    () =>
                    {
                        Debug.Assert(this.ListSelected != null);
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
                        Debug.Assert(this.ListSelected != null);
                        return this.ListSelected.CanUp(this.SelectedTo);
                    });
            }
        }
        private vButtonVM? _BtnUp;
        public vButtonVM BtnDown
        {
            get
            {
                return this._BtnDown ??= new vButtonVM(
                    () =>
                    {
                        Debug.Assert(this.ListSelected != null);
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
                        Debug.Assert(this.ListSelected != null);
                        return this.ListSelected.CanDown(this.SelectedTo);
                    });
            }
        }
        private vButtonVM? _BtnDown;
        public vButtonVM BtnLeftAll
        {
            get
            {
                return this._BtnLeftAll ??= new vButtonVM(
                    () =>
                    {
                        Debug.Assert(this.ListSelected != null);
                        Debug.Assert(this.listSelectedTo != null);
                        this.listSelectedTo.Clear();
                        foreach (var item in this.ListSelected)
                        {
                            Debug.Assert(this.ListAll != null);
                            this.ListAll.Add(item);
                        }
                        this.ListSelected.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        if (this.ListSelected == null)
                            return false;
                        return this.ListSelected.Count > 0;
                    });
            }
        }
        private vButtonVM? _BtnLeftAll;
        public vButtonVM BtnLeft
        {
            get
            {
                return this._BtnLeft ??= new vButtonVM(
                    () =>
                    {
                        Debug.Assert(this.listSelectedTo != null);
                        Debug.Assert(this.listSelectedFrom != null);
                        foreach (var item in this.listSelectedTo.ToList())
                        {
                            Debug.Assert(this.ListAll != null);
                            Debug.Assert(this.ListSelected != null);
                            this.ListAll.Add(item);
                            this.ListSelected.Remove(item);
                        }
                        this.listSelectedFrom.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        return this.listSelectedTo.Count > 0;
                    });
            }
        }
        private vButtonVM? _BtnLeft;
        public vButtonVM BtnRight
        {
            get
            {
                return this._BtnRight ??= new vButtonVM(
                    () =>
                    {
                        Debug.Assert(this.listSelectedFrom != null);
                        Debug.Assert(this.listSelectedTo != null);
                        foreach (var item in this.listSelectedFrom.ToList())
                        {
                            Debug.Assert(this.ListSelected != null);
                            Debug.Assert(this.ListAll != null);
                            this.ListSelected.Add(item);
                            this.ListAll.Remove(item);
                        }
                        this.listSelectedTo.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        return this.listSelectedFrom.Count > 0;
                    });
            }
        }
        private vButtonVM? _BtnRight;
        public vButtonVM BtnRightAll
        {
            get
            {
                return this._BtnRightAll ??= new vButtonVM(
                    () =>
                    {
                        Debug.Assert(this.listSelectedFrom != null);
                        Debug.Assert(this.ListAll != null);
                        this.listSelectedFrom.Clear();
                        foreach (var item in this.ListAll)
                        {
                            Debug.Assert(this.ListSelected != null);
                            this.ListSelected.Add(item);
                        }
                        this.ListAll.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        if (this.ListAll == null)
                            return false;
                        return this.ListAll.Count > 0;
                    });
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
