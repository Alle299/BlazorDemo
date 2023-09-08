using System.ComponentModel.DataAnnotations;

namespace BlazorDemo_BlazorUI.Models
{
    /// <summary>
    /// Must match its counterpart innTierCoreTemplate.Blazor_AdminUI.Models
    /// </summary>
    public class LoginResult
    {
        public string ErrorMessage { get; set; }
        public string UserName { get; set; }
        public string jwtBearer { get; set; }
        public bool success { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "User name is required.")]
        //[EmailAddress(ErrorMessage = "User name is not valid.")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
    public class RegModel : LoginModel
    {
        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Password and confirm password do not match.")]
        public string confirmpwd { get; set; }
    }
}
