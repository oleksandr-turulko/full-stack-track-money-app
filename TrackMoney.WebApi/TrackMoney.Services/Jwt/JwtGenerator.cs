
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TrackMoney.Services.Jwt
{
    public class JwtGenerator
    {
        public static string GenerateJWT(string username, Guid userId)
        {
            var claims = new Claim[]
            {
                new(ClaimTypes.Name,username),
                new(ClaimTypes.PrimarySid, userId.ToString())
            };

            return GenerateToken(claims, DateTime.Now.AddDays(7));
        }

        private static string GenerateToken(Claim[] claims, DateTime expires)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = "my megatrone vs all autobots jwt secret";
            var issuer = "My transactions";
            var key = Encoding.UTF8.GetBytes(secret);
            var tokenDescriptior = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);
        }
    }

}


