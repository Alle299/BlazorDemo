using System.Data;

namespace BlazorDemo_WebAPI.Repositories
{
    public interface IDapperWrapper
    {
        public interface IDapperWrapper
        {
            IEnumerable<T> Query<T>(IDbConnection connection, string sql);
            IEnumerable<T> Query<T>(IDbConnection connection, string sql, Dictionary<string, object> parameters);
            IEnumerable<T> Execute<T>(IDbConnection connection, string sql, Dictionary<string, object> parameters);
            Int32 ExecuteBulk<T>(IDbConnection connection, string sql, IEnumerable<T> insertList);
        }
    }
}
