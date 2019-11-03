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
    public partial class DiffConfigModel
    {
        public DiffConfigModel(IConfigModel oldest_config, IConfigModel prev_config, IConfigModel current_config)
        {
            this.Constants = new DiffListConstants(
                oldest_config?.IGroupConstants.IListConstants,
                prev_config?.IGroupConstants.IListConstants,
                current_config.IGroupConstants.IListConstants);
            this.Enumerations = new DiffListEnumerations(
                oldest_config?.IGroupEnumerations.IListEnumerations,
                prev_config?.IGroupEnumerations.IListEnumerations,
                current_config.IGroupEnumerations.IListEnumerations);
            this.Catalogs = new DiffListCatalogs(
                oldest_config?.IGroupCatalogs.IListCatalogs,
                prev_config?.IGroupCatalogs.IListCatalogs,
                current_config.IGroupCatalogs.IListCatalogs);
            this.Documents = new DiffListDocuments(
                oldest_config?.IGroupDocuments,
                prev_config?.IGroupDocuments,
                current_config.IGroupDocuments);
            this.Model = current_config;
            //this.Model[DiffEnumHistoryAnnotation.DiffConfigModel.ToString()] = this;
        }

        public IConfigModel Model;
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
