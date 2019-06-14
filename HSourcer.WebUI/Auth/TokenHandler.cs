using HSourcer.Domain.Entities;
using HSourcer.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HSourcer.WebUI.Auth
{
    public class CustomTokenHandler
    {
        private readonly User user;
        public CustomTokenHandler(User user, IOptions<TokenConfig> tokenConfig)
        {
            TokenConfigOptions = tokenConfig;
            this.user = user;
        }

        public IOptions<TokenConfig> TokenConfigOptions { get; }

        public string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(TokenConfigOptions.Value.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, this.user.Id.ToString())
                }),
                Issuer = TokenConfigOptions.Value.Issuer,
                Audience = TokenConfigOptions.Value.Audience,

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
