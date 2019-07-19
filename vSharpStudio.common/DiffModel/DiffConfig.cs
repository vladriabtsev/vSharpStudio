using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common;

namespace vSharpStudio.common
{
    /// <summary>
    /// Diff model is using two previous release models and current model
    /// 1. Objects which exist only in current model are new objects
    /// 2. Objects which exist in previous model. but not in current model are deprecated objects
    /// 3. Objects which exist in oldest model. but not in previous and current model are objects for deletion
    /// Same aproach for properties of objects
    /// </summary>
    public partial class DiffConfig
    {
        public DiffConfig(IConfig oldest_config, IConfig prev_config, IConfig current_config)
        {
            this.Constants = new DiffListConstants(
                oldest_config?.GroupConstantsI.ListConstantsI,
                prev_config?.GroupConstantsI.ListConstantsI,
                current_config.GroupConstantsI.ListConstantsI);
            this.Enumerations = new DiffListEnumerations(
                oldest_config?.GroupEnumerationsI.ListEnumerationsI,
                prev_config?.GroupEnumerationsI.ListEnumerationsI,
                current_config.GroupEnumerationsI.ListEnumerationsI);
            this.Catalogs = new DiffListCatalogs(
                oldest_config?.GroupCatalogsI.ListCatalogsI,
                prev_config?.GroupCatalogsI.ListCatalogsI,
                current_config.GroupCatalogsI.ListCatalogsI);
            this.Documents = new DiffListDocuments(
                oldest_config?.GroupDocumentsI,
                prev_config?.GroupDocumentsI,
                current_config.GroupDocumentsI);
            this.Config = current_config;
            this.Config[DiffEnumHistoryAnnotation.DiffConfig.ToString()] = this;
        }

        public IConfig Config;
        public DiffListConstants Constants;
        public DiffListEnumerations Enumerations;
        public DiffListCatalogs Catalogs;
        public DiffListDocuments Documents;

        #region Constants
        // Renamed
        // Deleted - not exist in curent model and previous release model
        // Modified
        // Deprecated - removed in current model, but exist in previous release model
        #endregion Constants
    }
}
