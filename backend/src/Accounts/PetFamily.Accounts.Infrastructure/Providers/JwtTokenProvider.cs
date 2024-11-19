using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Infrastructure;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Application.Authorization;
using PetFamily.Infrastructure.Authentication.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Authentication.Providers
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly JwtOptions _options;

        public JwtTokenProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims = [
                new Claim(CustomClaims.Sub, user.Id.ToString()),
                new Claim(CustomClaims.Email, user.Email ?? ""),
                new Claim("Permission", "get.pets")
            ];

            var jwtToken = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                expires: DateTime.UtcNow.AddMinutes(Int32.Parse(_options.ExpiredMinutesTime)),
                signingCredentials: credentials,
                claims: claims);    

            var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return stringToken.ToString();

        }
    }
}
