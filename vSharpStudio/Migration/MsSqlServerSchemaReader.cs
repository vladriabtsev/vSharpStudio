using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using vSharpStudio.vm.Migration;

namespace vSharpStudio.Migration
{
  class MsSqlServerSchemaReader : SchemaReaderBase
  {
    // SchemaReader.ReadSchema
    public override Tables ReadSchema(DbConnection connection, DbProviderFactory factory)
    {
      var result = new Tables();

      _connection = connection;
      _factory = factory;

      var cmd = _factory.CreateCommand();
      cmd.Connection = connection;
      cmd.CommandText = TABLE_SQL;

      //pull the tables in a reader
      using (cmd)
      {

        using (var rdr = cmd.ExecuteReader())
        {
          while (rdr.Read())
          {
            Table tbl = new Table();
            tbl.Name = rdr["TABLE_NAME"].ToString();
            tbl.Schema = rdr["TABLE_SCHEMA"].ToString();
            tbl.IsView = string.Compare(rdr["TABLE_TYPE"].ToString(), "View", true) == 0;
            tbl.CleanName = CleanUp(tbl.Name);
            if (tbl.CleanName.StartsWith("tbl_")) tbl.CleanName = tbl.CleanName.Replace("tbl_", "");
            if (tbl.CleanName.StartsWith("tbl")) tbl.CleanName = tbl.CleanName.Replace("tbl", "");
            tbl.CleanName = tbl.CleanName.Replace("_", "");
            tbl.ClassName = Singularize(RemoveTablePrefixes(tbl.CleanName));

            result.Add(tbl);
          }
        }
      }

      foreach (var tbl in result)
      {
        tbl.Columns = LoadColumns(tbl);

        // Mark the primary keys
        var primaryKeys = GetPKs(tbl.Name).Select(pk => pk.ToLower().Trim());
        foreach (var pkColumn in tbl.Columns.Where(tblCol => primaryKeys.Any(pk => pk == tblCol.Name.ToLower().Trim())))
        {
          pkColumn.IsPK = true;
        }

        // Mark the columns with default columns
        var defaultCols = GetColumnsWithDefaultValues(tbl.Name).Select(pk => pk.ToLower().Trim());
        foreach (var pkColumn in tbl.Columns.Where(tblCol => defaultCols.Any(pk => pk == tblCol.Name.ToLower().Trim())))
        {
          pkColumn.HasDefaultValue = true;
        }

        try
        {
          tbl.OuterKeys = LoadOuterKeys(tbl);
          tbl.InnerKeys = LoadInnerKeys(tbl);
        }
        catch (Exception x)
        {
          var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
          WriteLine("");
          WriteLine("// -----------------------------------------------------------------------------------------");
          WriteLine(String.Format("// Failed to get relationships for `{0}` - {1}", tbl.Name, error));
          WriteLine("// -----------------------------------------------------------------------------------------");
          WriteLine("");
        }
      }


      return result;
    }

    DbConnection _connection;
    DbProviderFactory _factory;


    List<Column> LoadColumns(Table tbl)
    {

      using (var cmd = _factory.CreateCommand())
      {
        cmd.Connection = _connection;
        cmd.CommandText = COLUMN_SQL;

        var p = cmd.CreateParameter();
        p.ParameterName = "@tableName";
        p.Value = tbl.Name;
        cmd.Parameters.Add(p);

        p = cmd.CreateParameter();
        p.ParameterName = "@schemaName";
        p.Value = tbl.Schema;
        cmd.Parameters.Add(p);

        var result = new List<Column>();
        using (IDataReader rdr = cmd.ExecuteReader())
        {
          while (rdr.Read())
          {
            Column col = new Column();
            col.Name = rdr["ColumnName"].ToString();
            col.IsComputed = ((int)rdr["IsComputed"]) == 1;
            col.PropertyName = CleanUp(col.Name);
            col.PropertyType = GetPropertyType(rdr["DataType"].ToString());
            col.PropertyDbType = GetPropertyDbType(rdr["DataType"].ToString());
            col.IsNullable = rdr["IsNullable"].ToString() == "YES";
            col.IsAutoIncrement = ((int)rdr["IsIdentity"]) == 1;
            col.IsGeneratedUniqueIdentifier = rdr["DefaultSetting"].ToString().ToLower().Trim(' ', '(', ')') == "newid";
            result.Add(col);
            //WriteLine(rdr["DataType"]);
          }
        }

        return result;
      }
    }

