using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using BlazorDemo_WebAPI.Entities;
using BlazorDemo_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlazorDemo_WebAPI.Repositories
{
    /// <summary>
    /// Data repository for _UserRole database table.
    /// </summary>
    public class _UserRoleRepository : AdoRepository<_UserRole>, I_UserRoleRepository
	{
		private readonly string _connectionString;
		/// <summary>
		/// Data repository for the _UserRole database table.
		/// </summary>
		public _UserRoleRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<ApiResultModel> Add_Async(_UserRole entity, int? userID = null)
		{
			return await Task.Run(() =>
			{
				if (entity != null)
				{
					try
					{
						var sql = @"INSERT INTO _UserRole(RoleId, UserId, created, createdby)
                                    Output INSERTED.*
                                    VALUES(@roleId, @userID, @_userID)";

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            var parameters = new Dictionary<string, object>()
                            {
                                ["userID"] = entity._UserID,
                                ["roleID"] = entity._RoleID,
                            };
                            return new ApiResultModel
                            {
                                Success = true,
                                Result = new DapperWrapper().Execute(connection, sql, parameters),
                            };
                        }
                    }
					catch (Exception ex)
					{
						return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
					}
				}
				else
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = "Invalid _UserRole Entity." };
				}
			});
		}

		//public async Task<ApiResultModel> GetUserRole_Special_Async(
		//	Expression<Func<_UserRole, bool>> filter = null,
		//	Func<IQueryable<_UserRole>, IOrderedQueryable<_UserRole>>? primaryOrderBy = null,
		//	Func<IQueryable<_UserRole>, IOrderedQueryable<_UserRole>>? secondaryOrderBy = null,
		//	string includeProperties = ""
		//	)
		//{
		//	try
		//	{
		//		IQueryable<_UserRole> query = await Task.Run(() =>
		//	{
		//		var sql = @"SELECT * from _UserRole";
		//		var conn = new SqlConnection { ConnectionString = _connectionString };
		//		using (var command = new SqlCommand(sql, conn))
		//		{
		//			return GetRecords(command);
		//		}
		//	});

		//	// Implement filters if any.
		//	if (filter != null)
		//	{
		//		query = query.Where(filter);
		//	}

		//	if (includeProperties != null)
		//	{
		//		foreach (var includeProperty in includeProperties.Split
		//			(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
		//		{
		//			try { query = query.Include(includeProperty); }
		//			catch
		//			{
		//				// Exempt fields that does not exist such as excluded system fields. 
		//			}
		//		}
		//	}

		//	// Implement Sorting if any.
		//	if (primaryOrderBy != null)
		//	{
		//		query = primaryOrderBy(query);
		//		if (secondaryOrderBy != null)
		//		{
		//			query = secondaryOrderBy(query);
		//		}
		//	}
		//	else
		//	{
		//		query = query.OrderBy(o => o._RoleID);
		//	}
		//		return new ApiResultModel { Success = true, Result = query };
		//	}
		//	catch (SqlException ex)
		//	{
		//		return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException.StackTrace };
		//	}
		//	catch (Exception nex)
		//	{
		//		return new ApiResultModel { Success = false, Result = null, ErrorMessage = nex.InnerException.StackTrace };
		//	}
		//}

		public async Task<ApiResultModel> GetNumberOfUserRole_Async()
		{
			return await Task.Run(() =>
			{
				try
				{
					var sql = @"SELECT count(_UserID) FROM _User";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Query<Int32>(connection, sql),
                        };
                    }
                }
				catch (SqlException ex)
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
				}
			});
		}

		public async Task<ApiResultModel> All_Async()
		{
			return await Task.Run(() =>
			{
				try { 
					var sql = @"SELECT * FROM _UserRole";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Query<_UserRole>(connection, sql),
                        };
                    }
                }
				catch (SqlException ex)
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
				}
			});
		}

		public async Task RemovedByEntity_Async(_UserRole entity)
		{
			await Task.Run(() =>
			{
				if (entity != null)
				{
					try
					{
						// add entity output in query
						var entityID = Guid.NewGuid().ToString();
						var sql = @"Update _UserRole Set Removed = @Removed, RemovedBy =  @RemovedBy WHERE _UserID = @userID and _RoleID = @roleID";

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            var parameters = new Dictionary<string, object>()
                            {
                                ["userID"] = entity._UserID,
                                ["roleID"] = entity._RoleID,
                                ["Removed"] = DateTime.UtcNow,
                                ["RemovedBy"] = entity._RoleID, // need to be Logedin user
                            };
                            return new ApiResultModel
                            {
                                Success = true,
                                Result = new DapperWrapper().Execute(connection, sql, parameters),
                            };
                        }
                    }
					catch (SqlException ex)
					{
						return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
					}
				}
				else
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = "Invalid entity." };
				}
			});
		}

        public async Task<ApiResultModel> AddRangeWith_UserID_Async(AdditionForForeignkey addition)
        {
            var apiResultModel = await Task.Run(() =>
            {
                if (addition != null)
                {
					// Remove old entries by entityID
					try
					{
                        var entityID = Guid.NewGuid().ToString();
                        var sql = @"Delete from _UserRole OUTPUT DELETED.* WHERE _UserID = @_UserID";

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            var parameters = new Dictionary<string, object>()
                            {
                                ["_UserID"] = new Guid(addition.TrackerID),
                            };

                            var result = new DapperWrapper().Execute(connection, sql, parameters);
                        }
                    }
                    catch (SqlException ex)
                    {
                        return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex?.InnerException?.StackTrace };
                    }

                    // Add new entries by data list.
                    //var entityID = Guid.NewGuid().ToString();
                    var insertSql = @"insert into _UserRole (_UserID, _RoleID, Created, CreatedBy) OUTPUT INSERTED.* values (@_UserID, @_RoleID, @Created, @CreatedBy)";
                    var parametersList = new List<_UserRole>();
                    foreach (var toBeAddedID in addition.AdditionIDs)
					{
                        var parameters = new _UserRole()
                        {
                            _UserID = Guid.Parse(addition.TrackerID),
                            _RoleID = Guid.Parse(toBeAddedID),
                            Created = DateTime.UtcNow,
                            CreatedBy = Guid.Parse(addition.TrackerID), // need to be Logedin user
                        };
                        parametersList.Add(parameters);
                    }
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().ExecuteBulk<_UserRole>(connection, insertSql, parametersList),
                        };
                    }
                }
                else
                {
                    return new ApiResultModel { Success = false, Result = null, ErrorMessage = "Invalid entity." };
                }
            });

			return apiResultModel;
        }
	}	
}
