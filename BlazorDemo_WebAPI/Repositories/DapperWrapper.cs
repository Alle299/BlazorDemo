using Dapper;
using System.Data;

namespace BlazorDemo_WebAPI.Repositories
{
    public class DapperWrapper : IDapperWrapper
    {
        public IEnumerable<T> Query<T>(IDbConnection connection, string sql)
        {
            return connection.Query<T>(sql);
        }

        public IEnumerable<T> Query<T>(IDbConnection connection, string sql, Dictionary<string, object> parameters)
        {
            return connection.Query<T>(sql, parameters);
        }

        public Int32 Execute(IDbConnection connection, string sql, Dictionary<string, object> parameters)
        {
            return connection.Execute(sql, parameters);
        }
        public Int32 ExecuteBulk<T>(IDbConnection connection, string sql, IEnumerable<T> insertList)
        {
            return connection.Execute(sql, insertList);
        }
    }
}
