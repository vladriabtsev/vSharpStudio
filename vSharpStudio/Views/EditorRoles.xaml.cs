using System;
using System.Collections;
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
using Microsoft.Win32;
using vSharpStudio.common;
using vSharpStudio.vm.ViewModels;
using vSharpStudio.wpf.Controls;

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
            var gridView = (GridView)_tree.View;
            var roles = (GroupListRoles)this.DataContext;
            foreach (var t in roles.ListRoles)
            {
                var gvc = new GridViewColumn();
                gvc.DisplayMemberBinding = new Binding("FirstName");
                gvc.Header = t.Name;
                gvc.Width = 100;
                gridView.Columns.Add(gvc);
            }
            _tree.Model = new ModelNodeRoles(roles.ParentGroupListCommon.ParentModel);
        }
    }
    public class ModelNodeForeRoles
    {
        public ModelNodeForeRoles(ITreeConfigNode node)
        {
            this.Name = node.Name;
        }
        public string Name { get; set; }
    }
    public class ModelNodeRoles : ITreeModel
    {
        Model model;
        public ModelNodeRoles(Model model)
        {
            this.model = model;
        }
        public IEnumerable<object> GetChildren(object parent)
        {
            ITreeConfigNode node = null;
            if (parent == null)
                node = this.model;
            else
                node = (ITreeConfigNode)parent;
            var res = new List<object>();
            foreach (var t in node.GetListChildren())
            {
                res.Add(new ModelNodeForeRoles((ITreeConfigNode)t));
            }
            return res;
        }
        //    else if (key != null)
        //    {
        //        foreach (var name in key.GetSubKeyNames())
        //        {
        //            RegistryKey subKey = null;
        //            try
        //            {
        //                subKey = key.OpenSubKey(name);
        //            }
        //            catch
        //            {
        //            }
        //            if (subKey != null)
        //                yield return subKey;
        //        }

        //        foreach (var name in key.GetValueNames())
        //        {
        //            yield return new RegValue()
        //            {
        //                Name = name,
        //                Data = key.GetValue(name),
        //                Kind = key.GetValueKind(name)
        //            };
        //        }
        //    }
        //}

        public bool HasChildren(object parent)
        {
            return parent is RegistryKey;
        }
    }

    public struct RegValue
    {
        public string Name { get; set; }
        public object Data { get; set; }
        public RegistryValueKind Kind { get; set; }
    }
}
