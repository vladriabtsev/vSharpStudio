using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using vSharpStudio.vm.ViewModels;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.Views
{
    public class TreeSubModelObjectsSelection : TreeListView
    {
        ConfigModel cm = null;
        Config cfg = null;
        bool isLst = false;
        public TreeSubModelObjectsSelection()
        {
            this.DataContextChanged += TreeSubModelObjectsSelection_DataContextChanged;
        }

        private void TreeSubModelObjectsSelection_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
            }
            else
            {
                if (e.NewValue is Model)
                {
                    var sm = e.NewValue as Model;
                    cfg = (Config)sm.Parent.Parent;
                }
                else if (e.NewValue is GroupListModels)
                {
                    var gsm = e.NewValue as GroupListModels;
                    cfg = (Config)gsm.Parent;
                    isLst = true;
                }
                cm = cfg.Model;
                this.Model = cm;
            }
        }
    }
}
