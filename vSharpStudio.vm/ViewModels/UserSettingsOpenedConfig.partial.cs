using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public partial class UserSettingsOpenedConfig
    {
        public static string DateFormat = "yyyy-MMM-dd H:mm";
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.OpenedLastTimeOn.ToDateTime().ToString(DateFormat));
            sb.Append(" ");
            sb.Append(this.ConfigPath);
            return sb.ToString();
        }
        public string DateStr { get { return this.OpenedLastTimeOn.ToDateTime().ToString(DateFormat); } }
        public string ConfigPathWithDate { get { return this.ToString(); } }
        partial void OnCreating()
        {
            this._Guid = System.Guid.NewGuid().ToString();
        }
    }
}
