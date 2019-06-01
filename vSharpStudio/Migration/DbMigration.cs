using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using vSharpStudio.common;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Migration
{
    public class DbMigration
    {
        private DbMigration() { }
        private DbMigration(IDbDesign migrator) { _migrator = migrator; }
        public static DbMigration CreateMigration(IDbDesign migrator, Action oncreated, Action ondeleting)
        {
            DbMigration res = new DbMigration(migrator);
            return res;
        }
        private IDbDesign _migrator = null;
        private Action oncreated = null;

        /// <summary>
        /// Migrate DB structure from previous version to current.
        /// 
        /// Current code contains full code for a new configuration, and, if a DB structure was changed, 
        /// contains previous configuration, DB access layer to previous version DB with simple base 
        /// operations, and migration code
        /// </summary>
        /// <returns>true if success</returns>
        public bool Migrate()
        {
            // backup DB and Config, and code???

            // get DB model
            //DatabaseModel model = _migrator.GetDbModel();
            // get last migration version from DB
            int last_version = _migrator.GetMigrationVersion(); ;

            // get current migration version and previous version which can be ubdated
            // if current migration version can't migrate previous verion than stop !!!
            //if (last_version > 0 && ( // stop !!!
            //    MainPageVM.ConfigInstance.VersionMigrationSupportFromMin > last_version ||
            //    MainPageVM.ConfigInstance.VersionMigrationCurrent < last_version))
            //{
            //    return false; 
            //}

            // check if fields data types can be changed, if not than stop !!!
            //UpdateToModel(modelOnlyAdd);

            // update field types. 
            // Simple updates by SQL without data lost. 
            // In a case data lost, we has to have conversion code for field
            oncreated();

            // rename tables and fields

            // create new tables and fields in DB

            // apply migration code

            // delete objects which has to be deleted






            // check mapping information
            // if mapping wrong, show mapping errors and stop

            return true;
        }
    }
}
