using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class GenSettingsVm : ObservableObject
    {
        public GenSettingsVm() { }
        public GenSettingsVm(ITreeConfigNode node, string appProgectGenGuid) : this()
        {
            Config? cfg = null;
            var lst = new List<GenSettingsPropertyGridVm?>();
            ITreeConfigNode? n = node;
            while (n != null)
            {
                if (n is IGetNodeSetting ngs)
                {
                    var set = ngs.GetSettings(appProgectGenGuid);
                    if (set != null)
                    {
                        lst.Add(new GenSettingsPropertyGridVm(n, set));
                    }
                }
                if (n is Config cfgg)
                    cfg = cfgg;
                n = n.Parent;
            }
            Debug.Assert(cfg != null);
            //var apg=(AppProjectGenerator)cfg.DicNodes[appProgectGenGuid];
            ////lst.Add(new GenSettingsPropertyGridVm(apg, apg.GetEditableNodeSettings()));
            //var prj = apg.ParentAppProject;
            //var sln = prj.ParentAppSolution;
            this.NotEmptyColumns = lst.Count;
            for (int i = lst.Count; i < 10; i++)
            {
                lst.Add(null);
            }
            var lst2 = new List<GenSettingsPropertyGridVm?>();
            for (int i = lst.Count - 1; i >= 0; i--)
            {
                lst2.Add(lst[i]);
            }
            this.Settings = lst2;
        }
        public List<GenSettingsPropertyGridVm?> Settings { get => settings; set => SetProperty(ref settings, value); }
        private List<GenSettingsPropertyGridVm?> settings = new() { null, null, null, null, null, null, null, null, null, new GenSettingsPropertyGridVm(), };
        public int NotEmptyColumns { get; set; }
        public int PropertyGridWidth { get { return PropertyGridWidthStatic; } }
        public static int PropertyGridWidthStatic = 200;
    }
}
