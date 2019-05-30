using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConnMsSql : IvPluginSettingsVM
    {
        partial void OnInit()
        {
            // TODO set default values
            // TODO set different default values for debug, or additional connection string ?
#if DEBUG
#endif
        }
        public ITreeConfigNode Parent { get; set; }

        [BrowsableAttribute(false)]
        public string Settings
        {
            get
            {
                var proto = ConnMsSql.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
                //return proto.ToByteArray();
                //return Any.Pack(proto);
            }
        } //.proto_conn_ms_sql");

        public string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}
