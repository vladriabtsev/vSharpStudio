using System.Collections.Generic;

namespace vSharpStudio.vm.DbModel
{
    /// <summary>
    ///     A simple model for a database table used when reverse engineering an existing database.
    /// </summary>
    public class DatabaseTable
    {
        /// <summary>
        ///     The database that contains the table.
        /// </summary>
        public virtual DatabaseModel Database { get; set; }

        /// <summary>
        ///     The table name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     The table schema, or <c>null</c> to use the default schema.
        /// </summary>
        public virtual string Schema { get; set; }

        /// <summary>
        ///     The primary key of the table.
        /// </summary>
        public virtual DatabasePrimaryKey PrimaryKey { get; set; }

        /// <summary>
        ///     The ordered list of columns in the table.
        /// </summary>
        public virtual IList<DatabaseColumn> Columns { get; } = new List<DatabaseColumn>();

        /// <summary>
        ///     The list of unique constraints defined on the table.
        /// </summary>
        public virtual IList<DatabaseUniqueConstraint> UniqueConstraints { get; } = new List<DatabaseUniqueConstraint>();

        /// <summary>
        ///     The list of indexes defined on the table.
        /// </summary>
        public virtual IList<DatabaseIndex> Indexes { get; } = new List<DatabaseIndex>();

        /// <summary>
        ///     The list of foreign key constraints defined on the table.
        /// </summary>
        public virtual IList<DatabaseForeignKey> ForeignKeys { get; } = new List<DatabaseForeignKey>();
    }
}
