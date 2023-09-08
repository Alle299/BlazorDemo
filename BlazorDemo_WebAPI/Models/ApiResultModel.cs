using System.Data;
using Microsoft.Data.SqlClient;

using System.Linq.Expressions;
using BlazorDemo_WebAPI.Entities;

namespace BlazorDemo_WebAPI.Models
{
    public class ApiResultModel
    {
        public bool Success { get; set; }
        public dynamic? Result { get; set; }
        public string? ErrorMessage { get; set; }
        public string? jwtBearer { get; set; }
    }
}
