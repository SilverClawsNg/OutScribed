using Backend.Application.Interfaces;
using Backend.Domain.Models.UserManagement.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Infrastructure.Authentication
{
    public class JwtProvider(IConfiguration configuration) : IJwtProvider
    {

        private readonly IConfiguration _configuration = configuration;

        public string Generate(Account account)
        {
            var claims = new Claim[]
              {
                new(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new(ClaimTypes.Name, account.Username.Value.ToString()),
                new(ClaimTypes.Role,account.Role.ToString()),
              };


            var jwtIssuer = _configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtAudience = _configuration.GetSection("Jwt:Audience").Get<string>();
            var jwtKey = _configuration.GetSection("Jwt:Key").Get<string>();

            var signinCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                jwtIssuer,
                jwtAudience,
                claims,
                DateTime.UtcNow.AddSeconds(1),
                DateTime.UtcNow.AddMinutes(60),
                signinCredentials
                );

            var tokenToString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenToString;
        }
    }
}
