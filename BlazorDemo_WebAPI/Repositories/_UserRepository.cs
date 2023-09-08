using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using BlazorDemo_WebAPI.Entities;
using System.Text;
using BlazorDemo_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace BlazorDemo_WebAPI.Repositories
{
    /// <summary>
    /// Data repository for _User database table.
    /// </summary>
    public class _UserRepository : AdoRepository<_User>, I_UserRepository
	{
		private readonly string _connectionString;

        /// <summary>
        /// Data repository for the _User database table.
        /// </summary>
        public _UserRepository(string connectionString) : base(connectionString)
        {
			_connectionString = connectionString;
        }

        /// <summary>
        /// Non generic function to fetch user by username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApiResultModel> FindByUsernameAndHashedPassword_Async(string username, string password)
		{
			return await Task.Run(() =>
			{
				try
				{
					// ToDo - Add datainjection security
					var sql = @"SELECT top 1 * FROM _User WHERE UserName = @userName and PasswordHash = @passwordHash";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        var parameters = new Dictionary<string, object>()
                        {
                            ["userName"] = username,
                            ["passwordHash"] = Convert.ToBase64String(Encoding.ASCII.GetBytes(password)),
                        };
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Query<_User>(connection, sql, parameters),
                        };
                    }

                }
				catch (SqlException ex)
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException.StackTrace };
				}
			});
		}

		public async Task<ApiResultModel> GetByRoleName_Async(string roleName)
		{
			return await Task.Run(() =>
			{
				try
				{
					var sql = @"SELECT _User.* from _UserRole
									join _User on _UserRole.UserId = _User._UserId
									join _Role on _UserRole.RoleId = _Role._RoleId
									Where _Role.Mame = @roleName";

					using (var connection = new SqlConnection(_connectionString))
					{
						var parameters = new Dictionary<string, object>()
						{
							["roleName"] = roleName
						};
						return new ApiResultModel
						{
							Success = true,
							Result = new DapperWrapper().Query<_User>(connection, sql, parameters),
						};
					}
				}
				catch (SqlException ex)
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException.StackTrace };
				}
			});
		}


		//public async Task<ApiResultModel> GetUser_Special_Async(
		//	Expression<Func<_User, bool>>? filter = null,
		//	Func<IQueryable<_User>, IOrderedQueryable<_User>>? primaryOrderBy = null,
		//	Func<IQueryable<_User>, IOrderedQueryable<_User>>? secondaryOrderBy = null,
		//	string includeProperties = ""
		//	)
		//{
		//	try
		//	{
		//		IQueryable<_User> query = await Task.Run(() =>
		//		{
		//			var sql = @"SELECT _UserID, UserID, AccessFailedCount, ConcurrencyStamp, Discriminator, Email, EmailConfirmed, LockoutEnabled, LockoutEnd, NormalizedEmail,
		//				NormalizedUserName, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName
		//				FROM _User";
		//			var conn = new SqlConnection { ConnectionString = _connectionString };
		//			using (var command = new SqlCommand(sql, conn))
		//			{
		//				return GetRecords(command);
		//			}
		//		});

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
		//			query = query.OrderBy(o => o._UserID);
		//		}
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

		public async Task<ApiResultModel> GetNumberOf_Users_Async()
		{
			return await Task.Run(() =>
			{
				try { 
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
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
				}
			});
		}

		public async Task<ApiResultModel> All_Async()
		{
			return await Task.Run(() =>
			{
				try { 
					var sql = @"SELECT * FROM _User";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Query<_User>(connection, sql),
                        };
                    }
                }
				catch (SqlException ex)
				{
                    return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
                }
			});
		}

		/// <summary>
		/// Get single _User by its unique ID
		/// </summary>
		/// <param name="entityID"></param>
		/// <returns></returns>
		public async Task<ApiResultModel> GetByEntityID_Async(String entityID)
		{
			return await Task.Run(() =>
			{
				if (!string.IsNullOrEmpty(entityID))
				{
					try
					{
						var sql = @"SELECT * FROM _User where _UserID = @entityID";

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            var parameters = new Dictionary<string, object>()
                            {
                                ["entityID"] = entityID
                            };
                            return new ApiResultModel
                            {
                                Success = true,
                                Result = new DapperWrapper().Query<_User>(connection, sql, parameters),
                            };
                        }
                    }
					catch (SqlException ex)
					{
						return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
					}
				} else
                {
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = string.Format("{0} is not a valid enitity", entityID )};
				}
			});
		}

		public async Task<ApiResultModel> GetByName_Async(string entityName)
		{
			return await Task.Run(() =>
			{
				try { 
					var sql = @"SELECT top 1 * FROM _User WHERE [Name]  = @entityName";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        var parameters = new Dictionary<string, object>()
                        {
                            ["Name"] = entityName
                        };
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Query<_User>(connection, sql, parameters),
                        };
                    }
                }
				catch (SqlException ex)
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
				}
			});
		}

		public async Task<ApiResultModel> GetByUserName_Async(string entityUserName)
		{
			return await Task.Run(() =>
			{
				try { 
					var sql = @"SELECT top 1 * FROM _User WHERE [UserName]  = @entityUserName";

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        var parameters = new Dictionary<string, object>()
                        {
                            ["UserName"] = entityUserName
                        };
                        return new ApiResultModel
                        {
                            Success = true,
                            Result = new DapperWrapper().Query<_User>(connection, sql, parameters),
                        };
                    }
                }
				catch (SqlException ex)
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
				}
			});
		}
		public async Task<ApiResultModel> Add_Async(_User entity, Guid logedInUserID)
		{
			// ToDo: Add validation rules, like unique username
			return await Task.Run(() =>
			{
				if (entity != null)
				{
                    try
					{
                        entity._UserID = Guid.NewGuid();
						var sql = @"INSERT INTO _User(_UserID,UserName,PasswordHash,FirstName,LastName,Email,CreatedBy, Created)
											  OUTPUT INSERTED.*
											  VALUES(@_UserID,@UserName,@PasswordHash,@FirstName,@LastName,@Email,@CreatedBy,@Created);";

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            var parameters = new Dictionary<string, object>()
                            {
                                ["_UserID"] = entity._UserID,
                                ["UserName"] = entity.UserName,
                                ["PasswordHash"] = Convert.ToBase64String(Encoding.ASCII.GetBytes(entity.PasswordHash)),
                                ["FirstName"] = entity.FirstName,
                                ["LastName"] = entity.LastName,
                                ["Email"] = entity.Email,
                                ["Created"] = DateTime.UtcNow.ToString(),
                                ["CreatedBy"] = logedInUserID,
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
						return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
					}
				} else
                {
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = "Invalid entity." };
				}
			});
		}

		public async Task<ApiResultModel> Remove_Async(_User entity, Guid logedInUserID)
		{
			return await Task.Run(() =>
			{
				if (entity != null)
				{
					try
					{
						var entityID = Guid.NewGuid().ToString();
						var sql = @"UPDATE _User SET 
                                        Removed = @currentDateTime,
							            RemovedBy = @RemovedBy
                                        OUTPUT INSERTED.*
							            WHERE _UserID = @_UserID";

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            var parameters = new Dictionary<string, object>()
                            {
                                ["_UserID"] = entity._UserID,
                                ["currentDateTime"] = entity.Removed.ToString(),
                                ["RemovedBy"] = logedInUserID
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
						return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
					}
				}
				else
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = "Invalid entity." };
				}
			});
		}

		/// <summary>
		/// Fix Paswordhash interaction
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public async Task<ApiResultModel> Update_Async(_User entity, Guid logedInUserID)
		{
            return await Task.Run(() =>
			{
				if (entity != null)
				{
					try
					{
                        var entityID = Guid.NewGuid().ToString();
						var sql = @"UPDATE _User SET 
										UserName = @UserName,
										FirstName = @FirstName,
										LastName = @LastName,
                                        Email = @Email,
										ModifiedBy = @ModifiedBy,
										Modified = @Modified
										OUTPUT INSERTED.*
                                         WHERE _UserID = @_UserID";
                        //var conn = new SqlConnection { ConnectionString = _connectionString };
                        //using (var command = new SqlCommand(sql, conn))
                        //{
                        //	command.Parameters.Add(new SqlParameter("_UserID", entity._UserID));
                        //                      command.Parameters.Add(new SqlParameter("UserName", entity.UserName));
                        //             command.Parameters.Add(new SqlParameter("FirstName", entity.FirstName ));
                        //	command.Parameters.Add(new SqlParameter("LastName", entity.LastName ));
                        //	command.Parameters.Add(new SqlParameter("Email", entity.Email ));
                        //                      command.Parameters.Add(new SqlParameter("ModifiedBy", logedInUserID));
                        //                      command.Parameters.Add(new SqlParameter("Modified", DateTime.UtcNow));
                        //                      return new ApiResultModel { Success = true, Result = GetRecord(command) };
                        //}
                        using (var connection = new SqlConnection(_connectionString))
                        {
                            var parameters = new Dictionary<string, object>()
                            {
                                ["_UserID"] = entity._UserID,
                                ["UserName"] = entity.UserName,
                                ["FirstName"] = entity.FirstName,
                                ["LastName"] = entity.LastName,
                                ["Email"] = entity.Email,
                                ["ModifiedBy"] = logedInUserID,
                                ["Modified"] = DateTime.UtcNow,
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
						return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
					}
				}
				else
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = "Invalid entity." };
				}
			});
		}

        /// <summary>
        /// Fix Paswordhash interaction
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="logedInUserID"></param>
        /// <returns></returns>
        public async Task<ApiResultModel> ChangePassword_Async(_User entity, Guid logedInUserID)
        {
            return await Task.Run(() =>
            {
                if (entity != null)
                {
                    try
                    {
                        var entityID = Guid.NewGuid().ToString();
                        var sql = @"UPDATE _User SET 
										PasswordHash = @PasswordHash,
										ModifiedBy = @ModifiedBy,
										Modified = @Modified
										OUTPUT INSERTED.*
                                         WHERE _UserID = @_UserID";
                        //var conn = new SqlConnection { ConnectionString = _connectionString };
                        //using (var command = new SqlCommand(sql, conn))
                        //{
                        //    command.Parameters.Add(new SqlParameter("_UserID", entity._UserID));
                        //    command.Parameters.Add(new SqlParameter("PasswordHash", Convert.ToBase64String(Encoding.ASCII.GetBytes(entity.PasswordHash))));

                        //    command.Parameters.Add(new SqlParameter("ModifiedBy", logedInUserID));
                        //    command.Parameters.Add(new SqlParameter("Modified", DateTime.UtcNow));
                        //    return new ApiResultModel { Success = true, Result = GetRecord(command) };
                        //}
                        using (var connection = new SqlConnection(_connectionString))
                        {
                            var parameters = new Dictionary<string, object>()
                            {
                                ["_UserID"] = entity._UserID,
                                ["PasswordHash"] = Convert.ToBase64String(Encoding.ASCII.GetBytes(entity.PasswordHash)),
                                ["ModifiedBy"] = logedInUserID,
                                ["Modified"] = DateTime.UtcNow,
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
                        return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
                    }
                }
                else
                {
					// Add additional logging here.
                    return new ApiResultModel { Success = false, Result = null, ErrorMessage = "Invalid entity." };
                }
            });
        }

        public async Task DeleteByEntityID_Async(String entityID)
		{
			await Task.Run(() =>
			{
				if (entityID != null)
				{
					try
					{
						var sql = @"Delete from _User WHERE _UserID = @_UserID";
                        //var conn = new SqlConnection { ConnectionString = _connectionString };
                        //using (var command = new SqlCommand(sql, conn))
                        //{
                        //	command.Parameters.Add(new SqlParameter("entityID", entityID));
                        //	return new ApiResultModel { Success = true, Result = null };
                        //}
                        using (var connection = new SqlConnection(_connectionString))
                        {
                            var parameters = new Dictionary<string, object>()
                            {
                                ["_UserID"] = entityID
                            };
                            return new ApiResultModel
                            {
                                Success = true,
                                Result = null,
                            };
                        }
                    }
					catch (SqlException ex)
					{
						return new ApiResultModel { Success = false, Result = null, ErrorMessage = ex.InnerException?.StackTrace };
					}
				}
				else
				{
					return new ApiResultModel { Success = false, Result = null, ErrorMessage = "Invalid entity." };
				}
			});
		}

		/// <summary>
		/// PopulateRecord override for the _User entity.
		/// Yes an automapper function could be prudent here but for the sake of this example I decided to go with manual mapping.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		//public override _User PopulateRecord(SqlDataReader reader)
		//{
  //          int index = 0;
		//	return new _User
		//	{
		//		_UserID = reader.GetGuid(index++),
		//		UserName = reader.GetString(index++),
		//		PasswordHash = reader.GetString(index++),
		//		FailedLogins = reader.GetInt16(index++),
		//		FirstName = reader.GetString(index++),
		//		LastName = reader.GetString(index++),
		//		LastLogin = reader[index++] as DateTime? ?? null,
		//		Email = reader.GetString(index++),
		//		// Metadata fields
		//		Created = reader.GetDateTime(index++),
		//		CreatedBy = reader.GetGuid(index++),
		//		Modified = reader[index++] as DateTime? ?? null,
		//		ModifiedBy = reader[index++] as Guid? ?? null,
		//		Removed = reader[index++] as DateTime? ?? null,
		//		RemovedBy = reader[index++] as Guid? ?? null,
		//	};
		//}
    }
}
