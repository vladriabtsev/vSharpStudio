using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common;
using FluentValidation;

namespace vPlugin.DbModel.MsSql
{
    public partial class MsSqlConnectionSettings
    {
        public partial class MsSqlConnectionSettingsValidator
        {
            public MsSqlConnectionSettingsValidator()
            {
                RuleFor(x => x.MaxPoolSize).GreaterThan(0);
                RuleFor(x => x.MaxPoolSize).GreaterThan(x => x.MinPoolSize);
                RuleFor(x => x.MinPoolSize).GreaterThanOrEqualTo(0);
                RuleFor(x => x.MinPoolSize).LessThan(x => x.MaxPoolSize);
                RuleFor(x => x.ConnectRetryCount).GreaterThanOrEqualTo(0);
                RuleFor(x => x.ConnectRetryCount).LessThanOrEqualTo(255);
                RuleFor(x => x.ConnectRetryInterval).GreaterThanOrEqualTo(1);
                RuleFor(x => x.ConnectRetryInterval).LessThanOrEqualTo(60);
                RuleFor(x => x.PacketSize).GreaterThanOrEqualTo(512);
                RuleFor(x => x.PacketSize).LessThanOrEqualTo(32768);
                RuleFor(x => x.DataSource).NotEmpty();
                RuleFor(x => x.InitialCatalog).NotEmpty();
            }
        }
    }
}
