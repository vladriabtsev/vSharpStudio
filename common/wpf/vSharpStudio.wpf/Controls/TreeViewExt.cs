using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace vSharpStudio.wpf.Controls
{
    //public class TreeViewExt : TreeView
    //{
    //    public TreeViewExt() : base()
    //    {
    //        this.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(___ICH);
    //    }
    //    void ___ICH(object sender, RoutedPropertyChangedEventArgs<object> e)
    //    {
    //        if (SelectedItem != null)
    //        {
    //            SetValue(SelectedItem_Property, SelectedItem);
    //        }
    //    }
    //    public object SelectedItemBindable
    //    {
    //        get { return (object)GetValue(SelectedItem_Property); }
    //        set { SetValue(SelectedItem_Property, value); }
    //    }
    //    public static readonly DependencyProperty SelectedItem_Property = 
    //        DependencyProperty.Register("SelectedItemBindable", typeof(object), typeof(TreeViewExt), new UIPropertyMetadata(null));
    //}
    public class TreeViewEx : TreeView
    {
        public TreeViewEx()
        {
            this.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(TreeViewEx_SelectedItemChanged);
        }

        void TreeViewEx_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.SelectedItem = e.NewValue;
        }

        #region SelectedItem

        /// <summary>
        /// Gets or Sets the SelectedItem possible Value of the TreeViewItem object.
        /// </summary>
        public new object SelectedItem
        {
            get { return this.GetValue(TreeViewEx.SelectedItemProperty); }
            set { this.SetValue(TreeViewEx.SelectedItemProperty, value); }
        }
        public new static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(TreeViewEx),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SelectedItemProperty_Changed));
        static void SelectedItemProperty_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TreeViewEx targetObject = dependencyObject as TreeViewEx;
            if (targetObject != null)
            {
                TreeViewItem tvi = targetObject.FindItemNode(targetObject.SelectedItem) as TreeViewItem;
                if (tvi != null)
                {
                    tvi.IsSelected = true;
//                    targetObject.SubItems = tvi.Items;
                }
                //else
                //{
                //    targetObject.SubItems = null;
                //}
            }
        }
        //public ItemCollection SubItems
        //{
        //    get { return (ItemCollection)GetValue(ItemsPropertyProperty); }
        //    set { SetValue(ItemsPropertyProperty, value); }
        //}
        //public static readonly DependencyProperty ItemsPropertyProperty =
        //    DependencyProperty.Register("ItemsProperty", typeof(ItemCollection), typeof(TreeViewEx), new PropertyMetadata(null));

        #endregion SelectedItem   

        public TreeViewItem FindItemNode(object item)
        {
            TreeViewItem node = null;
            foreach (object data in this.Items)
            {
                node = this.ItemContainerGenerator.ContainerFromItem(data) as TreeViewItem;
                if (node != null)
                {
                    if (data == item)
                        break;
                    node = FindItemNodeInChildren(node, item);
                    if (node != null)
                        break;
                }
            }
            return node;
        }

        protected TreeViewItem FindItemNodeInChildren(TreeViewItem parent, object item)
        {
            TreeViewItem node = null;
            bool isExpanded = parent.IsExpanded;
            if (!isExpanded) //Can't find child container unless the parent node is Expanded once
            {
                parent.IsExpanded = true;
                parent.UpdateLayout();
            }
            foreach (object data in parent.Items)
            {
                node = parent.ItemContainerGenerator.ContainerFromItem(data) as TreeViewItem;
                if (data == item && node != null)
                    break;
                node = FindItemNodeInChildren(node, item);
                if (node != null)
                    break;
            }
            if (node == null && parent.IsExpanded != isExpanded)
                parent.IsExpanded = isExpanded;
            if (node != null)
                parent.IsExpanded = true;
            return node;
        }
    }
}
