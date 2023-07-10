using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataAccess.Repositories
{
    public class AuthenticationRepository: IAuthenticationRepository
    {
        private IConfiguration _config;

        public AuthenticationRepository(IConfiguration config)
        {
            _config = config;
        }

        public string GetJwtToken(AccountDTO member)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, member.Email));
            claims.Add(new Claim(ClaimTypes.Name, member.Name));
            foreach (RoleDTO role in member.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role.Code));
            }

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
               audience: _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}
