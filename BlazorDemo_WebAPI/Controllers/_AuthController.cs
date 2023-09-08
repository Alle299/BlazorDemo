using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlazorDemo_WebAPI.Repositories;
using BlazorDemo_WebAPI.Entities;
using Microsoft.Extensions.Options;
using BlazorDemo_WebAPI.Models;
using BlazorDemo_WebAPI.Helpers;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Data;

namespace BlazorDemo_WebAPI.Controllers
{
    [ApiController]
	[Route("CoreApi/[controller]")]
	[Produces("application/json")]
	public class _AuthController : Controller
    {
		// Dependency injections
		private readonly AppSettings _appSettings;
		private readonly AppOptions _appOptions;
        // Dependency injection - Repositories
        private readonly I_UserRepository __UserRepository;
		private readonly I_RoleRepository __RoleRepository;

		public _AuthController(IOptions<AppSettings> appSettings,
							   IOptions<AppOptions> appOptions,
                               I_UserRepository _UserRepository,
					           I_RoleRepository _RoleRepository)
		{
			_appSettings = appSettings.Value;
            _appOptions = appOptions.Value;
            __UserRepository = _UserRepository;
			__RoleRepository = _RoleRepository;


		}

        /// <summary>
        /// Uses username and pasword to retrieve the JWT bearer token.
        /// </summary>
        /// <remarks>
        /// Upon successfull connection, grab the "jwtBearer" key in the response, <br />go to the green "Authorize" button att he top of the swagger page and paste it there like this "Bearer #####".
        /// <br /><br />
		/// Test Accouts:<br />
        ///  Username: AleWah<br />
		///  Password: 299792<br />
		///  User Role<br />
		/// <br />
        ///  Username: AleWah2<br />
		///  Password: 299792<br />
		///  User Admin<br />
		/// <br />
        ///  Username: AleWah3<br />
		///  Password: 299792<br />
		///  User SuperAdmin<br />
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid username or password.</response>
        [HttpPost]
        [ProducesResponseType(typeof(LoginModel), 200)]
        [ProducesResponseType(500)]
		[Route("login")]
		public async Task<ApiResultModel> Login([FromBody] LoginModel login)
        {
			try
			{
				// Get user by username and password
				var userAnswer = await Task.Run(() =>
				{
					return new _UserRepository(_appOptions.DefaultConnection).FindByUsernameAndHashedPassword_Async(login.username, login.password);
				});

				if (userAnswer.Result == null)
				{
                    return new ApiResultModel { ErrorMessage = "Invalid username or password.", Success = false };
				}

				// Get user roles
				string _userID = ((_User)userAnswer.Result[0])._UserID.ToString();
				var roleAnswer = await Task.Run(() =>
				{
					return  new _RoleRepository(_appOptions.DefaultConnection).GetRolesByUserId_Async(_userID);
				});

				// Get jwtBearer from user and roles
				var Alle = userAnswer.Result[0];
                var jwtBearer = new JWT(_appSettings).Create(userAnswer.Result[0], roleAnswer.Result);

                return new ApiResultModel { jwtBearer = jwtBearer, Result = userAnswer.Result, Success = true };
			} catch (Exception ex)
            {
				return new ApiResultModel { ErrorMessage = ex.Message ?? "Unknown error", Success = false };
			}
		}
	}
}
