using Microsoft.IdentityModel.Tokens;
using BlazorDemo_WebAPI.Entities;
using BlazorDemo_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorDemo_WebAPI.Helpers
{
	public class JWT
	{
		private readonly AppSettings _appSettings;

		public JWT(AppSettings appSettings)
		{
			_appSettings = appSettings;
		}

		public string Create(_User user, List<_Role> roles)
		{
			var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_appSettings.Secret));
			var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.NameId, user._UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email), // this would be the username
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")) // this could be a unique ID assigned to the user by a database
			};

			// Add Role permisions
			claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r.Name)));

			var token = new JwtSecurityToken(
				issuer: _appSettings.ValidIssuer,
				audience: _appSettings.ValidAudience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(_appSettings.SignInExpiresInMinutes),
				signingCredentials: credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
