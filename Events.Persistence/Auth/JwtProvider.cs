using Events.Application.Interfaces;
using Events.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Mozilla;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Events.Persistence.Auth
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateRefreshToken()
        {
            Claim[] claims =
            [
                new("body", Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))),
            ];
            var refreshToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(_options.RefreshExpiresDays)
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            return tokenValue;
        }

        public string GenerateAccessToken(User user)
        {
            Claim[] claims = 
                [
                    new("userId", user.Id.ToString()),
                    new("Admin", user.IsAdmin.ToString())
                ]; 

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                    signingCredentials: signingCredentials,
                    claims: claims,
                    expires: DateTime.Now.AddHours(_options.ExpiresHours)    
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(accessToken);

            return tokenValue;
        }
    }
}
