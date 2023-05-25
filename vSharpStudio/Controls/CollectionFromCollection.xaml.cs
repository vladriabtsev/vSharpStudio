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

        #region Left
        public string? TitleLeft
        {
            get { return (string?)GetValue(TitleLeftProperty); }
            set { SetValue(TitleLeftProperty, value); }
        }
        public static readonly DependencyProperty TitleLeftProperty =
            DependencyProperty.Register("TitleLeft", typeof(string), typeof(CollectionFromCollection), new PropertyMetadata(null));
        public ObservableCollection<ISortingValue>? ListLeft
        {
            get { return (ObservableCollection<ISortingValue>?)GetValue(ListLeftProperty); }
            set { SetValue(ListLeftProperty, value); }
        }
        public static readonly DependencyProperty ListLeftProperty =
            DependencyProperty.Register("ListLeft", typeof(ObservableCollection<ISortingValue>), typeof(CollectionFromCollection), new PropertyMetadata(null, OnListLeftChanged));
        private static void OnListLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cfc = (CollectionFromCollection)d;
            Debug.Assert(cfc != null);
            if (e.OldValue != null)
            {
                var val = (ObservableCollection<ISortingValue>?)e.OldValue;
                if (val != null)
                    val.CollectionChanged -= cfc.Left_CollectionChanged;
            }
            if (e.NewValue != null)
            {
                var val = (ObservableCollection<ISortingValue>?)e.NewValue;
                if (val != null)
                    val.CollectionChanged += cfc.Left_CollectionChanged;
            }
            cfc.UpdateCommandStatuses();
        }
        private void Left_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.UpdateCommandStatuses();
        }
        public ISortingValue? SelectedLeft
        {
            get { return (ISortingValue?)GetValue(SelectedLeftProperty); }
            set { SetValue(SelectedLeftProperty, value); }
        }
        public static readonly DependencyProperty SelectedLeftProperty =
            DependencyProperty.Register("SelectedLeft", typeof(ISortingValue), typeof(CollectionFromCollection), new PropertyMetadata(null));
        /// <summary>
        /// Multi selection support
        /// </summary>
        public List<ISortingValue> ListSelectedLeft
        {
            get { return listSelectedLeft; }
        }
        private readonly List<ISortingValue> listSelectedLeft = new List<ISortingValue>();
        private void ListBoxLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var t in e.RemovedItems)
            {
                this.listSelectedLeft.Remove((ISortingValue)t);
            }
            foreach (var t in e.AddedItems)
            {
                this.listSelectedLeft.Add((ISortingValue)t);
            }
            if (this.listSelectedLeft.Count == 1)
                this.SelectedLeft = this.listSelectedLeft[0];
            else
                this.SelectedLeft = null;
            this.listSelectedRight.Clear();
            this.UpdateCommandStatuses();
        }
        #endregion Left

        #region Right
        public string? TitleRight
        {
            get { return (string?)GetValue(TitleRightProperty); }
            set { SetValue(TitleRightProperty, value); }
        }
        public static readonly DependencyProperty TitleRightProperty =
            DependencyProperty.Register("TitleRight", typeof(string), typeof(CollectionFromCollection), new PropertyMetadata(null));
        public object UpperRightContent
        {
            get { return (object)GetValue(UpperRightContentProperty); }
            set { SetValue(UpperRightContentProperty, value); }
        }
        public static readonly DependencyProperty UpperRightContentProperty =
            DependencyProperty.Register("UpperRightContent", typeof(object), typeof(CollectionFromCollection), new PropertyMetadata(null));
        public SortedObservableCollection<ISortingValue>? ListRight
        {
            get { return (SortedObservableCollection<ISortingValue>?)GetValue(ListRightProperty); }
            set { SetValue(ListRightProperty, value); }
        }
        public static readonly DependencyProperty ListRightProperty =
            DependencyProperty.Register("ListRight", typeof(SortedObservableCollection<ISortingValue>), typeof(CollectionFromCollection), new PropertyMetadata(null, OnListRightChanged));

        #region Collection Changed
        public static readonly RoutedEvent CollectionChangedRightEvent = EventManager.RegisterRoutedEvent(
            name: "CollectionChangedRight",
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedEventHandler),
            ownerType: typeof(CollectionFromCollection));
        public event RoutedEventHandler CollectionChangedRight
        {
            add { AddHandler(CollectionChangedRightEvent, value); }
            remove { RemoveHandler(CollectionChangedRightEvent, value); }
        }
        void RaiseCollectionChangedRightEvent()
        {
            RoutedEventArgs routedEventArgs = new(routedEvent: CollectionChangedRightEvent);
            RaiseEvent(routedEventArgs);
        }
        #endregion Collection Changed

        private static void OnListRightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cfc = (CollectionFromCollection)d;
            Debug.Assert(cfc != null);
            if (e.OldValue != null)
            {
                var val = (SortedObservableCollection<ISortingValue>?)e.OldValue;
                if (val != null)
                    val.CollectionChanged -= cfc.Right_CollectionChanged;
            }
            if (e.NewValue != null)
            {
                var val = (SortedObservableCollection<ISortingValue>?)e.NewValue;
                if (val != null)
                    val.CollectionChanged += cfc.Right_CollectionChanged;
            }
            cfc.UpdateCommandStatuses();
        }
        private void Right_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.UpdateCommandStatuses();
            this.RaiseCollectionChangedRightEvent();
        }
        public ISortingValue? SelectedRight
        {
            get { return (ISortingValue?)GetValue(SelectedRightProperty); }
            set { SetValue(SelectedRightProperty, value); }
        }
        public static readonly DependencyProperty SelectedRightProperty =
            DependencyProperty.Register("SelectedRight", typeof(ISortingValue), typeof(CollectionFromCollection), new PropertyMetadata(null));
        /// <summary>
        /// Multi selection support
        /// </summary>
        public List<ISortingValue> ListSelectedRight
        {
            get { return listSelectedRight; }
        }
        private readonly List<ISortingValue> listSelectedRight = new List<ISortingValue>();
        private void ListBoxRight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var t in e.RemovedItems)
            {
                this.listSelectedRight.Remove((ISortingValue)t);
            }
            foreach (var t in e.AddedItems)
            {
                this.listSelectedRight.Add((ISortingValue)t);
            }
            if (this.listSelectedRight.Count == 1)
                this.SelectedRight = this.listSelectedRight[0];
            else
                this.SelectedRight = null;
            this.listSelectedLeft.Clear();
            this.UpdateCommandStatuses();
        }
        #endregion Right

        #region Commands
        public vButtonVM BtnUp
        {
            get
            {
                return this._BtnUp ??= new vButtonVM(
                    () =>
                    {
                        Debug.Assert(this.ListRight != null);
                        var sel = this.SelectedRight;
                        this.ListRight.MoveUp(sel);
                        this.ListRight.Sort();
                        this.SelectedRight = sel;
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        if (this.SelectedRight == null)
                            return false;
                        if (this.ListRight != null)
                            return this.ListRight.CanUp(this.SelectedRight);
                        return false;
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
                        Debug.Assert(this.ListRight != null);
                        var sel = this.SelectedRight;
                        this.ListRight.MoveDown(sel);
                        this.ListRight.Sort();
                        this.SelectedRight = sel;
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        if (this.SelectedRight == null)
                            return false;
                        if (this.ListRight != null)
                            return this.ListRight.CanDown(this.SelectedRight);
                        return false;
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
                        Debug.Assert(this.ListRight != null);
                        Debug.Assert(this.listSelectedRight != null);
                        this.listSelectedRight.Clear();
                        foreach (var item in this.ListRight)
                        {
                            Debug.Assert(this.ListLeft != null);
                            this.ListLeft.Add(item);
                        }
                        this.ListRight.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        if (this.ListRight == null)
                            return false;
                        return this.ListRight.Count > 0;
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
                        Debug.Assert(this.listSelectedRight != null);
                        Debug.Assert(this.listSelectedLeft != null);
                        foreach (var item in this.listSelectedRight.ToList())
                        {
                            Debug.Assert(this.ListLeft != null);
                            Debug.Assert(this.ListRight != null);
                            this.ListLeft.Add(item);
                            this.ListRight.Remove(item);
                        }
                        this.listSelectedLeft.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        return this.listSelectedRight.Count > 0;
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
                        Debug.Assert(this.listSelectedLeft != null);
                        Debug.Assert(this.listSelectedRight != null);
                        foreach (var item in this.listSelectedLeft.ToList())
                        {
                            Debug.Assert(this.ListRight != null);
                            Debug.Assert(this.ListLeft != null);
                            this.ListRight.Add(item);
                            this.ListLeft.Remove(item);
                        }
                        this.listSelectedRight.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        return this.listSelectedLeft.Count > 0;
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
                        Debug.Assert(this.listSelectedLeft != null);
                        Debug.Assert(this.ListLeft != null);
                        this.listSelectedLeft.Clear();
                        foreach (var item in this.ListLeft)
                        {
                            Debug.Assert(this.ListRight != null);
                            this.ListRight.Add(item);
                        }
                        this.ListLeft.Clear();
                        this.UpdateCommandStatuses();
                    },
                    () =>
                    {
                        if (this.ListLeft == null)
                            return false;
                        return this.ListLeft.Count > 0;
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
        #endregion Commands
    }
}
