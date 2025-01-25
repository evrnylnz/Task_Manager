using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_Manager.Models;

namespace Task_Manager.Infrastructure
{
    public static class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(AppUser user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Role, user.AppRole.Role));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            if (!string.IsNullOrEmpty(user.Username))
                claims.Add(new Claim("Username", user.Username));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.ExpireDate);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: JwtTokenDefaults.Issuer,
                audience: JwtTokenDefaults.Audience, claims: claims, notBefore: DateTime.UtcNow,
                expires: expireDate, signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return new TokenResponseDto(handler.WriteToken(jwtSecurityToken), expireDate);
        }
    }
}
