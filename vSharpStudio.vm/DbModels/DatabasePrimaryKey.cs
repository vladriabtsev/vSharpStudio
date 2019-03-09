using System.Collections.Generic;

namespace vSharpStudio.vm.DbModel
{
    /// <summary>
    ///     A simple model for a database primary key used when reverse engineering an existing database.
    /// </summary>
    public class DatabasePrimaryKey
    {
        /// <summary>
        ///     The table on which the primary key is defined.
        /// </summary>
        public virtual DatabaseTable Table { get; set; }

        /// <summary>
        ///     The name of the primary key.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     The ordered list of columns that make up the primary key.
        /// </summary>
        public virtual IList<DatabaseColumn> Columns { get; } = new List<DatabaseColumn>();
    }
}
