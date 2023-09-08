using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace BlazorDemo_WebAPI.Controllers
{
    /// <summary>
    /// WebAPi Contgroller for testing user roles.
    /// </summary>
    [ApiController]
    [Route("CoreApi/[controller]")]
    [Produces("application/json")]
	public class _TestController : Controller
    {
		/// <summary>
		/// Quick test. Accessable to all.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("/roletest_NoRole")]
		public string TestApi_NoRole()
		{
			return "Success";
		}

        /// <summary>
        /// Quick test. Only accessable to useres with the User role.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpGet]
		[Route("/roletest_user")]
		public string RoleTestApi_User()
		{
			return "Success";
        }

        /// <summary>
        /// Quick test. Only accessable to useres with the Admin role.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("/roletest_admin")]
        public string RoleTestApi_Admin()
        {
            return "Success";
        }

        /// <summary>
        /// Quick test. Only accessable to useres with the SuperAdmin role.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        [Route("/roletest_superadmin")]
        public string RoleTestApi_SuperAdmin()
        {
            return "Success";
        }
    }
}
