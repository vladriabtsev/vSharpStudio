using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.Migration
{
  public class Column
  {
    public string Name;
    public string PropertyName;
    public string PropertyType;
    public string PropertyDbType;
    public bool IsPK;
    public bool HasDefaultValue;
    public bool IsNullable;
    public bool IsAutoIncrement;
    public bool IsComputed;
    public bool Ignore;
    public bool IsGeneratedUniqueIdentifier;
  }
  public class Key
  {
    public string Name;
    public string ReferencedTableName;
    public string ReferencedTableColumnName;
    public string ReferencingTableName;
    public string ReferencingTableColumnName;
  }
  public class Table
  {
    public List<Column> Columns;
    public List<Key> InnerKeys = new List<Key>();
    public List<Key> OuterKeys = new List<Key>();
    public string Name;
    public string Schema;
    public bool IsView;
    public string CleanName;
    public string ClassName;
    public string SequenceName;
    public bool Ignore;
    public bool Include;

    public Column GetColumn(string columnName)
    {
      return Columns.Single(x => string.Compare(x.Name, columnName, true) == 0);
    }
    public Column this[string columnName]
    {
      get
      {
        return GetColumn(columnName);
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
      get { return GetTable(tableName); }
    }
  }
}
