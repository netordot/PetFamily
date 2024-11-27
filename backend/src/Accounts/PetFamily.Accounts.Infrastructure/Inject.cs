using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Infrastructure.Data;
using PetFamily.Accounts.Infrastructure.Managers;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.Accounts.Infrastructure.Providers;
using PetFamily.Accounts.Infrastructure.Seeding;
using PetFamily.Application.AccountManagement.DataModels;

namespace PetFamily.Accounts.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddAuthorizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
        { 

            services.AddScoped<AccountsDbContext>();

            services.AddOptions<JwtOptions>();

            services.Configure<JwtOptions>(
                configuration.GetSection(JwtOptions.JWT));

            services.Configure<AdminOptions>(
               configuration.GetSection(AdminOptions.ADMIN));

            services.AddTransient<ITokenProvider, JwtTokenProvider>();
            services.AddScoped<IAccountManager, AccountManager>();

            services.AddSingleton<AdminAccountsSeeder>();

            services.AddScoped<AdminAccountsSeederService>();

            services.AddTransient<IRefreshSessionManager, RefreshSessionManager>();

            services.AddIdentity<User, Role>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AccountsDbContext>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var jwtOptions = configuration.GetSection(JwtOptions.JWT).Get<JwtOptions>()
                     ?? throw new ArgumentException("Missing Jwt configuration");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddAuthorization();

            //services.AddSingleton<IAuthorizationHandler, PermissionsRequirementHandler>();

            services.AddManagers();

            return services;
        }

        private static IServiceCollection AddManagers(this IServiceCollection services )
        {
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<PermissionManager>();
            services.AddScoped<RolePermissionManager>();

            return services;    
        }

    }
}
