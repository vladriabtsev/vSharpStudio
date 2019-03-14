using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.Migration;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.ViewModels
{
    //TODO report based on FlowDocument https://github.com/rodrigovedovato/FlowDocumentReporting
    public class ConfigRoot : Config
    {
        public ConfigRoot() { }
        public ConfigRoot(string configJson) : base(configJson) { }

        public List<EntityObjectProblem> GetUpdateDbProblems()
        {
            var res = new List<EntityObjectProblem>();
            var sr = new DbShemaReader(PathToProjectWithConnectionString, ConnectionStringName);
            switch (sr.ProviderName)
            {
                case DbShemaReader.PROVIDERNAMESQL:
                    return GetUpdateSqlDbProblems(sr.ConnectionString);
                    break;
                //case DbShemaReader.PROVIDERNAMESQLITE:
                //    break;
                default:
                    throw new ArgumentException("Unsupported ProviderName in connection string: " + sr.ProviderName);
            }

            return res;
        }


        public void UpdateDb()
        {
        }
    }
}
