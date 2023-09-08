using Newtonsoft.Json;

namespace BlazorDemo_BlazorUI.Models
{
    public class ApiResultModel
    {
        public bool Success { get; set; }
        public dynamic? Result { get; set; }
        public string? ErrorMessage { get; set; }
        public string? jwtBearer { get; set; }
    }
}
