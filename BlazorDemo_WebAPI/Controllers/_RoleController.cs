using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlazorDemo_WebAPI.Repositories;
using Microsoft.Extensions.Options;
using BlazorDemo_WebAPI.Entities;
using BlazorDemo_WebAPI.Models;
using System.Security.Claims;

namespace BlazorDemo_WebAPI.Controllers
{
    /// <summary>
    /// WebAPi Controller for the _Role database table.
    /// url: /_role/
    /// </summary>
    [ApiController]
    [Route("CoreApi/[controller]")]
    [Produces("application/json")]
    public class _RoleController : Controller
    {
        // Dependency injections
        private readonly I_RoleRepository __RoleRepository;

        public _RoleController(IOptions<AppOptions> options)
        {
            var connection = options.Value;
            __RoleRepository = new _RoleRepository(connection.DefaultConnection);
        }

        /// <summary>
        /// Get role names by user Guid
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">{\"TotalTax\":13}</response>
        /// <response code="404">Error: Not Found</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetRoleNamesByUserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<_Role>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResultModel> GetRolesByUserId_Async(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var answer = await Task.Run(() =>
                {
                    return __RoleRepository.GetRolesByUserId_Async(userId);
                });
                return new ApiResultModel { Result = answer.Result, Success = true };
            }
            return new ApiResultModel { Result = null, Success = false };
        }


        /// <summary>
        /// Get role by _Role Guid
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="404">Error: Not Found</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetByEntityID/{entityID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(_Role))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResultModel> GetByEntityID_Async(string entityID)
        {
            if (!string.IsNullOrEmpty(entityID))
            {
                var answer = await Task.Run(() =>
                {
                    return __RoleRepository.GetByEntityID_Async(entityID);
                });
                return new ApiResultModel { Result = answer.Result, Success = true };
            }
            return new ApiResultModel { Result = null, Success = false };
        }

        // CoreApi/_Role/GetNumberOf_Roles
        /// <summary>
        /// Get total number of _Roles entities.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="404">Error: Not Found</response>
        [HttpGet("GetNumberOf_Roles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResultModel> GetNumberOf_Roles_Async()
        {
            var answer = await Task.Run(() =>
            {
                return __RoleRepository.GetNumberOf_Roles_Async();
            });
            return new ApiResultModel { Result = answer.Result, Success = true };
        }

        /// <summary>
        /// Get all _Role entities
        /// </summary>
        /// <returns></returns>
        [HttpGet("all_roles")]
       // [Authorize(Roles = "User,Admin,SuperAdmin")]
        public async Task<ApiResultModel> All_Roles_Async()
        {
            //var logedInUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var answer = await Task.Run(() =>
            {
                return __RoleRepository.All_Async();
            });
            return answer;
        }

        /// <summary>
        /// Add _Role entity to database.
        /// </summary>
        /// <param name="_role"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "RequireSuperAdmin")]
        [Route("add_Role")]
        public async Task<ApiResultModel> add_role([FromBody] _Role _role)
        {
            var logedInUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var userAnswer = await Task.Run(() =>
            {
                return __RoleRepository.Add_Async(_role, logedInUserID);
            });

            return userAnswer;
        }

        /// <summary>
        /// Update existing _Role entity.
        /// </summary>
        /// <param name="_role"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = "RequireSuperAdmin")]
        [Route("update_role")]
        public async Task<ApiResultModel> update_role(_Role _role)
        {
            var logedInUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var userAnswer = await Task.Run(() =>
            {
                return __RoleRepository.Update_Async(_role, logedInUserID);
            });

            return userAnswer;
        }

        /// <summary>
        /// Tag _Role entity as removed and by who and when.
        /// </summary>
        /// <param name="_role"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "RequireSuperAdmin")]
        [Route("remove_role")]
        public async Task<ApiResultModel> remove_role(_Role _role)
        {
            var logedInUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var userAnswer = await Task.Run(() =>
            {
                return __RoleRepository.Remove_Async(_role, logedInUserID);
            });

            return userAnswer;
        }
    }
}
