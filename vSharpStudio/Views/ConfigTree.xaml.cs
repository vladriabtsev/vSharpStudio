﻿using System;
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
using vSharpStudio.ViewModels;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for ConfigTree.xaml
    /// </summary>
    public partial class ConfigTree : UserControl
    {
        public ConfigTree()
        {
            InitializeComponent();
        }

        //private void ConfigTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    (DataContext as MainPageVM).OnSelectedItemChanged(e.OldValue, e.NewValue);
        //}
    }
}