    List<Key> LoadOuterKeys(Table tbl)
    {
      using (var cmd = _factory.CreateCommand())
      {
        cmd.Connection = _connection;
        cmd.CommandText = OUTER_KEYS_SQL;

        var p = cmd.CreateParameter();
        p.ParameterName = "@tableName";
        p.Value = tbl.Name;
        cmd.Parameters.Add(p);

        var result = new List<Key>();
        using (IDataReader rdr = cmd.ExecuteReader())
        {
          while (rdr.Read())
          {
            var key = new Key();
            key.Name = rdr["FK"].ToString();
            key.ReferencedTableName = rdr["Referenced_tbl"].ToString();
            key.ReferencedTableColumnName = rdr["Referenced_col"].ToString();
            key.ReferencingTableColumnName = rdr["Referencing_col"].ToString();
            result.Add(key);
          }
        }

        return result;
      }
    }

    List<Key> LoadInnerKeys(Table tbl)
    {
      using (var cmd = _factory.CreateCommand())
      {
        cmd.Connection = _connection;
        cmd.CommandText = INNER_KEYS_SQL;

        var p = cmd.CreateParameter();
        p.ParameterName = "@tableName";
        p.Value = tbl.Name;
        cmd.Parameters.Add(p);

        var result = new List<Key>();
        using (IDataReader rdr = cmd.ExecuteReader())
        {
          while (rdr.Read())
          {
            var key = new Key();
            key.Name = rdr["FK"].ToString();
            key.ReferencingTableName = rdr["Referencing_tbl"].ToString();
            key.ReferencedTableColumnName = rdr["Referenced_col"].ToString();
            key.ReferencingTableColumnName = rdr["Referencing_col"].ToString();
            result.Add(key);
          }
        }

        return result;
      }
    }

    List<string> GetPKs(string table)
    {

      string sql = @"SELECT c.name AS ColumnName
                FROM sys.indexes AS i 
                INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id 
                INNER JOIN sys.objects AS o ON i.object_id = o.object_id 
                LEFT OUTER JOIN sys.columns AS c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
                WHERE (i.type = 1) AND (o.name = @tableName)";

      var pks = new List<string>();

      using (var cmd = _factory.CreateCommand())
      {
        cmd.Connection = _connection;
        cmd.CommandText = sql;

        var p = cmd.CreateParameter();
        p.ParameterName = "@tableName";
        p.Value = table;
        cmd.Parameters.Add(p);

        using (IDataReader rdr = cmd.ExecuteReader())
        {
          while (rdr.Read())
          {
            pks.Add(rdr["ColumnName"].ToString());
          }
        }
      }

      return pks;
    }

    List<string> GetColumnsWithDefaultValues(string table)
    {

      string sql = @"SELECT d.ColumnName FROM
						(SELECT c.name as ColumnName, object_definition(c.default_object_id) AS DefaultDefinition
						FROM   sys.columns as c
						INNER JOIN sys.objects as o ON o.object_id = c.object_id
						WHERE o.name=@tableName) as d
					WHERE d.DefaultDefinition IS NOT NULL";

      var defaultCols = new List<string>();

      using (var cmd = _factory.CreateCommand())
      {
        cmd.Connection = _connection;
        cmd.CommandText = sql;

        var p = cmd.CreateParameter();
        p.ParameterName = "@tableName";
        p.Value = table;
        cmd.Parameters.Add(p);

        using (IDataReader rdr = cmd.ExecuteReader())
        {
          while (rdr.Read())
          {
            defaultCols.Add(rdr["ColumnName"].ToString());
          }
        }
      }

      return defaultCols;
    }


    string GetPropertyType(string sqlType)
    {
      string sysType = "string";
      switch (sqlType)
      {
        case "bigint":
          sysType = "long";
          break;
        case "smallint":
          sysType = "short";
          break;
        case "int":
          sysType = "int";
          break;
        case "uniqueidentifier":
          sysType = "Guid";
          break;
        case "smalldatetime":
        case "datetime":
        case "datetime2":
        case "date":
        case "time":
          sysType = "DateTime";
          break;
        case "float":
          sysType = "double";
          break;
        case "real":
          sysType = "float";
          break;
        case "numeric":
        case "smallmoney":
        case "decimal":
        case "money":
          sysType = "decimal";
          break;
        case "tinyint":
          sysType = "byte";
          break;
        case "bit":
          sysType = "bool";
          break;
        case "image":
        case "binary":
        case "varbinary":
        case "timestamp":
          sysType = "byte[]";
          break;
        case "geography":
          sysType = "Microsoft.SqlServer.Types.SqlGeography";
          break;
        case "geometry":
          sysType = "Microsoft.SqlServer.Types.SqlGeometry";
          break;
      }
      return sysType;
    }

