using System.Collections.Generic;

namespace vSharpStudio.vm.DbModel
{
    /// <summary>
    ///     A simple model for a database foreign key constraint used when reverse engineering an existing database.
    /// </summary>
    public class DatabaseForeignKey
    {
        /// <summary>
        ///     The table that contains the foreign key constraint.
        /// </summary>
        public virtual DatabaseTable Table { get; set; }

        /// <summary>
        ///     The table to which the columns are constrained.
        /// </summary>
        public virtual DatabaseTable PrincipalTable { get; set; }

        /// <summary>
        ///     The ordered list of columns that are constrained.
        /// </summary>
        public virtual IList<DatabaseColumn> Columns { get; } = new List<DatabaseColumn>();

        /// <summary>
        ///     The ordered list of columns in the <see cref="PrincipalTable" /> to which the <see cref="Columns" />
        ///     of the foreign key are constrained.
        /// </summary>
        public virtual IList<DatabaseColumn> PrincipalColumns { get; } = new List<DatabaseColumn>();

        /// <summary>
        ///     The foreign key constraint name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     The action performed by the database when a row constrained by this foreign key
        ///     is deleted, or <c>null</c> if there is no action defined.
        /// </summary>
        public virtual ReferentialAction? OnDelete { get; set; }
    }
}
