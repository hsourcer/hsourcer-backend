using HSourcer.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
                Audience = TokenConfigOptions.Value.Issuer,

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
