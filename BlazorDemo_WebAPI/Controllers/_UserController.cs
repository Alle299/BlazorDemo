using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlazorDemo_WebAPI.Repositories;
using Microsoft.Extensions.Options;
using BlazorDemo_WebAPI.Entities;
using BlazorDemo_WebAPI.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BlazorDemo_WebAPI.Controllers
{
    /// <summary>
    /// WebAPi Controller for the _User database table.
    /// </summary>
    [ApiController]
    [Route("CoreApi/[controller]")]
    [Produces("application/json")]
    public class _UserController : Controller
    {
        // Repositories
        private readonly I_UserRepository __UserRepository;
        private readonly I_RoleRepository __RoleRepository;

        public _UserController(IOptions<AppOptions> options)
        {
            var connection = options.Value;
            __UserRepository = new _UserRepository(connection.DefaultConnection);
            __RoleRepository = new _RoleRepository(connection.DefaultConnection);
        }

        // [Authorize(Roles = "SuperAdmin")]
        // CoreApi/_user/details/{entityID}
        /// <summary>
        /// Get User details by User Guid
        /// </summary>
        /// <remarks>
        /// Example: entityID: c3bac6a4-db49-402d-8e49-9a36292064f4
        /// </remarks>
        /// <param name="entityID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
       // [Authorize(Policy = "RequireSuperAdmin")]
        [Route("details/{entityID}")]
        public async Task<ApiResultModel> Details(string entityID)
        {
          //  var logedInUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userAnswer = await Task.Run(() =>
            {
                return __UserRepository.GetByEntityID_Async(entityID);
            });
            return userAnswer;
        }

        // -----------------

        /// <summary>
        /// Get all _Users
        /// </summary>
        /// <returns>List of _User entities</returns>
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [Route("all_Users")]
        public async Task<ApiResultModel> All_Users()
        {
            //var logedInUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userAnswer = await Task.Run(() =>
            {
                return __UserRepository.All_Async();
            });
            return userAnswer;
        }

        /// <summary>
        /// Add _User entity to database.
        /// </summary>
        /// <param name="_user"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [Route("add_User")]
        public async Task<ApiResultModel> add_User([FromBody]_User _user)
        {
            var logedInUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var userAnswer = await Task.Run(() =>
            {
                return __UserRepository.Add_Async(_user, logedInUserID);
            });

            return userAnswer;
        }

        /// <summary>
        /// Update existing _User entity.
        /// </summary>
        /// <param name="_user"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "SuperAdmin")]
        [Route("update_User")]
        public async Task<ApiResultModel> update_User(_User _user)
        {
            var logedInUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var userAnswer = await Task.Run(() =>
            {
                return __UserRepository.Update_Async(_user, logedInUserID);
            });

            return userAnswer;
        }

        /// <summary>
        /// Tag _User entity as removed and by who and when.
        /// </summary>
        /// <param name="_user"></param>
        /// <returns></returns>
        [HttpPut]
        //    [Authorize(Roles = "SuperAdmin")]
        [Route("remove_User")]
        public async Task<ApiResultModel> Remove_User(_User _user)
        {
            var logedInUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var userAnswer = await Task.Run(() =>
            {
                return __UserRepository.Remove_Async(_user, logedInUserID);
            });

            return userAnswer;
        }

        /// <summary>
        /// Update existing _User entity.
        /// </summary>
        /// <param name="_user"></param>
        /// <returns></returns>
        [HttpPut]
       // [Authorize(Roles = "SuperAdmin")]
        [Route("changepassword_User")]
        public async Task<ApiResultModel> changepassword_User(_User _user)
        {
            var logedInUserID = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var userAnswer = await Task.Run(() =>
            {
                return __UserRepository.ChangePassword_Async(_user, logedInUserID);
            });

            return userAnswer;
        }
    }
}

