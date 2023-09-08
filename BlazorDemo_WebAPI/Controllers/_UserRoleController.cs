using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using BlazorDemo_WebAPI.Models;
using BlazorDemo_WebAPI.Repositories;

namespace BlazorDemo_WebAPI.Controllers
{
    [ApiController]
    [Route("CoreApi/[controller]")]
    [Produces("application/json")]
    public class _UserRoleController
    {
        // Repositories
        private readonly I_UserRoleRepository __UserRoleRepository;

        public _UserRoleController(IOptions<AppOptions> options)
        {
            var connection = options.Value;
            __UserRoleRepository = new _UserRoleRepository(connection.DefaultConnection);
        }

        /// <summary>
        /// Clear current entities by _UserID and add list if _Role Giuds with single _User Guid.
        /// </summary>
        /// <param name="addition"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "RequireSuperAdmin")]
        [Route("AddRangeWith_UserID_Async")]
        public async Task<ApiResultModel> AddRangeWith_UserID_Async(AdditionForForeignkey addition)
        {
            // AddRangeWith_UserID
            var answer = await Task.Run(() =>
            {
                return __UserRoleRepository.AddRangeWith_UserID_Async(addition);
            });
            return answer;
        }
    }
}
