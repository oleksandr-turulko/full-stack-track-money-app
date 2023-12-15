using System.IdentityModel.Tokens.Jwt;

namespace TrackMoney.Services.Jwt
{
    public static class JwtReader
    {
        public static async Task<string> GetIdFromJwt(string jwt)
        {
            if (jwt == null || jwt == "")
                return "";
            var handler = new JwtSecurityTokenHandler();

            var claims = handler.ReadJwtToken(jwt).Claims;

            var res = claims.ElementAt(1).Value;

            return res;
        }
    }
}
