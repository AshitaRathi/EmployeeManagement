using EmployeeManagement.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Service
{
    public class TokenService : ITokenService
    {
        //private readonly string _key;
        //private readonly string _issuer;
        //private readonly string _audience;

        //public TokenService(IConfiguration configuration)
        //{
        //    // Retrieve secret key and issuer/audience from configuration
        //    _key = configuration["Jwt:SecretKey"];
        //    _issuer = configuration["Jwt:Issuer"];
        //    _audience = configuration["Jwt:Audience"];
        //}

        public string GenerateToken(Employee employee)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecureKeyWith32Characters"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, employee.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7099",
                audience: "https://localhost:7099",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
