using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Dapper;

namespace vSharpStudio.db
{
    public class DbConnector
    {
        public string ConnectionString { get; private set; }
        //public void ExecNonQuery()
        //{
        //    using (IDbConnection db = new SqlConnection(this.ConnectionString))
        //    {
        //        return db.Query<Author>
        //        (“Select * From Author”).ToList();
        //    }
        //}

        //private static void CreateSelectGroupGS(StringBuilder sb, string where = null, ServiceSqlParameter[] param = null, string[] sort = null, int page = 0, int pagesize = 0)
        //{
        //    sb.Append("SELECT * FROM [dbo].[GroupGS]");
        //    if (where != null)
        //    {
        //        sb.Append(" WHERE ");
        //        sb.Append(where);
        //    }
        //    if (pagesize > 0 && sort == null)
        //        throw new Exception("To use paging sort parameter has to be provided");
        //    if (sort != null)
        //    {
        //        sb.Append(" ORDER BY ");
        //        string sep = "";
        //        foreach (var t in sort)
        //        {
        //            sb.Append(sep);
        //            sep = ", ";
        //            sb.Append(t);
        //        }
        //    }
        //    if (page > 0 && pagesize > 0)
        //    {
        //        sb.Append(" OFFSET ");
        //        sb.Append((page - 1) * pagesize);
        //        sb.Append(" ROWS FETCH NEXT ");
        //        sb.Append(pagesize);
        //        sb.Append(" ROWS ONLY");
        //    }
        //}
        //public IEnumerable<T> Get(string where, ServiceSqlParameter[] param = null, string[] sort = null, int page = 0, int pagesize = 0)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    CreateSelectGroupGS(sb, where, param, sort, page, pagesize);
        //    if (param == null)
        //    {
        //        return Connection.Query<GroupG>(
        //            sb.ToString(),
        //            transaction: Transaction,
        //            commandTimeout: CommandTimeout
        //        );
        //    }
        //    Dictionary<string, object> p = new Dictionary<string, object>();
        //    foreach (var t in param)
        //    {
        //        p[t.Name] = t.Value;
        //    }
        //    return Connection.Query<GroupG>(
        //        sb.ToString(),
        //        p,
        //        transaction: Transaction,
        //        commandTimeout: CommandTimeout
        //    );
        //}
        //public List<GroupG> GetList(string where, ServiceSqlParameter[] param = null, string[] sort = null, int page = 0, int pagesize = 0)
        //{
        //    IEnumerable<GroupG> lst = Get(where, param, sort, page, pagesize);
        //    List<GroupG> res = new List<GroupG>();
        //    foreach (var t in lst)
        //    {
        //        res.Add(t);
        //    }
        //    return res;
        //}

    }
    public class ServiceSqlParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
