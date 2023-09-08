using System.Data;
using Microsoft.Data.SqlClient;

using System.Linq.Expressions;
using BlazorDemo_WebAPI.Entities;
using BlazorDemo_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Linq;
using static Dapper.SqlMapper;

namespace BlazorDemo_WebAPI.Repositories
{
	/// <summary>
	/// Data repository for the _Setting table.
	/// </summary>
	public class _RoleRepository : AdoRepository<_Role>, I_RoleRepository
	{
		private readonly string _connectionString;

		/// <summary>
		/// Data repository for the _Setting table.
		/// </summary>
		public _RoleRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}

		#region DMZ - Default system functions.
		/// <summary>
		/// Get list of a users roles from the _UserRole table by _userID
		/// </summary>
		/// <param name="userId"></param>
		public async Task<ApiResultModel> GetRolesByUserId_Async(string userId)
		{

			return await Task.Run(() =>
			{
				try
				{
					var sql = @"SELECT _Role.* from _UserRole
									join _User on _UserRole._UserID = _User._UserID
									join _Role on _UserRole._RoleID = _Role._RoleID
									Where _User._UserID = @userId";

					using (var connection = new SqlConnection(_connectionString))
					{
                        var parameters = new Dictionary<string, object>()
                        {
                            ["userId"] = userId
                        };
						return new ApiResultModel
						{
							Success = true,
							Result = new DapperWrapper().Query<_Role>(connection, sql, parameters),
						};
					}
                }
				catch (SqlException ex)
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
				}
			});
		}
		#endregion DMZ


		public async Task<ApiResultModel> GetNumberOf_Roles_Async()
		{
			try { 
				return await Task.Run(() =>
				{
					var sql = @"SELECT count(_RoleID) FROM _Role";

					using (var connection = new SqlConnection(_connectionString))
					{
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Query<Int32>(connection, sql.ToString()),
                        };
                    }
                });
			}
			catch (SqlException ex)
			{
				return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
			}
		}

		/// <summary>
		/// Special query function for costom needs
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="primaryOrderBy"></param>
		/// <param name="secondaryOrderBy"></param>
		/// <param name="includeProperties"></param>
		//public async Task<ApiResultModel> GetRole_Special_Async(
		//	Expression<Func<List<_Role>, bool>> filter = null,
		//	Func<IQueryable<_Role>, IOrderedQueryable<_Role>> primaryOrderBy = null,
		//	Func<IQueryable<_Role>, IOrderedQueryable<_Role>> secondaryOrderBy = null,
		//	string includeProperties = "")
		//{
		//	try
		//	{
		//		IQueryable<List<_Role>> query = await Task.Run(() =>
		//		{
		//			var sql = @"SELECT _RoleID, ConcurrencyStamp, Name, NormalizedName
		//                      FROM _Role";
  //                  //var conn = new SqlConnection { ConnectionString = _connectionString };
  //                  //using (var command = new SqlCommand(sql, conn))
  //                  //{
  //                  //	return GetRecords(command);
  //                  //}
  //                  using (var connection = new SqlConnection(_connectionString))
  //                  {
		//				return new DapperWrapper().Query<List<_Role>>(connection, sql.ToString()).AsQueryable();
  //                  }
  //              });

		//		// Implement filters if any.
		//		if (filter != null)
		//		{
		//			query = query.Where(filter);
		//		}

		//		if (includeProperties != null)
		//		{
		//			foreach (var includeProperty in includeProperties.Split
		//				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
		//			{
		//				try { query = query.Include(includeProperty); }
		//				catch
		//				{
		//					// Exempt fields that does not exist such as excluded system fields. 
		//				}
		//			}
		//		}

		//		// Implement Sorting if any.
		//		if (primaryOrderBy != null)
		//		{
		//			query = primaryOrderBy(query);
		//			if (secondaryOrderBy != null)
		//			{
		//				query = secondaryOrderBy(query);
		//			}
		//		}
		//		else
		//		{
		//			query = query.OrderBy(o => o._RoleID);
		//		}
		//		return new ApiResultModel { Success = true, Result = query};
		//	}
		//	catch (SqlException ex)
		//	{
		//		return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
		//	}
		//}

		/// <summary>
		/// Get all rows from _Role, Use only when you have no other choice.
		/// </summary>
		public async Task<ApiResultModel> All_Async()
		{
			try
			{
				return await Task.Run(() =>
				{
					var sql = @"SELECT * FROM _Role";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Query<_Role>(connection, sql.ToString()),
                        };
                    }
                });
			}
			catch (SqlException ex)
			{
				return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
			}
		}

		/// <summary>
		/// Get single _Role by its unique ID
		/// </summary>
		/// <param name="entityID"></param>
		/// <returns></returns>
		public async Task<ApiResultModel> GetByEntityID_Async(String entityID)
		{
			try
			{
				return await Task.Run(() =>
				{
					var sql = @"SELECT *
								FROM _Role
								where _RoleID = @entityID";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        var parameters = new Dictionary<string, object>()
                        {
                            ["entityID"] = entityID
                        };
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Query<_Role>(connection, sql, parameters),
                        };
                    }
                });
			}
			catch (SqlException ex)
			{
				return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
			}
		}

		/// <summary>
		/// Add _role entity to database.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public async Task<ApiResultModel> Add_Async(_Role entity, Guid userID)
		{
			try
			{
				return await Task.Run(() =>
				{
					var entityID = Guid.NewGuid().ToString();
					var sql = @"INSERT INTO _Role(_RoleID, Name, created, createdby)
						        OUTPUT INSERTED.*
						        VALUES(@_RoleID, @Name, @CurrentDatetime @UserID;";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        var parameters = new Dictionary<string, object>()
                        {
                            ["_RoleID"] = entityID,
                            ["Name"] = entity.Name,
                            ["CurrentDatetime"] = DateTime.UtcNow,
                            ["UserID"] = userID,
                        };
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Execute(connection, sql, parameters),
                        };
                    }
                });
			}
			catch (SqlException ex)
			{
				return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
			}
		}

		public async Task<ApiResultModel> GetByName_Async(string entityName)
		{
			try {
				return await Task.Run(() =>
				{

					var sql = @"SELECT _RoleID, ConcurrencyStamp, Name, NormalizedName
			                FROM _Role
                            where Name = @entityName";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        var parameters = new Dictionary<string, object>()
                        {
                            ["Name"] = entityName,
                        };
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Query<_Role>(connection, sql, parameters),
                        };
                    }
                });
			}
			catch (SqlException ex)
			{
				return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
			}
		}

		/// <summary>
		/// Tag a _Role entity as removed and by who and when.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public async Task<ApiResultModel> Remove_Async(_Role entity, Guid userID)
		{					
			try
			{
				return await Task.Run(() =>
				{
					var entityID = Guid.NewGuid().ToString();
					var sql = @"UPDATE _Role SET 
									Removed = @currentDateTime,
									RemovedBy = @userID
									OUTPUT INSERTED.*
			     					WHERE _RoleID = @_RoleID";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        var parameters = new Dictionary<string, object>()
                        {
                            ["_RoleID"] = entity._RoleID,
                            ["currentDateTime"] = DateTime.UtcNow.ToString(),
                            ["userID"] = userID,
                        };
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Execute(connection, sql, parameters),
                        };
                    }

                });
			}
			catch (SqlException ex)
			{
				return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
			}
		}


		/// <summary>
		/// Udate the _Role enitity and by who and when
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public async Task<ApiResultModel> Update_Async(_Role entity, Guid userID)
		{
			try
			{
				return await Task.Run(() =>
				{
					var entityID = Guid.NewGuid().ToString();
					var sql = @"UPDATE _Role SET 
									Name = @Name,
									ModifiedBy = @userID,
									Modified = @currentDateTime,
									OUTPUT INSERTED.*
			     					WHERE _RoleID = @_RoleID";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        var parameters = new Dictionary<string, object>()
                        {
                            ["_RoleID"] = entity._RoleID,
                            ["Name"] = entity.Name,
                            ["currentDateTime"] = DateTime.UtcNow.ToString(),
                            ["userID"] = userID,
                        };
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Execute(connection, sql, parameters),
                        };
                    }
                });
			}
			catch (SqlException ex)
			{
				return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
			}
		}
	}
}
