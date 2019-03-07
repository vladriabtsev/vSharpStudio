using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Google.Protobuf;

namespace vSharpStudio.vm.ViewModels
{
  public partial class Config
  {
    partial void OnInit()
    {
      this.Guid = System.Guid.NewGuid().ToString();
    }
    public Config(string configJson) : base(ConfigValidator.Validator)
    {
      this._dto = Proto.Config.proto_config.Parser.ParseJson(configJson);
      this.initFromDto();
    }
    public string ExportToJson()
    {
      var res = JsonFormatter.Default.Format(this._dto);
      return res;
    }
  }
}
