using System.Data;
using Microsoft.Data.SqlClient;

using System.Linq.Expressions;
using BlazorDemo_WebAPI.Entities;
using BlazorDemo_WebAPI.Models;

namespace BlazorDemo_WebAPI.Repositories
{
    /// <summary>
    /// Interface of the User entity repository
	/// 
	/// For security reasons data can not be destroyed, only tagged as removed.
    /// </summary>
    public interface I_UserRepository
    {
        Task<ApiResultModel> FindByUsernameAndHashedPassword_Async(string username, string unhashedPassword);
        Task<ApiResultModel> GetByRoleName_Async(string name);

        Task<ApiResultModel> All_Async();

        Task<ApiResultModel> GetNumberOf_Users_Async();
        Task<ApiResultModel> GetByEntityID_Async(String entityID);
        Task<ApiResultModel> GetByName_Async(string entityName);
        Task<ApiResultModel> GetByUserName_Async(string entityName);
        Task<ApiResultModel> Add_Async(_User entity, Guid userID);
        Task<ApiResultModel> Remove_Async(_User entity, Guid userID);
        Task<ApiResultModel> Update_Async(_User entity, Guid userID);
        Task<ApiResultModel> ChangePassword_Async(_User entity, Guid userID);
        
    }
}
