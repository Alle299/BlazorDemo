using System.Data;
using Microsoft.Data.SqlClient;

using System.Linq.Expressions;
using BlazorDemo_WebAPI.Entities;
using BlazorDemo_WebAPI.Models;

namespace BlazorDemo_WebAPI.Repositories
{
    /// <summary>
    /// Interface for data repository
	/// 
	/// For security reasons data can not be destroyed, only tagged as removed.
    /// </summary>
    public interface I_RoleRepository
	{
		#region DMZ - Default system functions.
		Task<ApiResultModel> GetRolesByUserId_Async(string userId); // IEnumerable<_Role>
		#endregion DMZ

		Task<ApiResultModel> GetNumberOf_Roles_Async();
		Task<ApiResultModel> All_Async(); 
		Task<ApiResultModel> GetByEntityID_Async(string entityID);
		Task<ApiResultModel> GetByName_Async(string entityName);
		Task<ApiResultModel> Add_Async(_Role entity, Guid userID);
		Task<ApiResultModel> Remove_Async(_Role entity, Guid userID);
		Task<ApiResultModel> Update_Async(_Role entity, Guid userID);
	}
}
