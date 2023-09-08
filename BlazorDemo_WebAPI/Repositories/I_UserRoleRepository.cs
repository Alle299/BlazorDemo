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
    public interface I_UserRoleRepository
	{
		Task<ApiResultModel> Add_Async(_UserRole entity, int? userId);

		Task<ApiResultModel> GetNumberOfUserRole_Async();
		Task<ApiResultModel> All_Async();
		Task RemovedByEntity_Async(_UserRole entity);
		Task<ApiResultModel> AddRangeWith_UserID_Async(AdditionForForeignkey addition);

    }
}
