using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Data.SqlClient;

using System.Linq.Expressions;
using BlazorDemo_WebAPI.Entities;

namespace BlazorDemo_WebAPI.Models
{
    /// <summary>
    /// Class for the handling and processing of the login form.
    /// </summary>
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
