using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Accounts.Infrastructure.Data;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.SharedKernel.Constraints;

namespace PetFamily.Accounts.Infrastructure.Providers
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly JwtOptions _options;
        private readonly AccountsDbContext _context;

        public JwtTokenProvider(
            IOptions<JwtOptions> options,
            AccountsDbContext context
            )
        {
            _options = options.Value;
            _context = context;
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

        public async Task<Guid> GenerateRefreshToken(User user, CancellationToken cancellation)
        {
            var token = new RefreshSession()
            {
                RefreshToken = Guid.NewGuid(),
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(30)
            };

            await _context.RefreshSessions.AddAsync(token);
            await _context.SaveChangesAsync();

            return token.RefreshToken;
        }

    }
}
