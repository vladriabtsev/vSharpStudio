using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class DiffModel
    {
        public DiffModel(IConfig oldest_config, IConfig prev_config, IConfig current_config)
        {
            this.Constants = new DiffConstants(oldest_config.GroupConstantsI.ListConstantsI,
                                               prev_config.GroupConstantsI.ListConstantsI,
                                               current_config.GroupConstantsI.ListConstantsI);
            //this.Catalogs = new DiffCatalogs(oldest_config.GroupCatalogsI.ListCatalogsI,
            //                                   prev_config.GroupCatalogsI.ListCatalogsI,
            //                                   current_config.GroupCatalogsI.ListCatalogsI);
            this.Catalogs = new DiffCatalogs(oldest_config.GroupCatalogsI.ListCatalogsI,
                                               prev_config.GroupCatalogsI.ListCatalogsI,
                                               current_config.GroupCatalogsI.ListCatalogsI);
        }
    }
}
