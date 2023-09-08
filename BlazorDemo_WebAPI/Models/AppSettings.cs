using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemo_WebAPI.Models
{
	public class AppSettings
	{
        public string Secret { get; set; }
        public int SignInExpiresInMinutes { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}
