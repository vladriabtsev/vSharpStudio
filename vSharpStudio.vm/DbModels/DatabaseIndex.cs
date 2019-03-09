using System.Collections.Generic;

namespace vSharpStudio.vm.DbModel
{
    /// <summary>
    ///     A simple model for a database index used when reverse engineering an existing database.
    /// </summary>
    public class DatabaseIndex
    {
        /// <summary>
        ///     The table that contains the index.
        /// </summary>
        public virtual DatabaseTable Table { get; set; }

        /// <summary>
        ///     The index name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     The ordered list of columns that make up the index.
        /// </summary>
        public virtual IList<DatabaseColumn> Columns { get; } = new List<DatabaseColumn>();

        /// <summary>
        ///     Indicates whether or not the index constrains uniqueness.
        /// </summary>
        public virtual bool IsUnique { get; set; }

        /// <summary>
        ///     The filter expression, or <c>null</c> if the index has no filter.
        /// </summary>
        public virtual string Filter { get; set; }
    }
}
