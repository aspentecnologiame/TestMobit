using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestMobit.Domain.Configurations;
using TestMobit.Domain.Entities;

namespace TestMobit.Api.Security
{
    public class JwtBearerToken
    {
        private readonly AuthorizationSettings _authorizationSettings;
        private const string _role = "allVerbs";
        public JwtBearerToken(AuthorizationSettings authorizationSettings)
        {
            _authorizationSettings = authorizationSettings;
        }

        public string GenerateToken(UserEntity userEntity)
        {
            var claims = new[]
            {
                new Claim("userId", userEntity.Id.ToString()),
                new Claim("login", userEntity.Login),
                new Claim(ClaimTypes.Role, _role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authorizationSettings.SecretKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                claims: claims,
                signingCredentials: credential,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _authorizationSettings.Issuer,
                audience: _authorizationSettings.Audience
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
