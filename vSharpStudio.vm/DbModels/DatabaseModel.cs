using System.Collections.Generic;

namespace vSharpStudio.vm.DbModel
{
    public class DatabaseModel
    {
        /// <summary>
        ///     The database name, or <c>null</c> if none is set.
        /// </summary>
        public virtual string DatabaseName { get; set; }

        /// <summary>
        ///     The database schema, or <c>null</c> to use the default schema.
        /// </summary>
        public virtual string DefaultSchema { get; set; }

        /// <summary>
        ///     The list of tables in the database.
        /// </summary>
        public virtual IList<DatabaseTable> Tables { get; } = new List<DatabaseTable>();

        /// <summary>
        ///     The list of sequences in the database.
        /// </summary>
        public virtual IList<DatabaseSequence> Sequences { get; } = new List<DatabaseSequence>();
    }
}
