using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vSharpStudio.vm.Migration
{
    public class Column
    {
        public string? Name { get; set; }
        public string? PropertyName { get; set; }
        public string? PropertyType { get; set; }
        public string? PropertyDbType { get; set; }
        public bool IsPK { get; set; }
        public bool HasDefaultValue { get; set; }
        public bool IsNullable { get; set; }
        public bool IsAutoIncrement { get; set; }
        public bool IsComputed { get; set; }
        public bool Ignore { get; set; }
        public bool IsGeneratedUniqueIdentifier { get; set; }
    }

    public class Key
    {
        public string? Name { get; set; }
        public string? ReferencedTableName { get; set; }
        public string? ReferencedTableColumnName { get; set; }
        public string? ReferencingTableName { get; set; }
        public string? ReferencingTableColumnName { get; set; }
    }

    public class Table
    {
        public List<Column> Columns => new();
        public List<Key> InnerKeys => new();
        public List<Key> OuterKeys => new();
        public string? Name { get; set; }
        public string? Schema { get; set; }
        public bool IsView { get; set; }
        public string? CleanName { get; set; }
        public string? ClassName { get; set; }
        public string? SequenceName { get; set; }
        public bool Ignore { get; set; }
        public bool Include { get; set; }

        public Column GetColumn(string columnName)
        {
            return this.Columns.Single(x => string.Compare(x.Name, columnName, true) == 0);
        }

        public Column this[string columnName]
        {
            get
            {
                return this.GetColumn(columnName);
            }
        }
    }

    public class Tables : List<Table>
    {
        public Tables()
        {
        }

        public Table GetTable(string tableName)
        {
            return this.Single(x => string.Compare(x.Name, tableName, true) == 0);
        }

        public Table this[string tableName]
        {
            get { return this.GetTable(tableName); }
        }
    }
}
