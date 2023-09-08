using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BlazorDemo_WebAPI.Context
{
    /// <summary>
    /// Dapper context class
    /// </summary>
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Dapper context class constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Get connectionstring from configuration.
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection() => new SqlConnection(_configuration.GetConnectionString("SqlConnection"));

        /// <summary>
        /// Get connectionstring from configuration.
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateMasterConnection() => new SqlConnection(_configuration.GetConnectionString("MasterConnection"));
    }
}
