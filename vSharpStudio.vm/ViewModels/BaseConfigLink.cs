using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public partial class BaseConfigLink : INodeGenSettings, INewAndDeleteion
    {
        public static readonly string DefaultName = "BaseConfig";
        //        partial void OnRelativeConfigFilePathChanged()
        //        {
        //            if (this.IsNotNotifying)
        //                return;
        //            if (string.IsNullOrWhiteSpace(this._RelativeConfigFilePath))
        //                throw new Exception();
        //            var cfg = this.GetConfig();
        //            if (string.IsNullOrWhiteSpace(cfg.CurrentCfgFolderPath))
        //                return;
        //            string path = Path.GetFullPath(this._RelativeConfigFilePath);
        //#if NET48
        //            this._RelativeConfigFilePath = vSharpStudio.common.Utils.GetRelativePath(cfg.CurrentCfgFolderPath, path);
        //#else
        //            this._RelativeConfigFilePath = Path.GetRelativePath(cfg.CurrentCfgFolderPath, path);
        //#endif

        //        }
        [Browsable(false)]
        new public string IconName { get { return "icon3DScene"; } }
        //protected override string GetNodeIconName() { return "icon3DScene"; }
        partial void OnInit()
        {
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }
    }
}
