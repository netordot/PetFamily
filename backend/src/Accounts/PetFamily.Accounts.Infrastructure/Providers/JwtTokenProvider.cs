using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Application.Authorization;
using PetFamily.SharedKernel.Constraints;

namespace PetFamily.Accounts.Infrastructure.Providers
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
                new Claim(CustomClaims.Email, user.Email),
                new Claim(CustomClaims.Id, user.Id.ToString())
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
