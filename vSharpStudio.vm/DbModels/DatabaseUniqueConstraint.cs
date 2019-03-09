using System.Collections.Generic;

namespace vSharpStudio.vm.DbModel
{
    /// <summary>
    ///     A simple model for a database unique constraint used when reverse engineering an existing database.
    /// </summary>
    public class DatabaseUniqueConstraint
    {
        /// <summary>
        ///     The table on which the unique constraint is defined.
        /// </summary>
        public virtual DatabaseTable Table { get; set; }

        /// <summary>
        ///     The name of the constraint.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     The ordered list of columns that make up the constraint.
        /// </summary>
        public virtual IList<DatabaseColumn> Columns { get; } = new List<DatabaseColumn>();
    }
}