    string GetPropertyDbType(string sqlType)
    {
      string sysType = "String";
      switch (sqlType)
      {
        case "bigint":
          sysType = "Int64";
          break;
        case "smallint":
          sysType = "Int16";
          break;
        case "int":
          sysType = "Int32";
          break;
        case "uniqueidentifier":
          sysType = "Guid";
          break;
        case "smalldatetime":
        case "datetime2":
          sysType = "DateTime2";
          break;
        case "datetime":
          sysType = "DateTime";
          break;
        case "date":
          sysType = "Date";
          break;
        case "time":
          sysType = "Time";
          break;
        case "float":
          sysType = "Double";
          break;
        case "real":
          sysType = "Single";
          break;
        case "numeric":
          sysType = "VarNumeric";
          break;
        case "smallmoney":
        case "decimal":
          sysType = "Decimal";
          break;
        case "money":
          sysType = "Currency";
          break;
        case "tinyint":
          sysType = "Byte";
          break;
        case "bit":
          sysType = "Boolean";
          break;
        case "image":
        case "binary":
          sysType = "Binary";
          break;
        case "varbinary":
        case "timestamp":
          sysType = "byte[]";
          break;
        case "geography":
          sysType = "Microsoft.SqlServer.Types.SqlGeography";
          break;
        case "geometry":
          sysType = "Microsoft.SqlServer.Types.SqlGeometry";
          break;
      }
      return sysType;
    }


    const string TABLE_SQL = @"SELECT *
		FROM  INFORMATION_SCHEMA.TABLES
		WHERE TABLE_TYPE='BASE TABLE' OR TABLE_TYPE='VIEW'";

    const string COLUMN_SQL = @"SELECT 
			TABLE_CATALOG AS [Database],
			TABLE_SCHEMA AS Owner, 
			TABLE_NAME AS TableName, 
			COLUMN_NAME AS ColumnName, 
			ORDINAL_POSITION AS OrdinalPosition, 
			COLUMN_DEFAULT AS DefaultSetting, 
			IS_NULLABLE AS IsNullable, DATA_TYPE AS DataType, 
			CHARACTER_MAXIMUM_LENGTH AS MaxLength, 
			DATETIME_PRECISION AS DatePrecision,
			COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsIdentity') AS IsIdentity,
			COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsComputed') as IsComputed
		FROM  INFORMATION_SCHEMA.COLUMNS
		WHERE TABLE_NAME=@tableName AND TABLE_SCHEMA=@schemaName
		ORDER BY OrdinalPosition ASC";

    const string OUTER_KEYS_SQL = @"SELECT 
			FK = OBJECT_NAME(pt.constraint_object_id),
			Referenced_tbl = OBJECT_NAME(pt.referenced_object_id),
			Referencing_col = pc.name, 
			Referenced_col = rc.name
		FROM sys.foreign_key_columns AS pt
		INNER JOIN sys.columns AS pc
		ON pt.parent_object_id = pc.[object_id]
		AND pt.parent_column_id = pc.column_id
		INNER JOIN sys.columns AS rc
		ON pt.referenced_column_id = rc.column_id
		AND pt.referenced_object_id = rc.[object_id]
		WHERE pt.parent_object_id = OBJECT_ID(@tableName);";

    const string INNER_KEYS_SQL = @"SELECT 
			[Schema] = OBJECT_SCHEMA_NAME(pt.parent_object_id),
			Referencing_tbl = OBJECT_NAME(pt.parent_object_id),
			FK = OBJECT_NAME(pt.constraint_object_id),
			Referencing_col = pc.name, 
			Referenced_col = rc.name
		FROM sys.foreign_key_columns AS pt
		INNER JOIN sys.columns AS pc
		ON pt.parent_object_id = pc.[object_id]
		AND pt.parent_column_id = pc.column_id
		INNER JOIN sys.columns AS rc
		ON pt.referenced_column_id = rc.column_id
		AND pt.referenced_object_id = rc.[object_id]
		WHERE pt.referenced_object_id = OBJECT_ID(@tableName);";

  }
}
