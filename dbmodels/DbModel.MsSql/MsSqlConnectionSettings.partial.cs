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

namespace vPlugin.DbModel.MsSql
{
    public partial class MsSqlConnectionSettings : IvPluginGeneratorSettingsVM
    {
        partial void OnInit()
        {
            this.MaxPoolSize = 100;
            this.ConnectRetryCount = 1;
            this.ConnectRetryInterval = 10;
            this.PacketSize = 8000;
            this.ConnectTimeout = 15;
            this.Pooling = true;
#if DEBUG
#endif
        }
        public ITreeConfigNode Parent { get; set; }

        [BrowsableAttribute(false)]
        public string Settings
        {
            get
            {
                var proto = MsSqlConnectionSettings.ConvertToProto(this);
                return JsonFormatter.Default.Format(proto);
            }
        }

        public string GenerateCode()
        {
            SqlConnectionStringBuilder vm = new SqlConnectionStringBuilder();
            var from = this;
            //vm.Name = from.Name;
            //vm.Guid = from.Guid;
            if (!from.Pooling)
                vm.Pooling = from.Pooling;
            if (from.Pooling)
            { 
                if (from.MaxPoolSize != 100)
                    vm.MaxPoolSize = from.MaxPoolSize;
                if (from.MinPoolSize > 0)
                    vm.MinPoolSize = from.MinPoolSize;
            }
            if (from.ConnectRetryCount != 1)
                vm.ConnectRetryCount = from.ConnectRetryCount;
            if (from.ConnectRetryInterval != 10)
                vm.ConnectRetryInterval = from.ConnectRetryInterval;
            if (from.MultipleActiveResultSets)
                vm.MultipleActiveResultSets = from.MultipleActiveResultSets;
            //vm.NetworkLibrary = string.IsNullOrWhiteSpace(from.NetworkLibrary) ? null : from.NetworkLibrary;
            if (from.PacketSize != 8000)
                vm.PacketSize = from.PacketSize;
            if (from.PersistSecurityInfo)
                vm.PersistSecurityInfo = from.PersistSecurityInfo;
            if (from.Replication)
                vm.Replication = from.Replication;
            if (!string.IsNullOrWhiteSpace(from.TransactionBinding))
                vm.TransactionBinding = from.TransactionBinding;
            if (!string.IsNullOrWhiteSpace(from.TypeSystemVersion))
                vm.TypeSystemVersion = from.TypeSystemVersion;
            if (!string.IsNullOrWhiteSpace(from.UserID))
                vm.UserID = from.UserID;
            if (from.UserInstance)
                vm.UserInstance = from.UserInstance;
            if (!string.IsNullOrWhiteSpace(from.WorkstationID))
                vm.WorkstationID = from.WorkstationID;
            if (!string.IsNullOrWhiteSpace(from.Password))
                vm.Password = from.Password;

            //vm.Authentication = (System.Data.SqlClient.SqlAuthenticationMethod)from.Authentication;

            if (!string.IsNullOrWhiteSpace(from.InitialCatalog))
                vm.InitialCatalog = from.InitialCatalog;
            if (!string.IsNullOrWhiteSpace(from.ApplicationName))
                vm.ApplicationName = from.ApplicationName;
            if (from.AsynchronousProcessing)
                vm.AsynchronousProcessing = from.AsynchronousProcessing;
            if (from.IntegratedSecurity)
                vm.IntegratedSecurity = from.IntegratedSecurity;
            if (from.ContextConnection)
                vm.ContextConnection = from.ContextConnection;
            if (from.ConnectTimeout != 15)
                vm.ConnectTimeout = from.ConnectTimeout;
            if (!string.IsNullOrWhiteSpace(from.AttachDBFilename))
                vm.AttachDBFilename = from.AttachDBFilename;
            if (!string.IsNullOrWhiteSpace(from.DataSource))
                vm.DataSource = from.DataSource;
            if (from.Encrypt)
            {
                vm.Encrypt = from.Encrypt;
                vm.ColumnEncryptionSetting = (System.Data.SqlClient.SqlConnectionColumnEncryptionSetting)from.ColumnEncryptionSetting;
            }
            if (from.TrustServerCertificate)
                vm.TrustServerCertificate = from.TrustServerCertificate;
            if (from.Enlist)
                vm.Enlist = from.Enlist;
            if (!string.IsNullOrWhiteSpace(from.FailoverPartner))
            {
                vm.FailoverPartner = from.FailoverPartner;
                vm.ApplicationIntent = (System.Data.SqlClient.ApplicationIntent)from.ApplicationIntentValue;
                if (from.LoadBalanceTimeout > 0)
                    vm.LoadBalanceTimeout = from.LoadBalanceTimeout;
                if (from.MultiSubnetFailover)
                    vm.MultiSubnetFailover = from.MultiSubnetFailover;
                if (from.TransparentNetworkIPResolution)
                    vm.TransparentNetworkIPResolution = from.TransparentNetworkIPResolution;
            }
            if (!string.IsNullOrWhiteSpace(from.CurrentLanguage))
                vm.CurrentLanguage = from.CurrentLanguage;

            return vm.ConnectionString;
        }
    }
}
