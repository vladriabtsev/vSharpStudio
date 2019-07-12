using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public partial class BaseConfig : IBaseConfig
    {
        public static readonly string DefaultName = "BaseConfig";
        [BrowsableAttribute(false)]
        public Config Config
        {
            set
            {
                _Config = value;
                NotifyPropertyChanged();
                if (this.Parent != null)
                    this.Parent.Name = _Config.Name;
                //ValidateProperty();
            }
            get { return _Config; }
        }

        IConfig IBaseConfig.Config => _Config;

        private Config _Config;
    }
}
