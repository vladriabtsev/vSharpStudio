﻿using System;
using System.Collections;
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
using System.Xml.Linq;
using Microsoft.Win32;
using Serilog.Parsing;
using vSharpStudio.common;
using vSharpStudio.vm.Migration;
using vSharpStudio.vm.ViewModels;
using vSharpStudio.wpf.Controls;
using static System.Resources.ResXFileRef;

namespace vSharpStudio.Views
{
    /// <summary>
    /// Interaction logic for EditorRoles.xaml
    /// </summary>
    public partial class EditorRoles : UserControl
    {
        public EditorRoles()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext == null)
                return;
            var gridView = (GridView)_tree.View;
            var roles = (GroupListRoles)this.DataContext;
            foreach (var t in roles.ListRoles)
            {
                var gvc = new GridViewColumn();
                gvc.DisplayMemberBinding = new Binding("Kuku");
                gvc.Header = t.Name;
                gvc.Width = 100;

                // databinding & converter
                var frameworkElementFactory = new FrameworkElementFactory(typeof(TextBlock));
                var dataTemplate = new DataTemplate();
                dataTemplate.VisualTree = frameworkElementFactory;
                gvc.CellTemplate = dataTemplate;
                //var binding = new Binding(assignment.Id.ToString() + key);
                //binding.Converter = converter;

                gridView.Columns.Add(gvc);
            }
            _tree.Model = new ModelNodeRoles(roles.ParentGroupListCommon.ParentModel);
        }
    }
    //public class ModelNodeForeRoles
    //{
    //    public ModelNodeForeRoles(ITreeConfigNode node)
    //    {
    //        this.Node = node;
    //    }
    //    public ITreeConfigNode Node { get; private set; }
    //    public object? Kuku { get; private set; }
    //}
    public class ModelNodeRoles : ITreeModel
    {
        Model? model;
        public ITreeConfigNode Node { get; private set; }
        public object? Kuku { get; private set; }
        public ModelNodeRoles(Model model)
        {
            this.model = model;
            this.Node = model;
        }
        public ModelNodeRoles(ITreeConfigNode node)
        {
            this.Node = node;
        }
        public IEnumerable GetChildren(object parent)
        {
            ITreeConfigNode node = parent == null ? this.Node : ((ModelNodeRoles)parent).Node;
            var res = new List<object>();
            foreach (var t in node.GetListChildren())
            {
                var tt = (ITreeConfigNode)t;
                if (parent == null && (tt.Name == "Common" || tt.Name == "Enumerations"))
                    continue;
                res.Add(new ModelNodeRoles(tt));
            }
            return res;
        }
        public bool HasChildren(object parent)
        {
            var p = (ModelNodeRoles)parent;
            return p.Node.GetListChildren().Count > 0;
        }
    }
}
