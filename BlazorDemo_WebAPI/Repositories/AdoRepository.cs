using System.Data;
using Microsoft.Data.SqlClient;

using System.Linq.Expressions;


namespace BlazorDemo_WebAPI.Repositories
{
    // System critical non-generated file, not subject to regeneration overwrites.
    public abstract class AdoRepository<T> where T : class
	{
		private static string _connectionString;

		public AdoRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

	}
}