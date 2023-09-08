using System.Data;
using Dapper;

namespace BlazorDemo_WebAPI.Repositories
{
    /// <summary>
    /// Common functionality for all repositories
    /// </summary>
    public abstract class RepositoryBase
	{
		private IDbTransaction _transaction;
		private string connectionString;
		private IDbConnection Connection { get { return _transaction.Connection; } }

		public RepositoryBase(IDbTransaction transaction)
		{
			_transaction = transaction;
		}

		protected RepositoryBase(string connectionString)
		{
			this.connectionString = connectionString;
		}

		protected T ExecuteScalar<T>(string sql, object param)
		{
			return Connection.ExecuteScalar<T>(sql, param, _transaction);
		}

		protected T QuerySingleOrDefault<T>(string sql, object param)
		{
			return Connection.QuerySingleOrDefault<T>(sql, param, _transaction);
		}

		protected IEnumerable<T> Query<T>(string sql, object param = null)
		{
			return Connection.Query<T>(sql, param, _transaction);
		}

		protected IEnumerable<T> SpecialQuery<T>(string sql, object param = null)
		{
			return Connection.Query<T>(sql, param, _transaction);
		}

		protected void Execute(string sql, object param)
		{
			Connection.Execute(sql, param, _transaction);
		}
	}
}
